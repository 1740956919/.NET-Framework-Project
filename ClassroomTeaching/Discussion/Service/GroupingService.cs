using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Service
{
    public class GroupingService : IGroupingService
    {
        private readonly IGroupArrange _groupArrange = null;
        private readonly IGroup _group = null;
        private readonly IMember _member = null;
        private readonly IConfigSource _configSource = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly ILesson _lesson = null;

        public GroupingService(IGroupArrange groupArrangeService,
            IGroup groupService,
            IMember memberService,
            IConfigSource configSourceService,
            ITimeProvider timeProvider,
            ILesson lesson)
        {
            _groupArrange = groupArrangeService;
            _group = groupService;
            _member = memberService;
            _configSource = configSourceService;
            _timeProvider = timeProvider;
            _lesson = lesson;
        }

        /// <summary>
        /// 获取分组结果
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public GroupResult GetGroupResult(String lessonId)
        {
            var result = new GroupResult();
            var section = _configSource.GetSection<DiscussionConfigSection>(DiscussionConfigSection.DefaultSectionName);
            var lessonInfo = _lesson.Get(lessonId);
            if(lessonInfo.IsActive)
            {
                result.IsProcessing = true;
            }
            var groupArrangeCheckResult = _groupArrange.Check(lessonId);
            result.IsGrouping = groupArrangeCheckResult.IsGrouping;
            // 获取课时下所有的小组
            var groupInfos = _group.GetALlGroups(lessonId);
            // 生成分组标识对应分组名的字典
            var groupIdNameMap = groupInfos.ToDictionary(g => g.GroupId, g=> g.GroupName);
            var groupIds = groupIdNameMap.Select(g => g.Key).ToList();
            // 根据小组标识列表查询小组成员
            var groupIdMemberIdsInfos = _group.QueryMember(lessonId, groupIds);
            var groupIdMemberIdsMap = groupIdMemberIdsInfos.ToDictionary(g => g.GroupId, g => g.MemberIds);
            // 生成所有已分组的成员标识列表
            var groupedMemberIds = groupIdMemberIdsInfos.SelectMany(g => g.MemberIds);
            result.GroupedStudentCount = groupedMemberIds.Count();
            // 获取课时下所有的成员信息
            var memberSummaryInfos = _member.GetAllStudentInfos(lessonId);
            result.TotalStudentCount = memberSummaryInfos.Where(m => m.IsLogon).Count();
            // 生成成员标识和成员信息的字典
            var memberIdMemberInfoMap = memberSummaryInfos.ToDictionary(m=> m.MemberId);
            // 生成小组列表的数据结构
            result.Groups = groupIdNameMap.Select(g => new GroupFullInfo() 
            {
                GroupId = g.Key,
                Name = g.Value,
                Members = groupIdMemberIdsMap.ContainsKey(g.Key) ? groupIdMemberIdsMap[g.Key].Select(m => new MemberBasicInfo()
                {
                    MemberId = m,
                    Name = memberIdMemberInfoMap.ContainsKey(m) ? memberIdMemberInfoMap[m].Name : String.Empty,
                    Portrait = memberIdMemberInfoMap.ContainsKey(m) 
                    ? (String.IsNullOrEmpty(memberIdMemberInfoMap[m].Portrait) ? section.DefaultStudentPortrait : memberIdMemberInfoMap[m].Portrait)
                    : section.DefaultStudentPortrait
                }).ToList() : new List<MemberBasicInfo>()
            }).ToList();
            if (groupArrangeCheckResult.IsGrouping)
            {
                var nowTime = _timeProvider.GetNow();
                result.DuringSeconds = Convert.ToInt32((nowTime.ToUniversalTime().Subtract(groupArrangeCheckResult.StartTime.Value.ToUniversalTime())).TotalSeconds);
                result.UngroupStudents = memberSummaryInfos.Where(m => m.IsLogon && !groupedMemberIds.Contains(m.MemberId)).Select(m => m.Name).ToList();
            }
            return result;
        }

        /// <summary>
        /// 获取可选择的分组信息
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public OptionalGroupInfo GetOptionalGroupInfo(String lessonId)
        {
            var result = new OptionalGroupInfo();
            var groupArrangeCheckResult = _groupArrange.Check(lessonId);
            if (groupArrangeCheckResult.IsGrouping)
            {
                var groupInfos = _group.GetALlGroups(lessonId);
                result.Groups = groupInfos.Select(g => new GroupBasicInfo()
                {
                    GroupId = g.GroupId,
                    Name = g.GroupName
                }).ToList();
                var myGroupGetResult = _group.GetMyGroup(lessonId);
                if (myGroupGetResult.IsJoined)
                {
                    result.IsSelectedGroup = true;
                    result.SelectedGroupId = myGroupGetResult.GroupId;
                }
            }

            return result;
        }

        /// <summary>
        /// 加入分组
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="groupId"></param>
        public void Join(String lessonId, String groupId)
        {
            var groupArrangeCheckResult = _groupArrange.Check(lessonId);
            if (groupArrangeCheckResult.IsGrouping)
            {
                _groupArrange.Join(lessonId, groupId);
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.GroupClose }).ToJSONString())
                });
            }
        }

    
        /// <summary>
        /// 开始分组
        /// </summary>
        /// <param name="lessonId"></param>
        public void Start(String lessonId)
        {
            try
            {
                _groupArrange.Start(lessonId);
            }
            catch (ErrorCodeException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ex.ErrorCode, Data = ex.Message }).ToJSONString())
                });
            }
        }

        /// <summary>
        /// 停止分组
        /// </summary>
        /// <param name="lessonId"></param>
        public void Stop(String lessonId)
        {
            _groupArrange.Stop(lessonId);
        }
    }
}