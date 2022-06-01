using System;
using System.Threading;
using System.Threading.Tasks;
using Diabetes.Application.Interfaces;
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
            var noteInsulin = new Domain.NoteInsulin
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
                InsulinValue = request.InsulinValue,
                MeasuringDateTime = request.MeasuringDateTime,
                InsulinType = request.InsulinType,
                Comment = request.Comment
            };

            InsulinActionsDb.AddToDb(_dbContext, noteInsulin, cancellationToken);

            return Unit.Value;
        }
    }
}
