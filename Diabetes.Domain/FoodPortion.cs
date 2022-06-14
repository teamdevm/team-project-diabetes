using System;
using Diabetes.Domain.Normalized;

namespace Diabetes.Domain
{
    public class FoodPortion
    {
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        
        public Guid MealId { get; set; }
        public Meal Meal { get; set; } 
        
        public Guid UserId { get; set; }
        public int MassInGr { get; set; }
    }
}