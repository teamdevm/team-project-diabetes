using System;

namespace Diabetes.MVC.Models
{
    public class ActionHistoryItem
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }
    }
}