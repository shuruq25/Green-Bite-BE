using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order
            {
                ID = 1,
                Status = Order.OrderStatus.Pending,
                UserID = 1,
                CreatedAt = DateTime.Now,
                PaymentID = 1,
                OriginalPrice = 10,
                EstimatedArrival = DateTime.Now.AddDays(1),
            },
            new Order
            {
                ID = 2,
                Status = Order.OrderStatus.Shipped,
                UserID = 2,
                CreatedAt = DateTime.Now,
                PaymentID = 2,
                OriginalPrice = 100,
                EstimatedArrival = DateTime.Now.AddDays(1),
            },
            new Order
            {
                ID = 3,
                Status = Order.OrderStatus.Delivered,
                UserID = 3,
                CreatedAt = DateTime.Now,
                PaymentID = 3,
                OriginalPrice = 1000,
                EstimatedArrival = DateTime.Now.AddDays(1),
            },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(_orders);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            _orders.Add(order);
            return CreatedAtAction(nameof(GetAll), new { id = order.ID }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Order order)
        {
            var index = _orders.FindIndex(o => o.ID == id);
            if (index != -1)
            {
                _orders[index] = order;
                return Ok(_orders[index]);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var index = _orders.FindIndex(o => o.ID == id);
            if (index != -1)
            {
                _orders.RemoveAt(index);
                return Ok();
            }
            return NoContent();
        }
    }
}
