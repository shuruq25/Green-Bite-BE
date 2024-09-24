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
            new Cart { CartId = 1, Quantity = 2 },
            new Cart { CartId = 2, Quantity = 5 }
        };

        [HttpGet]
        public ActionResult GetCarts()
        {
            return Ok(carts);
        }

        [HttpGet("{cartId}")]
        public ActionResult GetCartById(int cartId)
        {
            var foundCart = carts.FirstOrDefault(c => c.CartId == cartId);
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

            newCart.CartId = carts.Max(c => c.CartId) + 1; // Generate new ID
            carts.Add(newCart);
            return CreatedAtAction(nameof(GetCartById), new { cartId = newCart.CartId }, newCart);
        }

        [HttpPut("{cartId}")]
        public ActionResult UpdateCart(int cartId,  Cart updatedCart)
        {
            var foundCart = carts.FirstOrDefault(c => c.CartId == cartId);
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

        [HttpDelete("{cartId}")]
        public ActionResult DeleteCart(int cartId)
        {
            var foundCart = carts.FirstOrDefault(c => c.CartId == cartId);
            if (foundCart == null)
            {
                return NotFound();
            }

            carts.Remove(foundCart);
            return NoContent();
        }
    }
}
