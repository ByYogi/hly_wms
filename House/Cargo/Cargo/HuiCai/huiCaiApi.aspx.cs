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
            var totalPiece = Convert.ToDouble(list.Sum(a => a.Piece));
            var totalPrevMonthPiece = Convert.ToDouble(list.Sum(a => a.PrevPiece));
            footlist.Add(new CargoHouseAimEntity
            {
                AimMoney = 0,
                PercentComplete = 0,
                HouseName = "汇总：",
                OPID = list.Count.ToString(),
                AimQuantity = list.Sum(c => c.AimQuantity),
                Piece = list.Sum(c => c.Piece),
                PrevPiece = list.Sum(c => c.PrevPiece),
                PreviousMonthRate = totalPrevMonthPiece > 0 ? Math.Round((totalPiece - totalPrevMonthPiece) / totalPrevMonthPiece * 100, 1) : 0,
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
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToString(Request["OrderType"]); }
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
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.ThrowGood = Convert.ToString(Request["OrderType"]); }
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
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_BrandData(queryEntity);
            List<CargoOrderDayStatisticsEntity> footlist = new List<CargoOrderDayStatisticsEntity>();

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            // Math.Round((Convert.ToDouble(item.Piece)- Convert.ToDouble(item.PrevMonthPiece)) / Convert.ToDouble(item.PrevMonthPiece) * 100, 1);
            var totalUniqueClientCount = Convert.ToDouble(list.Sum(c => c.UniqueClientCount));
            var totalclickCount = Convert.ToDouble(list.Sum(c => c.clickCount));
            var totalProPiece = Convert.ToDouble(list.Sum(c => c.ProPiece));
            var totalPiece = Convert.ToDecimal(list.Sum(a => a.Piece));
            var totalOverduePiece = Convert.ToDecimal(list.Sum(a => a.OverduePiece));
            var totalPrevMonthPiece = Convert.ToDecimal(list.Sum(a => a.PrevMonthPiece));

            var totalDayStatisticsCount = Convert.ToDecimal(list.Sum(a => a.DayStatisticsCount));
            var totalProCountAll = Convert.ToDecimal(list.Sum(a => a.ProCountAll));
            var totalProCount = Convert.ToDecimal(list.Sum(a => a.ProCount));
            var totalGlobalProCountAll = Convert.ToDecimal(list.Sum(a => a.GlobalProCountAll));
            var totalGlobalProCount = Convert.ToDecimal(list.Sum(a => a.GlobalProCount));

            var days = (queryEntity.EndDate - queryEntity.StartDate).Days;
            resHT["footer"] = new[] {
            new
            {
                HouseName = "汇总：",
                ProPiece = list.Sum(c => c.ProPiece),
                Piece = list.Sum(c => c.Piece),
                ProCountAll = list.Sum(c => c.ProCountAll),
                DayStatisticsCount = totalDayStatisticsCount,
                StockNum = list.Sum(c => c.StockNum),
                OverduePiece = list.Sum(c => c.OverduePiece),
                ProCount = totalProCount,
                //InventoryTurnover = list.Sum(c => c.InventoryTurnover),
                PreviousMonthRate=totalPrevMonthPiece>0? Math.Round((totalPiece- totalPrevMonthPiece) / totalPrevMonthPiece * 100, 1):0,
                LargeSpecRatio=Convert.ToDecimal(list.Sum(a=>a.Piece))>0? Math.Round(Convert.ToDecimal(list.Sum(a=>a.BigSizeCount))/Convert.ToDecimal(list.Sum(a=>a.Piece))*100,1):0,
                SalesRate=totalProCountAll>0&&totalDayStatisticsCount>0?(Math.Round(Convert.ToDouble((totalDayStatisticsCount)) / Convert.ToDouble(totalProCountAll) * 100, 2)):0,
                InventoryTurnover=totalProPiece>0&&totalPiece>0?Math.Round(totalProPiece / ((Convert.ToDouble(totalPiece) / days) * 30), 1):0,
                ClickConversionRate=totalUniqueClientCount>0&&totalclickCount>0? Math.Round(Convert.ToDouble((totalUniqueClientCount)) / Convert.ToDouble(totalclickCount) * 100, 1):0,
                LocalSkuOutOfStockRate=(totalProCountAll-totalProCount)>0?Math.Round((Convert.ToDouble((totalProCountAll-totalProCount)/totalProCountAll) * 100), 1):0,
                GlobalSkuOosRate=(totalGlobalProCountAll-totalGlobalProCount)>0?Math.Round((Convert.ToDouble((totalGlobalProCountAll-totalGlobalProCount)/totalGlobalProCountAll) * 100), 1):0,
                OverdueProCountRate=(totalPiece)>0&&totalOverduePiece>0?Math.Round((Convert.ToDouble(Convert.ToDouble(totalOverduePiece)/(totalProPiece)) * 100), 1):0,
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
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
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
        public void DayStatisticsExportV4()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_UserData(queryEntity);

            string err = "OK";
            var awbList = list;

            if (awbList.Count > 0) { OrderDayStatisticsDataExportV3 = awbList; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }
        public void DayStatisticsExportV6()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_TypeBrowseData(queryEntity);

            string err = "OK";
            var awbList = list;

            if (awbList.Count > 0) { OrderDayStatisticsDataExportV6 = awbList; }
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
        public List<CargoOrderDayStatisticsEntity> OrderDayStatisticsDataExportV3
        {
            get
            {
                if (Session["OrderDayStatisticsDataExportV3"] == null)
                {
                    Session["OrderDayStatisticsDataExportV3"] = new List<CargoOrderDayStatisticsEntity>();
                }
                return (List<CargoOrderDayStatisticsEntity>)(Session["OrderDayStatisticsDataExportV3"]);
            }
            set
            {
                Session["OrderDayStatisticsDataExportV3"] = value;
            }
        }
        public List<CargoOrderDayStatisticsEntity> OrderDayStatisticsDataExportV6
        {
            get
            {
                if (Session["OrderDayStatisticsDataExportV6"] == null)
                {
                    Session["OrderDayStatisticsDataExportV6"] = new List<CargoOrderDayStatisticsEntity>();
                }
                return (List<CargoOrderDayStatisticsEntity>)(Session["OrderDayStatisticsDataExportV6"]);
            }
            set
            {
                Session["OrderDayStatisticsDataExportV6"] = value;
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
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
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
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
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
        public void GetClickConversionRateDetailV2()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ProductCode"]))) { queryEntity.ProductCode = Convert.ToString(Request["ProductCode"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["Specs"]))) { queryEntity.Specs = Convert.ToString(Request["Specs"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["Figure"]))) { queryEntity.Figure = Convert.ToString(Request["Figure"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.GetClickConversionRateDetailV2(queryEntity);

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

        public void GetLargeSpecRatioDetail()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.GetLargeSpecRatioDetail(queryEntity);

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                Specs = "汇总：",
                BigSizeCount = list.Sum(c => c.BigSizeCount),
                Piece = list.Sum(c => c.Piece),
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }

        public void GetLocalSkuOutOfStockDetail()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.GetLocalSkuOutOfStockDetail(queryEntity);

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                Specs = "汇总：",
                GlobalProPiece = list.Count(),
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }

        public void GetGlobalSkuOosDetail()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["SuppClientNum"]))) { queryEntity.SuppClientNum = Convert.ToInt32(Request["SuppClientNum"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.GetGlobalSkuOosDetail(queryEntity);

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                Specs = "汇总：",
                GlobalProPiece = list.Count(),
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }

        public void GetAllUserCount()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.GetAllUserCount(queryEntity);

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                CompanyName = "汇总：",
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

        public void QueryHCYC_UserData()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["typeStr"]))) { queryEntity.typeStr = Convert.ToString(Request["typeStr"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_UserData(queryEntity);
            List<CargoOrderDayStatisticsEntity> footlist = new List<CargoOrderDayStatisticsEntity>();

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                HouseName = "汇总：",
                UserCount = list.Sum(c => c.UserCount),
                DayStatisticsCount = list.Sum(c => c.DayStatisticsCount),
                ClientCount = list.Sum(c => c.ClientCount),
                UserUniqueClientCount = list.Sum(c => c.UserUniqueClientCount),
                StoresUniqueClientCount = list.Sum(c => c.StoresUniqueClientCount),
                UniqueClientNum = list.Sum(c => c.UniqueClientNum),
                UniqueClientCount = list.Sum(c => c.UniqueClientCount),
                ProCount = list.Sum(c => c.ProCount),
                ProCountAll = list.Sum(c => c.ProCountAll),
                AllClientCount = list.Sum(c => c.AllClientCount),
                AllUserCount = list.Sum(c => c.AllUserCount),
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);

        }

        public void QueryHCYC_TypeBrowseData()
        {
            CargoHouseAimEntity queryEntity = new CargoHouseAimEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderType"]))) { queryEntity.OrderType = Convert.ToInt32(Request["OrderType"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToInt32(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["TypeID"]))) { queryEntity.TypeID = Convert.ToInt32(Request["TypeID"]); }
            CargoReportBus bus = new CargoReportBus();

            var list = bus.QueryHCYC_TypeBrowseData(queryEntity);
            List<CargoOrderDayStatisticsEntity> footlist = new List<CargoOrderDayStatisticsEntity>();

            var totalUniqueClientCount = Convert.ToDouble(list.Sum(c => c.UniqueClientCount));
            var totalclickCount = Convert.ToDouble(list.Sum(c => c.clickCount));

            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = new[] {
            new
            {
                CompanyName = "汇总：",
                UserCount = list.Sum(c => c.UserCount),
                DayStatisticsCount = list.Sum(c => c.DayStatisticsCount),
                clickCount = list.Sum(c => c.clickCount),
                UniqueClientNum = list.Sum(c => c.UniqueClientNum),
                UniqueClientCount = list.Sum(c => c.UniqueClientCount),
                 ClickConversionRate=totalUniqueClientCount>0&&totalclickCount>0? Math.Round(Convert.ToDouble((totalUniqueClientCount)) / Convert.ToDouble(totalclickCount) * 100, 1):0,
            }
            };
            //if (list.Count > 0) { CargoDailyReportType = "出库"; }

            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
        }

        #endregion

        #region 同步
        public void HCYCSynchronization()
        {
            DateTime? dt = null;
            DateTime? et = null;
            CargoOrderBus cargoOrderBus = new CargoOrderBus();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { dt = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { et = Convert.ToDateTime(Request["EndDate"]); }
            //生成当日小程序订单数据
            cargoOrderBus.InsertDayWxOrder("", dt, et);
            String json = JSON.Encode("ok");
            Response.Clear();
            Response.Write(json);
        }

        #endregion
    }
}