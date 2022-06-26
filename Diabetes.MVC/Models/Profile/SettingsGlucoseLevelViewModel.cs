using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;
using Diabetes.Domain.Normalized.Enums;
using Diabetes.Domain.Normalized.Enums.Units;

namespace Diabetes.MVC.Models
{
    public class SettingsGlucoseLevelViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Целевое значение глюкозы в крови до еды")]
        [Double(0, 20, ErrorMessage = "Допустимы числа от 0 до 20, с двумя знаками после запятой")]
        public string ValueBeforeEating
        {
            get => _valueBeforeEating?.Replace(',', '.');
            init => _valueBeforeEating = value;
        }

        private readonly string _valueBeforeEating = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Целевое значение глюкозы в крови до еды")]
        [Double(0, 360, ErrorMessage = "Допустимы числа от 0 до 360, с двумя знаками после запятой")]
        public string ValueBeforeEatingAlt
        {
            get => _valueBeforeEatingAlt?.Replace(',', '.');
            init => _valueBeforeEatingAlt = value;
        }

        private readonly string _valueBeforeEatingAlt = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Целевое значение глюкозы в крови после еды")]
        [Double(0, 20, ErrorMessage = "Допустимы числа от 0 до 20, с двумя знаками после запятой")]
        public string ValueAfterEating
        {
            get => _valueAfterEating?.Replace(',', '.');
            init => _valueAfterEating = value;
        }

        private readonly string _valueAfterEating = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Целевое значение глюкозы в крови после еды")]
        [Double(0, 360, ErrorMessage = "Допустимы числа от 0 до 360, с двумя знаками после запятой")]
        public string ValueAfterEatingAlt
        {
            get => _valueAfterEatingAlt?.Replace(',', '.');
            init => _valueAfterEatingAlt = value;
        }

        private readonly string _valueAfterEatingAlt = "";

        [DisplayName("Единица измерения уровня глюкозы в крови")]
        public GlucoseUnits GlucoseUnitsUsed { get; set; }
    }
}
