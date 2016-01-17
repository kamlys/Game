using System.Web;
using System.Web.Optimization;

namespace Game.GUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                "~/Scripts/materialize.js",
                "~/Scripts/materialize.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                    "~/Scripts/js/main_page.js",
                    "~/Scripts/js/ProductShow.js",
                    "~/Scirpts/js/circle-progress.js",
                    "~/Scripts/js/AdminScripts.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .IncludeDirectory("~/Content/", "*.css", true));

        }
    }
}
