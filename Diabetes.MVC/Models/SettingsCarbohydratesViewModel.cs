using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;
using Diabetes.Domain.Normalized.Enums;
using Diabetes.Domain.Normalized.Enums.Units;

namespace Diabetes.MVC.Models
{
    public class SettingsCarbohydratesViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Время начала завтрака")]
        public TimeSpan? BreakfastTime { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Время начала обеда")]
        public TimeSpan? LunchTime { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Время начала ужина")]
        public TimeSpan? DinnerTime { get; set; }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public class Int : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value != null && value is int;
            }
        }

        [Int(ErrorMessage = "Значение должно быть целым неотрицательным числом")]
        [DisplayName("Количество углеводов в 1 ХЕ")]
        public int CarbohydrateInBreadUnit { get; set; }
        public CarbohydrateUnits CarbohydrateUnits { get; set; } = CarbohydrateUnits.Carbohydrate;

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        public class MustBeTrueAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value != null && value is bool && (bool)value;
            }
        }

        [MustBeTrue(ErrorMessage = "Изменения не сохранены")]
        public bool Edited { get; set; } = false;
    }
}
