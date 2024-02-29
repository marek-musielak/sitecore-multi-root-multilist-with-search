using System;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.Pipelines.GetGlobalFilters;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Diagnostics;

namespace MyAssembly.MyNamespace
{
    /// <summary>
    /// Adds AND-OR expression with all the Multilist with Search start search locations
    /// </summary>
    public class AddMultiRootExpressionToQuery : GetGlobalFiltersProcessor
    {
        public override void Process(GetGlobalFiltersArgs args)
        {
            try
            {
                var location = HttpContext.Current?.Request.Form["StartSearchLocation"] ?? HttpContext.Current?.Request["StartSearchLocation"];

                if (location != null && location.Contains('|') && args.Query is IQueryable<UISearchResult> queryable)
                {
                    var expression = PredicateBuilder.False<UISearchResult>();

                    foreach (var root in location.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        expression = expression.Or(i => i["_path"].Equals(root));
                    }

                    args.Query = queryable.Where(expression);
                }
            }
            catch (Exception exc)
            {
                Log.Warn("Exception in AddMultiRootExpressionToQuery", exc, this);
            }
        }
    }
}
