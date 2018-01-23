using System;
using System.Collections.Generic;
using System.Text;

namespace Fora
{
    public class PagedList<T> : List<T>
    {
        public int Total { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PagedList(List<T> items, int total, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            Total = total;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            AddRange(items);
        }
    }
}
