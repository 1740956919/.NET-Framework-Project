using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Autofac;
using LINDGE.PARA.Fundamental.Message.WebAPI.Basic;
using LINDGE.PARA.Generic.Behavior.Collaboration.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Generic.Register.Census.API.Basic;
using LINDGE.PARA.Generic.Register.Census.API.Query;
using LINDGE.PARA.Generic.Sociality.Group.API.Get.RoleSet;
using LINDGE.PARA.Generic.Sociality.Group.API.Query.MemberRole;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.TeachingSpace;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Service;


namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType(typeof(InteractionStateService))
                 .As<IInteractionStateService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(GrabAnswerService))
                .As<IGrabAnswerService>().InstancePerLifetimeScope();     
            builder.RegisterType(typeof(PracticeService))
                .As<IPracticeService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(RandomCandidateService))
               .As<IRandomCandidateService>().InstancePerLifetimeScope();



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

            builder.RegisterTypes(
                typeof(ServiceModuleProxy<ICollaborationBehavior>),
                typeof(ServiceModuleProxy<IBatchCollaboration>))
                .AsImplementedInterfaces().InstancePerDependency()
                .WithParameter("serviceModuleName", "Generic.Behavior.Collaboration");
        }
    }
}