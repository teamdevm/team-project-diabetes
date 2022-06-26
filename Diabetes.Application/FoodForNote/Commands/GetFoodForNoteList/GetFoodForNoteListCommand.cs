using System;
using System.Collections.Generic;
using Diabetes.Domain.Common;
using MediatR;

namespace Diabetes.Application.FoodForNote.Commands.GetFoodForNoteList
{
    public class GetFoodForNoteListCommand : IRequest<PaginatedList<Domain.Food>>
    {
        public string SearchString
        {
            get => _searchString ?? "";

            set => _searchString = value;
        }

        private string _searchString;

        public int PageSize { get; set; }
        public int CurrentPage { get; set; } = 1;

        public List<Guid> UsedFoods { get; set; } = new List<Guid>();
    }
}