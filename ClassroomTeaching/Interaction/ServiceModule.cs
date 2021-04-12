using Autofac;
using LINDGE.PARA.Fundamental.Message.WebAPI.Basic;
using LINDGE.PARA.Fundamental.Runtime.CodeGenerate;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Basic;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Query;
using LINDGE.PARA.Generic.Behavior.Collaboration.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Generic.Engine.Job.WebAPI;
using LINDGE.PARA.Generic.Register.Census.API.Basic;
using LINDGE.PARA.Generic.Register.Census.API.Query;
using LINDGE.PARA.Generic.Sociality.Group.API.Get.RoleSet;
using LINDGE.PARA.Generic.Sociality.Group.API.Query.MemberRole;
using LINDGE.PARA.Generic.Sociality.User.API.Logon;
using LINDGE.PARA.Generic.Sociality.User.API.Profile;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.TeachingSpace;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Service;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType(typeof(ClassService))
                .As<IClassService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(MutualScoreService))
                .As<IMutualScoreService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(PerformanceService))
                .As<IPerformanceService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(EnvironmentService))
                .As<IEnvironmentService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(MaterialService))
                .As<IMaterialService>().InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IPasswordLogon>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Generic.Sociality.User")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IBatchJob>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Generic.Engine.Job")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IBehaviorResult>),
                typeof(ServiceModuleProxy<IBehaviorAttribute>),
                typeof(ServiceModuleProxy<IBehaviorCompletion>),
                typeof(ServiceModuleProxy<IBehaviorExecution>),
                typeof(ServiceModuleProxy<IBehaviorQuery>),
                typeof(ServiceModuleProxy<IBehaviorInfo>),
                typeof(ServiceModuleProxy<IBatchBehavior>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Generic.Behavior.Single")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IPictureSet>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Translayer.Static.Picture.Callback")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IBatchUserDevice>),
                typeof(ServiceModuleProxy<ILimitQueryProfileByProperties>),
                typeof(ServiceModuleProxy<IProfile>),
                typeof(ServiceModuleProxy<IQueryProfileById>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.User.Profile");

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IPasswordLogon>),
                typeof(ServiceModuleProxy<IDeviceLogon>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.User.Logon");

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IIndividualQuery>),
                typeof(ServiceModuleProxy<IIndividual>),
                typeof(ServiceModuleProxy<ICensusQuery>),
                typeof(ServiceModuleProxy<IEnrollment>),
                typeof(ServiceModuleProxy<IEnrolledIndividual>),
                typeof(ServiceModuleProxy<ICensus>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.Register.Census");

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<ILessonContent>),
                typeof(ServiceModuleProxy<ILessonInfo>),
                typeof(ServiceModuleProxy<ITeachingSpace>),
                typeof(ServiceModuleProxy<ILessonSync>),
                typeof(ServiceModuleProxy<ILessonManagement>),
                typeof(ServiceModuleProxy<ILessonActivity>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.Teaching.TeachingSpace");

            builder.RegisterTypes(typeof(ServiceModuleProxy<IMessage>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Fundamental.Message")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IGetRoleSetPath>),
                typeof(ServiceModuleProxy<IQueryUserMemberRole>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.Sociality.Group");

            builder.RegisterTypes(typeof(ServiceModuleProxy<IUniqueCode>))
                .AsImplementedInterfaces()
                .WithParameter("serviceModuleName", "Fundamental.Runtime.CodeGenerate")
                .InstancePerLifetimeScope();

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<ICollaborationBehavior>),
                typeof(ServiceModuleProxy<IBatchCollaboration>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.Behavior.Collaboration");

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<IResource>),
                typeof(ServiceModuleProxy<IResourceQuery>),
                typeof(ServiceModuleProxy<IResourceInfo>),
                typeof(ServiceModuleProxy<IResourceUsage>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.Bank.Resource");
        }
    }
}