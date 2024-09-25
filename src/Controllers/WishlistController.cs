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
                WishlistID = 1,
                ProductID = 2,
                DateAdded = DateTime.Now,
                UserID = 1,
            },
            new Wishlist
            {
                WishlistID = 2,
                ProductID = 2,
                DateAdded = DateTime.Now,
                UserID = 2,
            },
            new Wishlist
            {
                WishlistID = 3,
                ProductID = 2,
                DateAdded = DateTime.Now,
                UserID = 3,
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
            Wishlist? foundList = wishList.FirstOrDefault(l => l.WishlistID == id);
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
                new { id = newList.WishlistID },
                newList
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Deletec(int id)
        {
            Wishlist? foundList = wishList.FirstOrDefault(l => l.WishlistID == id);
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
            Wishlist? foundList = wishList.FirstOrDefault(l => l.WishlistID == id);
            if (foundList == null)
            {
                return NotFound();
            }
            foundList.ProductID = updatedList.ProductID;
            return Ok(foundList);
        }
    }
}
