using System;

namespace Ngk.Common
{
    public class InvitationCodeHelper
    {
        /**自定义进制(0,1没有加入,容易与o,l混淆)*/
        private static readonly char[] r = new char[] { 'Q', 'X', 'V', '5', 'A', 'G', 'W', '8', 'Z', 'O', 'C', '2', 'U', 'K', 'H', '9', 'S', 'N', '4', 'E', 'J', 'F', 'M', 'R', '3', 'B', 'T', 'D', '7', 'L', 'P', 'I', '6', 'Y' };
        /**进制长度*/
        private static readonly int l = r.Length;
        /**序列最小长度*/
        private static readonly int s = 6;

        /**
          * 根据ID生成六位随机码
          * @param num ID
          * @return 随机码
          */
        public static String ToSerialNumber(long num)
        {
            char[] buf = new char[10];
            int charPos = buf.Length;

            while ((num / l) > 0)
            {
                buf[--charPos] = r[(int)(num % l)];
                num /= l;
            }
            buf[--charPos] = r[(int)(num % l)];
            String str = new String(buf, charPos, (10 - charPos));
            //不够长度的自动随机补全
            while (str.Length < s)
            {
                str += r[l - 1];
            }
            return str;
        }
    }
}
