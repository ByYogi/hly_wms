using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    public partial class qyModifyPrice : QYBasePage
    {
        public WXOrderEntity wxOrder = new WXOrderEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string OrderNo = Convert.ToString(Request["OrderNo"]);
                CargoOrderBus bus = new CargoOrderBus();
                List<WXOrderEntity> wolist = bus.QueryWeixinOrder(new WXOrderEntity { OrderNo = OrderNo });
                if (wolist.Count > 0)
                {
                    wxOrder = wolist[0];
                }
                List<WXOrderManagerEntity> goods = bus.QueryWeixinOrderInfo(new WXOrderManagerEntity { OrderNo = OrderNo });
                if (goods.Count > 0)
                {
                    int Tnum = 0; decimal TCharge = 0.00M;
                    foreach (var it in goods)
                    {
                        //Tnum += it.OrderNum;
                        //TCharge += it.TotalCharge;
                        ltlGoods.Text += "<div class=\"weui-cell\" style=\"padding-left: 2px; padding-right: 10px; padding-top: 5px; padding-bottom: 5px;\"><div class=\"weui-cell__hd\" style=\"width: 60%\"><label class=\"weui-label\" style=\"width: 100%\">" + it.Title + "<br />周期：" + it.Batch + "</label></div><div class=\"weui-cell__hd\" style=\"width: 10%\">" + it.OrderNum.ToString() + "</div><div class=\"weui-cell__hd\" style=\"width: 15%\">￥" + it.OrderPrice.ToString() + "</div><div class=\"weui-cell__bd\" style=\"width: 15%\"><input class=\"weui-input\" placeholder=\"新价格\" onblur=\"bindModify(this)\"  id=" + it.ShelvesID + " data-num=\"" + it.OrderNum + "\" data-price=\"" + it.OrderPrice + "\" type=\"number\" style=\"border: 1px solid #95B8E7; border-radius: 5px 5px 5px 5px; padding-left: 4px;\" /></div></div>";
                    }

                    //ltlGoods.Text += "<div class=\"weui-cell\" style=\"padding-left: 0px; padding-right: 10px; padding-top: 5px; padding-bottom: 5px; font-weight: bold; color: red;\"><div class=\"weui-cell__hd\" style=\"width: 60%\"><label class=\"weui-label\" style=\"width: 100%\">&nbsp;&nbsp;&nbsp;&nbsp;汇总：</label></div><div class=\"weui-cell__hd\" style=\"width: 10%\">" + Tnum.ToString() + "</div><div class=\"weui-cell__hd\" style=\"width: 20%\">" + TCharge.ToString("F2") + "元</div><div class=\"weui-cell__bd\" style=\"width: 10%\"></div></div>";
                }
            }
        }
    }
}