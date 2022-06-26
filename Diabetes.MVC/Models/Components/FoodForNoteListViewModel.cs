using System.Collections.Generic;
using Diabetes.Domain;
using Diabetes.MVC.Models.FoodForNote;

namespace Diabetes.MVC.Models.Components
{
    public class FoodForNoteListViewModel
    {
        public List<FoodForNoteViewModel> Items { get; set; }

        public FoodForNoteListViewModel(List<FoodForNoteViewModel> items)
        {
            Items = items;
        }    
    }
}