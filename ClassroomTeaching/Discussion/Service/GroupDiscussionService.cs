using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using LINDGE.PARA.Generic.ClassroomTeaching.Brainstorming.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Brainstorming.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Cooperation.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Struct;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using LINDGE.Serialization;


namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Service
{
    public class GroupDiscussionService : IGroupDiscussionService
    {
        private readonly IGroup _group = null;
        private readonly ILesson _lesson = null;
        private readonly ILifecycle _lifecycle = null;      
        private readonly IStatistics _statistics = null;
        private readonly IEvaluative _evaluative = null;    
        private readonly IEnvironment _environment = null;      
        private readonly IGroupDiscuss _groupDiscuss = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly IDiscussControl _discussControl = null;
        private readonly Generic.ClassroomTeaching.Cooperation.Service.Interface.ILifecycle _lifecycleCooperation = null;
        private readonly Generic.ClassroomTeaching.Cooperation.Service.Interface.IStatistics _statisticsCooperation = null;

        public GroupDiscussionService(
            IGroup groupService,
            ILesson lessonService,
            ILifecycle lifecycleService,          
            IStatistics statisticsService,
            IEvaluative evaluativeService,
            IEnvironment environmentService, 
            IGroupDiscuss groupDiscussService,
            ITimeProvider timeProviderService,
            IDiscussControl discussControlService,
            Generic.ClassroomTeaching.Cooperation.Service.Interface.ILifecycle lifecycleCooperationService,
            Generic.ClassroomTeaching.Cooperation.Service.Interface.IStatistics statisticsCooperationService
            )
        {
            _group = groupService;
            _lesson = lessonService;
            _lifecycle = lifecycleService;
            _statistics = statisticsService;       
            _evaluative = evaluativeService;
            _environment = environmentService;
            _groupDiscuss = groupDiscussService;
            _timeProvider = timeProviderService;
            _discussControl = discussControlService;
            _lifecycleCooperation = lifecycleCooperationService;
            _statisticsCooperation = statisticsCooperationService;
        }

        /// <summary>
        /// 开始分组讨论
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="discussionType">讨论类型</param>
        /// <returns></returns>
        public string BeginGroupDiscussion(string lessonId,string discussionType)
        {
            string discussionId;
            try
            {
                discussionId = _discussControl.Start(lessonId, discussionType);
            }
            catch (ErrorCodeException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ex.ErrorCode, Data = ex.Message }).ToJSONString())
                });
            }
            return discussionId;
        }

        /// <summary>
        /// 结束分组讨论
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="discussionId">讨论标识</param>
        public void EndGroupDiscussion(string lessonId, string discussionId)
        {
            var evaluationInfos = _evaluative.GetEvaluativeInfoByLesson(lessonId);
            var discussEvaluateInfo = evaluationInfos.FirstOrDefault(e => e.RelationId == discussionId);
            if (discussEvaluateInfo != null)
            {
                _evaluative.CompleteMutualEvaluate(discussEvaluateInfo.MutualEvaluateId);
            }
            _discussControl.Stop(discussionId);
        }

        /// <summary>
        /// 获取小组的监控情况
        /// </summary>
        /// <param name="discussionId">讨论标识</param>
        /// <returns></returns>
        public DiscussionMonitorResult GetGroupDiscussionMonitor(string discussionId)
        {
            var discussionMonitorResult = new DiscussionMonitorResult();
            var discussionInfo = _groupDiscuss.Get(new GroupDiscussInfoGetParam() { 
                GroupDiscussId = discussionId
            });

            if (discussionInfo != null)
            {
                var nowTime = _timeProvider.GetNow();
                var startTime = Convert.ToDateTime(discussionInfo.StartTime);
                //小组信息
                var groupsInfos = _group.GetALlGroups(discussionInfo.LessonId);
                //设备信息
                var allDeviceInfos = _environment.GetAllAccepter(discussionInfo.LessonId);
                var groupDeviceMap = allDeviceInfos.Where(d => d.Type == DeviceTypes.GroupScreen).
                    ToDictionary(d => d.Id, d => d.BroadcastChannel?.DataUrl);

                discussionMonitorResult.DuringSeconds = Convert.ToInt32((nowTime.ToUniversalTime().Subtract(startTime.ToUniversalTime())).TotalSeconds);
                discussionMonitorResult.Groups = groupsInfos.Select(g => new Struct.GroupInfo()
                {
                    GroupId = g.GroupId,
                    Name = g.GroupName,
                    ChannelUrl = groupDeviceMap.ContainsKey(g.GroupId)? groupDeviceMap[g.GroupId]: String.Empty
                }).ToList();
            }
            return discussionMonitorResult;
        }

        /// <summary>
        /// 评审小组讨论结果
        /// </summary>
        /// <param name="discussionId">讨论标识</param>
        /// <returns></returns>
        public DiscussionReviewResult GetGroupDiscussionReview(string discussionId)
        {
            var discussionReviewResult = new DiscussionReviewResult();
            var discussionResult = _discussControl.Review(discussionId);
            if (discussionResult != null)
            {
                discussionReviewResult.DiscussType = discussionResult.Type;
                //互评信息
                var evaluativeInfos = _evaluative.GetEvaluativeInfoByLesson(discussionResult.LessonId);
                if(evaluativeInfos.Count > 0)
                {
                    var discussEvaluativeInfos = evaluativeInfos.Where(e => e.RelationId == discussionId).ToList();
                    discussionReviewResult.IsExistMutualEvaluative = discussEvaluativeInfos.Any();
                }              
                //设备信息
                var allDeviceInfos = _environment.GetAllAccepter(discussionResult.LessonId);
                var groupDeviceMap = allDeviceInfos.Where(d => d.Type == DeviceTypes.GroupScreen).ToDictionary(d => d.Name);
                var groupNameMap = allDeviceInfos.Where(d => d.Type == DeviceTypes.GroupScreen).ToDictionary(d => d.Id, d => d.Name);
                //小组讨论信息
                var groupDiscussInfos = _groupDiscuss.GetAllGroupDiscuss(discussionId);
                var groupDiscussMap = groupDiscussInfos.ToDictionary( g => groupNameMap[g.GroupId], g => g.GroupDiscussLocation);

                discussionReviewResult.GroupDiscussResults = discussionResult.DiscussInfos.Select(g => new DiscussionResultInfo()
                {
                    GroupName = g.GroupName,
                    GroupId = groupDeviceMap.ContainsKey(g.GroupName) ? groupDeviceMap[g.GroupName].Id : String.Empty,                   
                    ChannelUrl = groupDeviceMap.ContainsKey(g.GroupName) ? groupDeviceMap[g.GroupName].BroadcastChannel?.DataUrl : String.Empty,
                    BroadcastUrl = g.BroadcastUrl,
                    ScreenRecordUrl = g.ScreenRecordUrl,
                    DiscussBoardId = groupDiscussMap.ContainsKey(g.GroupName) ? groupDiscussMap[g.GroupName] : String.Empty
                }).ToList();
            }
            return discussionReviewResult;           
        }

        /// <summary>
        /// 获取分组讨论状态
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        public DiscussionStateInfo GetGroupDiscussionState(string lessonId)
        {
            var result = new DiscussionStateInfo();
            var lessonInfo = _lesson.Get(lessonId);
            if (lessonInfo.IsActive)
            {
                result.IsActive = true;
            }
            var discussCheckResult = _discussControl.Check(lessonId);   
            if(discussCheckResult != null)
            {
                var groupInfos = _group.GetALlGroups(lessonId);
                result.DiscussionId = discussCheckResult.DiscussId;
                result.State = discussCheckResult.State;
                result.Type = discussCheckResult.Type;               
                result.GroupCount = groupInfos.Count;
            }
            return result;
        }

        /// <summary>
        /// 获取我当前正在参与的分组讨论
        /// </summary>
        /// <param name="discussionId">讨论标识</param>
        /// <returns></returns>
        public MyDiscussionJoinResult GetMyDiscussion(string discussionId)
        {
            var result = new MyDiscussionJoinResult();
            var discussGroupIsJoinedResult = _discussControl.VerifyIsJoinGroup(discussionId);
            //是否分组
            if(discussGroupIsJoinedResult.HasGrouped)
            {
                result.HasGrouped = true;
                //获取所有小组讨论信息
                var groupDiscussInfos  = _groupDiscuss.GetAllGroupDiscuss(discussionId);
                var groupDiscussInfo = groupDiscussInfos.FirstOrDefault(g => g.GroupId == discussGroupIsJoinedResult.GroupId);
                if (groupDiscussInfo != null && !String.IsNullOrWhiteSpace(groupDiscussInfo.GroupDiscussLocation))
                {             
                    result.IsReady = groupDiscussInfo.State == DiscussStates.Progressing;
                    result.DiscussionType = groupDiscussInfo.Type;
                    result.DiscussionId = groupDiscussInfo.GroupDiscussLocation;
                }
                
            }
            return result; 
        }

        /// <summary>
        /// 参与小组讨论
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="discussionId">进行中的讨论标识</param>
        /// <returns></returns>
        public DiscussionJoinResult JoinDiscussion(string lessonId,string discussionId)
        {
            var discussionJoinResult = new DiscussionJoinResult();
            var myGroupDiscussInfo = _groupDiscuss.GetMyGroupDisucss(discussionId);
            if (!myGroupDiscussInfo.IsJoined)
            {
                var discussRoleInfo = _groupDiscuss.GetDiscussRole(discussionId);
                var deciveRoleInfo = _environment.GetAllDeviceRole(lessonId);
                if (myGroupDiscussInfo.Type == DisscussType.Brainstorming)
                {
                    var brainstormingIds = _lifecycle.Create(new List<BrainstormingCreateParam>() {
                        new BrainstormingCreateParam(){
                            ChairRoleSets = new List<string>{ discussRoleInfo.ChairRoleSet },
                            DiscussionBoardRoleSets = new List<string>{ discussRoleInfo.DiscussionBoardRoleSet,deciveRoleInfo.Teacher },
                            ParticipantRoleSets = new List<string>{ discussRoleInfo.ParticipantRoleSet },
                            Location = discussionId,
                            Name = myGroupDiscussInfo.Name
                        }
                    });
                    if (brainstormingIds.Count() > 0)
                    {
                        var brainstormingId = brainstormingIds[0];
                        //开始小组讨论
                        _groupDiscuss.Start(discussionId, brainstormingId);
                        discussionJoinResult.DiscussboardId = brainstormingId;
                    }
                }
                else if (myGroupDiscussInfo.Type == DisscussType.Cooperation)
                {
                    var cooperationIds = _lifecycleCooperation.Create(new List<CooperationCreateParam>() {
                        new CooperationCreateParam(){
                            ChairRoleSets = new List<string>{ discussRoleInfo.ChairRoleSet },
                            DiscussionBoardRoleSets = new List<string>{ discussRoleInfo.DiscussionBoardRoleSet,deciveRoleInfo.Teacher },
                            ParticipantRoleSets = new List<string>{ discussRoleInfo.ParticipantRoleSet },
                            Location = discussionId,
                            Name = myGroupDiscussInfo.Name
                        }
                    });
                    if (cooperationIds.Count() > 0)
                    {
                        var cooperationId = cooperationIds[0];
                        //开始小组讨论
                        var groupDiscussId = _groupDiscuss.Start(discussionId, cooperationId);
                        _groupDiscuss.ResetChair(groupDiscussId);
                        discussionJoinResult.DiscussboardId = cooperationId;
                    }
                }
            }
            else
            {                
                discussionJoinResult.DiscussboardId = myGroupDiscussInfo.GroupDiscussLocation;
            }
            discussionJoinResult.Type = myGroupDiscussInfo.Type;
            return discussionJoinResult;
        }

        /// <summary>
        /// 停止分组讨论
        /// </summary>
        /// <param name="discussionId">讨论标识</param>
        public void StopGroupDiscussion(string discussionId)
        {
            //获取所有小组讨论信息
            var groupDiscussInfos = _groupDiscuss.GetAllGroupDiscuss(discussionId);
            if (groupDiscussInfos.Count > 0)
            {
                //讨论类型
                var discussType = groupDiscussInfos[0].Type;
                AddMemberContribution(groupDiscussInfos,discussType);

                //结束小组讨论
                var broadcastIds = groupDiscussInfos.Select(g => g.GroupDiscussLocation).ToList();              
                if (discussType == DisscussType.Brainstorming)
                {
                    _lifecycle.Complete(broadcastIds);
                }
                else if (discussType == DisscussType.Cooperation)
                {
                    _lifecycleCooperation.Complete(broadcastIds);
                }
            }
            _discussControl.Pause(discussionId);
        }

        /// <summary>
        /// 添加小组成员贡献度
        /// </summary>
        /// <param name="groupDiscussInfos">小组讨论信息集合</param>
        /// <param name="discussType">讨论类型</param>
        private void AddMemberContribution(List<GroupDiscussInfo> groupDiscussInfos,string discussType)
        {
            //小组信息
            var groupIds = groupDiscussInfos.Select(g => g.GroupId).ToList();
            var groupInfos = _group.QueryMember(groupDiscussInfos[0].LessonId, groupIds);
            //所有的小组成员
            var memberIds = groupInfos.SelectMany(g => g.MemberIds).ToList();
            if (memberIds.Count > 0)
            {
                //获取成员贡献度
                var groupIdMap = groupInfos.ToDictionary(g => g.GroupId);            
                var statisticsMap = new Dictionary<string, Dictionary<string, int>>();
                if (discussType == DisscussType.Brainstorming)
                {
                    var statisticsGetParams = new List<Generic.ClassroomTeaching.Brainstorming.Param.StatisticsGetParam>();
                    foreach (var groupDiscussInfo in groupDiscussInfos)
                    {
                        var groupId = groupDiscussInfo.GroupId;
                        var groupMemberIds = groupIdMap[groupId].MemberIds;
                        if (groupMemberIds != null && groupMemberIds.Count > 0)
                        {
                            statisticsGetParams.Add(new Generic.ClassroomTeaching.Brainstorming.Param.StatisticsGetParam()
                            {
                                BrainstormingId = groupDiscussInfo.GroupDiscussLocation,
                                IsCountMemberIdea = true,
                                MemberIds = groupMemberIds
                            });
                        }
                    }
                    if (statisticsGetParams.Count > 0)
                    {
                        var statisticsGetInfo = _statistics.Get(statisticsGetParams);
                        statisticsMap = statisticsGetInfo.ToDictionary(s => s.BrainstormingId, s => s.MemberIdeaCountMap);
                    }
                }
                else
                {
                    var statisticsGetParams = new List<Generic.ClassroomTeaching.Cooperation.Param.StatisticsGetParam>();
                    foreach (var groupDiscussInfo in groupDiscussInfos)
                    {
                        var groupId = groupDiscussInfo.GroupId;
                        var groupMemberIds = groupIdMap[groupId].MemberIds;
                        if (groupMemberIds != null && groupMemberIds.Count > 0)
                        {
                            statisticsGetParams.Add(new Generic.ClassroomTeaching.Cooperation.Param.StatisticsGetParam()
                            {
                                CooperationId = groupDiscussInfo.GroupDiscussLocation,
                                IsCountMemberMessage = true,
                                MemberIds = groupMemberIds
                            });
                        }
                    }
                    if (statisticsGetParams.Count > 0)
                    {
                        var statisticsGetInfo = _statisticsCooperation.Get(statisticsGetParams);
                        statisticsMap = statisticsGetInfo.ToDictionary(s => s.CooperationId, s => s.MemberMessageCountMap);
                    }
                }
                //添加成员贡献度                           
                var memberContributeSetParam = groupDiscussInfos.Select(g => new MemberContributeSetParam()
                {
                    GroupDiscussId = g.GroupDiscussId,
                    MemberContributeInfos = statisticsMap.ContainsKey(g.GroupDiscussLocation) ?
                    statisticsMap[g.GroupDiscussLocation].ToList().Select(d => new MemberContributeInfo()
                    {
                        MemberId = d.Key,
                        ContributeValue = d.Value
                    }).ToList() :
                    new List<MemberContributeInfo>()
                }).ToList();
                _groupDiscuss.SetMemberContribute(memberContributeSetParam);
            }        
        }
    }
}