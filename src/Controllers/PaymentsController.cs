using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        public  List<Payment> payments = new List<Payment>
    {
        new Payment { Id = 1, FinalPrice = 100.00m, Method = PaymentMethod.CreditCard, PaymentDate = DateTime.Now, Status = PaymentStatus.Completed },
        new Payment { Id = 2, FinalPrice = 50.00m, Method = PaymentMethod.PayPal, PaymentDate = DateTime.Now, Status = PaymentStatus.Pending },
    };

    [HttpGet]
    public ActionResult <Payment> GetPayments()
    {
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public ActionResult<Payment> GetPaymentById(int id)
    {
        var foundPayment = payments.FirstOrDefault(p => p.Id == id);
        if (foundPayment == null)
        {
            return NotFound("Payment not found.");
        }
        return Ok(foundPayment);
    }

    [HttpPost]
    public ActionResult<Payment> CreatePayment(Payment newPayment)
    {
        if (newPayment == null || newPayment.FinalPrice <= 0)
        {
            return BadRequest("Invalid payment data.");
        }
        
        payments.Add(newPayment);
        return CreatedAtAction(nameof(GetPaymentById), new { id = newPayment.Id }, newPayment);
    }

    [HttpPut("{id}")]
    public ActionResult<Payment> UpdatePayment(int id, Payment updatedPayment)
    {
        var foundPayment = payments.FirstOrDefault(p => p.Id == id);
        if (foundPayment == null)
        {
            return NotFound("Payment not found.");
        }

        if (updatedPayment.FinalPrice > 0)
        {
            foundPayment.FinalPrice = updatedPayment.FinalPrice;
        }

        if (updatedPayment.Method != null) 
        {
            foundPayment.Method = updatedPayment.Method;
        }

        if (updatedPayment.PaymentDate != default)
        {
            foundPayment.PaymentDate = updatedPayment.PaymentDate;
        }

        if (updatedPayment.Status != default)
        {
            foundPayment.Status = updatedPayment.Status;
        }

        return Ok(foundPayment);
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePayment(int id)
    {
        var foundPayment = payments.FirstOrDefault(p => p.Id == id);
        if (foundPayment == null)
        {
            return NotFound("Payment not found.");
        }

        payments.Remove(foundPayment);
        return NoContent();
    } 
        
    }
}