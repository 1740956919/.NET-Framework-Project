using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Logic
{
    public class ConvertGender
    {
        /// <summary>
        /// 将性别字符串转换为枚举
        /// </summary>
        /// <param name="value">性别枚举值</param>
        /// <returns></returns>
        public static Gender ConvertEnumFromString(String value)
        {
            var gender = Gender.Unknown;
            switch (value)
            {
                case GenderConstants.UnknownCode: gender = Gender.Unknown; break;
                case GenderConstants.MaleCode: gender = Gender.Male; break;
                case GenderConstants.FemaleCode: gender = Gender.Female; break;
            }
            return gender;
        }
    }
}