using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Logic;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Service
{
    public class InteractService : IInteractService
    {
        private readonly IMember _member = null;
        private readonly IPractice _practice = null;
        private readonly IDiscussControl _discussControl = null;
        private readonly IProjection _projection = null;
        private readonly IGrabAnswer _grabAnswer = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly ILesson _lesson = null;
        private readonly IEvaluative _evaluative = null;
        private readonly IConfigSource _configSource = null;

        public InteractService(
             IMember memberService,
             IPractice practiceService,
             IDiscussControl discussControlService,
             IProjection projectionService,
             IGrabAnswer grabAnswerService,
             ITimeProvider timeProviderService,
             ILesson lessonService,
             IConfigSource configSourceService,
             IEvaluative evaluativeService)
        {
            _member = memberService;
            _practice = practiceService;
            _projection = projectionService;
            _discussControl = discussControlService;
            _grabAnswer = grabAnswerService;
            _timeProvider = timeProviderService;
            _lesson = lessonService;
            _evaluative = evaluativeService;
            _configSource = configSourceService;
        }

        /// <summary>
        /// 获取所有的投屏通道
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public List<ChannelInfo> GetAllChannels(string lessonId, string deviceType)
        {
            var deviceCategory = ConvertDeviceType.ConvertEnumFromString(deviceType);
            var channelInfos = _projection.GetAvailableChannels(lessonId, deviceCategory);                  

            return channelInfos.Where(c =>c.ConnectUrl != null).Select(c => new ChannelInfo() {
                Name = c.ChannelName,
                IP = Regex.Match(c.ConnectUrl, @"\d+\.\d+\.\d+\.\d*").Value,
                Port = Regex.Match(c.ConnectUrl, @"\:\d+").Value.Substring(1),
            }).ToList();
        }

        /// <summary>
        /// 查询当前的上课状态
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        public ClassStateResult GetClassState(String lessonId)
        {
            var lessonStateInfo = _lesson.GetState(lessonId);
            //互动信息
            var interactiveInfo = new InteractiveInfo();
            if (lessonStateInfo.IsActive)
            {
                if (lessonStateInfo.IsMutualEvaluating)
                {
                    var mutualEvaluateInfos = _evaluative.GetEvaluativeInfoByLesson(lessonId);
                    interactiveInfo.Type = InteractionTypes.MutualScore;
                    interactiveInfo.Id = mutualEvaluateInfos.Any() ? mutualEvaluateInfos[0].MutualEvaluateId : String.Empty;
                }
                else if (lessonStateInfo.IsGrabAnswering)
                {
                    var grabAnswerIsExistInfo = _grabAnswer.GetGrabAnswerByLesson(lessonId);
                    if (!grabAnswerIsExistInfo.IsGrabbed)
                    {
                        interactiveInfo.Type = InteractionTypes.GrabAnswer;
                        interactiveInfo.Id = grabAnswerIsExistInfo.GrabAnswerId;
                    }
                }             
                else if (lessonStateInfo.IsPracticing)
                {
                    var practiceWithIsExistInfo = _practice.GetPracticeByLesson(lessonId);
                    if (practiceWithIsExistInfo.State == PracticeStates.Progressing)
                    {
                        interactiveInfo.Type = InteractionTypes.Practice;
                        interactiveInfo.Id = practiceWithIsExistInfo.PracticeId;
                    }
                }
                else if (lessonStateInfo.IsGroupDiscussing)
                {
                    var discussCheckResult = _discussControl.Check(lessonId);
                    if(discussCheckResult.State == DiscussStates.Progressing)
                    {
                        interactiveInfo.Type = InteractionTypes.Discussion;
                        interactiveInfo.Id = discussCheckResult.DiscussId;
                    }
                }
                else if(lessonStateInfo.IsGrouping)
                {
                    interactiveInfo.Type = InteractionTypes.ChooseGroup;
                    interactiveInfo.Id = lessonId;
                }             
            }

            return new ClassStateResult()
            {
                IsActive = lessonStateInfo.IsActive,
                InteractiveInfo = interactiveInfo,
                NeedInteract = interactiveInfo.Id != null
            };
        }

        /// <summary>
        /// 学生参与抢答
        /// </summary>
        /// <param name="grabAnswerId">抢答标识</param>
        public GrabResult JoinGrabAnswer(string grabAnswerId)
        {
            var grabAnswerJoinResult = new GrabResult();
            var grabAnswerInfo = _grabAnswer.Join(grabAnswerId);

            //其他人抢答成功
            if (grabAnswerInfo.IsGrabbed && !grabAnswerInfo.IsMyself)
            {
                //从配置文件获取倒计时长
                var section = _configSource.GetSection<CustomConfigSection>(CustomConfigSection.DefaultSectionName);
                var countDownMillseconds = section.CountDownMillseconds;               
                //抢答成功的学生信息
                var studentInfo = new StudentInfo();
                var studentInfos = _member.GetStudentInfos(grabAnswerInfo.LessonId, new List<string> { grabAnswerInfo.MemberId });
                if (studentInfos != null && studentInfos.Count() > 0)
                {                 
                    studentInfo.Name = studentInfos[0].Name;
                    studentInfo.Photo = String.IsNullOrWhiteSpace(studentInfos[0].Portrait) ? section.DefaultStudentPortrait : studentInfos[0].Portrait;
                }
                //抢答所用的时间            
                var endTime = (DateTime)grabAnswerInfo.GrabbedTime;
                var duringSeconds = Convert.ToInt32(endTime.ToUniversalTime().Subtract(grabAnswerInfo.StartTime.ToUniversalTime()).TotalSeconds);
                studentInfo.SpendSeconds = duringSeconds - countDownMillseconds / 1000;                         
                grabAnswerJoinResult.Winer = studentInfo;
            }
            grabAnswerJoinResult.IsMySelf = grabAnswerInfo.IsMyself;
            return grabAnswerJoinResult;
        }

        /// <summary>
        /// 学生加入练习
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        public PracticeJoinResult JoinPractice(string practiceId)
        {
            var practiceInfo = _practice.Get(practiceId);
            var practiceJoinInfo = _practice.Join(practiceId);

            return new PracticeJoinResult()
            {
                Type = practiceInfo.Type,
                Answer = practiceJoinInfo.Answer,
                IsSubmitted = practiceJoinInfo.IsSubmitted
            };
        }

        /// <summary>
        ///学生端显示抢答信息
        /// </summary>
        /// <param name="grabAnswerId">抢答标识</param>
        /// <returns></returns>
        public GrabAnswerJoinResult ShowGrabAnswer(string grabAnswerId)
        {
            var grabAnswerJoinResult = new GrabAnswerJoinResult();
            var grabAnswerInfo = _grabAnswer.Get(grabAnswerId);
            if(grabAnswerInfo != null)
            {
                //从配置文件获取倒计时长
                var section = _configSource.GetSection<CustomConfigSection>(CustomConfigSection.DefaultSectionName);
                var countDownMillseconds = section.CountDownMillseconds; 
                var nowTime = _timeProvider.GetNow();
                var duringSeconds =  Convert.ToInt32(nowTime.ToUniversalTime().Subtract(grabAnswerInfo.StartTime.ToUniversalTime()).TotalMilliseconds);              
                if (duringSeconds - countDownMillseconds > 0)
                {
                    grabAnswerJoinResult.DuringMillseconds = duringSeconds - countDownMillseconds;
                    grabAnswerJoinResult.LeftMillseconds = 0;
                }
                else
                {
                    grabAnswerJoinResult.DuringMillseconds = 0;
                    grabAnswerJoinResult.LeftMillseconds = countDownMillseconds - duringSeconds;
                }

                //被抢答
                if (grabAnswerInfo.IsGrabbed)
                {
                    var grabResult = new GrabResult
                    {
                        IsMySelf = grabAnswerInfo.IsMyself
                    };
                    //其他人抢答到
                    if (!grabAnswerInfo.IsMyself)
                    {                        
                        //抢答成功的学生信息
                        var studentInfo = new StudentInfo();
                        var studentInfos = _member.GetStudentInfos(grabAnswerInfo.LessonId, new List<string> { grabAnswerInfo.MemberId });
                        if (studentInfos != null && studentInfos.Count() > 0)
                        { 
                            studentInfo.Name = studentInfos[0].Name;
                            studentInfo.Photo = String.IsNullOrWhiteSpace(studentInfos[0].Portrait) ? section.DefaultStudentPortrait : studentInfos[0].Portrait;
                        }
                        //抢答花费时间
                        DateTime endTime = (DateTime)grabAnswerInfo.GrabbedTime;
                        var grabDuringSeconds = Convert.ToInt32(endTime.ToUniversalTime().Subtract(grabAnswerInfo.StartTime.ToUniversalTime()).TotalSeconds);
                        studentInfo.SpendSeconds = grabDuringSeconds - countDownMillseconds / 1000;
                        grabResult.Winer = studentInfo;
                    }
                    grabAnswerJoinResult.GrabResult = grabResult;
                }
                grabAnswerJoinResult.IsGrabbed = grabAnswerInfo.IsGrabbed;                     
            }
            return grabAnswerJoinResult;
        }

        /// <summary>
        /// 学生提交练习答案
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="answer">答案</param>
        public void SubmitAnswer(string practiceId, string answer)
        {
            var practiceInfo = _practice.Get(practiceId);
            if (practiceInfo.State == PracticeStates.Stopped)
            {
                //练习已停止答题
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { ResultCode = 1001 }).ToJSONString())
                });
            }
            else
            {
                _practice.Submit(practiceId, answer);
            }
        }
    }
}