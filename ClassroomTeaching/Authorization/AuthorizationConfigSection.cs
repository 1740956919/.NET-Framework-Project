using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class AuthorizationConfigSection
    {
        /// <summary>
        /// 配置文件默认名称
        /// </summary>
        public const String DefaultSectionName = "Translayer.ClassroomTeaching.Authorization";

        /// <summary>
        /// 应用模块列表
        /// </summary>
        public List<ModuleInfo> Modules { get; set; }
        /// <summary>
        /// 设备配置信息列表
        /// </summary>
        public List<DeviceConfigInfo> DeviceConfigInfos { get; set; }
        /// <summary>
        /// EnvironmentVariable.XML文件位置 | 绝对路径
        /// </summary>
        public string EnvironmentVariableFilePath { get; set; }
        /// <summary>
        /// MainSite服务的Websites.xml配置文件路径 | 绝对路径
        /// </summary>
        public string MainSiteWebsitesFilePath { get; set; }
        /// <summary>
        /// Platform服务的Websites.xml配置文件路径 | 绝对路径
        /// </summary>
        public string PlatformWebsitesFilePath { get; set; }
        /// <summary>
        /// 支持的手机投屏数量
        /// </summary>
        public int PhoneScreenCount { get; set; }
    }

    public class DeviceConfigInfo
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LogonName { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 由常量Const.DeviceType定义
        /// </summary>
        public string Type { get; set; }

        public DeviceInfo ToDeviceInfo()
        {
            return new DeviceInfo()
            {
                Enabled = false,
                IsRegisted = false,
                Name = this.DisplayName,
                Number = this.Number,
                RegistedIPAddress = string.Empty,
                Type = this.Type
            };
        }
    }
}