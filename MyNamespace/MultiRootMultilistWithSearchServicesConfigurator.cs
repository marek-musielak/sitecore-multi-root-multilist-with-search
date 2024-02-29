using Microsoft.Extensions.DependencyInjection;
using Sitecore.Buckets.FieldTypes;
using Sitecore.DependencyInjection;

namespace MyAssembly.MyNamespace
{
    /// <summary>
    /// Registers custom factory in place of Sitecore default SourceFilterBuilderFactory
    /// </summary>
    public class MultiRootMultilistWithSearchServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection) 
            => serviceCollection.AddSingleton<SourceFilterBuilderFactory, MultiRootMultilistWithSearchSourceFilterBuilderFactory>();
    }
}
