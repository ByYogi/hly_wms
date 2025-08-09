using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo.WxApplet
{
    public class WeChatPayEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public WeChatPayInfo data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
    public class WeChatPayInfo
    {
        /// <summary>
        /// 10位时间戳 
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 订单详情扩展字符串
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 签名方式
        /// </summary>
        public string signType { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string paySign { get; set; }

    }
}
