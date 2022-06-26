using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Diabetes.Application.Statistics.Commands;
using Microsoft.AspNetCore.Identity;
using Diabetes.Persistence;
using System.Linq;
using System.Globalization;
using System;
using System.Threading.Tasks;
using Diabetes.MVC.Extensions;
using Diabetes.Domain.Normalized.Enums.Units;

namespace Diabetes.MVC.Controllers
{
    public class StatisticsCarbohydratesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<Account> _userManager;
        public StatisticsCarbohydratesController(IMediator mediator, UserManager<Account> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var viewModel = new StatisticsCarbohydratesViewModel
            {
                ReturnUrl = returnUrl,
                CarbohydratesUnit = user.CarbohydrateUnits == CarbohydrateUnits.Carbohydrate ? "0" : "1"
            };

            Func<DateTime, bool> DFilter = a => (DateTime.Now.Date - a.Date).Days < 7 &&
            (DateTime.Now.Date - a.Date).Days >= 0;

            var command = new GetCarbohydratesCommand
            {
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);
            itemsList = itemsList.Where(a => a.UserId == User.GetId()).ToList();

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => a.TotalCarbohydrates).ToList();
            if (viewModel.CarbohydratesUnit == "1") viewModel.Values = viewModel.Values.Select
                    (a => Math.Round(a / user.CarbohydrateInBreadUnit, 2)).ToList();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CarbohydratesGraphics(StatisticsCarbohydratesViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (viewModel.CustomDate == null) viewModel.CustomDate = DateTime.Now.ToString("yyyy-MM-dd");
            Func<DateTime, bool> DFilter = viewModel.CarbohydratesTimePeriod switch
            {
                "1" => a => (DateTime.Now.Year - a.Year) * 12 +
                DateTime.Now.Month - a.Month +
                (DateTime.Now.Day >= a.Day ? 0 : -1) == 0,
                "2" => a => a.Date == DateTime.Now.Date,
                "3" => a => a.Date == DateTime.ParseExact(viewModel.CustomDate, "yyyy-MM-dd",
                CultureInfo.InvariantCulture).Date,
                "4" => a => true,
                _ => a => (DateTime.Now.Date - a.Date).Days < 7 && (DateTime.Now.Date - a.Date).Days >= 0,
            };

            var command = new GetCarbohydratesCommand
            {
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);
            itemsList = itemsList.Where(a => a.UserId == User.GetId()).ToList();

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => a.TotalCarbohydrates).ToList();
            if (viewModel.CarbohydratesUnit == "1") viewModel.Values = viewModel.Values.Select
                    (a => Math.Round(a / user.CarbohydrateInBreadUnit, 2)).ToList();
            return View(viewModel);
        }
    }
}