using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    public class MealController:Controller
    {
        [Authorize]
        public IActionResult AddMeal()
        {
            return View();
        }
    }
}