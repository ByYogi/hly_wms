using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using Memcached.ClientLibrary;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class AppTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemcachedClient memClient = new MemcachedClient();
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
            memClient.PoolName = ConfigurationSettings.AppSettings["PoolName"];
            memClient.EnableCompression = true;
            memClient.CompressionThreshold = 10240;
            #endregion
            //1.根据回传的URL获取 Code
            string code = Request["code"];
            Common.WriteTextLog(code);
            //2.获取Token
            OAuthAccessTokenResult token = OAuthApi.GetAccessToken(Common.GetdltOpenAPPID(), Common.GetdltOpenAppSecret(), code);
            Common.WriteTextLog(token.openid);
            Common.WriteTextLog(token.access_token);
            //3.根据Token和Code获取用户UserID
            OAuthUserInfo userInfo = OAuthApi.GetUserInfo(token.access_token, token.openid);
            Common.WriteTextLog(userInfo.unionid);
            CargoWeiXinBus bus = new CargoWeiXinBus();
            if (!bus.IsExistWeixinUser(new WXUserEntity { wxOpenID = userInfo.openid }))
            {
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "微信服务号";
                log.Status = "0";
                log.NvgPage = "新增用户";
                log.UserID = userInfo.openid;
                log.Operate = "A";
                bus.AddWeixinUser(new WXUserEntity
                {
                    wxOpenID = userInfo.openid,
                    wxName = userInfo.nickname,
                    Sex = userInfo.sex,
                    Province = userInfo.province,
                    City = userInfo.city,
                    IsCertific = "0",
                    AvatarBig = userInfo.headimgurl,
                    AvatarSmall = userInfo.headimgurl,
                    ConsumerPoint = 10,
                    UnionID = userInfo.unionid,
                    Country = userInfo.country
                }, log);
            }
            WXUserEntity entity = bus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = userInfo.openid });
            if (entity != null && !string.IsNullOrEmpty(entity.wxOpenID))
            {
                string dtoken = string.Empty;
                //实体转化为Json字符串
                string userJson = JSON.Encode(entity);
                //WriteTextLog(userJson);
                //MD5加密
                using (MD5 md5Hash = MD5.Create())
                {
                    dtoken = Common.GetMd5Hash(md5Hash, userJson);
                    entity.Dtoken = dtoken;
                }
                memClient.Add(dtoken, entity);
                //返回用户信息给APP
                String json = JSON.Encode(entity);
                Response.Clear();
                Response.Write(json);
            }

        }
    }
}