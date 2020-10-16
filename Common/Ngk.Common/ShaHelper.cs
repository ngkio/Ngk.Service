using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ngk.Common
{
    public class ShaHelper
    {
        public static string Encrypt(string clearText, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            var sha = SHA512.Create();
            byte[] data = encoding.GetBytes(clearText);//将字符编码为一个字节序列 
            byte[] enData = sha.ComputeHash(data);//计算data字节数组的哈希值 
            sha.Clear();
            string str = "";
            for (int i = 0; i < enData.Length; i++)
            {
                str += enData[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
    }
}
