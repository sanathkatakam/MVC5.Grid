﻿using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NonFactors.Mvc.Grid
{
    public class HtmlGrid<T> : IHtmlGrid<T>
    {
        public IGrid<T> Grid { get; set; }
        public HtmlHelper Html { get; set; }
        public String PartialViewName { get; set; }

        public HtmlGrid(HtmlHelper html, IGrid<T> grid)
        {
            Html = html;
            Grid = grid;
            PartialViewName = "MvcGrid/_Grid";
            grid.ViewContext ??= html.ViewContext;
            grid.Query ??= grid.ViewContext.HttpContext.Request.QueryString;
        }

        public virtual String ToHtmlString()
        {
            return Html.Partial(PartialViewName, Grid).ToHtmlString();
        }
    }
}
