using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class Pagination : ViewComponent
    {
        public IViewComponentResult Invoke(PaginationViewModel model)
        {
            return View(model);
        }
    }
}