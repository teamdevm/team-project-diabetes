using System;
using System.Diagnostics.CodeAnalysis;
using Diabetes.Domain.Enums.User;
using Diabetes.Domain.Normalized.Enums.Units;
using Microsoft.AspNetCore.Identity;

namespace Diabetes.Persistence
{
    public class Account:IdentityUser
    {
        public string Name { get; set; }
        public DiabetesType DiabetesType { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }

        public TimeSpan? BreakfastTime { get; set; } = new TimeSpan(8, 0, 0);
        public TimeSpan? AfternoonSnackTime { get; set; }
        public TimeSpan? LunchTime { get; set; } = new TimeSpan(14, 0, 0);
        public TimeSpan? DinnerTime { get; set; } = new TimeSpan(20, 0, 0);

        public int CarbohydrateInBreadUnit { get; set; } = 12;
        
        public double? NormalGlucoseBeforeEating { get; set; } = 4.2;
        public double? MinimalGlucoseBeforeEating { get; set; }
        public double? MaximalGlucoseBeforeEating { get; set; }

        public double? NormalGlucoseAfterEating { get; set; } = 6.3;
        public double? MinimalGlucoseAfterEating { get; set; }
        public double? MaximalGlucoseAfterEating { get; set; }

        public GlucoseUnits GlucoseUnits { get; set; } = GlucoseUnits.MmolPerLiter;
        public InsulinUnits InsulinUnits { get; set; } = InsulinUnits.Unit;
        public CarbohydrateUnits CarbohydrateUnits { get; set; } = CarbohydrateUnits.Carbohydrate;
    }
}