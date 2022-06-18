using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;

namespace Diabetes.MVC.Models.UsersFood
{
    public class AddUsersFoodViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Ккал")]
        [Double(1,1000, ErrorMessage = "Допустимы числа от 1 до 1000, с двумя знаками после запятой")]
        public string Kcal {
            get => _kcal?.Replace(',','.');
            init => _kcal = value;
        }
        private readonly string _kcal = "";
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Жиры")]
        [Double(1,100, ErrorMessage = "Допустимы числа от 1 до 100, с двумя знаками после запятой")]
        public string Fat {
            get => _fat?.Replace(',','.');
            init => _fat = value;
        }
        private readonly string _fat = "";
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Белки")]
        [Double(1,100, ErrorMessage = "Допустимы числа от 1 до 100, с двумя знаками после запятой")]
        public string Protein {
            get => _protein?.Replace(',','.');
            init => _protein = value;
        }
        private readonly string _protein = "";
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Углеводы")]
        [Double(1,100, ErrorMessage = "Допустимы числа от 1 до 100, с двумя знаками после запятой")]
        public string Carbohydrate  {
            get => _сarbohydrate?.Replace(',','.');
            init => _сarbohydrate = value;
        }
        private readonly string _сarbohydrate = "";

        [DisplayName("Название")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        
        [DisplayName("Описание")]
        public string Details { get; set; }
    }
}