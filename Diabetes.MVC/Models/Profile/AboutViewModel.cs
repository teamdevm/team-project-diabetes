using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.MVC.Models
{
    public class AboutViewModel
    {
        public string ProgramName { get; set; } = "Diabetes v1.0";
        public string Description { get; set; } = "Приложение для больных диабетом";
        public string[] Authors { get; set; } =
        {
            "Разработчики",
            "- Валеев Рустам",
            "- Зырянова Анна",
            "- Корзников Артем",
            "- Чирухин Дмитрий",
            "- Шерстобитов Андрей"
        };
    }
}
