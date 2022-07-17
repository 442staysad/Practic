using System;
using System.Collections.Generic;
using System.Linq;

namespace Onion.Web.ViewModels
{
    public class PagedListViewModel
    {
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalItems { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public string SortField { get; set; }
        public string SortDirection { get; set; }

        public string FilterString { get; set; }

        public string Action { get; set; } = "Index";

        public IEnumerable<object> ListItems { get; set; }

        public PagedListViewModel(int count, int pageIndex, IEnumerable<object> listItems, int pageSize=10)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.TotalItems = count;
            this.ListItems=listItems.Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize)
                                                .AsEnumerable();

            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            TotalPages = totalPages;

            int startPage = pageIndex - 5;
            int endPage = pageIndex + 4;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                    startPage = endPage - 9;
            }


            if (TotalItems == 0)
            {
                StartPage = 0;
                PageIndex = 0;
            }
            else
            {
                StartPage = startPage;
                EndPage = endPage;
            }
        }
    }
}
