using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using Memcached.ClientLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo
{
    public partial class FormService : System.Web.UI.Page
    {
        //缓存
        //protected MemcachedClient mc = new MemcachedClient();
        public static void WriteTextLog(string strMessage, DateTime time)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + time.ToString("yyyy-MM-dd") + ".System.txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
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
        protected void Page_Load(object sender, EventArgs e)
        {
            //#region 缓存
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
            //mc.EnableCompression = true;
            //mc.CompressionThreshold = 10240;
            //#endregion
            String methodName = Request["method"];
            if (String.IsNullOrEmpty(methodName)) return;

            //invoke method
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);
            
            method.Invoke(this, null);
        }
        public void Login()
        {
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Operate = "L";
            log.Moudle = "用户登陆";
            log.Status = "0";
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            string json = string.Empty;
            try
            {
                //进行数据处理
                String UserName = Convert.ToString(Request["uname"]);
                String Pwd = Convert.ToString(Request["upwd"]).Trim();
                String Code = Convert.ToString(Request["ucode"]).Trim();
                String chkSave = Convert.ToString(Request["chkSave"]);
                if (string.IsNullOrEmpty(UserName))
                {
                    msg.Message = "用户名不能为空";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Write(json);
                    return;
                }
                if (string.IsNullOrEmpty(Pwd))
                {
                    msg.Message = "密码不能为空";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Write(json);
                    return;
                }
                if (Pwd.Trim().Length < 8)
                {
                    msg.Message = "密码不正确";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Write(json);
                    return;
                }
#if !DEBUG
                if (!ValidateCode.GetCheckResult("CheckCode", Code.Trim()))
                {
                    msg.Message = "验证码不正确";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Write(json);
                    return;
                }
#endif
                log.UserID = UserName;
                string cqd = string.Format("cmd={0}&loginName={1}&loginPwd={2}&houseCode={3}", "queryUserPermission", UserName, Pwd, "CAG");
                string result = wxHttpUtility.SendHttpRequest(Common.GethouseAPI(), cqd);

                userAPIMessage rows = Newtonsoft.Json.JsonConvert.DeserializeObject<userAPIMessage>(result);
                if (rows.Result)
                {
                    msg.Result = true;
                    msg.Message = "登陆成功";
                    #region 设置Cookie
                    HttpCookie Cookie = CookiesHelper.GetCookie("UserLoginInfo");
                    if (Cookie != null && Cookie["chkSave"] != null && Cookie["loginid"] != null)
                    {
                        CookiesHelper.SetCookie("UserLoginInfo", "loginid", UserName, DateTime.Now.AddDays(-1));
                        CookiesHelper.SetCookie("UserLoginInfo", UserName, Pwd, DateTime.Now.AddDays(-1));
                    }
                    Cookie = new HttpCookie("UserLoginInfo");
                    Cookie.Values.Add("loginid", UserName);
                    Cookie.Values.Add(UserName, Pwd);
                    if (chkSave.Equals("1"))
                    {
                        Cookie.Expires = DateTime.Now.AddYears(100);//cookies有效时间一年
                    }
                    else
                    {
                        Cookie.Expires = DateTime.Now;//cookies有效时间为浏览器进程
                    }
                    Cookie.Values.Add("chkSave", chkSave.ToString().ToUpper());
                    CookiesHelper.AddCookie(Cookie);
                    #endregion
                    CargoHouseBus bus = new CargoHouseBus();
                    CargoHouseEntity hent = bus.QueryCargoHouseByID(rows.userEnt.HouseID);

                    if (hent != null)
                    {
                        rows.userEnt.PickTitle = hent.PickTitle;
                        rows.userEnt.SendTitle = hent.SendTitle;
                        rows.userEnt.DepCity = hent.DepCity;
                    }
                    Session["user"] = rows.userEnt;
                    Session["List"] = rows.itemList;

                    log.UserID = UserName.Trim();
                }
                else
                {
                    msg.Message = rows.Message;
                }

            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            json = JSON.Encode(msg);
            Response.Write(json);
        }
        /// <summary>
        /// 获取新订单通知
        /// </summary>
        public void NewOrderNotice()
        {
            string ln = string.Empty;
            HttpCookie Cookie = CookiesHelper.GetCookie("UserLoginInfo");
            if (Cookie != null)
            {
                ln = Cookie.Values["loginid"];
            }
            //Common.WriteTextLog("缓存用户：" + ln);
            string noti = "";
            //string noti = RedisHelper.GetString("NewOrderNotice");
            if (!string.IsNullOrEmpty(ln))
            {
                noti = Convert.ToString(RedisHelper.GetString("NewOrderNotice" + "_" + ln));
                if (!string.IsNullOrEmpty(noti))
                {
                    RedisHelper.KeyDelete("NewOrderNotice" + "_" + ln);
                    // mc.Delete("NewOrderNotice" + "_" + ln);
                }
            }
            Response.Write(noti);
            ////RedisHelper.SetString()
            //CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
            //cargoNewOrder.HouseName = "东平云仓";
            //cargoNewOrder.OrderNo = "201401010001";
            //cargoNewOrder.OrderNum = "2";
            //cargoNewOrder.ClientInfo = "泰乐 华笙 广东省广州市白云区东平加油站左侧";
            //cargoNewOrder.ProductInfo = "马牌 215/55R16 CC6 98V";
            //cargoNewOrder.DeliveryName = "自提";
            //cargoNewOrder.ReceivePeople = "";

            //string json = JSON.Encode(cargoNewOrder);
            //RedisHelper.SetString("NewOrderNotice", json);
        }



    }
}