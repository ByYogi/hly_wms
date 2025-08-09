using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 查询新陆程 推送给外部系统的运单 数据 
    /// </summary>
    [Serializable]
    public class CargoNewayDataPush
    {
        public long ID { get; set; }
        public string NwAwbNo { get; set; }
        public string HlyAwbNo { get; set; }
        public string BelongSystem { get; set; }
        public string CurCity { get; set; }
        public string UserName { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime OPDATE { get; set; }
        public string AcceptPeople { get; set; }
        public string AcceptCellphone { get; set; }
        public string AcceptAddress { get; set; }
        public string ShipperName { get; set; }
        public int Piece { get; set; }
        public decimal Weight { get; set; }
        public string PushStatus { get; set; }
        public string Dest { get; set; } 
        public string SplitCB { get; set; }
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
