using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Diabetes.MVC.Attributes.Validation;
using Diabetes.Domain.Normalized.Enums;
using Diabetes.Domain.Normalized.Enums.Units;
using Diabetes.Domain.Normalized.Enums.User;

namespace Diabetes.MVC.Models
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        //[Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Введите текущий пароль")]
        [DataType(DataType.Password)]
        public string? PasswordOld { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Тип диабета")]
        public DiabetesType DiabetesType { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Display(Name = "Рост")]
        public int? Height { get; set; }

        [Display(Name = "Вес")]
        public int? Weight { get; set; }
    }
}
