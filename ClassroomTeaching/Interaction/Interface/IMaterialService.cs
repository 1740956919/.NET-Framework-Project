using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface
{
    public interface IMaterialService
    {
        /// <summary>
        /// 获取课程内容
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        LessonContent GetLessonContent(String lessonId, String token);
        /// <summary>
        /// 下载本地资料
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        MaterialDownloadResult DownLoadLocalMaterial(String resourceId);
        /// <summary>
        /// 下发课程资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="resourceId"></param>
        void OpenLessonMaterial(String lessonId, String resourceId);
        /// <summary>
        /// 获取课程资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        LessonMaterial GetLessonMaterial(String lessonId);
        /// <summary>
        /// 预览课程资料
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        MaterialPreviewResult PreviewLessonMaterial(String resourceId);
        /// <summary>
        /// 获取下发的本地资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        List<LocalMaterial> GetOpenedLocalMaterials(String lessonId);
        /// <summary>
        /// 获取课程资料状态
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        LessonMaterialStatus GetLessonMaterialStatus(String classroomId);
        /// <summary>
        /// 下发本地资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        void OpenLocalMaterial(String lessonId, LocalMaterialOpenParam parameter);
    }
}
