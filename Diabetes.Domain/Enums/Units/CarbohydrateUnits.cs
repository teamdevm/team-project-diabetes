using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums.Units
{
    public enum CarbohydrateUnits
    {
        [Display(Name = "Углеводы в граммах")]
        Carbohydrate,
        [Display(Name = "Хлебные единицы (ХЕ)")]
        BreadUnits
    }
}