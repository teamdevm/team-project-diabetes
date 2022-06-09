using System;
using System.Collections.Generic;
using MediatR;

namespace Diabetes.Application.ActionHistoryItem.Commands.GetAllActionHistoryItems
{
    public class GetAllActionHistoryItemsCommand: IRequest<List<Domain.ActionHistoryItem>>
    {
        public Guid UserId { get; set; }
    }
}