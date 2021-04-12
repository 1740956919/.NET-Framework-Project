using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI
{
    public class ExceptionHelper
    {
        public static string GetCodeByException(ErrorCodeException ex)
        {
            String code = "500";
            switch (ex.ErrorCode)
            {
                case 1003:
                    code = "4004";//互评已结束
                    break;
                case 10002:
                    code = "4005";//无权限加入互评
                    break;
                default:
                    break;
            }
            return code;
        }
    }
}