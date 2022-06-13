using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums
{
    public enum InsulinType
    {
        [Display(Name ="Короткий")]
        Short,
        [Display(Name ="Длинный")]
        Long
    }
}