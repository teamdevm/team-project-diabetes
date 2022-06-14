using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.ActionHistoryItems.Commands.GetActionHistoryItems;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using Diabetes.Domain.Normalized.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.ActionHistoryItem.Commands.GetAllActionHistoryItems
{
    public class GetAllActionHistoryItems: IRequestHandler<GetAllActionHistoryItemsCommand, List<Domain.ActionHistoryItem>>
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        private readonly INoteInsulinDbContext _dbContextInsulin;

        public GetAllActionHistoryItems(
            IGlucoseLevelDbContext dbContextClucose, INoteInsulinDbContext dbContextInsulin)
        {
            _dbContextClucose = dbContextClucose;
            _dbContextInsulin = dbContextInsulin;
        }

        public async Task<List<Domain.ActionHistoryItem>> Handle(GetAllActionHistoryItemsCommand request, CancellationToken cancellationToken)
        {
            var queryInsulin = await _dbContextInsulin.InsulinNotes
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.LastUpdate)
                .Select(ins=> ins.ToHistoryItem())
                .ToListAsync(cancellationToken);
            
            var queryGlucose = await _dbContextClucose.GlucoseNotes
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.LastUpdate)
                .Select(glu=> glu.ToHistoryItem())
                .ToListAsync(cancellationToken);

            var actions = queryInsulin.Union(queryGlucose)
                .OrderByDescending(a => a.CreationDateTime)
                .ToList();

            return actions;
        }
    }
}