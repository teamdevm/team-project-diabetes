namespace Diabetes.MVC.Models.Components
{
    public class PaginationViewModel
    {
        public string SearchString { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int PageIndex { get; set; }

        public PaginationViewModel(string searchString, bool hasPreviousPage, bool hasNextPage, int pageIndex)
        {
            SearchString = searchString;
            HasPreviousPage = hasPreviousPage;
            HasNextPage = hasNextPage;
            PageIndex = pageIndex;
        }
    }
}