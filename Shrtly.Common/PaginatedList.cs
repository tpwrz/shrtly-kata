using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrtly.Common
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; }
        public int TotalRecords { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public PaginatedList(int totalRecords, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalRecords = totalRecords;
            PageSize = pageSize;
            TotalPages = (totalRecords > 0) ? ((int)Math.Ceiling(TotalRecords / (double)pageSize)) : totalRecords;
        }

        public PaginatedList(IEnumerable<T> items, int totalRecords, int pageIndex, int pageSize)
            : this(totalRecords, pageIndex, pageSize)
        {
            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}
