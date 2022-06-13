using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diabetes.Domain;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.GetGlucoseLevel
{
    public class GetGlucoseLevelCommand: IRequest<GlucoseNote>
    {
        public Guid Id { get; set; }
    }
}
