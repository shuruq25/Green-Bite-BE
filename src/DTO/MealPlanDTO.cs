using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO
{
    public class MealPlanDTO
    {
       // DTO for creating a new MealPlan
        public class MealPlanCreateDto
        {
            public string Name { get; set; }
            public string MealType { get; set; }
            public string Description { get; set; }
            public decimal TotalCalories { get; set; }
            public string DayOfWeek { get; set; }
            public DateTime? DeliveryTime { get; set; }
            public string NutritionalInfo { get; set; }
            public List<Guid> MealIds { get; set; } = new List<Guid>(); 
        }

        // DTO for reading an existing MealPlan (for example, in an API response)
        public class MealPlanReadDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string MealType { get; set; }
            public string Description { get; set; }
            public decimal TotalCalories { get; set; }
            public string DayOfWeek { get; set; }
            public DateTime? DeliveryTime { get; set; }
            public string NutritionalInfo { get; set; }
            public List<ProductDTO> Meals { get; set; } = new List<ProductDTO>(); 
        }

        // DTO for updating an existing MealPlan
        public class MealPlanUpdateDto
        {
            public string Name { get; set; }
            public string MealType { get; set; }
            public string Description { get; set; }
            public decimal TotalCalories { get; set; }
            public string DayOfWeek { get; set; }
            public DateTime? DeliveryTime { get; set; }
            public string NutritionalInfo { get; set; }
            public List<Guid> MealIds { get; set; } = new List<Guid>(); 
        }
    } 
    }
