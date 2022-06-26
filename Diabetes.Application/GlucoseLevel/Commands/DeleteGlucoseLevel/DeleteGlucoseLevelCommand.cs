using System;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.DeleteGlucoseLevel
{
    public class DeleteGlucoseLevelCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
