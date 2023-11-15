using OrdersApi.Dtos;

namespace OrdersApi.Services
{
    public interface IOrderService
    {
        public Task Save(OrderRequestDto requestDto);

        public Task<OrderResponsetDto> FindById(int id);

        public Task<PaginatedDataDto<OrderResponsetDto>> FindAll(int PageIndex, int PageSize);
    }
}
