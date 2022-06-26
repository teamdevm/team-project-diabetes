using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Diabetes.Application.Statistics.Commands;
using System.Linq;
using Diabetes.Persistence;
using Diabetes.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Globalization;
using Diabetes.MVC.Extensions;
using Diabetes.Domain.Normalized.Enums.Units;

namespace Diabetes.MVC.Controllers
{
    public class StatisticsGlucoseController: Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<Account> _userManager;
        public StatisticsGlucoseController(IMediator mediator, UserManager<Account> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize]
        public async Task <IActionResult> Index (string returnUrl)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var viewModel = new StatisticsGlucoseViewModel
            {
                ReturnUrl = returnUrl,
                HasNormalBeforeEating = user.NormalGlucoseBeforeEating.HasValue,
                HasNormalAfterEating = user.NormalGlucoseAfterEating.HasValue,
                GlucoseUnit = user.GlucoseUnits switch
                {
                    GlucoseUnits.MmolPerLiter => "0",
                    GlucoseUnits.MgramPerDeciliter => "1"
                }
            };

            Func<MeasuringTimeType, bool> MTFilter = a => true;

            Func<DateTime, bool> DFilter = a => (DateTime.Now.Date - a.Date).Days < 7 && (DateTime.Now.Date - a.Date).Days >= 0;

            var command = new GetGlucoseCommand
            {
                MeasuringTimeFilter = MTFilter,
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);
            itemsList = itemsList.Where(a => a.UserId == User.GetId()).ToList();

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => Math.Round(a.Value.Value, 2)).ToList();
            viewModel.NormalValuesBeforeEating = itemsList.Select(a => user.NormalGlucoseBeforeEating).ToList();
            viewModel.NormalValuesAfterEating = itemsList.Select(a => user.NormalGlucoseAfterEating).ToList();
            viewModel.HasNormalBeforeEating = user.NormalGlucoseBeforeEating.HasValue;
            viewModel.HasNormalAfterEating = user.NormalGlucoseAfterEating.HasValue;
            if (viewModel.GlucoseUnit == "1")
            {
                viewModel.Values = viewModel.Values.Select(a => 18 * a).ToList();
                viewModel.NormalValuesBeforeEating = viewModel.NormalValuesBeforeEating.Select(a => 18 * a).ToList();
                viewModel.NormalValuesAfterEating = viewModel.NormalValuesAfterEating.Select(a => 18 * a).ToList();
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task <IActionResult> GlucoseGraphics(StatisticsGlucoseViewModel viewModel)
        {
            if (viewModel.CustomDate == null) viewModel.CustomDate = DateTime.Now.ToString("yyyy-MM-dd");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            Func<MeasuringTimeType, bool> MTFilter = viewModel.GlucoseAdditional switch
            {
                "1" => a => a == MeasuringTimeType.BeforeEating,
                "2" => a => a == MeasuringTimeType.AfterEating,
                _ => a => true,
            };

            Func<DateTime, bool> DFilter = viewModel.GlucoseTimePeriod switch
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

            var command = new GetGlucoseCommand
            {
                MeasuringTimeFilter = MTFilter,
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);
            itemsList = itemsList.Where(a => a.UserId == User.GetId()).ToList();

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => Math.Round(a.Value.Value, 2)).ToList();
            viewModel.NormalValuesBeforeEating = itemsList.Select(a => user.NormalGlucoseBeforeEating).ToList();
            viewModel.NormalValuesAfterEating = itemsList.Select(a => user.NormalGlucoseAfterEating).ToList();
            viewModel.HasNormalBeforeEating = user.NormalGlucoseBeforeEating.HasValue;
            viewModel.HasNormalAfterEating = user.NormalGlucoseAfterEating.HasValue;
            if (viewModel.GlucoseUnit == "1")
            {
                viewModel.Values = viewModel.Values.Select(a => 18 * a).ToList();
                viewModel.NormalValuesBeforeEating = viewModel.NormalValuesBeforeEating.Select(a => 18 * a).ToList();
                viewModel.NormalValuesAfterEating = viewModel.NormalValuesAfterEating.Select(a => 18 * a).ToList();
            }
            return View(viewModel);
        }
    }
}
