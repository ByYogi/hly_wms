using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using NPOI.HSSF.Record.Formula.Eval;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.House
{
    public partial class HouseReturnOrder : BasePage
    {
        /// <summary>
        /// 退仓单主表List
        /// </summary>
        public List<CargoPurchaseOrderEntity> CargoHouseReturnOrderEntityList
        {
            get
            {
                if (Session["CargoHouseReturnOrderEntityList"] == null)
                {
                    Session["CargoHouseReturnOrderEntityList"] = new List<CargoPurchaseOrderEntity>();
                }
                return (List<CargoPurchaseOrderEntity>)(Session["CargoHouseReturnOrderEntityList"]);
            }
            set
            {
                Session["CargoHouseReturnOrderEntityList"] = value;
            }
        }
        /// <summary>
        /// 退仓单明细表List
        /// </summary>
        public List<CargoPurchaseOrderGoodsEntity> HouseReturnOrderGoodsEntityList
        {
            get
            {
                if (Session["HouseReturnOrderGoodsEntityList"] == null)
                {
                    Session["HouseReturnOrderGoodsEntityList"] = new List<CargoPurchaseOrderGoodsEntity>();
                }
                return (List<CargoPurchaseOrderGoodsEntity>)(Session["HouseReturnOrderGoodsEntityList"]);
            }
            set
            {
                Session["HouseReturnOrderGoodsEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出退货单主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoHouseReturnOrderEntityList.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("退货单号", typeof(string));
            table.Columns.Add("总件数", typeof(int));
            table.Columns.Add("合计总费用", typeof(string));
            table.Columns.Add("退货员", typeof(string));
            table.Columns.Add("所在仓库", typeof(string));
            table.Columns.Add("收货状态", typeof(string));
            table.Columns.Add("退货备注", typeof(string));
            table.Columns.Add("退货时间", typeof(string));
            List<CargoPurchaseOrderEntity> tot = new List<CargoPurchaseOrderEntity>();
            int i = 0;
            foreach (var it in CargoHouseReturnOrderEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["退货单号"] = it.FacOrderNo.ToString();
                newRows["总件数"] = it.Piece;
                newRows["合计总费用"] = it.TotalCharge.ToString();
                newRows["退货员"] = it.CreateAwb;
                newRows["所在仓库"] = it.HouseName;
                newRows["收货状态"] = GetStatusStr(it.ReceivingStatus);
                newRows["退货备注"] = it.Remark;
                newRows["退货时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "仓库退货单数据表");
        }
        /// <summary>
        /// 导出退货单明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click2(object sender, EventArgs e)
        {
            if (HouseReturnOrderGoodsEntityList.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("退货单号", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("退货件数", typeof(int));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载数", typeof(string));
            table.Columns.Add("产地", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("销售价", typeof(string));
            List<CargoPurchaseOrderGoodsEntity> tot = new List<CargoPurchaseOrderGoodsEntity>();
            int i = 0;
            foreach (var it in HouseReturnOrderGoodsEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["退货单号"] = it.FacOrderNo.ToString();
                newRows["产品编码"] = it.ProductCode.ToString();
                newRows["品牌"] = it.TypeName;
                newRows["退货件数"] = it.ReturnPiece;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["载数"] = it.LoadSpeed;
                newRows["产地"] = it.Born == 0 ? "国产" : "进口";
                newRows["批次"] = it.Batch;
                newRows["销售价"] = it.SalePrice;
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "仓库退货单产品明细数据表");
        }
        public string GetStatusStr(string status)
        {
            if (status.Equals("0")) { return "未收货"; }
            else if (status.Equals("1")) { return "已收货"; }
            else if (status.Equals("2")) { return "部分收货"; }
            else { return string.Empty; }
        }
    }
}