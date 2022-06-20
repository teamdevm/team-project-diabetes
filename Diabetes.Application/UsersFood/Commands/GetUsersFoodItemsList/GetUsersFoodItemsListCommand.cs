using System;
using System.Collections.Generic;
using Diabetes.Domain.Common;
using MediatR;

namespace Diabetes.Application.UsersFood.Commands.GetUsersFoodItemsList
{
    public class GetUsersFoodItemsListCommand:IRequest<PaginatedList<Domain.UsersFood>>
    {
        public string SearchString
        {
            get => _searchString ?? "";

            set => _searchString = value;
        }

        private string _searchString;

        public int PageSize { get; set; }
        public int CurrentPage { get; set; } = 1;
        public Guid UserId { get; set; }
        
    }
}