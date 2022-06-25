using System.Collections.Generic;
using Diabetes.Domain;
using Diabetes.Domain.Common;

namespace Diabetes.MVC.Models.Foods
{
    public class FoodListViewModel
    {
        public PaginatedList<Food> FoodItems { get; set; }
        public string SearchString { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
    }
}