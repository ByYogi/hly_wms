using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// Tbl_Cargo_InvoiceHeader（客户发票抬头数据表）
    /// </summary>
    [Serializable]
    public class CargoClientInvoiceHeaderEntity
    {
        public long ID { get; set; }
        public int ClientNum { get; set; }
        public string HeaderInfo { get; set; }
        public string HeaderType { get; set; }
        public string InvoiceType { get; set; }
        public string SocialCode { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string RegisAddress { get; set; }
        public string RegisTelephone { get; set; }
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
}
