using System;
using System.Globalization;
using System.Threading.Tasks;
using Diabetes.Application.Food.Commands.GetFoodListItems;
using Diabetes.Application.NoteInsulin.Commands.CreateNoteInsulin;
using Diabetes.Application.UsersFood.Commands.AddUsersFood;
using Diabetes.Application.UsersFood.Commands.DeleteUsersFood;
using Diabetes.Application.UsersFood.Commands.EditUsersFood;
using Diabetes.Application.UsersFood.Commands.GetUsersFoodItem;
using Diabetes.Application.UsersFood.Commands.GetUsersFoodItemsList;
using Diabetes.MVC.Extensions;
using Diabetes.MVC.Models.Foods;
using Diabetes.MVC.Models.UsersFood;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class UsersFoodController:Controller
    {
        private readonly IMediator _mediator;
        private const int PageSize = 10;

        public UsersFoodController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        public async Task<IActionResult> Index(UsersFoodListViewModel vm = null)
        {
            vm ??= new UsersFoodListViewModel();

            var command = new GetUsersFoodItemsListCommand()
            {
                SearchString = vm.SearchString,
                PageSize = PageSize,
                UserId = User.GetId(),
                CurrentPage = vm.CurrentPage
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
            
            var command = new DeleteUsersFoodCommand() 
            {
                UserId = User.GetId(), 
                Id = id,
            };
            
            await _mediator.Send(command);

            return RedirectToAction("Index", "UsersFood");
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

            return RedirectToAction("Index", "UsersFood");
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

            return RedirectToAction("Index", "UsersFood");
        }
    }
}