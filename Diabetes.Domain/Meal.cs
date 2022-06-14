using System;
using System.Collections.Generic;

namespace Diabetes.Domain
{
    public class Meal
    {
        public Guid Id { get; set; }

        public Guid CarbohydrateNoteId { get; set; }
        public CarbohydrateNote CarbohydrateNote { get; set; }

        public List<FoodPortion> Portions { get; set; } = new();
        public List<Food> Foods { get; set; } = new();
    }
}