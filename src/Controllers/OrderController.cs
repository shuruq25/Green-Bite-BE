using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        protected IMapper _mapper;
        protected IOrderService _orderService;

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderById(Guid id)
        {
            return Ok(await _orderService.GetOrderByIdAsync(id));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDTO.Create order)
        {
            var createdOrderDTO = await _orderService.CreateOneOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrderDTO.ID }, createdOrderDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDTO.Update order)
        {
            if (await _orderService.UpdateOrderAsync(id, order))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _orderService.DeleteByIdAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
