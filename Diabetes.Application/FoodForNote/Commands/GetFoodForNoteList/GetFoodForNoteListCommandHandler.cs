using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.FoodForNote.Commands.GetFoodForNoteList
{
    public class GetFoodForNoteListCommandHandler:IRequestHandler<GetFoodForNoteListCommand ,PaginatedList<Domain.Food>>
    {
        private IFoodDbContext _dbContext;

        public GetFoodForNoteListCommandHandler(IFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<Domain.Food>> Handle(GetFoodForNoteListCommand request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Foods
                .Where(f => !request.UsedFoods.Contains(f.Id) && f.Name.ToLower().StartsWith(request.SearchString.ToLower()) &&
                            f.Discriminator == nameof(Food))
                .OrderBy(f => f.Name);

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<Domain.Food>(items, count, request.CurrentPage, request.PageSize);
        }
    }
}