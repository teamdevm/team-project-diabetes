using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.GetGlucoseLevel
{
    public class GetGlucoseLevelCommand: IRequest<Domain.GlucoseLevel>
    {
        public Guid Id { get; set; }
    }
}
