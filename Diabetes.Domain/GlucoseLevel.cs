using System;

namespace Diabetes.Domain
{
    public class GlucoseLevel
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public double ValueInMmol { get; set; }
        public DateTime MeasuringDateTime { get; set; }
        public string BeforeAfterEating { get; set; }
        public string Comment { get; set; }
    }
}