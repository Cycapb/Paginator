using Microsoft.AspNetCore.Html;
using System;

namespace Paginator.Abstract
{
    public interface IPageCreator
    {
        IHtmlContent CreatePages(int itemsPerPage, int currentPage, int totalItems, string idToReplace, Func<int, string> pageUrl);
    }
}