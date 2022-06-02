using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Models
{
    public class DeleteInsulinViewModel
    { 
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Guid записи")]
        public string Id { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}