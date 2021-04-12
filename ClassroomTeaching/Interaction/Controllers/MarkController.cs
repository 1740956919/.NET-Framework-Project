using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class MarkController : ApiController
    {
        private readonly IMark _mark = null;

        public MarkController(IMark markService)
        {
            _mark = markService;
        }

        /// <summary>
        /// 开始标注
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns>标注标识</returns>
        [HttpPut]
        public object Start(string id)
        {
            var behaviorId = _mark.Start(id);
            return new
            {
                BehaviorId = behaviorId
            };
        }

        /// <summary>
        /// 结束标注
        /// </summary>
        /// <param name="id">标注标识</param>
        [HttpDelete]
        public void Stop(string id)
        {
            _mark.Stop(id);
        }
    }
}
