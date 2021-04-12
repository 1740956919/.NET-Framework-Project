using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using LINDGE.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    /// <summary>
    /// 互评控制器
    /// </summary>
    [AvatarAuthorize]
    public class MutualScoreController : ApiController
    {
        private readonly IMutualScoreService _mutualScoreService = null;

        public MutualScoreController(IMutualScoreService mutualScoreService)
        {
            _mutualScoreService = mutualScoreService;
        }

        /// <summary>
        /// 开始学生互评
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        public MutualScoreStartResult Start([FromBody]MutualScoreStartParam parameter)
        {
            var result = _mutualScoreService.Start(parameter);
            if (result.IsSuccess)
            {
                return result;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = result.ErrorCode, Data = result.ErrorData }).ToJSONString())
                });
            }
        }

        /// <summary>
        /// 结束学生互评
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Stop(String id)
        {
            _mutualScoreService.Stop(id);
        }

        /// <summary>
        /// 获取互评信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpGet]
        public StudentMutualScoreInfo GetMutualScoreInfo(String id)
        {
            return _mutualScoreService.GetMutualScoreInfo(id);
        }
    }
}
