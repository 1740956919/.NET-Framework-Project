using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Struct
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentInfo
    {
        /// <summary>
        /// 关联的用户Key 不为空
        /// </summary>
        public String CorrelativeCode { get; set; }
        /// <summary>
        /// 成员标识 不为空
        /// </summary>
        public String MemberId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public String Number { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public String Gender { get; set; }
        /// <summary>
        /// 头像在zip中的路径
        /// </summary>
        public String PortraitFile { get; set; }
        /// <summary>
        /// 头像保存的句柄
        /// </summary>
        public String PortraitId { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        public String GroupName { get; set; }
    }
}
