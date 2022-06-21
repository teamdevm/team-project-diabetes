using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;
using Diabetes.Domain.Normalized.Enums;
using Diabetes.Domain.Normalized.Enums.Units;
using static Diabetes.MVC.Models.Profile.CustomValidation;

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

        [Int(ErrorMessage = "Значение должно быть целым неотрицательным числом")]
        [DisplayName("Количество углеводов в 1 ХЕ")]
        public int CarbohydrateInBreadUnit { get; set; }
        [DisplayName("Единица измерения употребленных углеводов")]
        public CarbohydrateUnits CarbohydrateUnits { get; set; } = CarbohydrateUnits.Carbohydrate;
    }
}
