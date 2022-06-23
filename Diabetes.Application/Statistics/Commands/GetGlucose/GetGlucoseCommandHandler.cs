using Diabetes.Application.Interfaces;
using System.Collections.Generic;
using Diabetes.Domain;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Statistics.Commands
{
    public class GetGlucoseCommandHandler
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        public GetGlucoseCommandHandler(IGlucoseLevelDbContext dbContextClucose) => _dbContextClucose = dbContextClucose;
        public async Task<List<GlucoseNote>> Handle(GetGlucoseCommand request, CancellationToken cancellationToken)
        {
            var queryGlucose = await _dbContextClucose.GlucoseNotes
                .Where(a => request.MeasuringTimeFilter(a.MeasuringTimeType))
                .Where(a => request.DateFilter(a.MeasuringDateTime))
                .OrderBy(a => a.MeasuringDateTime)
                .ToListAsync(cancellationToken);

            return queryGlucose;
        }
    }
}
