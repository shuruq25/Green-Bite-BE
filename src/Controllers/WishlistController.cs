using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WishlistController : ControllerBase
    {
        public static List<Wishlist> wishList = new List<Wishlist>
        {
            new Wishlist
            {
                Wishlist_ID = 1,
                Product_ID = 2,
                Date_Added = DateTime.Now,
            },
            new Wishlist
            {
                Wishlist_ID = 2,
                Product_ID = 2,
                Date_Added = DateTime.Now,
            },
            new Wishlist
            {
                Wishlist_ID = 3,
                Product_ID = 2,
                Date_Added = DateTime.Now,
            },
        };

        [HttpGet]
        public ActionResult GetWishlist()
        {
            return Ok(wishList);
        }

        [HttpGet("{id}")]
        public ActionResult GetWishlisttById(int id)
        {
            Wishlist? foundList = wishList.FirstOrDefault(l => l.Wishlist_ID == id);
            if (foundList == null)
            {
                return NotFound();
            }
            return Ok(foundList);
        }

        [HttpPost]
        public ActionResult AddWishlist(Wishlist newList)
        {
            wishList.Add(newList);
            return CreatedAtAction(
                nameof(GetWishlisttById),
                new { id = newList.Wishlist_ID },
                newList
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Deletec(int id)
        {
            Wishlist? foundList = wishList.FirstOrDefault(l => l.Wishlist_ID == id);
            if (foundList == null)
            {
                return NotFound();
            }
            wishList.Remove(foundList);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateWishlist(int id, Wishlist updatedList)
        {
            Wishlist? foundList = wishList.FirstOrDefault(l => l.Wishlist_ID == id);
            if (foundList == null)
            {
                return NotFound();
            }
            foundList.Product_ID = updatedList.Product_ID;
            return Ok(foundList);
        }
    }
}
