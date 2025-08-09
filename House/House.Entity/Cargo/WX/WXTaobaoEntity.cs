using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 微信商城与淘宝订单数据实体1.3.11.Tbl_Taobao_Order（微信商城与淘宝订单关联数据表）
    /// </summary>
    [Serializable]
    public class WXTaobaoEntity
    {
        [Description("表主键")]
        public Int64 ID { get; set; }
        [Description("微信用户表主键")]
        public Int64 WXID { get; set; }
        [Description("淘宝订单号")]
        public string TaobaoID { get; set; }
        [Description("买家昵称")]
        public string buyer_nick { get; set; }
        [Description("商品图片路径")]
        public string pic_path { get; set; }
        [Description("实付金额")]
        public decimal payment { get; set; }
        [Description("商品单价")]
        public decimal price { get; set; }
        [Description("订单总金额")]
        public decimal total_fee { get; set; }
        [Description("买家是否评价")]
        public string buyer_rate { get; set; }
        [Description("收货人姓名")]
        public string receiver_name { get; set; }
        [Description("收货人省份")]
        public string receiver_state { get; set; }
        [Description("收货人城市")]
        public string receiver_city { get; set; }
        [Description("收货人地区")]
        public string receiver_district { get; set; }
        [Description("收货人地址")]
        public string receiver_address { get; set; }
        [Description("收货人手机号码")]
        public string receiver_mobile { get; set; }
        [Description("收货人电话")]
        public string receiver_phone { get; set; }
        [Description("购买数量")]
        public int num { get; set; }
        [Description("订单状态")]
        public string status { get; set; }
        [Description("商品标题")]
        public string Title { get; set; }
        [Description("订单创建时间")]
        public DateTime created { get; set; }
        [Description("付款时间")]
        public DateTime pay_time { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        [Description("物流公司")]
        public string logicCompany { get; set; }
        [Description("物流单号")]
        public string logicno { get; set; }
        [Description("买家支付宝账号")]
        public string buyer_alipay { get; set; }
        [Description("淘宝店铺")]
        public string Shop { get; set; }
        public string wxName { get; set; }
        public string wxOpenID { get; set; }
        public string AvatarBig { get; set; }
        public int TodayOrderNum { get; set; }
        public decimal TodayFanLi { get; set; }
        public decimal TodayTiCheng { get; set; }
        public Int64 ParentID { get; set; }
        /// <summary>
        /// 我 的返利 2%
        /// </summary>
        public decimal Rebate { get; set; }
        /// <summary>
        /// 上级用户
        /// </summary>
        public string OneWxName { get; set; }
        public long OneWxID { get; set; }
        /// <summary>
        /// 上级提成 5%
        /// </summary>
        public decimal OneMoney { get; set; }
        /// <summary>
        /// 上上级用户
        /// </summary>
        public string SecendWxName { get; set; }
        public long SecendWxID { get; set; }
        /// <summary>
        /// 上上级提成 3%
        /// </summary>
        public decimal SecendMoney { get; set; }
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
    /// 淘宝返利数据表1.3.12.Tbl_Taobao_CashBack（淘宝订单返现和提成记录）
    /// </summary>
    [Serializable]
    public class WXTaobaoCashBackEntity
    {
        public long ID { get; set; }
        public long WXID { get; set; }
        public string TaobaoID { get; set; }
        public decimal payment { get; set; }
        public string CashType { get; set; }
        public decimal Cash { get; set; }
        public DateTime OP_DATE { get; set; }
    }
}
