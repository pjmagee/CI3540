using System.Collections;
using System.Collections.Generic;

namespace CI3540.UI.Utils.HtmlHelpers.Paging
{
    public interface IPagedList<T> : IPagedList, IList<T>
    {

    }

    public interface IPagedList : IEnumerable
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}