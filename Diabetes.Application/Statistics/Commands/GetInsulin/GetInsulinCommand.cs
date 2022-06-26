using System;
using Diabetes.Domain.Enums;
using Diabetes.Domain;
using System.Collections.Generic;
using MediatR;

namespace Diabetes.Application.Statistics.Commands
{
    public class GetInsulinCommand : IRequest<List<InsulinNote>>
    {
        public Func<InsulinType, bool> InsulinFilter { get; set; }
        public Func<DateTime, bool> DateFilter { get; set; }
    }
}
