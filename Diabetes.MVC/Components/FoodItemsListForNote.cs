using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class FoodItemsListForNote:ViewComponent
    {
        public IViewComponentResult Invoke(FoodListViewModel model)
        {
            return View(model);
        }
        
    }
}