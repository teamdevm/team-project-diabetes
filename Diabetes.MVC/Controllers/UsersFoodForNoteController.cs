using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Diabetes.Application.Food.Commands.GetFoodById;
using Diabetes.Application.FoodForNote.Commands.GetUsersFoodForNoteList;
using Diabetes.Application.UsersFood.Commands.AddUsersFood;
using Diabetes.Application.UsersFood.Commands.DeleteUsersFood;
using Diabetes.Application.UsersFood.Commands.EditUsersFood;
using Diabetes.Application.UsersFood.Commands.GetUsersFoodItem;
using Diabetes.Application.UsersFood.Commands.GetUsersFoodItemsList;
using Diabetes.Domain.Enums;
using Diabetes.MVC.Extensions;
using Diabetes.MVC.Models.FoodForNote;
using Diabetes.MVC.Models.UsersFood;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class UsersFoodForNoteController:Controller
    {
        private readonly IMediator _mediator;
        private const int PageSize = 10;
        private List<Guid> UsedFood => HttpContext.GetMeal(HttpContextExtensions.AddKey).Foods.Select(f=>f.Food.Id).ToList(); 

        public UsersFoodForNoteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        public async Task<IActionResult> Index(UsersFoodListViewModel vm = null)
        {
            vm ??= new UsersFoodListViewModel();

            var command = new GetUsersFoodForNoteListCommand
            {
                SearchString = vm.SearchString,
                PageSize = PageSize,
                UserId = User.GetId(),
                CurrentPage = vm.CurrentPage,
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
                return RedirectToAction("Index", "UsersFood");

            var vm = new UsersFoodListViewModel
            {
                CurrentPage = currentPage.Value,
                SearchString = searchString
            };

            var command = new GetUsersFoodItemsListCommand()
            {
                SearchString = vm.SearchString,
                PageSize = PageSize,
                UserId = User.GetId(),
                CurrentPage = vm.CurrentPage
            };

            var items = await _mediator.Send(command);

            vm.FoodItems = items;

            return View("Index", vm);
        }
        
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id == Guid.Empty) 
                return NotFound();
            
            HttpContext.RemoveFood(id, HttpContextExtensions.AddKey);
            
            var command = new DeleteUsersFoodCommand() 
            {
                UserId = User.GetId(), 
                Id = id,
            };
            
            await _mediator.Send(command);

            return RedirectToAction("Index", "UsersFoodForNote");
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> AddToNoteList(Guid id)
        {
            if(id == Guid.Empty)
                return RedirectToAction("Index", "FoodForNote");

            var command = new GetFoodByIdCommand {Id = id};
            var food = await _mediator.Send(command);

            if (food == null)
                return RedirectToAction("Index", "UsersFoodForNote");

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

            return RedirectToAction("Index", "UsersFoodForNote");
        }
        
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var getUsersFoodCommand = new GetUsersFoodItemCommand
            {
                Id = id,
                UserId = User.GetId()
            };

            var model = await _mediator.Send(getUsersFoodCommand);
            
            if (model == null)
                return RedirectToAction("Index", "UsersFoodForNote");
            
            var viewModel = new EditUsersFoodViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Details = model.Details,
                Fat = model.Fat.ToString(),
                Kcal = model.Kcal.ToString(),
                Protein = model.Protein.ToString(),
                Carbohydrate = model.Carbohydrate.ToString()
            };
            
            return View(viewModel);
        }

        [HttpPost("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Edit(EditUsersFoodViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var command = new EditUsersFoodCommand 
            {
                UserId = User.GetId(),
                Id = viewModel.Id,
                Name = viewModel.Name,
                Details = viewModel.Details,
                Fat = double.Parse(viewModel.Fat, NumberStyles.Float, CultureInfo.InvariantCulture),
                Kcal = double.Parse(viewModel.Kcal, NumberStyles.Float, CultureInfo.InvariantCulture),
                Carbohydrate = double.Parse(viewModel.Carbohydrate, NumberStyles.Float, CultureInfo.InvariantCulture),
                Protein = double.Parse(viewModel.Protein, NumberStyles.Float, CultureInfo.InvariantCulture),
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "UsersFoodForNote");
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Add(string returnUrl)
        {
            var viewModel = new AddUsersFoodViewModel();
            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddUsersFoodViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var command = new AddUsersFoodCommand
            {
                UserId = User.GetId(),
                Name = viewModel.Name,
                Details = viewModel.Details,
                Carbohydrate = double.Parse(viewModel.Carbohydrate, NumberStyles.Float, CultureInfo.InvariantCulture),
                Fat = double.Parse(viewModel.Fat, NumberStyles.Float, CultureInfo.InvariantCulture),
                Kcal = double.Parse(viewModel.Kcal, NumberStyles.Float, CultureInfo.InvariantCulture),
                Protein = double.Parse(viewModel.Protein, NumberStyles.Float, CultureInfo.InvariantCulture),
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "UsersFoodForNote");
        }
    }
}