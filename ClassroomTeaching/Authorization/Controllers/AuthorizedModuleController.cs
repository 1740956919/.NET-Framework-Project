using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Controllers
{
    public class AuthorizedModuleController : ApiController
    {
        private readonly IAuthorizeService _authorizaService = null;

        public AuthorizedModuleController(IAuthorizeService authorizaService)
        {
            _authorizaService = authorizaService;
        }

        [HttpGet]
        public List<AuthorizedModuleInfo> Get()
        {
            return _authorizaService.GetAuthorizedModuleInfos();
        }
    }
}
