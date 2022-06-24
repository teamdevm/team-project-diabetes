using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Diabetes.MVC.Models
{
    public class InsulinGraphics
    {
        public string Categorical { get; set; }
        public double Value { get; set; }
    }

    public class StatisticsInsulinViewModel
    {
        [DisplayName("Дополнительный параметр")]
        public string InsulinAdditional { get; set; }

        [DisplayName("Временной промежуток")]
        public string InsulinTimePeriod { get; set; }

        public List<double> Values;
        public List<string> Categorical;
        public List<InsulinGraphics> GetData() =>
            Categorical.Zip(Values, (a, b) => new InsulinGraphics() { Categorical = a, Value = b }).ToList();
        public string ReturnUrl { get; set; }
    }
}
