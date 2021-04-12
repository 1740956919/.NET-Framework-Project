using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result
{
    /// <summary>
    /// 上课状态
    /// </summary>
    public class ClassStateResult
    {
        public ClassStateResult()
        {
            this.InteractiveInfo = new InteractiveInfo();
        }

        /// <summary>
        /// 是否正在上课
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 是的需要互动，及是否显示互动操作页面
        /// </summary>
        public bool NeedInteract { get; set; }
        /// <summary>
        /// 互动信息，NeedInteract为true时有效
        /// </summary>
        public InteractiveInfo InteractiveInfo { get; set; }
    }
}