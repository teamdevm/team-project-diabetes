using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums.User
{
    public enum DiabetesType
    {
        [Display(Name = "Первый тип")]
        First,
        [Display(Name = "Второй тип")]
        Second
    }
}