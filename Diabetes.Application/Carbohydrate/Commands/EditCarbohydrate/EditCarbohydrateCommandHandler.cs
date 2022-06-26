using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Carbohydrate.Commands.EditCarbohydrate
{
    public class EditCarbohydrateCommandHandler:IRequestHandler<EditCarbohydrateCommand, Unit>
    {
        private ICarbohydrateNoteDbContext _noteContext;
        private IFoodPortionDbContext _portionDbContext;

        public EditCarbohydrateCommandHandler(ICarbohydrateNoteDbContext noteContext, IFoodPortionDbContext portionDbContext)
        {
            _noteContext = noteContext;
            _portionDbContext = portionDbContext;
        }
        
        public async Task<Unit> Handle(EditCarbohydrateCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteContext.CarbohydrateNotes
                .Include(c=>c.Portions)
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId, cancellationToken);
            
            if(note == null)
                return Unit.Value;

            note.Value = request.Value;
            note.Comment = request.Comment;
            note.LastUpdate = DateTime.Now;
            note.MeasuringDateTime = request.CreatingDateTime;
            note.Portions = request.FoodPortions;
            
            await _noteContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}