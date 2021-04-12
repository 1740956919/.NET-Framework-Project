using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class PracticeJoinController : ApiController
    {
        private readonly IPracticeService _practiceService = null;

        public PracticeJoinController(IPracticeService practiceService)
        {
            _practiceService = practiceService;
        }

        /// <summary>
        /// 学生加入练习
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <returns></returns>
        [HttpGet]
        public PracticeJoinResult JoinPractice(string id)
        {
            return _practiceService.JoinPractice(id);
        }

        /// <summary>
        /// 提交练习答案
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <param name="answer">答案</param>
        [HttpPut]
        public void SubmitAnswer(string id, [FromBody] AnswerSubmitParam submitParam)
        {
            _practiceService.SubmitAnswer(id, submitParam.Answer);
        }
    }
}
