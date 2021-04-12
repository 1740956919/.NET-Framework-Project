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
    /// 导入学生名单
    /// </summary>
    public interface IImportStudent
    {
        /// <summary>
        /// 导入学生名单
        /// </summary>
        /// <param name="parameter">学生名单导入参数</param>
        [WebAPIInvocation(ServiceName = "ImportStudent", Method = "POST", ParameterConvertion = ParameterConvertionType.JSON)]
        void ImportStudent(StudentImportParam parameter);
    }
}
