using Diabetes.Application.NoteInsulin.Commands.CreateNoteInsulin;
using Diabetes.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    public class InsulinController:Controller
    {
        private readonly IMediator _mediator;

        public InsulinController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddInsulin(string returnUrl)
        {
            var viewModel = new CreateInsulinViewModel {ReturnUrl = returnUrl};
            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddInsulin(CreateInsulinViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var command = new CreateNoteInsulinCommand
            {
                UserId = Guid.NewGuid(),
                InsulinValue = viewModel.Value,
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} {viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                InsulinType = viewModel.Type,
                Comment = viewModel.Comment
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditInsulin(EditInsulinViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Guid vwId;
            bool isValid = Guid.TryParse(viewModel.Id, out vwId);

            if (isValid)
            {
                var command = new EditNoteInsulinCommand
                {
                    UserId = Guid.NewGuid(),
                    Id = vwId,
                    InsulinValue = viewModel.Value,
                    MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} {viewModel.MeasuringTime}", "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                    InsulinType = viewModel.Type,
                    Comment = viewModel.Comment
                };

                await _mediator.Send(command);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteInsulin(DeleteInsulinViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            Guid vwId;
            bool isValid = Guid.TryParse(viewModel.Id, out vwId);

            if (isValid)
            {
                var command = new DeleteNoteInsulinCommand
                {
                    UserId = Guid.NewGuid(),
                    Id = vwId,
                };

                await _mediator.Send(command);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}