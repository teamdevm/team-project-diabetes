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
                .Include(f=>f.Portions)
                .Include(f=>f.CarbohydrateNotes)
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Id == request.Id, cancellationToken);

            if (model == null) 
                return Unit.Value;

            foreach (var portion in model.Portions)
            {
                portion.CarbohydrateNote.Value += portion.Food.Carbohydrate / 100 * portion.MassInGr;
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            
            _context.UsersFoods.Remove(model);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}