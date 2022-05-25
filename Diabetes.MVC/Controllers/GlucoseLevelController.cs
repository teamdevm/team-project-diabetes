using System;
using System.Threading.Tasks;
using Diabetes.Application.GlucoseLevel.Commands.CreateGlucoseLevel;
using Diabetes.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
                ValueInMmol = viewModel.ValueInMmol,
                Comment = viewModel.Comment,
                MeasuringDateTime = viewModel.MeasuringDateTime
            };
            
            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
    }
}