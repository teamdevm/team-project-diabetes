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
using Diabetes.Application.ActionHistoryItem.Commands.GetItemForUpdDel;
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

            GetActionHistoryItemsCommand command = new GetActionHistoryItemsCommand
            {
                UserId = new Guid(user.Id),
                Number = actionHistoryItemsNumber
            };

            List<ActionHistoryItem> list = await _mediator.Send(command);

            HomeViewModel viewModel = new HomeViewModel
            {
                UserName = user.Name,
                ActionHistoryItems = list
            };

            return View(viewModel);
        }

        [HttpGet]
        // тест в postman: GET https://localhost:5001/Home/getrecord
        // Id - из БД, Type - Глюкоза или Инсулин
        // (в теории могут попасться записи с одним guid, тк таблицы разные => пока решила разделить)

        // возможно, нужно будет править или вообще убирать метод из контроллера, но для тестов такой вид должен подойти
        public async Task<IActionResult> getrecord(HistoryItemViewModel viewModel)
        {
            ActionHistoryType? type = null;
            if (viewModel.Type == "Глюкоза") type = ActionHistoryType.GlucoseLevel;
            else if (viewModel.Type == "Инсулин") type = ActionHistoryType.Insulin;

            GetItemForUpdDelCommand command = new GetItemForUpdDelCommand
            {
                Id = Guid.Parse(viewModel.Id),
                Type = type
            };

            ActionHistoryItem item = await _mediator.Send(command);
            /*здесь брейкпоинт, чтобы посмотреть, какая запись нашлась*/ return View(viewModel);
        }
    }
}