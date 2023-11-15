using InventoryApi_Service.Dtos;
using InventoryApi_Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi_Service.Controllers
{

    [ApiController]
    [Route("api/inventory")]
    public class InventoryController: ControllerBase
    {

        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public async Task<ActionResult> Save([FromBody] InventoryRequestDto inventory) {

           await _inventoryService.Save(inventory);

            return CreatedAtAction( nameof(Save) ,"");
        }

        [HttpGet("exist/{sku}")]
        public async Task<ActionResult<bool>> Exist([FromRoute] string sku)
        {

            var Exist= await _inventoryService.Exist(sku);

            return Ok(Exist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryRequestDto>> FindById([FromRoute] int id)
        {

            var Exist = await _inventoryService.FindById(id);

            return Ok(Exist);
        }

        [HttpGet("currentstock/{sku}")]
        public async Task<ActionResult<int>> CurrentStock([FromRoute] string sku)
        {

            var Stock= await _inventoryService.CurrentStock(sku);


            return Ok(Stock);
        }

        [HttpPut("reducestock/{sku}/{count}")]
        public async Task<ActionResult<InventoryRequestDto>> ReduceStock([FromRoute] string sku, [FromRoute] int count)
        {

            await _inventoryService.ReduceStock(sku, count);
            return NoContent();
        }

        [HttpPut("increasestock/{sku}/{count}")]
        public async Task<ActionResult<InventoryRequestDto>> IncrementStock([FromRoute] string sku, [FromRoute] int count)
        {

           await _inventoryService.IncrementStock(sku, count);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Remove([FromRoute] int id)
        {
            await _inventoryService.Remove(id);

            return NoContent();
        }

        

    }
}
