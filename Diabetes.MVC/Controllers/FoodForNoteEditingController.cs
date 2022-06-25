using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diabetes.Application.Food.Commands.GetFoodById;
using Diabetes.Application.FoodForNote.Commands.GetFoodForNoteList;
using Diabetes.Application.UsersFood.Commands.AddUsersFood;
using Diabetes.MVC.Extensions;
using Diabetes.MVC.Models.FoodForNote;
using Diabetes.MVC.Models.Foods;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class FoodForNoteEditingController:Controller
    {
        private const int PageSize = 10;
        private IMediator _mediator;
        private List<Guid> UsedFood => HttpContext.GetMeal(HttpContextExtensions.EditKey).Foods.Select(f=>f.Food.Id).ToList(); 

        public FoodForNoteEditingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        public async Task<IActionResult> Index(FoodListViewModel vm = null)
        {
            vm ??= new FoodListViewModel();

            var command = new GetFoodForNoteListCommand
            {
                SearchString = vm.SearchString,
                PageSize = PageSize,
                UsedFoods = UsedFood
            };

            var items = await _mediator.Send(command);

            vm.FoodItems = items;

            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Pagination(string searchString, int? currentPage)
        {
            searchString ??= "";
            if (currentPage == null)
                return RedirectToAction("Index", "FoodForNote");

            var vm = new FoodListViewModel
            {
                CurrentPage = currentPage.Value,
                SearchString = searchString
            };

            var command = new GetFoodForNoteListCommand
            {
                SearchString = vm.SearchString,
                PageSize = PageSize,
                CurrentPage = vm.CurrentPage,
                UsedFoods = UsedFood
            };

            var items = await _mediator.Send(command);

            vm.FoodItems = items;

            return View("Index", vm);
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public IActionResult DeleteFromNoteList(Guid id)
        {
            if (id != Guid.Empty)
            {
                HttpContext.RemoveFood(id, HttpContextExtensions.EditKey);
            }

            var vm = HttpContext.GetMeal(HttpContextExtensions.EditKey);
            
            return RedirectToAction("Edit", "Carbohydrate", new{id = vm.Id});
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Cancel()
        {
            HttpContext.Clear(HttpContextExtensions.EditKey);

            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> AddToNoteList(Guid id)
        {
            if(id == Guid.Empty)
                return RedirectToAction("Index", "FoodForNoteEditing");

            var command = new GetFoodByIdCommand {Id = id};
            var food = await _mediator.Send(command);

            var vm = new FoodForNoteViewModel
            {
                FoodId = id,
                Food = food
            };

            return View(vm);
        }
        
        [Authorize]
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> AddToNoteList(FoodForNoteViewModel vm)
        {
            var command = new GetFoodByIdCommand {Id = vm.FoodId};
            var food = await _mediator.Send(command);
            vm.Food = food;
            
            HttpContext.AddFood(vm, HttpContextExtensions.EditKey);

            var meal = HttpContext.GetMeal(HttpContextExtensions.EditKey);

            return RedirectToAction("Edit", "Carbohydrate", new {id = meal.Id});
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public IActionResult EditFromNoteList(Guid id)
        {
            var vm = HttpContext.GetFoodById(id, HttpContextExtensions.EditKey);

            var meal = HttpContext.GetMeal(HttpContextExtensions.EditKey);
            
            if (vm == null)
                return RedirectToAction("Edit", "Carbohydrate", new {id = meal.Id});
            
            return View(vm);
        }
        
        [Authorize]
        [HttpPost("{id:guid}")]
        public IActionResult EditFromNoteList(FoodForNoteViewModel vm)
        {
            HttpContext.Edit(vm, HttpContextExtensions.EditKey);

            var meal = HttpContext.GetMeal(HttpContextExtensions.EditKey);

            return RedirectToAction("Edit", "Carbohydrate", new {Id = meal.Id});
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> AddToFavorite(Guid id)
        {
            
            if(id == Guid.Empty)
                return RedirectToAction("Index", "FoodForNoteEditing");
            
            var commandGet = new GetFoodByIdCommand
            {
                Id = id
            };

            var model = await _mediator.Send(commandGet);

            var commandAdd = new AddUsersFoodCommand
            {
                UserId = User.GetId(),
                Name = model.Name,
                Details = model.Details,
                Fat = model.Fat,
                Carbohydrate = model.Carbohydrate,
                Kcal = model.Kcal,
                Protein = model.Protein
            };

            await _mediator.Send(commandAdd);
            
            return RedirectToAction("Index", "FoodForNoteEditing");
        }
    }
}