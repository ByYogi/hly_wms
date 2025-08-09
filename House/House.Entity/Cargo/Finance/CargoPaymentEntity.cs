using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 付款申请书数据实体1.6.12.Tbl_Cargo_Payment（付款申请书数据表）
    /// </summary>
    [Serializable]
    public class CargoPaymentEntity
    {
        public long ID { get; set; }
        public string ApplyDep { get; set; }
        public string ApplyPeople { get; set; }
        public string ReceiveUnit { get; set; }
        public string CardName { get; set; }
        public string CardBank { get; set; }
        public string CardNum { get; set; }
        public decimal PayMoney { get; set; }
        public string PayWay { get; set; }
        public string PayMemo { get; set; }
        public string RelateAwb { get; set; }
        public DateTime OP_DATE { get; set; }
        public int HouseID { get; set; }
        public int PrintNum { get; set; }
        public string HouseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PrintName { get; set; }
        public List<CargoPayRelateAwbEntity> PayRelateList { get; set; }
        public List<CargoPayPrintInfoEntity> PayPrintList { get; set; }

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
    /// 付款申请与单号关联数据实体1.6.13.Tbl_Cargo_PayRelateAwb(付款申请书与单号关联数据表)
    /// </summary>
    [Serializable]
    public class CargoPayRelateAwbEntity
    {
        public long ID { get; set; }
        public long PayID { get; set; }
        public string RelateAwb { get; set; }
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
    /// 付款申请书打印记录数据实体1.6.15.Tbl_Cargo_PayPrintInfo（付款申请书打印记录表）
    /// </summary>
    [Serializable]
    public class CargoPayPrintInfoEntity
    {
        public long ID { get; set; }
        public long PayID { get; set; }
        public string PrintName { get; set; }
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
