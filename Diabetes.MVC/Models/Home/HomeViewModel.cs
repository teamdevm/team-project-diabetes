using System.Collections.Generic;

namespace Diabetes.MVC.Models
{
    public class HomeViewModel
    {
        public string UserName { get; set; }
        public List<ActionHistoryItem> ActionHistoryItems { get; set; }
    }
}