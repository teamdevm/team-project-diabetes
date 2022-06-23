using System;
using MediatR;

namespace Diabetes.Application.Carbohydrate.Commands.DeleteCarbohydrate
{
    public class DeleteCarbohydrateCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}