using System;
using System.Collections.Generic;
using MediatR;

namespace Diabetes.Application.Food.Commands.GetFoodByRangeId
{
    public class GetFoodByRangeIdCommand:IRequest<List<Domain.Food>>
    {
        public List<Guid> Ids { get; set; }
    }
}