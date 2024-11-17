using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO
{
    
    public class MealPlanMealDTO
    {
        public class MealPlanMealCreateDto
        {
            public Guid ProductId { get; set; }
        }

        public class MealPlanMealReadDto
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }

        }

        public class MealPlanMealUpdateDto
        {
            public Guid ProductId { get; set; }
        }
    }
}
   
