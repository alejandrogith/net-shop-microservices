
namespace OrdersApi.Dtos
{


    public record OrderRequestDto(string EmailUser, List<OrderDetailRequestDto> Products_info);

    public record  OrderDetailRequestDto(string ProductSKU,int Count);


    public record ProductInfoDto(string sku,  decimal price);



    public record OrderResponsetDto(int Id, string EmailUser, List<OrderDetailResponseDto> Products_info,decimal TotalCost);

    public record OrderDetailResponseDto(int Id, int OrderMasterId, string ProductSKU, int Count);












}
