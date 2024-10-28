using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;
using src.Utils;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /api/v1/orders/{id}
        // Retrieve a specific order by ID (Authenticated user)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }

        // GET: /api/v1/orders
        // Retrieve all orders (Admin only)
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // POST: /api/v1/orders
        // Create a new order (Authenticated user)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] OrderDTO.Create order)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return BadRequest("Invalid User ID.");
            }
            order.UserID = parsedUserId;
            var createdOrderDTO = await _orderService.CreateOneOrderAsync(parsedUserId, order);

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = createdOrderDTO.ID },
                createdOrderDTO
            );
        }

        // PUT: /api/v1/orders/{id}
        // Update an existing order by ID (Authenticated user)
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDTO.Update order)
        {
            var updated = await _orderService.UpdateOrderAsync(id, order);
            if (!updated)
            {
                throw CustomException.NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }

        // DELETE: /api/v1/orders/{id}
        // Delete an order by ID (Authenticated user)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _orderService.DeleteByIdAsync(id);
            if (!deleted)
            {
                throw CustomException.NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }
    }
}

