using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 订单价格修改申请数据实体1.8.5.Tbl_QY_OrderUpdatePrice（订单价格修改申请表）
    /// </summary>
    [Serializable]
    public class QyOrderUpdatePriceEntity
    {
        public long OID { get; set; }
        public long OrderID { get; set; }
        public string OrderNo { get; set; }
        public int HouseID { get; set; }
        public string ApplyID { get; set; }
        public string ApplyName { get; set; }
        public DateTime ApplyDate { get; set; }
        public string CheckID { get; set; }
        public string CheckName { get; set; }
        public DateTime CheckTime { get; set; }
        public string Reason { get; set; }
        public string CheckResult { get; set; }
        public string ApplyStatus { get; set; }
        public string OrderType { get; set; }
        public string SaleManName { get; set; }
        public string SaleManID { get; set; }
        public string HouseName { get; set; }
        public DateTime OP_DATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TNum { get; set; }
        public string CheckType { get; set; }
        public string ReadStatus { get; set; }
        public string ApproveType { get; set; }
        /// <summary>
        /// 订单审批类型
        /// </summary>
        public string OrderCheckType { get; set; }
        /// <summary>
        /// 查询所属仓库订单
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 审批仓库权限列表
        /// </summary>
        public string CheckHouseID { get; set; }
        public List<QyOrderUpdateGoodsEntity> UpdatePriceGoodsList { get; set; }
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
    /// 订单价格修改申请明细数据实体1.8.6.Tbl_QY_OrderUpdateGoods（订单修改申请明细表）
    /// </summary>
    [Serializable]
    public class QyOrderUpdateGoodsEntity
    {
        public long OID { get; set; }
        public long OrderID { get; set; }
        public long ShelvesID { get; set; }
        public int OrderNum { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal ModifyPrice { get; set; }
        public long ProductID { get; set; }
        public string ContainerCode { get; set; }
        public decimal CostPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxCostPrice { get; set; }
        public decimal FinalCostPrice { get; set; }
        public string Title { get; set; }
        public string Batch { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string GoodsCode { get; set; }
        public string Model { get; set; }
        public string LoadIndex { get; set; }
        public int TypeID { get; set; }
        public int TypeParentID { get; set; }
        public string SpeedLevel { get; set; }
        public string TypeName { get; set; }
        public string ProductCode { get; set; }
        public string HouseName { get; set; }
        
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
