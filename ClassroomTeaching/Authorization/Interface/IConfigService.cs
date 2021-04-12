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
    /// <summary>
    /// 配置服务
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// 获取服务端配置
        /// </summary>
        /// <returns></returns>
        ServiceConfigInfo GetServiceConfig();
        /// <summary>
        /// 设置服务端配置
        /// </summary>
        /// <param name="parameter"></param>
        void SetServiceConfig(ServiceConfigSetParam parameter);
        /// <summary>
        /// 获取终端注册结果
        /// </summary>
        /// <returns></returns>
        DeviceRegisteResult GetDeviceRegisteResult();
        /// <summary>
        /// 注册终端
        /// </summary>
        /// <param name="deviceRegistedInfo"></param>
        /// <returns></returns>
        string RegistDevice(DeviceRegisteInfo deviceRegistedInfo);
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
    }
}
