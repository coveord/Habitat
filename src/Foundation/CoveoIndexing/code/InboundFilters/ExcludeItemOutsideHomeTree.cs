using Coveo.SearchProvider.Pipelines;
using Coveo.SearchProvider.InboundFilters;

namespace Sitecore.Foundation.CoveoIndexing
{
    public class ExcludeItemOutsideHomeTree : AbstractCoveoInboundFilterProcessor
    {
        public override void Process(CoveoInboundFilterPipelineArgs args)
        {
            if (args.IndexableToIndex != null && !args.IsExcluded && ShouldExecute(args))
            {
                var path = args.IndexableToIndex.Item.Paths.FullPath;

                if (path.Contains("Settings") || path.Contains("Global")  || path.Contains("Campaigns"))
                {
                    args.IsExcluded = true;
                }
            }
        }
    }
}