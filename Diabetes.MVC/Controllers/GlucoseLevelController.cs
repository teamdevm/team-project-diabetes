using System;
using System.Threading.Tasks;
using Diabetes.Application.GlucoseLevel.Commands.CreateGlucoseLevel;
using Diabetes.Application.GlucoseLevel.Commands.UpdateGlucosesLevel;
using Diabetes.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Diabetes.MVC.Controllers
{
    public class GlucoseLevelController: Controller
    {
        private readonly IMediator _mediator;

        public GlucoseLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public IActionResult AddGlucoseLevel(string returnUrl)
        {
            var viewModel = new CreateGlucoseLevelViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddGlucoseLevel(CreateGlucoseLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var command = new CreateGlucoseLevelCommand
            {
                UserId = Guid.NewGuid(), //Временно, пока нет авторизации
                ValueInMmol = viewModel.ValueInMmol.Value,
                Comment = viewModel.Comment,
                BeforeAfterEating = viewModel.BeforeAfterEating,
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} " +
                $"{viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
            
            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGlucoseLevel
            (CreateGlucoseLevelViewModel viewModel, DateTime oldTime)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var command = new UpdateGlucoseLevelCommand
            {
                UserId = Guid.NewGuid(), //Временно, пока нет авторизации
                ValueInMmol = viewModel.ValueInMmol.Value,
                Comment = viewModel.Comment,
                BeforeAfterEating = viewModel.BeforeAfterEating,
                oldMeasuringDateTime = oldTime,
                newMeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} " +
                $"{viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGlucoseLevel(CreateGlucoseLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var command = new CreateGlucoseLevelCommand
            {
                UserId = Guid.NewGuid(), //Временно, пока нет авторизации
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} " +
                $"{viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
    }
}