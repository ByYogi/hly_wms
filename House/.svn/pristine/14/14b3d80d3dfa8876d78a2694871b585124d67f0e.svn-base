using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Arrive
{
    public partial class AwbStatusTrack : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导出实体
        /// </summary>
        public List<AwbStatusTrackEntity> AwbStatusList
        {
            get
            {
                if (Session["AwbStatusList"] == null)
                {
                    Session["AwbStatusList"] = new List<AwbStatusTrackEntity>();
                }
                return (List<AwbStatusTrackEntity>)(Session["AwbStatusList"]);
            }
            set
            {
                Session["AwbStatusList"] = value;
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (AwbStatusList.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(decimal));
            table.Columns.Add("回单照片", typeof(string));
            table.Columns.Add("受理日期", typeof(DateTime));
            table.Columns.Add("开单时间", typeof(DateTime));
            table.Columns.Add("运单号", typeof(string));
            table.Columns.Add("副单号", typeof(string));
            table.Columns.Add("最迟时效", typeof(string));
            table.Columns.Add("出发站", typeof(string));
            table.Columns.Add("中间站", typeof(string));
            table.Columns.Add("到达站", typeof(string));
            table.Columns.Add("中转站", typeof(string));
            table.Columns.Add("客户代码", typeof(string));//
            table.Columns.Add("发货单位", typeof(string));//
            table.Columns.Add("发货人", typeof(string));
            table.Columns.Add("发货联系方式", typeof(string));//
            table.Columns.Add("收货单位", typeof(string));//
            table.Columns.Add("收货人", typeof(string));
            table.Columns.Add("收货联系方式", typeof(string));//
            table.Columns.Add("物品名称", typeof(string));
            table.Columns.Add("包装", typeof(string));
            table.Columns.Add("数量", typeof(decimal));
            table.Columns.Add("分批数量", typeof(decimal));
            table.Columns.Add("数量单价", typeof(decimal));
            table.Columns.Add("重量", typeof(decimal));
            table.Columns.Add("重量单价", typeof(decimal));
            table.Columns.Add("体积", typeof(decimal));
            table.Columns.Add("体积单价", typeof(decimal));
            table.Columns.Add("运输费", typeof(decimal));
            table.Columns.Add("提货费", typeof(decimal));
            table.Columns.Add("送货费", typeof(decimal));
            table.Columns.Add("其它费用", typeof(decimal));
            table.Columns.Add("合计", typeof(decimal));
            table.Columns.Add("代收款", typeof(decimal));
            table.Columns.Add("回扣", typeof(decimal));
            table.Columns.Add("保险费", typeof(decimal));
            table.Columns.Add("结款方式", typeof(string));
            table.Columns.Add("备注", typeof(string));
            table.Columns.Add("送货方式", typeof(string));
            table.Columns.Add("运单状态", typeof(string));
            table.Columns.Add("回单信息", typeof(string));
            table.Columns.Add("录单员", typeof(string));
            table.Columns.Add("配载合同号", typeof(string));
            table.Columns.Add("车辆牌照", typeof(string));
            table.Columns.Add("发车时间", typeof(string));
            table.Columns.Add("实际到达时间", typeof(string));
            table.Columns.Add("司机姓名", typeof(string));
            table.Columns.Add("手机号码", typeof(string));
            table.Columns.Add("合同费用", typeof(decimal));
            table.Columns.Add("回单要求", typeof(int));
            table.Columns.Add("到达中转时间", typeof(string));
            table.Columns.Add("中转承运商", typeof(string));
            table.Columns.Add("中转承运单号", typeof(string));
            table.Columns.Add("中转承运商联系方式", typeof(string));
            table.Columns.Add("到达配送时间", typeof(string));
            table.Columns.Add("配送司机", typeof(string));
            table.Columns.Add("车牌号", typeof(string));
            table.Columns.Add("联系方式", typeof(string));
            table.Columns.Add("签收人", typeof(string));
            table.Columns.Add("签收时间", typeof(string));
            table.Columns.Add("发送时间", typeof(string));
            table.Columns.Add("确认时间", typeof(string));
            int i = 0;
            foreach (var it in AwbStatusList)
            {
                i++;
                it.EnSafe();
                foreach (var gd in it.AwbGoods)
                {
                    DataRow newRows = table.NewRow();
                    newRows["序号"] = i;
                    newRows["回单照片"] = it.UploadReturnPic.Equals("0") ? "未上传" : "已上传";
                    newRows["受理日期"] = it.HandleTime.ToString("yyyy-MM-dd");
                    newRows["开单时间"] = it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                    newRows["运单号"] = it.AwbNo.Trim();
                    newRows["副单号"] = it.HAwbNo.Trim();
                    newRows["最迟时效"] = it.LatestTimeLimit.Trim();
                    newRows["出发站"] = it.Dep.Trim();
                    newRows["中间站"] = it.MidDest.Trim();
                    newRows["到达站"] = it.Dest.Trim();
                    newRows["中转站"] = Convert.ToString(it.Transit).Trim();
                    newRows["客户代码"] = it.ClientNum.Trim();
                    newRows["发货单位"] = it.ShipperUnit.Trim();
                    newRows["发货人"] = it.ShipperName.Trim();
                    newRows["发货联系方式"] = it.ShipperPhone.Trim();
                    newRows["收货单位"] = it.AcceptUnit.Trim();
                    newRows["收货人"] = it.AcceptPeople.Trim();
                    newRows["收货联系方式"] = it.AcceptPhone.Trim();
                    newRows["物品名称"] = gd.Goods.Trim();
                    newRows["包装"] = gd.Package.Trim();
                    newRows["数量"] = it.Piece;
                    if (it.AwbGoods.Count == 1)
                    { newRows["分批数量"] = it.AwbPiece; }
                    else
                    { newRows["分批数量"] = gd.Piece; }
                    newRows["数量单价"] = gd.PiecePrice;
                    newRows["重量"] = gd.Weight;
                    newRows["重量单价"] = gd.WeightPrice;
                    newRows["体积"] = gd.Volume;
                    newRows["体积单价"] = gd.VolumePrice;
                    newRows["运输费"] = it.TransportFee;
                    newRows["提货费"] = it.OtherFee;
                    newRows["送货费"] = it.DeliverFee;
                    newRows["其它费用"] = it.TransitFee;
                    newRows["合计"] = it.TotalCharge;
                    newRows["代收款"] = it.CollectMoney;
                    newRows["回扣"] = it.Rebate;
                    newRows["保险费"] = it.InsuranceFee;
                    newRows["结款方式"] = GetText(it.CheckOutType, "CheckOutType");
                    newRows["备注"] = it.Remark.Trim();
                    newRows["送货方式"] = GetText(it.DeliveryType, "DeliveryType");
                    newRows["运单状态"] = GetText(it.TruckFlag, "TruckFlag");
                    newRows["回单信息"] = it.ReturnInfo.Trim();
                    newRows["录单员"] = it.CreateAwb.Trim();
                    newRows["配载合同号"] = it.ContractNum.Trim();
                    newRows["车辆牌照"] = it.TruckNum.Trim();
                    newRows["发车时间"] = it.StartTime.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.StartTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.StartTime.ToString("MM-dd HH:mm");
                    newRows["实际到达时间"] = it.ActArriveTime.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.ActArriveTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.ActArriveTime.ToString("MM-dd HH:mm");
                    newRows["司机姓名"] = it.Driver.Trim();
                    newRows["手机号码"] = it.DriverCellPhone.Trim();
                    newRows["合同费用"] = it.ContractFee;
                    newRows["回单要求"] = it.ReturnAwb;
                    newRows["到达中转时间"] = it.TTime.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.TTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.TTime.ToString("MM-dd HH:mm");
                    newRows["中转承运商"] = it.TShortName;
                    newRows["中转承运单号"] = it.TAssistNum;
                    newRows["中转承运商联系方式"] = it.TPhone;
                    newRows["到达配送时间"] = it.DTime.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.DTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.DTime.ToString("MM-dd HH:mm");
                    newRows["配送司机"] = it.DDrive;
                    newRows["车牌号"] = it.DTruckNum;
                    newRows["联系方式"] = it.DPhone;
                    newRows["签收人"] = it.Signer;
                    newRows["签收时间"] = it.SignTime.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.SignTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.SignTime.ToString("yyyy-MM-dd HH:mm");
                    newRows["发送时间"] = it.SendReturnAwbDate.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.SendReturnAwbDate.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.SendReturnAwbDate.ToString("yyyy-MM-dd HH:mm");
                    newRows["确认时间"] = it.ConfirmReturnAwbDate.ToString("yyyy-MM-dd").Equals("0001-01-01") || it.ConfirmReturnAwbDate.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "" : it.ConfirmReturnAwbDate.ToString("yyyy-MM-dd HH:mm");

                    table.Rows.Add(newRows);
                }
            }
            ToExcel.DataTableToExcel(table, "", "客户货物跟踪表");
        }

        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("DeliveryType"))
            {
                if (value.Trim() == "0")
                    retStr = "送货";
                else if (value.Trim() == "1")
                    retStr = "自提";
            }
            else if (id.Contains("TruckFlag"))
            {
                if (value.Trim() == "0")
                    retStr = "在站";
                else if (value.Trim() == "1")
                    retStr = "出发";
                else if (value.Trim() == "2")
                    retStr = "在途";
                else if (value.Trim() == "3")
                    retStr = "到达";
                else if (value.Trim() == "4")
                    retStr = "结束";
                else if (value.Trim() == "5")
                    retStr = "关注";
                else if (value.Trim() == "6")
                    retStr = "客户";
                else if (value.Trim() == "7")
                    retStr = "签收";
                else if (value.Trim() == "8")
                    retStr = "配送";
                else if (value.Trim() == "9")
                    retStr = "中转";
            }
            else if (id.Contains("CheckOutType"))
            {
                if (value.Trim() == "0")
                    retStr = "现付";
                else if (value.Trim() == "1")
                    retStr = "回单";
                else if (value.Trim() == "2")
                    retStr = "月结";
                else if (value.Trim() == "3")
                    retStr = "到付";
                else if (value.Trim() == "4")
                    retStr = "代收款";
            }
            return retStr;
        }
    }
}