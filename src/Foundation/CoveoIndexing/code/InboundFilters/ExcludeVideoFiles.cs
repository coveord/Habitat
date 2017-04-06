using Coveo.SearchProvider.Pipelines;
using Coveo.SearchProvider.InboundFilters;

namespace SitecoreHabitatDemo
{
    public class ExcludeVideoFiles : AbstractCoveoInboundFilterProcessor
    {
        public override void Process(CoveoInboundFilterPipelineArgs args)
        {
            if (args.IndexableToIndex != null && !args.IsExcluded && ShouldExecute(args))
            {
                var extension = args.IndexableToIndex.Item.GetFieldValue("Extension");

                if ((extension == "mp4") || (extension == "m4v"))
                {
                    args.IsExcluded = true;
                }
            }
        }
    }
}