using Autofac;
using LINDGE.PARA.Generic.ClassroomTeaching.Brainstorming.Service.Interface;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType(typeof(GroupDiscussionService))
                 .As<IGroupDiscussionService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(GroupingService))
                 .As<IGroupingService>().InstancePerLifetimeScope();

     
        }
    }
}