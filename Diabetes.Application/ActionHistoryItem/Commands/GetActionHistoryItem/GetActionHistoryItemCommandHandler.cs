using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.ActionHistoryItems.Commands.GetActionHistoryItems
{
    public class GetActionHistoryItemsCommandHandler : IRequestHandler<GetActionHistoryItemsCommand, List<Domain.ActionHistoryItem>>
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        private readonly INoteInsulinDbContext _dbContextInsulin;

        public GetActionHistoryItemsCommandHandler(
            IGlucoseLevelDbContext dbContextClucose, INoteInsulinDbContext dbContextInsulin)
        {
            _dbContextClucose = dbContextClucose;
            _dbContextInsulin = dbContextInsulin;
        }

        public async Task<List<Domain.ActionHistoryItem>> Handle(GetActionHistoryItemsCommand request, CancellationToken cancellationToken)
        {
            var queryInsulin = await _dbContextInsulin.InsulinNotes
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.CreationDateTime)
                .Take(request.Number)
                .Select(ins=> new Domain.ActionHistoryItem
                {
                    Id = ins.Id,
                    Type = ActionHistoryType.Insulin,
                    Title = "Инсулин",
                    Value = ins.InsulinValue.ToString(),
                    Details = ins.InsulinType,
                    DateTime = ins.MeasuringDateTime,
                    CreationDateTime = ins.CreationDateTime
                })
                .ToListAsync(cancellationToken);
            
            var queryGlucose = await _dbContextClucose.GlucoseLevels
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.CreationDateTime)
                .Take(request.Number)
                .Select(glu=> new Domain.ActionHistoryItem
                {
                    Id = glu.Id,
                    Type = ActionHistoryType.GlucoseLevel,
                    Title = "Глюкоза",
                    Value = glu.ValueInMmol.ToString(),
                    Details = glu.BeforeAfterEating,
                    DateTime = glu.MeasuringDateTime,
                    CreationDateTime = glu.CreationDateTime
                })
                .ToListAsync(cancellationToken);

            var actions = queryInsulin.Union(queryGlucose)
                .OrderByDescending(a => a.CreationDateTime)
                .Take(request.Number)
                .ToList();

            return actions;
        }
    }
}