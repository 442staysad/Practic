using Microsoft.AspNetCore.Mvc;
using Onion.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Onion.Web.ViewModels
{
    public class PaginatedListViewModel
    {
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalItems { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public string FilterString { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd.MM.yyyy",ApplyFormatInEditMode =true,ConvertEmptyStringToNull =true,NullDisplayText ="дд.мм.гггг")]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public string SortDirection { get; set; }
        public string SortField { get; set; }
        public string Action { get; set; } = "Index";

        public IEnumerable<object> ListItems { get; set; }

        public PaginatedListViewModel(int count, int pageIndex, IEnumerable<object> listItems, int pageSize=10)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.TotalItems = count;
            this.ListItems=listItems.Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize);

            int totalPages = (int)Math.Ceiling(count / (decimal)pageSize);

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
