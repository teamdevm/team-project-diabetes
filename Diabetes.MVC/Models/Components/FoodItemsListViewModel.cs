using System.Collections.Generic;
using Diabetes.Domain;

namespace Diabetes.MVC.Models.Components
{
    public class FoodItemsListViewModel
    {
        public List<Food> Items { get; set; }

        public FoodItemsListViewModel(List<Food> items)
        {
            Items = items;
        }    
    }
}