using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Order
{
    public partial class ArrivalOrderManager : BasePage
    {
        public List<CargoOrderEntity> CargoPurchaseOrderEntityList
        {
            get
            {
                if (Session["CargoPurchaseOrderEntityList"] == null)
                {
                    Session["CargoPurchaseOrderEntityList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["CargoPurchaseOrderEntityList"]);
            }
            set
            {
                Session["CargoPurchaseOrderEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoPurchaseOrderEntityList.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("进货单号", typeof(string));
            table.Columns.Add("供应商名称", typeof(string));
            table.Columns.Add("供应商姓名", typeof(string));
            table.Columns.Add("订单类型", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("费用", typeof(string));
            table.Columns.Add("运输费用", typeof(string));
            table.Columns.Add("装卸费用", typeof(string));
            table.Columns.Add("合计费用", typeof(string));
            table.Columns.Add("开单员", typeof(string));
            table.Columns.Add("来货单号", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            List<CargoOrderEntity> tot = new List<CargoOrderEntity>();
            int i = 0;
            foreach (var it in CargoPurchaseOrderEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["进货单号"] = it.OrderID.ToString();
                newRows["供应商名称"] = it.AcceptUnit;
                newRows["供应商姓名"] = it.AcceptPeople;
                newRows["订单类型"] = it.TrafficType;

                newRows["数量"] = it.Piece;
                newRows["费用"] = it.TransportFee.ToString();
                newRows["运输费用"] = it.TransitFee.ToString();
                newRows["装卸费用"] = it.HandFee.ToString();
                newRows["合计费用"] = it.TotalCharge.ToString();
                newRows["开单员"] = it.CreateAwb;
                newRows["来货单号"] = it.FacOrderNo;
                newRows["开单时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "进货单数据表");
        }

    }
}