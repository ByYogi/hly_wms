using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer
{
    public partial class addSaleOrder : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoOrderBus bus = new CargoOrderBus();
                CargoOrderEntity order = bus.QueryUnpaidOrder(UserInfor.LoginName);
                if (order != null  )
                {
                    if (order.Piece > 0)
                    {
                        UnpaidText.Text = "您有" + order.Piece + "笔订单金额" + order.TotalCharge + "元未结算&nbsp;&nbsp;&nbsp;";
                    }
                    this.RebateMoney.Value = order.RebateMoney.ToString();
                }
            }
        }
    }
}