using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class ActionItemsList:ViewComponent
    {
        public IViewComponentResult Invoke(ActionItemsListViewModel model)
        {
            return View(model);
        }
    }
}