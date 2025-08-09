using Cargo.QY;
using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.House;
using House.Entity.Cargo.Order;
using House.Entity.Cargo.Product;
using House.Entity.House;
using iText.Layout.Element;
using Memcached.ClientLibrary;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Formula.Functions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.HuiCai
{
    public partial class huiCaiApi : BasePage
    {
        /// <summary>
        /// 方法的入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string methodName = string.Empty;
            try
            {
                methodName = Request["method"];
                if (String.IsNullOrEmpty(methodName)) return;
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                method.Invoke(this, null);
            }
            catch (Exception ex)
            {
                LogBus bus = new LogBus();
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Operate = "";
                log.Moudle = "";
                log.Status = "1";
                log.NvgPage = "";
                log.UserID = UserInfor.LoginName.Trim();
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }

        #region 云仓数据
        public void QueryHCYC_House()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_House(queryEntity);
            List<CargoHouseAimEntity> footlist = new List<CargoHouseAimEntity>();
            footlist.Add(new CargoHouseAimEntity
            {
                AimMoney = 0,
                PercentComplete = 0,
                HouseName = "汇总：",
                OPID = list.Count.ToString(),
                AimQuantity = list.Sum(c => c.AimQuantity),
                Piece = list.Sum(c => c.Piece)
            });
            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = footlist;
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);

        }
        public void HCYCAccessStatisticsData()
        {
            CargoDLTDataStatisEntity queryEntity = new CargoDLTDataStatisEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Request["HouseID"]))//一级分类
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]);//所属仓库ID
            }
            else
            {
                queryEntity.HouseID = UserInfor.HouseID;//用户所属仓库权限ID
            }
            CargoReportBus bus = new CargoReportBus();

            Dictionary<string, object> list = bus.HCYCAccessStatisData(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        public void HCYCStatisticsData()
        {
            CargoDLTDataStatisEntity queryEntity = new CargoDLTDataStatisEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            CargoReportBus bus = new CargoReportBus();

            Dictionary<string, object> list = bus.HCYCStatisticsData(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        public void QueryHCYC_TotalDays()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_TotalDays(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        public void QueryHCYC_TotalMonths()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_TotalMonths(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        public void QueryHCYC_OrderDayStatistics()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_OrderDayStatistics(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        public void QueryHCYC_BrandData()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_BrandData(queryEntity);
            List<CargoOrderDayStatisticsEntity> footlist = new List<CargoOrderDayStatisticsEntity>();

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                HouseName = "汇总：",
                ProPiece = list.Sum(c => c.ProPiece),
                Piece = list.Sum(c => c.Piece),
                DayStatisticsCount = list.Sum(c => c.DayStatisticsCount),
                StockNum = list.Sum(c => c.StockNum),
                ProCount = list.Sum(c => c.ProCount),
                InventoryTurnover = list.Sum(c => c.InventoryTurnover),
    
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);

        }
        public void DayStatisticsExport()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            CargoReportBus bus = new CargoReportBus();

            var dataGoods = bus.QueryHCYC_BrandData(queryEntity);

            string err = "OK";
            var awbList = dataGoods;

            if (awbList.Count > 0) { OrderDayStatisticsDataExport = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public void DayStatisticsExportV2()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_House(queryEntity);

            string err = "OK";
            var awbList = list;

            if (awbList.Count > 0) { OrderDayStatisticsDataExportV2 = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public void DayStatisticsExportV3()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            CargoReportBus bus = new CargoReportBus();

            var dataGoods = bus.QueryHCYC_BrandData(queryEntity);

            string err = "OK";
            var awbList = dataGoods;

            if (awbList.Count > 0) { OrderDayStatisticsDataExport = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public List<CargoOrderDayStatisticsEntity> OrderDayStatisticsDataExport
        {
            get
            {
                if (Session["OrderDayStatisticsDataExport"] == null)
                {
                    Session["OrderDayStatisticsDataExport"] = new List<CargoOrderDayStatisticsEntity>();
                }
                return (List<CargoOrderDayStatisticsEntity>)(Session["OrderDayStatisticsDataExport"]);
            }
            set
            {
                Session["OrderDayStatisticsDataExport"] = value;
            }
        }
        public List<CargoHouseAimEntity> OrderDayStatisticsDataExportV2
        {
            get
            {
                if (Session["OrderDayStatisticsDataExportV2"] == null)
                {
                    Session["OrderDayStatisticsDataExportV2"] = new List<CargoHouseAimEntity>();
                }
                return (List<CargoHouseAimEntity>)(Session["OrderDayStatisticsDataExportV2"]);
            }
            set
            {
                Session["OrderDayStatisticsDataExportV2"] = value;
            }
        }

        public void GetSalesRateDetail()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.GetSalesRateDetail(queryEntity);

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                Specs = "汇总：",
                ProPiece = list.Sum(c => c.ProPiece),
                Piece = list.Sum(c => c.Piece)
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }

        public void GetClickConversionRateDetail()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.GetClickConversionRateDetail(queryEntity);

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                Specs = "汇总：",
                clickCount = list.Sum(c => c.clickCount),
                UniqueClientCount = list.Sum(c => c.UniqueClientCount),
                UniqueClientNum = list.Sum(c => c.UniqueClientNum),
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 获取云仓数据
        /// </summary>
        public void CargoPermisionHouse()
        {
            CargoReportBus bus = new CargoReportBus();
            List<CargoHouseEntity> list = new List<CargoHouseEntity>();
            var data = bus.CargoPermisionHouse();
            foreach (var item in data)
            {
                list.Add(new CargoHouseEntity { HouseID = item.ID, Name = item.Name });
            }
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }

        #endregion
    }
}