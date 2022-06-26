using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
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
            var glucose = new GlucoseNote()
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
                Value = request.ValueInMmol,
                MeasuringDateTime = request.MeasuringDateTime,
                LastUpdate = DateTime.Now,
                MeasuringTimeType = request.MeasuringTimeType,
                Comment = request.Comment
            };

            await _dbContext.GlucoseNotes.AddAsync(glucose, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}