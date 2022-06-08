using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.GlucoseLevel.Commands.GetGlucoseLevel
{
    public class GetGlucoseLevelCommandHandler: IRequestHandler<GetGlucoseLevelCommand, Domain.GlucoseLevel>
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        public GetGlucoseLevelCommandHandler(IGlucoseLevelDbContext dbContextClucose) => _dbContextClucose = dbContextClucose;
        public async Task<Domain.GlucoseLevel> Handle(GetGlucoseLevelCommand request, CancellationToken cancellationToken)
        {
            List<Domain.GlucoseLevel> queryGlucose = await _dbContextClucose.GlucoseLevels
            .Where(p => p.Id == request.Id)
            .ToListAsync(cancellationToken);

            return queryGlucose.Count == 0 ? null : queryGlucose[0];
        }
    }
}
