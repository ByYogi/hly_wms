using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 财务报销数据实体1.6.7.Tbl_Cargo_Expense（报销申请表）
    /// </summary>
    [Serializable]
    public class CargoExpenseEntity
    {
        [Description("报销单号")]
        public Int64 ExID { get; set; }
        [Description("报销人")]
        public string ExName { get; set; }
        [Description("报销部门")]
        public string ExDepart { get; set; }
        [Description("报销时间")]
        public DateTime ExDate { get; set; }
        [Description("受款人")]
        public string ReceiveName { get; set; }
        [Description("开户行")]
        public string CardBank { get; set; }
        [Description("受款账号")]
        public string ReceiveNumber { get; set; }
        [Description("付款方式")]
        public string ChargeType { get; set; }
        [Description("金额")]
        public decimal ExCharge { get; set; }
        [Description("备注")]
        public string Reason { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("报销单提交人")]
        public string OperaName { get; set; }
        [Description("报销单提交人ID")]
        public string OperaID { get; set; }
        [Description("报销单提交日期")]
        public DateTime ExpenseDate { get; set; }
        public int CostType { get; set; }
        public string CostName { get; set; }
        /// <summary>
        /// 二级会计科目ID数组
        /// </summary>
        public string CostSID { get; set; }
        public List<CargoExpenseDetailEntity> exDetail { get; set; }
        [Description("报销状态")]
        public string Status { get; set; }
        [Description("拒审原因")]
        public string DenyReason { get; set; }
        [Description("结算状态")]
        public string CheckStatus { get; set; }
        [Description("审批流程")]
        public int ApproveID { get; set; }
        [Description("审批流程名称")]
        public string ApproveName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ExpenseID { get; set; }
        /// <summary>
        /// 当前审批人ID
        /// </summary>
        [Description("当前审批人ID")]
        public string UserID { get; set; }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        [Description("当前审批人姓名")]
        public string UserName { get; set; }
        public DateTime CheckTime { get; set; }
        [Description("下一审批人ID")]
        public string NextCheckID { get; set; }
        [Description("下一审批人姓名")]
        public string NextCheckName { get; set; }
        /// <summary>
        /// 上一审批人
        /// </summary>
        public string LastUserName { get; set; }
        /// <summary>
        /// 已收
        /// </summary>
        public decimal ReceivedMoney { get; set; }
        /// <summary>
        /// 未收
        /// </summary>
        public decimal UncollectMoney { get; set; }
        public string RType { get; set; }
        public string FromTO { get; set; }
        public string Memo { get; set; }
        public string Summary { get; set; }
        public decimal DetailCharge { get; set; }
        public int ZID { get; set; }
        public int FID { get; set; }
        public int SID { get; set; }
        public string ZName { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string CheckID { get; set; }
        /// <summary>
        /// 主管审批名字
        /// </summary>
        public string SupervisorName { get; set; }
        /// <summary>
        /// 财务审批名字
        /// </summary>
        public string FinanceName { get; set; }
        /// <summary>
        /// 领导审批名字 
        /// </summary>
        public string LeaderName { get; set; }
        public string CargoPermisID { get; set; }
        public int HouseID { get; set; }
        public string HouseName { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int ClientID { get; set; }
        /// <summary>
        /// 客户姓名 
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 报销类型
        /// </summary>
        [Description("报销类型")]
        public string ExType { get; set; }
        public DateTime HappenDate { get; set; }

        public string OneCheckID { get; set; }
        public string OneCheckName { get; set; }
        public string TwoCheckID { get; set; }
        public string TwoCheckName { get; set; }
        public string ThreeCheckID { get; set; }
        public string ThreeCheckName { get; set; }
        public string FourCheckID { get; set; }
        public string FourCheckName { get; set; }
        public string FiveCheckID { get; set; }
        public string FiveCheckName { get; set; }
        public string SixCheckID { get; set; }
        public string SixCheckName { get; set; }
        public string SevenCheckID { get; set; }
        public string SevenCheckName { get; set; }
        public string EightCheckID { get; set; }
        public string EightCheckName { get; set; }
        public string NineCheckID { get; set; }
        public string NineCheckName { get; set; }
        public string TenCheckID { get; set; }
        public string TenCheckName { get; set; }
        public string ApproveType { get; set; }
        public string CopyCheck { get; set; }
        public string CopyCheckName { get; set; }
        public string CopyUserID { get; set; }
        public string CopyUserName { get; set; }
        public string CheckType { get; set; }
        public string ReadStatus { get; set; }
        public string Result { get; set; }
        public string Opera { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string ChargeUnit { get; set; }
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
    /// 报销详细清单列表实体
    /// </summary>
    [Serializable]
    public class CargoExpenseDetailEntity
    {
        public Int64 DetailID { get; set; }
        public Int64 ExID { get; set; }
        public string Memo { get; set; }
        public string Summary { get; set; }
        public DateTime HappenDate { get; set; }
        public decimal DetailCharge { get; set; }
        public int ZID { get; set; }
        public int FID { get; set; }
        public int SID { get; set; }
        public string ZName { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
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
    }
    /// <summary>
    /// 报销单号，详细单号和相关运单单据的关联表
    /// </summary>
    [Serializable]
    public class CargoExpenseOrderIDEntity
    {
        public Int64 DetailID { get; set; }
        public Int64 ExID { get; set; }
        public string OrderNo { get; set; }
    }

    /// <summary>
    /// 报销审批路线图实体
    /// </summary>
    [Serializable]
    public class CargoExpenseApproveRoutEntity
    {
        public Int64 ExID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Opera { get; set; }
        public DateTime OperaDate { get; set; }
        public string Result { get; set; }
        public string ApproveType { get; set; }
        public string OrderCheckType { get; set; }
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
    /// 报销科目数据实体
    /// </summary>
    public class CargoZeroSubjectEntity
    {
        public int ZID { get; set; }
        public string ZName { get; set; }
    }
    /// <summary>
    /// 报销一级科目数据实体
    /// </summary>
    public class CargoFirstSubjectEntity
    {
        public int FID { get; set; }
        public int ZID { get; set; }
        public string FName { get; set; }
    }

    /// <summary>
    /// 二级科目实体
    /// </summary>
    public class CargoSecondSubjectEntity
    {
        public int FID { get; set; }
        public int SID { get; set; }
        public string SName { get; set; }
    }
    /// <summary>
    /// 会计科目实体
    /// </summary>
    public class FinanceSubjectEntity
    {
        public int RowNumber { get; set; }
        public int OldZID { get; set; }
        public int ZID { get; set; }
        public string ZName { get; set; }
        public int OldFID { get; set; }
        public int FID { get; set; }
        public string OldFName { get; set; }
        public string FName { get; set; }
        public int OldSID { get; set; }
        public int SID { get; set; }
        public string OldSName { get; set; }
        public string SName { get; set; }
        public int UseCount { get; set; }
    }
    /// <summary>
    /// 其它业务实体
    /// </summary>
    public class FinanceOtherEntity
    {
        [Description("ID")]
        public int ID { get; set; }
        public string IDs { get; set; }
        [Description("所属仓库")]
        public int HouseID { get; set; }
        public string CargoPermisID { get; set; }
        [Description("项目类型")]
        public string ProjectType { get; set; }
        [Description("项目类别")]
        public string ProjectCategory { get; set; }
        [Description("发生时间")]
        public DateTime HappenDate { get; set; }
        [Description("客户编码")]
        public int ClientNum { get; set; }
        [Description("客户名称")]
        public string AcceptUnit { get; set; }
        [Description("涉及金额")]
        public decimal AmountInvolved { get; set; }
        [Description("结算状态")]
        public string CheckStatus { get; set; }
        [Description("操作人")]
        public string OP_ID { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ReceivedMoney { get; set; }
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
