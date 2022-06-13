using Diabetes.Application.NoteInsulin.Commands.CreateNoteInsulin;
using Diabetes.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Diabetes.Application.NoteInsulin.Commands.GetNoteInsulin;
using Diabetes.Domain;
using Diabetes.MVC.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    [Route("[controller]/[action]")]
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
                UserId = User.GetId(),
                InsulinValue = double.Parse(viewModel.Value),
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} {viewModel.MeasuringTime}", 
                    "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                InsulinType = viewModel.Type,
                Comment = viewModel.Comment
            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> EditInsulin(Guid id)
        {
            var getInsulinCommand = new GetNoteInsulinCommand
            {
                Id = id
            };

            var model = await _mediator.Send(getInsulinCommand);
            
            var viewModel = new EditInsulinViewModel
            {
                Id = model.Id,
                Value = model.InsulinValue.ToString(),
                MeasuringTime = model.MeasuringDateTime.ToString("HH:mm"),
                MeasuringDate = model.MeasuringDateTime.ToString("yyyy-MM-dd"),
                Comment = model.Comment,
                Type = model.InsulinType
            };
            
            return View(viewModel);
        }

        [HttpPost("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> EditInsulin(EditInsulinViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var command = new EditNoteInsulinCommand 
            {
                UserId = User.GetId(),
                Id = viewModel.Id,
                InsulinValue = double.Parse(viewModel.Value),
                MeasuringDateTime = DateTime.ParseExact($"{viewModel.MeasuringDate} {viewModel.MeasuringTime}",
                        "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                InsulinType = viewModel.Type,
                Comment = viewModel.Comment

            };

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteInsulin(Guid id)
        {
            if(id == Guid.Empty) 
                return NotFound();
            
            var command = new DeleteNoteInsulinCommand 
            {
                UserId = User.GetId(), 
                Id = id,
            };
            
            await _mediator.Send(command);

            return RedirectToAction("Index", "Home");
        }
    }
}