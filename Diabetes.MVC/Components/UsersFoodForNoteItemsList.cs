using Diabetes.MVC.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace Diabetes.MVC.Components
{
    public class UsersFoodForNoteItemsList:ViewComponent
    {
        public IViewComponentResult Invoke(UsersFoodItemsListViewModel model)
        {
            return View(model);
        }
        
    }
}