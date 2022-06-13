using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;

namespace Diabetes.MVC.Models
{
    public class EditInsulinViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Guid записи")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Значение (ед.)")]
        [Double(1,100, ErrorMessage = "Допустимы числа от 1 до 100, с двумя знаками после запятой")]
        public string Value {
            get => _value?.Replace('.',',');
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
        public string Type { get; set; }
        
        [DisplayName("Заметка")]
        public string Comment { get; set; }

        public string ReturnUrl { get; set; }
    }
}