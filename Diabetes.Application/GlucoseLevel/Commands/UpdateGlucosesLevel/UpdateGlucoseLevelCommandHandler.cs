using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var glucoseLevel = _dbContext.GlucoseLevels.FirstOrDefaultAsync
                (g=>g.Id == request.Id && g.UserId == request.UserId, cancellationToken).Result;
            if (glucoseLevel != null)
            {
                //обновляем поля
                glucoseLevel.BeforeAfterEating = request.BeforeAfterEating;
                glucoseLevel.Comment = request.Comment;
                glucoseLevel.MeasuringDateTime = request.MeasuringDateTime;
                glucoseLevel.ValueInMmol = request.ValueInMmol;
                glucoseLevel.CreationDateTime = DateTime.Now;

                //сохраняем изменения
                _dbContext.GlucoseLevels.Update(glucoseLevel);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
