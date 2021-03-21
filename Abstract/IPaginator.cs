using System.Collections.Generic;
using Paginator.Models;

namespace Paginator.Abstract
{
    public interface IPaginator
    {
        IList<PageItem> Paginate(int currentPage,  int totalPages);
    }
}