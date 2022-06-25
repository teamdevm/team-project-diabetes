using Diabetes.Domain.Enums;
using Diabetes.Domain.Normalized;
using Diabetes.Domain.Normalized.Enums;

namespace Diabetes.Domain
{
    public class InsulinNote:Note
    {
        public InsulinType InsulinType { get; set; }
        
        public override ActionHistoryItem ToHistoryItem()
        {
            return new ActionHistoryItem
            {
                Id = Id,
                Type = NoteType.Insulin,
                Title = NoteType.Insulin.ToLocalizedString(),
                Value = Value.ToString(),
                Details = InsulinType.ToLocalizedString(),
                DateTime = MeasuringDateTime,
                CreationDateTime = LastUpdate
            };
        }
    }
}