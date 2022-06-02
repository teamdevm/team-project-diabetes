using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diabetes.MVC.Models;
using Diabetes.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Diabetes.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Account> _userManager;

        public HomeController(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var list = new List<ActionHistoryItem>
            {
                new ActionHistoryItem
                {
                    Type = ActionHistoryType.Insulin,
                    Title = "Инсулин",
                    Value = "100",
                    Details = "Короткий",
                    DateTime = DateTime.Now
                },
                new ActionHistoryItem
                {
                    Type = ActionHistoryType.GlucoseLevel,
                    Title = "Глюкоза",
                    Value = "20",
                    Details = "До еды",
                    DateTime = DateTime.Now
                },
                new ActionHistoryItem
                {
                    Type = ActionHistoryType.Insulin,
                    Title = "Инсулин",
                    Value = "50",
                    Details = "Длинный",
                    DateTime = DateTime.Now
                },
                new ActionHistoryItem
                {
                    Type = ActionHistoryType.Insulin,
                    Title = "Инсулин",
                    Value = "50",
                    Details = "Длинный",
                    DateTime = DateTime.Now
                },
                new ActionHistoryItem
                {
                    Type = ActionHistoryType.Insulin,
                    Title = "Инсулин",
                    Value = "50",
                    Details = "Длинный",
                    DateTime = DateTime.Now
                }
            };

            var viewModel = new HomeViewModel
            {
                UserName = user.Name,
                ActionHistoryItems = list
            };

            return View(viewModel);
        }
    }
}