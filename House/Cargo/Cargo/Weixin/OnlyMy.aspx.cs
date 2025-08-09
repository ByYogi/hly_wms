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
    public partial class OnlyMy : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoWeiXinBus bus = new CargoWeiXinBus();
                WXTaobaoEntity entity = bus.QueryTodayTaobaoStatis(new WXTaobaoEntity { WXID = WxUserInfo.ID });
                ltlTodayNum.Text = entity.TodayOrderNum.ToString();
                ltlTC.Text = entity.TodayTiCheng.ToString();
                ltlFX.Text = entity.TodayFanLi.ToString();
            }
        }
    }
}