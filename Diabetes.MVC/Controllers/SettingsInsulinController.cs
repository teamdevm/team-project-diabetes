using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    public class SettingsInsulinController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
