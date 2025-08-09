using Cargo.systempage;
using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NPOI.HSSF.Util.HSSFColor;

namespace Cargo.QY
{
    public partial class qyFactoryPurchaseCheck : QYBasePage
    {
        public CargoFactoryPurchaseOrderEntity orderEntity = new CargoFactoryPurchaseOrderEntity();
        public string CheckResult = string.Empty;
        public string IsCheck = string.Empty;
        public string ty = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ty = Convert.ToString(Request["ty"]);
                CargoOrderBus bus = new CargoOrderBus();
                orderEntity = bus.QueryFactoryPurchaseOrderInfo(new CargoFactoryPurchaseOrderEntity { OrderNo = Convert.ToString(Request["OrderNo"]) });
                if (orderEntity.OrderID.Equals(0))
                {
                    return;
                }
                if (!QyUserInfo.CheckRole.Contains(orderEntity.NextCheckID))
                {
                    IsCheck = "0";
                }
                CargoFinanceBus f = new CargoFinanceBus();
                List<CargoExpenseApproveRoutEntity> result = f.QueryExpenseRout(new CargoExpenseApproveRoutEntity { ExID = orderEntity.OrderID, ApproveType = "5" });
                string res = "<h3 style=\"color:#49b348;padding-left:5px;\">审批流程</h3><ul class=\"weui-comment\">";
                if (result.Count > 0)
                {
                    foreach (var it in result)
                    {
                        string opera = string.Empty;
                        switch (it.Opera)
                        {
                            case "0": opera = "<span class=\"mui-h5\">通过</span>"; break;
                            case "1": opera = "<span class=\"mui-h5\">拒审</span>"; break;
                            case "2": opera = "<span class=\"mui-h5\">结束</span>"; break;
                            case "3": opera = "<span class=\"mui-h5\">提交申请</span>"; break;
                            case "5": opera = "<span class=\"mui-h5\" style=\"color:#8ec160\">评论</span>"; break;
                            default: break;
                        }
                        res += "<li class=\"weui-comment-item\" style=\"padding-left:10px;border-bottom:1px solid #f5f5f5\"><div class=\"weui-comment-li\"><span class=\"check\" style=\"font-size:12px;padding-right:30px;\">" + opera + "</span></div><div class=\"userinfo\"><strong class=\"nickname\" style=\"font-size:12px;\">" + it.UserName + "</strong></div><div class=\"weui-comment-msg\" style=\"color:black;\">" + it.Result + "</div><p class=\"time\">" + it.OperaDate.ToString("yyyy-MM-dd HH:mm:ss") + "</p></li>";
                    }
                }
                res += "</ul>";
                ltlRoute.Text = res;
                List<CargoFactoryPurchaseOrderGoodsEntity> goodsList = bus.QueryFactoryPurchaseOrderGoods(new CargoFactoryPurchaseOrderEntity { OrderID = orderEntity.OrderID });
                if (goodsList.Count > 0)
                {
                    foreach (var it in goodsList)
                    {
                        ltlGoods.Text += "<div class=\"weui-cell\" style=\"padding-left: 2px; padding-right: 10px; padding-top: 5px; padding-bottom: 5px;\"><div class=\"weui-cell__hd\" style=\"width: 45%\"><label class=\"weui-label\" style=\"width: 100%\">" + it.TypeName +" "+ it.Specs + " " + it.Figure + "<br />" +it.GoodsCode + " " + it.LoadIndex + it.SpeedLevel + "</label></div><div class=\"weui-cell__hd\" style=\"width: 15%\">" + it.Piece + "</div><div class=\"weui-cell__hd\" style=\"width: 15%\">￥" + it.UnitPrice + "</div><div class=\"weui-cell__bd\" style=\"width: 15%;font-size:13px;font-weight:bolder;color:red;\">￥" + (it.UnitPrice * it.Piece).ToString("F2") + "</div></div>";
                    }
                }
            }
        }
    }
}