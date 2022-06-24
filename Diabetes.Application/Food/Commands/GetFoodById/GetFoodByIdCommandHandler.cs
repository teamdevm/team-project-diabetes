using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Food.Commands.GetFoodById
{
    public class GetFoodByIdCommandHandler:IRequestHandler<GetFoodByIdCommand, Domain.Food>
    {
        private readonly IFoodDbContext _dbContext;

        public GetFoodByIdCommandHandler(IFoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Domain.Food> Handle(GetFoodByIdCommand request, CancellationToken cancellationToken)
        {
            var model = await _dbContext.Foods.FirstOrDefaultAsync(f=>f.Id == request.Id, cancellationToken: cancellationToken);

            return model;
        }
    }
}