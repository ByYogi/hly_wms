using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    public class OrderListEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OrderListInfo data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
    public class OrderListInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, List<OrderGoodsInfo>> goodsMap { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderInfo> orderList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalRow { get; set; }
    }
    public class OrderInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal amountReal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dateAdd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goodsNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool hasRefund { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 待支付
        /// </summary>
        public string statusStr { get; set; }
    }
    public class OrderGoodsInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int goodsId { get; set; }
        /// <summary>
        /// 兔毛马甲
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int orderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pic { get; set; }
    }
}
