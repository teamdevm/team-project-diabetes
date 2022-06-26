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
    public class GetCarbohydratesCommandHandler : IRequestHandler<GetCarbohydratesCommand, List<CarbohydrateNote>>
    {
        private readonly ICarbohydrateNoteDbContext _dbContextCarb;
        private readonly IFoodPortionDbContext _dbContextPortion;
        private readonly IFoodDbContext _dbContextFood;
        public GetCarbohydratesCommandHandler
            (ICarbohydrateNoteDbContext dbContextCarb, IFoodDbContext dbContextFood, IFoodPortionDbContext dbContextPortion)
        {
            _dbContextCarb = dbContextCarb;
            _dbContextPortion = dbContextPortion;
            _dbContextFood = dbContextFood;
        }
        public async Task<List<CarbohydrateNote>> Handle(GetCarbohydratesCommand request, CancellationToken cancellationToken)
        {
            var queryCarbs = await _dbContextCarb.CarbohydrateNotes
                .ToListAsync(cancellationToken);
            var queryPortions = await _dbContextPortion.FoodPortions.ToListAsync(cancellationToken);
            var queryFood = await _dbContextFood.Foods.ToListAsync(cancellationToken);

            queryCarbs = queryCarbs.Where(a => request.DateFilter(a.MeasuringDateTime))
                .OrderBy(a => a.MeasuringDateTime)
                .ToList();
            queryCarbs.ForEach(a => a.Portions = queryPortions.Where(b => b.CarbohydrateNoteId == a.Id).ToList());
            queryCarbs.ForEach(a => a.Portions.ForEach(b => b.Food = queryFood.Find(c => c.Id == b.FoodId)));
            return queryCarbs;
        }
    }
}
