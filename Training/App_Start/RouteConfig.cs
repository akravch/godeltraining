using System.Web.Mvc;
using System.Web.Routing;

namespace Training
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NewPost",
                url: "Posts/New",
                defaults: new { controller = "Posts", action = "New" }
            );

            routes.MapRoute(
                name: "Posts",
                url: "Posts/{category}",
                defaults: new { controller = "Posts", action = "Index", category = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Posts", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
