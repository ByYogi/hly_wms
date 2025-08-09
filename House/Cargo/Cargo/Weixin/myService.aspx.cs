using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class myService :WXBasePage// System.Web.UI.Page
    {
        public WXUserEntity wxUser = new WXUserEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            wxUser = WxUserInfo;
            //Common.WriteTextLog("myService openid:" + wxUser.wxOpenID);
        }
    }
}