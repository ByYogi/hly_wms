using House.Entity.Cargo;
using House.Entity.Cargo.Order;
using NPOI.HSSF.Record.Formula.Eval;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Supplier.Order
{
    public partial class ToDayOrders : BasePage
    {
        /// <summary>
        /// 即日达订单List
        /// </summary>
        public List<CargoOrderEntity> ToDayOrdersEntityList
        {
            get
            {
                if (Session["ToDayOrdersEntityList"] == null)
                {
                    Session["ToDayOrdersEntityList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["ToDayOrdersEntityList"]);
            }
            set
            {
                Session["ToDayOrdersEntityList"] = value;
            }
        }

        /// <summary>
        /// 即日达订单明细表List
        /// </summary>
        public List<CargoOrderGoodsEntity> ToDayOrderGoodsEntityList
        {
            get
            {
                if (Session["ToDayOrderGoodsEntityList"] == null)
                {
                    Session["ToDayOrderGoodsEntityList"] = new List<CargoOrderGoodsEntity>();
                }
                return (List<CargoOrderGoodsEntity>)(Session["ToDayOrderGoodsEntityList"]);
            }
            set
            {
                Session["ToDayOrderGoodsEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出即日达订单主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (ToDayOrdersEntityList.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("开单时间", typeof(string));

            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("总价", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("收货人", typeof(string));
            table.Columns.Add("联系电话", typeof(string));

            table.Columns.Add("收货地址", typeof(string));
            table.Columns.Add("发货方式", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("订单状态", typeof(string));
            List<CargoPurchaseOrderEntity> tot = new List<CargoPurchaseOrderEntity>();
            int i = 0;
            foreach (var it in ToDayOrdersEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["开单时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["订单号"] = it.OrderNo.ToString();
                newRows["数量"] = it.Piece;
                newRows["总价"] = it.TransportFee.ToString();
                newRows["客户名称"] = it.AcceptUnit;
                newRows["收货人"] = it.AcceptPeople;
                newRows["联系电话"] = it.AcceptCellphone;
                newRows["收货地址"] = it.AcceptAddress.ToString();
                newRows["发货方式"] = GetDeliveryStr(it.DeliveryType);
                newRows["所属仓库"] = it.HouseName;
                newRows["订单状态"] = GetStatusStr(it.AwbStatus);
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "即日达订单数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        /// <summary>
        /// 导出即日达订单明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click2(object sender, EventArgs e)
        {
            if (ToDayOrderGoodsEntityList.Count <= 0) { return; }

            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("数量", typeof(int));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("货品代码", typeof(string));

            table.Columns.Add("载数", typeof(string));
            table.Columns.Add("产地", typeof(string));
            table.Columns.Add("批次", typeof(string));
            table.Columns.Add("实际销售价", typeof(string));
            table.Columns.Add("操作时间", typeof(string));
            List<CargoOrderGoodsEntity> tot = new List<CargoOrderGoodsEntity>();
            int i = 0;
            foreach (var it in ToDayOrderGoodsEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["品牌"] = it.TypeName;
                newRows["数量"] = it.Piece;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["货品代码"] = it.GoodsCode;
                newRows["载数"] = it.LoadSpeed;
                newRows["产地"] = it.Born.Equals("0") ? "国产" : "进口";
                newRows["批次"] = it.Batch;
                newRows["实际销售价"] = it.ActSalePrice.ToString();
                newRows["操作时间"] = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                table.Rows.Add(newRows);

            }
            ToExcel.DataTableToExcel(table, "", "即日达订单明细数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }



        /// <summary>
        /// 获取配送
        /// </summary>
        public string GetDeliveryStr(string status)
        {
            if (status.Equals("0")) { return "配送"; }
            if (status.Equals("1")) { return "自提"; }
            else { return string.Empty; }
        }
        /// <summary>
        /// 获取订单状态
        /// </summary>
        public string GetStatusStr(string status)
        {
            if (status.Equals("0")) { return "已下单"; }
            else if (status.Equals("1")) { return "出库中"; }
            else if (status.Equals("2")) { return "已出库"; }
            else if (status.Equals("3")) { return "已装车"; }
            else if (status.Equals("4")) { return "已到达"; }
            else if (status.Equals("5")) { return "已签收"; }
            else if (status.Equals("6")) { return "已拣货"; }
            else if (status.Equals("7")) { return "配送"; }
            else if (status.Equals("8")) { return "到货确认"; }
            else { return string.Empty; }
        }
    }
}