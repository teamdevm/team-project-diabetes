using System;
using Diabetes.Domain.Enums;
using Diabetes.Domain.Normalized;

namespace Diabetes.Domain
{
    public class FoodPortion
    {
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        
        public Guid CarbohydrateNoteId { get; set; }
        public CarbohydrateNote CarbohydrateNote { get; set; }

        public int MassInGr { get; set; }
    }
}