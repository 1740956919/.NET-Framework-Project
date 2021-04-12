using System;
using System.Web.Http;

using LINDGE.PARA;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Runtime.WebAPI;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.ServiceContext), "Init")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.ServiceContext), "InitOver")]

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI
{
    public static class ServiceContext
    {
        /// <summary>
        /// 获取当前服务模块的运行时上下文。
        /// </summary>
        public static IRuntimeContext Context
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取一个全局实例。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("应尽量避免获取全局实例。")]
        public static T Resolve<T>()
        {
            return ServiceContext.Context.IoC.Resolve<T>();
        }

        #region Init
        /// <summary>
        /// 应用程序的入口函数之一，初始化服务模块上下文。
        /// </summary>
        public static void Init()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            LoadConfig();
            RegisterRoute(config);

            config.Filters.Add(new ProxyExceptionFilterAttribute());
            config.DependencyResolver = new IoCDependencyResolver(ServiceContext.Context.IoC);
        }

        /// <summary>
        /// 完成初始化操作
        /// </summary>
        public static void InitOver()
        {
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
        #endregion Init

        /// <summary>
        /// 加载服务模块的配置文件。
        /// </summary>
        private static void LoadConfig()
        {
            String dataFolder = AppDomain.CurrentDomain.GetData("DataDirectory") as String;
            // 设置日志文件夹。
            String logFolder = System.IO.Path.Combine(dataFolder, "Logs");
            AppDomain.CurrentDomain.SetData("LogDirectory", logFolder);

            var loader = new ServiceContextInitializer();
            var sysDefine = new ConfigSourceDefine()
            {
                ConfigSourceType = typeof(ConfigurationFile),
                Arguments = new String[] { System.IO.Path.Combine(dataFolder, "System.config") }
            };
            ServiceContext.Context = loader.Load(sysDefine);
        }

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="config"></param>
        private static void RegisterRoute(HttpConfiguration config)
        {
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Trace",
                routeTemplate: "Trace",
                defaults: new { controller = "Trace" }
            );
        }

    }
}