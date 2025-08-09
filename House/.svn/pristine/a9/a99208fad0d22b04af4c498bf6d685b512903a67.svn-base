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
    public partial class qyOrderSign : QYBasePage
    {
        public CargoOrderEntity order = new CargoOrderEntity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Convert.ToString(Request["OrderID"]))) { return; }
                Int64 orderID = Convert.ToInt64(Request["OrderID"]);

                CargoOrderBus bus = new CargoOrderBus();
                order = bus.QueryOrderInfoByOrderID(orderID);
                ltlOrderNo.Text = order.OrderNo + "---" + order.Dep + "---" + order.Dest;
                ltlAccept.Text = order.AcceptPeople;
                ltlAcceptCellphone.Text = order.AcceptCellphone;
                ltlAcceptAddress.Text = order.AcceptAddress;
                ltlPiece.Text = order.Piece.ToString();
                ltlCharge.Text = order.TotalCharge.ToString("F2");
                ltlSaleMan.Text = order.SaleManName;
                ltlCreateAwb.Text = order.CreateAwb + "---" + order.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}