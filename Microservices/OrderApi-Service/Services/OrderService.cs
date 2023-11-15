using Mapster;
using Microsoft.EntityFrameworkCore;
using OrderApi_Service.Exeptions;
using OrdersApi.Data;
using OrdersApi.Dtos;
using System.Linq;

namespace OrdersApi.Services
{
    public class OrderService : IOrderService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly OrderDbContext _orderDbContext;

        public OrderService(IHttpClientFactory httpClientFactory, IConfiguration configuration, OrderDbContext orderDbContext)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _orderDbContext = orderDbContext;
        }

        public async Task<PaginatedDataDto<OrderResponsetDto>> FindAll(int PageIndex, int PageSize)
        {
            IQueryable<OrderMasterEntity> Query = _orderDbContext.OrderMasterEntity;

            var Orders = await Query
                        .Skip(PageSize * (PageIndex - 1))
                        .Take(PageSize)
                        .Include(x => x.OrderDetailEntity)
                        .Select(x => new OrderResponsetDto(x.Id, x.EmailUser, x.OrderDetailEntity.Adapt<List<OrderDetailResponseDto>>(), x.TotalCost))
                        .AsNoTracking()
                        .ToListAsync();

            var count = await Query.CountAsync();



            return new PaginatedDataDto<OrderResponsetDto>(Orders, PageIndex, PageSize, count);
        }

        public async Task<OrderResponsetDto> FindById(int id)
        {


            var Order =await _orderDbContext.OrderMasterEntity.Where(x => x.Id == id)
                                                        .Include(x=>x.OrderDetailEntity)
                                                        .Select(x => new OrderResponsetDto(x.Id, x.EmailUser, x.OrderDetailEntity.Adapt<List<OrderDetailResponseDto>>(), x.TotalCost))
                                                       .FirstOrDefaultAsync();

            return Order;
        }

        public async Task Save(OrderRequestDto requestDto)
        {
            await CustomerExist(requestDto.EmailUser);

            var InfoProducts = await GetProductsInfo(requestDto.Products_info);

            var orderMasterEntity = requestDto.Adapt<OrderMasterEntity>();
            orderMasterEntity.CreatedAt = DateTime.Now;
            orderMasterEntity.TotalCost = InfoProducts.Select(x => x.price).Sum();

            await _orderDbContext.OrderMasterEntity.AddAsync(orderMasterEntity);
           await  _orderDbContext.SaveChangesAsync();


            var OrderDetails = new List<OrderDetailEntity>();
         
            InfoProducts.ForEach(product =>
            {
                var OrderDetail= new OrderDetailEntity();
                OrderDetail.ProductSKU = product.sku;
                OrderDetail.OrderMasterId = orderMasterEntity.Id;
                OrderDetail.Cost = product.price;

                OrderDetails.Add(OrderDetail);
            });
         
            _orderDbContext.OrderDetailEntity.AddRange(OrderDetails);
            await _orderDbContext.SaveChangesAsync();


        }


        private async Task<bool> CustomerExist(string Email) {
            var BaseURL = _configuration["Customer-Service"];

           var CustomerExist=await _httpClient.GetFromJsonAsync<bool>($"{BaseURL}/api/customer/exist/{Email}");

            if (!CustomerExist)
                throw new CustomNotFoundException($"The Customer with Email: {Email} does not exist");

            return CustomerExist;
        }

        private async  Task<List<ProductInfoDto>> GetProductsInfo(List<OrderDetailRequestDto> orderDetailrequestDtos) {

            var ProductIp = _configuration["Product-Service"];
            var InventoryIp = _configuration["Inventory-Service"];

            var productsInfo=new List<ProductInfoDto>();


            foreach (var item in orderDetailrequestDtos)
            {
                if (!string.IsNullOrEmpty(item.ProductSKU))
                {
                   
                    var InventoryProductExist = await _httpClient.GetFromJsonAsync<bool>($"{InventoryIp}/api/inventory/exist/{item.ProductSKU}");

                    if (!InventoryProductExist)
                        throw new CustomNotFoundException($"The Product with SKU {item.ProductSKU} does not exist");

                    var CurrentStock = await _httpClient.GetFromJsonAsync<int>($"{InventoryIp}/api/inventory/currentstock/{item.ProductSKU}");

                    if ((CurrentStock - item.Count) < 0)
                        throw new CustomNotFoundException($"The stock quantity of the product with the SKU {item.ProductSKU} exceeds the stock by {item.Count - CurrentStock} units");

                   await _httpClient.PutAsync($"{InventoryIp}/api/inventory/reducestock/{item.ProductSKU}/{item.Count}",null);
                  
                    
                   var Product= await _httpClient.GetFromJsonAsync<ProductInfoDto>($"{ProductIp}/api/product/sku/{item.ProductSKU}");

                   productsInfo.Add(new ProductInfoDto(item.ProductSKU, Product.price* item.Count  ) );
                }
            }



            return productsInfo;
        }




    }
}
