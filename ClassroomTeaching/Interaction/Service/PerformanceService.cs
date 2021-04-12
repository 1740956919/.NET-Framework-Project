using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.Const;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.Behavior.Single.Struct;
using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Param;
using LINDGE.PARA.Generic.Sociality.Group.API.Query.MemberRole;
using LINDGE.PARA.Generic.Sociality.Group.Param;
using LINDGE.PARA.Generic.TeachingSpace.Const;
using LINDGE.PARA.Generic.TeachingSpace.Struct;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Service
{
    /// <summary>
    /// 课堂表现管理服务
    /// </summary>
    public class PerformanceService : IPerformanceService
    {
        private readonly IClassroom _classroom = null;
        private readonly IBatchBehavior _batchBehavior = null;
        private readonly IBehaviorAttribute _behaviorAttribute = null;
        private readonly IBehaviorExecution _behaviorExecution = null;
        private readonly ILessonManagement _lessonManagement = null;
        private readonly IQueryUserMemberRole _queryUserMemberRole = null;
        private readonly IBehaviorCompletion _behaviorCompletion = null;
        private readonly ILessonPerformance _lessonPerformance = null;
        private readonly ITimeProvider _timeProvider = null;

        public PerformanceService(IClassroom classroom,
            ITimeProvider timeProvider,
            IProxy<IBatchBehavior> batchBehaviorProxy,
            IProxy<IBehaviorAttribute> behaviorAttributeProxy,
            IProxy<IBehaviorExecution> behaviorExecutionProxy,
            IProxy<ILessonManagement> lessonManagementProxy,
            IProxy<IQueryUserMemberRole> queryUserMemberRoleProxy,
            IProxy<IBehaviorCompletion> behaviorCompletionProxy,
            IProxy<ILessonPerformance> lessonPerformanceProxy)
        {
            _classroom = classroom;
            _timeProvider = timeProvider;
            _batchBehavior = batchBehaviorProxy.GetObject();
            _behaviorAttribute = behaviorAttributeProxy.GetObject();
            _behaviorExecution = behaviorExecutionProxy.GetObject();
            _lessonManagement = lessonManagementProxy.GetObject();
            _queryUserMemberRole = queryUserMemberRoleProxy.GetObject();
            _behaviorCompletion = behaviorCompletionProxy.GetObject();
            _lessonPerformance = lessonPerformanceProxy.GetObject();
        }

        /// <summary>
        /// 开始评价
        /// </summary>
        /// <param name="classroomId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public PerformanceScoreStartResult StartScore(String classroomId, ScoreStartParam parameter)
        {
            if (!parameter.IsVaild())
                throw new ArgumentException(nameof(parameter));

            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            if (classroomWorkInfo.IsOnClass)
            {
                // 创建行为
                var behaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
                {
                    new BehaviorCreateParam()
                    {
                        Action = BehaviorActionNames.SetPerformance,
                        Attributes = new Dictionary<String, String>()
                        {
                            { BehaviorAttributeNames.LessonId, classroomWorkInfo.ActiveLesson.LessonId },
                            { BehaviorAttributeNames.PerformanceCreate, parameter.ToJSONString() },
                        },
                        Exclusive = new BehaviorExclusive()
                        {
                            ActionList = new List<String>(){ BehaviorActionNames.SetPerformance },
                            ActionRange = ActionExclusiveRange.ActionList,
                            PrincipalRange = PrincipalExclusiveRange.Global
                        },
                        Principal = new BehaviorPrincipal()
                        {
                            Type = PrincipalType.MySelf
                        },
                        Reception = new BehaviorReception()
                        {
                            Data = classroomWorkInfo.ActiveLesson.LessonId,
                            Type = ReceptionType.Identify
                        }
                    }
                })[0];

                // 创建成功，则使用该行为
                // 创建失败，则将已占用行为的属性更新
                var behaviorId = String.Empty;
                if (behaviorCreateResult.Success)
                {
                    behaviorId = behaviorCreateResult.BehaviorId;
                    _behaviorExecution.Execute(new List<BehaviorExecuteParam>()
                    {
                        new BehaviorExecuteParam()
                        {
                            Action = BehaviorActionNames.SetPerformance,
                            BehaviorIds =  new List<String>(){ behaviorId }
                        }
                    });
                }
                else if (!String.IsNullOrWhiteSpace(behaviorCreateResult.OccupiedBehaviorId))
                {
                    behaviorId = behaviorCreateResult.OccupiedBehaviorId;
                    _behaviorAttribute.Update(new List<BehaviorAttributeUpdateParam>()
                    {
                        new BehaviorAttributeUpdateParam()
                        {
                            BehaviorIds = new List<String>(){ behaviorId },
                            UpdateParams = new Dictionary<String, String>()
                            {
                                { BehaviorAttributeNames.LessonId, classroomWorkInfo.ActiveLesson.LessonId },
                                { BehaviorAttributeNames.PerformanceCreate, parameter.ToJSONString() },
                            }
                        }
                    });
                }

                return new PerformanceScoreStartResult()
                {
                    BehaviorId = behaviorId
                };
            }
            else
            {
                throw new Exception("Class Not Begin");
            }
        }

        /// <summary>
        /// 完成评价
        /// </summary>
        /// <param name="scoreBehaviorId"></param>
        /// <param name="parameter"></param>
        public void CompleteScore(String scoreBehaviorId, ScoreCompleteParam parameter)
        {
            if (!parameter.IsValid())
                throw new ArgumentException("参数无效");

            var behaviorAttributeInfo = _behaviorAttribute.Get(new BehaviorAttributeGetParam()
            {
                BehaviorIds = new List<string>() { scoreBehaviorId },
                Keys = new List<string>() { Const.BehaviorAttributeNames.LessonId, Const.BehaviorAttributeNames.PerformanceCreate },
            })[0];
            var attribute = behaviorAttributeInfo.Attributes;
            var performanceSetCreateParam = new PerformanceSetCreateParam();
            performanceSetCreateParam.FromJSONString(attribute[Const.BehaviorAttributeNames.PerformanceCreate]);
            var targetIds = performanceSetCreateParam.TargetIds;
            var targetType = performanceSetCreateParam.TargetType;
            var lessonId = attribute[Const.BehaviorAttributeNames.LessonId];

            var memberIds = new List<string>();
            if (targetType == TargetType.Group)
            {
                var lessonInfo = _lessonManagement.GetLessonSummary(lessonId).Info;
                // 获取所有小组成员
                var queryResult = _queryUserMemberRole.Query(new QueryMemberRoleParameter()
                {
                    Condition = new AndOperator()
                    {
                        OperatorExpression = new List<IConditionParameter>()
                        {
                            new EqualOperator(){ Name = Generic.Sociality.Group.Const.QueryName.GroupId, Value = lessonInfo.RelativeGroupId },
                            new InSetOperator(){ Name = Generic.Sociality.Group.Const.QueryName.RoleSetName, Value = targetIds }
                        }
                    }
                });
                if (queryResult.TotalCount > 0)
                {
                    memberIds = queryResult.Results.Select(r => r.MemberId).ToList();
                }
            }
            else if (targetType == TargetType.SingleUser)
            {
                memberIds = targetIds;
            }
            else if (targetType == TargetType.ManyUser)
            {
                memberIds = targetIds;
            }

            var performanceSetInfo = new PerformanceSetInfo()
            {
                LessonId = lessonId,
                Performance = parameter.Evaluation,
                PerformanceCategory = parameter.Code,
                TargetIds = targetIds,
                TargetType = targetType
            };
            // 添加评价内容到行为结果
            _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
            {
                new BehaviorCompleteParam()
                {
                    BehaviorIds = new List<string>(){ scoreBehaviorId },
                    ResultCode = "SetPerformance",
                    ResultData = performanceSetInfo.ToJSONString(),
                    ResultType = ResultType.UserDefine
                }
            });
            Func<string, int> ConvertPerformanceValue = (performance) =>
            {
                var result = 0;
                if (performance == Performance.Praise)
                {
                    result = 1;
                }
                else if (performance == Performance.Trample)
                {
                    result = -1;
                }
                return result;
            };
            _lessonPerformance.AddPerformanceRecord(lessonId, memberIds.Select(m => new MemberPerformance()
            {
                MemberId = m,
                Performances = new List<PerformanceData>()
                {
                    new PerformanceData()
                    {
                        Category = (PerformanceCategory)Enum.Parse(typeof(PerformanceCategory), parameter.Code),
                        Description = string.Empty,
                        RegisterTime = _timeProvider.GetNow(),
                        Value = ConvertPerformanceValue(parameter.Evaluation)
                    }
                }
            }).ToList());
        }
    }
}