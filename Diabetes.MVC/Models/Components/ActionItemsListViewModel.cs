using System.Collections.Generic;
using Diabetes.Domain;

namespace Diabetes.MVC.Models.Components
{
    public class ActionItemsListViewModel
    {
        public List<ActionHistoryItem> Items { get; set; }

        public ActionItemsListViewModel(List<ActionHistoryItem> items)
        {
            Items = items;
        }
    }
}