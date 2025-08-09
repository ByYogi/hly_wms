using House.Entity.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dealer
{
    public class BasePage : System.Web.UI.Page
    {
        //protected MemcachedClient mc = new MemcachedClient();

        protected override void OnLoad(EventArgs e)
        {
            //string[] serverlist = ConfigurationSettings.AppSettings["memcachedServer"].Split('/');
            //SockIOPool pool = SockIOPool.GetInstance(ConfigurationSettings.AppSettings["PoolName"]);
            //pool.SetServers(serverlist);
            //pool.InitConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["InitConnections"]);//连接池初始容量
            //pool.MinConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MinConnections"]);//最小容量
            //pool.MaxConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MaxConnections"]);//最大容量
            //pool.SocketConnectTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketConnectTimeout"]);//数据读取超时时间
            //pool.SocketTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketTimeout"]);//Socket连接超时时间
            //pool.MaintenanceSleep = Convert.ToInt64(ConfigurationSettings.AppSettings["MaintenanceSleep"]);//线程池维护线程之间的休眠时间
            //pool.Failover = Convert.ToBoolean(ConfigurationSettings.AppSettings["Failover"]);//使用缓存服务器自动切换功能，当一台服务器死了可以自动切换到另外一台查找缓存

            //pool.Nagle = Convert.ToBoolean(ConfigurationSettings.AppSettings["Nagle"]);//禁用Nagle算法
            //pool.Initialize();

            //mc.PoolName = ConfigurationSettings.AppSettings["PoolName"];
            //mc.EnableCompression = false;
            //检测Session是否存在
            //如果Session不存在,Cookies不存在,则返回登录页面
            // 如果Session不存在,Cookies存在,则自动登录
            CheckSeeion();
            base.OnLoad(e);
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

            if (Session["user"] == null)
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
        protected SystemUserEntity UserInfor
        {
            get { return (SystemUserEntity)Session["user"]; }
            //get { return (SystemUserEntity)mc.Get(UserName); }
        }
        protected string _UserName;
        protected string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
    }
}