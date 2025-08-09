using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 到达运单中转实体
    /// </summary>
    [Serializable]
    public class TransitEntity
    {
        public string BelongSystem { get; set; }
        [Description("中转单号")]
        public Int64 TransitID { get; set; }
        [Description("承运商ID")]
        public Int64 CarrierID { get; set; }
        [Description("承运商简称")]
        public string CarrierShortName { get; set; }
        [Description("承运商联系人")]
        public string Boss { get; set; }
        [Description("承运商联系电话")]
        public string Telephone { get; set; }
        [Description("承运商联系地址")]
        public string Address { get; set; }
        [Description("运单ID")]
        public Int64 AwbID { get; set; }
        [Description("到达运单ID")]
        public Int64 ArriveID { get; set; }
        [Description("运单号")]
        public string AwbNo { get; set; }
        [Description("发车时间")]
        public DateTime StartTime { get; set; }
        [Description("预计到达时间")]
        public DateTime PreArriveTime { get; set; }
        [Description("运费")]
        public decimal TransportFee { get; set; }
        [Description("预付")]
        public decimal PrepayFee { get; set; }
        [Description("到付")]
        public decimal ArriveFee { get; set; }
        [Description("其它费用")]
        public decimal OtherFee { get; set; }
        [Description("提货费")]
        public decimal DeliveryFee { get; set; }
        [Description("送货费")]
        public decimal SendFee { get; set; }
        [Description("装卸费")]
        public decimal HandFee { get; set; }
        [Description("代收款")]
        public decimal CollectMoney { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("开始时间")]
        public DateTime StartDate { get; set; }
        [Description("结束时间")]
        public DateTime EndDate { get; set; }
        [Description("操作类别")]
        public string Mode { get; set; }
        [Description("付款方式")]
        public string CheckOutType { get; set; }
        [Description("外协单号")]
        public string AssistNum { get; set; }
        public string CurCity { get; set; }
        public string UserName { get; set; }
        public int PieceTotal { get; set; }
        public string Dest { get; set; }
        public string Dep { get; set; }
        public string Transit { get; set; }
        public string AwbCheckOutType { get; set; }
        public decimal TotalCharge { get; set; }
        public string FinanceFirstCheck { get; set; }
        public string FirstCheckName { get; set; }
        public DateTime FirstCheckDate { get; set; }
        public int Piece { get; set; }
        public int FenPiPiece { get; set; }
        public int AwbPiece { get; set; }
        public decimal AwbWeight { get; set; }
        public decimal AwbVolume { get; set; }
        public decimal PiecePrice { get; set; }
        public decimal Weight { get; set; }
        public decimal WeightPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal VolumePrice { get; set; }
        public DateTime HandleTime { get; set; }
        public string ShipperUnit { get; set; }
        public string AcceptUnit { get; set; }
        public string CheckStatus { get; set; }
        /// <summary>
        /// 去NULL,替换危险字符
        /// </summary>
        public void EnSafe()
        {
            PropertyInfo[] pSource = this.GetType().GetProperties();

            foreach (PropertyInfo s in pSource)
            {
                if (s.PropertyType.Name.ToUpper().Contains("STRING"))
                {
                    if (s.GetValue(this, null) == null)
                        s.SetValue(this, "", null);
                    else
                        s.SetValue(this, s.GetValue(this, null).ToString().Replace("'", "’"), null);
                }
            }
        }
    }
}
