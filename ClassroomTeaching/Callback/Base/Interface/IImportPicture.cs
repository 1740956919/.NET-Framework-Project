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
    /// 导入图片
    /// </summary>
    public interface IImportPicture
    {
        /// <summary>
        /// 导入图片
        /// </summary>
        /// <param name="parameter">图片导入参数</param>
        [WebAPIInvocation(ServiceName = "ImportPicture", Method = "POST", ParameterConvertion = ParameterConvertionType.JSON)]
        void ImportPicture(PictureImportParam parameter);
    }
}
