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
    /// 分组安排控制器
    /// </summary>
    [AvatarAuthorize]
    public class GroupController : ApiController
    {
        private readonly IGroupingService _groupingService = null;

        public GroupController(IGroupingService groupingService)
        {
            _groupingService = groupingService;
        }

        /// <summary>
        /// 开始分组
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        public void Start(String id)
        {
            _groupingService.Start(id);
        }

        /// <summary>
        /// 停止分组
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Stop(String id)
        {
            _groupingService.Stop(id);
        }

        /// <summary>
        /// 获取分组结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public GroupResult Get(String id)
        {
            return _groupingService.GetGroupResult(id);
        }
    }
}
