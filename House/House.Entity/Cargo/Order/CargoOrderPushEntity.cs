using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 订单推送数据记录表1.4.4.Tbl_Cargo_OrderPush（订单推送数据表）
    /// </summary>
    [Serializable]
    public class CargoOrderPushEntity
    {
        public Int64 ID { get; set; }
        public string OrderNo { get; set; }
        public string AwbNo { get; set; }
        public string Dep { get; set; }
        public string Dest { get; set; }
        public int Piece { get; set; }
        public decimal TransportFee { get; set; }
        public string ClientNum { get; set; }
        public string AcceptUnit { get; set; }
        public string AcceptAddress { get; set; }
        public string AcceptPeople { get; set; }
        public string AcceptTelephone { get; set; }
        public string AcceptCellphone { get; set; }
        public string HouseName { get; set; }
        public string HouseID { get; set; }
        public string OP_ID { get; set; }
        public string PushType { get; set; }
        public string PushStatus { get; set; }
        public string HLYSendUnit { get; set; }
        public string BusinessID { get; set; }
        /// <summary>
        /// 物流公司ID
        /// </summary>
        public int LogisID { get; set; }
        /// <summary>
        /// 数据类型，0=默认订单，1=好来运新系统订单
        /// </summary>
        public int Type { get; set; }
        public DateTime OP_DATE { get; set; }
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
