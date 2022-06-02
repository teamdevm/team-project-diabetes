using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.MVC.Models
{
    public class DeleteGlucoseLevelViewModel
    {
        public Guid Id { get; set; }
        public string ReturnUrl { get; set; }
    }
}
