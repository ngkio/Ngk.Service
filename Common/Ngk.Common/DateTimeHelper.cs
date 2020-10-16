using System;

namespace Ngk.Common
{
    public static class DateTimeHelper
    {
        public static DateTime? ConvertFromTimeStamp(long timeStamp)
        {
            if (timeStamp > 0)
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long time = timeStamp * 10000;
                if (time.ToString().Length < 17)
                {
                    time = time * 1000;
                }
                TimeSpan toNow = new TimeSpan(time);
                var result = dtStart.Add(toNow);
                return result;
            }
            return null;
        }

        /// <summary>
        /// 取时间戳，高并发情况下会有重复。想要解决这问题请使用sleep线程睡眠1毫秒。
        /// </summary>
        /// <param name="date"></param>
        /// <param name="accurateToMilliseconds">精确到毫秒</param>
        /// <returns>返回一个长整数时间戳</returns>
        public static long GetTimeStamp(DateTime date,bool accurateToMilliseconds = false)
        {
            if (accurateToMilliseconds)
            {

                // 使用当前时间计时周期数（636662920472315179）减去1970年01月01日计时周期数（621355968000000000）除去（删掉）后面4位计数（后四位计时单位小于毫秒，快到不要不要）再取整（去小数点）。

                //备注：DateTime.Now.ToUniversalTime不能缩写成DateTime.Now.Ticks，会有好几个小时的误差。

                //621355968000000000计算方法 long ticks = (new DateTime(1970, 1, 1, 8, 0, 0)).ToUniversalTime().Ticks;

                return (date.ToUniversalTime().Ticks - 621355968000000000) / 10000;

            }
            else
            {

                //上面是精确到毫秒，需要在最后除去（10000），这里只精确到秒，只要在10000后面加三个0即可（1秒等于1000毫米）。
                return (date.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            }
        }
    }
}
