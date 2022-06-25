using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Diabetes.Application.ActionHistoryItem.Commands.GetAllActionHistoryItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diabetes.MVC.Models;
using Diabetes.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Diabetes.Domain;
using Diabetes.Application.ActionHistoryItems.Commands.GetActionHistoryItems;
using Diabetes.MVC.Extensions;
using MediatR;

namespace Diabetes.MVC.Controllers
{
    public class HomeController : Controller
    {
        const int actionHistoryItemsNumber = 5;

        private readonly UserManager<Account> _userManager;
        private readonly IMediator _mediator;
        
        private async Task<Account> GetAccount() => await _userManager.FindByNameAsync(User.Identity.Name);

        public HomeController(IMediator mediator, UserManager<Account> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await GetAccount();

            var command = new GetActionHistoryItemsCommand
            {
                UserId = User.GetId(),
                Number = actionHistoryItemsNumber
            };

            List<ActionHistoryItem> list = await _mediator.Send(command);

            HomeViewModel viewModel = new HomeViewModel
            {
                UserName = user.Name,
                ActionHistoryItems = list
            };

            ViewBag.MinimalGlucoseBeforeEating = user.MinimalGlucoseBeforeEating;     
            ViewBag.MaximalGlucoseBeforeEating = user.MaximalGlucoseBeforeEating;  
            ViewBag.MinimalGlucoseAfterEating = user.MinimalGlucoseAfterEating;  
            ViewBag.MaximalGlucoseAfterEating = user.MaximalGlucoseAfterEating;  

            return View(viewModel);
        }
        
        [Authorize]
        public async Task<IActionResult> Actions()
        {

            var command = new GetAllActionHistoryItemsCommand
            {
                UserId = User.GetId(),
            };

            var list = await _mediator.Send(command);

            var viewModel = new ActionsViewModel
            {
                ActionHistoryItems = list
            };
            
            var user = await GetAccount();
            
            ViewBag.MinimalGlucoseBeforeEating = user.MinimalGlucoseBeforeEating;     
            ViewBag.MaximalGlucoseBeforeEating = user.MaximalGlucoseBeforeEating;  
            ViewBag.MinimalGlucoseAfterEating = user.MinimalGlucoseAfterEating;  
            ViewBag.MaximalGlucoseAfterEating = user.MaximalGlucoseAfterEating; 

            return View(viewModel);
        }
    }
}