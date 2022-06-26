using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.MVC.Models
{
    public class AboutViewModel
    {
        public string ProgramName { get; set; } = "DiaDiary - дневник диабета";
        public string Description { get; set; } =
            "Приложение для людей страдающих от диабета. " +
            "Позволяет вести учёт принимаемых углеводов, введённых доз инсулина, " +
            "а также контролировать уровень глюкозы в крови. В приложении нет платных услуг";
        public string[] Authors { get; set; } =
        {
            "Разработчики",
            "- Валеев Рустам",
            "- Зырянова Анна",
            "- Корзников Артем",
            "- Чирухин Дмитрий",
            "- Шерстобитов Андрей"
        };
        public string Version { get; set; } = "v1.0 pre-alpha";
    }
}