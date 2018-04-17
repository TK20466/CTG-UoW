using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace ConnSquad
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/ctg.css"));

            bundles.Add(new ScriptBundle("~/Bundles/dependencies").Include(
                    "~/Scripts/jquery-3.1.1.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/moment.js",
                    "~/Scripts/bootstrap-datetimepicker.js",
                    "~/Scripts/knockout-3.4.2.js",
                    "~/Scripts/ko-datebind.js",
                    "~/Scripts/knockout.validation.js"
                ));
            bundles.Add(new ScriptBundle("~/Bundles/app").Include(
                    "~/Scripts/app/*.js"
                ));
        }

        public static void _RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/ctg.css"));

            bundles.Add(new AngularTemplatesBundle("sharedServices", "~/bundles/templates/partialHTML")
                .IncludeDirectory("~/App/", "*.html", true));
            
            var appScripts = ScriptBundleFactory.Create("~/bundles/appdependencies");

            appScripts.Include("~/App/modules.js");

            appScripts.IncludeDirectory("~/App/Shared", "*.js", true);
            appScripts.IncludeDirectory("~/App/Events", "*.js", true);
            appScripts.IncludeDirectory("~/App/Homepage", "*.js", true);
            appScripts.IncludeDirectory("~/App/Navbar", "*.js", true);

            bundles.Add(appScripts);

            var appMain = ScriptBundleFactory.Create("~/bundles/app").Include("~/App/app.js", "~/App/routing.js");

            bundles.Add(appMain);


            // Enable optimisation based on web.config setting
            BundleTable.EnableOptimizations =
                bool.Parse(ConfigurationManager.AppSettings["BundleOptimisation"]);
        }

    }
    public class PartialsTransform : IBundleTransform
    {
        private readonly string _moduleName;
        public PartialsTransform(string moduleName)
        {
            _moduleName = moduleName;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var strBundleResponse = new StringBuilder();
            // Javascript module for Angular that uses templateCache 
            strBundleResponse.AppendFormat(
                @"angular.module('{0}').run(['$templateCache',function(t){{",
                _moduleName);

            foreach (var file in response.Files)
            {
                // Get content of file
                var content = file.ApplyTransforms();
                // Remove newlines and replace ' with \\'
                content = content.Replace("'", "\\'").Replace("\r\n", "");
                // Find templateUrl by getting file path and removing inital ~
                var templateUrl = file.IncludedVirtualPath.Replace("~", "").Replace("\\", "/").ToLower();
                if (templateUrl[0] == '/')
                    templateUrl = templateUrl.Substring(1);
                // Add content of template file inside an Angular put method
                strBundleResponse.AppendFormat("t.put('{0}','{1}');", templateUrl, content);
            }
            strBundleResponse.Append(@"}]);");

            response.Files = new BundleFile[] { };
            response.Content = strBundleResponse.ToString();
            response.ContentType = "text/javascript";
        }
    }

    public class AngularTemplatesBundle : Bundle
    {
        public AngularTemplatesBundle(string moduleName, string virtualPath)
            : base(virtualPath, new PartialsTransform(moduleName))
        {
        }
    }

    public static class ScriptBundleFactory
    {
        public static Bundle Create(string virtualPath)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["MinifyBundles"]))
                return new ScriptBundle(virtualPath);
            return new Bundle(virtualPath);
        }   
    }
}
