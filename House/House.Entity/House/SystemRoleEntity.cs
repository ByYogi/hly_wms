using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.House
{
    /// <summary>
    /// 角色 实体类 对应 1.1.9.Tbl_SysRole（角色表）
    /// </summary>
    [Serializable]
    public class SystemRoleEntity
    {
        public int RoleID { get; set; }
        public string CName { get; set; }
        public string Remark { get; set; }
        public string DelFlag { get; set; }
        public string IsAdmin { get; set; }
        public DateTime OP_DATE { get; set; }
    }
    /// <summary>
    /// 角色与导航关联实体
    /// </summary>
    [Serializable]
    public class SystemRoleItemEntity
    {
        public int RoleID { get; set; }
        public int ItemID { get; set; }
        public DateTime OP_DATE { get; set; }
    }
}
