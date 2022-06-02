using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Domain
{
    public class NoteInsulin
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public double InsulinValue { get; set; }
        public DateTime MeasuringDateTime { get; set; }
        public string InsulinType { get; set; }
        public string Comment { get; set; }
    }
}
