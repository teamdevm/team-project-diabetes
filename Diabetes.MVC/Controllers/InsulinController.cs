using Diabetes.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Controllers
{
    public class InsulinController:Controller
    {
        [HttpGet]
        public IActionResult AddInsulin(string returnUrl)
        {
            var viewModel = new CreateInsulinViewModel {ReturnUrl = returnUrl};
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult AddInsulin(CreateInsulinViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}