using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class MealPlan
    {


        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PackageType { get; set; }
        public  string ImageUrl { get; set; }

        public string Description { get; set; }
        public decimal TotalCalories { get; set; }
        public List<MealPlanMeal> MealPlanMeals { get; set; }
        public Guid DietaryGoalId { get; set; }

        public DietaryGoal DietaryGoal { get; set; }
        public Guid SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
        public decimal PricePerWeek { get; set; }
        public int Weeks { get; set; }


    }
}
