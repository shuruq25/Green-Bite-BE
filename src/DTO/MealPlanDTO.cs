using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using static src.DTO.MealPlanMealDTO;

namespace src.DTO
{
    public class MealPlanDTO
    {
        // DTO for creating a new MealPlan
        public class MealPlanCreateDto
        {
         public string Name { get; set; }
        public string PackageType { get; set; }
        public  string ImageUrl { get; set; }

        public string Description { get; set; }
        public decimal TotalCalories { get; set; }
        public Guid DietaryGoalId { get; set; }

        public Guid SubscriptionId { get; set; }

        public decimal PricePerWeek { get; set; }
        public int Weeks { get; set; }

            public List<MealPlanMealCreateDto> MealPlanMeals { get; set; } = new List<MealPlanMealCreateDto>();
        }

        // DTO for reading an existing MealPlan 
        public class MealPlanReadDto
        {
            public Guid Id { get; set; }
                public string Name { get; set; }
        public string PackageType { get; set; }
        public  string ImageUrl { get; set; }

        public string Description { get; set; }
        public decimal TotalCalories { get; set; }
        public Guid DietaryGoalId { get; set; }

        public DietaryGoal DietaryGoal { get; set; }
        public Guid SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
        public decimal PricePerWeek { get; set; }
        public int Weeks { get; set; }
            public List<MealPlanMealReadDto> MealPlanMeals { get; set; } = new List<MealPlanMealReadDto>();

        }

        // DTO for updating an existing MealPlan
        public class MealPlanUpdateDto
        {       public string Name { get; set; }
        public string PackageType { get; set; }
        public  string ImageUrl { get; set; }

        public string Description { get; set; }
        public decimal TotalCalories { get; set; }
        public Guid DietaryGoalId { get; set; }

        public Guid SubscriptionId { get; set; }

        public decimal PricePerWeek { get; set; }
        public int Weeks { get; set; }
            public List<MealPlanMealUpdateDto> MealPlanMeals { get; set; } = new List<MealPlanMealUpdateDto>();
        }
    }
}
