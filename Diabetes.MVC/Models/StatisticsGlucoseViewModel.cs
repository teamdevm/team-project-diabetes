using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Diabetes.MVC.Models
{
    public class GlucoseGraphics
    {
        public string Categorical { get; set; }
        public double Value { get; set; }
    }

    public class StatisticsGlucoseViewModel
    {
        [DisplayName("Дополнительный параметр")]
        public string GlucoseAdditional { get; set; }

        [DisplayName("Временной промежуток")]
        public string GlucoseTimePeriod { get; set; }

        public List<double> Values;
        public List<string> Categorical;
        public List<GlucoseGraphics> GetData() =>
            Categorical.Zip(Values, (a, b) => new GlucoseGraphics() { Categorical = a, Value = b }).ToList();
        public string ReturnUrl { get; set; }
    }
}
