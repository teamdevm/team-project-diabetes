using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.Domain.Enums;
using Diabetes.MVC.Attributes.Validation;

namespace Diabetes.MVC.Models.UsersFood
{
    public class EditUsersFoodViewModel
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
        [Double(0,100, ErrorMessage = "Допустимы числа от 0 до 100, с двумя знаками после запятой")]
        public string Fat {
            get => _fat?.Replace(',','.');
            init => _fat = value;
        }
        private readonly string _fat = "";
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Белки")]
        [Double(0,100, ErrorMessage = "Допустимы числа от 0 до 100, с двумя знаками после запятой")]
        public string Protein {
            get => _protein?.Replace(',','.');
            init => _protein = value;
        }
        private readonly string _protein = "";
        
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Углеводы")]
        [Double(0,100, ErrorMessage = "Допустимы числа от 0 до 100, с двумя знаками после запятой")]
        public string Carbohydrate  {
            get => _сarbohydrate?.Replace(',','.');
            init => _сarbohydrate = value;
        }
        private readonly string _сarbohydrate = "";

        [DisplayName("Название")]
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Длина должна быть не более 50 символов")]
        public string Name { get; set; }
        
        [DisplayName("Описание")]
        [StringLength(200, ErrorMessage = "Длина должна быть не более 200 символов")]
        public string Details { get; set; }

        public Guid Id { get; set; }
    }
}