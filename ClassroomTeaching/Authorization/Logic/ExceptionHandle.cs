using LINDGE.PARA.Generic.Sociality.User;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Const;
using LINDGE.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Logic
{
    public class ExceptionHandle
    {
        public static String GetCodeByException(Exception ex)
        {
            String code;
            if (ex is ProxyException)
            {
                ProxyException proxyErr = ex as ProxyException;
                switch (proxyErr.ErrorCode)
                {
                    case 0x11101:
                        code = ErrorCode.PasswordError;//登录密码错误
                        break;
                    case 0x11108:
                        code = ErrorCode.LogonAlreadyForbidden;//登录已禁用
                        break;
                    case 0x11103:
                        code = ErrorCode.MultipointLogonForbidden;   // 多点登陆禁用
                        break;
                    case (Int32)ServiceErrorCode.NotFoundProfile:
                        code = ErrorCode.NotFoundProfile;//用户名不存在
                        break;
                    case (Int32)ServiceErrorCode.NotFoundPassword:
                        code = ErrorCode.NotFoundPassword;//密码不存在
                        break;
                    case (Int32)ServiceErrorCode.AccountLocked:
                        code = ErrorCode.AccountLocked;//账号已锁定
                        break;
                    case (Int32)ServiceErrorCode.NoLicense:
                        code = ErrorCode.NotFoundLicense;//没有找到许可证信息
                        break;
                    case (Int32)ServiceErrorCode.ReadLicenseFailed:
                        code = ErrorCode.NotReadLicense;//读取许可证失败
                        break;
                    case (Int32)ServiceErrorCode.CheckLicenseFailed:
                        code = ErrorCode.NotCheckLicense;//检查许可证失败
                        break;
                    case (Int32)ServiceErrorCode.NoSecuritySection:
                        code = ErrorCode.NoSecuritySection;//没有找到安全配置
                        break;
                    case (Int32)ServiceErrorCode.InvalidSecuritySection:
                        code = ErrorCode.InvalidSecuritySection;//安全配置不合法
                        break;
                    case (Int32)ServiceErrorCode.NoLicenseServer:
                        code = ErrorCode.NoLicenseServer;//没有找到许可证服务
                        break;
                    default:
                        code = ErrorCode.ServerError;
                        break;
                }
            }
            else
            {
                code = ErrorCode.ServerError;
            }
            return code;
        }
    }
}