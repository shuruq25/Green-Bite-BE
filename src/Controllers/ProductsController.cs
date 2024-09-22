using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        public List<Product> products = new List<Product>
{
    new Product { Id = 1, Name = "Vitamin D supplement",Price= 15, Description= "Supports Healthy Bones and Teeth\n Supports Healthy Immune System Function"},
    new Product { Id = 2, Name = "Healing Lotion" ,Price=30, Description= "Delivers Long-Lasting Hydration"},
    new Product { Id = 3, Name = " Hair Growth Supplement",Price=7, Description= "Advanced Hair Health"},

};

        [HttpGet]
        public ActionResult GetProduct()
        {
            return Ok(products);
        }



//         [HttpGet("{id}")]
//         public ActionResult GetProductById(int id)
//         {
//             Product? foundProduct = products.FirstOrDefault(p => p.Id == id);
//             if (foundProduct == null)
//             {
//                 return NotFound("there is no product with this ID");
//             }
//             return Ok(foundProduct);


//         }
//         [HttpPost]
//         public ActionResult CreateProduct(Product newProduct)
//         {
//             products.Add(newProduct);
//             return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);

//         }

//         [HttpPut("{id}")]
//         public ActionResult UpdateProduct(int id ,Product newProduct) { 
//           Product? foundProduct = products.FirstOrDefault(p => p.Id == id);
//             if (foundProduct == null)
//             {
//                 return NotFound();
//             }
//             foundProduct.Name=newProduct.Name;
//             return Ok(foundProduct);

//         }


// [HttpDelete("{id}")]
//         public ActionResult DeleteProduct(int id)
//         {
//             Product? foundProduct = products.FirstOrDefault(p => p.Id == id);
//             if (foundProduct == null)
//             {
//                 return NotFound();
//             }

//             products.Remove(foundProduct);
//             return NoContent();
//         }





    }
}