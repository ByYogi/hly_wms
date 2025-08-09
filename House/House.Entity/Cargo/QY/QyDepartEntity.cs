using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{ 
    /// <summary>
    /// 企业号部门数据实体1.6.1.Tbl_QY_Depart（企业号部门表）
    /// </summary>
    [Serializable]
    public class QyDepartEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parentid { get; set; }
        public string DepOrder { get; set; }
        public string ParentName { get; set; }
        public int BossID { get; set; }
        public string Boss { get; set; }
        public int LeaderID { get; set; }
        public string Leader { get; set; }
        public DateTime OP_DATE { get; set; }
    }

    public class DepartmentListReturnJson
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public List<userlist> userlist;
    }
    public class userlist
    {
        /// <summary>
        /// 成员UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 成员所属部门id列表
        /// </summary>
        public List<int> department { get; set; }
        /// <summary>
        /// 职务信息
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 头像url
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 激活状态: 1=已激活，2=已禁用，4=未激活，5=退出企业。
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 是否管理人员
        /// </summary>
        public int isleader { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string english_name { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 部门排序值
        /// </summary>
        public List<int> order { get; set; }
        /// <summary>
        /// 主部门
        /// </summary>
        public int main_department { get; set; }
        /// <summary>
        /// 员工个人二维码
        /// </summary>
        public string qr_code { get; set; }
        /// <summary>
        /// 表示在所在的部门内是否为上级
        /// </summary>
        public List<int> is_leader_in_dept { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 头像缩略图url
        /// </summary>
        public string thumb_avatar { get; set; }
    }
}
