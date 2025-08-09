using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 客户来款记录数据实体1.3.3.Tbl_Cargo_InComeMoney（来款数据记录表）
    /// </summary>
    [Serializable]
    public class CargoClientIncomeEntity
    {
        public Int64 IncomeID { get; set; }
        public Int64 ClientID { get; set; }
        public int ClientNum { get; set; }
        public decimal Money { get; set; }
        public DateTime CreateDate { get; set; }
        public string RecordType { get; set; }
        public int BasicID { get; set; }
        public int HouseID { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }
        public string Boss { get; set; }
        public string CargoPermisID { get; set; }
        public string Cellphone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CardType { get; set; }
        public string Aliases { get; set; }
        public string Bank { get; set; }
        public string CardName { get; set; }
        public string CardNum { get; set; }
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
