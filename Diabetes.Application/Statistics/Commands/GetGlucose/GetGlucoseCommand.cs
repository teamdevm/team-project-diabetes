using System;
using Diabetes.Domain.Normalized.Enums;
using Diabetes.Domain;
using System.Collections.Generic;
using MediatR;

namespace Diabetes.Application.Statistics.Commands
{
    public class GetGlucoseCommand: IRequest<List<GlucoseNote>>
    {
        public Func<MeasuringTimeType, bool> MeasuringTimeFilter { get; set; }
        public Func<DateTime, bool> DateFilter { get; set; }
    }
}
