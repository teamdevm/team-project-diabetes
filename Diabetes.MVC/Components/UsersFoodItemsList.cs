using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class UsersFoodItemsList:ViewComponent
    {
        public IViewComponentResult Invoke(UsersFoodItemsListViewModel model)
        {
            return View(model);
        }
        
    }
}