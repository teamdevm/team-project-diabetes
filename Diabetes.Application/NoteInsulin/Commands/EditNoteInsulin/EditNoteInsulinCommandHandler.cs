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
            Domain.NoteInsulin lastEntity = _dbContext.InsulinNotes.Find(request.Id);

            if (lastEntity != null)
            {
                var entity = new Domain.NoteInsulin
                {
                    UserId = request.UserId,
                    Id = request.Id,
                    InsulinValue = request.InsulinValue,
                    MeasuringDateTime = request.MeasuringDateTime,
                    InsulinType = request.InsulinType,
                    Comment = request.Comment
                };

                _dbContext.InsulinNotes.Update(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
