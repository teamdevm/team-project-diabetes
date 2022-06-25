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
        const int unit = 18;

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
            //Значения без округления
            double vbe, vae, vbe_alt, vae_alt;
            bool isNanVbe = !double.TryParse(model.ValueBeforeEating.Replace('.', ','), out vbe);
            bool isNanVae = !double.TryParse(model.ValueAfterEating.Replace('.', ','), out vae);
            bool isNanVbeAlt = !double.TryParse(model.ValueBeforeEatingAlt.Replace('.', ','), out vbe_alt);
            bool isNanVaeAlt = !double.TryParse(model.ValueAfterEatingAlt.Replace('.', ','), out vae_alt);

            if (!ModelState.IsValid)
            {
                //Предобработка значений в случае, если был введен неверный разделитель
                if (model.GlucoseUnitsUsed == GlucoseUnits.MgramPerDeciliter)
                {
                    if (isNanVae && !isNanVaeAlt)
                        vae = vae_alt / unit;
                    else if (isNanVbe && !isNanVbeAlt)
                        vbe = vbe_alt / unit;
                }
                else
                {
                    if (isNanVaeAlt && !isNanVae)
                        vae_alt = vae / unit;
                    else if (isNanVbeAlt && !isNanVbe)
                        vbe_alt = vbe / unit;
                }

                //Не рассматривается случай если введены значения некорректной точности
                SettingsGlucoseLevelViewModel modelUpdated = null;
                if (!isNanVae && !isNanVaeAlt && !isNanVbe && !isNanVbeAlt)
                { }
                else
                {
                    modelUpdated = new SettingsGlucoseLevelViewModel
                    {
                        ValueBeforeEating = Math.Round(vbe, 2).ToString(),
                        ValueAfterEating = Math.Round(vae, 2).ToString(),
                        ValueBeforeEatingAlt = Math.Round(vbe_alt, 2).ToString(),
                        ValueAfterEatingAlt = Math.Round(vae_alt, 2).ToString(),
                        GlucoseUnitsUsed = model.GlucoseUnitsUsed
                    };

                    ModelState.Clear();
                    if (TryValidateModel(modelUpdated))
                        model = modelUpdated;
                }
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (model.GlucoseUnitsUsed == GlucoseUnits.MgramPerDeciliter)
                {
                    vbe = vbe_alt / unit;
                    vae = vae_alt / unit;
                }

                user.GlucoseUnits = model.GlucoseUnitsUsed;
                user.NormalGlucoseBeforeEating = Convert.ToDouble(vbe);
                user.NormalGlucoseAfterEating = Convert.ToDouble(vae);
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
                ValueBeforeEatingAlt = Math.Round((double)(user.NormalGlucoseBeforeEating * unit), 2).ToString(),
                ValueAfterEatingAlt = Math.Round((double)(user.NormalGlucoseAfterEating * unit), 2).ToString(),
                GlucoseUnitsUsed = user.GlucoseUnits
            };

            return View(viewModel);
        }
    }
}
