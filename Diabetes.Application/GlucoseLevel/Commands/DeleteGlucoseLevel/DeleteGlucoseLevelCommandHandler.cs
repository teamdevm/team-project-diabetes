using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.DeleteGlucoseLevel
{
    class DeleteGlucoseLevelCommandHandler : IRequestHandler<DeleteGlucoseLevelCommand, Unit>
    {
        private readonly IGlucoseLevelDbContext _dbContext;
        public DeleteGlucoseLevelCommandHandler(IGlucoseLevelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteGlucoseLevelCommand request, CancellationToken cancellationToken)
        {
            //ищем уровень глюкозы в БД
            var keys = new object[1] { request.Id };
            var glucoseLevel = _dbContext.GlucoseLevels.FindAsync(keys, cancellationToken).Result;
            if (glucoseLevel == null) throw new Exception("Value not found");

            //удаляем и сохраняем изменения
            _dbContext.GlucoseLevels.Remove(glucoseLevel);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
