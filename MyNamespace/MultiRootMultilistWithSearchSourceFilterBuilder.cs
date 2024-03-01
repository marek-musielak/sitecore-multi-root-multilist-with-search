using System;
using System.Linq;
using Sitecore.Buckets.FieldTypes;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace MyAssembly.MyNamespace
{
    /// <summary>
    /// If StartSearchLocation parameter contains '|' character, calculates all the roots from the query and puts them in `location` part
    /// </summary>
    public class MultiRootMultilistWithSearchSourceFilterBuilder : SourceFilterBuilder
    {
        private readonly Item _currentItem;

        public MultiRootMultilistWithSearchSourceFilterBuilder(Item targetCurrentItem, string fieldId, string fieldSource) : base(targetCurrentItem, fieldId, fieldSource)
        {
            _currentItem = targetCurrentItem;
        }

        public override void BuildLocationPart(
            string sourceName,
            string filterName,
            string defaultValue)
        {
            var completed = false;

            try
            {
                var locationValue = SourceParts["StartSearchLocation"];

                if (locationValue != null)
                {
                    GetResult().Add(filterName, MakeValueQueryableList(locationValue));
                    completed = true;
                }
            }
            catch (Exception exc)
            {
                Log.Error("Exception in BuildLocationPart", exc, this);
            }

            // In case of custom code was not executed or there was an exception, let's call default implementation
            if (!completed) 
                base.BuildLocationPart(sourceName, filterName, defaultValue);

        }

        private string MakeValueQueryableList(string filterValue)
        {
            var query = GetQuery(filterValue);

            if (string.IsNullOrEmpty(query))
                return filterValue;

            if (query.StartsWith("fast:", StringComparison.InvariantCultureIgnoreCase))
            {
                Log.Warn("Fast query are no longer supported. Sitecore Queries will be used instead. Query: " + query, this);
                query = query.Substring(5);
            }

            return string.Join("|", _currentItem.Axes.SelectItems(query).Select(item => item.ID.ToShortID().ToString().ToLower()));
        }

        private static string GetQuery(string filterValue) 
            => filterValue == null || !filterValue.StartsWith("query:") 
                ? null 
                : filterValue.Replace("->", "=").Substring(6);
    }
}
