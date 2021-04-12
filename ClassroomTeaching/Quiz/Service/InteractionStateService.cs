using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Generic.Behavior.Single.Query;
using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Service.Interface;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using LINDGE.Proxy;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Service
{
    public class InteractionStateService : IInteractionStateService
    {
        private readonly IClassroom _classroom = null;
        private readonly IBehaviorQuery _behaviorQuery = null;
        private readonly IBehaviorInfo _behaviorInfo = null;


        public InteractionStateService(
            IClassroom classroom,
            IProxy<IBehaviorQuery> behaviorQueryProxy,
            IProxy<IBehaviorInfo> behaviorInfoProxy)
        {
            _classroom = classroom;
            _behaviorQuery = behaviorQueryProxy.GetObject();
            _behaviorInfo = behaviorInfoProxy.GetObject();
        }

        /// <summary>
        /// 查询测验的状态
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        public QuizState GetQuizState(String classroomId)
        {
            var result = new QuizState();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            result.IsOnClass = classroomWorkInfo.IsOnClass;
            if (classroomWorkInfo.IsOnClass)
            {
                result.LessonId = classroomWorkInfo.ActiveLesson.LessonId;
                var queryResult = _behaviorQuery.ComplexQuery(new List<Query.Base.Param.QueryParameter>()
                {
                    new Query.Base.Param.QueryParameter()
                    {
                        Conditions = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                            {
                                new InSetOperator { Name = Generic.Behavior.Single.Query.QueryName.Action, Value = TaskType.quizTypes },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.ReceptionData, Value = classroomWorkInfo.ActiveLesson.LessonId },
                                new StateEqualOperator { StateName = Generic.Behavior.Single.Query.StateName.IsInvalid, StateValue = true}
                            }
                        }
                    }
                });
                if (queryResult.Any() && queryResult[0].Results.Any())
                {
                    result.IsInQuiz = true;
                    var behaviorInfo = _behaviorInfo.GetBehaviorInfo(new List<Generic.Behavior.Single.Param.BehaviorInfoGetParam>()
                    {
                        new Generic.Behavior.Single.Param.BehaviorInfoGetParam()
                        {
                            IsIncludeAction = true
                        }
                    })[0];
                    result.QuizInfo.QuizId = behaviorInfo.BehaviorId;
                    result.QuizInfo.Type = behaviorInfo.Action;
                }
            }

            return result;
        }
    }
}