using Autofac;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Service;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType(typeof(InteractService))
                .As<IInteractService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(LessonService))
                .As<ILessonService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(ScanService))
                .As<IScanService>().InstancePerLifetimeScope();

            builder.RegisterTypes(
            typeof(ServiceModuleProxy<IPictureSet>))
            .AsImplementedInterfaces()
            .WithParameter("serviceModuleName", "Translayer.Static.Picture.Callback")
            .InstancePerLifetimeScope();
        }
    }
}