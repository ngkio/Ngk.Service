using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Ngk.Common.Enum;

namespace Ngk.Common
{
    /// <summary>
    /// 语言辅助类
    /// </summary>
    public class LanguageHelper
    {

        /// <summary>
        /// 获取所有语言字符串集合
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLangStrList()
        {
            return new List<string>() { "ko", "en", "zh" };
        }

        /// <summary>
        /// 获取外语语言列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetForeignLangStrList()
        {
            return new List<string>() { "ko", "en" };
        }

        public static EnumLanguageType GetEnumLanguageType(HttpContext httpContext)
        {
            var lang = GetHeadParamByKey(httpContext, "lang");
            try
            {
                if (!string.IsNullOrEmpty(lang))
                {
                    var type = (EnumLanguageType)System.Enum.Parse(typeof(EnumLanguageType), lang, true);
                    return type;
                }
            }
            catch
            {
                //ignore
            }
            return EnumLanguageType.Zh;
        }

        /// <summary>
        /// 获取语言请求参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetStringLanguageType(HttpContext httpContext)
        {
            var langStr = GetHeadParamByKey(httpContext, "lang");
            var langStrs = GetLangStrList();
            if (string.IsNullOrEmpty(langStr) || !langStrs.Contains(langStr))
            {
                return "zh";
            }
            return langStr.ToLower();
        }

        /// <summary>
        /// 根据key获取参数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetHeadParamByKey(HttpContext httpContext, string key)
        {
            string result = "zh";
            try
            {
                result = httpContext.Request.Headers[key];
                if (string.IsNullOrEmpty(result)) result = "zh";
            }
            catch
            {
                result = "zh";
            }
            return result.ToLower();
        }

    }
}
