using System;
using Diabetes.Domain.Normalized.Enums;

namespace Diabetes.Domain
{
    public abstract class Note
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public NoteType Type { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime MeasuringDateTime { get; set; }
        public double? Value { get; set; }
        public string Comment { get; set; }

        public abstract ActionHistoryItem ToHistoryItem();
    }
}