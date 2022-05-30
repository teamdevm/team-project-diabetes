using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class AddSelector:ViewComponent
    {
        public IViewComponentResult Invoke(AddSelectorModel model)
        {
            return View(model);
        }
    }
}