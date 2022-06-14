using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums.User
{
    public enum Gender
    {
        [Display(Name = "Мужской")]
        Male,
        [Display(Name = "Женский")]
        Female
    }
}