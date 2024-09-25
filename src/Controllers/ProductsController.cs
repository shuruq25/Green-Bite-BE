using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        public List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Vitamin D supplement",
                Price = 15.99m,
                Description = "Supports Healthy Bones and Teeth ",
            },
            new Product
            {
                Id = 2,
                Name = "Healing Lotion",
                Price = 30.70m,
                Description = "Delivers Long-Lasting Hydration",
            },
            new Product
            {
                Id = 3,
                Name = " Hair Growth Supplement",
                Price = 7.45m,
                Description = "Advanced Hair Health",
            },
        };

        [HttpGet]
        public ActionResult GetProduct()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            Product? foundProduct = products.FirstOrDefault(p => p.Id == id);
            if (foundProduct == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(foundProduct);
        }

        [HttpPost]
        public ActionResult CreateProduct(Product newProduct)
        {
            if (newProduct == null || string.IsNullOrEmpty(newProduct.Name))
            {
                return BadRequest("Invalid product data.");
            }
            products.Add(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product updatedProduct)
        {
            Product? foundProduct = products.FirstOrDefault(p => p.Id == id);
            if (foundProduct == null)
            {
                return NotFound("Product not found.");
            }
            if (!string.IsNullOrEmpty(updatedProduct.Name))
            {
                foundProduct.Name = updatedProduct.Name;
            }

            if (updatedProduct.Price >= 0)
            {
                foundProduct.Price = updatedProduct.Price;
            }

            if (!string.IsNullOrEmpty(updatedProduct.Description))
            {
                foundProduct.Description = updatedProduct.Description;
            }
            return Ok(foundProduct);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            Product? foundProduct = products.FirstOrDefault(p => p.Id == id);
            if (foundProduct == null)
            {
                return NotFound("Product not found.");
            }

            products.Remove(foundProduct);
            return NoContent();
        }
    }
}
