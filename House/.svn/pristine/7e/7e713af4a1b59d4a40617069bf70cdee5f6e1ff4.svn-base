using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 运单表
    /// </summary>
    [Serializable]
    public class AwbEntity
    {
        public string BelongSystem { get; set; }
        public int RowNumber { get; set; }
        /// <summary>
        /// 运单表主键自动增长
        /// </summary>
        [Description("运单ID")]
        public Int64 AwbID { get; set; }
        [Description("托运单号")]
        public string AwbNo { get; set; }
        [Description("副单号")]
        public string HAwbNo { get; set; }
        [Description("起运站")]
        public string Dep { get; set; }
        [Description("到达站")]
        public string Dest { get; set; }
        public string MidDest { get; set; }
        [Description("中转站")]
        public string Transit { get; set; }
        [Description("总件数")]
        public int Piece { get; set; }
        [Description("总重量")]
        public decimal Weight { get; set; }
        [Description("总体积")]
        public decimal Volume { get; set; }
        /// <summary>
        /// 分批件数
        /// </summary>
        [Description("分批件数")]
        public int AwbPiece { get; set; }
        /// <summary>
        /// 分批重量
        /// </summary>
        [Description("分批重量")]
        public decimal AwbWeight { get; set; }
        /// <summary>
        /// 分批体积
        /// </summary>
        [Description("分批体积")]
        public decimal AwbVolume { get; set; }
        public decimal PiecePrice { get; set; }
        public decimal WeightPrice { get; set; }
        public decimal VolumePrice { get; set; }
        public int Attach { get; set; }
        [Description("保费")]
        public decimal InsuranceFee { get; set; }
        [Description("中转费")]
        public decimal TransitFee { get; set; }
        [Description("运输费")]
        public decimal TransportFee { get; set; }
        [Description("配送费")]
        public decimal DeliverFee { get; set; }
        [Description("提货费")]
        public decimal OtherFee { get; set; }
        [Description("总费用")]
        public decimal TotalCharge { get; set; }
        public decimal SumCharge { get; set; }
        [Description("回扣")]
        public decimal Rebate { get; set; }
        [Description("现付")]
        public decimal NowPay { get; set; }
        [Description("提付")]
        public decimal PickPay { get; set; }
        [Description("结算方式")]
        public string CheckOutType { get; set; }
        [Description("代收款")]
        public decimal CollectMoney { get; set; }
        public decimal DSK { get; set; }
        [Description("回单要求")]
        public int ReturnAwb { get; set; }
        [Description("运输方式")]
        public string TrafficType { get; set; }
        [Description("送货方式")]
        public string DeliveryType { get; set; }
        [Description("装卸工")]
        public string SteveDore { get; set; }
        [Description("托运人姓名")]
        public string ShipperName { get; set; }
        [Description("托运人单位")]
        public string ShipperUnit { get; set; }
        [Description("托运人地址")]
        public string ShipperAddress { get; set; }
        [Description("托运人电话")]
        public string ShipperTelephone { get; set; }
        [Description("托运人手机号码")]
        public string ShipperCellphone { get; set; }
        [Description("发货联系方式电话/手机号码")]
        public string ShipperPhone { get; set; }
        [Description("收货单位")]
        public string AcceptUnit { get; set; }
        [Description("收货地址")]
        public string AcceptAddress { get; set; }
        [Description("收货联系人")]
        public string AcceptPeople { get; set; }
        [Description("收货联系电话")]
        public string AcceptTelephone { get; set; }
        [Description("收货手机号码")]
        public string AcceptCellphone { get; set; }
        [Description("收货联系电话/手机号码")]
        public string AcceptPhone { get; set; }

        public int PrintNum { get; set; }
        [Description("开单受理时间")]
        public DateTime HandleTime { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作人ID")]
        public string OP_ID { get; set; }
        public string UserName { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("作废标识")]
        public string DelFlag { get; set; }
        [Description("开单员")]
        public string CreateAwb { get; set; }
        [Description("开单时间")]
        public DateTime CreateDate { get; set; }

        public List<AwbGoodsEntity> AwbGoods { get; set; }
        public List<AwbFilesEntity> AwbFiles { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string OrderNum { get; set; }
        public string Goods { get; set; }
        [Description("配载合同号")]
        public string ContractNum { get; set; }
        public int ClientID { get; set; }
        [Description("回单状态")]
        public string ReturnStatus { get; set; }
        [Description("回单信息")]
        public string ReturnInfo { get; set; }
        [Description("到达时间")]
        public DateTime ArriveDate { get; set; }
        [Description("运输性质")]
        public string TransKind { get; set; }
        [Description("承运商ID")]
        public Int64 CarrierID { get; set; }
        public string CarrierShortName { get; set; }
        public string CarrierName { get; set; }
        public string DriverCellPhone { get; set; }//承运商联系电话
        public DateTime StartTime { get; set; }//发车时间
        public string AwbStatus { get; set; }
        public decimal ActMoney { get; set; }
        public DateTime ReturnDate { get; set; }
        public string SendReturnAwbStatus { get; set; }
        public string Sender { get; set; }
        public DateTime SendReturnAwbDate { get; set; }
        public string ConfirmReturnAwbStatus { get; set; }
        public DateTime ConfirmReturnAwbDate { get; set; }
        public string Confirmer { get; set; }
        public string ConfirmerName { get; set; }
        public string SendClientStatus { get; set; }
        public string ConfirmInfo { get; set; }
        public string FinanceFirstCheck { get; set; }
        public string FirstCheckName { get; set; }
        public DateTime FirstCheckDate { get; set; }
        public string FinanceSecondCheck { get; set; }
        public string SecondCheckName { get; set; }
        public DateTime SecondCheckDate { get; set; }
        [Description("结算状态")]
        public string CheckStatus { get; set; }
        public string AccountID { get; set; }
        public decimal PrepayFee { get; set; }
        public decimal ArriveFee { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal SendFee { get; set; }
        public decimal HandFee { get; set; }
        public decimal AwbProfit { get; set; }//运单利润
        public string PayMode { get; set; }
        public string TruckNum { get; set; }
        /// <summary>
        /// 已收
        /// </summary>
        public decimal ReceivedMoney { get; set; }
        /// <summary>
        /// 未收
        /// </summary>
        public decimal UncollectMoney { get; set; }
        public string RType { get; set; }
        public string FromTO { get; set; }
        /// <summary>
        /// 按照回单发送时间查询标志
        /// </summary>
        public string ReturnDateTip { get; set; }
        /// <summary>
        /// 签收人
        /// </summary>
        public string Signer { get; set; }
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime? SignTime { get; set; }
        /// <summary>
        /// 时效
        /// </summary>
        public int TimeLimit { get; set; }
        /// <summary>
        /// 最迟时效
        /// </summary>
        public string LatestTimeLimit { get; set; }

        public string FilePath { get; set; }
        public Int64 ArriveID { get; set; }
        public string FP { get; set; }
        public string ShouFlag { get; set; }
        public string ShouCheckName { get; set; }
        public DateTime ShouCheckDate { get; set; }
        public string ShowCheckStatus { get; set; }
        public decimal ShowMoney { get; set; }
        public string ShowOPID { get; set; }
        public DateTime ShowDate { get; set; }
        public string FuFlag { get; set; }
        public string FuCheckName { get; set; }
        public DateTime FuCheckDate { get; set; }
        public string FuCheckStatus { get; set; }
        public decimal FuMoney { get; set; }
        public string FuOPID { get; set; }
        public DateTime FuDate { get; set; }
        public string AKind { get; set; }
        /// <summary>
        /// 回单签收
        /// </summary>
        public DateTime AStartDate { get; set; }
        public DateTime AEndDate { get; set; }
        /// <summary>
        /// 回单发送
        /// </summary>
        public DateTime SStartDate { get; set; }
        public DateTime SEndDate { get; set; }
        /// <summary>
        /// 回单确认
        /// </summary>
        public DateTime CStartDate { get; set; }
        public DateTime CEndDate { get; set; }
        /// <summary>
        /// 配送时间
        /// </summary>
        public DateTime DStartDate { get; set; }
        public DateTime DEndDate { get; set; }
        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime GiveTime { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        public DateTime AbnormalTime { get; set; }
        /// <summary>
        /// 中转承运商ID
        /// </summary>
        public Int64 TCarrierID { get; set; }
        //public string CityCode { get; set; }
        public string ClientNum { get; set; }
        public string ClerkNo { get; set; }
        public string ClerkName { get; set; }
        /// <summary>
        /// 延误原因
        /// </summary>
        public string DelayReason { get; set; }
        public string DelayDay { get; set; }
        /// <summary>
        /// 是否延误
        /// </summary>
        public string IsDelay { get; set; }
        public string HLY { get; set; }
        public string DayReportNum { get; set; }
        public string DayReportCheck { get; set; }
        /// <summary>
        /// 上传回单照片状态
        /// 0：未上传默认
        /// 1：已上传
        /// </summary>
        public string UploadReturnPic { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
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
    /// 运单的货物品名表 有时运单中包含多个货物品名
    /// </summary>
    [Serializable]
    public class AwbGoodsEntity
    {
        public string BelongSystem { get; set; }
        public int GoodsID { get; set; }
        [Description("托运单号")]
        public string AwbNo { get; set; }
        [Description("包装")]
        public string Package { get; set; }
        [Description("品名")]
        public string Goods { get; set; }
        [Description("件数")]
        public int Piece { get; set; }
        [Description("件数单价")]
        public decimal PiecePrice { get; set; }
        [Description("重量")]
        public decimal Weight { get; set; }
        [Description("重量单价")]
        public decimal WeightPrice { get; set; }
        [Description("体积")]
        public decimal Volume { get; set; }
        [Description("体积单价")]
        public decimal VolumePrice { get; set; }
        [Description("声明价值")]
        public string DeclareValue { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        public string ShouFlag { get; set; }
        public string ShowCheckStatus { get; set; }
        public decimal ShowMoney { get; set; }
        public string ShowOPID { get; set; }
        public DateTime ShowDate { get; set; }
        public string FuFlag { get; set; }
        public string FuCheckStatus { get; set; }
        public decimal FuMoney { get; set; }
        public string FuOPID { get; set; }
        public DateTime FuDate { get; set; }
        public string AKind { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public string GoodsCode { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string Batch { get; set; }
        public string ContainerCode { get; set; }
        public string TypeName { get; set; }

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
    /// 运单与副单号关联实体
    /// </summary>
    [Serializable]
    public class HAwbNoEntity
    {
        public string AwbNo { get; set; }
        public string HAwbNo { get; set; }
        public string BelongSystem { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
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
