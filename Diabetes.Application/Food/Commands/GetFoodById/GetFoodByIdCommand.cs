using System;
using MediatR;

namespace Diabetes.Application.Food.Commands.GetFoodById
{
    public class GetFoodByIdCommand:IRequest<Domain.Food>
    {
        public Guid Id { get; set; }
    }
}