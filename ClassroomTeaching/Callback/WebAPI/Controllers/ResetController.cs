using System;
using System.Collections.Generic;
using System.Web.Http;

using LINDGE.PARA;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Controllers
{
    /// <summary>
    /// 清除配置缓存的控制器。
    /// </summary>
    public class ResetController : ApiController
    {
        private IEnumerable<ICacheControl> buffers = null;

        public ResetController(IEnumerable<ICacheControl> buffers)
        {
            this.buffers = buffers;
        }

        /// <summary>
        /// 执行注册在运行时上下文中所有ICacheControl接口的Clear方法。
        /// </summary>
        [HttpGet]
        public void Clear()
        {
            foreach (var bufferControl in this.buffers)
            {
                bufferControl.Clear();
            }
        }
    }
}