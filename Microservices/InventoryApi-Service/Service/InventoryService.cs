using InventoryApi_Service.Data;
using InventoryApi_Service.Dtos;
using InventoryApi_Service.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Service.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly InventoryDbContext _inventoryDbContext;
        private readonly IConfiguration _configuration;

        public InventoryService(InventoryDbContext inventoryDbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _inventoryDbContext = inventoryDbContext;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["ProductApiIP"]);
            _configuration = configuration;
        }

        public async Task<bool> Exist(string SKU)
        {

          return  await _inventoryDbContext.InventoryEntity
                        .Where(x=>x.ProductSKU==SKU)
                        .CountAsync() >0;
        }

        public async Task<InventoryRequestDto?> FindById(int Id)
        {
            return await _inventoryDbContext.InventoryEntity
                        .Where(x => x.Id == Id)
                        .Select(x=> x.Adapt<InventoryRequestDto>())
                        .FirstOrDefaultAsync();
        }

        public async Task IncrementStock(string sku, int count)
        {
            var Inventory = await FindCurrentStock(sku);

            Inventory.Stock = Inventory.Stock + count;

            _inventoryDbContext.InventoryEntity.Update(Inventory);
            await _inventoryDbContext.SaveChangesAsync();
        }

        private async Task<InventoryEntity> FindCurrentStock(string sko) { 
        
            return await _inventoryDbContext.InventoryEntity
                .Where(x => x.ProductSKU == sko)
                .FirstOrDefaultAsync();
        }

        public async Task ReduceStock(string sku, int count)
        {
            var Inventory = await FindCurrentStock(sku);

            var CurrentStock = Inventory.Stock;

            CurrentStock = count >= CurrentStock ? 0 
                : CurrentStock- count;

            Inventory.Stock = CurrentStock;

            _inventoryDbContext.InventoryEntity.Update(Inventory);
            await _inventoryDbContext.SaveChangesAsync();

        }

        public async Task Remove(int Id)
        {
            var Entity = new InventoryEntity();
            Entity.Id = Id;

            _inventoryDbContext.InventoryEntity.Remove(Entity);
            await _inventoryDbContext.SaveChangesAsync();
        }

        public async Task Save(InventoryRequestDto requestDto)
        {

            var Entity = requestDto.Adapt<InventoryEntity>();

            await _inventoryDbContext.InventoryEntity.AddAsync(Entity);
            await _inventoryDbContext.SaveChangesAsync();
        
        }

        public async Task<int> CurrentStock(string SKU)
        {
            return await _inventoryDbContext.InventoryEntity
                                    .Where(x => x.ProductSKU == SKU)
                                    .Select(x => x.Stock)
                                    .FirstOrDefaultAsync();
        }
    }
}
