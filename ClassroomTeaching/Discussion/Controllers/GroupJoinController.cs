using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Controllers
{
    /// <summary>
    /// 学生加入分组控制器(手机)
    /// </summary>
    [AvatarAuthorize]
    public class GroupJoinController : ApiController
    {
        private readonly IGroupingService _groupingService = null;

        public GroupJoinController(IGroupingService groupingService)
        {
            _groupingService = groupingService;
        }

        /// <summary>
        /// 获取所有可选择的分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public OptionalGroupInfo Get(String id)
        {
            return _groupingService.GetOptionalGroupInfo(id);
        }

        /// <summary>
        /// 选择小组加入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receptor"></param>
        [HttpPost]
        public void Join(String id, String receptor)
        {
            _groupingService.Join(id, receptor);
        }
    }
}
