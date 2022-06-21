﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;
using Diabetes.Domain.Normalized.Enums;
using Diabetes.Domain.Normalized.Enums.Units;
using static Diabetes.MVC.Models.Profile.CustomValidation;

namespace Diabetes.MVC.Models
{
    public class SettingsGlucoseLevelViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Целевое значение глюкозы в крови до еды")]
        [Double(1, 2000, ErrorMessage = "Допустимы числа от 1 до 2000, с двумя знаками после запятой")]
        public string ValueBeforeEating
        {
            get => _valueBeforeEating?.Replace(',', '.');
            init => _valueBeforeEating = value;
        }

        private readonly string _valueBeforeEating = "";

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Целевое значение глюкозы в крови после еды")]
        [Double(1, 2000, ErrorMessage = "Допустимы числа от 1 до 2000, с двумя знаками после запятой")]
        public string ValueAfterEating
        {
            get => _valueAfterEating?.Replace(',', '.');
            init => _valueAfterEating = value;
        }

        private readonly string _valueAfterEating = "";

        [DisplayName("Единица измерения уровня глюкозы в крови")]
        public GlucoseUnits GlucoseUnits { get; set; }
    }
}