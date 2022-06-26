using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Diabetes.Persistence;
using MediatR;
using System.Threading.Tasks;
using Diabetes.Domain.Normalized.Enums.Units;
using Diabetes.MVC.Extensions;

namespace Diabetes.MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator, UserManager<Account> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                IdentityResult result;

                if (model.Email != user.Email)
                {
                    result = await _userManager.SetEmailAsync(user, model.Email);
                    if (!result.Succeeded)
                    { 
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }
                if (model.Password != null && model.Password != "")
                {
                    if (model.PasswordOld == null && model.PasswordOld != "")
                    { 
                        ModelState.AddModelError(string.Empty, "Введите текущий пароль");
                        return View(model);
                    }

                    result = await _userManager.ChangePasswordAsync(user, model.PasswordOld, model.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                user.Name = model.Name;
                user.DiabetesType = model.DiabetesType;
                user.Birthdate = model.Birthdate;
                user.Gender = model.Gender;
                user.Height = model.Height;
                user.Weight = model.Weight;

                result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                return RedirectToAction("Index", "Account");
            }           

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var viewModel = new ProfileViewModel
            {
                Name = user.Name,
                Email = user.Email,
                DiabetesType = user.DiabetesType,
                Birthdate = user.Birthdate,
                Gender = user.Gender,
                Height = user.Height,
                Weight = user.Weight
            };

            return View(viewModel);
        }
    }
}
