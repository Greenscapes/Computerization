﻿using System.Web;
using System.Web.Optimization;

namespace CMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
#if DEBUG
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-mocks.js",
                "~/Scripts/angular-filter.min.js",
                "~/Scripts/angular-google-maps.js",
                "~/Scripts/angular-google-maps-street-view.js",
                "~/Scripts/loadash.js",
                "~/Scripts/moment.js",
                "~/Scripts/dateFormat.js",
                "~/scripts/toastr.min.js",

#else
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-resource.min.js",
                 "~/Scripts/angular-mocks.js",
                "~/Scripts/loadash.min.js",
                 "~/Scripts/angular-google-maps.min",
                 "~/Scripts/angular-google-maps-street-view.min.js",
                    "~/Scripts/moment.min.js",
                   "~/Scripts/angular-filter.min.js",
                "~/Scripts/dateFormat.js",
                "~/scripts/toastr.min.js",
#endif
 "~/Scripts/ui-bootstrap-tpls-0.13.0.min.js"

 ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js", true)
                .IncludeDirectory("~/Scripts/services", "*.js", true)
                .IncludeDirectory("~/Scripts/directives", "*.js", true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                 "~/Content/customizedbs.css",
                 "~/Content/kendoui/kendo.common.min.css",
               
                 "~/Content/kendoui/kendo.default.min.css"
                
                 ));
               
        }
    }
}
