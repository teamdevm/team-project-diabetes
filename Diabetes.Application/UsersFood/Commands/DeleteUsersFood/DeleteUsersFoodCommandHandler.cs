using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.UsersFood.Commands.DeleteUsersFood
{
    public class DeleteUsersFoodCommandHandler:IRequestHandler<DeleteUsersFoodCommand, Unit>
    {
        private IUsersFoodDbContext _context;

        public DeleteUsersFoodCommandHandler(IUsersFoodDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUsersFoodCommand request, CancellationToken cancellationToken)
        {
            var model = await _context.UsersFoods
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Id == request.Id, cancellationToken);

            if (model != null)
            {
                _context.UsersFoods.Remove(model);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}