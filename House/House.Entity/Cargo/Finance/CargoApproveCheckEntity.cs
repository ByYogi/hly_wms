using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CargoApproveCheckEntity
    {
        public long CheckID { get; set; }
        public long ApproveID { get; set; }
        public string CheckUserID { get; set; }
        public string CheckName { get; set; }
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 审批类型0：审批，1：阅读
        /// </summary>
        public string CheckType { get; set; }
        /// <summary>
        /// 阅读状态0：未读1：已读
        /// </summary>
        public string ReadStatus { get; set; }
        /// <summary>
        /// 审批类型
        /// </summary>
        public string ApproveType { get; set; }

        public void EnSafe()
        {
            PropertyInfo[] pSource = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo s in pSource)
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
