using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 申请审批报销数据实体1.9.1.Tbl_Cargo_Approve（申请审批数据表）
    /// </summary>
    [Serializable]
    public class CargoApproveEntity
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string ApplyID { get; set; }
        public string ApplyName { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Memo { get; set; }
        public int AppSetID { get; set; }
        public string CurrID { get; set; }
        public string CurrName { get; set; }
        public DateTime CheckTime { get; set; }
        public string NextCheckID { get; set; }
        public string NextCheckName { get; set; }
        public string ApplyStatus { get; set; }
        public string DenyReason { get; set; }
        public DateTime OP_DATE { get; set; }
        public string ApplyType { get; set; }
        public int HouseID { get; set; }
        public int ThrowHouse { get; set; }
        public string ThrowHouseName { get; set; }
        public long ClientID { get; set; }
        public string ApproveName { get; set; }
        public string CheckType { get; set; }
        public string ClientName { get; set; }
        public string CheckUserID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OStartTime { get; set; }
        public DateTime OEndTime { get; set; }
        public decimal OTime { get; set; } 
        public decimal LimitMoney { get; set; }
        /// <summary>
        /// 申请关联数据列表
        /// </summary>
        public List<CargoApproveRelateEntity> RelateList { get; set; }
        /// <summary>
        /// 我的申请文件数据列表
        /// </summary>
        public List<CargoApproveFileEntity> FileList { get; set; }
        /// <summary>
        /// 审批路线图数据
        /// </summary>
        public List<CargoExpenseApproveRoutEntity> RouteList { get; set; }
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
