using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HouseServices
{
    public static class wxHttpUtility
    {
        // 对请求参数进行计算获取参数签名
        //private static String digest(String paramStr, String body, String timestamp, String nonce)
        //{
        //    paramStr = "(" + paramStr + ")(" + (body == null ? "" : body) + ")(" + timestamp + ")(" + nonce + ")";
        //    byte[] bytes = Encoding.UTF8.GetBytes(paramStr);
        //    byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

        //    StringBuilder builder = new StringBuilder();
        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        builder.Append(hash[i].ToString("x2"));
        //    }
        //    return builder.ToString();
        //}
        // 对请求参数进行计算获取参数签名
        private static String digest(String paramStr, String body, String timestamp, String nonce, String secret)
        {
            paramStr = "(" + paramStr + ")(" + (body == null ? "" : body) + ")(" + timestamp + ")(" + nonce + ")(" + secret + ")";
            //paramStr = "";
            byte[] bytes = Encoding.UTF8.GetBytes(paramStr);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                //int tempInt = ((int)hash[i]) & 0xff;
                //if (tempInt < 16)
                //{
                //    builder.Append("0");
                //}
                //builder.Append(hash[i].ToString("x6"));
                builder.Append(hash[i].ToString("x2"));
                //builder.Append(Convert.ToString(hash[i], 16));
            }
            return builder.ToString();
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受 
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string ContiSendPostHttpRequest(string url, string contentType, string requestData)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string tms = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            //WebHeaderCollection whc = new WebHeaderCollection();
            //whc.Add("ext-app-id", "DGZDLT001");
            //whc.Add("ext-app-secret", "58d99b2acb7e45209997967fe730adb3");
            //whc.Add("System-Code", "o_sync");
            //whc.Add("Timestamp", tms);
            //whc.Add("Nonce", "conti");
            //whc.Add("Signature", digest("", requestData, tms, "conti"));
            //request.Headers = whc;
            string nonce = "ContiDLQF";
            request.Headers.Add("ext-app-id", "DGZDLT001");
            request.Headers.Add("Timestamp", tms);
            request.Headers.Add("Nonce", nonce);
            string signature = digest("", requestData, tms, nonce, "9f810688923c4f7a887b281b32688ebf");
            request.Headers.Add("Signature", signature);
            request.ContentType = contentType;
            byte[] postBytes = null;
            postBytes = Encoding.UTF8.GetBytes(requestData);
            request.ContentLength = postBytes.Length;
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            string result = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }

                }
            }
            return result;
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="data">数据</param>
        public static string SendHttpRequest(string url, string data, string contentType = null)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/x-www-form-urlencoded";
            }
            return SendPostHttpRequest(url, contentType, data);
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="data">数据</param>
        public static string SendHttpRequest(string url, string data)
        {
            return SendPostHttpRequest(url, "application/x-www-form-urlencoded", data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetData(string url)
        {
            return SendGetHttpRequest(url, "application/x-www-form-urlencoded");
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string SendPostHttpRequest(string url, string contentType, string requestData)
        {
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            byte[] postBytes = null;
            request.ContentType = contentType;
            postBytes = Encoding.UTF8.GetBytes(requestData);
            request.ContentLength = postBytes.Length;
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            string result = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string SendGetHttpRequest(string url, string contentType)
        {
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = contentType;
            string result = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 云配开思推送配置
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string PostHttpRequest(string url, string contentType, string Data, string APISession)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            TimeSpan timeSpan = DateTime.UtcNow.AddMinutes(-5) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime = DateTime.Now;
            long timestamp = (long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string sign = "38476BFFE6274DBCAB38647884D467C6" + "cassec.sand.passive.data" + "gzshly60be866dbe52" + APISession + timestamp;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(sign);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                sign = sb.ToString().ToUpper();
            }
            request.Headers.Add("apiName", "cassec.sand.passive.data");
            request.Headers.Add("appKey", "gzshly60be866dbe52");
            request.Headers.Add("sign", sign);
            request.Headers.Add("session", APISession);
            request.Headers.Add("timestamp", timestamp.ToString());
            request.Method = "POST";
            request.ContentType = contentType;
            byte[] postBytes = null;
            postBytes = Encoding.UTF8.GetBytes(Data);
            request.ContentLength = postBytes.Length;
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }

                }
            }
            return result;


        }

        public static string NextDataPostHttpRequest(string url, string Data)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            TimeSpan timeSpan = DateTime.UtcNow.AddMinutes(-5) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime = DateTime.Now;
            long timestamp = (long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            request.Headers.Add("timestamp", timestamp.ToString());
            request.Headers.Add("access_token", "1199ab3cadbe0e47344db09ecd457cbb");
            //request.Headers.Add("User-Agent", "AOOZI.Robot");
            request.UserAgent = "AOOZI.Robot";
            request.Headers.Add("mch_id", "00020");
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] postBytes = null;
            postBytes = Encoding.UTF8.GetBytes(Data);
            request.ContentLength = postBytes.Length;
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }

                }
            }
            return result;


        }
    }
}
