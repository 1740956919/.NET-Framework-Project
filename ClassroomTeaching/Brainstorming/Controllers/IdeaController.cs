using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class IdeaController : ApiController
    {
        private readonly IIdeaService _ideaService = null;

        public IdeaController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        /// <summary>
        /// 提交点子（手机）
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <param name="ideaContent">点子内容</param>
        [HttpPut]
        public void SubmitIdea(string id, [FromBody] IdeaParam ideaParam)
        {
            _ideaService.SubmitIdea(id, ideaParam.IdeaContent);
        }

        /// <summary>
        /// 删除点子
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <param name="receptor">点子标识</param>
        [HttpDelete]
        public void DeleteIdea(string id, string receptor)
        {
            _ideaService.DeleteIdea(id, receptor);
        }
    }
}
