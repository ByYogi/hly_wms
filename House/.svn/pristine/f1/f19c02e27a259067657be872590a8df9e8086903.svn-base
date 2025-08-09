using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 审批流程设置数据实体1.6.10.Tbl_Cargo_ExpenseApproveSet（报销流程审批设置表）
    /// </summary>
    [Serializable]
    public class CargoApproveSetEntity
    {
        [Description("主键")]
        public int ID { get; set; }
        [Description("审批流程名称")]
        public string ApproveName { get; set; }
        [Description("一级审批ID")]
        public string OneCheckID { get; set; }
        [Description("一级审批")]
        public string OneCheckName { get; set; }
        [Description("二级审批ID")]
        public string TwoCheckID { get; set; }
        [Description("二级审批")]
        public string TwoCheckName { get; set; }
        [Description("三级审批ID")]
        public string ThreeCheckID { get; set; }
        [Description("三级审批")]
        public string ThreeCheckName { get; set; }
        [Description("四级审批ID")]
        public string FourCheckID { get; set; }
        [Description("四级审批")]
        public string FourCheckName { get; set; }
        [Description("五级审批ID")]
        public string FiveCheckID { get; set; }
        [Description("五级审批")]
        public string FiveCheckName { get; set; }
        [Description("六级审批ID")]
        public string SixCheckID { get; set; }
        [Description("六级审批")]
        public string SixCheckName { get; set; }
        public string SevenCheckID { get; set; }
        public string SevenCheckName { get; set; }
        public string EightCheckID { get; set; }
        public string EightCheckName { get; set; }
        public string NineCheckID { get; set; }
        public string NineCheckName { get; set; }
        public string TenCheckID { get; set; }
        public string TenCheckName { get; set; }
        [Description("审批类型")]
        public string ApproveType { get; set; }
        [Description("删除标识")]
        public string DelFlag { get; set; }
        [Description("抄送角色ID")]
        public string CopyCheck { get; set; }
        [Description("抄送角色")]
        public string CopyCheckName { get; set; }
        [Description("抄送具体人")]
        public string CopyUserID { get; set; }
        [Description("抄送具体人姓名")]
        public string CopyUserName { get; set; }
        [Description("自动审批角色ID")]
        public string AutoPassID { get; set; }
        [Description("自动审批角色内容")]
        public string AutoPassName { get; set; }


        public string CargoPermisID { get; set; }
        public int HouseID { get; set; }
        public string HouseName { get; set; }
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
