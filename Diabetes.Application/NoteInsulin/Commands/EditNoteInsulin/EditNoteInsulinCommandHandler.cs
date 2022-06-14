using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.NoteInsulin.Commands.CreateNoteInsulin
{
    public class EditNoteInsulinCommandHandler : IRequestHandler<EditNoteInsulinCommand, Unit>
    {
        private readonly INoteInsulinDbContext _dbContext;

        public EditNoteInsulinCommandHandler(INoteInsulinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(EditNoteInsulinCommand request, CancellationToken cancellationToken)
        {
            var insulin = await _dbContext.InsulinNotes
                .FirstOrDefaultAsync(i=>i.Id == request.Id && i.UserId == request.UserId, cancellationToken);

            if (insulin != null)
            {

                insulin.Value = request.InsulinValue;
                insulin.MeasuringDateTime = request.MeasuringDateTime;
                insulin.InsulinType = request.InsulinType;
                insulin.Comment = request.Comment;
                insulin.LastUpdate = DateTime.Now;

                _dbContext.InsulinNotes.Update(insulin);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
