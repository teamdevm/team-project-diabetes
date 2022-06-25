using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class FoodItemsListForNoteEditing:ViewComponent
    {
        public IViewComponentResult Invoke(FoodListViewModel model)
        {
            return View(model);
        }
        
    }
}