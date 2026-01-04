using DocumentFormat.OpenXml.Spreadsheet;
using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.House;
using House.Entity.Cargo.Product;
using House.Entity.House;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Formula.Functions;
using Org.BouncyCastle.Asn1.Ocsp;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Client
{
    public partial class clientManagerApi : System.Web.UI.Page
    {
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
                log.UserID = "test";
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }
        #region 客户档案

        public void GetClientKpiData()
        {
            List<CargoClintKpi> kpis = new List<CargoClintKpi>();
            CargoClientBus bus = new CargoClientBus();
            var data = bus.GetClientKpiData();//平台用户总数
            kpis.AddRange(data);
            //JSON
            String json = JSON.Encode(kpis);
            Response.Clear();
            Response.Write(json);
        }

        public void GetRankingUserData()
        {
            var limit = Convert.ToInt32(Request["limit"]);
            List<CargoClintRanking> datas = new List<CargoClintRanking>();
            CargoClientBus bus = new CargoClientBus();
            var data = bus.GetRankingUserData(limit);//
            datas.AddRange(data);
            //JSON
            String json = JSON.Encode(datas);
            Response.Clear();
            Response.Write(json);
        }

        public void GetRankingUserConsumptionData()
        {
            var limit = Convert.ToInt32(Request["limit"]);
            List<CargoClintRanking> datas = new List<CargoClintRanking>();
            CargoClientBus bus = new CargoClientBus();
            var data = bus.GetRankingUserConsumptionData(limit);//
            datas.AddRange(data);
            //JSON
            String json = JSON.Encode(datas);
            Response.Clear();
            Response.Write(json);
        }

        public void GetRankingUserHotsellingData()
        {
            var limit = Convert.ToInt32(Request["limit"]);
            List<CargoClintRanking> datas = new List<CargoClintRanking>();
            CargoClientBus bus = new CargoClientBus();
            var data = bus.GetRankingUserHotsellingData(limit);//
            datas.AddRange(data);
            //JSON
            String json = JSON.Encode(datas);
            Response.Clear();
            Response.Write(json);
        }
        /// <summary>
        /// 用户所拥有权限的仓库集合
        /// </summary>
        public void CargoPermisionHouse()
        {
            CargoHouseBus bus = new CargoHouseBus();
            List<CargoHouseEntity> list = new List<CargoHouseEntity>();
            list = bus.QueryALLHouse();
            var resultData = list.Select(a => new { name = a.Name, id = a.HouseID }).ToList();
            String json = JSON.Encode(resultData);
            Response.Clear();
            Response.Write(json);
        }
        public void getPagedCustomers()
        {
            var page = Convert.ToInt32(Request["page"]);
            var pageSize = Convert.ToInt32(Request["pageSize"]);
            string customerName = Request["paramData[customerName]"] ?? "";
            string customerCode = Request["paramData[customerCode]"] ?? "";
            string phoneNumber = Request["paramData[phoneNumber]"] ?? "";
            string warehouse = Request["paramData[warehouse]"] ?? "";
            string customerType = Request["paramData[customerType]"] ?? "";
            string activeLevel = Request["paramData[activeLevel]"] ?? "";
            string preferredBrand = Request["paramData[preferredBrand]"] ?? "";
            string preferredSpec = Request["paramData[preferredSpec]"] ?? "";
            string valueLevel = Request["paramData[valueLevel]"] ?? "";
            var param = new CargoCustomerFilter
            {
                customerName = customerName,
                customerCode = customerCode,
                phoneNumber = phoneNumber,
                warehouse = warehouse,
                customerType = customerType,
                activeLevel = activeLevel,
                preferredBrand = preferredBrand,
                preferredSpec = preferredSpec,
                valueLevel = valueLevel
            };
            param.page = page;
            param.pageSize = pageSize;
            CargoClientBus bus = new CargoClientBus();
            var list = bus.getPagedCustomers(param);
            var resultData = new
            {
                data = new
                {
                    success = true,
                    list = (List<CargoCustomerParam>)list["list"],
                    total = list["total"]
                },
            };
            String json = JSON.Encode(resultData);
            Response.Clear();
            Response.Write(json);
        }
        #endregion
    }
}