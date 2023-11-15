using ProductsApi.Data;
using ProductsApi.Dtos;

namespace ProductsApi.Service
{
    public interface IProductService
    {
        public Task<PaginatedDataDto<ProductResponseDto>> FindAll(int PageIndex,int PageSize);
        public Task<ProductResponseDto> FindById(int id);
        public Task<ProductResponseDto> FindBySKU(string sku);
        public Task<ProductResponseDto> Save(ProductRequestDto product);
        public Task Update(int id,ProductRequestDto product);
        public Task Delete(int Id);

        public Task<bool> Exist(int Id);
    }
}
