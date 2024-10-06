using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> GetAllPayments(int page = 1, int pageSize = 10)
        {
            var payments = await _paymentService.GetAllPayments(page, pageSize);
            return Ok(payments);
        }

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


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PaymentReadDto>> CreatePayment([FromBody] PaymentCreateDto newPayment)
        {
            if (newPayment == null || newPayment.PaidPrice <= 0)
            {
                throw CustomException.BadRequest("Invalid payment data.");
            }
            var createdPaymentDto = await _paymentService.CreatePayment(newPayment);

            return CreatedAtAction(
                nameof(GetPaymentById),
                new { id = createdPaymentDto.Id },
                createdPaymentDto
            );
        }

        /*[HttpPut("{id}")]*/
        /*[Authorize]*/
        /*public async Task<ActionResult> UpdatePayment(*/
        /*    Guid id,*/
        /*    [FromBody] PaymentDTO.PaymentUpdateDto updatedPayment*/
        /*)*/
        /*{*/
        /*    if (await _paymentService.UpdatePaymentById(id, updatedPayment))*/
        /*    {*/
        /*        return NoContent();*/
        /*    }*/
        /*    throw CustomException.NotFound();*/
        /*}*/

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePayment(Guid id)
        {
            var deleted = await _paymentService.DeletePaymentById(id);
            if (deleted)
            {
                return NoContent();
            }
            throw CustomException.NotFound();
        }
    }
}

