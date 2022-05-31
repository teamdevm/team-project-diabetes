using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Models
{
    public class CreateInsulinViewModel
    { 
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0,100, ErrorMessage = "Значение должно быть от 0 до 100")]
        [DisplayName("Значение (ед.)")]
        public double Value { get; set; }
        
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