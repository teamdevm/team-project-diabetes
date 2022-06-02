using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using MediatR;

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
            Domain.NoteInsulin entity = _dbContext.InsulinNotes.Find(request.Id);

            if (entity != null)
            {
                if (entity.UserId == request.UserId)
                {
                    entity.InsulinValue = request.InsulinValue;
                    entity.MeasuringDateTime = request.MeasuringDateTime;
                    entity.InsulinType = request.InsulinType;
                    entity.Comment = request.Comment;

                    _dbContext.InsulinNotes.Update(entity);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
