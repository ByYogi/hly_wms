
using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using System;
using System.Reflection;
using System.Web;

namespace Supplier.systempage
{
    public partial class sysService : BasePage
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
        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        public void UpdateUserPwd()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = "";
            log.Moudle = "服务管理";
            log.Status = "0";
            log.NvgPage = "修改供应商客户密码";
            log.Operate = "U";
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
                    string pwd= Common.EncodePassword(Convert.ToString(Request["NewPwd"]).Trim());
                    //修改密码
                    CargoClientBus bus = new CargoClientBus();
                    bus.UpdateClentPwd(new CargoClientEntity { ClientNum = Convert.ToInt32(Request["LoginName"]), LoginPwd = pwd }, log);
                    msg.Result = true;
                    msg.Message = "修改成功";
                   
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

        }
        #endregion

    }
}