using House.Entity.Cargo;
using House.Entity.Dto;
using NPOI.HSSF.Record.Formula.Eval;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.House
{
    public partial class PurchaseOrderManager : BasePage
    {
        public List<ExportPurchaseOrderDto> CargoPurchaseOrderEntityList
        {
            get
            {
                if (Session["CargoPurchaseOrderEntityList"] == null)
                {
                    Session["CargoPurchaseOrderEntityList"] = new List<ExportPurchaseOrderDto>();
                }
                return (List<ExportPurchaseOrderDto>)(Session["CargoPurchaseOrderEntityList"]);
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
            table.Columns.Add("进仓单号", typeof(string));
            table.Columns.Add("进仓仓库", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("合计费用", typeof(string));
            table.Columns.Add("收货状态", typeof(string));
            table.Columns.Add("开单员", typeof(string));
            table.Columns.Add("备注信息", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("产品编码", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("载速", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("进货数量", typeof(string));
            table.Columns.Add("收货数量", typeof(string));
            table.Columns.Add("成本价", typeof(string));
            table.Columns.Add("销售价", typeof(string));

            List<CargoOrderEntity> tot = new List<CargoOrderEntity>();
            int i = 0;
            foreach (var it in CargoPurchaseOrderEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["进仓单号"] = it.FacOrderNo;
                newRows["进仓仓库"] = it.HouseName;
                newRows["数量"] = it.Piece;
                newRows["合计费用"] = it.TotalCharge.ToString();
                newRows["收货状态"] = GetStatus(it.ReceivingStatus);
                newRows["开单员"] = it.CreateAwb;
                newRows["备注信息"] = it.Remark;
                newRows["开单时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["品牌"] = it.TypeName;
                newRows["产品编码"] = it.ProductCode;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["货品代码"] = it.GoodsCode;
                newRows["载速"] = it.LoadIndex + it.SpeedLevel;
                newRows["批次"] = it.Batch;
                newRows["进货数量"] = it.InPiece.ToString();
                newRows["收货数量"] = it.ReceivePiece.ToString();
                newRows["成本价"] = it.CostPrice.ToString();
                newRows["销售价"] = it.SalePrice.ToString();
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "进仓订单列表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        public string GetStatus(string val)
        {
            if (val == "0") { return "未收货"; }
            else if (val == "1") { return "已收货"; }
            else if (val == "2") { return "部分收货"; }
            else { return ""; }

        }
    }
}