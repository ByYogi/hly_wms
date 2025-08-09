using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Finance
{
    public partial class financeReceiveMoneyManager : BasePage
    {
        public List<CargoOrderEntity> CargoOrderEntityList
        {
            get
            {
                if (Session["CargoOrderEntityList"] == null)
                {
                    Session["CargoOrderEntityList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["CargoOrderEntityList"]);
            }
            set
            {
                Session["CargoOrderEntityList"] = value;
            }
        }
        public string Un = string.Empty;
        public string Ln = string.Empty;
        public string HouseName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Un = UserInfor.UserName.Trim();
            Ln = UserInfor.LoginName.Trim();
            HouseName = UserInfor.HouseName.Trim();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (CargoOrderEntityList.Count <= 0)
            {
                return;
            }
            CargoHouseBus houseBus = new CargoHouseBus();
            CargoHouseEntity houseEntity = houseBus.QueryCargoHouseByID(UserInfor.HouseID);

            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("订单号", typeof(string));
            if (houseEntity.BelongHouse.Equals("6"))
            {
                table.Columns.Add("数量", typeof(decimal));
                table.Columns.Add("供应商进仓价", typeof(decimal));
                table.Columns.Add("轮胎收入", typeof(decimal));
                table.Columns.Add("配送费", typeof(decimal));
                table.Columns.Add("平台费", typeof(decimal));
                table.Columns.Add("优惠券", typeof(decimal));
                table.Columns.Add("超期费", typeof(decimal));
                table.Columns.Add("合计", typeof(decimal));
            }
            else
            {
                table.Columns.Add("数量", typeof(decimal));
                table.Columns.Add("收入", typeof(decimal));
                table.Columns.Add("合计", typeof(decimal));
            }
            table.Columns.Add("审核状态", typeof(string));
            table.Columns.Add("结算状态", typeof(string));
            table.Columns.Add("所属仓库", typeof(string));
            table.Columns.Add("品牌", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("花纹", typeof(string));
            table.Columns.Add("载速", typeof(string));
            table.Columns.Add("货品代码", typeof(string));
            table.Columns.Add("周期", typeof(string));
            //table.Columns.Add("出发站", typeof(string));
            //table.Columns.Add("到达站", typeof(string));
            table.Columns.Add("客户名称", typeof(string));//
            table.Columns.Add("收货人", typeof(string));
            table.Columns.Add("联系手机", typeof(string));
            table.Columns.Add("收货地址", typeof(string));
            table.Columns.Add("业务员", typeof(string));
            table.Columns.Add("订单类型", typeof(string));
            table.Columns.Add("开单员", typeof(string));
            table.Columns.Add("开单时间", typeof(string));
            table.Columns.Add("订单状态", typeof(string));
            table.Columns.Add("下单方式", typeof(string));
            table.Columns.Add("商城订单号", typeof(string));
            table.Columns.Add("付款方式", typeof(string));
            table.Columns.Add("支付订单号", typeof(string));
            table.Columns.Add("供应商", typeof(string));
            table.Columns.Add("优惠券类型", typeof(string));
            table.Columns.Add("送货方式", typeof(string));
            table.Columns.Add("物流公司单号", typeof(string));
            //table.Columns.Add("物流配送费用", typeof(string));
            table.Columns.Add("物流公司名称", typeof(string));
            table.Columns.Add("通联订单ID", typeof(string));
            table.Columns.Add("入库时间", typeof(DateTime));
            table.Columns.Add("入库天数", typeof(string));
            CargoOrderBus orderBus = new CargoOrderBus();
            int i = 0;
            foreach (var it in CargoOrderEntityList)
            {
                i++;
                it.EnSafe();
                List<CargoProductShelvesEntity> cargos = orderBus.queryOrderProductForAPP(it.OrderNo);
                if ( cargos==null || cargos.Count()==0) {
                    continue;
                }
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["订单号"] = it.OrderNo.Trim();
                if (houseEntity.BelongHouse.Equals("6"))
                {
                    newRows["数量"] = it.Piece;
                    newRows["供应商进仓价"] = cargos[0].originalPrice;
                    newRows["轮胎收入"] = it.OrderModel.Trim().Equals("1") ? -it.TransportFee : it.TransportFee;
                    newRows["配送费"] = it.TransitFee;
                    newRows["平台费"] = it.OtherFee;
                    newRows["优惠券"] = it.InsuranceFee;
                    newRows["超期费"] = it.OverDueFee;

                    newRows["合计"] = it.OrderModel.Trim().Equals("1") ? -it.TotalCharge : it.TotalCharge;
                }
                else
                {
                    newRows["数量"] = it.Piece;
                    newRows["收入"] = it.OrderModel.Trim().Equals("1") ? -it.TransportFee : it.TransportFee;
                    newRows["合计"] = it.OrderModel.Trim().Equals("1") ? -it.TotalCharge : it.TotalCharge;
                }
                newRows["审核状态"] = GetText(it.FinanceSecondCheck.Trim(), "FinanceSecondCheck");
                newRows["结算状态"] = GetText(it.CheckStatus.Trim(), "CheckStatus");
                newRows["所属仓库"] = it.HouseName.Trim();
                newRows["品牌"] = cargos[0].TypeName;
                newRows["规格"] = cargos[0].Specs;
                newRows["花纹"] = cargos[0].Figure;
                newRows["载速"] = cargos[0].LoadIndex + cargos[0].SpeedLevel;
                newRows["货品代码"] = cargos[0].GoodsCode;
                newRows["周期"] = cargos[0].Batch;
                //newRows["出发站"] = it.Dep.Trim();
                //newRows["到达站"] = it.Dest.Trim();
                newRows["客户名称"] = it.AcceptUnit.Trim();
                newRows["收货人"] = it.AcceptPeople.Trim();
                newRows["联系手机"] = it.AcceptCellphone.Trim();
                newRows["收货地址"] = it.AcceptAddress.Trim();
                newRows["业务员"] = it.SaleManName.Trim();
                newRows["订单类型"] = GetText(it.OrderModel.Trim(), "OrderModel");
                newRows["开单员"] = it.CreateAwb.Trim();
                newRows["开单时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                newRows["订单状态"] = GetText(it.AwbStatus.Trim(), "AwbStatus");
                newRows["下单方式"] = GetText(it.OrderType.Trim(), "OrderType");
                newRows["商城订单号"] = it.WXOrderNo;
                newRows["付款方式"] = GetText(it.PayWay, "PayWay");
                newRows["支付订单号"] = it.WXPayOrderNo;
                newRows["供应商"] = it.SuppClientName;
                newRows["优惠券类型"] = GetText(it.CouponType, "CouponType");
                newRows["送货方式"] = GetText(it.DeliveryType, "DeliveryType");
                newRows["物流公司单号"] = it.LogisAwbNo;
                //newRows["物流配送费用"] = it.TransitFee;
                newRows["物流公司名称"] = it.LogisticName;
                newRows["通联订单ID"] = it.Trxid;
                if (cargos[0].InCargoStatus == 0)
                {
                    newRows["入库时间"] =  DateTime.Now.ToString("1900-01-01");
                    newRows["入库天数"] =  "";
                }
                else
                {
                    var inTime = Convert.ToDateTime(cargos[0].InHouseTime);
                    var CreateDate = Convert.ToDateTime(it.CreateDate);
                    var day = (CreateDate - inTime).Days;
                    newRows["入库时间"] =inTime;
                    newRows["入库天数"] = day;
                }
                    


                table.Rows.Add(newRows);
            }

            ToExcel.DataTableToExcel(table, "", "应收账款管理数据表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("OrderModel"))
            {
                if (value.Trim() == "0")
                    retStr = "发货单";
                else if (value.Trim() == "1")
                    retStr = "退货单";
            }
            else if (id.Contains("DeliveryType"))
            {
                if (value.Trim() == "0")
                    retStr = "急送";
                else if (value.Trim() == "1")
                    retStr = "自提";
                else if (value.Trim() == "2")
                    retStr = "普送";
                else
                    retStr = "";
            }
            else if (id.Contains("PayWay"))
            {
                if (value.Trim() == "0")
                    retStr = "微信付款";
                else if (value.Trim() == "1")
                    retStr = "额度付款";
                else
                    retStr = "";
            }
            else if (id.Contains("FinanceSecondCheck"))
            {
                if (value.Trim() == "0")
                    retStr = "未审";
                else if (value.Trim() == "1")
                    retStr = "已审";
            }
            else if (id.Contains("CouponType"))
            {
                if (value.Trim() == "0")
                    retStr = "平台券";
                else if (value.Trim() == "1")
                    retStr = "供应商券";
            }
            else if (id.Contains("CheckStatus"))
            {
                if (value.Trim() == "0")
                    retStr = "未结算";
                else if (value.Trim() == "1")
                    retStr = "已结清";
                else if (value.Trim() == "2")
                    retStr = "未结清";
            }
            else if (id.Contains("TradeType"))
            {
                if (value.Trim() == "0")
                    retStr = "账号交易";
                else if (value.Trim() == "1")
                    retStr = "预收款交易";
                else if (value.Trim() == "2")
                    retStr = "返利款交易";
                else if (value.Trim() == "3")
                    retStr = "微信支付";
            }
            else if (id.Contains("OrderType"))
            {
                if (value.Trim() == "0")
                    retStr = "电脑单";
                else if (value.Trim() == "1")
                    retStr = "企业微信单";
                else if (value.Trim() == "2")
                    retStr = "微信商城单";
                else if (value.Trim() == "3")
                    retStr = "APP单";
                else if (value.Trim() == "4")
                    retStr = "小程序单";
            }
            else if (id.Contains("AwbStatus"))
            {
                if (value.Trim() == "0")
                    retStr = "已下单";
                else if (value.Trim() == "1")
                    retStr = "出库中";
                else if (value.Trim() == "2")
                    retStr = "已出库";
                else if (value.Trim() == "3")
                    retStr = "运输在途";
                else if (value.Trim() == "4")
                    retStr = "已到达";
                else if (value.Trim() == "5")
                    retStr = "已签收";
            }
            return retStr;
        }
    }
}