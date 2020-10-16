using System;

namespace Ngk.Common
{
    public static class MathHelper
    {

        /// <summary>
        /// 将小数转换为小于或等于指定位数的小数
        /// </summary>
        /// <param name="number"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static decimal Ceiling(decimal number, int length)
        {
            if (length < 1)
            {
                throw new Exception("参数错误！");
            }
            var rate = (int)Math.Pow(10, length);
            return Math.Ceiling(number * rate) / rate;
        }

        /// <summary>
        /// 将小数转换为小于或等于指定位数的小数
        /// </summary>
        /// <param name="number"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static double Ceiling(double number, int length)
        {
            if (length < 1)
            {
                throw new Exception("参数错误！");
            }
            var rate = (int)Math.Pow(10, length);
            return Math.Ceiling(number * rate) / rate;
        }

        /// <summary>
        /// 将小数转换为大于或等于指定位数的小数
        /// </summary>
        /// <param name="number"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static decimal Floor(decimal number, int length)
        {
            if (length < 1)
            {
                throw new Exception("参数错误！");
            }
            var rate = (int)Math.Pow(10, length);
            return Math.Floor(number * rate) / rate;
        }

        /// <summary>
        /// 将小数转换为大于或等于指定位数的小数
        /// </summary>
        /// <param name="number"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static double Floor(double number, int length)
        {
            if (length < 1)
            {
                throw new Exception("参数错误！");
            }
            var rate = (int)Math.Pow(10, length);
            return Math.Floor(number * rate) / rate;
        }
    }
}
