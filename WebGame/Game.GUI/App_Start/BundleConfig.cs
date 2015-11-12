using System.Web;
using System.Web.Optimization;

namespace Game.GUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/uikit").Include(
                "~/Scripts/js/uikit.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                    "~/Scripts/js/main_page.js",
                    "~/Scripts/js/ProductShow.js",
                    "~/Scripts/js/AdminScripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/uikit.css",
                      "~/Content/Style.scss",
                      "~/Content/Style.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Styles").Include(
              "~/Styles/Map.css"));
        }
    }
}
