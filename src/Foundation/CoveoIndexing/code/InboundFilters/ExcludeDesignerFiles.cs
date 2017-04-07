using Coveo.SearchProvider.Pipelines;
using Coveo.SearchProvider.InboundFilters;

namespace Sitecore.Foundation.CoveoIndexing
{
    public class ExcludeDesignerFiles : AbstractCoveoInboundFilterProcessor
    {
        public override void Process(CoveoInboundFilterPipelineArgs args)
        {
            if (args.IndexableToIndex != null && !args.IsExcluded && ShouldExecute(args))
            {
                var extension = args.IndexableToIndex.Item.GetFieldValue("Extension");

                if ((extension == "tif") || (extension == "indd") || (extension == "psd"))
                {
                    args.IsExcluded = true;
                }
            }
        }
    }
}