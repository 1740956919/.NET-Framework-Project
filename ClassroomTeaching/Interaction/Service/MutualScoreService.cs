using LINDGE.PARA.Fundamental.Message.Const;
using LINDGE.PARA.Fundamental.Message.Struct;
using LINDGE.PARA.Fundamental.Message.WebAPI.Basic;
using LINDGE.PARA.Generic.Behavior.Collaboration.API.Basic;
using LINDGE.PARA.Generic.Behavior.Collaboration.Param;
using LINDGE.PARA.Generic.Behavior.Collaboration.Struct;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Generic.Behavior.Single.Const;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.Behavior.Single.Struct;
using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Service.Interface;
using LINDGE.PARA.Generic.Register.Census.API.Basic;
using LINDGE.PARA.Generic.Register.Census.API.Query;
using LINDGE.PARA.Generic.Register.Census.Param;
using LINDGE.PARA.Generic.Sociality.Group.API.Get.RoleSet;
using LINDGE.PARA.Generic.Sociality.Group.API.Query.MemberRole;
using LINDGE.PARA.Generic.Sociality.Group.Param;
using LINDGE.PARA.Generic.TeachingSpace.Const;
using LINDGE.PARA.Generic.TeachingSpace.Struct;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Service
{
    /// <summary>
    /// 互评管理服务
    /// </summary>
    public class MutualScoreService : IMutualScoreService
    {
        private readonly IClassroom _classroom = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly IBehaviorQuery _behaviorQuery = null;
        private readonly IBehaviorCompletion _behaviorCompletion = null;
        private readonly IBatchBehavior _batchBehavior = null;
        private readonly IBehaviorExecution _behaviorExecution = null;
        private readonly IBehaviorInfo _behaviorInfo = null;
        private readonly IMessage _message = null;
        private readonly ICensusQuery _censusQuery = null;
        private readonly IEnrolledIndividual _enrolledIndividual = null;
        private readonly ILessonInfo _lessonInfo = null;
        private readonly IGetRoleSetPath _getRoleSetPath = null;
        private readonly IBatchCollaboration _batchCollaboration = null;
        private readonly ICollaborationCompletion _collaborationCompletion = null;
        private readonly ICollaborationBehavior _collaborationBehavior = null;
        private readonly IBehaviorResult _behaviorResult = null;
        private readonly ICollaborationParticipation _collaborationParticipation = null;
        private readonly ICollaborationInfo _collaborationInfo = null;
        private readonly ILessonManagement _lessonManagement = null;
        private readonly IQueryUserMemberRole _queryUserMemberRole = null;
        private readonly ILessonPerformance _lessonPerformance = null;

        public MutualScoreService(
            IClassroom classroomService,
            ITimeProvider timeProvider,
            IProxy<IBehaviorQuery> behaviorQuery,
            IProxy<IBehaviorCompletion> behaviorCompletion,
            IProxy<IBatchBehavior> batchBehavior,
            IProxy<IBehaviorExecution> behaviorExecution,
            IProxy<IBehaviorInfo> behaviorInfoProxy,
            IProxy<IMessage> messageProxy,
            IProxy<ICensusQuery> censusQueryProxy,
            IProxy<IEnrolledIndividual> enrolledIndividualProxy,
            IProxy<ILessonInfo> lessonInfoProxy,
            IProxy<IGetRoleSetPath> getRoleSetPathProxy,
            IProxy<IBatchCollaboration> batchCollaborationProxy,
            IProxy<ICollaborationCompletion> collaborationCompletionProxy,
            IProxy<ICollaborationBehavior> collaborationBehaviorProxy,
            IProxy<IBehaviorResult> behaviorResultProxy,
            IProxy<ICollaborationParticipation> collaborationParticipationProxy,
            IProxy<ICollaborationInfo> collaborationInfoProxy,
            IProxy<ILessonManagement> lessonManagementProxy,
            IProxy<IQueryUserMemberRole> queryUserMemberRoleProxy,
            IProxy<ILessonPerformance> lessonPerformanceProxy)
        {
            _classroom = classroomService;
            _timeProvider = timeProvider;
            _behaviorQuery = behaviorQuery.GetObject();
            _behaviorCompletion = behaviorCompletion.GetObject();
            _batchBehavior = batchBehavior.GetObject();
            _behaviorExecution = behaviorExecution.GetObject();
            _behaviorInfo = behaviorInfoProxy.GetObject();
            _message = messageProxy.GetObject();
            _censusQuery = censusQueryProxy.GetObject();
            _enrolledIndividual = enrolledIndividualProxy.GetObject();
            _lessonInfo = lessonInfoProxy.GetObject();
            _getRoleSetPath = getRoleSetPathProxy.GetObject();
            _batchCollaboration = batchCollaborationProxy.GetObject();
            _collaborationCompletion = collaborationCompletionProxy.GetObject();
            _collaborationBehavior = collaborationBehaviorProxy.GetObject();
            _behaviorResult = behaviorResultProxy.GetObject();
            _collaborationParticipation = collaborationParticipationProxy.GetObject();
            _collaborationInfo = collaborationInfoProxy.GetObject();
            _lessonManagement = lessonManagementProxy.GetObject();
            _queryUserMemberRole = queryUserMemberRoleProxy.GetObject();
            _lessonPerformance = lessonPerformanceProxy.GetObject();
        }

        /// <summary>
        /// 获取互评信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public StudentMutualScoreInfo GetMutualScoreInfo(String classroomId)
        {
            var result = new StudentMutualScoreInfo();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            if (classroomWorkInfo.IsOnClass)
            {
                var behaviorQueryResults = _behaviorQuery.ComplexQuery(new List<QueryParameter>()
                {
                    new QueryParameter()
                    {
                        Conditions = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                            {
                                new EqualOperator{ Name = Generic.Behavior.Single.Query.QueryName.Action, Value = BehaviorActionNames.MutualScore },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.ReceptionData, Value = classroomWorkInfo.ActiveLesson.LessonId },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.IsComplete, Value = "FALSE" }
                            }
                        }
                    }
                });
                if (behaviorQueryResults.Any() && behaviorQueryResults[0].TotalCount > 0)
                {
                    var behaviorId = behaviorQueryResults[0].Results[0];
                    result.IsMutualScoring = true;
                    result.ScoreSessonId = behaviorId;

                    var behaviorInfos = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
                    {
                        new BehaviorInfoGetParam()
                        {
                            IsIncludeAttribute = true,
                            IsIncludeTime = true,
                            BehaviorIds = new List<String>(){ behaviorId }
                        }
                    });
                    if (behaviorInfos.Any())
                    {
                        var nowTime = _timeProvider.GetNow();
                        result.MutualScoreInfo = new MutualScoreTargetInfo()
                        {
                            DuringSeconds = Convert.ToInt32(nowTime.Subtract(behaviorInfos[0].Time.CreateTime).TotalSeconds),
                            RelationId = behaviorInfos[0].Attributes.ContainsKey(BehaviorAttributeNames.RelationId) ? behaviorInfos[0].Attributes[BehaviorAttributeNames.RelationId] : String.Empty,
                            RelationType = behaviorInfos[0].Attributes[BehaviorAttributeNames.RelationType]
                        };
                        result.MutualScoreInfo.ScoredTarget.FromJSONString(behaviorInfos[0].Attributes[BehaviorAttributeNames.ScoredTarget]);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取互评进度
        /// </summary>
        /// <param name="scoreSessonId"></param>
        /// <returns></returns>
        public MutualScoreProgress GetProgress(String scoreSessonId)
        {
            var result = new MutualScoreProgress();
            var collaborationIdUserBehaviorInfoMap = _collaborationBehavior.Get(new List<CollaborationBehaviorGetParam>()
            {
                new CollaborationBehaviorGetParam()
                {
                    BehaviorIds = new List<String>(){ scoreSessonId },
                    IsAllUser = true
                }
            });
            // 获取协作行为下全部的从行为
            var userSlaveBehaviorMap = new Dictionary<String, String>();
            if (collaborationIdUserBehaviorInfoMap.ContainsKey(scoreSessonId))
            {
                userSlaveBehaviorMap = collaborationIdUserBehaviorInfoMap[scoreSessonId].UserBehaviors
                    .Where(c => !c.IsMasterBehavior).ToDictionary(c => c.UserId, c => c.BehaviorId);
            }
            var slaveBehaviorIds = userSlaveBehaviorMap.Values.ToList();
            // 获取已递交的从行为信息和主行为属性
            var completedBehaviorIds = new List<String>();
            var behaviorInfoGetParams = new List<BehaviorInfoGetParam>()
            {
                new BehaviorInfoGetParam()
                {
                    BehaviorIds = new List<String>(){ scoreSessonId },
                    IsIncludeAttribute = true
                }
            };
            if (slaveBehaviorIds.Any())
            {
                behaviorInfoGetParams.Add(new BehaviorInfoGetParam()
                {
                    BehaviorIds = slaveBehaviorIds,
                    IsIncludeState = true
                });
            }

            var behaviorInfos = _behaviorInfo.GetBehaviorInfo(behaviorInfoGetParams);
            completedBehaviorIds = behaviorInfos.Where(b => b.State.HasFlag(BehaviorState.IsCompleted) && b.BehaviorId != scoreSessonId).Select(b => b.BehaviorId).ToList();
            var masterBehaviorAttributes = behaviorInfos.Where(b => b.BehaviorId == scoreSessonId).FirstOrDefault()?.Attributes;
            var behaviorIdDataMap = new Dictionary<String, String>();
            if (completedBehaviorIds.Count > 0)
            {
                var behaviorResult = _behaviorResult.Get(completedBehaviorIds);
                behaviorIdDataMap = behaviorResult.ToDictionary(b => b.BehaviorId, b => b.Result.ResultData);
            }
            var lessonId = masterBehaviorAttributes.ContainsKey(BehaviorAttributeNames.LessonId) ? masterBehaviorAttributes[BehaviorAttributeNames.LessonId] : String.Empty;
            var censusId = this.GetCensusByLessonId(lessonId);
            var enrolledIndividualGetParams = new List<EnrolledIndividualGetParam>()
            {
                new EnrolledIndividualGetParam()
                {
                    CensusIds = new List<String>(){ censusId },
                    FieldNames = new List<String>(){ StudentFieldNames.LocalMemberId }
                }
            };
            if (userSlaveBehaviorMap.Keys.Any())
            {
                enrolledIndividualGetParams.Add(new EnrolledIndividualGetParam()
                {
                    CensusIds = new List<String>() { censusId },
                    UserIds = userSlaveBehaviorMap.Keys.ToList(),
                    FieldNames = new List<String>() { StudentFieldNames.LocalMemberId }
                });
            }
            var enrolledIndividualGetResults = _enrolledIndividual.Get(enrolledIndividualGetParams);
            result.TotalCount = enrolledIndividualGetResults[0].UserIndividualInfoMap.Keys.Count();
            // 获取用户标识对应的成员标识
            var userIdMemberIdMap = new Dictionary<String, String>();
            if (userSlaveBehaviorMap.Count > 0)
            {
                var userIds = userSlaveBehaviorMap.Keys.ToList();
                foreach (var userIndividualInfo in enrolledIndividualGetResults[1].UserIndividualInfoMap)
                {
                    var userId = userIndividualInfo.Key;
                    var memberId = String.Empty;
                    if (userIndividualInfo.Value.Count > 0 && userIndividualInfo.Value[0].IndividualDataset.ContainsKey(StudentFieldNames.LocalMemberId))
                    {
                        memberId = userIndividualInfo.Value[0].IndividualDataset[StudentFieldNames.LocalMemberId];
                    }
                    userIdMemberIdMap.Add(userId, memberId);
                }
                var memberScoreInfos = userSlaveBehaviorMap.Select(u => new MemberScoreInfo()
                {
                    MemberId = userIdMemberIdMap.ContainsKey(u.Key) ? userIdMemberIdMap[u.Key] : String.Empty,
                    IsSubmited = behaviorIdDataMap.ContainsKey(u.Value),
                    Score = behaviorIdDataMap.ContainsKey(u.Value) ? float.Parse(behaviorIdDataMap[u.Value]) : 0f
                }).ToList();

                result.ScoredCount = memberScoreInfos.Count(m => m.IsSubmited);
                result.AverageScore = memberScoreInfos.Count(m => m.IsSubmited) > 0 ? memberScoreInfos.Where(m => m.IsSubmited).Sum(m => m.Score) / result.ScoredCount : 0;
            }

            return result;
        }

        /// <summary>
        /// 开始互评
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public MutualScoreStartResult Start(MutualScoreStartParam parameter)
        {
            if (!parameter.IsValid())
            {
                throw new ArgumentException("parameter is not valid");
            }
            var result = new MutualScoreStartResult();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = parameter.ClassroomId
            });
            if (classroomWorkInfo.IsOnClass)
            {
                var lessonInfos = _lessonInfo.GetLessonInfo(new Generic.TeachingSpace.Param.LessonInfoGetParam()
                {
                    SpaceId = parameter.ClassroomId,
                    LessonIds = new List<String>() { classroomWorkInfo.ActiveLesson.LessonId }
                });
                if (lessonInfos.Any())
                {
                    var interactionActions = BehaviorActionNames.InteractiveAction;
                    var attributes = new Dictionary<String, String>()
                    {
                        { BehaviorAttributeNames.ScoredTarget, parameter.ScoredTarget.ToJSONString() },
                        { BehaviorAttributeNames.RelationType, parameter.RelationType },
                        { BehaviorAttributeNames.LessonId, classroomWorkInfo.ActiveLesson.LessonId }
                    };
                    if (!String.IsNullOrWhiteSpace(parameter.RelationId))
                    {
                        attributes.Add(BehaviorAttributeNames.RelationId, parameter.RelationId);
                    }
                    // 创建主行为
                    var masterBehaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
                    {
                        new BehaviorCreateParam()
                        {
                            Action = BehaviorActionNames.MutualScore,
                            Attributes = attributes,
                            Exclusive = new BehaviorExclusive()
                            {
                                ActionList = interactionActions,
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
                    if (masterBehaviorCreateResult.Success)
                    {
                        result.ScoreSessonId = masterBehaviorCreateResult.BehaviorId;
                        var groupId = lessonInfos[0].RelativeGroupId;
                        var getRoleSetPathResult = _getRoleSetPath.Get(new List<GetRoleSetParameter>()
                        {
                            new GetRoleSetParameter()
                            {
                                GroupId = groupId,
                                RoleSetName = GroupRoleSetNames.Teaching
                            }
                        })[0];

                        // 创建协作行为
                        var collaborationCreateParam = new CollaborationCreateParam()
                        {
                            AcceptableParticipants = new List<BehaviorPrincipal>()
                            {
                                new BehaviorPrincipal()
                                {
                                    Data = getRoleSetPathResult.RankPath[GroupRanks.Student],
                                    Type = PrincipalType.ManyUser
                                }
                            },
                            Attributes = attributes,
                            BehaviorIds = new List<String>() { masterBehaviorCreateResult.BehaviorId },
                            SlaveCreateData = new CollaborationSlaveCreateData()
                            {
                                DataCode = BehaviorActionNames.MutualScore,
                                ReuseExistDataCode = false,
                                SlaveBehaviorCreateData = new BehaviorCreateData()
                                {
                                    Action = BehaviorActionNames.MutualScore,
                                    Reception = null
                                }
                            },
                            UserData = new CollaborationUserData()
                            {
                                GroupTag = BehaviorActionNames.MutualScore,
                                ProjectId = classroomWorkInfo.ActiveLesson.LessonId,
                            }
                        };
                        _batchCollaboration.Create(new List<CollaborationCreateParam>()
                        {
                            collaborationCreateParam
                        });
                        // 发送通知
                        _message.SendMessage(new MessageData()
                        {
                            Content = MessageCodes.Interact,
                            IsRetained = true,
                            Quality = QualityOfServiceType.AtLeastOnce,
                            Topic = MessageCodes.All
                        });
                    }
                    else if (!String.IsNullOrWhiteSpace(masterBehaviorCreateResult.OccupiedBehaviorId))
                    {
                        result.IsSuccess = false;
                        result.ErrorCode = Const.ErrorCodes.ExclusiveBehaviorExist;
                        result.ErrorData = masterBehaviorCreateResult.OccupiedAction;
                    }
                    else
                    {
                        throw new Exception("Behavior Create Error");
                    }
                }
            }
            else
            {
                throw new Exception("Class Not Begin Error");
            }

            return result;
        }

        /// <summary>
        /// 结束互评
        /// </summary>
        /// <param name="scoreSessonId"></param>
        public void Stop(String scoreSessonId)
        {
            var behaviorInfo = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
            {
                new BehaviorInfoGetParam()
                {
                    IsIncludeAction = true,
                    BehaviorIds = new List<String>(){ scoreSessonId }
                }
            })[0];
            if (behaviorInfo.Action == BehaviorActionNames.MutualScore)
            {
                var collaborationInfo = _collaborationInfo.Get(new List<CollaborationInfoGetParam>()
                {
                    new CollaborationInfoGetParam()
                    {
                        BehaviorIds = new List<String>(){ scoreSessonId },
                        IncludeAttributeKeys = new List<String>(){
                            Const.BehaviorAttributeNames.ScoredTarget,
                            Const.BehaviorAttributeNames.LessonId
                        },
                        IsIncludeAttribute = true,
                        IsIncludeSlaveBehaviorIds = true,
                    }
                })[scoreSessonId];
                var slaveBehaviorIds = collaborationInfo.SlaveBehaviorIds;
                var score = 0f;
                if (slaveBehaviorIds != null && slaveBehaviorIds.Count > 0)
                {
                    var behaviorIdDataMap = new Dictionary<String, String>();
                    var behaviorResult = _behaviorResult.Get(slaveBehaviorIds);
                    behaviorIdDataMap = behaviorResult.ToDictionary(b => b.BehaviorId, b => b.Result.ResultData);
                    if (behaviorIdDataMap.Values.Count > 0)
                    {
                        score = behaviorIdDataMap.Values.Average(d => float.Parse(d)) / 10;
                    }
                }
                // 根据评价类型，获取成员标识
                var scoredTargetInfo = new ScoredTargetInfo();
                scoredTargetInfo.FromJSONString(collaborationInfo.Attributes[Const.BehaviorAttributeNames.ScoredTarget]);
                var lessonId = collaborationInfo.Attributes[Const.BehaviorAttributeNames.LessonId];
                var memberIds = new List<string>();
                if (scoredTargetInfo.TargetType == TargetType.Group)
                {
                    // 获取小组下的成员
                    var lessonInfo = _lessonManagement.GetLessonSummary(lessonId).Info;
                    var queryResult = _queryUserMemberRole.Query(new QueryMemberRoleParameter()
                    {
                        Condition = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                        {
                            new EqualOperator(){ Name = Generic.Sociality.Group.Const.QueryName.GroupId, Value = lessonInfo.RelativeGroupId },
                            new EqualOperator(){ Name = Generic.Sociality.Group.Const.QueryName.RoleSetName, Value = scoredTargetInfo.ScoredId }
                        }
                        }
                    });
                    memberIds = queryResult.TotalCount > 0 ? queryResult.Results.Select(r => r.MemberId).ToList() : memberIds;
                }
                else if (scoredTargetInfo.TargetType == TargetType.SingleUser)
                {
                    memberIds.Add(scoredTargetInfo.ScoredId);
                }
                _collaborationCompletion.Complete(new List<CollaborationCompleteParam>()
                {
                    new CollaborationCompleteParam()
                    {
                        SlaveBehaviorResult = new BehaviorCompleteData()
                        {
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        },
                        CollaborationResult = new BehaviorCompleteData()
                        {
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        },
                        MasterBehaviorResult = new BehaviorCompleteData()
                        {
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        },
                        BehaviorIds = new List<String>(){ scoreSessonId }
                    }
                });
                _lessonPerformance.AddPerformanceRecord(lessonId, memberIds.Select(m => new MemberPerformance()
                {
                    MemberId = m,
                    Performances = new List<PerformanceData>()
                    {
                        new PerformanceData()
                        {
                            Category = PerformanceCategory.MutualEvaluation,
                            Description = string.Empty,
                            RegisterTime = _timeProvider.GetNow(),
                            Value = (float)Math.Round(score, 2)
                        }
                    }
                }).ToList());
                // 发送通知
                _message.SendMessage(new MessageData()
                {
                    Content = MessageCodes.Interact,
                    IsRetained = true,
                    Quality = QualityOfServiceType.AtLeastOnce,
                    Topic = MessageCodes.All
                });
            }
            else
            {
                throw new Exception("Stop MutualScore Error");
            }
        }

        /// <summary>
        /// 学生打分
        /// </summary>
        /// <param name="scoreSessonId">互评会话标识</param>
        /// <param name="score">Score</param>
        public void Score(String scoreSessonId, float score)
        {
            // 获取我的从行为
            var collaborationIdUserBehaviorInfoMap = _collaborationBehavior.Get(new List<CollaborationBehaviorGetParam>()
            {
                new CollaborationBehaviorGetParam()
                {
                    BehaviorIds = new List<String>(){ scoreSessonId },
                    IsAllUser = false,
                    UserIds = new List<String>() { PrincipalType.MySelf }
                }
            });

            var userIdSlaveBehaviorIdMap = new Dictionary<String, String>();
            if (collaborationIdUserBehaviorInfoMap.ContainsKey(scoreSessonId))
            {
                userIdSlaveBehaviorIdMap = collaborationIdUserBehaviorInfoMap[scoreSessonId].UserBehaviors
                    .Where(c => !c.IsMasterBehavior).ToDictionary(c => c.UserId, c => c.BehaviorId);
            }
            // 通过myself获取到的成员标识是用户标识，无法通过key进行匹配，故而取value的第一个值
            var slaveBehaviorId = userIdSlaveBehaviorIdMap.Count > 0 ? userIdSlaveBehaviorIdMap.Values.First() : String.Empty;

            _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
            {
                new BehaviorCompleteParam()
                {
                    BehaviorIds = new List<String>(){ slaveBehaviorId },
                    ResultCode = "SubmitEvaluate",
                    ResultType = ResultType.UserDefine,
                    ResultData = score.ToString()
                }
            });
        }

        /// <summary>
        /// 验证我的互评打分是否完成
        /// </summary>
        /// <param name="scoreSessonId"></param>
        /// <returns></returns>
        public StudentVerifyResult VerifyMyMutualScore(String scoreSessonId)
        {
            var result = new StudentVerifyResult();
            // 获取我的从行为
            var collaborationIdUserBehaviorInfoMap = _collaborationBehavior.Get(new List<CollaborationBehaviorGetParam>()
            {
                new CollaborationBehaviorGetParam()
                {
                    BehaviorIds = new List<String>(){ scoreSessonId },
                    IsAllUser = false,
                    UserIds = new List<String>() { PrincipalType.MySelf }
                }
            });

            var userIdSlaveBehaviorIdMap = new Dictionary<String, String>();
            if (collaborationIdUserBehaviorInfoMap.ContainsKey(scoreSessonId))
            {
                userIdSlaveBehaviorIdMap = collaborationIdUserBehaviorInfoMap[scoreSessonId].UserBehaviors
                    .Where(c => !c.IsMasterBehavior).ToDictionary(c => c.UserId, c => c.BehaviorId);
            }
            // 通过myself获取到的成员标识是用户标识，无法通过key进行匹配，故而取value的第一个值
            var slaveBehaviorId = userIdSlaveBehaviorIdMap.Count > 0 ? userIdSlaveBehaviorIdMap.Values.First() : String.Empty;
            if (!String.IsNullOrWhiteSpace(slaveBehaviorId))
            {
                // 获取我所打分数
                var completedBehaviorIds = new List<String>();
                var behaviorInfos = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
                {
                    new BehaviorInfoGetParam()
                    {
                        BehaviorIds = new List<String>(){ slaveBehaviorId },
                        IsIncludeState = true
                    }
                });
                completedBehaviorIds = behaviorInfos.Where(b => b.State.HasFlag(BehaviorState.IsCompleted)).Select(b => b.BehaviorId).ToList();
                var behaviorIdResultMap = new Dictionary<String, String>();
                if (completedBehaviorIds.Count > 0)
                {
                    var behaviorResult = _behaviorResult.Get(completedBehaviorIds);
                    behaviorIdResultMap = behaviorResult.ToDictionary(b => b.BehaviorId, b => b.Result.ResultData);
                }
                if (behaviorIdResultMap.ContainsKey(slaveBehaviorId))
                {
                    result.IsCompleted = true;
                }
            }
            else
            {
                // 加入协作行为
                var slaveBehaviorInfo = _collaborationParticipation.Join(new List<ParticipationJoinParam>()
                {
                    new ParticipationJoinParam()
                    {
                        BehaviorIds = new List<String>(){ scoreSessonId },
                        ReuseExistSlaveBehavior = true
                    }
                })[scoreSessonId];
                if (slaveBehaviorInfo.Success)
                {
                    // 执行从行为
                    var executeResult = _behaviorExecution.Execute(new List<BehaviorExecuteParam>()
                    {
                        new BehaviorExecuteParam()
                        {
                            Action = BehaviorActionNames.MutualScore,
                            BehaviorIds = new List<String>(){ slaveBehaviorInfo.BehaviorId }
                        }
                    })[0];

                    if (!executeResult.Success)
                    {
                        throw new Exception($"behavior execute faild. slave behaviorId is {slaveBehaviorInfo.BehaviorId}. error is {slaveBehaviorInfo.ErrorCode}");
                    }
                }
                else
                {
                    var errorCode = String.Empty;
                    if (slaveBehaviorInfo.ErrorCode == Generic.Behavior.Collaboration.Const.CreateErrorCode.NotJoinable)
                    {
                        errorCode = Const.ErrorCodes.MutualScoreStopped;
                    }
                    else if (slaveBehaviorInfo.ErrorCode == Generic.Behavior.Collaboration.Const.CreateErrorCode.NotAcceptable)
                    {
                        errorCode = Const.ErrorCodes.JoinWithoutPermission;
                    }
                    if(!String.IsNullOrWhiteSpace(errorCode))
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Content = new StringContent((new { Code = errorCode }).ToJSONString())
                        });
                    }
                }
            }
            return result;
        }
        private String GetCensusByLessonId(String lessonId)
        {
            var queryParam = new AndOperator()
            {
                OperatorExpression = new List<IConditionParameter>()
                {
                    new  EqualOperator(){ Name = Generic.Register.Census.Query.QueryName.CensusCode, Value = lessonId }
                }
            };
            var queryResult = _censusQuery.Query(new List<QueryParameter>()
            {
                new QueryParameter()
                {
                    Conditions = queryParam
                }
            })[0];
            return queryResult.Results[0];
        }
    }
}