using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 每天仓库 库存统计数据实体1.7.3.Tbl_Cargo_DayCargoStock（每天仓库库存数据表）
    /// </summary>
    [Serializable]
    public class CargoDayStockEntity
    {
        public long ID { get; set; }
        public DateTime StatisDate { get; set; }
        public int HouseID { get; set; }
        public string Name { get; set; }
        public int TotalSum { get; set; }
        public int MaPaiSum { get; set; }
        public double MaPaiTotalValue { get; set; }
        public int YKHMSum { get; set; }
        public double YKHMTotalValue { get; set; }
        public int HanTaiSum { get; set; }
        public double HanTaiTotalValue { get; set; }
        public int GuBoSum { get; set; }
        public double GuBoTotalValue { get; set; }
        public int MQLSum { get; set; }
        public double MQLTotalValue { get; set; }
        public int WKTSum { get; set; }
        public double WKTTotalValue { get; set; }
        public int QiTaSum { get; set; }
        public double QiTaTotalValue { get; set; }
        public double TotalValue { get; set; }
        public int BigTyreSum { get; set; }
        public int MidTyreSum { get; set; }
        public int SmallTyreSum { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 区域父ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 显示用月数据
        /// </summary>
        public string StatisDis { get; set; }
        /// <summary>
        /// 当前月租金金额
        /// </summary>
        public decimal CurRentMoney { get; set; }
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
}
