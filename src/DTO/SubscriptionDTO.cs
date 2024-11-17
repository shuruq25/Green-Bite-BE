using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using static src.Entity.Subscription;

namespace src.DTO
{
    public class SubscriptionDTO
    {

        public class SubscriptionCreateDto
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }

            public int Duration { get; set; }
            public SubscriptionStatus Status { get; set; }
            public DateTime Start { get; set; } = DateTime.UtcNow;
            public DateTime End { get; set; }
            public decimal Price { get; set; }
            public string SubscriptionType { get; set; }


        }


        public class SubscriptionReadDto
        {
            public Guid ID { get; set; }
            public Guid UserId { get; set; }
            public string SubscriptionType { get; set; }

            public string Name { get; set; }

            public int Duration { get; set; }
            public SubscriptionStatus Status { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public decimal Price { get; set; }


        }


        public class SubscriptionUpdateDto
        {
            public string SubscriptionType { get; set; }

            public string Name { get; set; }

            public Guid UserId { get; set; }
            public int Duration { get; set; }
            public SubscriptionStatus Status { get; set; }
            public DateTime Start { get; set; } = DateTime.UtcNow;
            public DateTime End { get; set; }
            public decimal Price { get; set; }

        }
    }
}
