using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 移库数据实体 Tbl_Cargo_MoveContainer（移库数据表）
    /// </summary>
    [Serializable]
    public class CargoMoveContainerEntity
    {
        public long ID { get; set; }
        public string OldCCode { get; set; }
        public int OldCID { get; set; }
        public long ProductID { get; set; }
        public string NewCCode { get; set; }
        public int NewCID { get; set; }
        public int MoveNum { get; set; }
        public string MoveStatus { get; set; }
        public int HouseID { get; set; }
        public string OPID { get; set; }
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
