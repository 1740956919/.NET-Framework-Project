using LINDGE.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Const
{
    public class ResultCodes
    {
        /// <summary>
        /// 正确
        /// </summary>
        public const Int32 NoError = 0;

        public static String GetError(Int32 errorCode)
        {
            var error = "未知错误";
            switch (errorCode)
            {
                case ResultCodes.NoError:
                    error = String.Empty;
                    break;
                case SecurityException.InvalidKey:
                    error = "无效的密钥。";
                    break;
                case SecurityException.InvalidLicenseFile:
                    error = "无效的许可证文件。";
                    break;
                case SecurityException.DeviceNotMatched:
                    error = "加密锁和许可证文件不匹配。";
                    break;
                case SecurityException.IllegalDevice:
                    error = "无效的加密锁设备。";
                    break;
                case SecurityException.DeviceNotFound:
                    error = "没有找到加密锁设备。";
                    break;
                case SecurityException.NoProductAuth:
                    error = "没有找到产品授权信息，或者申请许可的用户数不足。";
                    break;
                case SecurityException.AuthExpired:
                    error = "许可证文件已过期。";
                    break;
                case SecurityException.TimeTampered:
                    error = "系统时间被篡改。";
                    break;
                case SecurityException.NoFeatureAuth:
                    error = "没有找到指定特性的授权信息。";
                    break;
                case SecurityException.NotLoadLibrary:
                    error = "支持库加载失败。";
                    break;
                case SecurityException.AccessDeviceFailed:
                    error = "访问设备失败，或者设备未找到。";
                    break;
                case SecurityException.OperateDeviceFailed:
                    error = "操作设备失败。";
                    break;
                case SecurityException.ReadDataFailed:
                    error = "无法从设备中读取设备。";
                    break;
                case SecurityException.WriteDataFailed:
                    error = "无法向设备中写入数据。";
                    break;
            }

            return error;
        }
    }
}