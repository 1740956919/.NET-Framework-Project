using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Service
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IConfigSource _configSource = null;

        public EnvironmentService(IConfigSource configSourceService)
        {
            _configSource = configSourceService;
        }

        public EnvironmentInfo Get()
        {
            var config = _configSource.GetSection<FrontRouteSection>(FrontRouteSection.DefaultSectionName);
            // 生成重定向路由
            var figureConfigWebAPI = EnvironmentVariableConverter.Translate(config.RouteTables[0].RouteTable["figure_config"]); 
            // 生成第二次服务API地址
            var interactionWebAPI = EnvironmentVariableConverter.Translate(config.RouteTables[0].RouteTable["classroomteaching_interaction"]);
            var screenWebAPI = EnvironmentVariableConverter.Translate(config.RouteTables[0].RouteTable["classroomteaching_screen"]);
            return new EnvironmentInfo()
            {
                FigureConfigWebAPI = figureConfigWebAPI,
                InteractionWebAPI = interactionWebAPI,
                ScreenWebAPI = screenWebAPI
            };
        }
    }
}