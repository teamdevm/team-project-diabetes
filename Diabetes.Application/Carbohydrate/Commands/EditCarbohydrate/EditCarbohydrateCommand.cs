using System;
using System.Collections.Generic;
using Diabetes.Domain;
using MediatR;

namespace Diabetes.Application.Carbohydrate.Commands.EditCarbohydrate
{
    public class EditCarbohydrateCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public double Value { get; set; }
        public DateTime CreatingDateTime { get; set; }
        public string Comment { get; set; }
        public List<FoodPortion> FoodPortions { get; set; }
    }
}