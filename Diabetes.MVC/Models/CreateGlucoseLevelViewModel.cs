using System;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Models
{
    public class CreateGlucoseLevelViewModel
    { 
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0,20, ErrorMessage = "Значение должно быть от 0 до 20")]
        public double ValueInMmol { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime MeasuringDateTime { get; set; }
        public string Comment { get; set; }
        public string ReturnUrl { get; set; }
    }
}