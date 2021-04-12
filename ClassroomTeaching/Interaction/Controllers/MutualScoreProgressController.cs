using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    /// <summary>
    /// 互评进度控制器
    /// </summary>
    [AvatarAuthorize]
    public class MutualScoreProgressController : ApiController
    {
        private readonly IMutualScoreService _mutualScoreService = null;

        public MutualScoreProgressController(IMutualScoreService mutualScoreService)
        {
            _mutualScoreService = mutualScoreService;
        }

        /// <summary>
        /// 获取互评进度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public MutualScoreProgress GetProgress(String id)
        {
            return _mutualScoreService.GetProgress(id);
        }
    }
}
