using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.FoodForNote.Commands.GetUsersFoodForNoteList
{
    public class GetUsersFoodForNoteListCommandHandler:IRequestHandler<GetUsersFoodForNoteListCommand ,PaginatedList<Domain.UsersFood>>
    {
        private IUsersFoodDbContext _dbContext;

        public GetUsersFoodForNoteListCommandHandler(IUsersFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<Domain.UsersFood>> Handle(GetUsersFoodForNoteListCommand request, CancellationToken cancellationToken)
        {
            var query = _dbContext.UsersFoods
                .Where(f => !request.UsedFoods.Contains(f.Id) && f.Name.ToLower().StartsWith(request.SearchString.ToLower()) 
                                                              && f.UserId == request.UserId)
                .OrderBy(f => f.Name);

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<Domain.UsersFood>(items, count, request.CurrentPage, request.PageSize);
        }
    }
}