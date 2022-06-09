using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Diabetes.Application.Interfaces;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.NoteInsulin.Commands.GetNoteInsulin
{
    public class GetNoteInsulinCommandHandler: IRequestHandler<GetNoteInsulinCommand, Domain.NoteInsulin>
    {
        private readonly INoteInsulinDbContext _dbContextInsulin;
        public GetNoteInsulinCommandHandler(INoteInsulinDbContext dbContextInsulin) =>_dbContextInsulin = dbContextInsulin;
        public async Task<Domain.NoteInsulin> Handle (GetNoteInsulinCommand request, CancellationToken cancellationToken)
        {
            List<Domain.NoteInsulin> queryInsulin = await _dbContextInsulin.InsulinNotes
            .Where(p => p.Id == request.Id)
            .ToListAsync(cancellationToken);

            return queryInsulin.Count == 0 ? null : queryInsulin[0];
        }
    }
}
