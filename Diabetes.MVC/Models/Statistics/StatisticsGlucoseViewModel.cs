using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Diabetes.MVC.Models
{
    public class GlucoseGraphics
    {
        public string Categorical { get; set; }
        public double Value { get; set; }
        public double? NormalValueBeforeEating { get; set; }
        public double? NormalValueAfterEating { get; set; }
    }

    public class StatisticsGlucoseViewModel
    {
        [DisplayName("Дополнительный параметр")]
        public string GlucoseAdditional { get; set; }

        [DisplayName("Временной промежуток")]
        public string GlucoseTimePeriod { get; set; }

        [DisplayName("Произвольная дата")]
        public string CustomDate { get; set; }

        public List<double> Values;
        public List<string> Categorical;
        public List<double?> NormalValuesBeforeEating;
        public List<double?> NormalValuesAfterEating;

        public List<GlucoseGraphics> GetData() =>
            Categorical.Zip(Values, (a, b) => new Tuple<string, double>(a, b))
            .Zip(NormalValuesBeforeEating, (a, b) => new Tuple<string, double, double?>(a.Item1, a.Item2, b))
            .Zip(NormalValuesAfterEating, (a, b) => new GlucoseGraphics() 
            { Categorical = a.Item1, Value = a.Item2, NormalValueBeforeEating = a.Item3, NormalValueAfterEating = b})
            .ToList();

        public bool HasNormalBeforeEating { get; set; }
        public bool HasNormalAfterEating { get; set; }
        public string ReturnUrl { get; set; }
    }
}
