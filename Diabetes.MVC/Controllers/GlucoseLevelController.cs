using System;
using System.Threading.Tasks;
using Diabetes.Application.GlucoseLevel.Commands.CreateGlucoseLevel;
using Diabetes.Application.GlucoseLevel.Commands.UpdateGlucosesLevel;
using Diabetes.Application.GlucoseLevel.Commands.DeleteGlucoseLevel;
using Diabetes.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using Diabetes.MVC.Extensions;

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
        [Authorize]
        public IActionResult AddGlucoseLevel(string returnUrl)
        {
            var viewModel = new CreateGlucoseLevelViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost] // вставка (удаление и изменение - по схожей схеме)
        [Authorize]
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
                UserId = User.GetId(),
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
        [Authorize]
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

        [HttpGet("[controller]/[action]/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGlucoseLevel(Guid id)
        { 
            if(id == Guid.Empty) 
                return RedirectToAction("Index", "Home");
            
            var command = new DeleteGlucoseLevelCommand 
            {
                Id = id,
                UserId = User.GetId()
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
    }
}