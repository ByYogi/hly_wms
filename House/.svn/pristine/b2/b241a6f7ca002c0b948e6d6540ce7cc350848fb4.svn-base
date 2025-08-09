using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 物流线路管理数据实体1.6.10.Tbl_Cargo_LogisLine（物流线路管理数据表）
    /// </summary>
    [Serializable]
    public class CargoLogisLineEntity
    {

        [Description("表主键")]
        public int LineID { get; set; }
        [Description("区域大仓")]
        public int HouseID { get; set; }
        /// <summary>
        /// 所属仓库
        /// </summary>
        public string HouseName { get; set; }
        public string AreaName { get; set; }
        [Description("线路名称")]
        public string LineName { get; set; }
        [Description("物流承运商")]
        public string LogisticName { get; set; }
        [Description("物流承运商ID")]
        public int LogisID { get; set; }
        [Description("备注")]
        public string Memo { get; set; }
        [Description("删除标识")]
        public string DelFlag { get; set; }
        public string OP_ID { get; set; }
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
        public List<CargoLogisLineClientEntity> LogisLineClientList { get; set; }
    }


    /// <summary>
    /// 线路与客户绑定关系数据实体1.6.11.Tbl_Cargo_LogisLineClient（物流线路客户绑定数据表）
    /// </summary>
    [Serializable]
    public class CargoLogisLineClientEntity
    {
        public int ID { get; set; }
        public int LineID { get; set; }
        public string LineName { get; set; }
        public long ClientID { get; set; }
        public string OP_ID { get; set; }
        public DateTime OP_DATE { get; set; }

        public string ShopCode { get; set; }
        public string ClientName { get; set; }
        public string ClientShortName { get; set; }
        public string Boss { get; set; }
        public string Cellphone { get; set; }
        public string Telephone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
