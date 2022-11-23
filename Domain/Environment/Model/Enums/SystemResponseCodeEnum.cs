using System.ComponentModel;

namespace Domain.Environment.Model.Enums
{
    public enum SystemResponseCodeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("Success")]
        Success = 000,

        /// <summary>
        /// API不存在
        /// </summary>
        [Description("404 API Not Found")]
        ApiNotFound = 101,

        #region 商戶檢查 2xx

        /// <summary>
        /// 查无商户号
        /// </summary>
        [Description("Merchant Not Found")]
        MerchantNoNotFound = 202,

        /// <summary>
        /// 查无商户号
        /// </summary>
        [Description("Merchant cannot be null")]
        MerchantNoNullError = 203,

        /// <summary>
        /// 查无商户号
        /// </summary>
        [Description("Merchant Format Error")]
        MerchantFormatError = 204,

        /// <summary>
        /// 商户停用中
        /// </summary>
        [Description("Merchant is Unavailable﻿")]
        MerchantUnavailable = 205,

        /// <summary>
        /// 商户冻结中
        /// </summary>
        [Description("Merchant is Lock﻿")]
        MerchantStopped = 206,

        /// <summary>
        /// 商户配置错误
        /// </summary>
        [Description("Merchant configuration error")]
        MerchantKeyConfigError = 207,

        /// <summary>
        /// 商户配置错误
        /// </summary>
        [Description("Merchant configuration error")]
        MerchantSystemKeyConfigError = 208,

        #endregion 商戶檢查 2xx

        #region 請求參數檢查 3xx

        /// <summary>
        /// request参数错误
        /// </summary>
        [Description("Request cannot be null")]
        RequestError = 303,

        /// <summary>
        /// signMsg 不能為空
        /// </summary>
        [Description("SignMsg cannot be null")]
        SignMsgNullError = 304,

        /// <summary>
        /// signMsg 格式錯誤
        /// </summary>
        [Description("SignMsg format Error")]
        SignMsgFormatError = 305,

        #endregion 請求參數檢查 3xx

        #region 加解密檢查 4xx

        /// <summary>
        /// 验签失败
        /// </summary>
        [Description("signMsg verification failed")]
        SignVerifyFailed = 401,

        /// <summary>
        /// 加解密失败
        /// </summary>
        [Description("decrypt failed")]
        DecryptFailed = 402,

        /// <summary>
        /// 密文格式错误
        /// </summary>
        [Description("decrypt format error")]
        RsaFormatError = 403,

        /// <summary>
        /// 密文格式拼接错误
        /// </summary>
        [Description("decrypt string mapping to model failed")]
        RsaDecryptModelFailed = 404,

        #endregion 加解密檢查 4xx

        #region 訂單 5xx

        /// <summary>
        /// 沒有可用帳號
        /// </summary>
        [Description("No Available Account")]
        NoAvailableAccount = 500,

        #endregion 訂單 5xx

        /// <summary>
        /// 系统内部错误
        /// </summary>
        [Description("system error")]
        SystemError = 999
    }
}