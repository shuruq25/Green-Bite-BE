using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Entity
{
    public class Payment
    {
        public int Id { get; set;}
        public decimal FinalPrice { get; set; }
        public PaymentMethod? Method { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public int CouponId{ get; set; }

    }
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        Cash,
        VISA,
        Other


    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

}