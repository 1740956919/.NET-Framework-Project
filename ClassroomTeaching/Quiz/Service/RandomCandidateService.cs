using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using LINDGE.Serialization;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Service
{
    public class RandomCandidateService : IRandomCandidateService
    {
        private readonly IMember _member = null;
        private readonly IRollCall _rollCall = null;
        private readonly IEvaluative _evaluative = null;
        private readonly IConfigSource _configSource = null;

        public RandomCandidateService(
            IMember memberService, 
            IRollCall rollCallService,
            IConfigSource configSourceService,
            IEvaluative evaluativeService)
        {
            _member = memberService;
            _rollCall = rollCallService;
            _evaluative = evaluativeService;
            _configSource = configSourceService;
        }

        /// <summary>
        /// 结束随机选人
        /// </summary>
        /// <param name="randomRollCallId">随机选人标识</param>
        /// <param name="sceneId">场景标识</param>
        public void CompleteRandomCandidate(string randomRollCallId, string lessonId)
        {
            //互评信息
            var mutualEvaluateInfos = _evaluative.GetEvaluativeInfoByLesson(lessonId);
            var mutualEvaluateInfo = mutualEvaluateInfos.Where(m => m.RelationId == randomRollCallId).FirstOrDefault();
            if (mutualEvaluateInfo != null)
            {
                _evaluative.CompleteMutualEvaluate(mutualEvaluateInfo.MutualEvaluateId);
            }
            //结束随机选人
            _rollCall.CompleteRandomRollCall(randomRollCallId);
        }

        /// <summary>
        /// 创建随机选人
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        public string CreateRandomCandidate(string sceneId)
        {
            var randomRollCallInfo = _rollCall.CreateRandomRollCall(sceneId);
            return randomRollCallInfo != null ? randomRollCallInfo.RandomRollCallId : string.Empty;
        }

        /// <summary>
        /// 获取所有登录的学生
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        public List<StudentInfo> GetAllStudentsBySceneId(string sceneId)
        {
            var members = _member.GetAllStudentInfos(sceneId);
            var section = _configSource.GetSection<QuizConfigSection>(QuizConfigSection.DefaultSectionName);
            return members.Where(m => m.IsLogon).Select(s => new StudentInfo()
            {
                StudentId = s.MemberId,
                Name = s.Name,
                Photo = String.IsNullOrWhiteSpace(s.Portrait) ? section.DefaultStudentPortrait : s.Portrait
            }).ToList();          
        }

        /// <summary>
        /// 获取随机选人信息
        /// </summary>
        /// <param name="randomRollCallId">随机标识</param>
        /// <returns></returns>
        public RandomCandidateResult GetRandomCandidate(string randomRollCallId)
        {
            var result = new RandomCandidateResult();
            var randomRollCallInfo = _rollCall.GetRandomRollCallInfo(randomRollCallId);
            if(randomRollCallInfo == null) 
            {
                //班级中不存在学生
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { ResultCode = 4001 }).ToJSONString())
                });
            }

            //互评信息
            var mutualEvaluateInfos = _evaluative.GetEvaluativeInfoByLesson(randomRollCallInfo.LessonId);
            if(mutualEvaluateInfos.Count > 0)
            {
                var mutualEvaluateInfo = mutualEvaluateInfos.Where(m => m.RelationId == randomRollCallId).FirstOrDefault();
                result.IsExistMutualEvaluative = mutualEvaluateInfo != null;
                result.MutualEvaluateId = mutualEvaluateInfo != null ? mutualEvaluateInfo.MutualEvaluateId : string.Empty;
            }
        
            //随机选中的学生信息
            var studentInfo = new StudentInfo();
            var studentInfos = _member.GetStudentInfos(randomRollCallInfo.LessonId, new List<string> { randomRollCallInfo.MemberId });
            var section = _configSource.GetSection<QuizConfigSection>(QuizConfigSection.DefaultSectionName);
            if (studentInfos != null && studentInfos.Count() > 0)
            {
                studentInfo.StudentId = studentInfos[0].MemberId;
                studentInfo.Name = studentInfos[0].Name;             
                studentInfo.Photo = String.IsNullOrWhiteSpace(studentInfos[0].Portrait) ? section.DefaultStudentPortrait : studentInfos[0].Portrait;
                result.Student = studentInfo;
            }
            return result;
        }
    }
}