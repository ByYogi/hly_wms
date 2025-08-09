using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo.Order
{
    /// <summary>
    /// 进货订单表
    /// </summary>
    [Serializable]
    public class CargoPurchaseOrderEntity
    {
        /// <summary>
        /// 进货单号
        /// </summary>
        [Description("进货单号")]
        public string FacOrderNo { get; set; }
        [Description("订单号")]
        public Int64 OrderID { get; set; }
        [Description("客户编码")]
        public int ClientNum { get; set; }

        /// <summary>
        /// 物流公司运单号
        /// </summary>
        [Description("物流公司运单号")]
        public string LogisAwbNo { get; set; }
        /// <summary>
        /// 物流公司ID
        /// </summary>
        [Description("物流公司ID")]
        public int LogisID { get; set; }
        /// <summary>
        /// 物流结算 0现付 1到付
        /// </summary>
        [Description("物流结算")]
        public string DeliverySettlement { get; set; }
        /// <summary>
        /// 出发站
        /// </summary>
        [Description("出发站")]
        public string Dep { get; set; }
        /// <summary>
        /// 到达站
        /// </summary>
        [Description("到达站")]
        public string Dest { get; set; }
        /// <summary>
        /// 总件数
        /// </summary>
        [Description("总件数")]
        public int Piece { get; set; }
        /// <summary>
        /// 配送费
        /// </summary>
        [Description("配送费")]
        public double TransitFee { get; set; }
        /// <summary>
        /// 销售费用
        /// </summary>
        [Description("销售费用")]
        public double TransportFee { get; set; }
        /// <summary>
        /// 装卸费
        /// </summary>
        [Description("装卸费")]
        public double HandFee { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        [Description("其他费用")]
        public double OtherFee { get; set; }
        /// <summary>
        /// 合计总费用
        /// </summary>
        [Description("合计总费用")]
        public double TotalCharge { get; set; }
        /// <summary>
        /// 结算方式（0:现付1:回单2:月结3:到付4:代收款5:微信付款6:额度付款7:现金8:周结9:半月结）
        /// </summary>
        [Description("结算方式")]
        public int CheckOutType { get; set; }
        /// <summary>
        /// 订单类型(0:内部订单/订货单，1:外部订单，2:退货订单，3:外采订单/调货单，4.进仓单,5.进仓退货单)
        /// </summary>
        [Description("订单类型")]
        public int TrafficType { get; set; }
        /// <summary>
        /// 送货方式（0:送货1:自提）
        /// </summary>
        [Description("送货方式")]
        public int DeliveryType { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [Description("客户名称")]
        public string AcceptUnit { get; set; }
        /// <summary>
        /// 客户详细地址
        /// </summary>
        [Description("客户详细地址")]
        public string AcceptAddress { get; set; }
        /// <summary>
        /// 客户联系人
        /// </summary>
        [Description("客户联系人")]
        public string AcceptPeople { get; set; }
        /// <summary>
        /// 客户联系电话
        /// </summary>
        [Description("客户联系电话")]
        public string AcceptTelephone { get; set; }
        /// <summary>
        /// 客户手机
        /// </summary>
        [Description("客户手机")]
        public string AcceptCellphone { get; set; }
        /// <summary>
        /// 开单员ID
        /// </summary>
        [Description("开单员ID")]
        public string CreateAwbID { get; set; }
        /// <summary>
        /// 开单员
        /// </summary>
        [Description("开单员")]
        public string CreateAwb { get; set; }
        /// <summary>
        /// 开单时间
        /// </summary>
        [Description("开单时间")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 结算状态(0:未结算1:已结清2:未结清)
        /// </summary>
        [Description("结算状态")]
        public int CheckStatus { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        [Description("所在仓库")]
        public int HouseID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        [Description("账单号")]
        public string AccountNo { get; set; }

        /// <summary>
        ///收货状态(0:未收货1:已收货2:部分收货)
        /// </summary>
        [Description("收货状态")]
        public string ReceivingStatus { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string OP_ID { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OP_DATE { get; set; }

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
        #region 用于前端显示/查询字段
        public string HouseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 查询所属仓库ID
        /// </summary>
        public string CargoPermisID { get; set; }
        #endregion
    }
}
