using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.GlucoseLevel.Commands.GetGlucoseLevel
{
    public class GetGlucoseLevelCommandHandler: IRequestHandler<GetGlucoseLevelCommand, GlucoseNote>
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        public GetGlucoseLevelCommandHandler(IGlucoseLevelDbContext dbContextClucose) => _dbContextClucose = dbContextClucose;
        public async Task<GlucoseNote> Handle(GetGlucoseLevelCommand request, CancellationToken cancellationToken)
        {
            var queryGlucose = await _dbContextClucose.GlucoseNotes
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

            return queryGlucose;
        }
    }
}
