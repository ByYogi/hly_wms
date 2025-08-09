using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace House.Entity.Cargo.Order
{
    /// <summary>
    /// 进货订单与产品关联表
    /// </summary>
    [Serializable]
    public class CargoPurchaseOrderGoodsEntity
    {
        [Description("订单号")]
        public Int64 OrderID { get; set; }
        /// <summary>
        /// 进货单号
        /// </summary>
        public string FacOrderNo { get; set; }
        [Description("产品类型ID")]
        public int TypeID { get; set; }
        /// <summary>
        /// 来货件数
        /// </summary>
        [Description("来货件数")]
        public int Piece { get; set; }
        /// <summary>
        /// 收货件数
        /// </summary>
        [Description("收货件数")]
        public int ReceivePiece { get; set; }
        /// <summary>
        /// 退货件数
        /// </summary>
        [Description("退货件数")]
        public int ReturnPiece { get; set; }
        public int InPiece { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        [Description("产品编码")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [Description("规格")]
        public string Specs { get; set; }
        /// <summary>
        /// 花纹
        /// </summary>
        [Description("花纹")]
        public string Figure { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        [Description("型号")]
        public string Model { get; set; }
        /// <summary>
        /// 货品代码
        /// </summary>
        [Description("货品代码")]
        public string GoodsCode { get; set; }
        /// <summary>
        /// 载重
        /// </summary>
        [Description("载重")]
        public string LoadIndex { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        [Description("速度")]
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 生产地（0国产1进口）
        /// </summary>
        [Description("生产地")]
        public int Born { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Description("批次")]
        public string Batch { get; set; }
        /// <summary>
        /// 周期年份
        /// </summary>
        public int BatchYear { get; set; }
        /// <summary>
        /// 进货价
        /// </summary>
        [Description("进货价")]
        public double UnitPrice { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        [Description("成本价")]
        public double CostPrice { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        [Description("批发价")]
        public double TradePrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        [Description("销售价")]
        public double SalePrice { get; set; }
        [Description("进仓价")]
        public double InHousePrice { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string OP_ID { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OP_DATE { get; set; }
        public int HouseID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ClientNum { get; set; }
        public string TrafficType { get; set; }
        public DateTime CreateDate { get; set; }
        #region 传值
        /// <summary>
        /// 进仓订单详情产品名称
        /// </summary>
        public string ProductName { get; set; }
        // <summary>
        /// 进仓订单详情产品类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 载数
        /// </summary>
        public string LoadSpeed { get; set; }
        #endregion

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

    public class CargoPurchaseOrderDto
    {
        /// <summary>
        /// 进仓单号
        /// </summary>
        public string FacOrderNo { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string AcceptUnit { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string AcceptAddress { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public int ClientNum { get; set; }
        /// <summary>
        /// 订单退货备注
        /// </summary>
        public string returnRemark { get; set; }
        /// <summary>
        /// 进仓订单产品明细List
        /// </summary>
        public List<CargoPurchaseOrderGoodsEntity> List { get; set; }

        // <summary>
        /// 进仓单所在仓库
        /// </summary>
        public int HouseID { get; set; }
        /// <summary>
        /// 进仓订单
        /// </summary>
        public CargoPurchaseOrderEntity PurchaseOrder { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
    }
}
