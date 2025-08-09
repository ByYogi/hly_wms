using House.Business.Cargo;
using House.Entity.Cargo;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;

namespace Cargo.APP
{
    /// <summary>
    /// AppHandler 的摘要说明
    /// </summary>
    public class AppHandler : IHttpHandler
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
        /// APP登陆接口 微信登陆和手机号登陆
        /// </summary>
        /// <param name="context"></param>
        private void AppLogin(HttpContext context)
        {
            string UnionID = Convert.ToString(context.Request["UnionID"]);
            string DevOpenID = Convert.ToString(context.Request["DevOpenID"]);
            CargoWeiXinBus bus = new CargoWeiXinBus();
            WXUserEntity wxUser = bus.QueryWeixinUserByOpendID(new WXUserEntity { UnionID = UnionID });
            //已经存在微信用户信息
            if (wxUser != null && !string.IsNullOrEmpty(wxUser.wxOpenID))
            {
                //Session["WeixinUser"] = wxUser;
                //实体转化为Json字符串
                string userJson = JSON.Encode(wxUser);
                //MD5加密
                using (MD5 md5Hash = MD5.Create())
                {
                    string dtoken = Common.GetMd5Hash(md5Hash, userJson);
                    wxUser.Dtoken = dtoken;
                }
            }
            else
            {
                //新的微信登陆用户
            }
            //返回用户信息给APP
            String json = JSON.Encode(wxUser);
            context.Response.Write(json);
        }
        private void QueryTypeStock(HttpContext context)
        {
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            string res = Convert.ToString(context.Request["key"]);
            res = res.ToUpper().Replace("/", "").Replace("R", "").Replace("C", "").Replace("F", "");
            if (!res.Contains("/") && !res.ToUpper().Contains("R"))
            {
                if (res.Length <= 3)
                {
                    queryEntity.Specs = res;
                }
                if (res.Length > 3 && res.Length <= 5)
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3);
                }
                if (res.Length > 5)
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, 2) + "R" + res.Substring(5, res.Length - 5);
                }
            }
            if (res.ToUpper().Contains("F"))
            {
                queryEntity.Specs = res;
                if (!res.ToUpper().Contains("/"))
                {
                    queryEntity.Specs = res.Substring(0, 3) + "/" + res.Substring(3, res.Length - 3);
                }
            }
            if (res.ToUpper().Contains("R") && res.ToUpper().Contains("C"))
            {
                queryEntity.Specs = res;
            }
            queryEntity.TypeID = Convert.ToInt32(context.Request["ProductTypeID"]);
            queryEntity.OnShelves = "0";
            //queryEntity.HouseID = WxUserInfo.HouseID;
            CargoProductBus bus = new CargoProductBus();
            Hashtable list = bus.QueryWeixinInHouseProduct(1, 1000, queryEntity);
            List<CargoContainerShowEntity> goods = list["rows"] as List<CargoContainerShowEntity>;
            //JSON
            String json = JSON.Encode(goods);
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