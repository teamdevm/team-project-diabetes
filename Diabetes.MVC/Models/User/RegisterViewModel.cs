using System;
using System.ComponentModel.DataAnnotations;

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
        public string? DiabetesType { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Пол")]
        public string? Gender { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Рост")]
        public int? Height { get; set; }
        
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Вес")]
        public int? Weight { get; set; }
    }
}