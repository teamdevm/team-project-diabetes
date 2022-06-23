using System.Collections.Generic;
using Diabetes.Domain;
using Diabetes.MVC.Models.FoodForNote;

namespace Diabetes.MVC.Models.Components
{
    public class FoodListViewModel
    {
        public List<Food> Items { get; set; }

        public FoodListViewModel(List<Food> items)
        {
            Items = items;
        } 
    }
}