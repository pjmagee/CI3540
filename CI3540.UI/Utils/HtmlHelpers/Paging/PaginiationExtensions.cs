using System;
using System.Web.Mvc;

/*
<!-- Usage in razor (note @model): -->
@using BootstrapSupport
@model IPagedList

@Html.Pager(Model.PageIndex,
            Model.TotalPages,
            x => Url.Action("Index", new {page = x}),
            " pagination-right")

// Index action on the HomeController from the sample project:
public ActionResult Index(int page = 1)
{
    var pageSize = 3;
    var homeInputModels = _models;
    return View(homeInputModels.ToPagedList(page, pageSize));
}
*/

namespace CI3540.UI.Utils.HtmlHelpers.Paging
{
    public static class PaginiationExtensions
    {
        /// <summary>
        /// Renders a bootstrap standard pagination bar
        /// </summary>
        /// <remarks>
        /// http://twitter.github.com/bootstrap/components.html#pagination
        /// </remarks>
        /// <param name="helper">The html helper</param>
        /// <param name="currentPage">Zero-based page number of the page on which the pagination bar should be rendered</param>
        /// <param name="totalPages">The total number of pages</param>
        /// <param name="pageUrl">
        ///     Expression to construct page url (e.g.: x => Url.Action("Index", new {page = x}))
        /// </param>
        /// <param name="additionalPagerCssClass">Additional classes for the navigation div (e.g. "pagination-right pagination-mini")</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, int currentPage, int totalPages, Func<int, string> pageUrl, string additionalPagerCssClass = "")
        {
            if (totalPages <= 1)
                return MvcHtmlString.Empty;

            var div = new TagBuilder("div");
            div.AddCssClass("pagination");
            div.AddCssClass(additionalPagerCssClass);

            var ul = new TagBuilder("ul");

            for (var i = 1; i < totalPages + 1; i++)
            {
                var li = new TagBuilder("li");
                if (i == (currentPage + 1))
                    li.AddCssClass("active");

                var a = new TagBuilder("a");
                a.MergeAttribute("href", pageUrl(i));
                a.SetInnerText(i.ToString());

                li.InnerHtml = a.ToString();
                
                ul.InnerHtml += li;
            }

            div.InnerHtml = ul.ToString();

            return MvcHtmlString.Create(div.ToString());
        }

        
        public static MvcHtmlString SmartPager(this HtmlHelper helper, int currentPage, int totalPages, Func<int, string> pageUrl, string additionalPagerCssClass = "")
        {
            currentPage++;

            if (totalPages <= 1)
                return MvcHtmlString.Empty;

            int adjacents = 2;

            var div = new TagBuilder("div");
            div.AddCssClass("pagination");
            div.AddCssClass(additionalPagerCssClass);

            var ul = new TagBuilder("ul");

            // Start Buttons
            if (currentPage > 1) // Has previous page
            {
                var firstLi = new TagBuilder("li");
                var firstA = new TagBuilder("a");
                firstA.MergeAttribute("href", pageUrl(1));
                firstA.SetInnerText("<<");
                firstLi.InnerHtml = firstA.ToString();
                ul.InnerHtml += firstLi;

                var secondLi = new TagBuilder("li");
                var secondA = new TagBuilder("a");
                secondA.MergeAttribute("href", pageUrl(currentPage - 1));
                secondA.SetInnerText("<");
                secondLi.InnerHtml = secondA.ToString();
                ul.InnerHtml += secondLi;
            }
            else
            {
                var firstLi = new TagBuilder("li");
                firstLi.InnerHtml = "<span>&lt;</span>";
                ul.InnerHtml += firstLi;

                var secondLi = new TagBuilder("li");
                secondLi.InnerHtml = "<span>&lt;&lt;</span>";
                ul.InnerHtml += secondLi;
            }


            if (totalPages < 7 + (adjacents*2))
            {
                for (var i = 1; i <= totalPages; i++)
                {
                    var li = new TagBuilder("li");
                    var a = new TagBuilder("a");
                    a.MergeAttribute("href", pageUrl(i));
                    a.SetInnerText(i.ToString());

                    if (i == currentPage)
                    {
                        li.AddCssClass("active");
                    }

                    li.InnerHtml = a.ToString();

                    ul.InnerHtml += li;
                }
            }
            else if (totalPages >= 7 + (adjacents*2))
            {
                if (currentPage < 1 + (adjacents*3))
                {
                    for (var i = 1; i < (4 + (adjacents*2)); i++)
                    {
                        var li = new TagBuilder("li");
                        var a = new TagBuilder("a");
                        a.MergeAttribute("href", pageUrl(i));
                        a.SetInnerText(i.ToString());

                        if (i == currentPage)
                        {
                            li.AddCssClass("active");
                        }

                        li.InnerHtml = a.ToString();
                        ul.InnerHtml += li;
                    }

                    // skip a bunch
                    var skipLi = new TagBuilder("li");
                    skipLi.AddCssClass("disabled");
                    var span = new TagBuilder("span");
                    span.SetInnerText("...");
                    skipLi.InnerHtml = span.ToString();
                    ul.InnerHtml += skipLi;

                    // Last page minus 1
                    var lpm1 = new TagBuilder("li");
                    var alpm1 = new TagBuilder("a");
                    alpm1.MergeAttribute("href", pageUrl(totalPages - 1));
                    alpm1.SetInnerText((totalPages - 1).ToString());
                    lpm1.InnerHtml = alpm1.ToString();
                    ul.InnerHtml += lpm1;

                    // Last page
                    var lp = new TagBuilder("li");
                    var alp = new TagBuilder("a");
                    alp.MergeAttribute("href", pageUrl(totalPages));
                    alp.SetInnerText(totalPages.ToString());
                    lp.InnerHtml = alp.ToString();
                    ul.InnerHtml += lp;

                }
                else if (totalPages - (adjacents*2) > currentPage && currentPage > (adjacents*2))
                {
                    // First page
                    var fp = new TagBuilder("li");
                    var afp = new TagBuilder("a");
                    afp.MergeAttribute("href", pageUrl(1));
                    afp.SetInnerText(1.ToString());
                    fp.InnerHtml = afp.ToString();

                    ul.InnerHtml += fp;

                    // Second page
                    var sp = new TagBuilder("li");
                    var asp = new TagBuilder("a");
                    asp.MergeAttribute("href", pageUrl(2));
                    asp.SetInnerText(2.ToString());
                    sp.InnerHtml = asp.ToString();

                    ul.InnerHtml += sp;

                    // skip a bunch
                    var skipLi = new TagBuilder("li");
                    skipLi.AddCssClass("disabled");
                    var span = new TagBuilder("span");
                    span.SetInnerText("...");
                    skipLi.InnerHtml = span.ToString();
                    ul.InnerHtml += skipLi;

                    for (var i = currentPage - adjacents; i <= currentPage + adjacents; i++)
                    {
                        var li = new TagBuilder("li");
                        var a = new TagBuilder("a");
                        a.MergeAttribute("href", pageUrl(i));
                        a.SetInnerText(i.ToString());

                        if (i == currentPage)
                        {
                            li.AddCssClass("active");
                        }

                        li.InnerHtml = a.ToString();
                        ul.InnerHtml += li;
                    }

                    // skip a bunch
                    ul.InnerHtml += skipLi;

                    // Last page minus 1
                    var lpm1 = new TagBuilder("li");
                    var alpm1 = new TagBuilder("a");
                    alpm1.MergeAttribute("href", pageUrl(totalPages - 1));
                    alpm1.SetInnerText((totalPages - 1).ToString());
                    lpm1.InnerHtml = alpm1.ToString();
                    ul.InnerHtml += lpm1;

                    // Last page
                    var lp = new TagBuilder("li");
                    var alp = new TagBuilder("a");
                    alp.MergeAttribute("href", pageUrl(totalPages));
                    alp.SetInnerText(totalPages.ToString());
                    lp.InnerHtml = alp.ToString();
                    ul.InnerHtml += lp;

                }
                else
                {
                    // First page
                    var fp = new TagBuilder("li");
                    var afp = new TagBuilder("a");
                    afp.MergeAttribute("href", pageUrl(1));
                    afp.SetInnerText(1.ToString());
                    fp.InnerHtml = afp.ToString();
                    ul.InnerHtml += fp;

                    // Second page
                    var sp = new TagBuilder("li");
                    var asp = new TagBuilder("a");
                    asp.MergeAttribute("href", pageUrl(2));
                    asp.SetInnerText(2.ToString());
                    sp.InnerHtml = asp.ToString();
                    ul.InnerHtml += sp;

                    // skip a bunch
                    var skipLi = new TagBuilder("li");
                    skipLi.AddCssClass("disabled");
                    var span = new TagBuilder("span");
                    span.SetInnerText("...");
                    skipLi.InnerHtml = span.ToString();
                    ul.InnerHtml += skipLi;

                    for (var i = totalPages - (1 + (adjacents*3)); i <= totalPages; i++)
                    {
                        var li = new TagBuilder("li");
                        var a = new TagBuilder("a");
                        a.MergeAttribute("href", pageUrl(i));
                        a.SetInnerText(i.ToString());

                        if (i == currentPage)
                        {
                            li.AddCssClass("active");
                        }

                        li.InnerHtml = a.ToString();
                        ul.InnerHtml += li;
                    }
                }
            }

            // End Buttons
            if (currentPage < totalPages)
            {
                var firstLi = new TagBuilder("li");
                var firstA = new TagBuilder("a");
                firstA.MergeAttribute("href", pageUrl(currentPage + 1));
                firstA.SetInnerText(">");
                firstLi.InnerHtml = firstA.ToString();
                ul.InnerHtml += firstLi;

                var secondLi = new TagBuilder("li");
                var secondA = new TagBuilder("a");
                secondA.MergeAttribute("href", pageUrl(totalPages));
                secondA.SetInnerText(">>");
                secondLi.InnerHtml = secondA.ToString();
                ul.InnerHtml += secondLi;
            }
            else
            {
                var firstLi = new TagBuilder("li");
                firstLi.InnerHtml = "<span>&gt;</span>";
                ul.InnerHtml += firstLi;

                var secondLi = new TagBuilder("li");
                secondLi.InnerHtml = "<span>&gt;&gt;</span>";
                ul.InnerHtml += secondLi;
            }

            div.InnerHtml = ul.ToString();

            return MvcHtmlString.Create(div.ToString());
        }
    }
}