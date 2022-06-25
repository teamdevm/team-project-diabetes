using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using Diabetes.Domain.Normalized;
using MediatR;

namespace Diabetes.Application.NoteInsulin.Commands.CreateNoteInsulin
{
    public class CreateNoteInsulinCommandHandler : IRequestHandler<CreateNoteInsulinCommand, Unit>
    {
        private readonly INoteInsulinDbContext _dbContext;

        public CreateNoteInsulinCommandHandler(INoteInsulinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateNoteInsulinCommand request, CancellationToken cancellationToken)
        {
            var noteInsulin = new InsulinNote()
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
                Value = request.InsulinValue,
                MeasuringDateTime = request.MeasuringDateTime,
                LastUpdate = DateTime.Now,
                InsulinType = request.InsulinType,
                Comment = request.Comment
            };

            await _dbContext.InsulinNotes.AddAsync(noteInsulin, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
