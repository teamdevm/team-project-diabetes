using System;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.DeleteGlucoseLevel
{
    class DeleteGlucoseLevelCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public DateTime MeasuringDateTime { get; set; }
    }
}
