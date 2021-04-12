using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Generic.Sociality.User.API.Logon;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Storage.LightingTable;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Proxy.WebAPIClient;
using LINDGE.Serialization;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Service
{
    public class ConfigService : IConfigService
    {
        private readonly ITeachingScene _teachingScene = null;
        private readonly IEnvironment _environment = null;
        private readonly IConfigSource _configSource = null;
        private readonly IPasswordLogon _passwordLogon = null;

        public ConfigService(ITeachingScene teachingSceneService,
            IEnvironment environmentService,
            IConfigSource configSourceService,
            IProxy<IPasswordLogon> passwordLogonProxy)
        {
            _teachingScene = teachingSceneService;
            _environment = environmentService;
            _configSource = configSourceService;
            _passwordLogon = passwordLogonProxy.GetObject();
        }

        public DeviceRegisteResult GetDeviceRegisteResult()
        {
            var sourceIP = HttpRequestExtension.GetRemoteAddress();

            var deviceInfos = _environment.GetAllDeviceInfos();
            var result = new DeviceRegisteResult()
            {
                Devices = deviceInfos.Select(d => d.ToDeviceInfo()).OrderBy(d => d.Name).ToList()
            };
            var myRegistedInfo = deviceInfos.FirstOrDefault(d => d.RegistedIPAddress == sourceIP);
            if (myRegistedInfo != null)
            {
                result.IsRegisted = true;
                result.RegistedInfo = new DeviceRegisteInfo()
                {
                    Number = myRegistedInfo.Number,
                    RegistedIPAddress = myRegistedInfo.RegistedIPAddress
                };
            }
            return result;
        }

        public ServiceConfigInfo GetServiceConfig()
        {
            var environmentVariableTable = _configSource.GetSection<EnvironmentVariableTable>(EnvironmentVariableTable.DefaultSectionName);
            var ip = environmentVariableTable["FrontProxyServer"];

            var serviceConfig = _environment.GetServiceConfig();
            var serviceConfigInfo = new ServiceConfigInfo()
            {
                ServerIPAddress = ip,
                LicenseFileName = serviceConfig.LicenseFileName,
                WifiPassword = serviceConfig.WifiPassword,
                WifiSSID = serviceConfig.WifiSSID
            };

            var deviceInfos = _environment.GetAllDeviceInfos();
            var teacherAccount = deviceInfos.First(d => d.Type == DeviceTypes.TeacherScreen).Id;
            var logonResult = _passwordLogon.Verify(new PasswordLogonParameter()
            {
                IP = HttpRequestExtension.GetRemoteAddress(),
                LogonName = teacherAccount,
                Password = teacherAccount
            });
            AvatarAuthContextExtensions.RecordAuthentication(logonResult.Token);
            serviceConfigInfo.Devices = deviceInfos.Select(d => d.ToDeviceInfo()).OrderBy(d => d.Name).ToList();
            return serviceConfigInfo;
        }

        public void Init()
        {
            var configSource = _configSource.GetSection<AuthorizationConfigSection>(AuthorizationConfigSection.DefaultSectionName);
            var deviceConfigInfos = configSource?.DeviceConfigInfos ?? new List<DeviceConfigInfo>();
            if (deviceConfigInfos.Count > 0)
            {
                _environment.InitDevice(deviceConfigInfos.Select(d => new Generic.ClassroomTeaching.Scene.Struct.DeviceInfo()
                {
                    Id = d.LogonName,
                    Name = d.DisplayName,
                    Number = d.Number,
                    Type = DeviceType.ConvertToSceneDeviceType(d.Type),
                    RegistedIPAddress = string.Empty
                }).ToList(), configSource.PhoneScreenCount);
            }
            // 清空服务端配置
            _environment.ClearServiceConfig();

            var teachingSceneInfos = _teachingScene.Query();
            if (teachingSceneInfos.Count > 0)
            {
                // 删除以前的教学场景
                _teachingScene.Delete(teachingSceneInfos.Select(t => t.SceneId).ToList());
            }
            // 创建教学场景
            _teachingScene.Create(new List<string>() { "教学场景" });
        }

        public string RegistDevice(DeviceRegisteInfo deviceRegistedInfo)
        {
            var result = string.Empty;
            try
            {
                result = _environment.RegisteDevice(deviceRegistedInfo.Number, deviceRegistedInfo.RegistedIPAddress);
            }
            catch (ErrorCodeException ex)
            {
                if (ex.ErrorCode == ErrorCodes.RegistedDevice)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent((new { Code = 7001 }).ToJSONString())
                    });
                }
                if (ex.ErrorCode == ErrorCodes.UnenabledDevice)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent((new { Code = 7002 }).ToJSONString())
                    });
                }
            }
            return result;
        }

        public void SetServiceConfig(ServiceConfigSetParam parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter.ServerIPAddress))
            {
                var serverIPAddress = parameter.ServerIPAddress;
                #region 更新ip地址
                var configSource = _configSource.GetSection<AuthorizationConfigSection>(AuthorizationConfigSection.DefaultSectionName);

                // 修改配置文件的IP地址 EnvironmentVariable.xml
                var environmentVariableFilePath = configSource.EnvironmentVariableFilePath;
                var environmentXmlDoc = new XmlDocument();
                environmentXmlDoc.Load(environmentVariableFilePath);
                var frontProxyServerNode = environmentXmlDoc.SelectSingleNode("EnvironmentVariable/FrontProxyServer");
                frontProxyServerNode.InnerText = serverIPAddress;
                var frontSiteServerNode = environmentXmlDoc.SelectSingleNode("EnvironmentVariable/FrontSiteServer");
                frontSiteServerNode.InnerText = serverIPAddress;
                var mqttServerNode = environmentXmlDoc.SelectSingleNode("EnvironmentVariable/MQTTServer");
                mqttServerNode.InnerText = $"{serverIPAddress}:1883";
                environmentXmlDoc.Save(environmentVariableFilePath);

                this.UpdateHostWebsites(configSource.MainSiteWebsitesFilePath, new List<string>() { "127.0.0.1", serverIPAddress });
                this.UpdateHostWebsites(configSource.PlatformWebsitesFilePath, new List<string>() { "127.0.0.1", serverIPAddress });
                #endregion
            }

            var modifyFlag = ModifyServiceConfigFlag.None;
            var serviceConfigSetParam = new Generic.ClassroomTeaching.Scene.Param.ServiceConfigSetParam();
            if (!string.IsNullOrWhiteSpace(parameter.LicenseFilePath))
            {
                var licenseFilePath = parameter.LicenseFilePath;
                if (!File.Exists(licenseFilePath))
                {
                    throw new FileNotFoundException($"许可证文件不存在:{licenseFilePath}");
                }
                var licenceFileString = File.ReadAllText(licenseFilePath);
                if (string.IsNullOrWhiteSpace(licenceFileString))
                {
                    throw new Exception($"许可证内容异常");
                }
                var licenseFileContent = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(licenceFileString));
                var licenseFileName = Path.GetFileName(licenseFilePath);
                modifyFlag |= ModifyServiceConfigFlag.LicenseFileContent;
                modifyFlag |= ModifyServiceConfigFlag.LicenseFileName;
                serviceConfigSetParam.LicenseFileContent = licenseFileContent;
                serviceConfigSetParam.LicenseFileName = licenseFileName;
            }

            if (!string.IsNullOrWhiteSpace(parameter.WifiPassword))
            {
                modifyFlag |= ModifyServiceConfigFlag.WifiPassword;
                serviceConfigSetParam.WifiPassword = parameter.WifiPassword;
            }

            if (!string.IsNullOrWhiteSpace(parameter.WifiSSID))
            {
                modifyFlag |= ModifyServiceConfigFlag.WifiSSID;
                serviceConfigSetParam.WifiSSID = parameter.WifiSSID;
            }

            if (modifyFlag != ModifyServiceConfigFlag.None)
            {
                serviceConfigSetParam.ModifyFlag = modifyFlag;
                _environment.SetServiceConfig(serviceConfigSetParam);
            }

            if (parameter.DeviceModifyParams != null && parameter.DeviceModifyParams.Count > 0)
            {
                _environment.UpdateDeviceInfo(parameter.DeviceModifyParams.Select(d => new Generic.ClassroomTeaching.Scene.Param.DeviceUpdateParam()
                {
                    DisplayName = d.Name,
                    Enabled = d.Enabled,
                    Number = d.Number,
                }).ToList());
            }
        }

        private void UpdateHostWebsites(String configPath, List<String> ipList)
        {
            var websitesXmlDoc = new XmlDocument();
            websitesXmlDoc.Load(configPath);
            var fixedIpListNode = websitesXmlDoc.SelectSingleNode("Websites/ITEM/FixedIpList");
            if (fixedIpListNode == null)
            {
                fixedIpListNode = websitesXmlDoc.CreateElement("FixedIpList");
                var parentNode = websitesXmlDoc.SelectSingleNode("Websites/ITEM");
                parentNode.AppendChild(fixedIpListNode);
            }
            else
            {
                fixedIpListNode.RemoveAll();
            }

            foreach (var ipAddress in ipList)
            {
                var ipItemNode = websitesXmlDoc.CreateElement("ITEM");
                ipItemNode.InnerText = ipAddress;
                fixedIpListNode.AppendChild(ipItemNode);
            }
            websitesXmlDoc.Save(configPath);
        }
    }
}