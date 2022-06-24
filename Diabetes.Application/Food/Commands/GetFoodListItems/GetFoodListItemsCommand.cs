using System.Collections.Generic;
using Diabetes.Domain.Common;
using MediatR;

namespace Diabetes.Application.Food.Commands.GetFoodListItems
{
    public class GetFoodListItemsCommand:IRequest<PaginatedList<Domain.Food>>
    {
        public string SearchString
        {
            get => _searchString ?? "";

            set => _searchString = value;
        }

        private string _searchString;

        public int PageSize { get; set; }
        public int CurrentPage { get; set; } = 1;
    }
}