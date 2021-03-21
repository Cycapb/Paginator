using System.Collections.Generic;
using Paginator.Abstract;
using Paginator.Models;

namespace Paginator.Concrete
{
    public class Paginator:IPaginator
    {
        public IList<PageItem> Paginate(int currentPage,  int totalPages)
        {
            var delta = 2;
            var left = currentPage - delta;
            var right = currentPage + delta + 1;
            var range = new List<int>();
            var rangeWithDots = new List<PageItem>();
            var l = 0;

            for (var i = 1; i <= totalPages; i++)
            {
                if (i == 1 || i == totalPages || i >= left && i < right)
                {
                    range.Add(i);
                }
            }

            foreach (var i in range)
            {
                if (l != 0)
                {
                    if (i - l == 2)
                    {
                        rangeWithDots.Add(new PageItem((l + 1).ToString()));
                    }
                    else if (i - l != 1)
                    {
                        rangeWithDots.Add(new PageItem("..."));
                    }
                }
                if (i == 0)
                {
                    continue;
                }
                rangeWithDots.Add(new PageItem(i.ToString()));
                l = i;
            }
            return rangeWithDots;
        }
    }
}
