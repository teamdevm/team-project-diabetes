using System;
using System.Collections.Generic;
using Diabetes.Domain;
using MediatR;

namespace Diabetes.Application.ActionHistoryItems.Commands.GetActionHistoryItems
{
    public class GetActionHistoryItemsCommand : IRequest<List<Domain.ActionHistoryItem>>
    {
        public Guid UserId { get; set; }
        public int Number { get; set; }
    }
}