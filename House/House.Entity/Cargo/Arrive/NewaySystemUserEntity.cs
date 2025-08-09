using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 新路程用户实体类
    /// </summary>
    [Serializable]
    public class NewaySystemUserEntity
    {
        /// <summary>
        /// 自增长表主键
        /// </summary>
        [Description("用户ID")]
        public int UserID { get; set; }
        /// <summary>
        /// 用户编号相当于员工号
        /// </summary>
        [Description("用户编号")]
        public string UserNum { get; set; }
        /// <summary>
        /// 登陆系统用户名
        /// </summary>
        [Description("用户登陆名")]
        public string LoginName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        [Description("用户登陆密码")]
        public string LoginPwd { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [Description("用户真实姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 性别 
        /// </summary>
        [Description("性别")]
        public string Sex { get; set; }
        [Description("年龄")]
        public int Age { get; set; }
        /// <summary>
        /// 用户的身份证号
        /// </summary>
        [Description("身份证号")]
        public string UserIDNum { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public string RoleCName { get; set; }
        [Description("所属角色ID")]
        public int RoleID { get; set; }
        public string UnitCName { get; set; }
        [Description("所属单位ID")]
        public int UnitID { get; set; }
        public string DepCName { get; set; }
        [Description("所属部门ID")]
        public int DepID { get; set; }
        /// <summary>
        /// 所属岗位
        /// </summary>
        [Description("所属岗位")]
        public string UserJob { get; set; }
        [Description("住址电话")]
        public string AddressPhone { get; set; }
        [Description("公司电话")]
        public string CompanyPhone { get; set; }
        [Description("手机号码")]
        public string CellPhone { get; set; }
        [Description("电子邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 入岗日期时间
        /// </summary>
        [Description("入岗日期")]
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 转正日期
        /// </summary>
        [Description("转正日期")]
        public DateTime OfficialTime { get; set; }
        /// <summary>
        /// 合约到期时间
        /// </summary>
        [Description("合约到期时间")]
        public DateTime DealCloseTime { get; set; }
        /// <summary>
        /// 合约种类0试用1在职2离职
        /// </summary>
        [Description("合约种类")]
        public string DealType { get; set; }
        public string IPAddress { get; set; }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        [Description("作废标识")]
        public string DelFlag { get; set; }
        public DateTime OP_DATE { get; set; }
        [Description("城市权限")]
        public string CityCode { get; set; }
        [Description("所在城市")]
        public string CurCity { get; set; }
        [Description("入库员代码")]
        public string ClerkNo { get; set; }
        public string BelongSystem { get; set; }
        public string ManSystem { get; set; }
        public string ManSystemName { get; set; }
        public string AccountTag { get; set; }
        public string CacheTag { get; set; }
        public string SystemName { get; set; }
        public string IsAdmin { get; set; }
        public string ButtonPower { get; set; }
        public decimal PayLimit { get; set; }
        public List<CityEntity> AllCity { get; set; }
        public List<SystemSetEntity> AllSystem { get; set; }

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
    /// <summary>
    /// 用户与导航的权限关系实体
    /// </summary>
    public class NewaySystemUserItemEntity
    {
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public DateTime OP_DATE { get; set; }
    }

    /// <summary>
    /// 系统设置实体
    /// </summary>
    [Serializable]
    public class SystemSetEntity
    {
        [Description("系统ID")]
        public int SystemID { get; set; }
        [Description("系统名称")]
        public string SystemName { get; set; }
        [Description("系统代码")]
        public string SystemCode { get; set; }
        [Description("系统负责人")]
        public string SystemPeople { get; set; }
        [Description("负责人电话")]
        public string Cellphone { get; set; }
        [Description("作废标识")]
        public string DelFlag { get; set; }
        [Description("账单前缀")]
        public string AccountTag { get; set; }
        [Description("缓存前缀")]
        public string CacheTag { get; set; }
        [Description("开始日期")]
        public DateTime StartDate { get; set; }
        [Description("结束日期")]
        public DateTime EndDate { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
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
