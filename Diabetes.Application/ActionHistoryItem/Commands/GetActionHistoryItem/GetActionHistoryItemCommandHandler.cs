using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using Diabetes.Domain.Normalized.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.ActionHistoryItems.Commands.GetActionHistoryItems
{
    public class GetActionHistoryItemsCommandHandler : IRequestHandler<GetActionHistoryItemsCommand, List<Domain.ActionHistoryItem>>
    {
        private readonly IGlucoseLevelDbContext _dbContextGlucose;
        private readonly INoteInsulinDbContext _dbContextInsulin;

        public GetActionHistoryItemsCommandHandler(
            IGlucoseLevelDbContext dbContextGlucose, INoteInsulinDbContext dbContextInsulin)
        {
            _dbContextGlucose = dbContextGlucose;
            _dbContextInsulin = dbContextInsulin;
        }

        public async Task<List<Domain.ActionHistoryItem>> Handle(GetActionHistoryItemsCommand request, CancellationToken cancellationToken)
        {
            var queryInsulin = await _dbContextInsulin.InsulinNotes
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.LastUpdate)
                .Take(request.Number)
                .Select(ins=> ins.ToHistoryItem())
                .ToListAsync(cancellationToken);
            
            var queryGlucose = await _dbContextGlucose.GlucoseNotes
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.LastUpdate)
                .Take(request.Number)
                .Select(glu=> glu.ToHistoryItem())
                .ToListAsync(cancellationToken);

            var actions = queryInsulin.Union(queryGlucose)
                .OrderByDescending(a => a.CreationDateTime)
                .Take(request.Number)
                .ToList();

            return actions;
        }
    }
}