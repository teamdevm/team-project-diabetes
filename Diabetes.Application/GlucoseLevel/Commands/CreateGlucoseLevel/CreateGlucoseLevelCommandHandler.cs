using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.CreateGlucoseLevel
{
    public class CreateGlucoseLevelCommandHandler:IRequestHandler<CreateGlucoseLevelCommand, Unit>
    {
        private readonly IGlucoseLevelDbContext _dbContext;

        public CreateGlucoseLevelCommandHandler(IGlucoseLevelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateGlucoseLevelCommand request, CancellationToken cancellationToken)
        {
            var glucoseLevel = new Domain.GlucoseLevel
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
                ValueInMmol = request.ValueInMmol,
                MeasuringDateTime = request.MeasuringDateTime,
                CreationDateTime = DateTime.Now,
                BeforeAfterEating = request.BeforeAfterEating,
                Comment = request.Comment
            };

            await _dbContext.GlucoseLevels.AddAsync(glucoseLevel, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}