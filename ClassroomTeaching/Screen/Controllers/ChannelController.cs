using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class ChannelController : ApiController
    {
        private readonly IReceiveChannelService _receiveChannelService = null;

        public ChannelController(IReceiveChannelService receiveChannelService)
        {
            _receiveChannelService = receiveChannelService;
        }

        /// <summary>
        /// 检查画面是否包含数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ChannelInfo> ProofChannelData([FromBody]ChannelProofParam parameter)
        {
            return _receiveChannelService.ProofChannelData(parameter);
        }
    }
}
