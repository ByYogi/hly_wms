using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace House.Entity.House
{
    /// <summary>
    /// 导航实体
    /// </summary>
    [Serializable]
    public class SystemItemEntity
    {
        [Description("表主键")]
        public int ItemID { get; set; }
        [Description("系统ID")]
        public int HouseID { get; set; }
        [Description("系统代码")]
        public string HouseCode { get; set; }
        [Description("系统名称")]
        public string HouseName { get; set; }
        [Description("导航中文名称")]
        public string CName { get; set; }
        [Description("系统英文名称")]
        public string EName { get; set; }
        [Description("系统链接")]
        public string ItemSrc { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [Description("父级ID")]
        public int ParentID { get; set; }
        [Description("导航级别")]
        public string ItemLevel { get; set; }
        [Description("导航排序")]
        public string ItemSort { get; set; }
        [Description("删除标志")]
        public string DelFlag { get; set; }
        [Description("导航图标")]
        public string ItemIcon { get; set; }

        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 父级名称
        /// </summary>
        public string ParentCName { get; set; }
        /// <summary>
        /// 系统备注
        /// </summary>
        public string HouseRemark { get; set; }

    }
}
