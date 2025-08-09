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

namespace Supplier.Report
{
    public partial class OrderDetails : BasePage
    {
        /// <summary>
        /// 订单销售List
        /// </summary>
        public List<CargoOrderEntity> OrderSaleEntityList
        {
            get
            {
                if (Session["OrderSaleEntityList"] == null)
                {
                    Session["OrderSaleEntityList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["OrderSaleEntityList"]);
            }
            set
            {
                Session["OrderSaleEntityList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出订单销售主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (OrderSaleEntityList.Count <= 0) { return; }

            DataTable table = new DataTable();
            table.Columns.Add("出库仓库", typeof(string));
            table.Columns.Add("订单类型", typeof(string));
            table.Columns.Add("订单号", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("送货方式", typeof(string));
            table.Columns.Add("订单数量", typeof(int));
            table.Columns.Add("订单金额", typeof(string));
            table.Columns.Add("服务费", typeof(string));
            table.Columns.Add("配送费", typeof(string));
            table.Columns.Add("优惠券", typeof(string));
            table.Columns.Add("应得金额", typeof(string));
            table.Columns.Add("订单状态", typeof(string));
            int i = 0;
            int TotalPiece = 0; decimal Rebate = 0.00M; decimal TotalSale = 0.00M; decimal TransitFee = 0.00M; decimal InsuranceFee = 0.00M;
            decimal TTTotalCharge = 0.00M;
            foreach (var it in OrderSaleEntityList)
            {
                i++;
                DataRow newRows = table.NewRow();
                newRows["出库仓库"] = it.HouseName;
                newRows["订单类型"] = GetOrderTypeStr(it.ThrowGood);
                newRows["订单号"] = it.OrderNo.ToString();
                newRows["开单时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["客户名称"] = it.AcceptUnit;
                newRows["送货方式"] = GetDeliveryStr(it.DeliveryType);


                if (it.OrderModel.Equals("1"))
                {
                    newRows["订单数量"] = "-" + it.Piece;
                    newRows["订单金额"] = "-" + it.TotalCharge.ToString();
                    newRows["服务费"] = "-" + it.OtherFee.ToString();
                    newRows["配送费"] = "-" + it.TransitFee.ToString();
                    newRows["优惠券"] = "-" + it.InsuranceFee.ToString();
                    newRows["应得金额"] = "-" + it.Rebate.ToString();
                    newRows["订单状态"] = GetStatusStr(it.AwbStatus);

                    TotalPiece -= it.Piece;
                    Rebate -= it.OtherFee;
                    TransitFee -= it.TransitFee;
                    InsuranceFee -= it.InsuranceFee;
                    TotalSale -= it.TotalCharge;
                    TTTotalCharge -= it.Rebate;
                }
                else
                {
                    newRows["订单数量"] = it.Piece;
                    newRows["订单金额"] = it.TotalCharge.ToString();
                    newRows["服务费"] = it.OtherFee.ToString();
                    newRows["配送费"] = it.TransitFee.ToString();
                    newRows["优惠券"] = it.InsuranceFee.ToString();
                    newRows["应得金额"] = it.Rebate.ToString();
                    newRows["订单状态"] = GetStatusStr(it.AwbStatus);

                    TotalPiece += it.Piece;
                    Rebate += it.OtherFee;
                    TransitFee += it.TransitFee;
                    InsuranceFee += it.InsuranceFee;
                    TotalSale += it.TotalCharge;
                    TTTotalCharge += it.Rebate;
                }

                table.Rows.Add(newRows);
            }
            DataRow footRows = table.NewRow();
            footRows["出库仓库"] = "汇总：";
            footRows["订单数量"] = TotalPiece;
            footRows["订单金额"] = TotalSale;
            footRows["服务费"] = Rebate;
            footRows["配送费"] = TransitFee;
            footRows["优惠券"] = InsuranceFee;
            footRows["应得金额"] = TTTotalCharge;
            table.Rows.Add(footRows);

            ToExcel.DataTableToExcel(table, "", "订单销售数据表" + DateTime.Now.ToString("yyyyMMdd"));
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string GetOrderTypeStr(string status)
        {
            if (status.Equals("22")) { return "急送单"; }
            else if (status.Equals("23")) { return "次日达单"; }
            else if (status.Equals("24")) { return "渠道单"; }
            else if (status.Equals("25")) { return "退仓单"; }
            else if (status.Equals("5")) { return "退货单"; }
            else if (status.Equals("0")) { return "普送单"; }
            else { return string.Empty; }
        }

        /// <summary>
        /// 获取送货方式
        /// </summary>
        public string GetDeliveryStr(string status)
        {
            if (status.Equals("0")) { return "送货"; }
            if (status.Equals("1")) { return "自提"; }
            if (status.Equals("2")) { return "普送"; }
            else { return string.Empty; }
        }
        /// <summary>
        /// 获取订单状态
        /// </summary>
        public string GetStatusStr(string status)
        {
            if (status.Equals("0")) { return "已下单"; }
            else if (status.Equals("1")) { return "正在备货"; }
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