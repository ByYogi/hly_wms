using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class wxQRCode : WXBasePage
    {
        public string PID = string.Empty;
        public string user = string.Empty;
        public string IsE = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PID = Convert.ToString(Request["PID"]);
                if (PID.Equals(WxUserInfo.ID.ToString()))
                {
                    IsE = "1";
                }
                else
                {
                    CargoWeiXinBus bus = new CargoWeiXinBus();
                    LogEntity log = new LogEntity();
                    log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                    log.Moudle = "微信服务号";
                    log.NvgPage = "绑定上级";
                    log.Status = "0";
                    log.Operate = "A";
                    log.UserID = WxUserInfo.wxOpenID;
                    bus.UpdateWxUserParentID(new WXUserEntity { ID = WxUserInfo.ID, ParentID = Convert.ToInt64(PID), wxOpenID = WxUserInfo.wxOpenID }, log);

                    WXUserEntity wx = bus.QueryWeixinUserByID(new WXUserEntity { ID = Convert.ToInt64(PID) });
                    user = wx.wxName;
                }
            }

        }
    }
}