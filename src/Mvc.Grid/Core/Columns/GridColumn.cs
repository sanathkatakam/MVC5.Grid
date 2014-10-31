﻿using System;
using System.Net;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : IGridColumn<TModel> where TModel : class
    {
        public Func<TModel, TValue> Expression { get; private set; }
        public Boolean IsEncoded { get; private set; }
        public String CssClasses { get; private set; }
        public String Format { get; private set; }
        public String Title { get; private set; }
        public Int32 Width { get; private set; }

        public GridColumn() : this(null)
        {
        }
        public GridColumn(Func<TModel, TValue> expression)
        {
            Expression = expression;
            IsEncoded = true;
        }

        public IGridColumn<TModel> Formatted(String format)
        {
            Format = format;

            return this;
        }
        public IGridColumn<TModel> Encoded(Boolean encode)
        {
            IsEncoded = encode;

            return this;
        }
        public IGridColumn<TModel> Css(String cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public IGridColumn<TModel> SetWidth(Int32 width)
        {
            Width = width;

            return this;
        }
        public IGridColumn<TModel> Titled(String title)
        {
            Title = title;

            return this;
        }

        public IHtmlString ValueFor(IGridRow row)
        {
            String value = GetRawValueFor(row);
            if (IsEncoded) value = WebUtility.HtmlEncode(value);

            return new HtmlString(value);
        }

        private String GetRawValueFor(IGridRow row)
        {
            if (Expression == null)
                return String.Empty;

            TValue value = Expression(row.Model as TModel);
            if (value == null)
                return String.Empty;

            if (Format == null)
                return value.ToString();

            return String.Format(Format, value);
        }
    }
}