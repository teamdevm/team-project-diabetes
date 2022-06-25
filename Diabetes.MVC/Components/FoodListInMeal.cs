using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class FoodListInMeal:ViewComponent
    {
        public IViewComponentResult Invoke(FoodForNoteListViewModel model)
        {
            return View(model);
        }
        
    }
}