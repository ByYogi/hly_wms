using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.House
{
    /// <summary>
    /// 系统管理实体类 对应系统管理数据表1.1.1.Tbl_SysHouse（系统管理表）
    /// </summary>
    [Serializable]
    public class SystemSetEntity
    {
        [Description("表主键")]
        public int HouseID { get; set; }
        [Description("系统名称")]
        public string HouseName { get; set; }
        [Description("系统代码")]
        public string HouseCode { get; set; }
        [Description("系统联系人")]
        public string Person { get; set; }
        [Description("联系人电话 ")]
        public string Cellphone { get; set; }
        [Description("QQ号")]
        public string QQ { get; set; }
        [Description("微信号")]
        public string Weixin { get; set; }
        [Description("邮箱地址")]
        public string Email { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("删除标志")]
        public string DelFlag { get; set; }
    }
}
