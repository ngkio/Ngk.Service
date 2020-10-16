using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ngk.Common
{
    public static class MappingHelper
    {
        /// <summary>
        /// 实体转换成字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ConvertToDictionary(this object obj)
        {
            var result = new Dictionary<string, string>();
            try
            {
                Type t = obj.GetType();
                foreach (var memberInfo in t.GetProperties())
                {
                    result.Add(memberInfo.Name, memberInfo.GetValue(obj).ToString());
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return result;
        }

        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>字典数据</returns>
        public static Dictionary<string, string> DeserializeToDictionary(this object obj)
        {
            var jsonStr = JsonConvert.SerializeObject(obj);
            Dictionary<string, string> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
            return jsonDict;
        }

        /// <summary>
        /// 将键值对转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(Dictionary<string, object> dictionary)
        {
            var str = JsonConvert.SerializeObject(dictionary);
            var obj = JsonConvert.DeserializeObject<T>(str);
            return obj;
        }

        /// <summary>
        /// 将对转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(String jsonStr)
        {
            var obj = JsonConvert.DeserializeObject<T>(jsonStr);
            return obj;
        }

    }
}
