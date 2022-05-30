using System;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.CreateGlucoseLevel
{
    public class CreateGlucoseLevelCommand:IRequest<Unit>
    {
                public Guid UserId { get; set; }
                public double ValueInMmol { get; set; }
                public DateTime MeasuringDateTime { get; set; }
                public string BeforeAfterEating { get; set; }
                public string Comment { get; set; }
    }
}