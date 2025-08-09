using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 发送手机号码验证码数据实体1.5.4.Tbl_Cargo_MobileCode（手机发送验证码数据记录表）
    /// </summary>
    [Serializable]
    public class WXMobileCodeEntity
    {
        public int ID { get; set; }
        public string Cellphone { get; set; }
        public int SendNum { get; set; }
        public DateTime OP_DATE { get; set; }
    }
}
