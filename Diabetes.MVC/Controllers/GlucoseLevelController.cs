using System;
using System.Threading.Tasks;
using Diabetes.Application.GlucoseLevel.Commands.CreateGlucoseLevel;
using Diabetes.Application.GlucoseLevel.Commands.UpdateGlucosesLevel;
using Diabetes.Application.GlucoseLevel.Commands.DeleteGlucoseLevel;
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

        [HttpPost] // вставка (удаление и изменение - по схожей схеме)
        public async Task<IActionResult> AddGlucoseLevel(CreateGlucoseLevelViewModel viewModel)
        {
            // валидация
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            // создание команды
            var command = new CreateGlucoseLevelCommand
            {
                UserId = Guid.NewGuid(), //Временно, пока нет авторизации
                ValueInMmol = viewModel.ValueInMmol.Value,
                Comment = viewModel.Comment,
                BeforeAfterEating = viewModel.BeforeAfterEating,
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} " +
                $"{viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
            
            // отправка команды на сервер
            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGlucoseLevel
            (UpdateGlucoseLevelViewModel viewModel) // изменение
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            bool isValid = Guid.TryParse(viewModel.Id, out Guid vwId);
            if (isValid)
            {
                var command = new UpdateGlucoseLevelCommand
                {
                    Id = vwId,
                    ValueInMmol = viewModel.ValueInMmol.Value,
                    Comment = viewModel.Comment,
                    BeforeAfterEating = viewModel.BeforeAfterEating,
                    MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} " +
                    $"{viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
                };

                await _mediator.Send(command);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete] // удаление
        public async Task<IActionResult> DeleteGlucoseLevel(DeleteGlucoseLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            bool isValid = Guid.TryParse(viewModel.Id, out Guid vwId);
            if (isValid)
            {
                var command = new DeleteGlucoseLevelCommand
                {
                    Id = vwId
                };

                await _mediator.Send(command);
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}