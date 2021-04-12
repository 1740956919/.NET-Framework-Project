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
    public class TextInputClientController : ApiController
    {
        private readonly IIdeaService _ideaService = null;

        public TextInputClientController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }
      
        /// <summary>
        /// 获取内容输入行为（手机）
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <returns></returns>
        [HttpPut]
        public object GetTextInputBehavior(string id)
        {
            return new { 
                BehaviorId = _ideaService.GetTextInput(id) 
            };
        }

        /// <summary>
        /// 提交内容输入结果（手机）
        /// </summary>
        /// <param name="id">行为标识</param>
        /// <param name="textInputParam"></param>
        [HttpPost]
        public void SubmitContent(string id, [FromBody] TextInputParam textInputParam)
        {
            _ideaService.SubmitTextInout(id, textInputParam);
        }

        /// <summary>
        /// 查询是否需要输入内容（手机）
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <returns></returns>
        [HttpGet]
        public object IsTextInput(string id)
        {
            return new {
                AllowInput = _ideaService.GetInputText(id)
            };

        }
    }
}
