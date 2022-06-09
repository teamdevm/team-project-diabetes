using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Models
{
    public class EditGlucoseLevelViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [RegularExpression("^[1-9]([0-9]{0,1})?(\\,[0-9]{1,2})?$", 
            ErrorMessage = "Допустимы числа от 1 до 100, с двумя знаками после запятой и разделителем - \',\'")]
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
        public string BeforeAfterEating { get; set; }

        [DisplayName("Заметка")]
        public string Comment { get; set; }

        public Guid Id { get; set; }
        public string ReturnUrl { get; set; }
    }
}
