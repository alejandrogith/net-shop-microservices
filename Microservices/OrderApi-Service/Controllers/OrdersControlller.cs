using Microsoft.AspNetCore.Mvc;
using OrdersApi.Dtos;
using OrdersApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrdersApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrdersControlller : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrdersControlller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> Save([FromBody] OrderRequestDto value)
        {
              await   _orderService.Save(value);

            return  CreatedAtAction(nameof(Save), null);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedDataDto<OrderResponsetDto>>> FindAll([FromQuery] int PageIndex, [FromQuery] int PageSize)
        {
            var Products = await _orderService.FindAll(PageIndex, PageSize);

            return Ok(Products);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<PaginatedDataDto<OrderResponsetDto>>> FindById([FromRoute] int id)
        {
            var Order = await _orderService.FindById(id);

            return Ok(Order);
        }


    }
}
