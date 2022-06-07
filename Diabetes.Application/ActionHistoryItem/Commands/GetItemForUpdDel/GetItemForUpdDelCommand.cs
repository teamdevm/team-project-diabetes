using System;
using MediatR;
using Diabetes.Domain;

namespace Diabetes.Application.ActionHistoryItem.Commands.GetItemForUpdDel
{
    public class GetItemForUpdDelCommand: IRequest<Domain.ActionHistoryItem>
    {
        public Guid Id { get; set; }
        public ActionHistoryType? Type { get; set; }
    }
}
