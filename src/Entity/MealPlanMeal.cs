using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace src.Entity
{
    public class MealPlanMeal
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }



    }
}