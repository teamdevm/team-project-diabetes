using System;
using System.Collections.Generic;
using Diabetes.Domain;
using MediatR;

namespace Diabetes.Application.Carbohydrate.Commands.AddCarbohydrate
{
    public class AddCarbohydrateCommand:IRequest<Unit>
    {
        public Guid MealId { get; set; }
        public Guid UserId { get; set; }
        public double Value { get; set; }
        public DateTime CreatingDateTime { get; set; }
        public string Comment { get; set; }
        public List<FoodPortion> FoodPortions { get; set; }
    }
}