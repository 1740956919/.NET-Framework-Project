using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface
{
    public interface IIdeaService
    {
        /// <summary>
        /// 获取数据收集结果
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        BrainstormingInfo GetBrainstormingInfo(string brainstormingId, BrainstormingInfoGetParam parameter);
        /// <summary>
        /// 提交点子（手机）
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="ideaContent">点子内容</param>
        void SubmitIdea(string brainstormingId, string ideaContent);
        /// <summary>
        /// 更新点子分类
        /// </summary>
        /// <param name="brainstormingId"></param>
        /// <param name="ideaId"></param>
        /// <param name="categoryId"></param>
        void UpdateIdeaCategory(string brainstormingId, string ideaId, string categoryId);

        /// <summary>
        /// 获取头脑风暴状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BrainstormingState GetBrainstormingState(string brainstormingId);
        /// <summary>
        /// 启动讨论板
        /// </summary>
        /// <param name="id"></param>
        void StartDiscussionBoard(string brainstormingId);
        /// <summary>
        /// 清除点子分类
        /// </summary>
        /// <param name="brainstormingId"></param>
        /// <param name="ideaId"></param>
        void ClearIdeaCategory(string brainstormingId, string ideaId);

        /// <summary>
        /// 删除点子
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="ideaId">点子标识</param>
        void DeleteIdea(string brainstormingId, string ideaId);
        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        CategoryCreateResult CreateCategory(string brainstormingId, CategoryCreateParam parameter);
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="categroyId">分类标识</param>
        void DeleteCategory(string brainstormingId, string categroyId);
        /// <summary>
        /// 修改分类名称
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <param name="categroyId">分类标识</param>
        /// <param name="categoryName">分类名称</param>
        void RenameCategory(string brainstormingId, string categroyId, string categoryName);
        /// <summary>
        /// 创建内容输入行为
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        String StartTextInput(string brainstormingId);
        /// <summary>
        /// 获取内容输入结果
        /// </summary>
        /// <param name="behaviorId">行为标识</param>
        /// <returns></returns>
        TextInputResult GetTextInputContent(string behaviorId);
        /// <summary>
        /// 放弃文本输入
        /// </summary>
        /// <param name="behaviorId">行为标识</param>
        void DeleteTextInput(string behaviorId);
        /// <summary>
        /// 获取内容输入行为（手机）
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        String GetTextInput(string brainstormingId);
        /// <summary>
        /// 提交内容输入结果（手机）
        /// </summary>
        /// <param name="behaviorId">行为标识</param>
        /// <param name="parameter"></param>
        void SubmitTextInout(string behaviorId, TextInputParam parameter);
        /// <summary>
        /// 查询是否需要输入内容（手机）
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        Boolean GetInputText(string brainstormingId);
    }
}