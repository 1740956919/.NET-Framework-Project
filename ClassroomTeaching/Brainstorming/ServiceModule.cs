using Autofac;
using LINDGE.PARA.Fundamental.Message.WebAPI.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType(typeof(IdeaService))
                 .As<IIdeaService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(VoteService))
                 .As<IVoteService>().InstancePerLifetimeScope();

            builder.RegisterTypes(
               typeof(ServiceModuleProxy<IBatchBehavior>),
               typeof(ServiceModuleProxy<IBehaviorAttribute>),
               typeof(ServiceModuleProxy<IBehaviorCompletion>),
               typeof(ServiceModuleProxy<IBehaviorExecution>),
               typeof(ServiceModuleProxy<IBehaviorQuery>),
               typeof(ServiceModuleProxy<IBehaviorInfo>)
             )
               .AsImplementedInterfaces()
               .WithParameter("serviceModuleName", "Generic.Behavior.Single")
               .InstancePerLifetimeScope();

            builder.RegisterTypes(typeof(ServiceModuleProxy<IMessage>))
              .AsImplementedInterfaces()
              .WithParameter("serviceModuleName", "Fundamental.Message")
              .InstancePerLifetimeScope();
        }
    }
}