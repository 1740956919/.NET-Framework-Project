using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using LINDGE.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Interface
{
    /// <summary>
    /// 导出课堂报告
    /// </summary>
    public interface IExportActivityPicture
    {
        /// <summary>
        /// 导出课堂报告活动图片
        /// </summary>
        /// <param name="parameter"></param>
        [WebAPIInvocation(ServiceName = "ExportActivityPicture", Method = "POST", ParameterConvertion = ParameterConvertionType.JSON)]
        void ExportPictures(PictureExportParam parameter); 
    }
}
