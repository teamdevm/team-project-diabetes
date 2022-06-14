using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums
{
    public enum MeasuringTimeType
    {
        [Display(Name ="До еды")]
        BeforeEating,
        [Display(Name ="После еды")]
        AfterEating
    }
}