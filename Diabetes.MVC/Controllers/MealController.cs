using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Controllers
{
    public class MealController:Controller
    {
        public IActionResult AddMeal()
        {
            return View();
        }
    }
}