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
    public class TextInputController : ApiController
    {
        private readonly IIdeaService _ideaService = null;

        public TextInputController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        /// <summary>
        /// 创建内容输入行为
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <returns></returns>
        [HttpPut]
        public object CreateTextInputBehavior(string id)
        {
            return new {
                BehaviorId = _ideaService.StartTextInput(id) 
            };
        }

        /// <summary>
        /// 获取内容输入结果
        /// </summary>
        /// <param name="id">记录内容输入的行为标识</param>
        /// <returns></returns>
        [HttpGet]
        public TextInputResult GetTextInputContent(string id)
        {
            return _ideaService.GetTextInputContent(id);
        }

        /// <summary>
        /// 放弃输入
        /// </summary>
        /// <param name="id">记录内容输入的行为标识</param>
        [HttpDelete]
        public void GiveUpInput(string id)
        {
            _ideaService.DeleteTextInput(id);
        }
       
    }
}
