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
    /// 同步云端课时信息
    /// </summary>
    public interface ISyncCloudLesson
    {
        /// <summary>
        /// 同步云端课时信息
        /// </summary>
        /// <param name="parameter">封面添加参数</param>
        [WebAPIInvocation(ServiceName = "SyncCloudLesson", Method = "POST", ParameterConvertion = ParameterConvertionType.JSON)]
        void SyncCloudLesson(CloudLessonSyncParam parameter);
    }
}
