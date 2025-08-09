using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 向快递100订阅或查询物流跟踪信息数据实体
    /// </summary>
    [Serializable]
    public class CargoKD100Entity
    {
        /// <summary>
        /// 中转承运单号
        /// </summary>
        public string TransitNo { get; set; }
        /// <summary>
        /// 承运公司快递100编码
        /// </summary>
        public string ComCode { get; set; }
        /// <summary>
        /// 物流状态快递单当前状态，包括0在途，1揽收，2疑难，3签收，4退签，5派件，6退回等8个状态
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 中转单号
        /// </summary>
        public long TransitID { get; set; }

        public string AwbNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        public string TruckFlag { get; set; }
        public string CurrentLocation { get; set; }
        public DateTime ArriveTime { get; set; }
        public string DetailInfo { get; set; }
        public string OP_ID { get; set; }
        public string Signer { get; set; }
        public long AwbID { get; set; }
        public string BelongSystem { get; set; }
        public List<CargoKD100AwbStatusEntity> awbStatusList { get; set; }
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

    [Serializable]
    public class CargoKD100AwbStatusEntity
    {
        public string context { get; set; }
        public DateTime time { get; set; }
        public DateTime ftime { get; set; }
    }
    [Serializable]
    public class ReturnMessage
    {
        public bool result { get; set; }
        public string returnCode { get; set; }
        public string message { get; set; }
    }
    /// <summary>
    /// 新陆程中转表数据实体
    /// </summary>
    [Serializable]
    public class NWTransitEntity
    {
        /// <summary>
        /// 承运公司
        /// </summary>
        public string CarrierName { get; set; }
        /// <summary>
        /// 承运公司快递100编码
        /// </summary>
        public string ComCode { get; set; }
        /// <summary>
        /// 中转承运单号
        /// </summary>
        public string TransitNo { get; set; }
        /// <summary>
        /// 中转单号
        /// </summary>
        public long TransitID { get; set; }
        /// <summary>
        /// 订阅状态0:未订阅1:已订阅
        /// </summary>
        public string PollStatus { get; set; }
        /// <summary>
        /// 所属系统
        /// </summary>
        public string BelongSystem { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        public long AwbID { get; set; }
        public string AwbNo { get; set; }
        public string AwbStatus { get; set; }
        public string OPID { get; set; }
        public DateTime SignDate { get; set; }
    }
}
