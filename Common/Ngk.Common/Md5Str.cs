using System.Security.Cryptography;
using System.Text;

namespace Ngk.Common
{
    public static class Md5Str
    {

        /// <summary>
        /// 获取MD5加密字符串
        /// </summary>
        /// <param name="ClearText"></param>
        /// <returns></returns>
        public static string MD5(this string ClearText, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.Default;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = encoding.GetBytes(ClearText.ToString());//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
    }
}
