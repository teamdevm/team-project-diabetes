using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Diabetes.Application.Interfaces;
using System.Threading;
using Diabetes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.NoteInsulin.Commands.GetNoteInsulin
{
    public class GetNoteInsulinCommandHandler: IRequestHandler<GetNoteInsulinCommand, InsulinNote>
    {
        private readonly INoteInsulinDbContext _dbContextInsulin;
        public GetNoteInsulinCommandHandler(INoteInsulinDbContext dbContextInsulin) =>_dbContextInsulin = dbContextInsulin;
        public async Task<InsulinNote> Handle (GetNoteInsulinCommand request, CancellationToken cancellationToken)
        {
            var queryInsulin = await _dbContextInsulin.InsulinNotes
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);

            return queryInsulin;
        }
    }
}
