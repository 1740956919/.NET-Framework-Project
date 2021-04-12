using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    public class CookieSetController : ApiController
    {
        [HttpPut]
        public void SetCookie(String id)
        {
            AvatarAuthContextExtensions.RecordAuthentication(id);
        }
    }
}
