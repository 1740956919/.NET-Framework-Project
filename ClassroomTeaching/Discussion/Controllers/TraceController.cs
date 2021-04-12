using System;
using System.Net.Http;
using System.Web.Http;

using LINDGE.PARA.Runtime.WebAPI;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Controllers
{
    public class TraceController : ApiController
    {
        /// <summary>
        /// 验证服务模块是否能够正常工作。
        /// 访问地址：<code><![CDATA[http://<serviceuri>/trace]]></code>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Cache(0)]
        public HttpResponseMessage ValidateService()
        {
            return this.Request.ValidateService(ServiceContext.Context.IoC, ValidateScope.All);
        }
    }
}
