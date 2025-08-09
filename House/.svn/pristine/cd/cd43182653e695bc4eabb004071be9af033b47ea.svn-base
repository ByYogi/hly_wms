using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    public class OrderDetailEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OrderDetailInfo data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
    public class OrderDetailInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public List<OrderDetailGoods> goods { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OrderDetailLogistics logistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderDetailLogisticsTraces> logisticsTraces { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderDetailLogs> logs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OrderDetailOrderInfo orderInfo { get; set; }
    }
    public class OrderDetailLogisticsTraces
    {
        /// <summary>
        /// 
        /// </summary>
        public string AcceptStation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AcceptTime { get; set; }
    }
    public class OrderDetailGoods
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long goodsId { get; set; }
        /// <summary>
        /// 兔毛马甲
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long orderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int persion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pic { get; set; }
    }

    public class OrderDetailLogistics
    {
        /// <summary>
        /// 广东省广州市白云区广园中路238号广州市白云区人民政府
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 海珠区
        /// </summary>
        public string areaStr { get; set; }
        /// <summary>
        /// 广州市
        /// </summary>
        public string cityStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipperName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trackingNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 张三
        /// </summary>
        public string linkMan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 广东省
        /// </summary>
        public string provinceStr { get; set; }
    }

    public class OrderDetailLogs
    {
        /// <summary>
        /// 下单
        /// </summary>
        public string typeStr { get; set; }
    }

    public class OrderDetailOrderInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal amountLogistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal amountReal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goodsNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 订单关闭
        /// </summary>
        public string statusStr { get; set; }
    }
}
