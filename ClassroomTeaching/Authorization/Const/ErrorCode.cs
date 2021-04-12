using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Const
{
    public class ErrorCode
    {
        /// <summary>
        /// 设备未注册
        /// </summary>
        public const int DeviceNotRegisted = 1000;
        /// <summary>
        /// 当前用户不存在/平台不存在当前微信号
        /// </summary>
        public const string NotExistUser = "601";
        /// <summary>
        /// 登录密码错误
        /// </summary>
        public const string PasswordError = "602";
        /// <summary>
        /// 当前微信号已被绑定
        /// </summary>
        public const string WeChatAlreadyBind = "603";
        /// <summary>
        /// 登录已禁用
        /// </summary>
        public const string LogonAlreadyForbidden = "604";
        /// <summary>
        /// 多点登陆禁用
        /// </summary>
        public const string MultipointLogonForbidden = "605";
        /// <summary>
        /// 用户名已存在
        /// </summary>
        public const string UserAlreadyExist = "606";
        /// <summary>
        /// 该邮箱/手机已经存在
        /// </summary>
        public const string SafetyDeviceAlreadyExist = "607";
        /// <summary>
        /// 该属性不能为空
        /// </summary>
        public const string PropreityNoValue = "608";
        /// <summary>
        /// 该邮箱/手机号不是安全邮箱/手机
        /// </summary>
        public const string NotFoundSafetyDevice = "609";
        /// <summary>
        /// 用户名不存在
        /// </summary>
        public const string NotFoundProfile = "610";
        /// <summary>
        /// 密码不存在
        /// </summary>
        public const string NotFoundPassword = "611";
        /// <summary>
        /// 微信号不存在
        /// </summary>
        public const string NotFoundWeixin = "612";
        /// <summary>
        /// 该属性获取失败
        /// </summary>
        public const string NotSupportProperity = "613";
        /// <summary>
        /// 修改失败
        /// </summary>
        public const string NotSupportChange = "614";
        /// <summary>
        /// 删除密码失败
        /// </summary>
        public const string NotSupportClearPassword = "615";
        /// <summary>
        /// 清空密码失败
        /// </summary>
        public const string NotSupportEmptyPassword = "616";
        /// <summary>
        /// 该邮箱/手机号获取失败
        /// </summary>
        public const string NotSupportSecurityDevice = "617";
        /// <summary>
        /// 该邮箱/手机号操作失败
        /// </summary>
        public const string NotSupportSecurityAction = "618";
        /// <summary>
        /// 该验证码不合法
        /// </summary>
        public const string InvalidVerifyCode = "619";
        /// <summary>
        /// 该操作不合法
        /// </summary>
        public const string InvalidAction = "620";
        /// <summary>
        /// 账号已锁定
        /// </summary>
        public const string AccountLocked = "621";
        /// <summary>
        /// 没有找到许可证信息
        /// </summary>
        public const string NotFoundLicense = "622";
        /// <summary>
        /// 读取许可证失败
        /// </summary>
        public const string NotReadLicense = "623";
        /// <summary>
        /// 检查许可证失败
        /// </summary>
        public const string NotCheckLicense = "624";
        /// <summary>
        /// 没有找到安全配置
        /// </summary>
        public const string NoSecuritySection = "625";
        /// <summary>
        /// 安全配置不合法
        /// </summary>
        public const string InvalidSecuritySection = "626";
        /// <summary>
        /// 没有找到许可证服务
        /// </summary>
        public const string NoLicenseServer = "627";
        /// <summary>
        /// 服务器错误
        /// </summary>
        public const string ServerError = "628";
        /// <summary>
        /// 获得微信用户信息失败
        /// </summary>
        public const string NotExistWeChatInfo = "700";
        /// <summary>
        /// 微信登录超时
        /// </summary>
        public const string WeChatLogonTimeout = "701";
        /// <summary>
        /// 二维码不存在
        /// </summary>
        public const string NotExistQRCode = "702";
    }
}