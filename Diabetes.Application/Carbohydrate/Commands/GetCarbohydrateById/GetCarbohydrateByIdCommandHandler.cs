using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Carbohydrate.Commands.GetCarbohydrateById
{
    public class GetCarbohydrateByIdCommandHandler:IRequestHandler<GetCarbohydrateByIdCommand, CarbohydrateNote>
    {
        private ICarbohydrateNoteDbContext _dbContext;

        public GetCarbohydrateByIdCommandHandler(ICarbohydrateNoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarbohydrateNote> Handle(GetCarbohydrateByIdCommand request, CancellationToken cancellationToken)
        {
            return await _dbContext.CarbohydrateNotes
                .Include(c=>c.Portions)
                .Include(c=>c.Foods)
                .FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId, cancellationToken);
        }
    }
}