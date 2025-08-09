using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 财务基础账号数据实体1.6.1.Tbl_Cargo_BasicData（财务基础数据管理）
    /// </summary>
    [Serializable]
    public class CargoFinanceBasicDataEntity
    {
        [Description("ID号")]
        public int BasicID { get; set; }
        [Description("所属仓库")]
        public int HouseID { get; set; }
        [Description("类型")]
        public string CardType { get; set; }
        [Description("别名")]
        public string Aliases { get; set; }
        [Description("开户行")]
        public string Bank { get; set; }
        [Description("开户名")]
        public string CardName { get; set; }
        [Description("账号")]
        public string CardNum { get; set; }
        [Description("余额")]
        public decimal OverMoney { get; set; }
        [Description("备注")]
        public string Memo { get; set; }
        [Description("状态")]
        public string Status { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 适用仓库ID
        /// </summary>
        public string ApplyHouseID { get; set; }
        /// <summary>
        /// 查询所属仓库订单
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 所属仓库名称
        /// </summary>
        public string HouseName { get; set; }
        public string PermisBasicID { get; set; }
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
