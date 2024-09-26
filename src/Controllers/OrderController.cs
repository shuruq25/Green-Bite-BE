using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Repository;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        protected IOrderRepository _orders;

        public OrderController(IOrderRepository orders)
        {
            _orders = orders;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return Ok(await _orders.GetAllOrdersAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            var newOrder = await _orders.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetAll), new { id = order.ID }, order);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] Order order)
        {
            if (await _orders.UpdateOrderAsync(id, order))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _orders.DeleteOrderAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
