using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Services;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        protected readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPayments()
        {
            return Ok(await _paymentService.GetAllPaymets());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaymentById(Guid id)
        {
            var payment = await _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePayment([FromBody] PaymentDTO.PaymentCreateDto newPayment)
        {
            if (newPayment == null || newPayment.FinalPrice <= 0)
            {
                return BadRequest("Invalid payment data.");
            }
            var createdPaymentDto = await _paymentService.CreatePayment(newPayment);


            return CreatedAtAction(nameof(GetPaymentById), new { id = createdPaymentDto.Id }, createdPaymentDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePayment(Guid id,[FromBody] PaymentDTO.PaymentUpdateDto updatedPayment)
        {
            if (await _paymentService.UpdatePaymentById(id, updatedPayment))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(Guid id)
        {
            if (await _paymentService.DeletePaymentById(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
