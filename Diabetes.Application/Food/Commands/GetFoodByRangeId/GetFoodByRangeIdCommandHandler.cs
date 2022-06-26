using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Food.Commands.GetFoodByRangeId
{
    public class GetFoodByRangeIdCommandHandler:IRequestHandler<GetFoodByRangeIdCommand, List<Domain.Food>>
    {
        private IFoodDbContext _dbContext;

        public GetFoodByRangeIdCommandHandler(IFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Domain.Food>> Handle(GetFoodByRangeIdCommand request, CancellationToken cancellationToken)
        {
            var foods = new List<Domain.Food>();
            
            foreach (var id in request.Ids)
            {
                foods.Add(await _dbContext.Foods.FirstOrDefaultAsync(f=>f.Id == id, cancellationToken: cancellationToken));
            }

            return foods;
        }
    }
}