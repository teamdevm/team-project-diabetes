using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using MediatR;

namespace Diabetes.Application.ActionHistoryItems.Commands.GetActionHistoryItems
{
    public class GetActionHistoryItemsCommandHandler : IRequestHandler<GetActionHistoryItemsCommand, List<ActionHistoryItem>>
    {
        private readonly IGlucoseLevelDbContext _dbContextClucose;
        private readonly INoteInsulinDbContext _dbContextInsulin;

        public GetActionHistoryItemsCommandHandler(
            IGlucoseLevelDbContext dbContextClucose, INoteInsulinDbContext dbContextInsulin)
        {
            _dbContextClucose = dbContextClucose;
            _dbContextInsulin = dbContextInsulin;
        }

        public async Task<List<ActionHistoryItem>> Handle(GetActionHistoryItemsCommand request, CancellationToken cancellationToken)
        {
            string val = request.UserId.ToString();
            /*var queryInsulin = (from p in _dbContextInsulin.InsulinNotes
                                where p.UserId == request.UserId
                                orderby p.MeasuringDateTime descending
                                select p).ToList();
            var queryGlucose = (from p in _dbContextClucose.GlucoseLevels
                                where p.UserId.CompareTo(request.UserId) == 0
                                orderby p.MeasuringDateTime descending
                                select p).ToList();*/
            var queryInsulin = _dbContextInsulin.InsulinNotes
                               //.Where(p => p.UserId == request.UserId)
                               .OrderByDescending(p => p.MeasuringDateTime)
                               .ToList();
            var queryGlucose = _dbContextClucose.GlucoseLevels
                               //.Where(p => p.UserId == request.UserId)
                               .OrderByDescending(p => p.MeasuringDateTime)
                               .ToList();

            int j;
            j = 0;
            while (j < queryInsulin.Count)
            {
                if (queryInsulin[j].UserId != request.UserId)
                {
                    queryInsulin.RemoveAt(j);
                }
                else
                    j++;
            }
            j = 0;
            while (j < queryGlucose.Count)
            {
                if (queryGlucose[j].UserId != request.UserId)
                {
                    queryGlucose.RemoveAt(j);
                }
                else
                    j++;
            }

            int ahiCount = request.Number;
            if (queryInsulin.Count + queryGlucose.Count < 5)
                ahiCount = queryInsulin.Count + queryGlucose.Count;

            var set = new List<ActionHistoryItem>();
            int iIns = 0;
            int iGlu = 0;

            if (queryGlucose.Count == 0 && queryInsulin.Count == 0)
                return set;

            for (int i = 0; i < ahiCount; i++)
            {
                if (queryGlucose.Count == 0 || queryGlucose.Count == iGlu)
                {
                    set.Add(
                    new ActionHistoryItem
                    {
                        Type = ActionHistoryType.Insulin,
                        Title = "Инсулин",
                        Value = queryInsulin[iIns].InsulinValue.ToString(),
                        Details = queryInsulin[iIns].InsulinType,
                        DateTime = queryInsulin[iIns].MeasuringDateTime
                    });
                    iIns++;
                }
                else if (queryInsulin.Count == 0 || queryInsulin.Count == iIns)
                {
                    set.Add(
                    new ActionHistoryItem
                    {
                        Type = ActionHistoryType.GlucoseLevel,
                        Title = "Глюкоза",
                        Value = queryGlucose[iGlu].ValueInMmol.ToString(),
                        Details = queryGlucose[iGlu].BeforeAfterEating,
                        DateTime = queryGlucose[iGlu].MeasuringDateTime
                    });
                    iGlu++;
                }
                else if (DateTime.Compare(queryInsulin[iIns].MeasuringDateTime, 
                    queryGlucose[iGlu].MeasuringDateTime) > 0)
                {
                    set.Add(
                    new ActionHistoryItem
                    {
                        Type = ActionHistoryType.Insulin,
                        Title = "Инсулин",
                        Value = queryInsulin[iIns].InsulinValue.ToString(),
                        Details = queryInsulin[iIns].InsulinType,
                        DateTime = queryInsulin[iIns].MeasuringDateTime
                    });
                    iIns++;
                }
                else
                {
                    set.Add(
                    new ActionHistoryItem
                    {
                        Type = ActionHistoryType.GlucoseLevel,
                        Title = "Глюкоза",
                        Value = queryGlucose[iGlu].ValueInMmol.ToString(),
                        Details = queryGlucose[iGlu].BeforeAfterEating,
                        DateTime = queryGlucose[iGlu].MeasuringDateTime
                    });
                    iGlu++;
                }
            }

            return set;
        }
    }
}