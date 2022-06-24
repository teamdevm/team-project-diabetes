using System;
using System.Collections.Generic;
using Diabetes.Domain;
using Diabetes.Domain.Common;

namespace Diabetes.MVC.Models.Components
{
    public class UsersFoodItemsListViewModel
    {
        public PaginatedList<Domain.UsersFood> Items { get; set; }

        public UsersFoodItemsListViewModel(PaginatedList<Domain.UsersFood> items)
        {
            Items = items;
        }
    }
}