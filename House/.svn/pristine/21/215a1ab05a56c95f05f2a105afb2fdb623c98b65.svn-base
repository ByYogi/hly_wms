using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 定时推送数据实体1.11.1.Tbl_Cargo_PushData（定时推送微信订单通知）
    /// </summary>
    [Serializable]
    public class CargoPushDataEntity
    {
        public long ID { get; set; }
        [Description("推送标题")]
        public string PushTitle { get; set; }
        [Description("订单内容")]
        public string OrderInfo { get; set; }
        [Description("总金额")]
        public decimal TotalCharge { get; set; }
        [Description("商城订单号")]
        public string WXOrderNo { get; set; }
        [Description("微信OPENID")]
        public string wxOpenID { get; set; }
        [Description("模板ID")]
        public string TemplateID { get; set; }
        [Description("所属仓库")]
        public string HouseName { get; set; }
        [Description("仓库ID")]
        public int HouseID { get; set; }
        [Description("推送类型")]
        public string PushType { get; set; }
        [Description("推送状态")]
        public string PushStatus { get; set; }
        [Description("推送时间")]
        public DateTime PushDate { get; set; }
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
