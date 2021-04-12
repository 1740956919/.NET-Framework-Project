using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.Const;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.Behavior.Single.Struct;
using LINDGE.PARA.Generic.ClassroomTeaching.Cooperation.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FileInfo = LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct.FileInfo;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Service
{
    public class CooperationService : ICooperationService
    {
        private readonly ILifecycle _lifecycle = null;
        private readonly IGroupDiscuss _groupDiscuss = null;
        private readonly IMember _member = null;
        private readonly IMessage _message = null;
        private readonly IIOProvider _iOProvider = null;
        private readonly IConfigSource _configSource = null;
        private readonly IBatchBehavior _batchBehavior = null;
        private readonly IBehaviorExecution _behaviorExecution = null;
        private readonly IBehaviorCompletion _behaviorCompletion = null;

        public CooperationService(ILifecycle lifecycleService,
            IGroupDiscuss groupDiscussService,
            IMember memberService,
            IMessage messageService,
            IIOProvider iOProvider,
            IConfigSource configSource,
            IProxy<IBatchBehavior> batchBehaviorProxy,
            IProxy<IBehaviorExecution> behaviorExecutionProxy,
            IProxy<IBehaviorCompletion> behaviorCompletionProxy)
        {
            _lifecycle = lifecycleService;
            _groupDiscuss = groupDiscussService;
            _member = memberService;
            _message = messageService;
            _iOProvider = iOProvider;
            _configSource = configSource;
            _batchBehavior = batchBehaviorProxy.GetObject();
            _behaviorExecution = behaviorExecutionProxy.GetObject();
            _behaviorCompletion = behaviorCompletionProxy.GetObject();
        }

        /// <summary>
        /// 主持讨论
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="parameter"></param>
        public void ChairDiscussion(String cooperationDiscussId, DiscussionChairParam parameter)
        {
            // 获取协作讨论信息
            var cooperationInfos = _lifecycle.Get(new List<String>() { cooperationDiscussId });
            var behavoirId = String.Empty;
            var flag = false;
            if (cooperationInfos.Any() && cooperationInfos[0].State == Generic.ClassroomTeaching.Cooperation.Const.CooperationState.Progressing)
            {
                // 获取主持人
                var groupDiscussId = _groupDiscuss.GetGroupDiscussId(cooperationInfos[0].Location, cooperationDiscussId);
                var chairId = _groupDiscuss.GetChair(groupDiscussId);
                if (!String.IsNullOrEmpty(chairId))
                {
                    // 有主持人
                    flag = true;
                } 
                else
                {
                    // 创建独占行为
                    var behaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
                    {
                        new BehaviorCreateParam()
                        {
                            Action = BehaviorActionNames.SetChair,
                            Principal = new BehaviorPrincipal()
                            {
                                Type = PrincipalType.MySelf
                            },
                            Reception = new BehaviorReception()
                            {
                                Data = cooperationDiscussId,
                                Type = ReceptionType.Identify
                            },
                            Exclusive = new BehaviorExclusive()
                            {
                                ActionList = new List<String>(){ BehaviorActionNames.SetChair },
                                ActionRange = ActionExclusiveRange.ActionList,
                                PrincipalRange = PrincipalExclusiveRange.Global
                            }
                        }
                    })[0];
                    if (behaviorCreateResult.Success)
                    {
                        behavoirId = behaviorCreateResult.BehaviorId;
                    }
                    else if (!String.IsNullOrWhiteSpace(behaviorCreateResult.OccupiedBehaviorId))
                    {
                        behavoirId = behaviorCreateResult.OccupiedBehaviorId;
                    } 
                    else
                    {
                        flag = true;
                    }
                    if (!flag && !String.IsNullOrWhiteSpace(behavoirId))
                    {
                        // 执行行为
                        _behaviorExecution.Execute(new List<BehaviorExecuteParam>()
                        {
                            new BehaviorExecuteParam()
                            {
                                Action = BehaviorActionNames.SetChair,
                                BehaviorIds = new List<String>(){ behavoirId }
                            }
                        });
                        _groupDiscuss.ChangeChair(groupDiscussId, 
                            parameter.IsAssignMember ? parameter.MemberId : Generic.ClassroomTeaching.Scene.Const.PrincipalType.MySelf);

                        _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
                        {
                            new BehaviorCompleteParam()
                            {
                                BehaviorIds = new List<String>() { behavoirId },
                                ResultCode = "Complete",
                                ResultType = ResultType.UserDefine
                            }
                        });
                    }
                }
                if (flag)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Content = new StringContent((new { Code = ErrorCodes.ExistChair }).ToJSONString())
                    });
                }
            } 
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.IsNotDiscussing }).ToJSONString())
                });
            }
        }

        /// <summary>
        /// 更换主持人
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="memberId"></param>
        public void ChangeChair(String cooperationDiscussId, String memberId)
        {
            // 获取协作讨论信息
            var cooperationInfos = _lifecycle.Get(new List<String>() { cooperationDiscussId });
            if (cooperationInfos.Any())
            {
                var groupDiscussId = _groupDiscuss.GetGroupDiscussId(cooperationInfos[0].Location, cooperationDiscussId);
                _groupDiscuss.ChangeChair(groupDiscussId, memberId);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public FileDownloadResult DownloadFile(String cooperationDiscussId, String messageId)
        {
            var result = new FileDownloadResult();
            var messageInfos = _message.Get(new List<String>() { messageId});
            if (messageInfos.Any() && messageInfos[0].ContentInfo.IsIncludeFile)
            {
                result.FileStream = _iOProvider.Open(messageInfos[0].ContentInfo.Files[0].Handle, FileMode.Open, FileAccess.Read, FileShare.Read);
                result.FileName = messageInfos[0].ContentInfo.Files[0].Name;
            }

            return result;
        }

        /// <summary>
        /// 获取协作讨论状态
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <returns></returns>
        public CooperationStatus GetCooperationStatus(String cooperationDiscussId)
        {
            var result = new CooperationStatus();
            // 获取协作讨论信息
            var cooperationInfos = _lifecycle.Get(new List<String>() { cooperationDiscussId });
            if(cooperationInfos.Any())
            {
                result.State = cooperationInfos[0].State;
                if(cooperationInfos[0].State == Generic.ClassroomTeaching.Cooperation.Const.CooperationState.Progressing)
                {
                    // 获取主持人
                    var groupDiscussId = _groupDiscuss.GetGroupDiscussId(cooperationInfos[0].Location, cooperationDiscussId);
                    var chairId = _groupDiscuss.GetChair(groupDiscussId);
                    result.HasChair = !String.IsNullOrEmpty(chairId);
                    result.ChairMemberId = chairId;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取组成员信息
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <returns></returns>
        public DiscussionMemberInfo GetGroupMembers(String cooperationDiscussId)
        {
            var result = new DiscussionMemberInfo();
            // 获取协作讨论信息
            var cooperationInfos = _lifecycle.Get(new List<String>() { cooperationDiscussId });
            if (cooperationInfos.Any())
            {
                result.State = cooperationInfos[0].State;
                // 获取小组讨论标识
                var groupDiscussId = _groupDiscuss.GetGroupDiscussId(cooperationInfos[0].Location, cooperationDiscussId);
                // 获取小组讨论信息
                var groupDiscussSummaryInfo = _groupDiscuss.Get(new Generic.ClassroomTeaching.Scene.Param.GroupDiscussInfoGetParam()
                {
                    GroupDiscussId = groupDiscussId,
                    IncludeGroupInfo = true,
                    IncludeGroupMember = true
                });
                var lessonId = groupDiscussSummaryInfo.LessonId;
                var groupMemberIds = groupDiscussSummaryInfo.GroupMember.MemberIds;
                // 获取成员信息
                if (groupMemberIds.Any())
                {
                    var memberSummaryInfos = _member.GetStudentInfos(lessonId, groupMemberIds);
                    // 获取主持人
                    var chairId = _groupDiscuss.GetChair(groupDiscussId);
                    result.GroupMembers = memberSummaryInfos.Select(m => new GroupMemberInfo()
                    {
                        MemberId = m.MemberId,
                        Name = m.Name,
                        IsChair = m.MemberId == chairId ? true : false
                    }).ToList();
                }
            }
            return result;
        }

        /// <summary>
        /// 获取历史消息信息
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <returns></returns>
        public HistoryMessageInfo GetHistoryMessageInfo(String cooperationDiscussId)
        {
            var result = new HistoryMessageInfo();
            var cooperationInfos = _lifecycle.Get(new List<String> { cooperationDiscussId });
            if (cooperationInfos.Any())
            {
                result.State = cooperationInfos[0].State;
                var groupDiscussId = _groupDiscuss.GetGroupDiscussId(cooperationInfos[0].Location, cooperationDiscussId);
                result.Messages = QueryMessage(groupDiscussId, cooperationDiscussId, null);
                // 获取主持人
                var chairId = _groupDiscuss.GetChair(groupDiscussId);
                result.ChairId = chairId;
            }
            return result;
        }

        /// <summary>
        /// 查询指定时间后的消息
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<MessageInfo> QueryPeriodMessages(String cooperationDiscussId, MessageQueryParam parameter)
        {
            var result = new List<MessageInfo>();
            var cooperationInfos = _lifecycle.Get(new List<String> { cooperationDiscussId });
            if (cooperationInfos.Any())
            {
                var groupDiscussId = _groupDiscuss.GetGroupDiscussId(cooperationInfos[0].Location, cooperationDiscussId);
                result = QueryMessage(groupDiscussId, cooperationDiscussId, parameter.PostTime);
            }
            return result;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="cooperationDiscussId">协作讨论标识</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public MessageInfo SendMessage(String cooperationDiscussId, MessageSendParam parameter)
        {
            var result = new MessageInfo();
            var cooperationInfos = _lifecycle.Get(new List<String> { cooperationDiscussId });
            var section = _configSource.GetSection<CooperationConfigSection>(CooperationConfigSection.DefaultSectionName);
            var messageInfo = _message.Create(cooperationDiscussId, new Generic.ClassroomTeaching.Cooperation.Struct.ContnetInfo()
            {
                Content = parameter.Content,
                IsIncludeFile = parameter.IsFile,
                Files = parameter.IsFile && parameter.FileInfo != null ? new List<Generic.ClassroomTeaching.Cooperation.Struct.FileInfo>()
                {
                    new Generic.ClassroomTeaching.Cooperation.Struct.FileInfo()
                    {
                        Handle = parameter.FileInfo.Handle,
                        Size = parameter.FileInfo.Size,
                        Name = parameter.FileInfo.FileName,
                        Type = parameter.FileInfo.FileType
                    }
                }: null
            });
            var authorId = messageInfo.AuthorId;
            var groupDiscussId = _groupDiscuss.GetGroupDiscussId(cooperationInfos[0].Location, cooperationDiscussId);
            var groupDiscussSummaryInfo = _groupDiscuss.Get(new Generic.ClassroomTeaching.Scene.Param.GroupDiscussInfoGetParam()
            {
                GroupDiscussId = groupDiscussId,
                IncludeGroupInfo = true
            });
            var lessonId = groupDiscussSummaryInfo.LessonId;
            // 获取成员信息
            var memberSummaryInfos = _member.GetStudentInfos(lessonId, new List<String>() { authorId });
            result.Content = messageInfo.ContentInfo.Content;
            result.Author = new AuthorInfo()
            {
                IsMySelf = messageInfo.IsMySelf,
                AuthorId = memberSummaryInfos.Any() ? memberSummaryInfos[0].MemberId: String.Empty,
                DisplayName = memberSummaryInfos.Any() ? memberSummaryInfos[0].Name : String.Empty,
                Portrait = memberSummaryInfos.Any() && !String.IsNullOrEmpty(memberSummaryInfos[0].Portrait) ? memberSummaryInfos[0].Portrait : section.DefaultStudentPortrait
            };
            result.CreateTime = messageInfo.PostTime;
            result.MessageId = messageInfo.MessageId;
            result.IsFile = messageInfo.ContentInfo.IsIncludeFile;
            result.FileInfo = messageInfo.ContentInfo.IsIncludeFile ? new FileInfo()
            {
                Size = messageInfo.ContentInfo.Files[0].Size,
                FileName = messageInfo.ContentInfo.Files[0].Name,
                FileType = messageInfo.ContentInfo.Files[0].Type,
                Handle = messageInfo.ContentInfo.Files[0].Handle
            } : null;
            return result;
        }

        /// <summary>
        /// 查询消息
        /// </summary>
        /// <param name="slaveBehaviorId"></param>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="postTime"></param>
        /// <returns></returns>
        private List<MessageInfo> QueryMessage(String slaveBehaviorId, String cooperationDiscussId, DateTime? postTime)
        {
            var result = new List<MessageInfo>();
            var section = _configSource.GetSection<CooperationConfigSection>(CooperationConfigSection.DefaultSectionName);
            // 获取小组讨论信息
            var groupDiscussSummaryInfo = _groupDiscuss.Get(new Generic.ClassroomTeaching.Scene.Param.GroupDiscussInfoGetParam()
            {
                GroupDiscussId = slaveBehaviorId,
                IncludeGroupInfo = true,
                IncludeGroupMember = true
            });
            var lessonId = groupDiscussSummaryInfo.LessonId;
            var groupMemberIds = groupDiscussSummaryInfo.GroupMember.MemberIds;
            if (groupMemberIds.Any())
            {
                var messageInfos = _message.Query(new Generic.ClassroomTeaching.Cooperation.Param.MessageQueryParam()
                {
                    CooperationId = cooperationDiscussId,
                    IsOnlyQueryAfterSpecifiedTime = postTime == null ? false : true,
                    SpecifiedTime = postTime == null ? null : postTime,
                    AuthorIds = groupMemberIds
                });
                var authorIds = messageInfos.Select(m => m.AuthorId).Distinct().ToList();
                // 获取成员信息
                if (authorIds.Any())
                {
                    var memberSummaryInfos = _member.GetStudentInfos(lessonId, authorIds);
                    var memberIdNamePortraitMap = memberSummaryInfos.ToDictionary(m => m.MemberId, m => new AuthorInfo()
                    {
                        DisplayName = m.Name,
                        Portrait = m.Portrait
                    });

                    result = messageInfos.Select(m => new MessageInfo()
                    {
                        Author = new AuthorInfo()
                        {
                            AuthorId = m.AuthorId,
                            DisplayName = memberIdNamePortraitMap.ContainsKey(m.AuthorId) ? memberIdNamePortraitMap[m.AuthorId].DisplayName : String.Empty,
                            Portrait = memberIdNamePortraitMap.ContainsKey(m.AuthorId) && !String.IsNullOrEmpty(memberIdNamePortraitMap[m.AuthorId].Portrait) ? memberIdNamePortraitMap[m.AuthorId].Portrait : section.DefaultStudentPortrait,
                            IsMySelf = m.IsMySelf
                        },
                        Content = m.ContentInfo.Content,
                        CreateTime = m.PostTime,
                        IsFile = m.ContentInfo.IsIncludeFile,
                        MessageId = m.MessageId,
                        FileInfo = m.ContentInfo.IsIncludeFile ? new FileInfo()
                        {
                            FileName = m.ContentInfo.Files[0].Name,
                            Size = m.ContentInfo.Files[0].Size,
                            FileType = m.ContentInfo.Files[0].Type,
                            Handle = m.ContentInfo.Files[0].Handle
                        } : null
                    }).OrderBy(m => m.CreateTime).ToList();
                }
            }
            return result;
        }
    }
}