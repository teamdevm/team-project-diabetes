using System;
using Diabetes.Domain;
using MediatR;

namespace Diabetes.Application.Carbohydrate.Commands.GetCarbohydrateById
{
    public class GetCarbohydrateByIdCommand:IRequest<CarbohydrateNote>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}