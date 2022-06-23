using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Diabetes.Application.Carbohydrate.Commands.AddCarbohydrate;
using Diabetes.Application.Carbohydrate.Commands.DeleteCarbohydrate;
using Diabetes.Application.Food.Commands.GetFoodById;
using Diabetes.Application.Food.Commands.GetFoodByRangeId;
using Diabetes.Domain;
using Diabetes.MVC.Extensions;
using Diabetes.MVC.Models.Meal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    public class CarbohydrateController:Controller
    {
        private IMediator _mediator;

        public CarbohydrateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        public IActionResult AddMeal()
        {
            var vm = HttpContext.GetMeal();

            return View(vm);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddMeal(MealViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Foods = HttpContext.GetMeal().Foods;
                return View(vm);   
            }
                

            vm.Foods = HttpContext.GetMeal().Foods;

            var carbohydrateNoteId = Guid.NewGuid();
            var command = new AddCarbohydrateCommand
            {
                MealId = carbohydrateNoteId,
                UserId = User.GetId(),
                Value = double.Parse(vm.Value, NumberStyles.Float, CultureInfo.InvariantCulture),
                Comment = vm.Comment,
                CreatingDateTime = DateTime.ParseExact($"{vm.CreatingDate} " + $"{vm.CreatingTime}", "yyyy-MM-dd HH:mm",
                    CultureInfo.InvariantCulture),
                FoodPortions = vm.Foods.Select(f => new FoodPortion
                    {
                        FoodId = f.FoodId, 
                        CarbohydrateNoteId = carbohydrateNoteId,
                        MassInGr = f.MassInGram
                    }
                ).ToList()
            };

            foreach (var foodPortion in command.FoodPortions)
            {
                foodPortion.Food = await _mediator.Send(new GetFoodByIdCommand {Id = foodPortion.FoodId});
            }

            await _mediator.Send(command);
            
            HttpContext.Clear();

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        { 
            if(id == Guid.Empty) 
                return NotFound();
            
            var command = new DeleteCarbohydrateCommand 
            {
                Id = id,
                UserId = User.GetId()
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
    }
}