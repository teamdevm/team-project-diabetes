using System;
using Diabetes.Domain.Enums;
using Diabetes.Domain.Normalized.Enums;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.CreateGlucoseLevel
{
    public class CreateGlucoseLevelCommand:IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public double ValueInMmol { get; set; }
        public DateTime MeasuringDateTime { get; set; }
        public MeasuringTimeType MeasuringTimeType { get; set; }
        public string Comment { get; set; }
    }
}