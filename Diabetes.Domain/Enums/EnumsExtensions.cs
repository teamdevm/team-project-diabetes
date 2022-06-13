namespace Diabetes.Domain.Normalized.Enums
{
    public static class EnumsExtensions
    {
        public static string ToLocalizedString(this MeasuringTimeType measuringTimeType)
        {
            return measuringTimeType switch
            {
                MeasuringTimeType.AfterEating => "После еды",
                MeasuringTimeType.BeforeEating => "До еды"
            };
        }
        
        public static string ToLocalizedString(this InsulinType insulinType)
        {
            return insulinType switch
            {
                InsulinType.Long => "Длинный",
                InsulinType.Short => "Короткий"
            };
        }
        
        public static string ToLocalizedString(this NoteType noteType)
        {
            return noteType switch
            {
                NoteType.Glucose => "Глюкоза",
                NoteType.Insulin => "Инсулин",
                NoteType.Carbohydrate => "Углеводы"
            };
        }
        
    }
}