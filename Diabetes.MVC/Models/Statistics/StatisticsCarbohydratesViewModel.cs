using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Diabetes.MVC.Models
{
    public class CarbohydratesGraphics
    {
        public string Categorical { get; set; }
        public double Value { get; set; }
    }

    public class StatisticsCarbohydratesViewModel
    {
        [DisplayName("Временной промежуток")]
        public string CarbohydratesTimePeriod { get; set; }
        [DisplayName("Единица измерения")]
        public string CarbohydratesUnit { get; set; }
        [DisplayName("Произвольная дата")]
        public string CustomDate { get; set; }

        public List<double> Values;
        public List<string> Categorical;
        public List<CarbohydratesGraphics> GetData() =>
            Categorical.Zip(Values, (a, b) => new CarbohydratesGraphics() { Categorical = a, Value = b }).ToList();
        public string ReturnUrl { get; set; }
    }
}
