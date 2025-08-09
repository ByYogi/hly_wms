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
    public partial class TruckContractManager : BasePage
    {
        public string company = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserInfor.NewLandBelongSystem.Equals("DR"))
            {
                company = "广州市鼎融";
            }
            else if (UserInfor.NewLandBelongSystem.Equals("FJ")) { company = "福建新陆程"; }
            else if (UserInfor.NewLandBelongSystem.Equals("YQ")) { company = "云起物流"; }
            else if (UserInfor.NewLandBelongSystem.Equals("ZY")) { company = "众盈物流"; }
            else
            {
                company = "广州市新陆程";
            }
        }
        public List<DepManifestEntity> driverlist
        {
            get
            {
                if (Session["driverlist"] == null)
                {
                    Session["driverlist"] = new List<DepManifestEntity>();
                }
                return (List<DepManifestEntity>)(Session["driverlist"]);
            }
            set
            {
                Session["driverlist"] = value;
            }
        }
        public DepManifestEntity depmanifest
        {
            get
            {
                if (Session["depmanifest"] == null)
                {
                    Session["depmanifest"] = new DepManifestEntity();
                }
                return (DepManifestEntity)(Session["depmanifest"]);
            }
            set
            {
                Session["depmanifest"] = value;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDerived_Click(object sender, EventArgs e)
        {
            if (driverlist.Count <= 0)
            {
                return;
            }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(decimal));
            table.Columns.Add("合同号", typeof(string));
            table.Columns.Add("发车时间", typeof(string));
            table.Columns.Add("配载时间", typeof(string));
            table.Columns.Add("到达时间", typeof(string));
            table.Columns.Add("起运站", typeof(string));
            table.Columns.Add("到达站", typeof(string));//
            table.Columns.Add("车辆牌照", typeof(string));
            table.Columns.Add("车长", typeof(string));
            table.Columns.Add("车型", typeof(string));
            table.Columns.Add("司机姓名", typeof(string));
            table.Columns.Add("手机号码", typeof(string));
            table.Columns.Add("身份证号码", typeof(string));
            table.Columns.Add("配载票数", typeof(decimal));
            table.Columns.Add("配载件数", typeof(decimal));
            table.Columns.Add("配载重量", typeof(decimal));
            table.Columns.Add("配载体积", typeof(decimal));
            table.Columns.Add("总收入", typeof(decimal));
            table.Columns.Add("干线运费", typeof(decimal));
            table.Columns.Add("其它费用", typeof(decimal));
            table.Columns.Add("预付", typeof(decimal));
            table.Columns.Add("到付", typeof(decimal));
            table.Columns.Add("开户行", typeof(string));
            table.Columns.Add("开户名", typeof(string));
            table.Columns.Add("银行卡号", typeof(string));
            table.Columns.Add("卸货地址", typeof(string));
            table.Columns.Add("备注", typeof(string));
            table.Columns.Add("配载员", typeof(string));
            table.Columns.Add("调度员", typeof(string));
            table.Columns.Add("监装员", typeof(string));
            table.Columns.Add("合同状态", typeof(string));
            table.Columns.Add("审核状态", typeof(string));

            int i = 0;
            foreach (var it in driverlist)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                newRows["序号"] = i;
                newRows["合同号"] = it.ContractNum.Trim();
                newRows["发车时间"] = it.StartTime.ToString("yyyy-MM-dd HH:mm");
                newRows["配载时间"] = it.CreateTime.ToString("yyyy-MM-dd HH:mm");
                newRows["到达时间"] = it.ActArriveTime.ToString("yyyy-MM-dd") == "0001-01-01" ? "" : it.ActArriveTime.ToString("yyyy-MM-dd HH:mm");
                newRows["起运站"] = it.Dep.Trim();
                newRows["到达站"] = it.Dest.Trim();
                newRows["车辆牌照"] = it.TruckNum.Trim();
                newRows["车长"] = it.Length;
                newRows["车型"] = GetText(it.Model.Trim(), "Model");
                newRows["司机姓名"] = it.Driver.Trim();
                newRows["手机号码"] = it.DriverCellPhone.Trim();
                newRows["身份证号码"] = it.DriverIDNum.Trim();
                newRows["配载票数"] = it.AwbNum;
                newRows["配载件数"] = it.TotalAwbPiece;
                newRows["配载重量"] = it.Weight;
                newRows["配载体积"] = it.Volume;
                newRows["总收入"] = it.TotalFee;
                newRows["干线运费"] = it.TransportFee;
                newRows["其它费用"] = it.OtherFee;
                newRows["预付"] = it.PrepayFee;
                newRows["到付"] = it.ArriveFee;
                newRows["开户行"] = it.CardBank.Trim();
                newRows["开户名"] = it.CardName.Trim();
                newRows["银行卡号"] = it.CardNum.Trim();
                newRows["卸货地址"] = it.UnLoadAddress.Trim();
                newRows["备注"] = it.Remark.Trim();
                newRows["配载员"] = it.Manifester.Trim();
                newRows["调度员"] = it.Dispatcher.Trim();
                newRows["监装员"] = it.Loader.Trim();
                newRows["合同状态"] = GetText(it.CancelFlag, "CancelFlag");
                newRows["审核状态"] = GetText(it.DelFlag, "DelFlag");
                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "车辆合同表");
        }
        /// <summary>
        /// 导出配载明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (depmanifest.AwbInfo.Count <= 0) { return; }
            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(string));
            table.Columns.Add("运单号", typeof(string));
            table.Columns.Add("副单号", typeof(string));
            table.Columns.Add("开单日期", typeof(string));
            table.Columns.Add("客户名称", typeof(string));
            table.Columns.Add("出发站", typeof(string));
            table.Columns.Add("中转站", typeof(string));
            table.Columns.Add("移库站", typeof(string));
            table.Columns.Add("到达站", typeof(string));
            table.Columns.Add("品名", typeof(string));
            table.Columns.Add("总件数", typeof(string));
            table.Columns.Add("分批件数", typeof(decimal));
            table.Columns.Add("分批重量", typeof(decimal));
            table.Columns.Add("分批体积", typeof(decimal));
            table.Columns.Add("总运费", typeof(decimal));
            table.Columns.Add("代收款", typeof(decimal));
            table.Columns.Add("付款方式", typeof(string));
            table.Columns.Add("备注信息", typeof(string));
            int i = 0;
            int tPiece = 0, fPiece = 0;
            decimal fWeight = 0.0M, fVolume = 0.0M, tCharge = 0.0M, dCharge = 0.0M;
            int noStopPiece = 0, transitPiece = 0, noStopNum = 0, transitNum = 0;//直达件数，中转件数，直达票数，中转票数
            decimal noStopVolume = 0.0M, transitVolume = 0.0M, noStopCharge = 0.0M, transitCharge = 0.0M;//直达体积，中转体积，直达收入，中转收入
            foreach (var it in depmanifest.AwbInfo)
            {
                i++;
                it.EnSafe();
                DataRow newRows = table.NewRow();
                newRows["序号"] = i.ToString();
                newRows["运单号"] = it.AwbNo;
                newRows["副单号"] = it.HAwbNo;
                newRows["开单日期"] = it.HandleTime.ToString("yyyy-MM-dd");
                newRows["客户名称"] = it.ShipperUnit.Trim();
                newRows["出发站"] = it.Dep.Trim();
                newRows["中转站"] = it.Transit.Trim();
                newRows["移库站"] = it.MidDest.Trim();
                newRows["到达站"] = it.Dest.Trim();
                newRows["品名"] = it.Goods.Trim();
                newRows["总件数"] = it.Piece.ToString();
                newRows["分批件数"] = it.AwbPiece;
                newRows["分批重量"] = it.AwbWeight;
                newRows["分批体积"] = it.AwbVolume;
                newRows["总运费"] = it.TotalCharge;
                newRows["代收款"] = it.CollectMoney;
                newRows["付款方式"] = GetText(it.CheckOutType, "CheckOutType");
                newRows["备注信息"] = it.Remark.Trim();
                table.Rows.Add(newRows);
                tPiece += it.Piece; fPiece += it.AwbPiece; fWeight += it.AwbWeight; fVolume += it.AwbVolume; tCharge += it.TotalCharge; dCharge += it.CollectMoney;
                if (it.Dest.Equals(it.Transit) || string.IsNullOrEmpty(it.Transit))//直达
                {
                    noStopNum++; noStopPiece += it.AwbPiece; noStopVolume += it.AwbVolume; noStopCharge += it.TotalCharge;
                }
                else//中转
                {
                    transitNum++; transitPiece += it.AwbPiece; transitVolume += it.AwbVolume; transitCharge += it.TotalCharge;
                }
            }
            DataRow tRows = table.NewRow();
            tRows["运单号"] = "收入合计：";
            tRows["总件数"] = tPiece.ToString();
            tRows["分批件数"] = fPiece;
            tRows["分批重量"] = fWeight;
            tRows["分批体积"] = fVolume;
            tRows["总运费"] = tCharge;
            tRows["代收款"] = dCharge;
            table.Rows.Add(tRows);
            DataRow gRows = table.NewRow();
            gRows["运单号"] = "干线运费：";
            gRows["总运费"] = depmanifest.TransportFee;
            table.Rows.Add(gRows);
            DataRow mRows = table.NewRow();
            mRows["运单号"] = "毛利";
            mRows["总运费"] = tCharge - depmanifest.TransportFee;
            table.Rows.Add(mRows);

            DataRow aRow = table.NewRow();
            aRow["运单号"] = "直达票数";
            aRow["副单号"] = noStopNum.ToString();
            aRow["品名"] = "中转票数";
            aRow["总件数"] = transitNum.ToString();
            table.Rows.Add(aRow);

            DataRow bRow = table.NewRow();
            bRow["运单号"] = "直达件数";
            bRow["副单号"] = noStopPiece.ToString();
            bRow["品名"] = "中转件数";
            bRow["总件数"] = transitPiece.ToString();
            table.Rows.Add(bRow);

            DataRow cRow = table.NewRow();
            cRow["运单号"] = "直达体积";
            cRow["副单号"] = noStopVolume.ToString();
            cRow["品名"] = "中转体积";
            cRow["总件数"] = transitVolume.ToString();
            table.Rows.Add(cRow);

            DataRow dRow = table.NewRow();
            dRow["运单号"] = "直达收入";
            dRow["副单号"] = noStopCharge.ToString();
            dRow["品名"] = "中转收入";
            dRow["总件数"] = transitCharge.ToString();
            table.Rows.Add(dRow);

            DataRow eRow = table.NewRow();
            eRow["运单号"] = "直达收入占比";
            eRow["副单号"] = ((double)noStopCharge / (double)tCharge).ToString("P");
            eRow["品名"] = "中转收入占比";
            eRow["总件数"] = ((double)transitCharge / (double)tCharge).ToString("P");
            table.Rows.Add(eRow);

            ToExcel.DataTableToExcel(table, "【" + depmanifest.CreateTime.ToString("yyyy-MM-dd") + depmanifest.Dep.Trim() + depmanifest.Dest.Trim() + depmanifest.ContractNum.Trim() + "】配载清单/司机车牌：" + depmanifest.TruckNum.Trim() + "    司机姓名：" + depmanifest.Driver.Trim() + "    司机联系方式：" + depmanifest.DriverCellPhone.Trim() + "    司机身份证号码：" + depmanifest.DriverIDNum.Trim(), depmanifest.ContractNum.Trim() + "配载清单表");
        }
        private string GetText(string value, string id)
        {
            string retStr = string.Empty;
            if (id.Contains("Model"))
            {
                if (value.Trim() == "0")
                    retStr = "厢车";
                else if (value.Trim() == "1")
                    retStr = "高栏";
                else if (value.Trim() == "2")
                    retStr = "平板";
                else if (value.Trim() == "3")
                    retStr = "冷柜";
                else if (value.Trim() == "4")
                    retStr = "面包车";
                else if (value.Trim() == "5")
                    retStr = "微型车";
            }
            else if (id.Contains("CancelFlag"))
            {
                if (value.Trim() == "0")
                    retStr = "正常";
                else if (value.Trim() == "1")
                    retStr = "作废";
            }
            else if (id.Contains("DelFlag"))
            {
                if (value.Trim() == "0")
                    retStr = "未审";
                else if (value.Trim() == "1")
                    retStr = "已审";
                else if (value.Trim() == "2")
                    retStr = "拒审";
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