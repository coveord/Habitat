namespace Sitecore.Foundation.CoveoIndexing.InboundFilters
{
    using System;
    using System.Linq;
    using Coveo.SearchProvider.InboundFilters;
    using Coveo.SearchProvider.Pipelines;

    public class ExcludeFilesWithExtensionTypes : AbstractCoveoInboundFilterProcessor
    {
        private const char FILE_EXTENSION_SEPARATOR = ';';

        /// <summary>
        /// Semicolon separated list of file extensions to exclude. Ex.: png;jpg;gif
        /// </summary>
        public string ExtensionTypesToExclude { get; set; }

        public override void Process(CoveoInboundFilterPipelineArgs args)
        {
            if (args.IndexableToIndex != null && !args.IsExcluded && this.ShouldExecute(args)) {
                string[] extensionTypesToExclude = this.ExtensionTypesToExclude.Split(FILE_EXTENSION_SEPARATOR);
                string itemExtension = args.IndexableToIndex.Item.GetFieldValue(Constants.ItemExtensionFieldName);

                if (extensionTypesToExclude.Any(toExclude => StringComparer.OrdinalIgnoreCase.Equals(toExclude, itemExtension))) {
                    args.IsExcluded = true;
                }
            }
        }
    }
}