using System.Web.Http;

namespace CMS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "TaskLists",
            //    routeTemplate: "api/properties/{propertyId}/tasklists",
            //    defaults: new { controller = "PropertyTaskLists"});

            //config.Routes.MapHttpRoute(
            //    name: "TaskListTasks",
            //    routeTemplate: "api/tasklists/{taskListId}/tasks",
            //    defaults: new { controller = "PropertyTasks" });

            //config.Routes.MapHttpRoute(
            //    name: "TaskListTasks",
            //    routeTemplate: "api/tasklists",
            //    defaults: new { controller = "PropertyTasks" });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
