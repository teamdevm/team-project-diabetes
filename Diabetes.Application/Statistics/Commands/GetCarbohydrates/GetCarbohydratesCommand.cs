using System;
using Diabetes.Domain;
using System.Collections.Generic;
using MediatR;

namespace Diabetes.Application.Statistics.Commands
{
    public class GetCarbohydratesCommand : IRequest<List<CarbohydrateNote>>
    {
        public Func<DateTime, bool> DateFilter { get; set; }
    }
}
