using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO
{
    
        // DTO for representing the relationship between MealPlan and Meal
    public class MealPlanMealDTO
    {
        public class MealPlanMealCreateDto
        {
            public Guid MealPlanId { get; set; }
            public Guid MealId { get; set; }
        }

        public class MealPlanMealReadDto
        {
            public Guid MealPlanId { get; set; }
            public Guid MealId { get; set; }

        }

        public class MealPlanMealUpdateDto
        {
            public Guid MealPlanId { get; set; }
            public Guid MealId { get; set; }
        }
    }
}
   
