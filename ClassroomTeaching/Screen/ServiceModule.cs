using Autofac;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType(typeof(BroadcastService))
                 .As<IBroadcastService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(ReceiveChannelService))
                .As<IReceiveChannelService>().InstancePerLifetimeScope();

            builder.RegisterTypes(
              typeof(ServiceModuleProxy<IBehaviorAttribute>))
          .AsImplementedInterfaces()
          .WithParameter("serviceModuleName", "Generic.Behavior.Single")
          .InstancePerLifetimeScope();
        }
    }
}