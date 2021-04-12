using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;

using LINDGE.PARA.Fundamental.Message.Const;
using LINDGE.PARA.Fundamental.Message.Struct;
using LINDGE.PARA.Fundamental.Message.WebAPI.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Generic.Behavior.Single.Const;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.Behavior.Single.Query;
using LINDGE.PARA.Generic.Behavior.Single.Struct;
using LINDGE.PARA.Generic.ClassroomTeaching.Brainstorming.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Brainstorming.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Brainstorming.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.Proxy;
using LINDGE.Serialization;
using System.Globalization;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Service
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdea _idea = null;     
        private readonly IMember _member = null;
        private readonly IMessage _message = null;
        private readonly ICategory _category = null;
        private readonly ILifecycle _lifecycle = null;
        private readonly IConfigSource _configSource = null;
        private readonly IGroupDiscuss _groupDiscuss = null;     
        private readonly ICategoryVote _categoryVote = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly IBehaviorInfo _behaviorInfo = null;
        private readonly IBatchBehavior _batchBehavior = null;
        private readonly IBehaviorQuery _behaviorQuery = null;        
        private readonly IBehaviorAttribute _behaviorAttribute = null;
        private readonly IBehaviorExecution _behaviorExecution = null;
        private readonly IBehaviorCompletion _behaviorCompletion = null;

        public IdeaService(
            IIdea ideaService,
            IMember memberService,
            ICategory categoryService,
            ILifecycle lifecycleService,
            IGroupDiscuss groupDiscussService,
            ICategoryVote categoryVoteService,
            ITimeProvider timeProviderService,
            IConfigSource configSourceService,
            IProxy<IMessage> messageProxy,
            IProxy<IBehaviorInfo> behaviorInfoProxy,
            IProxy<IBatchBehavior> batchBehaviorProxy,
            IProxy<IBehaviorQuery> behaviorQueryProxy,
            IProxy<IBehaviorAttribute> behaviorAttributeProxy,
            IProxy<IBehaviorExecution> behaviorExecutionProxy,
            IProxy<IBehaviorCompletion> behaviorCompletionProxy
            )
        {
            _idea = ideaService;
            _member = memberService;
            _category = categoryService;
            _lifecycle = lifecycleService;
            _configSource = configSourceService;
            _groupDiscuss = groupDiscussService;
            _categoryVote = categoryVoteService;
            _timeProvider = timeProviderService;
            _message = messageProxy.GetObject();
            _behaviorInfo = behaviorInfoProxy.GetObject();
            _batchBehavior = batchBehaviorProxy.GetObject();
            _behaviorQuery = behaviorQueryProxy.GetObject();
            _behaviorAttribute = behaviorAttributeProxy.GetObject();
            _behaviorExecution = behaviorExecutionProxy.GetObject();
            _behaviorCompletion = behaviorCompletionProxy.GetObject();
        }

        /// <summary>
        /// 清除点子分类
        /// </summary>
        /// <param name="brainstormingId"></param>
        /// <param name="ideaId"></param>
        public void ClearIdeaCategory(string brainstormingId, string ideaId)
        {
            _category.ClearIdeaCategory(new List<string>() { ideaId });
            _category.ClearEmptyCategory(brainstormingId);
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public CategoryCreateResult CreateCategory(string brainstormingId, CategoryCreateParam parameter)
        {
            var cateGoryId = _category.Create(brainstormingId, parameter.CategoryName);
            if(parameter.IdeaIds.Count > 0)
            {
                _category.AddIdea(cateGoryId, parameter.IdeaIds);
            }
           
            return new CategoryCreateResult() {
                CategoryId = cateGoryId
            };
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="categroyId">分类标识</param>
        public void DeleteCategory(string brainstormingId, string categroyId)
        {
            _category.Delete(categroyId);
        }

        /// <summary>
        /// 删除点子
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="ideaId">点子标识</param>
        public void DeleteIdea(string brainstormingId, string ideaId)
        {
            _idea.Delete(new List<string>() { ideaId });
            _category.ClearEmptyCategory(brainstormingId);
        }

        /// <summary>
        /// 删除文本输入行为
        /// </summary>
        /// <param name="behaviorId"></param>
        public void DeleteTextInput(string behaviorId)
        {
            _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
            {
                new BehaviorCompleteParam()
                {
                    BehaviorIds = new List<String>(){ behaviorId },
                    ResultCode = "Delete",
                    ResultType = ResultType.UserDefine
                }
            });
        }

        /// <summary>
        /// 获取数据收集结果
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        public BrainstormingInfo GetBrainstormingInfo(string brainstormingId, BrainstormingInfoGetParam parameter)
        {
            var result = new BrainstormingInfo();
            //头脑风暴信息
            var brainstormingInfo = _lifecycle.Get(new List<string>() { brainstormingId })[0];
            var allGroupDiscussInfo = _groupDiscuss.GetAllGroupDiscuss(brainstormingInfo.Location);
            var groupDiscussIds = allGroupDiscussInfo.Where(g => g.GroupDiscussLocation == brainstormingId).Select(g => g.GroupDiscussId).ToList();
            var collectTime = _timeProvider.GetNow().ToUniversalTime();
            if(parameter.IsNew)
            {
                _behaviorAttribute.Update(new List<BehaviorAttributeUpdateParam>()
                {
                    new BehaviorAttributeUpdateParam()
                    {
                        BehaviorIds = new List<string>(){ groupDiscussIds[0] },
                        UpdateParams = new Dictionary<string, string>()
                        {
                            { Const.BehaviorAttributes.CollectTime, collectTime.ToString("O") }
                        }
                    }
                });
            } 
            else
            {
                var behaviorAttributes = _behaviorAttribute.Get(new BehaviorAttributeGetParam()
                {
                    BehaviorIds = new List<string>() { groupDiscussIds[0] },
                    Keys = new List<string>() { Const.BehaviorAttributes.CollectTime }
                });
                if(behaviorAttributes.Any() && !String.IsNullOrWhiteSpace(behaviorAttributes[0].Attributes[Const.BehaviorAttributes.CollectTime]))
                {
                    collectTime = DateTime.ParseExact(behaviorAttributes[0].Attributes[Const.BehaviorAttributes.CollectTime], "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }
            }

            var groupDiscussInfo = _groupDiscuss.Get(new GroupDiscussInfoGetParam() { 
                GroupDiscussId = groupDiscussIds[0],
                IncludeGroupInfo = false,
                IncludeGroupMember = true
            });
            var memberIds = groupDiscussInfo.GroupMember.MemberIds;
            if (memberIds.Count() > 0)
            {
                //点子信息
                var ideaInfos = _idea.Query(new IdeaQueryParam()
                {
                    BrainstormingId = brainstormingId,                 
                    AuthorIds = memberIds,
                    InculdeCategorized = true,
                    IsBeforePostTime = true,
                    PostTime = collectTime
                });
                //过滤掉没有点子的成员标识
                memberIds = ideaInfos.GroupBy(i => i.AuthorId).Where(g => memberIds.Contains(g.Key)).Select(g => g.Key).ToList();
                if (memberIds.Any())
                {
                    var memberSummaryInfos = _member.GetStudentInfos(groupDiscussInfo.LessonId, memberIds);
                    var sectionStudent = _configSource.GetSection<BrainstormingConfigSection>(BrainstormingConfigSection.DefaultSectionName);
                    //组成员点子列表
                    result.MemberIdeas = memberSummaryInfos.Select(m => new MemberIdeaInfo()
                    {
                        MemberId = m.MemberId,
                        Name = m.Name,
                        Portrait = String.IsNullOrWhiteSpace(m.Portrait) ? sectionStudent.DefaultStudentPortrait : m.Portrait,
                        Ideas = ideaInfos.Where(i => i.IsCategory == false && i.AuthorId == m.MemberId).Select(i => new IdeaInfo()
                        {
                            AuthorId = i.AuthorId,
                            IdeaId = i.IdeaId,
                            IdeaContent = i.Content
                        }).ToList()
                    }).ToList();
                }

                //所有分类信息
                var categoryInfos = _category.GetAllCategory(brainstormingId);
                var categoryIds = categoryInfos.Select(c => c.CategoryId).ToList();
                //分类投票结果
                var voteResult = _categoryVote.GetVoteResult(brainstormingId, categoryIds);
                var categoryVoteInfoMap = voteResult.VoteInfos.ToDictionary(c => c.CategoryId);
                //讨论结果
                result.DisscussResults = categoryInfos.Select(c => new DisscussResult()
                {
                    Name = c.Name,
                    CategoryId = c.CategoryId,                   
                    IsVoted = categoryVoteInfoMap.ContainsKey(c.CategoryId) && categoryVoteInfoMap[c.CategoryId].IsVoted,
                    VoteCount = categoryVoteInfoMap.ContainsKey(c.CategoryId) ? categoryVoteInfoMap[c.CategoryId].VoteCount : 0,
                    Ideas = ideaInfos.Where(i => i.IsCategory && i.CategoryId == c.CategoryId).Select(i => new IdeaInfo()
                    {
                        AuthorId = i.AuthorId,
                        IdeaId = i.IdeaId,
                        IdeaContent = i.Content
                    }).ToList()
                }).ToList();
            }
            return result;
        }

        /// <summary>
        /// 获取头脑风暴状态
        /// </summary>
        /// <param name="brainstormingId"></param>
        /// <returns></returns>
        public Struct.BrainstormingState GetBrainstormingState(String brainstormingId)
        {
            var result = new Struct.BrainstormingState();
            //头脑风暴信息
            var brainstormingInfo = _lifecycle.Get(new List<String>() { brainstormingId })[0];

            if (brainstormingInfo.State == Generic.ClassroomTeaching.Brainstorming.Const.BrainstormingState.Completed)
            {
                result.State = BrainstormState.Completed;
            } 
            else
            {
                var allGroupDiscussInfo = _groupDiscuss.GetAllGroupDiscuss(brainstormingInfo.Location);
                var groupDiscussIds = allGroupDiscussInfo.Where(g => g.GroupDiscussLocation == brainstormingId).Select(g => g.GroupDiscussId).ToList();
                // 为获取开始时间，总人数
                var groupDiscussInfo = _groupDiscuss.Get(new GroupDiscussInfoGetParam()
                {
                    GroupDiscussId = groupDiscussIds[0],
                    IncludeGroupInfo = false,
                    IncludeGroupMember = true
                });
                var startTime = (DateTime)groupDiscussInfo.StartTime;
                var nowTime = _timeProvider.GetNow();

                result.DuringSeconds = Convert.ToInt32((nowTime.ToUniversalTime().Subtract(startTime.ToUniversalTime())).TotalSeconds);
                result.TotalCount = groupDiscussInfo.GroupMember.MemberIds.Count;

                var behaviorAttributes = _behaviorAttribute.Get(new BehaviorAttributeGetParam()
                {
                    BehaviorIds = new List<string>() { groupDiscussIds[0] },
                    Keys = new List<string>() { Const.BehaviorAttributes.CollectTime }
                });
                if (behaviorAttributes.Any())
                {
                    result.State = BrainstormState.Discussing;
                } 
                else
                {
                    result.State = BrainstormState.Preparing;
                }
            }

            return result;
        }

        /// <summary>
        /// 查询是否需要输入内容
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        public Boolean GetInputText(string brainstormingId)
        {
            var result = false;
            var operatorExpression = new List<IConditionParameter>
            {
                new EqualOperator() { Name = QueryName.Action, Value = BehaviorActions.TextInput },
                new EqualOperator() { Name = QueryName.ReceptionData, Value = brainstormingId },
                new EqualOperator() { Name = QueryName.IsComplete, Value = "FALSE" }
            };
            var behaviorQueryResults = _behaviorQuery.ComplexQuery(new List<QueryParameter>() {
                new QueryParameter(){
                    Conditions = new AndOperator(){
                        OperatorExpression = new List<IConditionParameter>()
                        {
                            new AndOperator()
                            {
                                OperatorExpression = operatorExpression
                            }
                        },
                    }
                }
            }).ToList();
          
            if (behaviorQueryResults != null && behaviorQueryResults.Count > 0 && behaviorQueryResults[0].Results != null
               && behaviorQueryResults[0].Results.Count > 0)
            {
                var behaviorId = behaviorQueryResults[0].Results[0];
                var behaviorInfos = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>() {
                    new BehaviorInfoGetParam()
                    {
                        BehaviorIds = new List<string>(){ behaviorId },
                        IsIncludePrincipal = true
                    }
                });
                if(behaviorInfos != null && behaviorInfos.Count > 0 && behaviorInfos[0] != null)
                {
                    var principalInfo = behaviorInfos[0].Principal;
                    if (principalInfo.Data == null || principalInfo.Data == PrincipalType.MySelf)
                    {
                        result = true;
                    }
                }                
            }
            return result;
        }

        /// <summary>
        /// 获取内容输入行为（手机）
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        public String GetTextInput(string brainstormingId)
        {
            var brainstormingInfo = _lifecycle.Get(new List<string>() { brainstormingId })[0];
            if(brainstormingInfo.State == Generic.ClassroomTeaching.Brainstorming.Const.BrainstormingState.Completed)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.BrainStormingCompleted }).ToJSONString())
                });              
            }

            var operatorExpression = new List<IConditionParameter>
            {
                new EqualOperator() { Name = QueryName.Action, Value = BehaviorActions.TextInput },
                new EqualOperator() { Name = QueryName.ReceptionData, Value = brainstormingId },
                new EqualOperator() { Name = QueryName.IsComplete, Value = "FALSE" }
            };

            var behaviorQueryResults = _behaviorQuery.ComplexQuery(new List<QueryParameter>() {
                new QueryParameter(){
                    Conditions = new AndOperator(){
                        OperatorExpression = operatorExpression
                    }
                }
            }).ToList();


            string behaviorId;
            if (behaviorQueryResults != null && behaviorQueryResults.Count > 0 && behaviorQueryResults[0].Results != null
                && behaviorQueryResults[0].Results.Count > 0)
            {
                behaviorId = behaviorQueryResults[0].Results[0];
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.TextInputCompleted }).ToJSONString())
                });
            }

            var behaviorExecuteResult =  _behaviorExecution.Execute(new List<BehaviorExecuteParam>()
            {
                new BehaviorExecuteParam()
                {
                    Action = BehaviorActions.TextInput,
                    BehaviorIds = new List<String>(){ behaviorId }                  
                }
            });
            
            if(behaviorExecuteResult != null && behaviorExecuteResult.Count > 0 && !behaviorExecuteResult[0].Success)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.TextInputProgressing }).ToJSONString())
                });
            }

            return  behaviorId;
        }

        /// <summary>
        /// 获取内容输入结果
        /// </summary>
        /// <param name="behaviorId">记录内容输入的行为标识</param>
        /// <returns></returns>
        public TextInputResult GetTextInputContent(string behaviorId)
        {
            var result = new TextInputResult();
            var behaviorInfos = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
            {
                new BehaviorInfoGetParam()
                {
                    BehaviorIds = new List<string>(){ behaviorId },
                    IsIncludeAttribute = true,
                    IsIncludeState = true
                }
            });
            if (behaviorInfos != null && behaviorInfos.Count > 0 && behaviorInfos[0] != null)
            {
                var behaviorState = behaviorInfos[0].State;
                var behaviorAttrMap = behaviorInfos[0].Attributes ?? new Dictionary<string, string>();
                if (behaviorState.HasFlag(BehaviorState.IsCompleted))
                {                    
                    result.IsCompleted = true;
                    result.InputContent = behaviorAttrMap.ContainsKey(TextInputConst.textInput) ? behaviorAttrMap[TextInputConst.textInput] : String.Empty;
                }
                else
                {
                    result.IsCompleted = false;
                    result.InputContent = behaviorAttrMap.ContainsKey(TextInputConst.textInput) ? behaviorAttrMap[TextInputConst.textInput] : String.Empty;
                }
            }
            return result;
        }

        /// <summary>
        /// 修改分类名称
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="categroyId">分类标识</param>
        /// <param name="categoryName">分类名称</param>
        public void RenameCategory(string brainstormingId, string categroyId, string categoryName)
        {
            _category.Modify(new Generic.ClassroomTeaching.Brainstorming.Struct.CategoryInfo() { 
                CategoryId = categroyId,
                Name = categoryName
            });
        }

        /// <summary>
        /// 启动讨论板
        /// </summary>
        /// <param name="brainstormingId"></param>
        public void StartDiscussionBoard(string brainstormingId)
        {
            var brainstormingInfo = _lifecycle.Get(new List<String>() { brainstormingId })[0];
            var allGroupDiscussInfo = _groupDiscuss.GetAllGroupDiscuss(brainstormingInfo.Location);
            var groupDiscussIds = allGroupDiscussInfo.Where(g => g.GroupDiscussLocation == brainstormingId).Select(g => g.GroupDiscussId).ToList();

            if (brainstormingInfo.State == Generic.ClassroomTeaching.Brainstorming.Const.BrainstormingState.Progressing)
            {
                var utcNow = _timeProvider.GetNow().ToUniversalTime();
                _behaviorAttribute.Update(new List<BehaviorAttributeUpdateParam>()
                {
                    new BehaviorAttributeUpdateParam()
                    {
                        BehaviorIds = new List<string>(){ groupDiscussIds[0] },
                        UpdateParams = new Dictionary<string, string>()
                        {
                            { Const.BehaviorAttributes.CollectTime, utcNow.ToString("O") }
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 创建内容输入行为
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        public String StartTextInput(string brainstormingId)
        {
            var brainstormingInfo = _lifecycle.Get(new List<string>() { brainstormingId })[0];
            var memberRoles = _groupDiscuss.GetDiscussRole(brainstormingInfo.Location);
            var createResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
            {
                new BehaviorCreateParam()
                {
                    Action = BehaviorActions.TextInput,
                  
                    Exclusive = new BehaviorExclusive()
                    {                      
                        ActionList = new List<string>(){ BehaviorActions.TextInput },
                        ActionRange = ActionExclusiveRange.ActionList,
                        PrincipalRange = PrincipalExclusiveRange.Global
                    },
                    Principal = new BehaviorPrincipal()
                    {
                        Type = PrincipalType.Undetermined
                    },
                    Reception = new BehaviorReception()
                    {
                        Data = brainstormingId,
                        Type = ReceptionType.Identify
                    },
                    Limit = new BehaviorLimit()
                    {
                        ExpireTime = _timeProvider.GetNow().AddDays(1)
                    },
                    Qualify = new BehaviorQualify()
                    {
                        Principal = new List<BehaviorPrincipal>()
                        {
                            new BehaviorPrincipal()
                            {
                                Type = PrincipalType.ManyUser,
                                Data = memberRoles.ChairRoleSet
                            },
                            new BehaviorPrincipal()
                            {
                                Type = PrincipalType.ManyUser,
                                Data = memberRoles.ParticipantRoleSet
                            }
                        },
                        Manager = new List<BehaviorPrincipal>()
                        {
                            new BehaviorPrincipal()
                            {
                                Type = PrincipalType.MySelf
                            }
                        }
                    }
                }
            })[0];

            string behaviorId = String.Empty;
            if (createResult.Success)
            {
                behaviorId = createResult.BehaviorId;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(createResult.OccupiedBehaviorId))
                {
                    behaviorId = createResult.OccupiedBehaviorId;
                }              
            }

            // 发送通知
            _message.SendMessage(new MessageData()
            {
                Topic = MessageCodes.Group + brainstormingId,
                Content = MessageCodes.Content,
                IsRetained = true,
                Quality = QualityOfServiceType.AtLeastOnce,

            }) ;
            return  behaviorId;
        }

        /// <summary>
        /// 提交点子（手机）
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="ideaContent">点子内容</param>
        public void SubmitIdea(string brainstormingId, string ideaContent)
        {
            _idea.Create(brainstormingId, ideaContent);         
        }

        /// <summary>
        /// 提交内容输入结果（手机）
        /// </summary>
        /// <param name="behaviorId">行为标识</param>
        /// <param name="parameter"></param>
        public void SubmitTextInout(string behaviorId, TextInputParam parameter)
        {
            var behaviorExecuteResult = _behaviorExecution.Execute(new List<BehaviorExecuteParam>()
            {
                new BehaviorExecuteParam()
                {
                    Action = BehaviorActions.TextInput,
                    BehaviorIds = new List<String>(){ behaviorId }
                }
            });
            if(behaviorExecuteResult != null && behaviorExecuteResult.Count > 0 && behaviorExecuteResult[0].Success)
            {
                var updateParams = new Dictionary<string, string>
                {
                    { TextInputConst.textInput, parameter.Input }
                };
                var behaviorAttributeUpdateParam = new BehaviorAttributeUpdateParam()
                {
                    BehaviorIds = new List<string>() { behaviorId },
                    UpdateParams = updateParams
                };

                _behaviorAttribute.Update(new List<BehaviorAttributeUpdateParam>() { behaviorAttributeUpdateParam });

                if (parameter.IsComplete)
                {
                    _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
                    {
                        new BehaviorCompleteParam()
                        {
                            BehaviorIds = new List<String>(){ behaviorId },
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        }
                    });
                }
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.TextInputCompleted }).ToJSONString())
                });
            }            
        }

        /// <summary>
        /// 更新点子分类
        /// </summary>
        /// <param name="brainstormingId"></param>
        /// <param name="ideaId"></param>
        /// <param name="categoryId"></param>
        public void UpdateIdeaCategory(string brainstormingId, string ideaId, string categoryId)
        {
            _category.AddIdea(categoryId, new List<string>() { ideaId });
            _category.ClearEmptyCategory(brainstormingId);
        }
    }
}