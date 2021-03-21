using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Paginator.Abstract;
using System;

namespace Paginator.Concrete
{
    public class AjaxPageCreator : IPageCreator
    {
        private readonly IPaginator _paginator;

        public AjaxPageCreator(IPaginator paginator)
        {
            _paginator = paginator;
        }

        public IHtmlContent CreatePages(int itemsPerPage, int currentPage, int totalItems, string idToReplace, Func<int, string> urlCreator)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / itemsPerPage);
            var divTag = new TagBuilder("div");
            
            if (currentPage > 1)
            {
                var arrowElement = CreateArrow(false, currentPage, idToReplace, urlCreator);
                divTag.InnerHtml.AppendHtml(arrowElement);
            }

            var pageItems = _paginator.Paginate(currentPage, totalPages);

            for (int i = 1; i <= pageItems.Count; i++)
            {
                if (pageItems[i - 1].Page == "...")
                {
                    var dotTag = new TagBuilder("button");
                    dotTag.InnerHtml.Append("...");
                    dotTag.AddCssClass("btn btn-default btn-raised btn-round btn-xs");
                    divTag.InnerHtml.AppendHtml(dotTag);
                }
                else
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", urlCreator(int.Parse(pageItems[i - 1].Page)));
                    tag.InnerHtml.Append(pageItems[i - 1].Page);
                    
                    if (pageItems[i - 1].Page == currentPage.ToString())
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary btn-xs");
                    }

                    tag.AddCssClass("btn btn-default btn-raised btn-round btn-xs");
                    tag.MergeAttribute("data-ajax", "true");
                    tag.MergeAttribute("data-ajax-mode", "replace");
                    tag.MergeAttribute("data-ajax-update", idToReplace);
                    tag.MergeAttribute("data-ajax-url", urlCreator(int.Parse(pageItems[i - 1].Page)));
                    divTag.InnerHtml.AppendHtml(tag);
                }
            }

            if (currentPage < totalPages)
            {
                var arrowElement = CreateArrow(true, currentPage, idToReplace, urlCreator);
                divTag.InnerHtml.AppendHtml(arrowElement);
            }

            return divTag;
        }

        private static IHtmlContent CreateArrow(bool forward, int currentPage, string idToReplace, Func<int, string> pageUrl)
        {
            TagBuilder nextTag = new TagBuilder("a");
            nextTag.MergeAttribute("href", pageUrl(forward ? currentPage++ : currentPage--));
            nextTag.InnerHtml.Append(forward ? ">>" : "<<");
            nextTag.AddCssClass("btn btn-default btn-sm");
            nextTag.MergeAttribute("data-ajax", "true");
            nextTag.MergeAttribute("data-ajax-mode", "replace");
            nextTag.MergeAttribute("data-ajax-update", idToReplace);
            nextTag.MergeAttribute("data-ajax-url", pageUrl(forward ? currentPage++ : currentPage--));
            
            return nextTag;
        }
    }
}
