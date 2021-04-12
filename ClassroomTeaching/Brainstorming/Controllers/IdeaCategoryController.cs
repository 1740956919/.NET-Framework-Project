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
    public class IdeaCategoryController : ApiController
    {
        private readonly IIdeaService _ideaService = null;

        public IdeaCategoryController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        /// <summary>
        /// 更新点子分类
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <param name="receptor">分类标识</param>       
        [HttpPost]
        public void UpdateIdeaCategory(string id, string receptor, [FromBody] IdeaCategoryUpdateParam categoryIdeaParam)
        {
            _ideaService.UpdateIdeaCategory(id, receptor, categoryIdeaParam.CategoryId);
        }

        /// <summary>
        /// 清除点子分类
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <param name="receptor">点子标识</param>        
        [HttpDelete]
        public void ClearIdeaCategory(string id, string receptor)
        {
            _ideaService.ClearIdeaCategory(id, receptor);
        }
    }
}
