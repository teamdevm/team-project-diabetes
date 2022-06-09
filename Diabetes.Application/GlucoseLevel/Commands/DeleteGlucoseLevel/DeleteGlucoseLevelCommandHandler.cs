using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var glucoseLevel = await _dbContext.GlucoseLevels
                .FirstOrDefaultAsync(g=>g.Id == request.Id && g.UserId == request.UserId, cancellationToken);

            if (glucoseLevel != null)
            {
                //удаляем и сохраняем изменения
                _dbContext.GlucoseLevels.Remove(glucoseLevel);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
