using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Models
{
    public class CreateInsulinViewModel
    { 
        [Required(ErrorMessage = "Обязательное поле")]
        [RegularExpression("^[1-9]([0-9]{0,1})?(\\,[0-9]{1,2})?$", 
            ErrorMessage = "Допустимы числа от 1 до 100, с двумя знаками после запятой и разделителем - \',\'")]
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