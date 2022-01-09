using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Core.ViewModels
{
    public class PagedList<TEntity> : List<TEntity>
        where TEntity : class
    {
        public PagedList(int currentPage, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            PageSize = pageSize < 1 ? 30 : pageSize;
            CurrentPage = Math.Max(Math.Min(currentPage, TotalPages), 1);
        }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling(TotalCount / (double)PageSize);
            }
        }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasNextPage
        {
            get
            {
                return TotalPages > 0 && CurrentPage < TotalPages ? true : false;
            }
        }
        public bool HasPreviousPage
        {
            get
            {
                return TotalPages > 0 && CurrentPage > 1 ? true : false;
            }
        }
    }
}
