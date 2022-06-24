using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Diabetes.Application.Statistics.Commands;
using System.Linq;
using System.Collections.Generic;
using Diabetes.Domain.Enums;
using Diabetes.Domain;
using System;
using System.Threading.Tasks;

namespace Diabetes.MVC.Controllers
{
    public class StatisticsInsulinController : Controller
    {
        private readonly IMediator _mediator;

        public StatisticsInsulinController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var viewModel = new StatisticsInsulinViewModel
            {
                ReturnUrl = returnUrl,
            };

            Func<InsulinType, bool> IFilter = a => true;

            Func<DateTime, bool> DFilter = a => true;

            var command = new GetInsulinCommand
            {
                InsulinFilter = IFilter,
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => a.Value.Value).ToList();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> InsulinGraphics(StatisticsInsulinViewModel viewModel)
        {
            Func<InsulinType, bool> IFilter = viewModel.InsulinAdditional switch
            {
                "1" => a => a == InsulinType.Short,
                "2" => a => a == InsulinType.Long,
                _ => a => true,
            };

            Func<DateTime, bool> DFilter = viewModel.InsulinTimePeriod switch
            {
                "1" => a => a.Date == DateTime.Now.Date,
                "2" => a => (DateTime.Now.Date - a.Date).Days < 7 && (DateTime.Now.Date - a.Date).Days >= 0,
                "3" => a => (DateTime.Now.Year - a.Year) * 12 +
                DateTime.Now.Month - a.Month +
                (DateTime.Now.Day >= a.Day ? 0 : -1) == 0,
                _ => a => true,
            };

            var command = new GetInsulinCommand
            {
                InsulinFilter = IFilter,
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => a.Value.Value).ToList();
            return View(viewModel);
        }
    }
}