using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace House.Entity.Cargo
{
    /// <summary>
    /// 广丰订单导入系统数据实体Tbl_GTMC_ProOrder
    /// </summary>
    [Serializable]
    public class CargoGtmcProOrderEntity
    {
        [Description("表主键")]
        public Int64 TID { get; set; }
        /// <summary>
        /// 用于修改订单状态
        /// </summary>
        public string ID { get; set; }
        [Description("纳入番号")]
        public string TakeNo { get; set; }
        /// <summary>
        /// 厂家发车时间
        /// </summary>
        [Description("厂家发车时间")]
        public DateTime DepartTime { get; set; }
        [Description("纳入时间")]
        public DateTime TakeTime { get; set; }
        [Description("广丰订单号")]
        public string GtmcNo { get; set; }
        [Description("供应商代码")]
        public string SourceCode { get; set; }
        [Description("供应商名称")]
        public string SourceName { get; set; }
        [Description("订单状态")]
        public string OrderStatus { get; set; }
        [Description("操作时间")]
        public DateTime OPDATE { get; set; }
        [Description("智能系统订单号")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 开单时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 出库单状态
        /// </summary>
        public string AwbStatus { get; set; }
        [Description("所属系统ID")]
        public int HouseID { get; set; }
        [Description("所属系统")]
        public string HouseName { get; set; }

        [Description("订单数量")]
        public int Piece { get; set; }

        [Description("操作人员ID")]
        public string OPID { get; set; }
        [Description("操作人员")]
        public string OPName { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        [Description("订单类型")]
        public string OrderType { get; set; }
        /// <summary>
        /// 广丰工厂(一厂，二厂...)
        /// </summary>
        public string FactoryNo { get; set; }
        /// <summary>
        /// 路线名
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// 便次
        /// </summary>
        public string FrequentNo { get; set; }
        /// <summary>
        /// 受入号
        /// </summary>
        public string IncomeNo { get; set; }
        /// <summary>
        /// 主路线
        /// </summary>
        public string MainRoute { get; set; }
        /// <summary>
        /// 分割链
        /// </summary>
        public string SplitLine { get; set; }
        /// <summary>
        /// 订单文件名称
        /// </summary>
        public string OrderFileName { get; set; }
        /// <summary>
        /// 发车时间年月日
        /// </summary>
        public string DepartDate { get; set; }
        /// <summary>
        /// 发车时间时分秒
        /// </summary>
        public string DepartHour { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Info { get; set; }
        public string GoodsCode { get; set; }
        /// <summary>
        /// 在库数量
        /// </summary>
        public int InPiece { get; set; }
        /// <summary>
        /// 库存状态
        /// </summary>
        public int StockType { get; set; }
        public int LastMonth { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 载重指数
        /// </summary>
        public string LoadIndex { get; set; }
        /// <summary>
        /// 速度级别
        /// </summary>
        public string SpeedLevel { get; set; }
        /// <summary>
        /// 轮胎花纹
        /// </summary>
        public string Figure { get; set; }
        public string Model { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string TypeName { get; set; }
        public int TypeID { get; set; }
        public int TypeParentID { get; set; }
        public string CargoPermisID { get; set; }
        /// <summary>
        /// 线路ID
        /// </summary>
        public int LineID { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string LineName { get; set; }
        public string ProductCodeStr { get; set; }
        public string ProductCode { get; set; }
        public string Memo { get; set; }
        public string BusinessID { get; set; }
        public string BusinessName { get; set; }
        /// <summary>
        /// 订单分配类型  
        /// </summary>
        public string OrderAlloType { get; set; }
        /// <summary>
        /// 清单编号
        /// </summary>
        public int ExterOrderAlloNum { get; set; }
        /// <summary>
        /// 一级出库仓库区域大仓
        /// </summary>
        public int FirstLevelHouseID { get; set; }
        /// <summary>
        /// 一级出库仓库ID
        /// </summary>
        public int FirstLevelAreaID { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        //public string Product { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 定时程序自动处理开单状态 0:未处理 1:已处理
        /// </summary>
        public string AutoOrderHandleStatus { get; set; }

        /// <summary>
        /// 订单处理类型 0:人工处理 1:电脑自动处理
        /// </summary>
        public string OrderHandleType { get; set; }
        /// <summary>
        /// 定时程序自动处理时间
        /// </summary>
        public DateTime AutoOrderHandleTime { get; set; }

        public List<CargoGtmcProOrderDetailEntity> orderDetail { get; set; }
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
    /// 广丰订单导入系统数据明细数据实体Tbl_GTMC_ProOrderDetail
    /// </summary>
    [Serializable]
    public class CargoGtmcProOrderDetailEntity
    {
        public long DeID { get; set; }
        public long TID { get; set; }
        public string GoodsCode { get; set; }
        public int TypeID { get; set; }
        public int TypeParentID { get; set; }
        public string TypeParentName { get; set; }

        public string TypeName { get; set; }
        public string GoodsName { get; set; }

        public string Specs { get; set; }
        public string Cage { get; set; }
        public int PackNum { get; set; }
        public int TotalNum { get; set; }
        public int Piece { get; set; }
        public string status { get; set; }
        public DateTime OPDATE { get; set; }
        /// <summary>
        /// 库存情况
        /// </summary>
        public string StockInfo { get; set; }
        /// <summary>
        /// 可以开单情况
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 不可以开单库存
        /// </summary>
        public int NoStock { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string GtmcNo { get; set; }
        /// <summary>
        /// 店代码
        /// </summary>
        public string SourceCode { get; set; }
        public string SourceName { get; set; }
        public string Memo { get; set; }
        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public string HouseID { get; set; }
        public string HouseName { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public string AreaID { get; set; }
        /// <summary>
        /// 是否在线网关系内  0.否  1.是
        /// </summary>
        public int IsNetwork { get; set; }
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
