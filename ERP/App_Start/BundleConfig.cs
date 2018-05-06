using System.Web;
using System.Web.Optimization;

namespace ERP
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/jqueryui/jquery-ui.min.js", "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js", "~/Scripts/app.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                                                      "~/Content/bootstrap.css",
                                      "~/Content/bootstrap-rtl.min.css",
                     "~/Content/dt.css"
                      ));
        }
    }
}
