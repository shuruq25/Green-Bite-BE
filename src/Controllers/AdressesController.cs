using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AdressesController : ControllerBase
    {
        //*************************Create the List field of address class**************************
        public static List<Address> addresses = new List<Address>
        {
            new Address
            {
                AddressId = 1,
                Country = "USA",
                Street = "1st Ave",
                UserId = 101,
            },
            new Address
            {
                AddressId = 2,
                Country = "UK",
                Street = "Baker Street",
                UserId = 102,
            },
            new Address
            {
                AddressId = 3,
                Country = "France",
                Street = "Champs-Élysées",
                UserId = 103,
            },
        };

        //*************************The Logic********************************

        // GET
        [HttpGet]
        public ActionResult GetAddresses()
        {
            return Ok(addresses);
        }

        // GET By ID
        [HttpGet("{id}")]
        public ActionResult GetAddressById(int id)
        {
            var foundAddress = addresses.FirstOrDefault(a => a.AddressId == id);
            if (foundAddress == null)
            {
                return NotFound();
            }
            return Ok(foundAddress);
        }

        // POST
        [HttpPost]
        public ActionResult PostAddress(Address newAddress)
        {
            addresses.Add(newAddress);
            return Created("Created new address successfully", newAddress);
        }

        // DELETE
        [HttpDelete("{id}")]
        public ActionResult DeleteAddress(int id)
        {
            var foundAddress = addresses.FirstOrDefault(a => a.AddressId == id);
            if (foundAddress == null)
            {
                return NotFound();
            }
            addresses.Remove(foundAddress);
            return NoContent();
        }

        // Put
        [HttpPut("{id}")]
        public ActionResult PutAddress(int id, Address updatedAddress)
        {
            var foundAddress = addresses.FirstOrDefault(a => a.AddressId == id);
            if (foundAddress == null)
            {
                return NotFound();
            }
            foundAddress.Street = updatedAddress.Street;
            return Ok(foundAddress);
        }
    }
}
