using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class CategoryController : ApiController
    {
        private readonly IIdeaService _ideaService = null;

        public CategoryController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryCreateParam"></param>
        /// <returns></returns>
        [HttpPut]
        public CategoryCreateResult CreateCategory(string id, [FromBody] CategoryCreateParam categoryCreateParam)
        {
            return _ideaService.CreateCategory(id, categoryCreateParam);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <param name="receptor">分类标识</param>
        [HttpDelete]
        public void DeleteCategory(string id, string receptor)
        {
            _ideaService.DeleteCategory(id, receptor);
        }

        /// <summary>
        /// 修改分类名称
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <param name="receptor">分类标识</param>
        /// <param name="categoryCreateParam">分类名称</param>
        [HttpPost]
       public void SaveCategoryName(string id, string receptor, [FromBody] CategoryCreateParam categoryCreateParam)
       {
            _ideaService.RenameCategory(id, receptor, categoryCreateParam.CategoryName);
       }

    }
}
