using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using CRG.ES;
using CRG.ES.CommandProcessor;
using CRG.RavenConfig;
using CRG.ES.ClientAPI;
using MM.ES;

using System.Data.SqlClient;
using Raven.Client;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using Employee = Employee2.Domain.Employee;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Employee2.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public new IDocumentSession Session { get; set; }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var autofacConfig = new ConfigurationBuilder();
            autofacConfig.AddJsonFile("autofac.json");
            var module = new ConfigurationModule(autofacConfig.Build());
            builder.RegisterModule(module);

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule(new AutofacWebTypesModule());

            builder.RegisterModule(new ConfigCommandProcessing<Employee>("Employee2.Web",
                new[] { typeof(Employee).Assembly },
                 new[] { typeof(CommandHandler.EmployeeHandler).Assembly }));

            var ravenConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString;
            builder.RegisterModule(new RavenDbConfig(ravenConnectionString));

            builder.RegisterTypes(typeof(Employee2.ReadModels.Handler.EmployeeHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));

            builder.RegisterType<Employee2.ReadModels.RavenConfig.SetUpIndex>().As<IRequireStartup>();

            var container = builder.Build();
            var store = container.Resolve<IDocumentStore>();
            new Employee2.ReadModels.RavenConfig.EmployeeEvent_SetupIndex().Execute(store);

            container.Resolve<IEnumerable<IRequireStartup>>().ToList().ForEach(x => x.Start());


            var httpRequestResolver = new AutofacDependencyResolver(container);

            DependencyResolver.SetResolver(httpRequestResolver);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
