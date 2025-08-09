using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 微信企业号用户管理实体 1.6.2.Tbl_QY_User（企业员员工信息表）
    /// </summary>
    [Serializable]
    public class QyUserEntity
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string weixinID { get; set; }
        public string OpenID { get; set; }
        public string WxName { get; set; }
        public string CellPhone { get; set; }
        public string Department { get; set; }
        public string DepartName { get; set; }
        public int MainDepartment { get; set; }
        public string MainDepartmentName { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Isleader { get; set; }
        public string AvatarSmall { get; set; }
        public string AvatarBig { get; set; }
        public string Telephone { get; set; }
        public string Enable { get; set; }
        public string English_name { get; set; }
        public string QR_code { get; set; }
        public string Status { get; set; }
        public string Tag { get; set; }
        public string TagName { get; set; }
        /// <summary>
        /// 审批角色
        /// </summary>
        public string CheckRole { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 所属仓库ID
        /// </summary>
        public int HouseID { get; set; }
        /// <summary>
        /// 审批仓库
        /// </summary>
        public string CheckHouseID { get; set; }
        public string CheckHouseName { get; set; }
        /// <summary>
        /// 所属仓库名称
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 所管理仓库权限集合，以仓库ID组成的数组1,2,3,5,
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 所管理仓库名称权限集合
        /// </summary>
        public string CargoPermisName { get; set; }
        /// <summary>
        /// 是否前置仓账号
        /// </summary>
        public int IsHeadHouse { get; set; }
        /// <summary>
        /// 前置仓ID
        /// </summary>
        public int HeadHouseID { get; set; }
        /// <summary>
        /// 前置仓名称
        /// </summary>
        public string HeadHouseName { get; set; }
        public string IsQueryLockStock { get; set; }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
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
