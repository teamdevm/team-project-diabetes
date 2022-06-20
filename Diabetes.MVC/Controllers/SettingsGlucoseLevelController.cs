using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    public class SettingsGlucoseLevelController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
