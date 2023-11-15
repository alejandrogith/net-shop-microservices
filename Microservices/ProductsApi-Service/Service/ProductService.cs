using Mapster;
using Microsoft.EntityFrameworkCore;

using ProductsApi.Data;
using ProductsApi.Dtos;
using System.Text;
using System.Text.Json;

namespace ProductsApi.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ProductDbContext _dbContext;

        public ProductService(ProductDbContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["Inventory_ServiceIp"]);
            _configuration = configuration;
        }

        public async Task<ProductResponseDto> Save(ProductRequestDto productRequest)
        {
            var Product = productRequest.Adapt<ProductEntity>();

            Product.SKU=Guid.NewGuid().ToString();  

            await _dbContext.ProductEntity.AddAsync(Product);
            await _dbContext.SaveChangesAsync();

            var Inventory = new { productSKU = Product.SKU, stock=0 };

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(Inventory),Encoding.UTF8,"application/json");

            var Response= Product.Adapt<ProductResponseDto>();

            await _httpClient.PostAsync("/api/inventory", jsonContent);


            return Response;
        }

        public async Task Delete(int Id)
        {
            var Proyect = new ProductEntity();
            Proyect.Id= Id;

            _dbContext.ProductEntity.Remove(Proyect);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedDataDto<ProductResponseDto>> FindAll(int PageIndex, int PageSize)
        {
            IQueryable<ProductEntity> Query = _dbContext.ProductEntity;

            var Products= await Query
                        .Skip(PageSize * (PageIndex - 1))
                        .Take(PageSize)
                        .Select(x => x.Adapt<ProductResponseDto>())
                        .AsNoTracking()
                        .ToListAsync();

            var count = await Query.CountAsync();



            return new PaginatedDataDto<ProductResponseDto>(Products, PageIndex, PageSize, count);

        }

        public async Task<ProductResponseDto> FindById(int id)
        {
            var Product = await _dbContext.ProductEntity.Where(x => x.Id == id).FirstOrDefaultAsync();

            return Product.Adapt<ProductResponseDto>();
        }

        public async Task Update(int Id,ProductRequestDto productRequest)
        {
            var Product = productRequest.Adapt<ProductEntity>();
            Product.Id = Id;

           _dbContext.Update(Product);
           await  _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exist(int Id)
        {
            return await _dbContext.ProductEntity
                          .Where(x => x.Id == Id)
                          .CountAsync() > 0;
        }

        public async Task<ProductResponseDto> FindBySKU(string sku)
        {
            var Product = await _dbContext.ProductEntity
                            .Where(x => x.SKU == sku)
                            .FirstOrDefaultAsync();

            return Product.Adapt<ProductResponseDto>();
        }
    }
}
