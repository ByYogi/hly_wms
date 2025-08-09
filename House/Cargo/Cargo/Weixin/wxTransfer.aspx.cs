using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class wxTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.根据回传的URL获取 Code
            string code = Request["code"];
            try
            {
                //2.获取Token
                OAuthAccessTokenResult token = OAuthApi.GetAccessToken(Common.GetdltAPPID(), Common.GetdltAppSecret(), code);
                //3.根据Token和Code获取用户UserID
                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(token.access_token, token.openid);
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
                    WXUserEntity wxUser = bus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = userInfo.openid });
                    if (wxUser.ID.Equals(0))
                    {
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
                            Country = userInfo.country,
                            UserType = "2"
                        }, log);
                    }
                    else
                    {
                        //修改进去 
                        bus.UpdateWeixinUserByUnionID(new WXUserEntity
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
                            Country = userInfo.country,
                            UserType = "2"
                        });
                    }

                }
                Common.WriteTextLog("0001 wxTransfer:"+ userInfo.openid);
                WXUserEntity entity = bus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = userInfo.openid });
                Common.WriteTextLog("0002 wxTransfer:" + entity.wxOpenID);
                if (entity != null && !string.IsNullOrEmpty(entity.wxOpenID))
                {
                    Common.WriteTextLog("0003 wxTransfer:");
                    Session["WeixinUser"] = entity;
                    Response.Redirect(Session["To_Url"].ToString());
                }
                else
                {
                    Response.Redirect("../Weixin/Error.aspx");
                }
            }
            catch (Exception ex)
            {
                //WriteTextLog(ex.Message, DateTime.Now);
            }
        }
    }
}