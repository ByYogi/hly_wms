using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Cargo
{
    public class WXBasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
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
            #endregion
            MemcachedClient mc = new MemcachedClient();
            mc.PoolName = ConfigurationSettings.AppSettings["PoolName"];
            mc.EnableCompression = true;
            mc.CompressionThreshold = 10240;
            string DType = Convert.ToString(Request.Headers["dtype"]);
            if (!string.IsNullOrEmpty(DType) && DType.ToUpper().Equals("APP"))
            {
                //APp请求过来 的
                //获取Token
                string DToken = Convert.ToString(Request.Headers["dtoken"]);
                WXUserEntity wu = (WXUserEntity)mc.Get(DToken);
                Session["WeixinUser"] = wu;
            }
            else
            {


                if (Session["WeixinUser"] == null)
                {
                    //WriteTextLog(Request.RawUrl, DateTime.Now);
                    Session["To_Url"] = ".." + Request.RawUrl;
                    string appID = Common.GetdltAPPID();
                    string reUrl = Senparc.Weixin.HttpUtility.RequestUtility.UrlEncode(Common.GetdltTransfer_Url());

                    string url = @"https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appID + "&redirect_uri=" + reUrl + "&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
                    Response.Redirect(url);
                }
                //else
                //{
                //    CargoWeiXinBus bus = new CargoWeiXinBus();
                //    WXUserEntity entity = bus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = WxUserInfo.wxOpenID });
                //    //if (entity.HouseID.Equals(1))
                //    //{
                //    //    //关闭湖南仓库微信商城
                //    //    Response.Redirect("CloseSite.aspx");
                //    //}
                //    Session["WeixinUser"] = entity;
                //    string raw = Request.RawUrl;
                //    if (!raw.Contains("secKill") && !raw.Contains("wxQRCode") && !raw.Contains("ScanQrPayOrder"))
                //    {
                //        if (entity.ClientNum.Equals(0))
                //        {
                //            if (!raw.Contains("myAPI") && !raw.Contains("Only") && !raw.Contains("taobao") && !raw.Contains("chelunguan"))
                //            {
                //                if (!bus.IsBindingClientNum(new WXUserEntity { wxOpenID = WxUserInfo.wxOpenID }))
                //                {
                //                    Response.Redirect("WeiXinBindingClient.aspx");
                //                }
                //            }
                //        }
                //    }

                //}
            }

            //CargoWeiXinBus bus = new CargoWeiXinBus();
            //WXUserEntity entity = bus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = "okt9_5oTsjUBEelCCPrBNqBgqoHE" });
            //Session["WeixinUser"] = entity;
            //if (entity.ClientNum.Equals(0))
            //{
            //    string raw = Request.RawUrl;
            //    if (!raw.Contains("myAPI"))
            //    {
            //        CargoWeiXinBus bus = new CargoWeiXinBus();

            //        if (!bus.IsBindingClientNum(new WXUserEntity { wxOpenID = WxUserInfo.wxOpenID }))
            //        {
            //            Response.Redirect("WeiXinBindingClient.aspx");
            //        }
            //    }
            //}

            base.OnLoad(e);
            //WriteTextLog("0", DateTime.Now);
            //CheckSeeion();
        }

        //检测Session是否存在,如果当前登入用户已超时则自动登入
        protected void CheckSeeion()
        {
            //HttpCookie Cookie = CookiesHelper.GetCookie("UserLoginInfo");
            //if (Cookie == null)
            //{
            //    Server.Transfer("/Default.aspx");
            //}
            //UserName = Convert.ToString(Cookie["loginid"]);
            //if (!mc.KeyExists(UserName))
            //{
            //    Server.Transfer("/Default.aspx");
            //    //mc.Set("user", userInfo);
            //}

            if (Session["WeixinUser"] == null)
            {
                Response.Write("<script>top.location.href=\"/Default.aspx\"</script>");
                //Server.Transfer("/Default.aspx");
            }


            //if (Session["user"] == null || Cookie == null || Cookie["loginid"] == null)
            //{
            //    Server.Transfer("/Default.aspx");
            //}
        }
        //<summary>
        //获取当前登录用户信息
        // </summary>
        protected WXUserEntity WxUserInfo
        {
            get { return (WXUserEntity)Session["WeixinUser"]; }
            //get { return (SystemUserEntity)mc.Get(UserName); }
        }
    }
}