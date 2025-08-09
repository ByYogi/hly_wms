using House.Business.House;
using House.Entity;
using House.Entity.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace House.webSer
{
    /// <summary>
    /// userAPI 的摘要说明
    /// </summary>
    public class userAPI : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string cmd = context.Request["cmd"];
            MethodInfo Method = this.GetType().GetMethod(cmd, BindingFlags.NonPublic | BindingFlags.Instance);//通过反射机制,直接对应到相应的方法
            if (Method != null)
            {
                Method.Invoke(this, new object[] { context });
            }
            else
            {
                context.Response.Write("传入参数不正确");
            }
        }
        /// <summary>
        /// 通过登陆名，系统代码，部门代码查询该账号所拥有系统的权限，返回JSON
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="loginPwd">登陆密码</param>
        /// <param name="houseCode">系统代码</param>
        /// <param name="depCode">部门名称</param>
        private void queryUserPermission(HttpContext context)
        {
            string loginName = context.Request["loginName"];
            string loginPwd = context.Request["loginPwd"];
            string houseCode = context.Request["houseCode"];
            string depCode = context.Request["depCode"];
            SystemBus bus = new SystemBus();
            userAPIMessage result = new userAPIMessage();
            result.Result = true;
            result.Message = "登陆成功";
            if (string.IsNullOrEmpty(loginName))
            {
                result.Result = false;
                result.Message = "登陆名不能为空";
            }
            if (string.IsNullOrEmpty(loginPwd))
            {
                result.Result = false;
                result.Message = "登陆密码不能为空";
            }
            if (string.IsNullOrEmpty(houseCode))
            {
                result.Result = false;
                result.Message = "系统代码不能为空";
            }
            if (!bus.IsExistUser(new SystemUserEntity { LoginName = loginName }))
            {
                result.Result = false;
                result.Message = "登陆名不存在";
            }
            if (!bus.IsExistHouse(new SystemSetEntity { HouseCode = houseCode }))
            {
                result.Result = false;
                result.Message = "系统代码不存在";
            }
            SystemUserEntity userEn = new SystemUserEntity();
            int ck = bus.CheckUserLogin(loginName, loginPwd, out userEn);
            switch (ck)
            {
                case 1:
                    result.Result = false;
                    result.Message = "登陆名不正确或不存在";
                    break;
                case 3:
                    result.Result = false;
                    result.Message = "登陆密码不正确";
                    break;
                default:
                    if (!string.IsNullOrEmpty(depCode))
                    {
                        if (!userEn.DepCName.Equals(depCode))
                        {
                            result.Result = false;
                            result.Message = "传入部门名称和账号所在部门不匹配";
                        }
                    }
                    break;
            }

            if (result.Result)
            {
                List<SystemItemEntity> itemList = bus.QueryItemFormat(loginName, houseCode);
                if (itemList.Count <= 0)
                {
                    result.Result = false;
                    result.Message = "账号没有赋予该系统权限";
                }
                else
                {
                    result.itemList = itemList;
                    result.userEnt = userEn;
                }
            }
            //JSON
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 通过部门代码查询该部门所有用户信息
        /// </summary>
        /// <param name="context"></param>
        private void queryUserInfoByDepCode(HttpContext context)
        {
            string depCode = context.Request["depCode"];
            int hid = !string.IsNullOrEmpty(context.Request["houseID"]) ? Convert.ToInt32(context.Request["houseID"]) : 0;
            SystemBus bus = new SystemBus();
            userAPIMessage result = new userAPIMessage();
            result.Result = true;
            result.Message = "查询成功";

            List<SystemUserEntity> userList = bus.GetUserByDepCode(depCode, hid);
            if (userList.Count <= 0)
            {
                result.Result = false;
                result.Message = "部门代码有误";
            }
            else
            {
                result.userList = userList;
            }
            //JSON
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 根据分公司代码查询该公司下面所有员工
        /// </summary>
        /// <param name="context"></param>
        private void queryUserInfoByCode(HttpContext context)
        {
            string depCode = context.Request["depCode"];
            SystemBus bus = new SystemBus();
            userAPIMessage result = new userAPIMessage();
            result.Result = true;
            result.Message = "查询成功";

            List<SystemUserEntity> userList = bus.GetUserByDepCode(depCode);
            if (userList.Count <= 0)
            {
                result.Result = false;
                result.Message = "部门代码有误";
            }
            else
            {
                result.userList = userList;
            }
            //JSON
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="context"></param>
        private void UpdatePassword(HttpContext context)
        {
            string loginName = context.Request["loginName"];
            string loginPwd = context.Request["loginPwd"];

            userAPIMessage result = new userAPIMessage();
            result.Result = true;
            result.Message = "修改成功";
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "公司内部管理";
            log.NvgPage = "密码管理";
            log.UserID = loginName;
            log.Status = "0";
            log.Operate = "U";

            SystemUserEntity ent = new SystemUserEntity();
            ent.LoginName = loginName;
            ent.LoginPwd = Common.EncodePassword(loginPwd);
            SystemBus bus = new SystemBus();
            int re = bus.UpdateUserPwd(ent, log);
            if (re.Equals(1))
            {
                result.Message = "用户名不存在";
                result.Result = false;
            }
            //JSON
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        /// <summary>
        /// 通过用户ID查询该用户的信息
        /// </summary>
        /// <param name="context"></param>
        private void queryUserInfoByUserID(HttpContext context)
        {
            userAPIMessage result = new userAPIMessage();
            result.Result = true;
            result.Message = "查询成功";

            string loginName = context.Request["LoginName"];
            if (string.IsNullOrEmpty(loginName))
            {
                result.Result = false;
                result.Message = "用户名有误";
            }
            if (result.Result)
            {
                SystemBus bus = new SystemBus();

                SystemUserEntity userEn = bus.ReturnUserName(loginName);
                if (userEn == null || string.IsNullOrEmpty(userEn.LoginName))
                {
                    result.Result = false;
                    result.Message = "用户数据不存在";
                }
                else
                {
                    result.userEnt = userEn;
                }
            }
            //JSON
            String json = JSON.Encode(result);
            context.Response.Write(json);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}