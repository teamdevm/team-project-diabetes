using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.UsersFood.Commands.GetUsersFoodItemsList
{
    public class GetUsersFoodItemsListCommandHandler:IRequestHandler<GetUsersFoodItemsListCommand,PaginatedList<Domain.UsersFood>>
    {
        private readonly IUsersFoodDbContext _dbContext;

        public GetUsersFoodItemsListCommandHandler(IUsersFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<PaginatedList<Domain.UsersFood>> Handle(GetUsersFoodItemsListCommand request, CancellationToken cancellationToken)
        {
            var query = _dbContext.UsersFoods
                .Where(f => f.UserId == request.UserId && f.Name.ToLower().StartsWith(request.SearchString.ToLower()))
                .OrderBy(f => f.Name);
            var count = await query.CountAsync(cancellationToken);
            var items =  await query
                .Skip((request.CurrentPage-1)*request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken: cancellationToken);

            return new PaginatedList<Domain.UsersFood>(items, count, request.CurrentPage, request.PageSize);
        }
    }
}