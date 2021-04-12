using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    /// <summary>
    /// 课堂表现打分控制器
    /// </summary>
    [AvatarAuthorize]
    public class PerformanceScoreController : ApiController
    {
        private readonly IPerformanceService _performanceService = null;

        public PerformanceScoreController(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
        }


        /// <summary>
        /// 开始评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        [HttpPut]
        public PerformanceScoreStartResult StartScore(String id, [FromBody] ScoreStartParam parameter)
        {
            return _performanceService.StartScore(id, parameter);
        }

        /// <summary>
        /// 完成评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        [HttpPost]
        public void CompleteScore(String id, [FromBody]ScoreCompleteParam parameter)
        {
            _performanceService.CompleteScore(id,parameter);
        }
    }
}
