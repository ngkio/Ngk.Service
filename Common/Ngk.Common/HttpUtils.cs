using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Ngk.Common
{
    public class HttpUtils
    {
        //http  超时时间 单位秒
        public static int HTTP_TIME_OUT_SECONDS = 120000;

        /**
         * @param hostUrl
         * @param location
         * @param param
         * @param headers
         * @param timeOut
         * @param retryTimes
         * @return
         * @throws Exception
         */
        public static String Get(String hostUrl, String location, Dictionary<String, String> param, Dictionary<String, String> headers,
                                   int timeOut, int retryTimes)
        {
            for (int i = 1; i <= retryTimes; i++)
            {
                try
                {
                    return Get(hostUrl, location, param, headers, timeOut);
                }
                catch (Exception e)
                {
                    //Logger.error("==>请求异常，hostUrl: " + hostUrl + " => location: " + location + "=>重试! i: " + i, e);
                    if (i == retryTimes)
                    {
                        throw e;
                    }
                    continue;
                }
            }
            throw new Exception();
        }

        /**
         * get方式请求
         *
         * @return
         */
        public static String Get(String hostUrl, String location, Dictionary<String, String> param, Dictionary<String, String> headers, int? timeOut = null)
        {
            if (string.IsNullOrEmpty(hostUrl))
            {
                throw new Exception("hostUrl cannot be null!");
            }
            if (timeOut == null)
            {
                timeOut = HTTP_TIME_OUT_SECONDS;
            }
            try
            {
                //创建HttpGet
                String url = BuildGetUrl(hostUrl, location, param);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (url.Contains("https:"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }
                request.Method = "GET";
                request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = timeOut.Value;
                AddHeaders(request, headers);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**
         * get方式请求
         *
         * @return
         */
        public static String Get(String hostUrl, String location, Dictionary<String, String> param, Dictionary<String, String> headers)
        {
            return Get(hostUrl, location, param, headers, HTTP_TIME_OUT_SECONDS);
        }


        /**
         * @param host
         * @param port
         * @param relativeUrl
         * @param param
         * @return
         */
        public static String Get(String host, int port, String relativeUrl, Dictionary<String, String> param)
        {
            String absUrl = BuildUrl(host, port);
            return Get(absUrl, relativeUrl, param, null, null);
        }


        /**
         * get方式请求
         *
         * @return
         */
        public static String Get(String hostUrl, String location, Dictionary<String, String> param)
        {
            return Get(hostUrl, location, param, null, HTTP_TIME_OUT_SECONDS);
        }

        /**
         * @param hostUrl
         * @param location
         * @param data
         * @param headers
         * @param timeOut
         * @param retryTimes
         * @param <T>
         * @return
         * @throws Exception
         */
        public static String Post(String hostUrl, String location, object data, Dictionary<String, String> headers, int
                timeOut, int retryTimes)
        {

            for (int i = 1; i <= retryTimes; i++)
            {
                try
                {
                    return doPost(hostUrl, location, data, headers, timeOut);
                }
                catch (Exception e)
                {
                    //Logger.error("==>请求异常，hostUrl: " + hostUrl + " => location: " + location + "=>重试! i: " + i, e);
                    if (i == retryTimes)
                    {
                        throw e;
                    }
                }
            }

            throw new Exception("请求超时");
        }

        /**
         * @param hostUrl
         * @param location
         * @param data
         * @param headers
         * @param timeOut
         * @param <T>
         * @return
         * @throws Exception
         */
        public static String doPost(String hostUrl, String location, object data, Dictionary<String, String> headers, int? timeOut)
        {

            if (string.IsNullOrEmpty(hostUrl))
            {
                throw new Exception("hostUrl cannot be null!");
            }

            if (timeOut == null)
            {
                timeOut = HTTP_TIME_OUT_SECONDS;
            }
            try
            {
                String url = BuildUrl(hostUrl, location);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = timeOut.Value;
                AddHeaders(request, headers);
                if (data != null)
                {
                    String postData = JsonConvert.SerializeObject(data);
                    byte[] payload;
                    //将Json字符串转化为字节  
                    payload = System.Text.Encoding.UTF8.GetBytes(postData);
                    //设置请求的ContentLength   
                    request.ContentLength = payload.Length;
                    Stream writer;
                    try
                    {
                        writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
                    }
                    catch (Exception)
                    {
                        writer = null;
                        Console.Write("连接服务器失败!");
                    }
                    //将请求参数写入流
                    writer.Write(payload, 0, payload.Length);
                    writer.Close();//关闭请求流
                    // String strValue = "";//strValue为http响应所返回的字符流
                }
                HttpWebResponse response;
                try
                {
                    //获得响应流
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = ex.Response as HttpWebResponse;
                }
                Stream s = response.GetResponseStream();
                //  Stream postData = Request.InputStream;
                StreamReader sRead = new StreamReader(s);
                string postContent = sRead.ReadToEnd();
                sRead.Close();
                return postContent;//返回Json数据
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**
         * @param hostUrl
         * @param location
         * @param data
         * @param headers
         * @param <T>
         * @return
         * @throws Exception
         */
        public static String Post(String hostUrl, String location, object data, Dictionary<String, String> headers)
        {
            return doPost(hostUrl, location, data, headers, HTTP_TIME_OUT_SECONDS);
        }

        /**
         * @param host
         * @param port
         * @param relativeUrl
         * @param param
         * @return
         * @throws Exception
         */
        public static String Post(String host, int port, String relativeUrl, Dictionary<String, String> param)
        {
            String absUrl = BuildUrl(host, port);
            return Post(absUrl, relativeUrl, param);
        }

        /**
         * @param hostUrl
         * @param location
         * @param entity
         * @param <T>
         * @return
         * @throws Exception
         */
        public static String Post(String hostUrl, String location, object entity)
        {
            return doPost(hostUrl, location, entity, null, null);
        }

        /**
         * @param hostUrl
         * @param location
         * @return
         */
        public static String BuildUrl(String hostUrl, String location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return hostUrl;
            }
            return hostUrl + location;
        }

        /**
         * @param hostUrl
         * @param location
         * @return
         */
        public static String BuildGetUrl(String hostUrl, String location, Dictionary<String, String> param)
        {
            String url = BuildUrl(hostUrl, location);
            if (param != null && param.Any())
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(url);
                stringBuilder.Append("?");
                foreach (var data in param)
                {
                    if (data.Key != param.First().Key)
                    {
                        stringBuilder.Append("&");
                    }
                    stringBuilder.Append(data.Key);
                    stringBuilder.Append("=");
                    stringBuilder.Append(data.Value);
                }
                return stringBuilder.ToString();
            }
            return url;
        }

        /**
         * @return
         */
        public static void AddHeaders(HttpWebRequest httpRequest, Dictionary<String, String> header)
        {
            //httpRequest.Headers.Add("Accept-Encoding", "gzip");
            //httpRequest.Headers.Add("Pragma", "no-cache");
            //httpRequest.Headers.Add("User-Agent", "UserApi Http Client");
            //httpRequest.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            //httpRequest.addHeaders("Connection", "keep-alive");
            if (header != null && header.Any())
            {
                foreach (var data in header)
                {
                    httpRequest.Headers.Add(data.Key, data.Value);
                }
            }
        }

        /**
         * @param host
         * @param port
         * @return
         */
        public static String BuildUrl(String host, int port)
        {
            return "http://" + host + ":" + port;
        }


        #region 下载图片



        #endregion

    }

}
