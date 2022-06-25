using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.Domain.Enums;
using Diabetes.MVC.Attributes.Validation;

namespace Diabetes.MVC.Models.Glucose
{
    public class CreateGlucoseLevelViewModel
    { 
        [Required(ErrorMessage = "Обязательное поле")]
        [Double(1,20, ErrorMessage = "Допустимы числа от 1 до 20, с двумя знаками после запятой")]
        [DisplayName("Значение в ммоль/л")]
        public string ValueInMmol {
            get => _valueInMmol?.Replace(',','.');
            init => _valueInMmol = value;
        }

        private readonly string _valueInMmol = "";
        
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