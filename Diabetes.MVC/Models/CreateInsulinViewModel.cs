using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;
using Diabetes.Domain.Normalized.Enums;

namespace Diabetes.MVC.Models
{
    public class CreateInsulinViewModel
    { 
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Значение (ед.)")]
        [Double(1,100, ErrorMessage = "Допустимы числа от 1 до 100, с двумя знаками после запятой")]
        public string Value {
            get => _value?.Replace(',','.');
            init => _value = value;
        }

        private readonly string _value = "";
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Дата измерения")]
        public string MeasuringDate { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Время измерения")]
        public string MeasuringTime { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Тип инсулина")]
        public InsulinType InsulinType { get; set; }
        
        [DisplayName("Заметка")]
        public string Comment { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}