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

    public enum MeasuringTimeTypeExtended
    {
        [Display(Name = "Любое время")]
        Any,
        [Display(Name = "До еды")]
        BeforeEating,
        [Display(Name = "После еды")]
        AfterEating
    }
}