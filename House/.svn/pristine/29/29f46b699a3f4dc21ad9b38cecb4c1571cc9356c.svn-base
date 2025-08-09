using House.Business.Cargo;
using House.Business;
using House.Entity.Cargo;
using House.Entity.House;
using House.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Supplier
{
    public partial class FormService : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string methodName = string.Empty;
            try
            {
                methodName = Request["method"];
                if (String.IsNullOrEmpty(methodName)) return;
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(this, null);
            }
            catch (Exception ex)
            {
                LogBus bus = new LogBus();
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Operate = "";
                log.Moudle = "";
                log.Status = "1";
                log.NvgPage = "";
                log.UserID = UserInfor == null ? "" : UserInfor.LoginName.Trim();
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
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
                String chkSave = Convert.ToString(Request["chkSave"]);
                if (string.IsNullOrEmpty(UserName))
                {
                    msg.Message = "用户名不能为空";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Clear();
                    Response.Write(json);
                    Response.End();
                    return;
                }
                if (string.IsNullOrEmpty(Pwd))
                {
                    msg.Message = "密码不能为空";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Clear();
                    Response.Write(json);
                    Response.End();
                    return;
                }
                if (Pwd.Trim().Length < 8)
                {
                    msg.Message = "密码不正确";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Clear();
                    Response.Write(json);
                    Response.End();
                    return;
                }
                CargoClientBus bus = new CargoClientBus();
                CargoClientEntity client = bus.QueryCargoClient(Convert.ToInt32(UserName));
                if (!client.ClientID.Equals(0) && client.LoginPwd.Equals(Common.EncodePassword(Pwd)))
                {
                    if (string.IsNullOrEmpty(client.ClientTypeID) || string.IsNullOrEmpty(client.SettleHouseID))
                    {
                        msg.Message = "未授权使用系统";
                        json = JSON.Encode(msg);
                        Response.Clear();
                        Response.Write(json);
                        Response.End();
                        return;
                    }
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
                    CargoHouseBus house = new CargoHouseBus();
                    CargoHouseEntity houseEnt = house.QueryCargoHouseByID(client.HouseID);
                    Session["user"] = new SystemUserEntity { UserID = Convert.ToInt32(client.ClientID), LoginName = client.ClientNum.ToString(), UserUnit = client.ClientShortName, UserName = client.Boss, HouseID = client.HouseID, DepCity = houseEnt.DepCity, SaleManID = client.UserID, SaleManName = client.UserName, Address = client.Address, ClientTypeID = client.ClientTypeID, ClientTypeName = client.ClientTypeName, SettleHouseID = client.SettleHouseID, SettleHouseName = client.SettleHouseName, UpClientID = client.UpClientID, UpClientShortName = client.UpClientShortName };
                }
                else
                {
                    msg.Message = "用户名或密码不正确";
                    //返回处理结果
                    json = JSON.Encode(msg);
                    Response.Clear();
                    Response.Write(json);
                    Response.End();
                    return;
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            json = JSON.Encode(msg);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void ChangeClientPwd()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            try
            {
                if (msg.Result)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Request["LoginName"])))
                    {
                        msg.Message = "登陆名不能为空";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Request["Password"])))
                    {
                        msg.Message = "密码不能为空";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Request["NewPwd"])))
                    {
                        msg.Message = "确认密码不能为空";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (Convert.ToString(Request["Password"]).Length < 8)
                    {
                        msg.Message = "密码长度不足8位";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (Convert.ToString(Request["NewPwd"]).Length < 8)
                    {
                        msg.Message = "确认密码长度不足8位";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    if (!Convert.ToString(Request["Password"]).Trim().ToUpper().Equals(Convert.ToString(Request["NewPwd"]).Trim().ToUpper()))
                    {
                        msg.Message = "两次输入的密码必须一致";
                        msg.Result = false;
                    }
                }
                if (msg.Result)
                {
                    LogEntity log = new LogEntity();
                    log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                    log.Moudle = "客户管理";
                    log.NvgPage = "客户修改密码";
                    log.UserID = UserInfor.LoginName.Trim();
                    log.Operate = "U";
                    log.Status = "0";
                    CargoClientBus bus = new CargoClientBus();
                    int type = bus.ChangeClientPwd(new CargoClientEntity { ClientNum = Convert.ToInt32(Request["LoginName"]), LoginPwd = Common.EncodePassword(Convert.ToString(Request["NewPwd"]).Trim()) }, log);


                    if (type == 0)
                    {
                        msg.Result = true;
                        msg.Message = "修改成功";
                    }
                    else
                    {
                        msg.Result = false;
                        msg.Message = "修改失败";
                    }
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        public void CargoPermisionHouse()
        {
            CargoClientBus bus = new CargoClientBus();
            List<CargoSettleHouseEntity> list = new List<CargoSettleHouseEntity>();
            List<CargoSettleHouseEntity> List = bus.QuerySettleHouseList(new CargoSettleHouseEntity { ClientNum = Convert.ToInt32(UserInfor.LoginName) });
            foreach (var item in List)
            {
                list.Add(new CargoSettleHouseEntity { ID = item.ID, SettleHouseID= item.SettleHouseID, SettleHouseName = item.SettleHouseName, ClientTypeID=item.ClientTypeID,ClientTypeName=item.ClientTypeName });
            }
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void ChangeHouse()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            int ID = Convert.ToInt32(Request["ID"]);
            string SettleHouseName = Convert.ToString(Request["SettleHouseName"]);
            CargoClientBus bus = new CargoClientBus();
            msg.Result = true;
            msg.Message = "切换成功";
            bus.UpdateClientSettleHouse(new CargoSettleHouseEntity { ID = ID });
            CargoSettleHouseEntity info = bus.QuerySettleHouseInfo(new CargoSettleHouseEntity { ID = ID });
            SystemUserEntity us = (SystemUserEntity)Session["user"];
            us.ClientTypeID = info.ClientTypeID;
            us.ClientTypeName = info.ClientTypeName;
            us.SettleHouseID = info.SettleHouseID;
            us.SettleHouseName = info.SettleHouseName;
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Clear();
            Response.Write(res);
            Response.End();
        }
    }
}