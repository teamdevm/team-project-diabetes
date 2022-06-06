using System;

namespace Diabetes.Domain
{
    public class ActionHistoryItem
    {
        public Guid Id { get; set; }
        public ActionHistoryType Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}