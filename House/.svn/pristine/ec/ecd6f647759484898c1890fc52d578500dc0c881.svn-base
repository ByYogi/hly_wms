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
    public partial class orderPicekPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoOrderPickPlanGoodsEntity> QueryOrderPickPlanGoodsEntityList
        {
            get
            {
                if (Session["QueryOrderPickPlanGoodsEntityList"] == null)
                {
                    Session["QueryOrderPickPlanGoodsEntityList"] = new List<CargoOrderPickPlanGoodsEntity>();
                }
                return (List<CargoOrderPickPlanGoodsEntity>)(Session["QueryOrderPickPlanGoodsEntityList"]);
            }
            set
            {
                Session["QueryOrderPickPlanGoodsEntityList"] = value;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (QueryOrderPickPlanGoodsEntityList.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("产品名称", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("货位编码", typeof(string));
            table.Columns.Add("数量", typeof(string));
            table.Columns.Add("扫描数量", typeof(int));
            table.Columns.Add("扫描状态", typeof(string));
            table.Columns.Add("扫描人", typeof(string));
            table.Columns.Add("坑位号", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("客户联系人", typeof(string));
            table.Columns.Add("客户手机", typeof(string));
            int i = 0;
            string PickPlanNo = string.Empty;
            foreach (var it in QueryOrderPickPlanGoodsEntityList)
            {
                PickPlanNo = it.PickPlanNo;
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["订单号"] = it.OrderNo;
                newRows["产品名称"] = it.ProductName;
                newRows["货品代码"] = it.GoodsCode;
                newRows["货位编码"] = it.ContainerCode;
                newRows["数量"] = it.Piece;
                newRows["扫描数量"] = it.ScanPiece;
                newRows["扫描状态"] = it.ScanStatus.Equals("1") ? "已扫描" : it.ScanStatus.Equals("2") ? "扫描中" : "未扫描";
                newRows["扫描人"] = it.ScanUserID;
                newRows["坑位号"] = it.PitNum;
                newRows["客户名称"] = it.AcceptUnit;
                newRows["客户联系人"] = it.AcceptPeople;
                newRows["客户手机"] = it.AcceptCellphone;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "拣货计划" + PickPlanNo + "扫描明细");
        }
    }
}