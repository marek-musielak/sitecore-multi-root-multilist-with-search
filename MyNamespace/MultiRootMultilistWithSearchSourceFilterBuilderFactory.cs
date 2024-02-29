using Sitecore.Buckets.FieldTypes;
using Sitecore.Data.Items;

namespace MyAssembly.MyNamespace
{
    /// <summary>
    /// Returns custom builder in place of Sitecore default SourceFilterBuilder
    /// </summary>
    public class MultiRootMultilistWithSearchSourceFilterBuilderFactory : SourceFilterBuilderFactory
    {
        public override SourceFilterBuilder CreateSourceFilterBuilder(
            Item targetItem,
            string fieldId,
            string fieldSource)
        {
            return new MultiRootMultilistWithSearchSourceFilterBuilder(targetItem, fieldId, fieldSource);
        }
    }
}
