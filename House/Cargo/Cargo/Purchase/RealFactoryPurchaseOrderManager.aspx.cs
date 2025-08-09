using House.Entity.Cargo;
using NPOI.HSSF.Record.Formula.Eval;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Purchase
{
    public partial class RealFactoryPurchaseOrderManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public List<CargoRealFactoryPurchaseOrderGoodsEntity> QueryAPList
        {
            get
            {
                if (Session["QueryAPList"] == null)
                {
                    Session["QueryAPList"] = new List<CargoRealFactoryPurchaseOrderGoodsEntity>();
                }
                return (List<CargoRealFactoryPurchaseOrderGoodsEntity>)(Session["QueryAPList"]);
            }
            set
            {
                Session["QueryAPList"] = value;
            }
        }
        //入库单类型、付款方式、转账账户、开单员、开单时间、审核状态

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (QueryAPList.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("采购单号", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("产品名称", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("载重指数", typeof(string));
            table.Columns.Add("速度级别", typeof(string));
            table.Columns.Add("采购数量", typeof(string));
            table.Columns.Add("回告数量", typeof(string));
            table.Columns.Add("采购价格", typeof(string));
            table.Columns.Add("入库数量", typeof(string));
            table.Columns.Add("入库时间", typeof(string));
            table.Columns.Add("预计入库时间", typeof(string));
            table.Columns.Add("入库状态", typeof(string));
            table.Columns.Add("采购仓库", typeof(string));
            table.Columns.Add("需求部门", typeof(string));
            table.Columns.Add("上传单据", typeof(string));
            table.Columns.Add("含税", typeof(string));
            table.Columns.Add("供应商", typeof(string));
            table.Columns.Add("采购单类型", typeof(string));
            table.Columns.Add("入库单类型", typeof(string));
            table.Columns.Add("付款方式", typeof(string));
            table.Columns.Add("转账账户", typeof(string));
            table.Columns.Add("开单员", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("审核状态", typeof(string));
            int i = 0;
            string OrderNo = string.Empty;
            foreach (var it in QueryAPList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["采购单号"] = it.PurOrderNo;
                newRows["品牌"] = it.TypeName;
                newRows["产品编码"] = it.ProductCode;
                newRows["产品名称"] = it.GoodsName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["货品代码"] = it.GoodsCode;
                newRows["载重指数"] = it.LoadIndex;
                newRows["速度级别"] = it.SpeedLevel;
                newRows["采购数量"] = it.Piece;
                newRows["回告数量"] = it.ReplyPiece;
                newRows["采购价格"] = it.PurchasePrice;
                newRows["入库数量"] = it.InPiece;
                newRows["入库时间"] = it.InHouseTime < Convert.ToDateTime("2000-01-01") ? "" : it.InHouseTime.ToString();;
                newRows["预计入库时间"] = it.ETATime<Convert.ToDateTime("2000-01-01")?"": it.ETATime.ToString();
                string InCargoStatusName = string.Empty;
                if (it.InCargoStatus == 0) InCargoStatusName = "未入库";
                else if (it.InCargoStatus == 1) InCargoStatusName = "已入库";
                else if (it.InCargoStatus == 2) InCargoStatusName = "部分入库";
                else InCargoStatusName = "";
                newRows["入库状态"] = InCargoStatusName;
                newRows["采购仓库"] = it.HouseName;
                newRows["需求部门"] = it.PurDepart;
                newRows["上传单据"] = it.IsDocument == 0 ? "否" : "是";
                newRows["含税"] = it.WhetherTax == 0 ? "不含税" : (it.WhetherTax == 1 ? "含税" : ""); ;
                newRows["供应商"] = it.PurchaserName;
                newRows["采购单类型"] = it.PurchaseType == 0 ? "工厂采购单" : (it.PurchaseType == 1 ? "市场采购单" : "");
                newRows["入库单类型"] = it.PurchaseInStoreType == 0 ? "入仓单" : (it.PurchaseType == 1 ? "流转单" : "");
                string PaymentMethodName = string.Empty;
                if (it.PaymentMethod == 0) PaymentMethodName = "月结";
                else if (it.PaymentMethod == 1) PaymentMethodName = "周结";
                else if (it.PaymentMethod == 2) PaymentMethodName = "现结";
                else PaymentMethodName = "";
                newRows["付款方式"] = PaymentMethodName;
                newRows["转账账户"] = it.TransferAccount == 0 ? "对公" : (it.TransferAccount == 1?"对私":"");
                newRows["开单员"] = it.CreateAwb;
                newRows["开单时间"] = Convert.ToDateTime(it.CreateDate).ToShortDateString();
                string ApplyStatusName = string.Empty;
                if (it.ApplyStatus == 0) { ApplyStatusName = "已提交"; }
                else if (it.ApplyStatus == 1) { ApplyStatusName = "已通过"; }
                else if (it.ApplyStatus == 2) { ApplyStatusName = "已拒审"; }
                else if (it.ApplyStatus == 3) { ApplyStatusName = "已结束"; }
                else { ApplyStatusName = ""; }
                newRows["审核状态"] = ApplyStatusName;
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "采购明细信息");
        }
    }
}