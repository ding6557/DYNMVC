
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using DYN.BLL;
using DYN.Model;
using DYN.Web.App_Start;


namespace DYN.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            InjectDependency();
             
        }

        /// <summary>
        /// 提供为MvcApplication注册IOC容器
        /// </summary>
        void InjectDependency()
        {
            Type baseType = typeof(IDependency);
    
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired().InstancePerLifetimeScope();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                .AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }


}
