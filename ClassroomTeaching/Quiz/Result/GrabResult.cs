using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    public class GrabResult
    {
        /// <summary>
        /// 是否是我抢到的
        /// </summary>
        public bool IsMySelf { get; set; }
        /// <summary>
        /// 抢答成功者
        /// </summary>
        public WinnnerInfo Winer { get; set; }
    }
}