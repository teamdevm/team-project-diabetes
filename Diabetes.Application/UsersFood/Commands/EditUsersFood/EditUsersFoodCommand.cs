using System;
using MediatR;

namespace Diabetes.Application.UsersFood.Commands.EditUsersFood
{
    public class EditUsersFoodCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Kcal { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Carbohydrate { get; set; }
    }
}