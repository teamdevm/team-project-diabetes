using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diabetes.Domain;
using MediatR;

namespace Diabetes.Application.NoteInsulin.Commands.GetNoteInsulin
{
    public class GetNoteInsulinCommand: IRequest<InsulinNote>
    {
        public Guid Id { get; set; }
    }
}
