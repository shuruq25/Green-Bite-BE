using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;

namespace src.DTO
{
    public class SubscriptionDTO
    {
 
        public class SubscriptionCreateDto
        {
        public Guid UserId { get; set; }
     
        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; }
        public decimal Price { get; set; }


        }


        public class SubscriptionReadDto
        {
             public Guid ID { get; set; }
        public Guid UserId { get; set; }
        

        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime Start { get;  set;} = DateTime.Now;
        public DateTime End { get; set; }
        public decimal Price { get; set; }


        }


        public class SubscriptionUpdateDto
        {  
        public Guid UserId { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; }
        public decimal Price { get; set; }

        }
    }
}
