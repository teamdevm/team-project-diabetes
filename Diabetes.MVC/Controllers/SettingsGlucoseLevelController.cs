using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Diabetes.Persistence;
using MediatR;
using System.Threading.Tasks;
using Diabetes.Domain.Normalized.Enums.Units;
using System;

namespace Diabetes.MVC.Controllers
{
    public class SettingsGlucoseLevelController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMediator _mediator;

        public SettingsGlucoseLevelController(IMediator mediator, UserManager<Account> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(SettingsGlucoseLevelViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var viewModel = new SettingsGlucoseLevelViewModel
            {
                ValueBeforeEating = user.NormalGlucoseBeforeEating.ToString(),
                ValueAfterEating = user.NormalGlucoseAfterEating.ToString(),
                GlucoseUnitsUsed = user.GlucoseUnits,
            };

            return View(viewModel);
        }
    }
}
