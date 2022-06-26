using System;
using MediatR;

namespace Diabetes.Application.UsersFood.Commands.DeleteUsersFood
{
    public class DeleteUsersFoodCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}