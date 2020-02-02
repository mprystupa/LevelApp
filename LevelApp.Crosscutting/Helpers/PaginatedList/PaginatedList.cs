using System;
using System.Collections.Generic;

namespace LevelApp.Crosscutting.Helpers.PaginatedList
{
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            TotalPages = TotalPages > 0 ? TotalPages : 1; 
            
            this.AddRange(items);
        }
    }
}