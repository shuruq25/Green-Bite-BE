using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.product;
using static src.DTO.ProductDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        protected IProductService _productService;
        public ProductsController(IProductService service){
            _productService = service;

        }

    
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateOne(ProductCreateDto createDto)
        {
           var productCreated= await _productService.CreateOneAsync(createDto);
          // return Created();
          return Ok(productCreated);
       
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductReadDto>>> GetAll (){
            var productList= await _productService.GetAllAsync();
            return Ok(productList);

        }


    }
}