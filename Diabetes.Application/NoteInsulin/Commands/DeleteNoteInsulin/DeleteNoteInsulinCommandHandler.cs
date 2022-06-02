using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;

namespace Diabetes.Application.NoteInsulin.Commands.CreateNoteInsulin
{
    public class DeleteNoteInsulinCommandHandler : IRequestHandler<DeleteNoteInsulinCommand, Unit>
    {
        private readonly INoteInsulinDbContext _dbContext;

        public DeleteNoteInsulinCommandHandler(INoteInsulinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteNoteInsulinCommand request, CancellationToken cancellationToken)
        {
            Domain.NoteInsulin entity = _dbContext.InsulinNotes.Find(request.Id);

            if (entity != null)
            {
                if (entity.UserId == request.UserId)
                {
                    _dbContext.InsulinNotes.Remove(entity);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
