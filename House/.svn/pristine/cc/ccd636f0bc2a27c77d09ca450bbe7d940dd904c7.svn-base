using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 1.5.22.Tbl_Cargo_ExterOrderAllo（外部订单分配清单列表数据表）
    /// </summary>
    [Serializable]
    public class CargoExterOrderAlloEntity
    {
        public string OrderOwnerName { get; set; }
        public string ExterOrderAlloNo { get; set; }
        public int ExterOrderAlloNum { get; set; }
        public string OrderAlloType { get; set; }
        public int AlloPiece { get; set; }
        public string AcceptLoginName { get; set; }
        public string AcceptName { get; set; }
        public DateTime AcceptDATE { get; set; }
        public string OPLoginName { get; set; }
        public string OPName { get; set; }
        public DateTime OPDATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderNo { get; set; }
        public List<string> TID { get; set; }
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
