using System.Linq;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using System.Collections.Generic;
using MediatR;
using System.Threading;
using Diabetes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.ActionHistoryItem.Commands.GetItemForUpdDel
{
    public class GetItemForUpdDelCommandHandler: IRequestHandler <GetItemForUpdDelCommand, Domain.ActionHistoryItem>
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        private readonly INoteInsulinDbContext _dbContextInsulin;

        public GetItemForUpdDelCommandHandler(
            IGlucoseLevelDbContext dbContextClucose, INoteInsulinDbContext dbContextInsulin)
        {
            _dbContextClucose = dbContextClucose;
            _dbContextInsulin = dbContextInsulin;
        }

        public async Task<Domain.ActionHistoryItem> Handle(GetItemForUpdDelCommand request, CancellationToken cancellationToken)
        {
            switch (request.Type)
            {
                case ActionHistoryType.GlucoseLevel:
                    var queryGlucose = await _dbContextClucose.GlucoseLevels
                    .Where(p => p.Id == request.Id)
                    .Select(glu => new Domain.ActionHistoryItem
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

                    return queryGlucose.Count == 0 ? null : queryGlucose[0];

                case ActionHistoryType.Insulin:
                    var queryInsulin = await _dbContextInsulin.InsulinNotes
                    .Where(p => p.Id == request.Id)
                    .Select(ins => new Domain.ActionHistoryItem
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

                    return queryInsulin.Count == 0 ? null : queryInsulin[0];

                default: return null;
            }
        }
    }
}
