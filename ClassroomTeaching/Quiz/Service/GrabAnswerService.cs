using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using LINDGE.Serialization;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Service
{
    public class GrabAnswerService : IGrabAnswerService
    {
        private readonly IMember _member = null;
        private readonly IEvaluative _evaluative = null;
        private readonly IGrabAnswer _grabAnswer = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly IConfigSource _configSource = null;

        public GrabAnswerService(
            IMember memberService, 
            IGrabAnswer grabAnswerService, 
            IEvaluative evaluativeService,
            IConfigSource configSourceService,
            ITimeProvider timeProviderService)
        {
            _member = memberService;
            _evaluative = evaluativeService;
            _grabAnswer = grabAnswerService;
            _configSource = configSourceService;
            _timeProvider = timeProviderService;
        }

        /// <summary>
        /// 结束抢答
        /// </summary>
        /// <param name="grabAnswerId">抢答标识</param>
        /// <param name="sceneId">场景标识</param>
        public void CompleteGrabAnswer(string grabAnswerId, string lessonId)
        {
            //互评信息
            var mutualEvaluateInfos = _evaluative.GetEvaluativeInfoByLesson(lessonId);
            var mutualEvaluateInfo = mutualEvaluateInfos.Where(m => m.RelationId == grabAnswerId).FirstOrDefault();
            if (mutualEvaluateInfo != null)
            {             
                _evaluative.CompleteMutualEvaluate(mutualEvaluateInfo.MutualEvaluateId);
            }
            //结束抢答
            _grabAnswer.Complete(grabAnswerId);
        }

        /// <summary>
        /// 创建抢答
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns>抢答标识</returns>
        public string CreateGrabAnswer(string sceneId)
        {
            string grabAnserId;
            try
            {
                grabAnserId = _grabAnswer.Create(sceneId);
            }
            catch (ErrorCodeException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ex.ErrorCode, Data = ex.Message }).ToJSONString())
                });
            }
            return grabAnserId;
        }

        /// <summary>
        /// 获取抢答信息
        /// </summary>
        /// <param name="grabAnswerId">抢答流程标识</param>
        /// <returns></returns>
        public GrabAnswerResult GetGrabAnswer(string grabAnswerId)
        {
            var grabAnswerResult = new GrabAnswerResult();
            var grabAnswerInfo = _grabAnswer.Get(grabAnswerId);              

            //有人抢答到
            if (grabAnswerInfo.IsGrabbed)
            {
                //抢答成功的学生信息
                var studentInfo = new StudentInfo();
                var studentInfos = _member.GetStudentInfos(grabAnswerInfo.LessonId, new List<string> { grabAnswerInfo.MemberId });
                var section = _configSource.GetSection<QuizConfigSection>(QuizConfigSection.DefaultSectionName);
                if (studentInfos.Count() > 0)
                {
                    studentInfo.StudentId = studentInfos[0].MemberId;
                    studentInfo.Name = studentInfos[0].Name;
                    studentInfo.Photo = String.IsNullOrWhiteSpace(studentInfos[0].Portrait) ? section.DefaultStudentPortrait : studentInfos[0].Portrait;
                    grabAnswerResult.Student = studentInfo;
                }
                //互评信息
                var mutualEvaluateInfos = _evaluative.GetEvaluativeInfoByLesson(grabAnswerInfo.LessonId);    
                if(mutualEvaluateInfos.Count > 0)
                {
                    var mutualEvaluateInfo = mutualEvaluateInfos.Where(m => m.RelationId == grabAnswerId).FirstOrDefault();
                    grabAnswerResult.IsExistMutualEvaluative = mutualEvaluateInfo != null;
                    grabAnswerResult.MutualEvaluateId = grabAnswerResult.IsExistMutualEvaluative ? mutualEvaluateInfo.MutualEvaluateId : string.Empty;
                }
            }
            else
            {
                grabAnswerResult.IsGrabbing = true;
                var nowTime = _timeProvider.GetNow();
                var duringSeconds = Convert.ToInt32((nowTime.ToUniversalTime().Subtract(grabAnswerInfo.StartTime.ToUniversalTime())).TotalMilliseconds);
                var section = _configSource.GetSection<QuizConfigSection>(QuizConfigSection.DefaultSectionName);
                var leftSeconds = section.LeftMillSeconds;
                if (duringSeconds - leftSeconds > 0)
                {
                    grabAnswerResult.DuringSeconds = duringSeconds - leftSeconds;
                    grabAnswerResult.LeftSeconds = 0;
                }
                else
                {
                    grabAnswerResult.DuringSeconds = 0;
                    grabAnswerResult.LeftSeconds = leftSeconds - duringSeconds;
                }              
            }
            return grabAnswerResult;
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
            if (grabAnswerInfo != null)
            {
                //从配置文件获取倒计时长
                var section = _configSource.GetSection<QuizConfigSection>(QuizConfigSection.DefaultSectionName);
                var countDownMillseconds = section.CountDownMillseconds;
                var nowTime = _timeProvider.GetNow();
                var duringSeconds = Convert.ToInt32(nowTime.ToUniversalTime().Subtract(grabAnswerInfo.StartTime.ToUniversalTime()).TotalMilliseconds);
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
                        var studentInfo = new WinnnerInfo();
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
                var section = _configSource.GetSection<QuizConfigSection>(QuizConfigSection.DefaultSectionName);
                var countDownMillseconds = section.CountDownMillseconds;
                //抢答成功的学生信息
                var studentInfo = new WinnnerInfo();
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
    }
}