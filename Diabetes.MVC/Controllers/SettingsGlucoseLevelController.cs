using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Diabetes.Persistence;
using MediatR;
using System.Threading.Tasks;
using Diabetes.Domain.Normalized.Enums.Units;
using System;
using System.Text.RegularExpressions;
using Diabetes.MVC.Attributes.Validation;
using System.Globalization;

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
            bool isNanVbe = !double.TryParse(model.ValueBeforeEating.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out vbe);
            bool isNanVae = !double.TryParse(model.ValueAfterEating.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out vae);
            bool isNanVbeAlt = !double.TryParse(model.ValueBeforeEatingAlt.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out vbe_alt);
            bool isNanVaeAlt = !double.TryParse(model.ValueAfterEatingAlt.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out vae_alt);

            if (model.ValueBeforeEating == "NaN")
                isNanVbe = true;
            if (model.ValueAfterEating == "NaN")
                isNanVae = true;
            if (model.ValueBeforeEatingAlt == "NaN")
                isNanVbeAlt = true;
            if (model.ValueAfterEatingAlt == "NaN")
                isNanVaeAlt = true;

            bool isAccurate = false;

            if (!ModelState.IsValid)
            {
                double m_vbe = vbe, m_vae = vae, m_vbe_alt = vbe_alt, m_vae_alt = vae_alt;
                //Предобработка значений в случае, если был введен неверный разделитель
                if (model.GlucoseUnitsUsed == GlucoseUnits.MgramPerDeciliter)
                {
                    if (isNanVae && !isNanVaeAlt)
                        m_vae = vae = vae_alt / unit;
                    else if (isNanVbe && !isNanVbeAlt)
                        m_vbe = vbe = vbe_alt / unit;

                    m_vbe_alt = Math.Round(vbe_alt, 2);
                    m_vae_alt = Math.Round(vae_alt, 2);

                    var match_vbe = DoubleAttribute.regex.IsMatch(model.ValueBeforeEatingAlt);
                    var match_vae = DoubleAttribute.regex.IsMatch(model.ValueAfterEatingAlt);

                    if (vae_alt == m_vae_alt && vbe_alt == m_vbe_alt && match_vbe && match_vae)
                    {
                        isAccurate = true;
                        m_vbe = Math.Round(vbe, 2);
                        m_vae = Math.Round(vae, 2);
                    }
                }
                else
                {
                    if (isNanVaeAlt && !isNanVae)
                        m_vae_alt = vae_alt = vae / unit;
                    else if (isNanVbeAlt && !isNanVbe)
                        m_vbe_alt = vbe_alt = vbe / unit;

                    var match_vbe = DoubleAttribute.regex.IsMatch(model.ValueBeforeEating);
                    var match_vae = DoubleAttribute.regex.IsMatch(model.ValueAfterEating);

                    m_vbe = Math.Round(vbe, 2);
                    m_vae = Math.Round(vae, 2);

                    if (vae == m_vae && vbe == m_vbe && match_vbe && match_vae)
                    {
                        isAccurate = true;
                        m_vbe_alt = Math.Round(vbe_alt, 2);
                        m_vae_alt = Math.Round(vae_alt, 2);
                    }
                }

                //Не рассматривается случай если введены значения некорректной точности
                SettingsGlucoseLevelViewModel modelUpdated = null;
                if ((!isNanVae || !isNanVaeAlt || !isNanVbe || !isNanVbeAlt) && isAccurate)
                {
                    modelUpdated = new SettingsGlucoseLevelViewModel
                    {
                        ValueBeforeEating = m_vbe.ToString(),
                        ValueAfterEating = m_vae.ToString(),
                        ValueBeforeEatingAlt = m_vbe_alt.ToString(),
                        ValueAfterEatingAlt = m_vae_alt.ToString(),
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
