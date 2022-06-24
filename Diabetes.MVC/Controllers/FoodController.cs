using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diabetes.Application.Food.Commands.GetFoodById;
using Diabetes.Application.Food.Commands.GetFoodListItems;
using Diabetes.Application.UsersFood.Commands.AddUsersFood;
using Diabetes.Domain;
using Diabetes.MVC.Extensions;
using Diabetes.MVC.Models.Foods;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Controllers
{
    public class FoodController:Controller
    {
        private readonly IMediator _mediator;
        private const int PageSize = 10;

        public FoodController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        public async Task<IActionResult> Index(FoodListViewModel vm = null)
        {
            vm ??= new FoodListViewModel();

            var command = new GetFoodListItemsCommand
            {
                SearchString = vm.SearchString,
                PageSize = PageSize
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

            var vm = new FoodListViewModel
            {
                CurrentPage = currentPage.Value,
                SearchString = searchString
            };

            var command = new GetFoodListItemsCommand()
            {
                SearchString = vm.SearchString,
                PageSize = PageSize,
                CurrentPage = vm.CurrentPage
            };

            var items = await _mediator.Send(command);

            vm.FoodItems = items;

            return View("Index", vm);
        }
        
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> AddToFavorite(Guid id)
        {
            
            if(id == Guid.Empty)
                return RedirectToAction("Index", "Food");
            
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
            
            return RedirectToAction("Index", "Food");
        }
    }
}