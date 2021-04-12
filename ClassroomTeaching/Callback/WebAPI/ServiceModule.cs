using Autofac;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Basic;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Query;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Service;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType(typeof(ImportLessonService))
                .As<IImportLessonService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(ExportLessonReportService))
              .As<IExportLessonReportService>().InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IPictureSet>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Translayer.Static.Picture.Callback")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IPictureSet>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Translayer.Static.Picture.Callback");

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IResourceUsage>),
                typeof(ServiceModuleProxy<IResourceQuery>),
                typeof(ServiceModuleProxy<IResource>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Generic.Bank.Resource")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<ILessonSync>),
                typeof(ServiceModuleProxy<IBatchLesson>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.Teaching.TeachingSpace");
        }
    }
}