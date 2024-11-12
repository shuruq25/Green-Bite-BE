using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
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

        // POST: /api/v1/products
        // Create a new product (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductReadDto>> CreateOne([FromBody] ProductCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Product data is invalid.");
            }

            var productCreated = await _productService.CreateOneAsync(createDto);
            return Created($"api/v1/products/{productCreated.Id}", productCreated);
        }

        // GET: /api/v1/products
        // Get all products with pagination
        [HttpGet]
        public async Task<ActionResult<List<ProductListDto>>> GetAllAsync([FromQuery] PaginationOptions options)
        {
            var productList = await _productService.GetAllAsync(options);
            var totalCount = await _productService.CountProductsAsync();

            var response = new ProductListDto
            {
                Products = productList,
                TotalCount = totalCount
            };

            return Ok(response);
        }

        // GET: /api/v1/products/{id}
        // Get product by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById([FromRoute] Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found." });
            }
            return Ok(product);
        }

        // PUT: /api/v1/products/{id}
        // Update an existing product (Admin only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ProductUpdateDto updateDto)
        {
            var result = await _productService.UpdateOneAsync(id, updateDto);
            if (!result)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }
            var updatedProduct = await _productService.GetByIdAsync(id);
            return Ok(updatedProduct);
        }

        // DELETE: /api/v1/products/{id}
        // Delete a product by ID (Admin only)
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _productService.DeleteOneAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }
            return NoContent();
        }

        // GET: /api/v1/products/search
        // Search products based on various filters and criteria
        // Example: search?filter.category=vitamin&name=xyz&limit=10&offset=0
        [HttpGet("search")]
        public async Task<ActionResult<List<ProductReadDto>>> SearchProducts(
            [FromQuery] PaginationOptions searchOptions,
            [FromQuery] PaginationOptions paginationOptions)
        {
            var products = await _productService.SearchProductsAsync(searchOptions, paginationOptions);

            if (products == null || products.Count == 0)
            {
                return NotFound(new { message = "No products found matching the search criteria." });
            }

            return Ok(products);
        }

        // GET: /api/v1/products/sorted-filtered
        // Get products with sorting and filtering (based on category, price, etc.)
        // Example: api/v1/products/sorted-filtered?filter.category=Vitamins&limit=10&offset=0
        [HttpGet("sorted-filtered")]
        public async Task<ActionResult<List<ProductReadDto>>> GetAllWithSortingAndFiltering(
            [FromQuery] PaginationOptions paginationOptions)
        {
            var products = await _productService.GetAllWithSortingAndFilteringAsync(paginationOptions);

            if (products == null || products.Count == 0)
            {
                return NotFound(new { message = "No products found matching the filtering criteria." });
            }

            return Ok(products);
        }
        
        [HttpPatch("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ProductReadDto>> UpdateOneAsync([FromRoute] Guid id, ProductUpdateDto updateDto)
        {
            var userUpdated = await _productService.UpdateOneAsync(id, updateDto);
            return Ok(userUpdated);
        }

    }
}

