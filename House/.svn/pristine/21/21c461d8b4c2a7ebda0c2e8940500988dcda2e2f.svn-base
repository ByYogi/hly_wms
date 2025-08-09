using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    public class AppletRegisterEntity
    {
    }
    public class AppletResultData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public WeiChartData data { get; set; }
    }
    public class WeiChartData
    {
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string openid { get; set; }
        public string token { get; set; }
        public int uid { get; set; }

        public string OperaBrand { get; set; }
        public decimal LogisFee { get; set; }
        public decimal TwoLogisFee { get; set; }
        public decimal ThreeLogisFee { get; set; }
        public decimal NextDayLogisFee { get; set; }

        public decimal Discount { get; set; }

        public string CompanyName { get; set; }
        /// <summary>
        /// 可用额度金额 
        /// </summary>
        public decimal LimitMoney { get; set; }
        /// <summary>
        /// 额度付款权限0：冻结1：开通
        /// </summary>
        public string QuotaLimit { get; set; }
        /// <summary>
        /// 货到付款权限 0：打开1：关闭
        /// </summary>
        public string ArrivePayLimit { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 仓库客服电话号码
        /// </summary>
        public string HouseCellphone { get; set; }
        /// <summary>
        /// 营业结束时间
        /// </summary>
        public string EndBusHours { get; set; }
        /// <summary>
        /// 是否允许急送0:允许1:否
        /// </summary>
        public string IsCanRush { get; set; }
        /// <summary>
        /// 是否允许自提0:允许1:否
        /// </summary>
        public string IsCanPickUp { get; set; }
        /// <summary>
        /// 是否允许次日达0:允许1:否
        /// </summary>
        public string IsCanNextDay { get; set; }
        /// <summary>
        /// 多仓库下单多地址下单权限，0：无，1：可以
        /// </summary>
        public int MulAddressOrder { get; set; }
        /// <summary>
        /// 支付方式，0：全部，1：只允许在线支付。
        /// </summary>
        public int PaymentMethod { get; set; }
        /// <summary>
        /// 小程序是否显示急速达版面0：不显示1：显示
        /// </summary>
        public string IsShowExpedit { get; set; }
        /// <summary>
        /// 小程序是否显示次日达版面0：不显示1：显示
        /// </summary>
        public string IsShowNextDay { get; set; }
    }
    /// <summary>
    /// 微信小程序登录信息结构
    /// </summary>
    public class wechatlogininfo
    {
        public string code { get; set; }
        public string encrypteddata { get; set; }
        public string iv { get; set; }
        public string rawdata { get; set; }
        public string signature { get; set; }
    }
    /// <summary>
    /// 微信小程序用户信息结构
    /// </summary>
    public class wechatuserinfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarurl { get; set; }
        public string unionid { get; set; }
        public string session_key { get; set; }
        public string phone { get; set; }
        public watermark watermark { get; set; }

    }
    public class watermark
    {
        public string appid { get; set; }
        public string timestamp { get; set; }
    }
    /// <summary>
    /// 微信小程序从服务端获取的openid和sessionkey信息结构
    /// </summary>
    public class openidandsessionkey
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
