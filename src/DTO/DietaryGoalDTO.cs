using System;

namespace src.DTO
{
    public class DietaryGoalDTO
    {
        public class DietaryGoalCreateDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class DietaryGoalReadDto
        {
            public Guid DietaryGoalID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class DietaryGoalUpdateDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
