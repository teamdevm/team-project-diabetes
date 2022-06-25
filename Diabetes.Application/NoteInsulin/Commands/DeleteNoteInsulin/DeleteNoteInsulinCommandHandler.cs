using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var insulinNote = await _dbContext.InsulinNotes
                .FirstOrDefaultAsync(i=>i.Id == request.Id && i.UserId == request.UserId, cancellationToken);

            if (insulinNote != null)
            {
                //удаляем и сохраняем изменения
                _dbContext.InsulinNotes.Remove(insulinNote);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
