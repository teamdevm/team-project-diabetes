using System;
using Diabetes.Domain.Normalized.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Application.Statistics.Commands
{
    public class GetGlucoseCommand
    {
        public Predicate<MeasuringTimeType> MeasuringTimeFilter { get; set; }
        public Predicate<DateTime> DateFilter { get; set; }
    }
}
