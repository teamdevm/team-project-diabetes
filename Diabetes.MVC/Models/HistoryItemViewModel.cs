using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Models
{
    public class HistoryItemViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Guid записи")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Тип записи")]
        public string Type { get; set; }
    }
}
