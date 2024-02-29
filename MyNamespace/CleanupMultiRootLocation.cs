using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sitecore.Buckets.Pipelines;
using Sitecore.Buckets.Pipelines.UI.Search;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Diagnostics;

namespace MyAssembly.MyNamespace
{
    /// <summary>
    /// Performs cleanup of values "location" elements in UISearchArgs stringModel field.
    /// Removes all the pipe separated values
    /// </summary>
    public class CleanupMultiRootLocation : BucketsPipelineProcessor<UISearchArgs>
    {
        public override void Process(UISearchArgs args)
        {
            try
            {
                if (args.GetType().GetField("stringModel", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(args) is List<SearchStringModel> stringModel)
                {
                    var searchStringModels = stringModel.Where(m => m.Type == "location" && m.Value.Contains('|')).ToList();
                    foreach (var model in searchStringModels)
                    {
                        stringModel.Remove(model);
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Warn("Exception in CleanupMultiRootLocation", exc, this);
            }
        }
    }
}
