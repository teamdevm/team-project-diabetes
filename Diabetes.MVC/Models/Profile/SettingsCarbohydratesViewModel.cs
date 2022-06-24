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

        [Double(0, 999, ErrorMessage = "Значение должно быть целым числом от 0 до 999")]
        [DisplayName("Количество углеводов в 1 ХЕ")]
        public int? CarbohydrateInBreadUnit { get; set; }
        [DisplayName("Единица измерения употребленных углеводов")]
        public CarbohydrateUnits CarbohydrateUnitsUsed { get; set; } = CarbohydrateUnits.Carbohydrate;
    }
}
