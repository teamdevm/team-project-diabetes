using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Diabetes.Persistence;
using MediatR;
using System.Threading.Tasks;
using Diabetes.Domain.Normalized.Enums.Units;


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
