using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 申请审批关联数据表
    /// </summary>
    [Serializable]
    public class CargoApproveRelateEntity
    {
        public long ID { get; set; }
        public long ApproveID { get; set; }
        public long RelateID { get; set; }
        public int ThrowNum { get; set; }
        public decimal ThrowCharge { get; set; }
        public DateTime OP_DATE { get; set; }
        public string ApplyType { get; set; }
        public int TypeID { get; set; }
        public long ProductID { get; set; }
        public int Piece { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 花纹
        /// </summary>
        public string Figure { get; set; }
        /// <summary>
        /// 载重指数
        /// </summary>
        public string LoadIndex { get; set; }
        /// <summary>
        /// 速度级别
        /// </summary>
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 货位代码
        /// </summary>
        public string ContainerCode { get; set; }
        public string ProductName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TaxCostPrice { get; set; }
        public decimal NoTaxCostPrice { get; set; }
        public decimal TradePrice { get; set; }
        public decimal FinalCostPrice { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int HouseID { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string HouseName { get; set; }
        public int ContainerID { get; set; }
        public string TypeName { get; set; }

        public void EnSafe()
        {
            PropertyInfo[] pSource = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo s in pSource)
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
