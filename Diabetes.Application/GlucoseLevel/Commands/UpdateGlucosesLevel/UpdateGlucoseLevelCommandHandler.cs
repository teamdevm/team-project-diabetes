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
            var glucose =  await _dbContext.GlucoseNotes
                .FirstOrDefaultAsync(g=>g.Id == request.Id && g.UserId == request.UserId, cancellationToken);
            if (glucose != null)
            {
                //обновляем поля
                glucose.MeasuringTimeType = request.MeasuringTimeType;
                glucose.Comment = request.Comment;
                glucose.MeasuringDateTime = request.MeasuringDateTime;
                glucose.Value = request.ValueInMmol;
                glucose.LastUpdate = DateTime.Now;

                //сохраняем изменения
                _dbContext.GlucoseNotes.Update(glucose);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
