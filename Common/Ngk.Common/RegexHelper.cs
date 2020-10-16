using System;
using System.Text.RegularExpressions;

namespace Ngk.Common
{
    public static class RegexHelper
    {
        /// <summary>
        ///是否是电话
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            //15795682168
            if (Regex.IsMatch(str, @"^1\d{10,10}$"))
                return true;
            //028-54698755
            if (Regex.IsMatch(str, @"^0\d{2,2}-\d{8,8}$"))
                return true;
            //0576-5487596
            if (Regex.IsMatch(str, @"^0\d{3,3}-\d{7,7}$"))
                return true;
            //4005697513
            if (Regex.IsMatch(str, @"^4\d{9,9}$"))
                return true;
            //02854586987
            if (Regex.IsMatch(str, @"^0\d{10,10}$"))
                return true;
            return false;
        }

        /// <summary>
        ///是否是电子邮件
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            if (Regex.IsMatch(str, @"^[A-Z0-9a-z]{1,}(\w*@)(\w{1,})\.([a-zA-Z]{1,})$"))
                return true;
            return false;
        }

        /// <summary>
        ///是否是数字，包括正数、负数和小数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            if (Regex.IsMatch(str, @"^-{0,}\d(\.{0,1}\d{1,}){0,}$"))
                return true;
            return false;
        }

        /// <summary>
        ///是否是合法的身份证 (只支持18位)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIdCard(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;
            if (!Regex.IsMatch(str, @"^(\d{17,17})((\d{1,1}|X)|(\d{1,1}|x))$"))
                return false;
            str = str.ToUpper();
            var data = string.Format("{0}-{1}-{2}", str.Substring(6, 4), str.Substring(10, 2), str.Substring(12, 2));
            DateTime dt;
            var b = DateTime.TryParse(data, out dt);
            if (!b) return false;
            char[] newcard = str.Substring(0, 17).ToCharArray();
            double sum = 0;
            for (int i = 0; i < 17; i++)
            {
                int n = int.Parse(newcard[i].ToString());
                var x = n * Math.Pow(2, 17 - (i));
                sum += x;
            }
            sum = sum % 11;
            char[] check = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            if (check[(int)sum] != str[17])
                return false;
            return true;
        }

        /// <summary>
        /// 身份证号码转生日
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IDCardToBirthDay(string str)
        {
            if (str.Length < 14)
                return "";
            var data = string.Format("{0}-{1}-{2}", str.Substring(6, 4), str.Substring(10, 2), str.Substring(12, 2));
            return data;
        }

        /// <summary>
        /// 根据身份证判断是否为成人（超过12岁）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IDCardIsAdult(string str)
        {
            if (str.Length < 14)
                return false;
            var data = string.Format("{0}-{1}-{2}", str.Substring(6, 4), str.Substring(10, 2), str.Substring(12, 2));
            DateTime birthDay;
            if (DateTime.TryParse(data, out birthDay))
            {
                var now = DateTime.Now;
                int age = now.Year - birthDay.Year;
                if (now.Month < birthDay.Month || (now.Month == birthDay.Month && now.Day < birthDay.Day))
                {
                    age--;
                }
                return age >= 12;
            }
            return false;
        }

        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns> 
        public static string GetValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }


        /// <summary>
        ///是否是电子邮件
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAccount(string str)
        {
            if (str.Length <= 12)
            {
                if (Regex.IsMatch(str, @"[a-z1-5\.]{1,12}"))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 是否满足账号规则
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckAccount(string str)
        {
            if (str.Length == 12)
            {
                if (Regex.IsMatch(str, @"^[1-5a-z.]+$"))
                    return true;
            }
            return false;
        }
    }

}
