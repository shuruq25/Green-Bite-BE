using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
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
       // [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<ProductReadDto>> CreateOne(
            [FromBody] ProductCreateDto createDto
        )
        {
            var productCreated = await _productService.CreateOneAsync(createDto);
            return Created($"api/v1/products/{productCreated.Id}", productCreated);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductReadDto>>> GetAll(
            [FromQuery] PaginationOptions paginationOptions
        )
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

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] ProductUpdateDto updateDto
        )
        {
            var result = await _productService.UpdateOneAsync(id, updateDto);
            if (!result)
            {
                 throw CustomException.NotFound($"Product with ID {id} not found.");
            }
            var updatedProduct = await _productService.GetByIdAsync(id);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _productService.DeleteOneAsync(id);
            if (!deleted)
            {
                 throw CustomException.NotFound($"Product with ID {id} not found.");
            }
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ProductReadDto>>> SearchProducts(
            [FromBody] PaginationOptions options
        )
        {
            var products = await _productService.SearchProductsAsync(options);
            if (products == null || products.Count == 0)
            {
                 throw CustomException.NotFound("No products found matching the search criteria.");
            }
            return Ok(products);
        }
    }
}
