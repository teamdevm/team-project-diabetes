using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diabetes.MVC.Controllers
{
    public class AboutController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View(new Models.AboutViewModel {});
        }
    }
}
