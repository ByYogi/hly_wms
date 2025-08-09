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
    public partial class myConsume : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoWeiXinBus bus = new CargoWeiXinBus();
                List<WXUserPointEntity> point = bus.QueryWXUserPoint(new WXUserPointEntity { WXID = WxUserInfo.ID });
                if (point.Count > 0)
                {
                    foreach (var it in point)
                    {
                        string jj = it.CutPoint.Equals("0") ? "+" : "-";
                        string jl = string.Empty;
                        switch (it.PointType)
                        {
                            case "1": jl = "关注公众号"; break;
                            case "2": jl = "每日签到"; break;
                            case "3": jl = "商城购物"; break;
                            case "5": jl = "删除订单"; break;
                            default:
                                break;
                        }
                        ltlPoint.Text += "<div class=\"weui-cell\"><div class=\"weui-cell__bd\" style=\"font-size:12px;\"><p>" + jj + it.Point.ToString() + "</p></div><div class=\"weui-cell__ft\" style=\"font-size:12px;color: #FF7F24\">" + jl + "</div><div class=\"weui-cell__hd\" style=\"margin-left: 50px; height: 20px;font-size:12px;\">" + it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss") + "</div></div>";
                    }
                }
            }
        }
    }
}