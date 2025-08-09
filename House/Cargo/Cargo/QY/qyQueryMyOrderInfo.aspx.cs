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
    public partial class qyQueryMyOrderInfo : QYBasePage
    {
        public CargoOrderEntity OrderEntity = new CargoOrderEntity();
        public string CheckOutType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderno = Convert.ToString(Request["OrderNo"]);
                if (string.IsNullOrEmpty(orderno)) { return; }
                CargoOrderBus bus = new CargoOrderBus();
                OrderEntity = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = orderno });
                if (OrderEntity.CheckOutType.Equals("0")) { CheckOutType = "现付"; }
                else if (OrderEntity.CheckOutType.Equals("1")) { CheckOutType = "周期&nbsp;" + OrderEntity.Remark.Substring(2, 1) + "天"; }
                else if (OrderEntity.CheckOutType.Equals("2")) { CheckOutType = "月结"; }
                else if (OrderEntity.CheckOutType.Equals("3")) { CheckOutType = "到付"; }
                else if (OrderEntity.CheckOutType.Equals("4")) { CheckOutType = "代收"; }

                ltlApproveDate.Text = OrderEntity.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                List<CargoContainerShowEntity> orderDetail = bus.QueryOrderByOrderNo(new CargoOrderEntity { OrderNo = orderno });
                if (orderDetail.Count > 0)
                {
                    string rela = "<div class=\"mui-content-padded\"><h4>关联数据</h4></div><ul class=\"mui-table-view mui-table-view-chevron\">";
                    rela += "<table style='padding:0px;width:100%;text-align:center;'><tr style='font-size:12px;'><th class='otd'>件数</th><th class='otd'>价格</th><th class='otd'>品牌</th><th class='otd'>规格</th><th class='otd'>花纹</th><th class='otd'>载速</th><th class='otd'>批次</th></tr>";
                    foreach (var it in orderDetail)
                    {
                        rela += "<tr style='font-size:12px;'><td class='otd'>" + it.Piece.ToString() + "</td><td class='otd'>" + it.ActSalePrice.ToString("F2") + "</td><td class='otd'>" + it.TypeName + "</td><td class='otd'>" + it.Specs + "</td><td class='otd'>" + it.Figure + "</td><td class='otd'>" + it.LoadIndex.ToString() + it.SpeedLevel + "</td><td class='otd'>" + it.Batch + "</td></tr>";
                    }
                    rela += "</table>";
                    //rela += "<li class=\"mui-table-view-cell\" style=\"padding-right: 20px;\"><div class=\"mui-pull-left\" style=\"font-size: 12px; color: #666666;\">件数&nbsp;&nbsp;价格&nbsp;&nbsp;品牌&nbsp;&nbsp;规格&nbsp;&nbsp;花纹&nbsp;&nbsp;载速&nbsp;&nbsp;批次</div></li>";
                    //foreach (var it in orderDetail)
                    //{
                    //    rela += "<li class=\"mui-table-view-cell\" style=\"padding-right: 20px;\"><div class=\"mui-pull-left\" style=\"font-size: 12px; color: #666666;\">" + it.Piece + "&nbsp;&nbsp;" + it.ActSalePrice.ToString("F2") + "&nbsp;&nbsp;" + it.TypeName + "&nbsp;&nbsp;" + it.Specs + "&nbsp;&nbsp;" + it.Figure + "&nbsp;&nbsp;" + it.LoadIndex.ToString() + it.SpeedLevel + "&nbsp;&nbsp;" + it.Batch + "</div></li>";
                    //}
                    //rela += "</ul>";

                    ltlRelate.Text = rela;
                }
            }
        }
    }
}