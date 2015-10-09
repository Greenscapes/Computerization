using System.Web;
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
              
                "~/Scripts/angular-google-maps.js",
                "~/Scripts/angular-google-maps-street-view.js",
                   "~/Scripts/loadash.js",
               
#else
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-resource.min.js",
                 "~/Scripts/angular-mocks.js",
                "~/Scripts/loadash.min.js",
                 "~/Scripts/angular-google-maps.min",
                 "~/Scripts/angular-google-maps-street-view.min.js",
#endif
 "~/Scripts/ui-bootstrap-tpls-0.13.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app.js",
                "~/Scripts/services/authenticationService.js",
                "~/Scripts/services/modalService.js",
                "~/Scripts/controllers/modalsController.js",
                "~/Scripts/controllers/navController.js",
                "~/Scripts/controllers/typesController.js",
                "~/Scripts/controllers/taskListTypeCreateController.js",
                "~/Scripts/controllers/taskListTypeDetailController.js",
                "~/Scripts/controllers/crewTypeCreateController.js",
                "~/Scripts/controllers/crewTypeDetailController.js",
                "~/Scripts/controllers/employeesController.js",
                "~/Scripts/controllers/employeeCreateController.js",
                "~/Scripts/controllers/employeeDetailController.js",
                "~/Scripts/controllers/crewsController.js",
                "~/Scripts/controllers/crewController.js",
                "~/Scripts/controllers/crewCreateController.js",
                "~/Scripts/controllers/crewDetailController.js",
                "~/Scripts/controllers/crewMemberCreateController.js",
                "~/Scripts/controllers/crewMemberDetailController.js",
                "~/Scripts/controllers/propertiesController.js",
                "~/Scripts/controllers/propertyCreateController.js",
                "~/Scripts/controllers/propertyDetailController.js",
                "~/Scripts/controllers/taskCreateController.js",
                "~/Scripts/controllers/taskDetailController.js",
                "~/Scripts/controllers/taskListCreateController.js",
                "~/Scripts/controllers/taskListDetailController.js",
                "~/Scripts/controllers/customerschedulesController.js",
                "~/Scripts/controllers/tasklistsController.js",
                 "~/Scripts/controllers/eventSchedulesController.js",
                 "~/Scripts/controllers/crewListsController.js",
                 "~/Scripts/controllers/customersRoutesController.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

          
        }
    }
}
