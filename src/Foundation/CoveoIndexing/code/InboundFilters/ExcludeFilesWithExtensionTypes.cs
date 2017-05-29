using Coveo.SearchProvider.InboundFilters;
using Coveo.SearchProvider.Pipelines;

namespace Sitecore.Foundation.CoveoIndexing
{
    public class ExcludeFilesWithExtensionTypes : AbstractCoveoInboundFilterProcessor
    {
        /// <summary>
        /// ID of the field where activation checkbox is specified.
        /// </summary>
        public string ExtensionTypesToExclude { get; set; }

        public override void Process(CoveoInboundFilterPipelineArgs args)
        {
            string[] extensionTypesToExclude = ExtensionTypesToExclude.Split(';');

            if (args.IndexableToIndex != null && !args.IsExcluded && ShouldExecute(args))
            {
                string itemExtension = args.IndexableToIndex.Item.GetFieldValue("Extension");

                foreach (string extensionType in extensionTypesToExclude) {

                    if (extensionType == itemExtension)
                    {
                        args.IsExcluded = true;
                    }

                }

            }
        }
    }
}