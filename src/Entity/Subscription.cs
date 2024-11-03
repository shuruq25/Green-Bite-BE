using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;

namespace src.Entity
{
    public class Subscription
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string SubscriptionType { get; set; }

        public int Duration { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; }
        public decimal Price { get; set; }

        public enum SubscriptionStatus
        {
            Active,
            Inactive,
            Pending,
            Canceled,
            Expired
        }


    }
}