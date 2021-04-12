using Autofac;
using LINDGE.PARA.Generic.Sociality.User.API.Logon;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType(typeof(AuthorizeService))
                 .As<IAuthorizeService>().InstancePerLifetimeScope();

            builder.RegisterType(typeof(ConfigService))
                 .As<IConfigService>().InstancePerLifetimeScope();

            builder.RegisterTypes(
              typeof(ServiceModuleProxy<IPasswordLogon>))
              .AsImplementedInterfaces().InstancePerDependency()
              .WithParameter("serviceModuleName", "Generic.User.Logon");
        }
    }
}