using System;
using System.ComponentModel.DataAnnotations;
using Diabetes.Domain.Enums.User;
using Diabetes.MVC.Attributes.Validation;

namespace Diabetes.MVC.Models.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Email")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Повтор пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Тип диабета")]
        public DiabetesType DiabetesType { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [BirthDate(1,105, ErrorMessage = "Вам должно быть не менее 1 месяца и не более 105 лет")]
        public DateTime Birthdate { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }
        
        [Display(Name = "Рост")]
        public int? Height { get; set; }
        
        [Display(Name = "Вес")]
        public int? Weight { get; set; }
    }
}