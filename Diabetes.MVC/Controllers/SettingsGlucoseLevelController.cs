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
        public async Task<IActionResult> IndexAsync(SettingsGlucoseLevelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                user.GlucoseUnits = model.GlucoseUnitsUsed;
                user.NormalGlucoseBeforeEating = Convert.ToDouble(model.ValueBeforeEating.Replace('.', ','));
                user.NormalGlucoseAfterEating = Convert.ToDouble(model.ValueAfterEating.Replace('.', ','));
                //Если бы хранили в зависимости от единиц
                /*if (model.GlucoseUnitsUsed == GlucoseUnits.MmolPerLiter)
                {
                    user.NormalGlucoseBeforeEating = double.Parse(model.ValueBeforeEating);
                    user.NormalGlucoseAfterEating = double.Parse(model.ValueAfterEating);
                }
                else
                {
                    user.NormalGlucoseBeforeEating = double.Parse(model.ValueBeforeEatingAlt);
                    user.NormalGlucoseAfterEating = double.Parse(model.ValueAfterEatingAlt);
                }*/

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                return RedirectToAction("Index", "Settings");
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
                ValueBeforeEatingAlt = (user.NormalGlucoseBeforeEating * 20).ToString(),
                ValueAfterEatingAlt = (user.NormalGlucoseAfterEating * 20).ToString(),
                GlucoseUnitsUsed = user.GlucoseUnits,
            };

            return View(viewModel);
        }
    }
}
