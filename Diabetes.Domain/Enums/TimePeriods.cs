using System.ComponentModel.DataAnnotations;
namespace Diabetes.Domain.Enums
{
    public enum TimePeriods
    {
        [Display(Name = "За все время")]
        All,
        [Display(Name ="За сегодня")]
        Today,
        [Display(Name = "За неделю")]
        ThisWeek,
        [Display(Name = "За месяц")]
        ThisMonth
    }
}
