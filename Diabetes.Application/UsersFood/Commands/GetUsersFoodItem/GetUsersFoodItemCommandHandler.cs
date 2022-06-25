using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.UsersFood.Commands.GetUsersFoodItem
{
    public class GetUsersFoodItemCommandHandler:IRequestHandler<GetUsersFoodItemCommand, Domain.UsersFood>
    {
        private IUsersFoodDbContext _context;

        public GetUsersFoodItemCommandHandler(IUsersFoodDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.UsersFood> Handle(GetUsersFoodItemCommand request, CancellationToken cancellationToken)
        {
            return await _context.UsersFoods
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Id == request.Id, cancellationToken);
        }
    }
}