using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.UpdateGlucosesLevel
{
    class UpdateGlucoseLevelCommandHandler:IRequestHandler<UpdateGlucoseLevelCommand, Unit>
    {
        private readonly IGlucoseLevelDbContext _dbContext;
        public UpdateGlucoseLevelCommandHandler(IGlucoseLevelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateGlucoseLevelCommand request, CancellationToken cancellationToken)
        {
            //ищем уровень глюкозы в БД
            var keys = new object[1] { request.Id };
            var glucoseLevel = _dbContext.GlucoseLevels.FindAsync(keys, cancellationToken).Result;
            if (glucoseLevel == null) throw new Exception ("Value not found");

            //обновляем поля
            glucoseLevel.BeforeAfterEating = request.BeforeAfterEating;
            glucoseLevel.Comment = request.Comment;
            glucoseLevel.MeasuringDateTime = request.MeasuringDateTime;
            glucoseLevel.ValueInMmol = request.ValueInMmol;

            //сохраняем изменения
            _dbContext.GlucoseLevels.Update(glucoseLevel);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
