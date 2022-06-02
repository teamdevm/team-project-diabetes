using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diabetes.MVC.Models;
using Diabetes.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Diabetes.Domain;
using Diabetes.Application.ActionHistoryItems.Commands.GetActionHistoryItems;
using MediatR;

namespace Diabetes.MVC.Controllers
{
    public class HomeController : Controller
    {
        const int actionHistoryItemsNumber = 5;

        private readonly UserManager<Account> _userManager;
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator, UserManager<Account> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var command = new GetActionHistoryItemsCommand
            {
                UserId = new Guid(user.Id),
                Number = actionHistoryItemsNumber
            };

            List<ActionHistoryItem> list = await _mediator.Send(command);

            HomeViewModel viewModel = null;
            if (user != null)
                viewModel = new HomeViewModel
                {
                    UserName = user.Name,
                    ActionHistoryItems = list
                };
            else
                viewModel = new HomeViewModel
                {
                    UserName = "",
                    ActionHistoryItems = list
                };


            return View(viewModel);
        }
    }
}