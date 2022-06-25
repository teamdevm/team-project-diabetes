using System;
using MediatR;

namespace Diabetes.Application.UsersFood.Commands.GetUsersFoodItem
{
    public class GetUsersFoodItemCommand:IRequest<Domain.UsersFood>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}