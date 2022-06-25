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
using Diabetes.MVC.Models.Meal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class FoodForNoteController:Controller
    {

        private const int PageSize = 10;
        private IMediator _mediator;
        private List<Guid> UsedFood => HttpContext.GetMeal(HttpContextExtensions.AddKey).Foods.Select(f=>f.Food.Id).ToList(); 

        public FoodForNoteController(IMediator mediator)
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
        public async Task<IActionResult> AddToNoteList(Guid id)
        {
            if(id == Guid.Empty)
                return RedirectToAction("Index", "FoodForNote");

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
            vm.Food = await _mediator.Send(new GetFoodByIdCommand {Id = vm.FoodId});
            
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            HttpContext.AddFood(vm, HttpContextExtensions.AddKey);

            return RedirectToAction("Index", "FoodForNote");
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public IActionResult EditFromNoteList(Guid id)
        {
            var vm = HttpContext.GetFoodById(id, HttpContextExtensions.AddKey);
            
            return View(vm);
        }
        
        [Authorize]
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> EditFromNoteList(FoodForNoteViewModel vm)
        {
            vm.Food = await _mediator.Send(new GetFoodByIdCommand {Id = vm.FoodId});
            
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            
            HttpContext.Edit(vm, HttpContextExtensions.AddKey);

            return RedirectToAction("Add", "Carbohydrate");
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public IActionResult DeleteFromNoteList(Guid id)
        {
            if(id == Guid.Empty)
                return RedirectToAction("Add", "Carbohydrate");
            
            HttpContext.RemoveFood(id, HttpContextExtensions.AddKey);
            
            return RedirectToAction("Add", "Carbohydrate");
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult RememberData(MealViewModel vm)
        {
            HttpContext.RememberData(vm, HttpContextExtensions.AddKey);
            return RedirectToAction("Add", "Carbohydrate");
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> AddToFavorite(Guid id)
        {
            
            if(id == Guid.Empty)
                return RedirectToAction("Index", "FoodForNote");
            
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
            
            return RedirectToAction("Index", "FoodForNote");
        }
    }
}