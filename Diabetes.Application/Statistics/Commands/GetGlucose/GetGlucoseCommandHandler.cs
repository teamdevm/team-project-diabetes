using Diabetes.Application.Interfaces;
using System.Collections.Generic;
using Diabetes.Domain;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Diabetes.Application.Statistics.Commands
{
    public class GetGlucoseCommandHandler: IRequestHandler<GetGlucoseCommand, List<GlucoseNote>>
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        public GetGlucoseCommandHandler(IGlucoseLevelDbContext dbContextClucose) => _dbContextClucose = dbContextClucose;
        public async Task<List<GlucoseNote>> Handle(GetGlucoseCommand request, CancellationToken cancellationToken)
        {
            var queryGlucose = await _dbContextClucose.GlucoseNotes
                .ToListAsync(cancellationToken);

            queryGlucose = queryGlucose.Where(a => request.MeasuringTimeFilter(a.MeasuringTimeType))
                .Where(a => request.DateFilter(a.MeasuringDateTime))
                .OrderBy(a => a.MeasuringDateTime)
                .ToList();

            return queryGlucose;
        }
    }
}
