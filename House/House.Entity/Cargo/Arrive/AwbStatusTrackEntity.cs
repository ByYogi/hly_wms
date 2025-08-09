using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 运单状态跟踪
    /// </summary>
    [Serializable]
    public class AwbStatusTrackEntity
    {
        public string BelongSystem { get; set; }
        /// <summary>
        /// 运单表主键自动增长
        /// </summary>
        public Int64 AwbID { get; set; }
        public string AwbNo { get; set; }
        public string HAwbNo { get; set; }
        public string Dep { get; set; }
        public string Dest { get; set; }
        public string Transit { get; set; }
        public string MidDest { get; set; }
        public int Piece { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        /// <summary>
        /// 分批件数
        /// </summary>
        public int AwbPiece { get; set; }
        /// <summary>
        /// 分批重量
        /// </summary>
        public decimal AwbWeight { get; set; }
        /// <summary>
        /// 分批体积
        /// </summary>
        public decimal AwbVolume { get; set; }

        public string TrafficType { get; set; }
        public string DeliveryType { get; set; }

        public string ShipperName { get; set; }
        public string ShipperUnit { get; set; }
        public string ShipperTelephone { get; set; }
        public string ShipperCellphone { get; set; }
        public string ShipperPhone { get; set; }
        public string AcceptUnit { get; set; }
        public string AcceptAddress { get; set; }
        public string AcceptPeople { get; set; }
        public string AcceptTelephone { get; set; }
        public string AcceptPhone { get; set; }
        public string AcceptCellphone { get; set; }

        public DateTime HandleTime { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string OrderNum { get; set; }
        public string Goods { get; set; }
        public string ContractNum { get; set; }

        public string TruckNum { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime PreArriveTime { get; set; }

        public string Driver { get; set; }
        public string DriverCellPhone { get; set; }

        public string TruckFlag { get; set; }
        public DateTime ActArriveTime { get; set; }
        public string ReturnInfo { get; set; }
        public string ReturnStatus { get; set; }
        public string UserName { get; set; }
        [Description("开单员")]
        public string CreateAwb { get; set; }
        [Description("开单时间")]
        public DateTime CreateDate { get; set; }
        public List<AwbGoodsEntity> AwbGoods { get; set; }
        [Description("保费")]
        public decimal InsuranceFee { get; set; }
        [Description("运输费")]
        public decimal TransportFee { get; set; }
        public decimal TransitFee { get; set; }
        [Description("配送费")]
        public decimal DeliverFee { get; set; }
        [Description("提货费")]
        public decimal OtherFee { get; set; }
        [Description("总费用")]
        public decimal TotalCharge { get; set; }
        [Description("回扣")]
        public decimal Rebate { get; set; }
        [Description("结算方式")]
        public string CheckOutType { get; set; }
        [Description("代收款")]
        public decimal CollectMoney { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("运输性质")]
        public string TransKind { get; set; }
        [Description("承运商ID")]
        public Int64 CarrierID { get; set; }
        public string CarrierShortName { get; set; }
        public string CarrierName { get; set; }
        public decimal ActMoney { get; set; }
        public int ReturnAwb { get; set; }
        public DateTime ReturnDate { get; set; }
        public string SendReturnAwbStatus { get; set; }
        public DateTime SendReturnAwbDate { get; set; }
        public string ConfirmReturnAwbStatus { get; set; }
        public DateTime ConfirmReturnAwbDate { get; set; }
        public string SendClientStatus { get; set; }
        public DateTime SendClientTime { get; set; }
        [Description("结算状态")]
        public string CheckStatus { get; set; }
        /// <summary>
        /// 签收人
        /// </summary>
        public string Signer { get; set; }
        public DateTime SignTime { get; set; }
        /// <summary>
        /// 合同费用
        /// </summary>
        public decimal ContractFee { get; set; }
        public Int64 TransitID { get; set; }
        public string AssistNum { get; set; }
        public Int64 ArriveID { get; set; }
        /// <summary>
        /// 配送时间(配载时间)
        /// </summary>
        public DateTime DTime { get; set; }
        public string DDrive { get; set; }
        public string DTruckNum { get; set; }
        public string DPhone { get; set; }
        /// <summary>
        /// 中转时间（分流时间）
        /// </summary>
        public DateTime TTime { get; set; }
        public string TShortName { get; set; }
        public string TAssistNum { get; set; }
        public string TPhone { get; set; }
        public DateTime ArriveDate { get; set; }
        /// <summary>
        /// 时效
        /// </summary>
        public int TimeLimit { get; set; }
        /// <summary>
        /// 最迟时效
        /// </summary>
        public string LatestTimeLimit { get; set; }
        public string DelayDay { get; set; }
        public string DelayReason { get; set; }
        public string ClientNum { get; set; }
        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime GiveTime { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        public DateTime AbnormalTime { get; set; }
        /// <summary>
        /// 上传回单照片状态
        /// 0：未上传默认
        /// 1：已上传
        /// </summary>
        public string UploadReturnPic { get; set; }
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
