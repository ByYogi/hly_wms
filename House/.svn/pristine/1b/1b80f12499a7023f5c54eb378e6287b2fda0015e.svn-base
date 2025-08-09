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
    public partial class qyApprovePurchaseCheck : QYBasePage
    {
        public CargoRealFactoryPurchaseOrderEntity wxOrder = new CargoRealFactoryPurchaseOrderEntity();
        public long OID = 0;
        public string PurchaseTypeName = string.Empty;
        public string ApplyStatus = string.Empty;
        public string CheckResult = string.Empty;
        public string ApplyName = string.Empty;
        public string IsCheck = string.Empty;
        public string ty = string.Empty;
        public string OrderType = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ApproveType = Convert.ToString(Request["ApproveType"]);
                string OrderCheckType = Convert.ToString(Request["OrderCheckType"]);
                ty = Convert.ToString(Request["ty"]);
                string PurOrderNo = Convert.ToString(Request["OrderNo"]);
                OID = Convert.ToInt64(Request["OID"]);
                if (OID.Equals(0)) { return; }
                OrderType = Convert.ToString(Request["OrderType"]);
                QiyeBus qy = new QiyeBus();
                QyOrderUpdatePriceEntity qyU = qy.QueryOrderUpdatePriceEntity(new QyOrderUpdatePriceEntity { OID = OID });
                if (qyU.OID.Equals(0)) { return; }
                ApplyStatus = qyU.ApplyStatus;
                CheckResult = qyU.CheckResult;
                ApplyName = qyU.ApplyName;
                if (!QyUserInfo.CheckRole.Contains(qyU.CheckID))
                {
                    IsCheck = "0";
                }
                CargoFinanceBus f = new CargoFinanceBus();
                List<CargoExpenseApproveRoutEntity> result = f.QueryExpenseRout(new CargoExpenseApproveRoutEntity { ExID = OID, ApproveType = ApproveType });
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
                CargoRealFactoryPurchaseOrderBus cargoReal = new CargoRealFactoryPurchaseOrderBus();
                if (OrderType.Equals("1"))
                {
                    //采购订单
                    CargoRealFactoryPurchaseOrderEntity orderEnt = cargoReal.QueryCargoRealFactoryPurchaseEntity(new CargoRealFactoryPurchaseOrderEntity { PurOrderNo = PurOrderNo });
                    if (!orderEnt.PurOrderID.Equals(0))
                    {
                        PurchaseTypeName = orderEnt.PurchaseType.Equals("0") ? "工厂采购单" : "市场采购单";
                        if (orderEnt.purchaseOrderGoodsEntities.Count > 0)
                        {
                            foreach (var it in orderEnt.purchaseOrderGoodsEntities)
                            {
                                string cp = it.PurchasePrice.ToString();
                                string spcTitle = it.TypeName + it.Specs + " " + it.LoadIndex.ToString() + it.SpeedLevel + "<br />" + it.Figure;

                                ltlGoods.Text += "<div class=\"weui-cell\" style=\"padding-left: 2px; padding-right: 10px; padding-top: 5px; padding-bottom: 5px;\"><div class=\"weui-cell__hd\" style=\"width: 35%\"><label class=\"weui-label\" style=\"width: 100%\">" + spcTitle + "</label></div><div class=\"weui-cell__hd\" style=\"width: 10%\">" + it.Piece.ToString() + "</div><div class=\"weui-cell__hd\" style=\"width: 15%\">￥" + cp + "</div><div class=\"weui-cell__hd\" style=\"width: 15%\">" + it.HouseName + "</div><div class=\"weui-cell__hd\" style=\"width: 15%\">￥" + 0 + "</div><div class=\"weui-cell__bd\" style=\"width: 15%;font-size:13px;font-weight:bolder;color:red;\">￥" + 0 + "</div></div>";
                            }
                        }
                    }
                }
            }
        }
    }
}