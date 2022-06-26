using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums.Units
{
    public enum GlucoseUnits
    {
        [Display(Name = "ммоль/л")]
        MmolPerLiter,
        [Display(Name = "мг/дл")]
        MgramPerDeciliter
    }
}