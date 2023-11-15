using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Dtos;
using ProductsApi.Service;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Save([FromBody] ProductRequestDto productRequest)
        {

            var Product =await _productService.Save(productRequest);

            return CreatedAtAction(nameof(Save), Product);
        }


        [HttpGet]
        public async Task<ActionResult<PaginatedDataDto<ProductResponseDto>>> FindAll([FromQuery] int PageIndex, [FromQuery] int PageSize )
        {
            var Products = await _productService.FindAll(PageIndex, PageSize);

            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaginatedDataDto<ProductResponseDto>>> FindById([FromRoute] int id)
        {
            var Product = await _productService.FindById(id);

            return Ok(Product);
        }

        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<PaginatedDataDto<ProductResponseDto>>> FindBySKU([FromRoute] string sku)
        {
            var Product = await _productService.FindBySKU(sku);

            return Ok(Product);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProductRequestDto productRequest)
        {
            await _productService.Update(id, productRequest);

            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _productService.Delete(id);

            return NoContent();
        }

        [HttpGet("exist/{id}")]
        public async Task<ActionResult<bool>> Exist([FromRoute] int id)
        {

            var Exist = await _productService.Exist(id);

            return Ok(Exist);
        }


    }
}