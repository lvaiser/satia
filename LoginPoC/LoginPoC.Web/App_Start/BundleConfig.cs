using System.Web.Optimization;

namespace LoginPoC.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/angular")
                   .Include("~/Scripts/angular.min.js")
                   .Include("~/Scripts/lib/angular-bootstrap/ui-bootstrap-tpls.min.js")
                   .Include("~/Scripts/lib/notifyjs/dist/notify.js")
                   .Include("~/Scripts/lib/angular-block-ui/angular-block-ui.min.js")
                   .Include("~/Scripts/lib/moment/min/moment.min.js")
                   .Include("~/Scripts/lib/bootstrap-daterangepicker/daterangepicker.js")
                   .Include("~/Scripts/lib/angular-daterangepicker/angular-daterangepicker.min.js")
                   .Include("~/Scripts/Site/Angular-Extras/SATIA.Namespaces.js")
                   .IncludeDirectory("~/Scripts/Site/Angular-Extras/Directives", "*.js", true)
                   .IncludeDirectory("~/Scripts/Site/Angular-Extras/Services", "*.js", true)
                   .Include("~/Scripts/Site/Angular-Extras/SATIA.App.js")); // This one should always be the last one

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/site.css",
                      "~/Content/satiaStyle.css",
                      "~/Scripts/lib/angular-block-ui/angular-block-ui.min.css",
                      "~/Scripts/lib/bootstrap-daterangepicker/daterangepicker.css"));
        }
    }
}
