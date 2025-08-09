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
    public partial class taobao : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoWeiXinBus bus = new CargoWeiXinBus();
                WXUserEntity result = bus.QueryMyParentEntity(new WXUserEntity { ID = WxUserInfo.ID });
                if (!result.ID.Equals(0))
                {
                    ltlParent.Text = "<span style='font-size:14px;'>我的上级微信名称：" + result.wxName + "</span>";
                }
            }
        }
    }
}