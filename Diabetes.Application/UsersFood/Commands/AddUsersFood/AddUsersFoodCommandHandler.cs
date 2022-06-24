using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Food.Commands.GetFoodListItems;
using Diabetes.Application.Interfaces;
using MediatR;

namespace Diabetes.Application.UsersFood.Commands.AddUsersFood
{
    public class AddUsersFoodCommandHandler:IRequestHandler<AddUsersFoodCommand, Unit>
    {
        
        private readonly IUsersFoodDbContext _dbContext;

        public AddUsersFoodCommandHandler(IUsersFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
        public async Task<Unit> Handle(AddUsersFoodCommand request, CancellationToken cancellationToken)
        {
            var model = new Domain.UsersFood
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UserId = request.UserId,
                Details = request.Details,
                Carbohydrate = request.Carbohydrate,
                Fat = request.Fat,
                Kcal = request.Kcal,
                Protein = request.Protein
            };
            
            _dbContext.UsersFoods.Add(model);

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}