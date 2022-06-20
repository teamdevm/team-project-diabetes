using Diabetes.Domain.Enums;
using Diabetes.Domain.Normalized;
using Diabetes.Domain.Normalized.Enums;

namespace Diabetes.Domain
{
    public class GlucoseNote:Note
    {
        public MeasuringTimeType MeasuringTimeType { get; set; }
        
        public override ActionHistoryItem ToHistoryItem()
        {
            return new ActionHistoryItem
            {
                Id = Id,
                Type = NoteType.Glucose,
                Title = NoteType.Glucose.ToLocalizedString(),
                Value = Value.ToString(),
                Details = MeasuringTimeType.ToLocalizedString(),
                DateTime = MeasuringDateTime,
                CreationDateTime = LastUpdate
            };
        }
    }
}