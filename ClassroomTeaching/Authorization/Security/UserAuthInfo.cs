using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Security
{
    /// <summary>
    ///  User Authorization，记录完整的用户授权信息的数据结构。
    /// </summary>
    public sealed class UserAuthInfo
    {
        public String SerialNumber
        {
            get;
            set;
        }

        public Int32 ProductCode
        {
            get;
            set;
        }

        /// <summary>
        /// 授权用户信息
        /// </summary>
        public String CustomInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 授权销售区域
        /// </summary>
        public String Region
        {
            get;
            set;
        }

        /// <summary>
        /// 授权版本类型
        /// </summary>
        public Char EditionType
        {
            get;
            set;
        }

        /// <summary>
        /// 授权销售行业
        /// </summary>
        public String Domain
        {
            get;
            set;
        }

        /// <summary>
        /// 最大客户端数量
        /// </summary>
        public UInt16 MaxClientNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 如果是按有效期授权的，则返回截止日期；如果为DateTime.MinValue，表示非有效期授权模式。
        /// </summary>
        public DateTime ExpiredDate
        {
            get;
            set;
        }

        /// <summary>
        /// 如果是按使用时间授权的，则返回授权时间，单位小时；如果为0，表示非使用时间授权模式。
        /// </summary>
        public UInt32 LeftAuthHours
        {
            get;
            set;
        }

        /// <summary>
        /// 特性列表
        /// </summary>
        public List<String> FeatureNameList
        {
            get;
            set;
        }

        public void Clear()
        {
            this.CustomInfo = null;
            this.EditionType = '\0';
            this.Domain = null;
            this.Region = null;
            this.MaxClientNumber = 0;
            this.ExpiredDate = DateTime.MinValue;
            this.LeftAuthHours = 0;
            this.FeatureNameList.Clear();
        }
    }
}