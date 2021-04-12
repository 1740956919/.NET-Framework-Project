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
    /// 添加课程资料
    /// </summary>
    public interface IAddLessonMaterial
    {
        /// <summary>
        /// 添加课程资料
        /// </summary>
        /// <param name="parameter">课时资料添加参数</param>
        [WebAPIInvocation(ServiceName = "AddLessonMaterial", Method = "POST", ParameterConvertion = ParameterConvertionType.JSON)]
        void AddLessonMaterial(LessonMaterialAddParam parameter);
    }
}
