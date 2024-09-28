using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.product;
using src.Utils;
using static src.DTO.ProductDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        protected IProductService _productService;
        public ProductsController(IProductService service)
        {
            _productService = service;

        }


        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateOne([FromBody] ProductCreateDto createDto)
        {
            var productCreated = await _productService.CreateOneAsync(createDto);
            return Created($"api/v1/products/{productCreated.Id}", productCreated);

        }
        [HttpGet]
        public async Task<ActionResult<List<ProductReadDto>>> GetAll([FromQuery] PaginationOptions paginationOptions)
        {
            var productList = await _productService.GetAllAsync(paginationOptions);
            return Ok(productList);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById([FromRoute] Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ProductUpdateDto updateDto)
        {
            var result = await _productService.UpdateOneAsync(id, updateDto);
            if (!result)
            {
                return NotFound();
            }
            var updatedProduct = await _productService.GetByIdAsync(id);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _productService.DeleteOneAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}