using Autofac;
using System.Configuration;
using CRG.RavenConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Employee2.ReadModels.Handler;
using MM.ES;

namespace Employee2.ReadModel.Rebuild
{
    public class RegisterAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RavenDbConfig(ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString));

            // Register all handlers within the following assembly

            builder.RegisterTypes(typeof(EmployeeHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
           
        }
    }
}
