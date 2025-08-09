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
    public partial class qyExpenseApprove : QYBasePage
    {
        public CargoExpenseEntity expenseInfo = new CargoExpenseEntity();
        public string IsCheck = string.Empty;
        public string ty = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long ExID = 0;
                ExID = Convert.ToInt64(Request["ExID"]);
                ty = Convert.ToString(Request["ty"]);
                if (ExID.Equals(0)) { return; }
                CargoFinanceBus bus = new CargoFinanceBus();
                expenseInfo = bus.GetExpenseById(ExID);
                if (expenseInfo.Status=="3")
                {
                    return;
                }
                if (!QyUserInfo.CheckRole.Contains(expenseInfo.NextCheckID))
                {
                    IsCheck = "0";
                }
                List<CargoExpenseApproveRoutEntity> result = bus.QueryExpenseRout(new CargoExpenseApproveRoutEntity { ExID = ExID, ApproveType = "0" });
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
                if (expenseInfo.exDetail.Count>0)
                {
                    foreach (var item in expenseInfo.exDetail)
                    {
                        ltlGoods.Text += "<div class=\"weui-cell\" style=\"padding-left: 2px; padding-right: 10px; padding-top: 5px; padding-bottom: 5px;\"><div class=\"weui-cell__hd\" style=\"width: 2%\"></div><div class=\"weui-cell__hd\" style=\"width: 15%\"><label class=\"weui-label\" style=\"width: 100%\">" + item.ZName + "</label></div><div class=\"weui-cell__hd\" style=\"width: 25%\">" + item.FName.ToString() + "</div><div class=\"weui-cell__hd\" style=\"width: 40%\">" + item.Memo.ToString() + "</div><div class=\"weui-cell__bd\" style=\"width: 15%;font-size:13px;font-weight:bolder;color:red;\">￥" + item.DetailCharge.ToString("F2") + "</div></div>";
                    }
                }
            }
        }
    }
}