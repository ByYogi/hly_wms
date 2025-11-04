using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo.Order
{
    [Serializable]
    public class CargoReserveOrderGoodsEntity
    {
        public int InHouseDay { get; set; }
        public string OrderNo { get; set; }
        public long ProductID { get; set; }
        public int HouseID { get; set; }
        public int AreaID { get; set; }
        public string ContainerCode { get; set; }
        public int ContainerID { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int Piece { get; set; }
        public int ScanPiece { get; set; }
        /// <summary>
        /// 实际销售价业务员价
        /// </summary>
        public decimal ActSalePrice { get; set; }
        /// <summary>
        /// 供应商销售价
        /// </summary>
        public decimal SupplySalePrice { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        public string OutCargoID { get; set; }
        /// <summary>
        /// 入库表ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 关联订单
        /// </summary>
        public string RelateOrderNo { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderModel { get; set; }
        /// <summary>
        /// 特价商品上架ID
        /// </summary>
        public int SpecialID { get; set; }
        /// <summary>
        /// 规则类型
        /// </summary>
        public string RuleType { get; set; }
        /// <summary>
        /// 规则ID
        /// </summary>
        public string RuleID { get; set; }
        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleTitle { get; set; }
        /// <summary>
        /// 适用客户
        /// </summary>
        public string SuitClientNum { get; set; }
        public string GoodsCode { get; set; }
        public int OutCargoType { get; set; }
        public string Specs { get; set; }
        public string Batch { get; set; }
        public string Assort { get; set; }
        public decimal TaxCostPrice { get; set; }
        public decimal NoTaxCostPrice { get; set; }
        public string SpecsType { get; set; }
        public string Figure { get; set; }
        public string Born { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public string Model { get; set; }
        public int PurchaserID { get; set; }
        public string PurchaserName { get; set; }
        public string DeliveryBoss { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCellphone { get; set; }
        public decimal ConfirmSalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public string FacOrderNo { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TradePrice { get; set; }
        public decimal InHousePrice { get; set; }
        public string Supplier { get; set; }
        public string PayClientName { get; set; }
        public string AreaName { get; set; }
        /// <summary>
        /// 所在仓库
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 载数
        /// </summary>
        public string LoadSpeed { get; set; }
        public int SuppClientNum { get; set; }
        /// <summary>
        /// 发货单显示货品代码
        /// </summary>
        public string ShowGoodsCode { get; set; }
        /// <summary>
        /// 共享产品编码
        /// </summary>
        public string ShowProductCode { get; set; }
        /// <summary>
        /// 共享产品品牌名称
        /// </summary>
        public string ShowTypeName { get; set; }
        public string TagCode { get; set; }
        public string TyreCode { get; set; }
        public int AwbStatus { get; set; }
        public int DeliveryType { get; set; }
        public int FinanceSecondCheck { get; set; }
        public int CheckStatus { get; set; }
        public string CreateAwb { get; set; }
        public string BelongDepart { get; set; }
        public string BelongMonth { get; set; }
        public int OrderType { get; set; }
        public DateTime CreateDate { get; set; }

        public decimal OverDueFee { get; set; }
        public int OverDayNum { get; set; }
        public string Source { get; set; }
        public string SourceName { get; set; }

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
        #region 请求及返回字段
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PayClientNum { get; set; }
        /// <summary>
        /// 总成本价
        /// </summary>
        public decimal TotalCostPrice { get; set; }
        /// <summary>
        /// 查询所属仓库ID
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string AcceptUnit { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string ThrowGood { get; set; }
        /// <summary>
        /// 库存件数
        /// </summary>
        public int InHousePiece { get; set; }
        public DateTime InHouseTime { get; set; }
        /// <summary>
        /// 库存金额
        /// </summary>
        public decimal InHouseTotalPrice { get; set; }
        /// <summary>
        /// 平均销售价(销售总额/销售数量)
        /// </summary>
        public decimal AvgSalePrice { get; set; }
        /// <summary>
        /// 供应商地址
        /// </summary>
        public string SupplierAddress { get; set; }
        #endregion
    }
}
