using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Carbohydrate.Commands.DeleteCarbohydrate
{
    public class DeleteCarbohydrateCommandHandler:IRequestHandler<DeleteCarbohydrateCommand, Unit>
    {
        private ICarbohydrateNoteDbContext _dbContext;

        public DeleteCarbohydrateCommandHandler(ICarbohydrateNoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Unit> Handle(DeleteCarbohydrateCommand request, CancellationToken cancellationToken)
        {
            var note = await _dbContext.CarbohydrateNotes
                .FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId == request.UserId, cancellationToken);

            if (note != null)
            {
                _dbContext.CarbohydrateNotes.Remove(note);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}