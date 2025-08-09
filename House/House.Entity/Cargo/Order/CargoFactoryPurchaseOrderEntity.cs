using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 工厂订货单实体
    /// </summary>
    [Serializable]
    public class CargoFactoryPurchaseOrderEntity
    {
        [Description("表主键")]
        public long OrderID { get; set; }
        [Description("订单号")]
        public string OrderNo { get; set; }
        [Description("总件数")]
        public int Piece { get; set; }
        [Description("总费用")]
        public double TotalCharge { get; set; }
        [Description("仓库ID")]
        public int HouseID { get; set; }
        [Description("仓库名称")]
        public string HouseName { get; set; }
        [Description("审批状态")]
        public string CheckStatus { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作账号")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public string ApplyID { get; set; }
        public string ApplyName { get; set; }
        public DateTime ApplyDate { get; set; }
        public string NextCheckID { get; set; }
        public string NextCheckName { get; set; }
        public DateTime CheckTime { get; set; }
        public string CheckResult { get; set; }
        public string ApplyStatus { get; set; }
        public string FacOrderNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
    /// 工厂订货单产品实体
    /// </summary>
    public class CargoFactoryPurchaseOrderGoodsEntity
    {
        [Description("表主键")]
        public long ID { get; set; }
        [Description("订单号")]
        public string OrderNo { get; set; }
        public long OrderID { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string Specs { get; set; }
        public string Figure { get; set; }
        public string Model { get; set; }
        public string Born { get; set; }
        public string GoodsCode { get; set; }
        public string LoadIndex { get; set; }
        public string SpeedLevel { get; set; }
        public int Piece { get; set; }
        public int OldPiece { get; set; }
        public double UnitPrice { get; set; }
        public int WhetherTax { get; set; }
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
