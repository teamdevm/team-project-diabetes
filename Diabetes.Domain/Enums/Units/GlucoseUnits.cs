using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums.Units
{
    public enum GlucoseUnits
    {
        [Display(Name = "ללמכü/כ")]
        MmolPerLiter,
        [Display(Name = "לד/הכ")]
        MgramPerDeciliter
    }
}