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
        public MealType Type { get; set; }
        
        public string Description { get; set; }
        public decimal TotalCalories { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime? DeliveryTime { get; set;}

        public string NutritionalInfo{ get; set; }
         public ICollection<MealPlanMeal> MealPlanMeals { get; set; }
           public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack,
        Dessert,
        Drink
    }
       
    }
}
