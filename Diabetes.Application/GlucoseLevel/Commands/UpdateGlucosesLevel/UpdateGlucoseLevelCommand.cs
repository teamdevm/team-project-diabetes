using System;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.UpdateGlucosesLevel
{
    public class UpdateGlucoseLevelCommand:IRequest<Unit>
    {
        public Guid UserId { get; set; } // не изменяется
        public string Comment { get; set; } // не ключ => можно хранить только новый
        public double ValueInMmol { get; set; } // не ключ => можно хранить только новое
        public string BeforeAfterEating { get; set; } // не ключ => можно хранить только новое
        public DateTime oldMeasuringDateTime { get; set; }
        public DateTime newMeasuringDateTime { get; set; }
    }
}
