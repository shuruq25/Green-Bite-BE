using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class DietaryGoal
    {
        public Guid DietaryGoalID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}