using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Diabetes.Domain;
using Diabetes.MVC.Attributes.Validation;
using Diabetes.MVC.Models.FoodForNote;

namespace Diabetes.MVC.Models.Meal
{
    public class MealViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Дополнительные углеводы")]
        [Double(0,100, ErrorMessage = "Допустимы числа от 0 до 100, с двумя знаками после запятой")]
        public string Value {
            get => _value?.Replace(',','.');
            set => _value = value;
        }

        private string _value = "0";
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Дата заполнения")]
        public string CreatingDate { get; set; }
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Время заполнения")]
        public string CreatingTime { get; set; }
        
        [DisplayName("Заметка")]
        [StringLength(200, ErrorMessage = "Длина должна быть не более 200 символов")]
        public string Comment { get; set; }

        public List<FoodForNoteViewModel> Foods { get; set; } = new List<FoodForNoteViewModel>();

        public string FoodsValue => Foods
            .Select(f => f.MassInGramNum * f.Food.Carbohydrate / 100).Sum().ToString(CultureInfo.InvariantCulture);

        public Guid Id { get; set; }
    }
}