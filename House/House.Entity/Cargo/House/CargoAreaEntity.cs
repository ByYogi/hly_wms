using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.Xml.Serialization;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 仓储管理系统区域管理数据实体Tbl_Cargo_Area
    /// </summary>
    [Serializable]
    public class CargoAreaEntity
    {
        [Description("表主键")]
        public int AreaID { get; set; }
        [Description("区域名称")]
        public string Name { get; set; }
        [Description("区域代码")]
        public string Code { get; set; }
        [Description("父区域ID")]
        public int ParentID { get; set; }
        /// <summary>
        /// 父区域名称
        /// </summary>
        public string ParentName { get; set; }
        [Description("所属仓库ID")]
        public int HouseID { get; set; }
        /// <summary>
        /// 所属仓库名称
        /// </summary>
        public string HouseName { get; set; }
        [Description("备注")]
        public string Remark { get; set; }
        [Description("操作时间")]
        public DateTime OP_DATE { get; set; }
        /// <summary>
        /// 查询所属仓库
        /// </summary>
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 配送范围
        /// </summary>
        public string DeliveryArea { get; set; }
        /// <summary>
        /// 区域类型 前置仓或区域仓库
        /// </summary>
        public string AreaType { get; set; }
        public string MainHouseName { get; set; }
        public int MainHouseID { get; set; }
        /// <summary>
        /// 仓库订单代码 
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 订单出发站
        /// </summary>
        public string OrderDep { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Address { get; set; }
        [Description("删除标志")]
        public string DelFlag { get; set; }
        public int ClientNum { get; set; }
        public int OrderLevel { get; set; }
        public int LogisID { get; set; }
        public string LogisticName { get; set; }
        public string VirtualInventory { get; set; }
        public string IsShowStock { get; set; }
        
        public string ContiHouseCode { get; set; }
        /// <summary>
        /// 记录仓库区域最新地址坐标信息
        /// 小程序需要
        /// </summary>
        public static Dictionary<string, ParametersDic> HouseAreaInfo = new Dictionary<string, ParametersDic>();
        public static Dictionary<string, string> AreaTokenUpdate = new Dictionary<string, string>();
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
    public class ParametersDic
    {
        public int FirstAreaID { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
    }
    public class ParametersReturnJson
    {
        public string status { get; set; }
        public string message { get; set; }
        public Result result { get; set; }
    }
    public class Result
    {
        //public Rows rows { get; set; }
        public List<Rows> rows;
    }
    public class Rows
    {
        public List<Elements> elements;
    }
    public class Elements
    {
        /// <summary>
        /// 起点到终点的距离，单位：米
        /// </summary>
        public int distance { get; set; }
        /// <summary>
        /// 从起点到终点的结合路况的时间
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 对起终点计算的状态码，取值：
        /// 4 代表附近没有路，距离计算失败，此时distance为直线距离，预估耗时（duraction）会返回 0。
        /// 计算结果正常返回时，不返回status
        /// </summary>
        public int status { get; set; }
    }
}
