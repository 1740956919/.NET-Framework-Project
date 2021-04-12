using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface
{
    public interface IAuthorizeService
    {
        /// <summary>
        /// 校验许可证
        /// </summary>
        /// <returns></returns>
        LicenseCheckResult CheckLicense();
        /// <summary>
        /// 获取授权模块
        /// </summary>
        /// <returns></returns>
        List<AuthorizedModuleInfo> GetAuthorizedModuleInfos();
        /// <summary>
        /// 设备登录
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="verifyDeviceIsRegisted">校验设备是否注册</param>
        void DeviceLogon(DeviceLogonParam parameter, bool verifyDeviceIsRegisted);
    }
}
