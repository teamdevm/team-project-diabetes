using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Diabetes.Application.Statistics.Commands;
using System.Linq;
using System.Collections.Generic;
using Diabetes.Domain.Normalized.Enums;
using Diabetes.Domain;
using System;
using System.Threading.Tasks;

namespace Diabetes.MVC.Controllers
{
    public class StatisticsController: Controller
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator) => _mediator = mediator;

        //[Authorize]
        public IActionResult Index()
        {
            ViewBag.Categorical = new List<string>().ToArray();
            ViewBag.Values = new List<double>().ToArray();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GlucoseGraphicsAsync (StatisticsViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            Predicate<MeasuringTimeType> MeasuringTimeFilter = viewModel.GlucoseAdditional switch
            {
                "До еды" => a => a == MeasuringTimeType.BeforeEating,
                "После еды" => a => a == MeasuringTimeType.AfterEating,
                _ => a => true,
            };

            Predicate<DateTime> DateFilter = viewModel.GlucoseTimePeriod switch
            {
                "За сегодня" => a => a.Date == DateTime.Now.Date,
                "За неделю" => a => (DateTime.Now.Date - a.Date).Days < 7 && (DateTime.Now.Date - a.Date).Days >= 0,
                "За месяц" => a => (DateTime.Now.Year - a.Year) * 12 +
                DateTime.Now.Month - a.Month +
                (DateTime.Now.Day >= a.Day ? 0 : -1) == 0,
                _ => a => true,
            };

            var command = new GetGlucoseCommand
            {
                MeasuringTimeFilter = MeasuringTimeFilter,
                DateFilter = DateFilter
            };
            var itemsList = await _mediator.Send(command) as List<GlucoseNote>;

            ViewBag.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToArray();
            ViewBag.Values = itemsList.Select(a => a.Value).ToArray();

            return View(viewModel);
        }
    }
}
