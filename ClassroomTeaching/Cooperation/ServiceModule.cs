using Autofac;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType(typeof(CooperationService))
                .As<ICooperationService>().InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IBehaviorCompletion>),
                typeof(ServiceModuleProxy<IBehaviorExecution>),
                typeof(ServiceModuleProxy<IBehaviorQuery>),
                typeof(ServiceModuleProxy<IBatchBehavior>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Generic.Behavior.Single")
                .InstancePerLifetimeScope();
        }
    }
}