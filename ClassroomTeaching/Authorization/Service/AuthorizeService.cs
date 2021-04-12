using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Generic.Sociality.User.API.Logon;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Logic;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Security;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Proxy.WebAPIClient;
using LINDGE.Security;
using LINDGE.Serialization;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Service
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IEnvironment _environment = null;
        private readonly IConfigSource _configSource = null;
        private readonly IPasswordLogon _passwordLogon = null;

        public AuthorizeService(IEnvironment environmentService,
            IConfigSource configSourceService,
            IProxy<IPasswordLogon> passwordLogonProxy)
        {
            _environment = environmentService;
            _configSource = configSourceService;
            _passwordLogon = passwordLogonProxy.GetObject();
        }

        public LicenseCheckResult CheckLicense()
        {
            var licence = this.GetLicenceContent();
            var resultCode = 0;
            if (string.IsNullOrWhiteSpace(licence))
            {
                resultCode = SecurityException.InvalidLicenseFile;
            }
            else
            {
                int leftHours = 0, leftDays = 0;
                try
                {
                    var securityAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "LINDGE.Security.KeyDevice");
                    if (securityAssembly == null)
                    {
                        // 加载安全组件
                        ProductGuardian.LoadAssembly("sh-lindge_2016");
                    }
                    // 初始化密钥校验模块
                    ProductGuardian.LoadEliteEKeyController();
                    // 加载许可证文件。
                    using (var licStream = new MemoryStream(Convert.FromBase64String(licence)))
                    {
                        ProductGuardian.LoadLicenseFile(licStream, "4.0", 5000);
                        // 检查产品信息是否匹配。
                        ProductGuardian.CheckProductInfo(11, out leftHours, out leftDays);
                        ProductGuardian.CheckFeatureInfo("Kernel", 0x2);
                    }
                }
                catch (SecurityException ex)
                {
                    resultCode = ex.ErrorCode;
                }
                catch (Exception ex)
                {
                    resultCode = -1;
                }

                // 释放加密狗控制器
                ProductGuardian.UnloadParser();
            }

#if DEBUG
            resultCode = ResultCodes.NoError;
#endif

            return new LicenseCheckResult()
            {
                Error = ResultCodes.GetError(resultCode),
                IsSuccess = resultCode == ResultCodes.NoError
            };
        }

        public void DeviceLogon(DeviceLogonParam parameter, bool verifyDeviceIsRegisted)
        {
            if (verifyDeviceIsRegisted)
            {
                var registedDeviceInfos = _environment.GetRegistedDevice();
                if (!registedDeviceInfos.Exists(r => r.Id == parameter.LogonName))
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Content = new StringContent((new { Code = ErrorCode.DeviceNotRegisted }).ToJSONString())
                    });
                }
            }
            try
            {
                var logonResult = _passwordLogon.Verify(new PasswordLogonParameter()
                {
                    IP = HttpRequestExtension.GetRemoteAddress(),
                    LogonName = parameter.LogonName,
                    Password = parameter.Password
                });
                AvatarAuthContextExtensions.RecordAuthentication(logonResult.Token);
            }
            catch (Exception ex)
            {
                var resultCode = ExceptionHandle.GetCodeByException(ex);
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent((new { Code = resultCode }).ToJSONString())
                });
            }
        }

        public List<AuthorizedModuleInfo> GetAuthorizedModuleInfos()
        {
            var config = _configSource.GetSection<AuthorizationConfigSection>(AuthorizationConfigSection.DefaultSectionName);
            var licence = this.GetLicenceContent();
            var authorizedModuleInfos = new List<AuthorizedModuleInfo>();
            int leftHours = 0, leftDays = 0;
            try
            {
                var securityAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "LINDGE.Security.KeyDevice");
                if (securityAssembly == null)
                {
                    // 加载安全组件
                    ProductGuardian.LoadAssembly("sh-lindge_2016");
                }
                // 初始化密钥校验模块
                ProductGuardian.LoadEliteEKeyController();
                // 加载许可证文件。
                using (var licStream = new MemoryStream(Convert.FromBase64String(licence)))
                {
                    ProductGuardian.LoadLicenseFile(licStream, "4.0", 5000);
                    // 检查产品信息是否匹配。
                    ProductGuardian.CheckProductInfo(11, out leftHours, out leftDays);
                    try
                    {
                        var result = ProductGuardian.CheckFeatureInfo("Kernel", 0x2);
                        authorizedModuleInfos.AddRange(config.Modules.Where(m => m.Category == "Kernel").Select(m => new AuthorizedModuleInfo()
                        {
                            Entrance = m.Entrance,
                            Icon = m.Icon,
                            Id = m.Id,
                            Index = m.Index,
                            Name = m.Name
                        }));
                    }
                    catch { };

                    try
                    {
                        var result = ProductGuardian.CheckFeatureInfo("Teaching", 0x8);
                        authorizedModuleInfos.AddRange(config.Modules.Where(m => m.Category == "Teaching").Select(m => new AuthorizedModuleInfo()
                        {
                            Entrance = m.Entrance,
                            Icon = m.Icon,
                            Id = m.Id,
                            Index = m.Index,
                            Name = m.Name
                        }));
                    }
                    catch { };

                    try
                    {
                        var result = ProductGuardian.CheckFeatureInfo("Group", 0x10);
                        authorizedModuleInfos.AddRange(config.Modules.Where(m => m.Category == "Group").Select(m => new AuthorizedModuleInfo()
                        {
                            Entrance = m.Entrance,
                            Icon = m.Icon,
                            Id = m.Id,
                            Index = m.Index,
                            Name = m.Name
                        }));
                    }
                    catch { };
                }
            }
            catch { }

            // 释放加密狗控制器
            ProductGuardian.UnloadParser();
#if DEBUG
            authorizedModuleInfos = config.Modules.Select(m => new AuthorizedModuleInfo()
            {
                Entrance = m.Entrance,
                Icon = m.Icon,
                Id = m.Id,
                Index = m.Index,
                Name = m.Name
            }).ToList();
#endif
            return authorizedModuleInfos;
        }

        private string GetLicenceContent()
        {
            var serviceConfigInfo = _environment.GetServiceConfig();
            return serviceConfigInfo?.LicenseFileContent ?? string.Empty;
        }
    }
}