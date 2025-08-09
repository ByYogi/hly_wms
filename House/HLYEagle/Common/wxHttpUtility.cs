using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HLYEagle
{
    public static class wxHttpUtility
    {
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
        /// 带有证书的Https的PostForm请求
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="postData"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string PostForm(string requestUri, NameValueCollection postData, CookieContainer cookie)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = cookie;

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string key in postData.Keys)
            {
                stringBuilder.AppendFormat("&{0}={1}", key, postData.Get(key));
            }
            byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();

        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受 
        }

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
            //whc.Add("ext-app-id", "mp0182394");
            //whc.Add("ext-app-secret", "58d99b2acb7e45209997967fe730adb3");
            //whc.Add("ext-app-id", "DGZDLT001");
            //whc.Add("ext-app-secret", "9f810688923c4f7a887b281b32688ebf");
            //whc.Add("System-Code", "o_sync");
            //whc.Add("Timestamp", tms);
            //string nonce = System.Guid.NewGuid().ToString();
            string nonce = "ContiDLQF";
            //whc.Add("Nonce", nonce);
            //whc.Add("Signature", digest("", requestData, tms, nonce, "9f810688923c4f7a887b281b32688ebf"));
            //whc.Add("Content-Type", contentType);
            //request.Headers = whc;
            request.Headers.Add("ext-app-id", "DGZDLT001");
            request.Headers.Add("Timestamp", tms);
            request.Headers.Add("Nonce", nonce);
            string signature = digest("", requestData, tms, nonce, "9f810688923c4f7a887b281b32688ebf");
            request.Headers.Add("Signature", signature);
            //request.ContentType = "application/json";
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
    }
}