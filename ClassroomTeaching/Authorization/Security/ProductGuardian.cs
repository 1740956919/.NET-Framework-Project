using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using LINDGE.Security;


// 与混淆相关的选项
#if Eazfuscator
[assembly: Obfuscation(Feature = "string encryption", Exclude = false)]  // 混淆时进行字符串加密
[assembly: Obfuscation(Feature = "code control flow obfuscation", Exclude = false)] // 进行流程混淆
#else
[assembly: Obfuscation(Feature = "constants", Exclude = false)]  // 混淆时进行字符串加密
[assembly: Obfuscation(Feature = "ctrl flow", Exclude = false)] // 进行流程混淆
#endif

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Security
{
    /// <summary>
    /// 支持标准校验流程的模块
    /// </summary>
    internal static class ProductGuardian
    {
        private static Dictionary<String, Assembly> loadingAssemblies = null;
        private static Assembly cryptographAssembly = null;
        private static Assembly keyDeviceAssembly = null;
        private static Assembly licenseAssembly = null;
        private static Assembly protocolAssembly = null;

        private static Object keyController = null;
        private static ILicenseParser parser = null;

        /// <summary>
        /// Assembly搜索路径，用于动态加载Assembly时拦截依赖项查找的顺序
        /// </summary>
        private static string _assemblySearchPath = null;

#region Load Assembly
        /// <summary>
        /// 从资源文件中导出Assembly并且加载
        /// </summary>
        /// <param name="password">组件库的解压密码。</param>
        /// <returns></returns>
        public static void LoadAssembly(String password)
        {
            try
            {
                ProductGuardian.loadingAssemblies = new Dictionary<String, Assembly>();
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                // 缺省构造的情况下，传入参数的密码应该为"sh-lindge_2016"
                Assembly self = typeof(ProductGuardian).Assembly;

                // LINDGE.Security.Cryptograph
                Stream originStream = self.GetManifestResourceStream("LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Res.baidu.dat");
                if (originStream == null)
                    throw new FileNotFoundException("No Cryptograph library!");
                Stream libStream = new MemoryStream();
                SecurityUtilities.Decompress(originStream, libStream, password);
                Byte[] rawData = new Byte[libStream.Length];
                libStream.Seek(0, SeekOrigin.Begin);
                libStream.Read(rawData, 0, rawData.Length);
                libStream.Close();
                originStream.Close();
                ProductGuardian.cryptographAssembly = Assembly.Load(rawData);
                ProductGuardian.AddAssembly(ProductGuardian.cryptographAssembly);

                // LINDGE.Security.KeyDevice
                originStream = self.GetManifestResourceStream("LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Res.ali.dat");
                if (originStream == null)
                    throw new FileNotFoundException("No KeyDevice library!");
                libStream = new MemoryStream();
                SecurityUtilities.Decompress(originStream, libStream, password);
                rawData = new Byte[libStream.Length];
                libStream.Seek(0, SeekOrigin.Begin);
                libStream.Read(rawData, 0, rawData.Length);
                libStream.Close();
                originStream.Close();
                ProductGuardian.keyDeviceAssembly = Assembly.Load(rawData);
                ProductGuardian.AddAssembly(ProductGuardian.keyDeviceAssembly);

                // LINDGE.Security.Protocol
                originStream = self.GetManifestResourceStream("LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Res.360.dat");
                if (originStream == null)
                    throw new FileNotFoundException("No Protocol library!");
                libStream = new MemoryStream();
                SecurityUtilities.Decompress(originStream, libStream, password);
                rawData = new Byte[libStream.Length];
                libStream.Seek(0, SeekOrigin.Begin);
                libStream.Read(rawData, 0, rawData.Length);
                libStream.Close();
                originStream.Close();
                ProductGuardian.protocolAssembly = Assembly.Load(rawData);
                ProductGuardian.AddAssembly(ProductGuardian.protocolAssembly);

                // LINDGE.Security.License
                originStream = self.GetManifestResourceStream("LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Res.tencent.dat");
                if (originStream == null)
                    throw new FileNotFoundException("No License library!");
                libStream = new MemoryStream();
                SecurityUtilities.Decompress(originStream, libStream, password);
                rawData = new Byte[libStream.Length];
                libStream.Seek(0, SeekOrigin.Begin);
                libStream.Read(rawData, 0, rawData.Length);
                libStream.Close();
                originStream.Close();
                ProductGuardian.licenseAssembly = Assembly.Load(rawData);
                ProductGuardian.AddAssembly(ProductGuardian.licenseAssembly);
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            }
        }

        private static void AddAssembly(Assembly newAssembly)
        {
            var asmName = newAssembly.GetName();
            if (!ProductGuardian.loadingAssemblies.ContainsKey(asmName.Name))
                ProductGuardian.loadingAssemblies.Add(asmName.Name, newAssembly);
            if (!ProductGuardian.loadingAssemblies.ContainsKey(asmName.FullName))
                ProductGuardian.loadingAssemblies.Add(asmName.FullName, newAssembly);
        }
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (ProductGuardian.loadingAssemblies.ContainsKey(args.Name))
            {
                return ProductGuardian.loadingAssemblies[args.Name];
            }
            else if (!String.IsNullOrEmpty(_assemblySearchPath))
            {
                string fullName = new AssemblyName(args.Name).Name;
                string assemblyPath = Path.Combine(_assemblySearchPath, fullName + ".dll");
                if (File.Exists(assemblyPath))
                {
                    return Assembly.LoadFrom(assemblyPath);
                }
            }

            return null;
        }

        private static string _getAssemblyLocation()
        {
            var uri = new Uri(typeof(ProductGuardian).Assembly.CodeBase);
            return Path.GetDirectoryName(uri.LocalPath);
        }

#endregion Load Assembly

#region LocalLicenseParser
        /// <summary>
        /// 加载加密锁的支持库，并且返回EliteE加密锁控制器的实例。
        /// </summary>
        /// <returns></returns>
        /// <remarks>应用程序可根据不同的需要选择控制器。</remarks>
        public static void LoadEliteEKeyController()
        {
            _assemblySearchPath = _getAssemblyLocation();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            try
            {
                Stream originStream = typeof(ProductGuardian).Assembly.GetManifestResourceStream("LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Res.apple.dat");
                Type controllerType = ProductGuardian.keyDeviceAssembly.GetType("LINDGE.Security.KeyDevice.EliteEController");
                ProductGuardian.keyController = Activator.CreateInstance(controllerType, originStream, "+Lindge=Software-2003");

                originStream.Close();
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                _assemblySearchPath = null;
            }
        }

        /// <summary>
        /// 从许可证文件中加载许可信息。
        /// </summary>
        /// <param name="licFileStream">许可证文件的路径或者流</param>
        /// <param name="validVersion"></param>
        /// <param name="intervalSeconds">自动更新的间隔时间。</param>
        /// <returns>提供授权服务的主机标识</returns>
        public static String LoadLicenseFile(Stream licFileStream, String validVersion,Int32 intervalSeconds)
        {
            _assemblySearchPath = _getAssemblyLocation();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            try
            {
                Type typeParser = ProductGuardian.licenseAssembly.GetType("LINDGE.Security.License.LocalLicenseParser");
                ProductGuardian.parser = (ILicenseParser)Activator.CreateInstance(typeParser, ProductGuardian.keyController, intervalSeconds);

                return ProductGuardian.parser.Load(licFileStream, validVersion);
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                _assemblySearchPath = null;
            }
        }
#endregion Local License Parser

        /// <summary>
        /// 释放许可证解析模块。
        /// </summary>
        public static void UnloadParser()
        {
            if (ProductGuardian.parser != null)
            {
                ProductGuardian.parser.Dispose();
                ProductGuardian.parser = null;
            }
            if (ProductGuardian.keyController != null)
            {
                Type typController = ProductGuardian.keyController.GetType();
                typController.InvokeMember("Dispose", BindingFlags.InvokeMethod, null, ProductGuardian.keyController, null);
                ProductGuardian.keyController = null;
            }
        }

        /// <summary>
        /// 检查产品信息的授权信息是否合法。
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="leftDays">返回类型参数，特性模块剩余可使用天数。-1表示非有效期限的授权</param>
        /// <param name="leftHours">返回类型参数，特性模块剩余可使用小时数。-1表示非使用时间的授权</param>
        /// <returns></returns>
        public static ICryptoProvider CheckProductInfo(Int32 productCode, out Int32 leftHours, out Int32 leftDays)
        {
            leftHours = -1;
            leftDays = -1;

            var productLicense = ProductGuardian.parser.GetProduct();
            if (productLicense.ProductCode != productCode)
            {
                throw new SecurityException(SecurityException.NoProductAuth,
                    String.Format("Not found product({0} vs {1}) license!", productLicense.ProductCode, productCode));
            }

            if (productLicense.AuthType == ProductLicense.AuthDuration)    // Duration
            {
                if (productLicense.LeftAuthSeconds <= 0)
                {
                    throw new SecurityException(SecurityException.AuthExpired, "License is expired!");
                }
                else
                {
                    leftHours = (Int32)productLicense.LeftAuthHours;
                }
            }
            else if (productLicense.AuthType == ProductLicense.AuthExpiredDate)   // Expired
            {
                DateTime curTime = DateTime.Now;
                if (curTime.Date > productLicense.ExpiredDate.Date)
                {
                    throw new SecurityException(SecurityException.AuthExpired,
                        String.Format("License is expired after [{0}]!", productLicense.ExpiredDate));
                }
                else
                {
                    leftDays = (productLicense.ExpiredDate.Date - curTime.Date).Days;
                }
            }

            return productLicense.CryptoProvider;
        }

        /// <summary>
        /// 校验特征许可并且返回特征密钥。
        /// </summary>
        /// <param name="featureName"></param>
        /// <param name="featureCode"></param>
        /// <returns></returns>
        public static ICryptoProvider CheckFeatureInfo(String featureName,UInt32 featureCode)
        {
            var featureLicense = ProductGuardian.parser.GetFeature(featureName);
            if (featureLicense == null || featureLicense.FeatureCode != featureCode)
            {
                throw new SecurityException(SecurityException.NoFeatureAuth,
                    String.Format("Not found authorization of Feature[{0}({1})]", featureName, featureCode));
            }

            return featureLicense.CryptoProvider;
        }
    }
}
