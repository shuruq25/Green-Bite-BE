using Microsoft.AspNetCore.Mvc;
using src.Entity;


namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private static List<Cart> carts = new List<Cart>
        {
            new Cart { Id = 1, Quantity = 2 },
            new Cart { Id = 2, Quantity = 5 }
        };

        [HttpGet]
        public ActionResult GetCarts()
        {
            return Ok(carts);
        }

        [HttpGet("{id}")]
        public ActionResult GetCartById(int id)
        {
            var foundCart = carts.FirstOrDefault(c => c.Id == id);
            if (foundCart == null)
            {
                return NotFound();
            }
            return Ok(foundCart);
        }

        [HttpPost]
        public ActionResult CreateCart( Cart newCart)
        {
            if (newCart == null || newCart.Quantity <= 0)
            {
                return BadRequest();
            }
            carts.Add(newCart);
            return CreatedAtAction(nameof(GetCartById), new { id = newCart.Id }, newCart);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCart(int id,  Cart updatedCart)
        {
            var foundCart = carts.FirstOrDefault(c => c.Id == id);
            if (foundCart == null)
            {
                return NotFound();
            }

            if (updatedCart.Quantity > 0)
            {
                foundCart.Quantity = updatedCart.Quantity;
            }

            return Ok(foundCart);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCart(int id)
        {
            var foundCart = carts.FirstOrDefault(c => c.Id == id);
            if (foundCart == null)
            {
                return NotFound();
            }

            carts.Remove(foundCart);
            return NoContent();
        }
    }
}
