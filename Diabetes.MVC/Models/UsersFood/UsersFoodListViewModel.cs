using System;
using Diabetes.Domain.Common;

namespace Diabetes.MVC.Models.UsersFood
{
    public class UsersFoodListViewModel
    {
        public PaginatedList<Domain.UsersFood> FoodItems { get; set; }
        public string SearchString { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
    }
}