using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.Domain.Normalized.Enums;

namespace Diabetes.MVC.Models
{
    public class CreateGlucoseLevelViewModel
    { 
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0,20, ErrorMessage = "Значение должно быть от 0 до 20")]
        [DisplayName("Значение в ммоль/л")]
        public double? ValueInMmol { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Дата измерения")]
        public string MeasuringDate { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Время измерения")]
        public string MeasuringTime { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("До или после еды")]
        public MeasuringTimeType MeasuringTimeType { get; set; }
        
        [DisplayName("Заметка")]
        public string Comment { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}