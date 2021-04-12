using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using LINDGE.Serialization;


namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Service
{
    public class PracticeService : IPracticeService
    {
        private readonly IMember _member = null;
        private readonly IPractice _practice = null;
        private readonly IEvaluative _evaluative = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly IConfigSource _configSource = null;

        public PracticeService(
            IMember memberService, 
            IPractice practiceService, 
            IEvaluative evaluativeService,
            IConfigSource configSourceService,
            ITimeProvider timeProviderService)
        {
            _member = memberService;
            _practice = practiceService;
            _evaluative = evaluativeService;
            _timeProvider = timeProviderService;
            _configSource = configSourceService;
        }

        /// <summary>
        /// 结束练习
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="sceneId">课时标识</param>
        public void CompletePractice(string practiceId, string lessonId)
        {
            //互评信息
            var mutualEvaluateInfos = _evaluative.GetEvaluativeInfoByLesson(lessonId);
            var mutualEvaluateInfo = mutualEvaluateInfos.Where(m => m.RelationId == practiceId).FirstOrDefault();
            if (mutualEvaluateInfo != null)
            {
                _evaluative.CompleteMutualEvaluate(mutualEvaluateInfo.MutualEvaluateId);
            }
            //结束练习
            _practice.Complete(practiceId);
        }

        /// <summary>
        /// 创建练习
        /// </summary>
        /// <param name="parameter">练习创建参数</param>
        /// <returns></returns>
        public string CreatePractice(PracticeCreateParam parameter)
        {
            string practiceId;
            try
            {
                practiceId = _practice.Create(parameter.LessonId, parameter.Type, parameter.Title);
            }
            catch (ErrorCodeException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ex.ErrorCode, Data = ex.Message }).ToJSONString())
                });
            }

            return practiceId;
        }

        /// <summary>
        /// 获取练习简报信息
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public List<PracticeBriefResult> GetPracticeBrief(string practiceId)
        {
            var practiceBriefResult = new List<PracticeBriefResult>();
            var practiceAnswerInfo = _practice.GetSutdentAnswer(practiceId);
           
            if(practiceAnswerInfo != null && practiceAnswerInfo.Answers.Count > 0)
            {
                var answers = new List<String>();
                if (practiceAnswerInfo.Type == TaskType.MultipleChoice)
                {
                    answers = practiceAnswerInfo.Answers.SelectMany(a => a.Answer.Split(',')).ToList();
                }
                else
                {
                    answers = practiceAnswerInfo.Answers.Select(a => a.Answer).ToList();
                }

                practiceBriefResult = answers.GroupBy(a => a).Select(g => new PracticeBriefResult()
                {
                    Answer = g.Key,
                    Count = g.Count()
                }).ToList();
            }

            return practiceBriefResult;
        }

        /// <summary>
        /// 获取练习详情信息
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        public PracticeDetailResult GetPracticeDetail(string practiceId, string lessonId)
        {
            var result = new PracticeDetailResult();

            //互评信息
            var mutualEvaluateInfos = _evaluative.GetEvaluativeInfoByLesson(lessonId);
            var mutualEvaluateInfo = mutualEvaluateInfos.Where(m => m.RelationId == practiceId).FirstOrDefault();
            if (mutualEvaluateInfo != null)
            {
                result.IsExistMutualEvaluative = true;
                result.MutualEvaluateId = mutualEvaluateInfo.MutualEvaluateId;
                result.StudentTargetId = mutualEvaluateInfo.EvaluatedTarget != null ? mutualEvaluateInfo.EvaluatedTarget.TargetId : String.Empty;
            }
            var practiceAnswerInfo = _practice.GetSutdentAnswer(practiceId);
            //答题的学生id
            var studentIds = practiceAnswerInfo.Answers.Select(p => p.MemberId).ToList();

            var practiceDetailInfos = new List<PracticeStudentInfo>();
            if (studentIds.Count()>0)
            {
                var studentInfos = _member.GetStudentInfos(lessonId, studentIds);
                var studentMap = studentInfos.ToDictionary(s => s.MemberId);
                var section = _configSource.GetSection<QuizConfigSection>(QuizConfigSection.DefaultSectionName);
                result.PracticeStudentInfos = practiceAnswerInfo.Answers.Select(p => new PracticeStudentInfo()
                {
                    StudentId = studentMap.ContainsKey(p.MemberId) ? studentMap[p.MemberId].MemberId : String.Empty,
                    Name = studentMap.ContainsKey(p.MemberId) ? studentMap[p.MemberId].Name : String.Empty,
                    Photo = String.IsNullOrWhiteSpace(studentMap[p.MemberId].Portrait) ? section.DefaultStudentPortrait : studentMap[p.MemberId].Portrait,                  
                    Answer = p.Answer,
                    IsScore = p.IsScored,
                    Score = p.Score
                }).ToList();
            }
            return result;
        }

        /// <summary>
        /// 获取练习进度结果
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public PracticeReviewResult GetPracticeInfo(string practiceId)
        {
            var practiceReviewResult = new PracticeReviewResult();
            var practiceInfo = _practice.Get(practiceId);
            var memberStaticInfo = _member.GetStatisticsInfo(practiceInfo.LessonId);

            //练习汇总信息
            var practiceSummaryInfo = new PracticeInfo();                    
            if (practiceInfo.StopTime != null)
            {
                var stopTime = (DateTime)practiceInfo.StopTime;
                practiceSummaryInfo.DuringSeconds = Convert.ToInt32(stopTime.ToUniversalTime().Subtract(practiceInfo.StartTime.ToUniversalTime()).TotalSeconds);
            }
            var practiceStatisticInfo = _practice.Statistics(practiceId);
            practiceSummaryInfo.TotalCount = memberStaticInfo.JoinedCount;
            practiceSummaryInfo.ReplyCount = practiceStatisticInfo.SubmittedCount;
            practiceReviewResult.PracticeSummaryInfo = practiceSummaryInfo;

            
            practiceReviewResult.IsExistBrief = IsExistBrief(practiceInfo.Type);
            return practiceReviewResult;
        }

        /// <summary>
        /// 检查题型是否存在简报
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsExistBrief(string type)
        {
            var isExistBrief = false;
            if(type == TaskType.SingleChoice || type == TaskType.MultipleChoice || type == TaskType.SupplyBlank)
            {
                isExistBrief = true;
            }
            return isExistBrief;
        }

        /// <summary>
        /// 获取练习进度信息
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public PracticeProgressResult GetPracticeProgress(string practiceId)
        {
            var practiceProgressResult = new PracticeProgressResult();
            //练习信息
            var practiceInfo = _practice.Get(practiceId);
            var nowTime = _timeProvider.GetNow();
            var duringSeconds = Convert.ToInt32((nowTime.ToUniversalTime().Subtract(practiceInfo.StartTime.ToUniversalTime()).TotalSeconds));
            practiceProgressResult.DuringSeconds = duringSeconds;

            //学生信息
            var studentInfos = _member.GetAllStudentInfos(practiceInfo.LessonId);
            var students = studentInfos.Where(s => s.IsLogon).ToList();

            if (students.Count > 0)
            {
                var studentIds = students.Select(s => s.MemberId).ToList();
                var studentMap = students.ToDictionary(s => s.MemberId, s => s.Name);
                //学生答题信息
                var answerInfos = _practice.GetMemberPracticeStateInfo(practiceId, studentIds);
                //未答题的学生
                var waitAnswerStudentIds = answerInfos.Where(a => !a.IsSubmitted).Select(i => i.MemberId).ToList();
                var waitAnswerStudents = studentMap.Where(s => waitAnswerStudentIds.Contains(s.Key)).Select(s => s.Value).ToList();
                if (waitAnswerStudents.Count() > 5)
                {
                    waitAnswerStudents = waitAnswerStudents.GetRange(0, 5);
                }

                practiceProgressResult.TotalCount = students.Count;
                practiceProgressResult.ReplyCount = answerInfos.Where(a => a.IsJoined && a.IsSubmitted).Count();            
                practiceProgressResult.WaitAnswerStudents = waitAnswerStudents;             
            }

            return practiceProgressResult;
        }

        /// <summary>
        /// 获取练习状态
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public string GetPracticeState(string practiceId)
        {   
            var practiceInfo = _practice.Get(practiceId);
            return practiceInfo.State == PracticeStates.Progressing ? PracticeState.UnderWay : PracticeState.Complete;
        }

        /// <summary>
        /// 给学生打分
        /// </summary>
        /// <param name="practiceId"></param>
        /// <param name="studentId"></param>
        /// <param name="Score"></param>
        public void GradeStudent(string practiceId, string studentId, float Score)
        {
            _practice.Score(new PracticeScoreParam() { 
                PracticeId = practiceId,
                MemberId = studentId,
                Score = Score
            });
        }


        /// <summary>
        /// 停止练习
        /// </summary>
        /// <param name="practiceId"></param>
        public void StopPractice(string practiceId)
        {
            _practice.Stop(practiceId);
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
        /// 学生提交练习答案
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="answer">答案</param>
        public void SubmitAnswer(string practiceId, string answer)
        {
            var practiceInfo = _practice.Get(practiceId);
            if (practiceInfo.State == PracticeStates.Stopped)
            {
                // 练习已停止答题
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