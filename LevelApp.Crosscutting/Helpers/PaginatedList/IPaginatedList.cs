using System.Collections.Generic;

namespace LevelApp.Crosscutting.Helpers.PaginatedList
{
    public interface IPaginatedList<T> : IList<T>
    {
        int PageIndex { get; }
        int TotalPages { get; }
    }
}