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
    /// 开始课时
    /// </summary>
    public interface IStartLesson
    {
        /// <summary>
        /// 开始课时
        /// </summary>
        /// <param name="parameter">课时开始参数</param>
        [WebAPIInvocation(ServiceName = "StartLesson", Method = "POST", ParameterConvertion = ParameterConvertionType.JSON)]
        void StartLesson();
    }
}
