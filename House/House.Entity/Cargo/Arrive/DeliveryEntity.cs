using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 配送实体
    /// </summary>
    [Serializable]
    public class DeliveryEntity
    {
        public string BelongSystem { get; set; }
        [Description("配送单号")]
        public Int64 DeliveryID { get; set; }
        [Description("配送合同号")]
        public string DeliveryNum { get; set; }
        [Description("运单ID")]
        public Int64 AwbID { get; set; }
        [Description("到达运单ID")]
        public Int64 ArriveID { get; set; }
        [Description("运单号")]
        public string AwbNo { get; set; }
        [Description("车牌号")]
        public string TruckNum { get; set; }
        [Description("司机姓名")]
        public string Driver { get; set; }
        [Description("司机手机号码")]
        public string DriverCellPhone { get; set; }
        [Description("发车时间")]
        public DateTime StartTime { get; set; }
        [Description("预计到达时间")]
        public DateTime PreArriveTime { get; set; }
        [Description("运费")]
        public decimal TransportFee { get; set; }
        public decimal DeliverFee { get; set; }
        [Description("其它费用")]
        public decimal OtherFee { get; set; }
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
        public string CheckStatus { get; set; }
        public string CurCity { get; set; }
        public string UserName { get; set; }
        public int PieceTotal { get; set; }
        public string Dest { get; set; }
        public string MidDest { get; set; }
        public string DType { get; set; }
        public string ShortStatus { get; set; }
        public string Remark { get; set; }
        public string CheckOutType { get; set; }
        public decimal CollectMoney { get; set; }
        public decimal TotalCharge { get; set; }
        public DateTime CreateTime { get; set; }
        public string FinanceFirstCheck { get; set; }
        public string FirstCheckName { get; set; }
        public DateTime FirstCheckDate { get; set; }
        public string FinanceSecondCheck { get; set; }
        public string SecondCheckName { get; set; }
        public DateTime SecondCheckDate { get; set; }
        public int Piece { get; set; }
        public int AwbPiece { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public DateTime HandleTime { get; set; }
        public string ShipperUnit { get; set; }
        public string AcceptUnit { get; set; }
        public string AcceptAddress { get; set; }
        public string Goods { get; set; }
        public decimal ProfitPCT { get; set; }
        public string RType { get; set; }
        public string FromTO { get; set; }
        public string Model { get; set; }
        public decimal Length { get; set; }
        /// <summary>
        /// 已收
        /// </summary>
        public decimal ReceivedMoney { get; set; }
        /// <summary>
        /// 未收
        /// </summary>
        public decimal UncollectMoney { get; set; }
        public string AwbStatus { get; set; }
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
    /// <summary>
    /// 配送单号与运单号关联表
    /// </summary>
    [Serializable]
    public class DeliveryAwbEntity
    {
        public string BelongSystem { get; set; }
        [Description("配送单号")]
        public Int64 DeliveryID { get; set; }
        [Description("到达运单ID")]
        public Int64 ArriveID { get; set; }
        [Description("运单ID")]
        public Int64 AwbID { get; set; }
        [Description("运单号")]
        public string AwbNo { get; set; }
        [Description("操作类别")]
        public string Mode { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public string AwbStatus { get; set; }
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
