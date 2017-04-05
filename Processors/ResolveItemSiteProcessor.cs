// Copyright (c) 2005-2017, Coveo Solutions Inc.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Coveo.Framework.CNL;
using Coveo.Framework.Items;
using Coveo.Framework.Log;
using Coveo.Framework.Processor;
using Coveo.Framework.Sites;
using Coveo.Framework.Utils;
using Coveo.SearchProvider.Pipelines;

namespace SitecoreHabitatDemo.Processors
{
    /// <summary>
    /// Processor that resolves the site which an item belongs to.
    /// </summary>
    public class ResolveItemSiteProcessor : IProcessor<CoveoResolveItemSiteArgs>
    {
        private static readonly ILogger s_Logger = CoveoLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Dictionary<string, string> s_SitesHomeItemPaths;

        /// <summary>
        /// Gets the <see cref="ISiteContextFactory"/> used to retrieve site configuration and context.
        /// </summary>
        protected ISiteContextFactory SiteContextFactory { get; private set; }

        /// <summary>
        /// Gets the list of sites that are excluded by default.
        /// </summary>
        public static IEnumerable<string> DefaultExcludedSites { get; private set; }

        /// <summary>
        /// Initializes the <see cref="ResolveItemSiteProcessor"/> type.
        /// </summary>
        static ResolveItemSiteProcessor()
        {
            DefaultExcludedSites = new[] {
                "coveo_website",
                "coveoanalytics",
                "coveorest",
                "shell",
                "login",
                "admin",
                "service",
                "modules_shell",
                "modules_website",
                "scheduler",
                "system",
                "publisher"
            };

            s_SitesHomeItemPaths = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="ResolveItemSiteProcessor"/>.
        /// </summary>
        public ResolveItemSiteProcessor()
            : this(new SiteContextFactoryWrapper())
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ResolveItemSiteProcessor"/>.
        /// </summary>
        /// <param name="p_SiteContextFactory">The <see cref="ISiteContextFactory"/> used to retrieve site configuration and context.</param>
        public ResolveItemSiteProcessor(ISiteContextFactory p_SiteContextFactory)
        {
            Precondition.NotNull(p_SiteContextFactory, () => () => p_SiteContextFactory);

            SiteContextFactory = p_SiteContextFactory;
        }

        /// <inheritDoc />
        public void Process(CoveoResolveItemSiteArgs p_Args)
        {
            Precondition.NotNull(p_Args, () => () => p_Args);
            Precondition.NotNull(p_Args.Item, () => () => p_Args.Item);

            p_Args.ResolvedSiteName = ResolveItemSite(p_Args.Item);
        }

        /// <summary>
        /// Resolves the site for the given item. The default Sitecore sites, except the "website" one, are excluded
        /// from the resolution process.
        /// </summary>
        /// <param name="p_Item">The item whose site to resolve.</param>
        /// <returns>The resolved site name, or <c>null</c> when no suitable site is found.</returns>
        protected virtual string ResolveItemSite(IItem p_Item)
        {
            s_Logger.TraceEntering();
            Precondition.NotNull(p_Item, () => () => p_Item);

            string resolvedSiteName = null;
            IEnumerable<string> excludedSiteNames = GetExcludedSiteNames();

            IEnumerable<ISiteInfo> sites = SiteContextFactory.GetSites().Where(site => !excludedSiteNames.Contains(site.Name, StringComparer.OrdinalIgnoreCase));
            foreach (ISiteInfo site in sites) {
                if (IsItemInSite(site, p_Item)) {
                    // We've found a suitable site for the item, return its name.
                    resolvedSiteName = site.Name;
                    break;
                }
            }

            s_Logger.TraceExiting();
            return resolvedSiteName;
        }

        /// <summary>
        /// Gets the list of sites that are excluded from the resolution process.
        /// </summary>
        /// <returns>The list of site names to exclude from the site resolution process.</returns>
        protected virtual IEnumerable<string> GetExcludedSiteNames()
        {
            return DefaultExcludedSites;
        }

        /// <summary>
        /// Gets whether the given item belongs to a site.
        /// </summary>
        /// <param name="p_SiteInfo">The <see cref="ISiteInfo"/> that represents the site configuration.</param>
        /// <param name="p_Item">The <see cref="IItem"/> whose site to resolve.</param>
        /// <returns><c>true</c> when the item belongs to the site, otherwise <c>false</c>.</returns>
        protected virtual bool IsItemInSite(ISiteInfo p_SiteInfo,
                                            IItem p_Item)
        {
            s_Logger.TraceEntering();
            Precondition.NotNull(p_SiteInfo, () => () => p_SiteInfo);
            Precondition.NotNull(p_Item, () => () => p_Item);

            bool isItemInSite = false;

            if (p_SiteInfo.IsActive &&
                !String.IsNullOrEmpty(p_SiteInfo.RootPath)) {

                string homeItemPath;
                if (!s_SitesHomeItemPaths.TryGetValue(p_SiteInfo.Name, out homeItemPath)) {
                    homeItemPath = p_SiteInfo.RootPath.EnsureEndsWithSlash().RemoveLastCharacter();
                    s_SitesHomeItemPaths[p_SiteInfo.Name] = homeItemPath;
                }

                if (p_Item.Paths.FullPath.StartsWithIgnoreCase(homeItemPath)) {
                    isItemInSite = true;
                }
            }

            s_Logger.TraceExiting();
            return isItemInSite;
        }
    }
}
