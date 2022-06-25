using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain.Enums;
using MediatR;

namespace Diabetes.Application.Carbohydrate.Commands.AddCarbohydrate
{
    public class AddCarbohydrateCommandHandler:IRequestHandler<AddCarbohydrateCommand,Unit>
    {
        private ICarbohydrateNoteDbContext _noteContext;
        private IFoodPortionDbContext _portionDbContext;

        public AddCarbohydrateCommandHandler(ICarbohydrateNoteDbContext noteContext, IFoodPortionDbContext portionDbContext)
        {
            _noteContext = noteContext;
            _portionDbContext = portionDbContext;
        }

        public async Task<Unit> Handle(AddCarbohydrateCommand request, CancellationToken cancellationToken)
        {
            var carbohydrate = new Domain.CarbohydrateNote
            {
                Id = request.MealId,
                Value = request.Value,
                Comment = request.Comment,
                Type = NoteType.Carbohydrate,
                MeasuringDateTime = request.CreatingDateTime,
                LastUpdate = DateTime.Now,
                UserId = request.UserId,
            };

            var portions = request.FoodPortions;
            
            portions.ForEach(p=>p.CarbohydrateNote = carbohydrate);
            
            await _noteContext.CarbohydrateNotes.AddAsync(carbohydrate, cancellationToken);
            await _portionDbContext.FoodPortions.AddRangeAsync(portions, cancellationToken);
            await _noteContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}