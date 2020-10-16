using System;
using System.IO;
using System.Text;

namespace Ngk.Common
{
    public class StreamHelper
    {
        public static string GetStreamText(Stream stream)
        {
            try
            {
                if (!stream.CanRead)
                    return "";
                stream.Position = 0;
                Encoding encoding = Encoding.UTF8;
                /*
                这个StreamReader不能关闭，也不能dispose， 关了就傻逼了
                因为你关掉后，后面的管道  或拦截器就没办法读取了
                */
                var reader = new StreamReader(stream, encoding);
                string result = reader.ReadToEnd();
                /*
                这里也要注意：   stream.Position = 0; 
                当你读取完之后必须把stream的位置设为开始
                因为request和response读取完以后Position到最后一个位置，交给下一个方法处理的时候就会读不到内容了。
                */
                stream.Position = 0;
                return result;
            }
            catch
            {
                return "";
            }

        }
    }
}
