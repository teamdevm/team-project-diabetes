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
    public class GetInsulinCommandHandler : IRequestHandler<GetInsulinCommand, List<InsulinNote>>
    {
        private readonly INoteInsulinDbContext _dbContextInsulin;
        public GetInsulinCommandHandler(INoteInsulinDbContext dbContextInsulin) => _dbContextInsulin = dbContextInsulin;
        public async Task<List<InsulinNote>> Handle(GetInsulinCommand request, CancellationToken cancellationToken)
        {
            var queryInsulin = await _dbContextInsulin.InsulinNotes
                .ToListAsync(cancellationToken);

            queryInsulin = queryInsulin.Where(a => request.InsulinFilter(a.InsulinType))
                .Where(a => request.DateFilter(a.MeasuringDateTime))
                .OrderBy(a => a.MeasuringDateTime)
                .ToList();

            return queryInsulin;
        }
    }
}
