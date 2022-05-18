using System.Web.Mvc;
using System.Web.Routing;

namespace Insta
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            var container = new Container();

            container.Register<Service>();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalFilters.Filters.Add(new AuthorizationFilter());
        }
    }
}
