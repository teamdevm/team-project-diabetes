using System;
using System.Globalization;
using Diabetes.Domain.Enums;
using Diabetes.Domain.Normalized.Enums;

namespace Diabetes.Domain
{
    public class ActionHistoryItem
    {
        public Guid Id { get; set; }
        public NoteType Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public double ValueNum => double.Parse(Value.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture);
        public string Details { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}