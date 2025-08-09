using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 配载实体类Tbl_DepManifest
    /// </summary>
    [Serializable]
    public class DepManifestEntity
    {
        public string BelongSystem { get; set; }
        [Description("司机合同号")]
        public string ContractNum { get; set; }
        [Description("起始站")]
        public string Dep { get; set; }
        [Description("中转站")]
        public string Transit { get; set; }
        [Description("到达站")]
        public string Dest { get; set; }
        [Description("车牌号码")]
        public string TruckNum { get; set; }
        [Description("运费")]
        public decimal TransportFee { get; set; }
        [Description("预付")]
        public decimal PrepayFee { get; set; }
        [Description("到付")]
        public decimal ArriveFee { get; set; }
        public decimal OtherFee { get; set; }
        [Description("结款方式")]
        public string PayMode { get; set; }
        [Description("发车时间")]
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        [Description("运行时间")]
        public decimal PassTime { get; set; }
        [Description("预计到达时间")]
        public DateTime PreArriveTime { get; set; }
        [Description("重量")]
        public decimal Weight { get; set; }
        [Description("体积")]
        public decimal Volume { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        [Description("司机姓名")]
        public string Driver { get; set; }
        [Description("司机手机号码")]
        public string DriverCellPhone { get; set; }
        [Description("司机身份证号码")]
        public string DriverIDNum { get; set; }
        [Description("司机身份证地址")]
        public string DriverIDAddress { get; set; }
        [Description("卸货地址")]
        public string UnLoadAddress { get; set; }
        [Description("到达站联系人")]
        public string DestPeople { get; set; }
        [Description("到达站联系方式")]
        public string DestCellphone { get; set; }
        [Description("承运商ID")]
        public Int64 CarrierID { get; set; }
        [Description("运输性质")]
        public string TransKind { get; set; }
        [Description("承运商简称")]
        public string CarrierShortName { get; set; }
        public string CarrierName { get; set; }
        public string Boss { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public int Piece { get; set; }
        public decimal CutPay { get; set; }

        public string Model { get; set; }
        public decimal Length { get; set; }
        public decimal TruckWeight { get; set; }
        public string Range { get; set; }
        public string DelFlag { get; set; }
        public List<AwbEntity> AwbInfo { get; set; }
        public string ShipperUnit { get; set; }

        public string FinanceFirstCheck { get; set; }
        public string FirstCheckName { get; set; }
        public DateTime FirstCheckDate { get; set; }
        public string FinanceSecondCheck { get; set; }
        public string SecondCheckName { get; set; }
        public DateTime SecondCheckDate { get; set; }
        [Description("结算状态")]
        public string CheckStatus { get; set; }
        /// <summary>
        /// 回单状态
        /// </summary>
        public string ReturnStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string TruckFlag { get; set; }
        public DateTime ActArriveTime { get; set; }
        public string AwbNo { get; set; }
        /// <summary>
        /// 提货费
        /// </summary>
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 发货费
        /// </summary>
        public decimal SendFee { get; set; }
        /// <summary>
        /// 代收款
        /// </summary>
        public decimal CollectMoney { get; set; }
        /// <summary>
        /// 装卸费
        /// </summary>
        public decimal HandFee { get; set; }
        public string AccountID { get; set; }
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
        public string DType { get; set; }

        public decimal TotalFee { get; set; }
        public int TotalAwbPiece { get; set; }
        public int AwbNum { get; set; }
        //直达体积
        public decimal noStopVolume { get; set; }
        //直达收入
        public decimal noStopFee { get; set; }
        //中转体积
        public decimal transitVolume { get; set; }
        //中转收入
        public decimal tranistFee { get; set; }
        /// <summary>
        /// 监装员
        /// </summary>
        [Description("监装员")]
        public string Loader { get; set; }
        /// <summary>
        /// 配载员
        /// </summary>
        [Description("配载员")]
        public string Manifester { get; set; }
        public string Dispatcher { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 作废标志
        /// </summary>
        public string CancelFlag { get; set; }
        public decimal ProfitPCT { get; set; }
        public string CardBank { get; set; }
        public string CardName { get; set; }
        public string CardNum { get; set; }
        public string OPDateTip { get; set; }
        public decimal PiecePrice { get; set; }
        public decimal WeightPrice { get; set; }
        public decimal VolumePrice { get; set; }
        /// <summary>
        /// 卸货备注
        /// </summary>
        public string UnloadMemo { get; set; }
        /// <summary>
        /// 合同链接
        /// </summary>
        public string ContractURL { get; set; }
        /// <summary>
        /// 合同上传状态
        /// </summary>
        public string ContractStatus { get; set; }
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
