using Autofac;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Service;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterTypes(
               typeof(ServiceModuleProxy<IBehaviorQuery>),
               typeof(ServiceModuleProxy<IBehaviorInfo>),
               typeof(ServiceModuleProxy<IBehaviorExecution>),
               typeof(ServiceModuleProxy<IBatchBehavior>),
               typeof(ServiceModuleProxy<IBehaviorAttribute>))
               .AsImplementedInterfaces()
               .WithParameter("serviceModuleName", "Generic.Behavior.Single")
               .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IPictureUpload>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Translayer.Static.Picture.Callback")
                .InstancePerLifetimeScope();
            
            builder.RegisterType(typeof(LessonReportService))
               .As<ILessonReportService>().InstancePerLifetimeScope();
        }
    }
}