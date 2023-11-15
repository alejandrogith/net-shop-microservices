using InventoryApi_Service.Dtos;

namespace InventoryApi_Service.Service
{
    public interface IInventoryService
    {
        public Task Save(InventoryRequestDto requestDto);
        
        public Task Remove(int Id);

        public Task ReduceStock(string sku,int count);
        public Task IncrementStock(string sku, int count);

        public Task<InventoryRequestDto> FindById(int Id);
        public Task<bool> Exist(string SKU);

        public Task<int> CurrentStock(string SKU);
    }
}
