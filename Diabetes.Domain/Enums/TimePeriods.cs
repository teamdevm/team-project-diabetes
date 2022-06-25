using System.ComponentModel.DataAnnotations;
namespace Diabetes.Domain.Enums
{
    public enum TimePeriods
    {   [Display(Name = "За неделю")]
        ThisWeek,
        [Display(Name = "За месяц")]
        ThisMonth,
        [Display(Name ="За сегодня")]
        Today,
        [Display(Name = "Произвольная дата")]
        CustomDate,
        [Display(Name = "За все время")]
        All
    }
}
