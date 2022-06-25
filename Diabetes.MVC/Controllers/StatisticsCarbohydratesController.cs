using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Diabetes.Application.Statistics.Commands;
using System.Linq;
using System.Globalization;
using System;
using System.Threading.Tasks;

namespace Diabetes.MVC.Controllers
{
    public class StatisticsCarbohydratesController : Controller
    {
        private readonly IMediator _mediator;

        public StatisticsCarbohydratesController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var viewModel = new StatisticsCarbohydratesViewModel
            {
                ReturnUrl = returnUrl,
            };

            Func<DateTime, bool> DFilter = a => true;

            var command = new GetCarbohydratesCommand
            {
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => a.TotalCarbohydrates).ToList();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CarbohydratesGraphics(StatisticsCarbohydratesViewModel viewModel)
        {
            if (viewModel.CustomDate == null) viewModel.CustomDate = DateTime.Now.ToString("yyyy-MM-dd");
            Func<DateTime, bool> DFilter = viewModel.CarbohydratesTimePeriod switch
            {
                "1" => a => a.Date == DateTime.Now.Date,
                "2" => a => (DateTime.Now.Date - a.Date).Days < 7 && (DateTime.Now.Date - a.Date).Days >= 0,
                "3" => a => (DateTime.Now.Year - a.Year) * 12 +
                DateTime.Now.Month - a.Month +
                (DateTime.Now.Day >= a.Day ? 0 : -1) == 0,
                "4" => a => a.Date == DateTime.ParseExact(viewModel.CustomDate, "yyyy-MM-dd",
                CultureInfo.InvariantCulture).Date,
                _ => a => true,
            };

            var command = new GetCarbohydratesCommand
            {
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => a.TotalCarbohydrates).ToList();
            return View(viewModel);
        }
    }
}