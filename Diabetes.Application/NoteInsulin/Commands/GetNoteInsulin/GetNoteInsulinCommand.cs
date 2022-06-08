using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Diabetes.Application.NoteInsulin.Commands.GetNoteInsulin
{
    public class GetNoteInsulinCommand: IRequest<Domain.NoteInsulin>
    {
        public Guid Id { get; set; }
    }
}
