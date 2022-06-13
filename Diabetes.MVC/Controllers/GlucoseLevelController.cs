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
using Diabetes.Application.GlucoseLevel.Commands.GetGlucoseLevel;
using Diabetes.Domain;
using Diabetes.MVC.Extensions;

namespace Diabetes.MVC.Controllers
{
    [Route("[controller]/[action]")]
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

        [HttpPost] // вставка
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
                ValueInMmol = double.Parse(viewModel.ValueInMmol),
                Comment = viewModel.Comment,
                BeforeAfterEating = viewModel.BeforeAfterEating,
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} " +
                $"{viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
            
            // отправка команды на сервер
            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> EditGlucoseLevel(Guid id)
        {
            var getGlucoseCommand = new GetGlucoseLevelCommand
            {
                Id = id
            };
            var model = await _mediator.Send(getGlucoseCommand);

            var viewModel = new EditGlucoseLevelViewModel
            {
                Id = model.Id,
                ValueInMmol = model.ValueInMmol.ToString(),
                MeasuringTime = model.MeasuringDateTime.ToString("HH:mm"),
                MeasuringDate = model.MeasuringDateTime.ToString("yyyy-MM-dd"),
                Comment = model.Comment,
                BeforeAfterEating = model.BeforeAfterEating
            };

            return View(viewModel);
        }

        [HttpPost("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> EditGlucoseLevel (EditGlucoseLevelViewModel viewModel) // изменение
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            var command = new UpdateGlucoseLevelCommand 
            {
                Id = viewModel.Id,
                UserId = User.GetId(),
                ValueInMmol = double.Parse(viewModel.ValueInMmol),
                Comment = viewModel.Comment,
                BeforeAfterEating = viewModel.BeforeAfterEating,
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} " +
                $"{viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
            
            await _mediator.Send(command);
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteGlucoseLevel(Guid id)
        { 
            if(id == Guid.Empty) 
                return NotFound();
            
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