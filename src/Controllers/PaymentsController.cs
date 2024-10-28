using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Services;
using src.Utils;
using static src.DTO.PaymentDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: /api/v1/payments
        // Retrieves a list of payments with pagination (Admin only)
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> GetAllPayments(int page = 1, int pageSize = 10)
        {
            var payments = await _paymentService.GetAllPayments(page, pageSize);
            return Ok(payments);
        }

        // GET: /api/v1/payments/{id}
        // Retrieves a payment by its ID (Authenticated user)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PaymentReadDto>> GetPaymentById(Guid id)
        {
            var payment = await _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        // POST: /api/v1/payments
        // Creates a new payment (Authenticated user)
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PaymentReadDto>> CreatePayment([FromBody] PaymentCreateDto newPaymentDto)
        {
            var createdPayment = await _paymentService.CreatePayment(newPaymentDto);
            return CreatedAtAction(
                nameof(GetPaymentById),
                new { id = createdPayment.Id },
                createdPayment
            );
        }

        // DELETE: /api/v1/payments/{id}
        // Deletes a payment by its ID (Authenticated user)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePayment(Guid id)
        {
            var deleted = await _paymentService.DeletePaymentById(id);
            if (deleted)
            {
                return NoContent();
            }
            throw CustomException.NotFound($"Payment with ID {id} not found.");
        }
    }
}

