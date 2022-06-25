using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Diabetes.Application.Carbohydrate.Commands.AddCarbohydrate;
using Diabetes.Application.Carbohydrate.Commands.DeleteCarbohydrate;
using Diabetes.Application.Carbohydrate.Commands.EditCarbohydrate;
using Diabetes.Application.Carbohydrate.Commands.GetCarbohydrateById;
using Diabetes.Application.Food.Commands.GetFoodById;
using Diabetes.Domain;
using Diabetes.MVC.Extensions;
using Diabetes.MVC.Models.FoodForNote;
using Diabetes.MVC.Models.Meal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class CarbohydrateController:Controller
    {
        private IMediator _mediator;

        public CarbohydrateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        public IActionResult Add()
        {
            var vm = HttpContext.GetMeal(HttpContextExtensions.AddKey);

            return View(vm);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(MealViewModel vm)
        {
            vm.Foods = HttpContext.GetMeal(HttpContextExtensions.AddKey).Foods;
            
            if (!ModelState.IsValid)
            {
                return View(vm);   
            }

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
                        MassInGr = f.MassInGramNum
                    }
                ).ToList()
            };

            foreach (var foodPortion in command.FoodPortions)
            {
                foodPortion.Food = await _mediator.Send(new GetFoodByIdCommand {Id = foodPortion.FoodId});
            }

            await _mediator.Send(command);
            
            HttpContext.Clear(HttpContextExtensions.AddKey);

            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var note = await _mediator.Send(new GetCarbohydrateByIdCommand {Id = id, UserId = User.GetId()});

            if (note == null)
                return RedirectToAction("Index", "Home");
            
            var vm = HttpContext.GetMeal(HttpContextExtensions.EditKey);

            if (vm.Id != id)
            {
                vm = new MealViewModel
                {
                    Id = id,
                    Value = note.Value.ToString(),
                    Comment = note.Comment,
                    CreatingDate = note.MeasuringDateTime.ToString("yyyy-MM-dd"),
                    CreatingTime = note.MeasuringDateTime.ToString("HH:mm"),
                    Foods = note.Portions.Select(f =>
                    {
                        f.Food.Portions = new List<FoodPortion>();
                        f.Food.CarbohydrateNotes = new List<CarbohydrateNote>();
                        return new FoodForNoteViewModel
                        {
                            Food = f.Food,
                            FoodId = f.FoodId,
                            MassInGram = f.MassInGr.ToString(CultureInfo.InvariantCulture)
                        };
                    }).ToList()
                };
            
                HttpContext.AddMeal(vm, HttpContextExtensions.EditKey);
            }

            return View(vm);
        }
        
        [Authorize]
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Edit(MealViewModel vm)
        {
            vm.Foods = HttpContext.GetMeal(HttpContextExtensions.EditKey).Foods;
            
            if (!ModelState.IsValid)
            {
                return View(vm);   
            }
            
            var command = new EditCarbohydrateCommand
            {
                Id = vm.Id,
                UserId = User.GetId(),
                Value = double.Parse(vm.Value, NumberStyles.Float, CultureInfo.InvariantCulture),
                Comment = vm.Comment,
                CreatingDateTime = DateTime.ParseExact($"{vm.CreatingDate} " + $"{vm.CreatingTime}", "yyyy-MM-dd HH:mm",
                    CultureInfo.InvariantCulture),
                FoodPortions = vm.Foods.Select(f => new FoodPortion
                    {
                        FoodId = f.FoodId, 
                        CarbohydrateNoteId = vm.Id,
                        MassInGr = f.MassInGramNum
                    }
                ).ToList()
            };

            foreach (var foodPortion in command.FoodPortions)
            {
                foodPortion.Food = await _mediator.Send(new GetFoodByIdCommand {Id = foodPortion.FoodId});
            }

            await _mediator.Send(command);
            
            HttpContext.Clear(HttpContextExtensions.EditKey);

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

        [Authorize]
        [HttpPost]
        public IActionResult RememberEditedData(MealViewModel vm)
        {
            HttpContext.RememberData(vm, HttpContextExtensions.EditKey);
            return RedirectToAction("Edit", "Carbohydrate", new {id = vm.Id});
        }
    }
}