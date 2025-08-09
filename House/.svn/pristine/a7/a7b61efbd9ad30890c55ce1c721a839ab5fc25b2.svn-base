using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 马牌订单出库数据实体
    /// </summary>
    [Serializable]
    public class contiOutOrderEntity
    {
        public int code { get; set; }
        public string message { get; set; }
        public ContiOutDataList data { get; set; }
    }

    [Serializable]
    public class ContiOutDataList
    {
        public int total { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public int pages { get; set; }
        public List<saleOpenInfoResp> list { get; set; }
    }
    /// <summary>
    /// 出库状态同步
    /// </summary>
    [Serializable]
    public class OutOrderStatus
    {
        public string doNo { get; set; }
        public int deliveryType { get; set; }
        public string logisticsCompany { get; set; }
        public string logisticsNo { get; set; }
        public string contractName { get; set; }
        public string contractMobile { get; set; }
        public List<OutDetail> doDetails { get; set; }
    }

    [Serializable]
    public class OutDetail
    {

        public string skuCode { get; set; }
        public int outNum { get; set; }
        public List<BarCodeEntity> barCodeList { get; set; }
    }

    [Serializable]
    public class BarCodeEntity
    {
        public string barCode { get; set; }
        public string dot { get; set; }
        public string scanTime { get; set; }
    }

}
