using House.Business.Cargo;
using House.Entity.Cargo;
using House.Entity;
using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Cargo.MiniPro
{
    public partial class MiniProSer : System.Web.UI.Page
    {
        //慧采云仓小程序配置信息
        private static string appid = Common.GetHCYCAppID();
        private static string appsecret = Common.GetHCYCAppSecret();
        private static string Acctoken = string.Empty;
        public static void WriteTextLog(string strMessage)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + DateTime.Now.ToString("yyyy-MM-dd") + ".System.txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + DateTime.Now.ToString() + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }
        //缓存
        protected MemcachedClient mc = new MemcachedClient();
        protected void Page_Load(object sender, EventArgs e)
        { //string DType = Convert.ToString(Request.Headers["dtype"]);
            //if (string.IsNullOrEmpty(DType)) { WriteTextLog("缓存无值"); return; }
            #region 缓存
            string[] serverlist = ConfigurationSettings.AppSettings["memcachedServer"].Split('/');
            SockIOPool pool = SockIOPool.GetInstance(ConfigurationSettings.AppSettings["PoolName"]);
            pool.SetServers(serverlist);
            pool.InitConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["InitConnections"]);//连接池初始容量
            pool.MinConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MinConnections"]);//最小容量
            pool.MaxConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MaxConnections"]);//最大容量
            pool.SocketConnectTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketConnectTimeout"]);//数据读取超时时间
            pool.SocketTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketTimeout"]);//Socket连接超时时间
            pool.MaintenanceSleep = Convert.ToInt64(ConfigurationSettings.AppSettings["MaintenanceSleep"]);//线程池维护线程之间的休眠时间
            pool.Failover = Convert.ToBoolean(ConfigurationSettings.AppSettings["Failover"]);//使用缓存服务器自动切换功能，当一台服务器死了可以自动切换到另外一台查找缓存

            pool.Nagle = Convert.ToBoolean(ConfigurationSettings.AppSettings["Nagle"]);//禁用Nagle算法
            pool.Initialize();
            mc.PoolName = ConfigurationSettings.AppSettings["PoolName"];
            mc.EnableCompression = true;
            mc.CompressionThreshold = 10240;
            #endregion
            string methodName = string.Empty;
            try
            {
                methodName = Request["method"];
                if (String.IsNullOrEmpty(methodName)) return;
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(this, null);
            }
            catch (Exception ex) { }

        }
        /// <summary>
        /// 反序列化包含openid和sessionkey的json数据包
        /// </summary>
        /// <param name="code">json数据包</param>
        /// <returns>包含openid和sessionkey的类</returns>
        public openidandsessionkey decodeopenidandsessionkey(wechatlogininfo logininfo)
        {

            openidandsessionkey oiask = JsonConvert.DeserializeObject<openidandsessionkey>(getopenidandsessionkeystring(logininfo.code));
            if (!string.IsNullOrEmpty(oiask.errcode))
                return null;
            return oiask;
        }
        /// <summary>
        /// 获取openid和sessionkey的json数据包
        /// </summary>
        /// <param name="code">客户端发来的code</param>
        /// <returns>json数据包</returns>
        private string getopenidandsessionkeystring(string code)
        {
            string temp = "https://api.weixin.qq.com/sns/jscode2session?" +
              "appid=" + appid
              + "&secret=" + appsecret
              + "&js_code=" + code
              + "&grant_type=authorization_code";
            return wxHttpUtility.GetData(temp);
        }
        private void CheckToken(HttpContext context)
        {
            AppletResultData weiChat = new AppletResultData();
            weiChat.code = 0;

            string DToken = context.Request["token"];
            if (string.IsNullOrEmpty(DToken)) { weiChat.code = 0; }
            if (mc.KeyExists(DToken))
            {
                weiChat.code = 1;
            }
            //if (TokenDic.ContainsKey(DToken))
            //{
            //    weiChat.code = 1;
            //}
            //else
            //{
            //    weiChat.code = 0;
            //}
            //if (string.IsNullOrEmpty(DToken)) { weiChat.code = 1; }
            //WXUserEntity wxUser = (WXUserEntity)mc.Get(DToken);
            //if (wxUser.ID.Equals(0)) { WriteTextLog("缓存无值"); return; }
            String json = JSON.Encode(weiChat);
            context.Response.Write(json);
        }
        /// <summary>
        /// 微信小程序登陆接口
        /// </summary>
        /// <param name="context"></param>
        public void MiniProLogin()
        { }
    }
}