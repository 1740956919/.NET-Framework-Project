using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Interface
{
    /// <summary>
    /// 导入课程接口
    /// </summary>
    public interface IImportLessonService
    {
        /// <summary>
        /// 导入图片
        /// </summary>
        /// <param name="parameter"></param>
        void ImportPicture(PictureImportParam parameter);
        /// <summary>
        /// 同步云端课时信息
        /// </summary>
        /// <param name="parameter"></param>
        void SyncCloudLesson(CloudLessonSyncParam parameter);
        /// <summary>
        /// 添加课程资料
        /// </summary>
        /// <param name="parameter"></param>
        void AddLessonMaterial(LessonMaterialAddParam parameter);
        /// <summary>
        /// 导入学生名单
        /// </summary>
        /// <param name="parameter"></param>
        void ImportStudent(StudentImportParam parameter);
    }
}
