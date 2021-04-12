using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Controllers
{
    public class PracticeController : ApiController
    {
        private readonly IPracticeService _practiceService = null;

        public PracticeController(IPracticeService practiceService)
        {
            _practiceService = practiceService;
        }

        [AvatarAuthorize]
        /// <summary>
        /// 创建练习
        /// </summary>
        /// <param name="parameter">练习创建参数</param>
        /// <returns></returns>
        [HttpPut]
        public object CreatePractice([FromBody]PracticeCreateParam parameter)
        {
            return new 
            {
                PracticeId = _practiceService.CreatePractice(parameter)
            };
        }

        /// <summary>
        /// 停止练习
        /// </summary>
        /// <param name="id">练习标识</param>
        [HttpDelete]
        public void StopPractice(string id)
        {
            _practiceService.StopPractice(id);
        }

        /// <summary>
        /// 查询练习状态
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <returns></returns>
        [HttpGet]
        public object GetPracticeState(string id)
        {
            return new
            {
                PracticeState = _practiceService.GetPracticeState(id)
            };
        }
    }
}
