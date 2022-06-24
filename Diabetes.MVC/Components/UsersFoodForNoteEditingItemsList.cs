using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class UsersFoodForNoteEditingItemsList:ViewComponent
    {
        public IViewComponentResult Invoke(UsersFoodItemsListViewModel model)
        {
            return View(model);
        }
        
    }
}