using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.UsersFood.Commands.EditUsersFood
{
    public class EditUsersFoodCommandHandler:IRequestHandler<EditUsersFoodCommand, Unit>
    {
        private IUsersFoodDbContext _context;

        public EditUsersFoodCommandHandler(IUsersFoodDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditUsersFoodCommand request, CancellationToken cancellationToken)
        {
            var model = await _context.UsersFoods
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Id == request.Id, cancellationToken);

            if (model != null)
            {
                model.Name = request.Name;
                model.Details = request.Details;
                model.Kcal = request.Kcal;
                model.Carbohydrate = request.Carbohydrate;
                model.Fat = request.Fat;
                model.Protein = request.Protein;

                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}