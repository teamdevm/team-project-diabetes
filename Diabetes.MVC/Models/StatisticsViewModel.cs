using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Models
{
    public class StatisticsViewModel
    {
        [DisplayName("Дополнительный параметр для глюкозы")]
        public string GlucoseAdditional { get; set; }

        [DisplayName("Дополнительный параметр для инсулина")]
        public string InsulinAdditional { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Временной промежуток для глюкозы")]
        public string GlucoseTimePeriod { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Временной промежуток для инсулина")]
        public string InsulinTimePeriod { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Временной промежуток для углеводов")]
        public string CarbohydratesTimePeriod { get; set; }

        [DisplayName("Начало")]
        public string CustomTimeStart { get; set; }

        [DisplayName("Конец")]
        public string CustomTimeEnd { get; set; }

        public string ReturnUrl { get; set; }
    }
}
