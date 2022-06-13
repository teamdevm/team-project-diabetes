using System;
using System.Diagnostics.CodeAnalysis;
using Diabetes.Domain.Normalized.Enums.Units;
using Diabetes.Domain.Normalized.Enums.User;
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
        
        public TimeSpan? BreakfastTime { get; set; }
        public TimeSpan? AfternoonSnackTime { get; set; }
        public TimeSpan? LunchTime { get; set; }
        public TimeSpan? DinnerTime { get; set; }

        public int CarbohydrateInBreadUnit { get; set; } = 12;
        
        public double? NormalGlucoseBeforeEating { get; set; }
        public double? MinimalGlucoseBeforeEating { get; set; }
        public double? MaximalGlucoseBeforeEating { get; set; }
        
        public double? NormalGlucoseAfterEating { get; set; }
        public double? MinimalGlucoseAfterEating { get; set; }
        public double? MaximalGlucoseAfterEating { get; set; }

        public GlucoseUnits GlucoseUnits { get; set; } = GlucoseUnits.MmolPerLiter;
        public InsulinUnits InsulinUnits { get; set; } = InsulinUnits.Unit;
        public CarbohydrateUnits CarbohydrateUnits { get; set; } = CarbohydrateUnits.Carbohydrate;
    }
}