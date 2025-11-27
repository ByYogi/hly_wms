using Cargo.Extensions;
using Cargo.QY;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Bibliography;
using House.Business;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.Product;
using House.Entity.Dto.Order;
using House.Entity.Dto.Purchase;
using House.Entity.House;
using iText.Layout.Element;
using Newtonsoft.Json;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Formula.Functions;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using Senparc.Weixin.Annotations;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.AppStore;
using Senparc.Weixin.MP.TenPayLibV3;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO.Pipelines;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Cargo.Order.orderApi;
using static NPOI.HSSF.Record.Formula.AttrPtg;
using static NPOI.HSSF.Record.PageBreakRecord;
using static NPOI.HSSF.Util.HSSFColor;

namespace Cargo.Purchase
{
    public partial class purchaseApi : BasePage
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
                log.UserID = UserInfor.LoginName.Trim();
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }
        /// <summary>
        /// 查询采购单
        /// </summary>
        public void QueryCargoRealPurchase()
        {
            CargoRealFactoryPurchaseOrderEntity queryEntity = new CargoRealFactoryPurchaseOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ETATimeStart"]))) { queryEntity.ETATimeStart = Convert.ToDateTime(Request["ETATimeStart"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ETATimeEnd"]))) { queryEntity.ETATimeEnd = Convert.ToDateTime(Request["ETATimeEnd"]); }
            //if (!string.IsNullOrEmpty(Convert.ToString(Request["HouseID"]))) { queryEntity.HouseID = Convert.ToString(Request["HouseID"]); }
            if (!string.IsNullOrEmpty(Request["HouseID"]))//一级分类
            {
                queryEntity.CargoPermisID = Convert.ToString(Request["HouseID"]);//所属仓库ID
            }
            else
            {
                queryEntity.CargoPermisID = UserInfor.CargoPermisID;//所属仓库ID
            }
            queryEntity.PurOrderNo = Convert.ToString(Request["PurOrderNo"]);

            if (!string.IsNullOrEmpty(Request["PurchaserName"])) queryEntity.PurchaserName = Convert.ToString(Request["PurchaserName"]);
            if (!string.IsNullOrEmpty(Request["PurDepartID"]))
            {
                queryEntity.PurDepartID = Convert.ToInt32(Request["PurDepartID"]);
            }
            if (Request["ApplyStatus"] != "-1") { queryEntity.ApplyStatus = Convert.ToString(Request["ApplyStatus"]); }
            if (Request["PurchaseType"] != "-1") { queryEntity.PurchaseType = Convert.ToString(Request["PurchaseType"]); }
            if (Request["PurchaseInStoreType"] != "-1") { queryEntity.PurchaseInStoreType = Convert.ToString(Request["PurchaseInStoreType"]); }
            if (Request["APurchaseInStoreState"] != "-1") { queryEntity.PurchaseInStoreState = Convert.ToString(Request["APurchaseInStoreState"]); }
            if (Request["APurchaseUploadDoc"] != "-1") { queryEntity.PurchaseUploadDoc = Convert.ToString(Request["APurchaseUploadDoc"]); }
            if (!string.IsNullOrEmpty(Request["ATypeName"])) queryEntity.TypeName = Convert.ToString(Request["ATypeName"]);
            if (!string.IsNullOrEmpty(Request["ASpec"])) queryEntity.Specs = Convert.ToString(Request["ASpec"]);
            if (!string.IsNullOrEmpty(Request["AGoodsName"])) queryEntity.GoodsCode = Convert.ToString(Request["AGoodsName"]);
            if (!string.IsNullOrEmpty(Request["AFigure"])) queryEntity.Figure = Convert.ToString(Request["AFigure"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            Hashtable list = bus.QueryCargoRealPurchase(pageIndex, pageSize, queryEntity);
            var lists = (List<CargoRealFactoryPurchaseOrderEntity>)list["rows"];
            list["footer"] = new[] {
            new
            {
                PurOrderNo = lists.Count(),
                Piece = lists.Sum(c => c.Piece),
                ReplyNum = lists.Sum(c => c.ReplyNum),
                TransportFee = lists.Sum(c => c.TransportFee),
                TotalCharge = lists.Sum(c => c.TotalCharge)
            }
            };

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        public void cargoRealFactoryPurchaseHouses()
        {
            CargoRealFactoryPurchaseHouseEntity queryEntity = new CargoRealFactoryPurchaseHouseEntity();
            queryEntity.PurOrderID = Convert.ToInt64(Request["PurOrderID"]);
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            List<CargoRealFactoryPurchaseHouseEntity> list = bus.cargoRealFactoryPurchaseHouses(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        public void cargoRealFactoryPurchaseOrderGoods()
        {
            CargoRealFactoryPurchaseOrderGoodsEntity queryEntity = new CargoRealFactoryPurchaseOrderGoodsEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["PurOrderID"])))
            {
                queryEntity.PurOrderID = Convert.ToInt64(Request["PurOrderID"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["FPID"])))
            {
                queryEntity.FPID = Convert.ToInt64(Request["FPID"]);
            }
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            List<CargoRealFactoryPurchaseOrderGoodsEntity> list = bus.cargoRealFactoryPurchaseOrderGoods(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
        }
        public void QueryRealFactoryPODetail()
        {
            RealFactoryPODetailListDto result = new RealFactoryPODetailListDto();
            var queryParam = new RealFactoryPODetailParams
            {
                TypeName = Request.GetString("TypeName"),
                Specs = Request.GetString("Specs"),
                Figure = Request.GetString("Figure"),
                GoodsCode = Request.GetString("GoodsCode"),
                PurOrderNo = Request.GetString("PurOrderNo"),
                ProductCode = Request.GetString("ProductCode"),
                PurchaserName = Request.GetString("PurchaserName"),
                PurchaseType = Request.GetByte("PurchaseType"),
                StartDate = Request.GetDateTime("StartDate"),
                EndDate = Request.GetDateTime("EndDate"),
            };

            //分页
            int? pageIndex = Request.GetInt("page");
            int? pageSize = Request.GetInt("rows");
            queryParam.SetPage(pageIndex, pageSize);

            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            result = bus.QueryRealFactoryPODetail(queryParam);


            List<RealFactoryPODetailDto> footlist = new List<RealFactoryPODetailDto>(){new RealFactoryPODetailDto
                {
                    PurOrderNo  = "汇总：",
                    Piece = result.Data.Sum(c => c.Piece),
                    PurchasePrice = result.Data.Sum(c => c.PurchasePrice)
                }
            };
            Hashtable resHT = new Hashtable();
            resHT["rows"] = result.Data;
            resHT["total"] = result.DataTotal;
            resHT["footer"] = footlist;

            string resultjson = JSON.Encode(resHT);//result.ToJSON();
            Response.Clear();
            Response.Write(resultjson);
            Response.Flush();
        }

        public void GetRealFactoryPODetailExcel()
        {

            RealFactoryPODetailListDto result = new RealFactoryPODetailListDto();
            var queryParam = new RealFactoryPODetailParams
            {
                TypeName = Request.GetString("TypeName"),
                Specs = Request.GetString("Specs"),
                Figure = Request.GetString("Figure"),
                GoodsCode = Request.GetString("GoodsCode"),
                PurOrderNo = Request.GetString("PurOrderNo"),
                ProductCode = Request.GetString("ProductCode"),
                PurchaserName = Request.GetString("PurchaserName"),
                PurchaseType = Request.GetByte("PurchaseType"),
                StartDate = Request.GetDateTime("StartDate"),
                EndDate = Request.GetDateTime("EndDate"),
            };


            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            result = bus.QueryRealFactoryPODetail(queryParam);

            if (result.Data.Count <= 0) { return; }


            string tname = string.Empty;
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("序号", typeof(int)),
                new DataColumn("采购单号", typeof(string)),
                new DataColumn("开单时间", typeof(string)),
                new DataColumn("需求部门", typeof(string)),
                new DataColumn("物权方", typeof(string)),
                new DataColumn("是否含税", typeof(string)),
                new DataColumn("供应商", typeof(string)),
                new DataColumn("采购单类型", typeof(string)),
                new DataColumn("入库单类型", typeof(string)),
                new DataColumn("开单员", typeof(string)),

                new DataColumn("付款方式", typeof(string)),
                new DataColumn("产品编码", typeof(string)),
                new DataColumn("产品名称", typeof(string)),
                new DataColumn("品牌", typeof(string)),
                new DataColumn("规格", typeof(string)),
                new DataColumn("花纹", typeof(string)),
                new DataColumn("货品代码", typeof(string)),
                new DataColumn("载重指数", typeof(string)),
                new DataColumn("速度级别", typeof(string)),

                new DataColumn("采购数量", typeof(int)),
                new DataColumn("采购价格", typeof(int))
            });

            var ownershipDicts = new Dictionary<byte, string>
            {
                { 1, "昆明云仓" },
                { 2, "龙华云仓" },
                { 3, "东平云仓" },
                { 4, "沙井云仓" },
                { 5, "星沙云仓" },
                { 6, "增城云仓" },
                { 7, "西安云仓" },
                { 8, "汉口云仓" },
                { 9, "顺捷云仓" },
                { 10, "汕头云仓" },
                { 11, "渭南云仓" },
                { 12, "北辰云仓" },
                { 13, "南沙云仓" },
                { 14, "从化云仓" },
                { 15, "南海云仓" },
                { 16, "大兴云仓" },
                { 17, "经开云仓" },
                { 18, "香坊云仓" },
                { 19, "栾城云仓" },
                { 20, "铁西云仓" },
                { 21, "济南云仓" },
                { 22, "太原云仓" },
                { 23, "衡阳云仓" },
                { 24, "嘉定云仓" },
                { 25, "常熟云仓" },
                { 26, "杭州云仓" },
                { 27, "南山云仓" },
                { 28, "双流云仓" },
                { 29, "江宁云仓" },
                { 30, "连江云仓" },
                { 31, "兰州云仓" },
                { 32, "银川云仓" },
                { 33, "新疆云仓" },
                { 34, "南开云仓" },
                { 35, "兴宁云仓" },
                { 36, "花都云仓" },
                { 37, "蔡甸云仓" },
                { 38, "光明云仓" },
                { 39, "秀英云仓" },
                { 40, "贵阳云仓" },
                { 41, "揭阳云仓" },
                { 42, "南宁云仓" },
                { 43, "韶关云仓" },
                { 44, "肇庆云仓" },
                { 45, "广州狄乐OE" },
                { 46, "广州狄乐RE" },
                { 47, "湖北狄乐RE" },
                { 48, "湖南狄乐RE" }
            };

            int i = 0;
            foreach (var it in result.Data)
            {
                i++;
                DataRow newRows = table.NewRow();

                newRows["序号"] = i;
                newRows["采购单号"] = it.PurOrderNo;
                newRows["开单时间"] = it.CreateDate?.ToString("yyyy-MM-dd");
                newRows["需求部门"] = it.PurDepart;
                newRows["物权方"] =  ownershipDicts.TryGetValue(it.OwnerShip.GetValueOrDefault(), out string ownershipStr) ? ownershipStr : "";
                newRows["是否含税"] = it.WhetherTax;
                newRows["供应商"] = it.PurchaserName;
                newRows["采购单类型"] = it.PurchaseType;
                newRows["入库单类型"] = it.PurchaseInStoreType;
                newRows["开单员"] = it.CreateAwb;
                newRows["付款方式"] = it.PaymentMethod;

                newRows["产品编码"] = it.ProductCode;
                newRows["产品名称"] = it.ProductName;
                newRows["品牌"] = it.TypeName;
                newRows["规格"] = it.Specs;
                newRows["花纹"] = it.Figure;
                newRows["货品代码"] = it.GoodsCode;
                newRows["载重指数"] = it.LoadIndex;
                newRows["速度级别"] = it.SpeedLevel;
                newRows["采购数量"] = it.Piece;
                newRows["采购价格"] = it.PurchasePrice;

                switch (it.WhetherTax)
                {
                    case 0:
                        newRows["是否含税"] = "不含税";
                        break;
                    case 1:
                        newRows["是否含税"] = "含税";
                        break;
                    default:
                        newRows["补货单状态"] = "";
                        break;
                }
                switch (it.PurchaseType)
                {
                    case 0:
                        newRows["采购单类型"] = "工厂采购";
                        break;
                    case 1:
                        newRows["采购单类型"] = "市场采购";
                        break;
                    default:
                        newRows["采购单类型"] = "";
                        break;
                }
                switch (it.PurchaseInStoreType)
                {
                    case 0:
                        newRows["入库单类型"] = "入仓单";
                        break;
                    case 1:
                        newRows["入库单类型"] = "调货单";
                        break;
                    case 2:
                        newRows["入库单类型"] = "提送单";
                        break;
                    default:
                        newRows["入库单类型"] = "";
                        break;
                }
                switch (it.PaymentMethod)
                {
                    case 0:
                        newRows["付款方式"] = "月结";
                        break;
                    case 1:
                        newRows["付款方式"] = "周结";
                        break;
                    case 2:
                        newRows["付款方式"] = "现结";
                        break;
                    default:
                        newRows["入库单类型"] = "";
                        break;
                }
                table.Rows.Add(newRows);
            }

            ToExcel.DataTableToExcel(table, "", "采购明细" + DateTime.Now.ToString("yyMMddHHmmss"));
        }

        /// <summary>
        /// 修改物流单号和预计到货时间
        /// </summary>
        public void UpdateETRTimeLogisAwbNo()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoRealFactoryPurchaseHouseEntity ent = new CargoRealFactoryPurchaseHouseEntity();
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";

            try
            {
                ent.FPID = Convert.ToInt64(Request["FPID"]);
                ent.LogisID = Convert.ToInt32(Request["LogisID"]);
                ent.LogisAwbNo = Convert.ToString(Request["LogisAwbNo"]);
                ent.HouseName = Convert.ToString(Request["HouseName"]);
                ent.ETATime = Convert.ToDateTime(Request["ETATime"]);
                ent.PurOrderID = Convert.ToInt32(Request["PurOrderID"]);
                ent.DeliveryOrderNo = Convert.ToString(Request["DeliveryOrderNo"]);
                bus.UpdateETRTimeLogisAwbNo(ent, log);
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);

        }
        /// <summary>
        /// 删除明细
        /// </summary>
        public void DelPurchaseGoods()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoRealFactoryPurchaseOrderGoodsEntity> list = new List<CargoRealFactoryPurchaseOrderGoodsEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购管理";
            log.Status = "0";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            CargoRealFactoryPurchaseOrderBus cargoReal = new CargoRealFactoryPurchaseOrderBus();
            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoRealFactoryPurchaseOrderEntity orderEnt = cargoReal.QueryCargoRealFactoryPurchaseEntity(new CargoRealFactoryPurchaseOrderEntity { PurOrderID = Convert.ToInt64(row["PurOrderID"]) });
                    if (!orderEnt.CheckStatus.Equals("0"))
                    {
                        msg.Message = "采购单：" + orderEnt.PurOrderNo + "不是未结算状态，不允许删除";
                        msg.Result = false;
                        goto ERROR;
                    }
                    if (orderEnt.ApplyStatus.Equals("1"))
                    {
                        msg.Message = "采购单：" + orderEnt.PurOrderNo + "审批中，不允许删除";
                        msg.Result = false;
                        goto ERROR;
                    }
                    if (orderEnt.ApplyStatus.Equals("3"))
                    {
                        msg.Message = "采购单：" + orderEnt.PurOrderNo + "审批结束，不允许删除";
                        msg.Result = false;
                        goto ERROR;
                    }
                    break;
                }

                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoRealFactoryPurchaseOrderGoodsEntity
                    {
                        PurOrderID = Convert.ToInt64(row["PurOrderID"]),
                        FPID = Convert.ToInt64(row["FPID"]),
                        GoodsID = Convert.ToInt64(row["GoodsID"]),
                        ProductCode = Convert.ToString(row["ProductCode"]),
                        TypeName = Convert.ToString(row["TypeName"]),
                        GoodsCode = Convert.ToString(row["GoodsCode"]),
                        Figure = Convert.ToString(row["Figure"]),
                        TypeID = Convert.ToInt32(row["TypeID"]),
                        Piece = Convert.ToInt32(row["Piece"]),
                        ReplyPiece = Convert.ToInt32(row["ReplyPiece"]),
                        PurchasePrice = Convert.ToDecimal(row["PurchasePrice"])
                    });
                }
                if (msg.Result)
                {
                    cargoReal.DelPurchaseGoods(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        ERROR:
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }
        /// <summary>
        /// 修改采购数据
        /// </summary>
        public void UpdatePurchaseReplyNum()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoRealFactoryPurchaseOrderGoodsEntity ent = new CargoRealFactoryPurchaseOrderGoodsEntity();
            CargoRealFactoryPurchaseOrderBus cargoReal = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";

            try
            {
                CargoRealFactoryPurchaseOrderEntity orderEnt = cargoReal.QueryCargoRealFactoryPurchaseEntity(new CargoRealFactoryPurchaseOrderEntity { PurOrderID = Convert.ToInt64(Request["PurOrderID"]) });
                if (!orderEnt.CheckStatus.Equals("0"))
                {
                    msg.Message = "采购单：" + orderEnt.PurOrderNo + "不是未结算状态，不允许修改";
                    msg.Result = false;
                    goto ERROR;
                }
                if (orderEnt.ApplyStatus.Equals("1"))
                {
                    msg.Message = "采购单：" + orderEnt.PurOrderNo + "审批中，不允许修改";
                    msg.Result = false;
                    goto ERROR;
                }
                if (orderEnt.ApplyStatus.Equals("3"))
                {
                    msg.Message = "采购单：" + orderEnt.PurOrderNo + "审批结束，不允许修改";
                    msg.Result = false;
                    goto ERROR;
                }

                ent.PurOrderID = Convert.ToInt64(Request["PurOrderID"]);
                ent.FPID = Convert.ToInt64(Request["FPID"]);
                ent.GoodsID = Convert.ToInt64(Request["GoodsID"]);
                ent.Piece = Convert.ToInt32(Request["Piece"]);
                ent.ReplyPiece = Convert.ToInt32(Request["ReplyPiece"]);
                ent.PurchasePrice = Convert.ToDecimal(Request["PurchasePrice"]);
                cargoReal.UpdatePurchaseReplyNum(ent, log);
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
        ERROR:
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);

        }

        /// <summary>
        /// 保存采购单数据
        /// </summary>
        public void SavePurchaseOrderData()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string json = Request["submitData"];
            //string HorseFacOrderNo = Request["HorseFacOrderNo"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            if (GridRows.Count <= 0)
            {
                msg.Message = "没有采购明细数据";
                msg.Result = false;
                goto ERROR;
            }
            CargoFactoryOrderBus cargoFactoryOrderBus = new CargoFactoryOrderBus();

            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购管理";
            log.NvgPage = "新增采购单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            log.Status = "0";
            List<QyOrderUpdateGoodsEntity> goods = new List<QyOrderUpdateGoodsEntity>();

            #region 组织采购单数据
            CargoRealFactoryPurchaseOrderEntity ent = new CargoRealFactoryPurchaseOrderEntity();
            List<CargoRealFactoryPurchaseHouseEntity> purchaseHouseEntities = new List<CargoRealFactoryPurchaseHouseEntity>();
            List<CargoRealFactoryPurchaseOrderGoodsEntity> orderGoodsEntities = new List<CargoRealFactoryPurchaseOrderGoodsEntity>();
            ent.EnSafe();

            int pieceSum = 0;
            decimal trFee = 0.00M;
            ent.PurOrderNo = Common.GetMaxPurchaseOrderNum();
            ent.WhetherTax = Convert.ToString(Request["WhetherTax"]);
            ent.CheckStatus = "0";
            ent.CreateAwbID = UserInfor.LoginName;
            ent.CreateAwb = UserInfor.UserName;
            ent.CreateDate = DateTime.Now;
            ent.PurDepartID = Convert.ToInt32(Request["PurDepartID"]);//采购部门编码
            ent.PurDepart = Convert.ToString(Request["PurDepart"]);//采购部门名称
            ent.PurchaserID = Convert.ToInt32(Request["PurchaserID"]);
            ent.DeliveryAddress = Convert.ToString(Request["DeliveryAddress"]);
            ent.DeliveryTelephone = Convert.ToString(Request["DeliveryTelephone"]);
            ent.DeliveryBoss = Convert.ToString(Request["DeliveryBoss"]);
            ent.DeliveryCellphone = Convert.ToString(Request["DeliveryCellphone"]);
            ent.PurchaseType = Convert.ToString(Request["PurchaseType"]);//采购类型0:工厂采购单1:市场采购单
            ent.PurchaseInStoreType = Convert.ToString(Request["PurchaseInStoreType"]);//入库类型 0:入仓单1:调货单2:提送单
            ent.OwnerShip = Convert.ToString(Request["OwnerShip"]);
            ent.OP_ID = UserInfor.LoginName;
            ent.OP_DATE = DateTime.Now;
            ent.ApplyID = UserInfor.LoginName;
            ent.ApplyName = UserInfor.UserName;
            ent.ApplyDate = DateTime.Now;
            ent.ApplyStatus = Convert.ToString(Request["PurchaseType"]).Equals("1") ? "3" : "0";
            ent.Remark = Convert.ToString(Request["Remark"]);
            ent.BusinessID = Convert.ToString(Request["BusinessID"]);
            ent.TID = string.IsNullOrEmpty(Convert.ToString(Request["TID"])) ? 0 : Convert.ToInt64(Request["TID"]);
            List<CargoGtmcProOrderEntity> gtmcProOrderEntities = new List<CargoGtmcProOrderEntity>();
            //int HouseID = 0;
            if (ent.PurchaseType.Equals("1") && ent.PurchaseInStoreType.Equals("2"))
            {
                //市场采购单并且是提送单
                if (ent.TID.Equals(0))
                {
                    msg.Result = false;
                    msg.Message = "请选择关联ID号";
                    goto ERROR;
                }
                gtmcProOrderEntities = cargoFactoryOrderBus.QueryExterOrderDataList(new CargoGtmcProOrderEntity { TID = ent.TID });
                //HouseID = gtmcProOrderEntities[0].FirstLevelHouseID;
            }
            if (ent.PurchaseType.Equals("0") && ent.PurchaseInStoreType.Equals("2"))
            {
                msg.Result = false;
                msg.Message = "工厂采购单不可添加提送单";
                goto ERROR;
            }

            //string FacOrderNo = string.IsNullOrEmpty(Convert.ToString(Request["FacOrderNo"])) ? DateTime.Now.ToString("yyyyMMddHHmmss") : Convert.ToString(Request["FacOrderNo"]);
            //ent.FacOrderNo = FacOrderNo;
            foreach (Hashtable row in GridRows)
            {
                orderGoodsEntities.Add(new CargoRealFactoryPurchaseOrderGoodsEntity
                {
                    Piece = Convert.ToInt32(row["Piece"]),
                    ReplyPiece = Convert.ToInt32(row["Piece"]),
                    PurchasePrice = Convert.ToDecimal(row["PurchasePrice"]),
                    TypeID = Convert.ToInt32(row["TypeID"]),
                    TypeName = Convert.ToString(row["TypeName"]),
                    HouseID = Convert.ToInt32(row["HouseID"]),
                    ProductCode = Convert.ToString(row["ProductCode"]),
                    Batch = Convert.ToString(row["Batch"]),
                });
                //HouseID = Convert.ToInt32(row["HouseID"]);
                QyOrderUpdateGoodsEntity res = new QyOrderUpdateGoodsEntity();
                res.ModifyPrice = Convert.ToDecimal(row["PurchasePrice"]);//采购价格
                res.OrderNum = Convert.ToInt32(row["Piece"]);//采购数量
                res.OrderPrice = Convert.ToDecimal(row["PurchasePrice"]);//原价格
                res.ProductCode = Convert.ToString(row["ProductCode"]);//产品编码
                res.HouseName = Convert.ToString(row["HouseName"]);//入库仓库
                //res.ProductID = Convert.ToInt64(row["ProductID"]);
                //res.ContainerCode = Convert.ToString(row["ContainerCode"]);
                goods.Add(res);
                pieceSum += Convert.ToInt32(row["Piece"]);
                trFee += (string.IsNullOrEmpty(Convert.ToString(row["PurchasePrice"])) ? 0 : Convert.ToDecimal(row["PurchasePrice"])) * Convert.ToInt32(row["Piece"]);
            }
            if (pieceSum != Convert.ToInt32(Request["Total"]))
            {
                msg.Result = false;
                msg.Message = "采购总数量不一致，请核对后保存";
                goto ERROR;
            }
            if (trFee != Convert.ToDecimal(Request["TotalCharge"]))
            {
                msg.Result = false;
                msg.Message = "采购总金额不一致，请核对后保存";
                goto ERROR;
            }
            ent.Piece = pieceSum;
            ent.ReplyNum = pieceSum;
            ent.TransportFee = trFee;
            ent.TotalCharge = ent.TransportFee + ent.OtherFee;

            // 按 HouseID 分组并计算采购数量总和
            var groupedData = orderGoodsEntities.GroupBy(entity => entity.HouseID).Select(group => new
            {
                HouseID = group.Key,
                TotalPiece = group.Sum(entity => entity.Piece),
            });
            foreach (var group in groupedData)
            {
                purchaseHouseEntities.Add(new CargoRealFactoryPurchaseHouseEntity
                {
                    HouseID = group.HouseID,
                    OrderNum = group.TotalPiece,
                    ReplyNum = group.TotalPiece,
                    orderGoodsEntities = orderGoodsEntities.Where(c => c.HouseID.Equals(group.HouseID)).ToList(),
                });
            }
            ent.purchaseHouseEntities = purchaseHouseEntities;

            #endregion

            if (ent.PurchaseType.Equals("0"))
            {
                //工厂采购单 //提交审批 
                string approveID = Convert.ToString(Request["ApproveID"]);
                if (string.IsNullOrEmpty(approveID))
                {
                    msg.Result = false;
                    msg.Message = "请选择审批流程";
                    goto ERROR;
                }

                #region 推送给相关领导审批改价申请
                CargoOrderBus orderbus = new CargoOrderBus();
                CargoFinanceBus financeBus = new CargoFinanceBus();
                CargoExpenseApproveRoutEntity approveRoute = new CargoExpenseApproveRoutEntity();
                //CargoOrderEntity order = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = ent.OrderNo });
                //推送给相关领导审批改价申请
                QyOrderUpdatePriceEntity result = new QyOrderUpdatePriceEntity();
                result.HouseID = 93;
                result.ApplyID = UserInfor.LoginName;
                result.ApplyName = UserInfor.UserName;
                result.ApplyDate = DateTime.Now;
                result.OrderNo = ent.PurOrderNo;
                result.ApplyStatus = "0";
                result.OrderType = "1";
                //result.OrderID = order.OrderID;
                result.OrderCheckType = "7";
                string ApproveType = Convert.ToString(Request["ApproveID"]);//16:RE采购审批流程 17:OE采购审批流程

                //1.查询审批设置表获得审批流程
                QiyeBus qiye = new QiyeBus();
                CargoFinanceBus f = new CargoFinanceBus();
                //CargoApproveSetEntity AppSet = f.QueryApproveSetByID(33);
                CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
                //审批流程
                List<CargoApproveSetEntity> aset = f.QueryApproveSet(new CargoApproveSetEntity { ApproveType = ApproveType, DelFlag = "0", HouseID = 93 });
                if (aset.Count > 0) { AppSet = aset[0]; }
                else
                {
                    aset = f.QueryApproveSet(new CargoApproveSetEntity { ApproveType = "16", DelFlag = "0", HouseID = 93 });
                    if (aset.Count > 0) { AppSet = aset[0]; }
                }
                List<QyUserEntity> qyUser = new List<QyUserEntity>();
                //2.审批流程的每一级和当前人的审批角色相匹配
                if (AppSet.OneCheckID.Equals("3"))
                {
                    SystemUserEntity entity = new SystemUserEntity();
                    entity.LoginName = UserInfor.LoginName;
                    SystemUserEntity bossLeader = orderbus.QueryBossLeaderByLoginName(entity);
                    if (bossLeader != null)
                    {
                        //申请人有部门负责人
                        if (!string.IsNullOrEmpty(bossLeader.LoginName))
                        {
                            if (bossLeader.LoginName != UserInfor.LoginName)
                            {
                                qyUser.Add(new QyUserEntity { UserID = bossLeader.LoginName, WxName = bossLeader.UserName });
                                result.CheckID = AppSet.OneCheckID;
                                result.CheckName = AppSet.OneCheckName;
                            }
                            //申请人部门负责人是自己，推送给下一级
                            else
                            {
                                qyUser = qiye.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = "93" });
                                result.CheckID = AppSet.TwoCheckID;
                                result.CheckName = AppSet.TwoCheckName;
                            }
                        }
                        //申请人没有部门负责人，推送给下一级
                        else
                        {
                            qyUser = qiye.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = "93" });
                            result.CheckID = AppSet.TwoCheckID;
                            result.CheckName = AppSet.TwoCheckName;
                        }
                    }
                    //申请人信息跳过操作记录错误
                    else
                    {
                        Common.WriteTextLog("推送失败：申请人未查询到数据");
                        msg.Result = true;
                        return;
                    }
                }
                else
                {
                    qyUser = qiye.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, CheckHouseID = "93" });
                    result.CheckID = AppSet.OneCheckID;
                    result.CheckName = AppSet.OneCheckName;
                }

                ent.NextCheckID = result.CheckID;
                ent.NextCheckName = result.CheckName;

                result.Reason = ent.Remark;
                result.SaleManName = UserInfor.UserName;
                result.SaleManID = UserInfor.LoginName;
                result.UpdatePriceGoodsList = goods;
                long OID = qiye.AddOrderUpdatePrice(result, log);
                approveRoute.ExID = OID;
                approveRoute.UserID = Convert.ToString(UserInfor.UserID);
                approveRoute.UserName = UserInfor.UserName;
                approveRoute.Opera = "3";
                approveRoute.Result = ent.Remark;
                approveRoute.ApproveType = ApproveType;//16:RE采购审批流程 17:OE采购审批流程
                financeBus.AddExpenseApproveRout(approveRoute);
                //try
                //{
                //    string PurchaseType = ent.PurchaseType.Equals("0") ? "工厂采购单" : "市场采购单";
                //    string PurchaserName = Convert.ToString(Request["PurchaserName"]);
                //    QySendInfoEntity send = new QySendInfoEntity();
                //    send.title = "采购单申请";
                //    send.msgType = msgType.textcard;
                //    //send.toTag = strTag;
                //    send.content = "<div></div><div>采购单号：" + ent.PurOrderNo + "</div><div>采购类型：" + PurchaseType + "</div><div>供应商：" + PurchaserName + "</div><div>采购数量：" + ent.Piece.ToString() + "  " + ent.TotalCharge.ToString("F2") + "</div><div>申请人：" + result.ApplyName + "</div><div>申请时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</div><div>申请原因：" + result.Reason + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                //    send.url = "http://dlt.neway5.com/QY/qyApprovePurchaseCheck.aspx?OrderNo=" + ent.PurOrderNo + "&OID=" + OID + "&OrderType=1&OrderCheckType=7&ty=0&ApproveType=" + ApproveType;
                //    foreach (var it in qyUser)
                //    {
                //        send.toUser = it.UserID;
                //        WxQYSendHelper.PushInfo("0", "2", send);
                //        Common.WriteTextLog("推送成功：" + it.WxName + ent.PurOrderNo);
                //    }

                //}
                //catch (ApplicationException ex)
                //{
                //    Common.WriteTextLog("推送失败：" + ent.PurOrderNo);
                //    msg.Result = true;
                //}

                #endregion

            }

            //保存采购单
            bus.AddRealPurchaseOrder(ent, log);

            CargoHouseBus house = new CargoHouseBus();
            CargoClientBus clientBus = new CargoClientBus();
            CargoProductBus productBus = new CargoProductBus();
            CargoOrderBus OrderBus = new CargoOrderBus();
            CargoProductBasicPriceBus priceBus = new CargoProductBasicPriceBus();
            if (ent.PurchaseType.Equals("1") && ent.PurchaseInStoreType.Equals("2"))
            {
                //多仓库添加订单
                foreach (var pHouse in purchaseHouseEntities)
                {
                    //市场采购单并且是提送单
                    //根据ent.TID查询客户的一级仓库，订单信息
                    //系统自动生成订单、产品、入库、关联销售订单号
                    List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();

                    CargoClientEntity clientEntity = clientBus.QueryCargoClient(new CargoClientEntity { ShopCode = gtmcProOrderEntities[0].SourceCode, HouseIDStr = "65" });

                    CargoOrderEntity cargoOrder = new CargoOrderEntity();
                    cargoOrder.BusinessID = gtmcProOrderEntities[0].BusinessID;
                    CargoHouseEntity houseEnt = house.QueryCargoHouseByID(pHouse.HouseID);

                    cargoOrder.ShopCode = gtmcProOrderEntities[0].SourceCode;
                    cargoOrder.HAwbNo = gtmcProOrderEntities[0].GtmcNo;

                    cargoOrder.Dep = Convert.ToString(houseEnt.DepCity);
                    cargoOrder.Dest = gtmcProOrderEntities[0].City;
                    cargoOrder.InsuranceFee = cargoOrder.TransitFee = cargoOrder.DeliveryFee = cargoOrder.OtherFee = cargoOrder.Rebate = 0;
                    cargoOrder.HouseID = pHouse.HouseID;
                    cargoOrder.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                    cargoOrder.CheckOutType = "";
                    cargoOrder.TrafficType = "0";
                    cargoOrder.DeliveryType = "2";
                    cargoOrder.AcceptAddress = clientEntity.Address;
                    cargoOrder.AcceptPeople = clientEntity.Boss;

                    cargoOrder.IsPrintPrice = 0;
                    cargoOrder.AcceptUnit = clientEntity.ClientName;
                    cargoOrder.AcceptTelephone = clientEntity.Telephone;
                    cargoOrder.AcceptCellphone = clientEntity.Cellphone;
                    cargoOrder.CreateAwb = UserInfor.UserName;
                    cargoOrder.CreateAwbID = UserInfor.LoginName;
                    cargoOrder.CreateDate = DateTime.Now;// Convert.ToDateTime(Request["CreateDate"]);
                    cargoOrder.OP_ID = UserInfor.LoginName.Trim();
                    cargoOrder.SaleManID = clientEntity.UserID;
                    cargoOrder.SaleManName = clientEntity.UserName;
                    //cargoOrder.SaleCellPhone = Convert.ToString(Request["SaleCellPhone"]);
                    cargoOrder.Remark = ent.Remark + " " + clientEntity.ShopCode;
                    cargoOrder.ThrowGood = "12";
                    cargoOrder.LogisID = clientEntity.LogisLineLogisID;
                    cargoOrder.ClientNum = clientEntity.ClientNum;
                    cargoOrder.PayClientNum = clientEntity.ClientNum;
                    cargoOrder.PayClientName = clientEntity.ClientName;
                    cargoOrder.PurchaseHouseID = pHouse.HouseID;
                    cargoOrder.PurchaserID = Convert.ToString(Request["PurchaserID"]);
                    cargoOrder.PurchaserName = Convert.ToString(Request["PurchaserName"]);
                    cargoOrder.CheckStatus = "0";
                    decimal PretrFee = 0.00M;
                    int PrepieceSum = 0;
                    string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号

                    //CargoAreaEntity cargoAreaEntity = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = gtmcProOrderEntities[0].FirstLevelAreaID });

                    int OrderNum = 0;
                    cargoOrder.OrderNo = Common.GetMaxOrderNumByCurrentDate(pHouse.HouseID, houseEnt.HouseCode, out OrderNum);
                    cargoOrder.OrderNum = OrderNum;//最新订单顺序号

                    List<CargoProductEntity> productList = new List<CargoProductEntity>();
                    foreach (Hashtable row in GridRows)
                    {
                        if (Convert.ToInt32(row["HouseID"]) != pHouse.HouseID)
                        {
                            continue;
                        }

                        List<CargoProductTypeEntity> pType = productBus.QueryProductType(new CargoProductTypeEntity { TypeID = Convert.ToInt32(row["TypeID"]), ParentID = -1 });
                        //根据产品规格信息查询价格
                        string proYear, proMonth;
                        string BelongMonth = DateTime.Now.ToString("yyyyMM");
                        proYear = BelongMonth.Substring(0, 4);
                        proMonth = BelongMonth.Substring(4, 2);
                        CargoProductBasicPriceEntity basicPriceEntity = new CargoProductBasicPriceEntity();
                        basicPriceEntity.ProductCode = Convert.ToString(row["ProductCode"]);
                        basicPriceEntity.GoodsCode = Convert.ToString(row["GoodsCode"]);
                        basicPriceEntity.ProYear = proYear;
                        basicPriceEntity.ProMonth = proMonth;
                        basicPriceEntity.HouseID = pHouse.HouseID;
                        basicPriceEntity.TypeID = pType.Count > 0 ? -1 : pType[0].TypeID;
                        basicPriceEntity.Born = -1;
                        double OESalePrice = 0;
                        CargoProductBasicPriceEntity basicPrice = house.QueryProductBasicPrice(basicPriceEntity);
                        if (basicPrice != null && basicPrice.PID != 0)
                        {
                            OESalePrice = basicPrice.OESalePrice;
                        }

                        if (pType.Count > 0)
                        {
                            CargoContainerEntity containerEntity = house.QueryTopOneContainer(new CargoContainerEntity { HouseID = pHouse.HouseID });
                            CargoProductEntity entProduct = new CargoProductEntity();
                            entProduct.ParentID = pType[0].ParentID;
                            entProduct.ProductName = house.QueryCargoHouse(new CargoHouseEntity { HouseID = pHouse.HouseID }).CargoDepart;
                            entProduct.GoodsName = Convert.ToString(row["ProductName"]);//产品名称
                            entProduct.TypeID = pType[0].TypeID;
                            entProduct.TypeName = pType[0].TypeName;
                            entProduct.Model = Convert.ToString(row["Model"]);
                            entProduct.GoodsCode = Convert.ToString(row["GoodsCode"]);
                            entProduct.Figure = Convert.ToString(row["Figure"]);
                            entProduct.Specs = Convert.ToString(row["Specs"]);
                            entProduct.Batch = Convert.ToString(row["Batch"]);
                            entProduct.TreadWidth = 0;
                            entProduct.FlatRatio = 0;
                            entProduct.Meridian = "R";
                            entProduct.HubDiameter = 0;
                            entProduct.SpeedLevel = Convert.ToString(row["SpeedLevel"]);
                            entProduct.LoadIndex = Convert.ToString(row["LoadIndex"]);
                            entProduct.UnitPrice = Convert.ToDecimal(row["PurchasePrice"]);
                            entProduct.CostPrice = Convert.ToDecimal(row["PurchasePrice"]);
                            entProduct.TaxCostPrice = Convert.ToDecimal(row["PurchasePrice"]);
                            entProduct.NoTaxCostPrice = Convert.ToDecimal(row["PurchasePrice"]);
                            entProduct.Numbers = Convert.ToInt32(row["Piece"]);
                            entProduct.SalePrice = OESalePrice.Equals(0) ? 1 : Convert.ToDecimal(OESalePrice);
                            entProduct.TradePrice = 0;
                            entProduct.PackageNum = 0;
                            entProduct.PackageWeight = 0;
                            entProduct.Source = "9";
                            entProduct.Born = Convert.ToString(row["Born"]);
                            entProduct.BelongDepart = "1";
                            entProduct.HouseID = pHouse.HouseID;
                            entProduct.Company = "1";
                            entProduct.AreaID = containerEntity.AreaID;
                            entProduct.ContainerID = containerEntity.ContainerID;
                            entProduct.ContainerCode = containerEntity.ContainerCode;
                            entProduct.InCargoID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//入库单号
                            entProduct.BelongMonth = DateTime.Now.ToString("yyyyMM");
                            entProduct.SourceOrderNo = ent.PurOrderNo;
                            entProduct.Assort = "OER";
                            entProduct.InHouseType = "0";
                            entProduct.OrderNo = cargoOrder.OrderNo;
                            entProduct.OPID = UserInfor.LoginName;
                            entProduct.Supplier = "狄乐汽服OE";
                            entProduct.SupplierAddress = "广东广州白云区东平横岗东街好来运B栋3楼";
                            entProduct.SuppClientNum = 551098;
                            entProduct.ProductCode = Convert.ToString(row["ProductCode"]);
                            entProduct.OwnerShip = Convert.ToString(Request["OwnerShip"]);
                            entProduct.GoodsClass = "0";

                            entProduct.PurchaserID = Convert.ToInt32(Request["PurchaserID"]);
                            entProduct.PurchaserName = Convert.ToString(Request["PurchaserName"]);
                            entProduct.DeliveryBoss = Convert.ToString(Request["DeliveryBoss"]);
                            entProduct.DeliveryAddress = Convert.ToString(Request["DeliveryAddress"]);

                            cargoOrder.PurchaserBoss = Convert.ToString(Request["PurchaserBoss"]);
                            cargoOrder.PurchaserAddress = Convert.ToString(Request["PurchaserAddress"]);
                            cargoOrder.PurchaserCellphone = Convert.ToString(Request["PurchaserCellphone"]);


                            productList.Add(entProduct);
                        }

                        #region 订单详情

                        entDest.Add(new CargoOrderGoodsEntity
                        {
                            OrderNo = cargoOrder.OrderNo,
                            HouseID = pHouse.HouseID,
                            TypeID = Convert.ToInt32(row["TypeID"]),
                            Piece = Convert.ToInt32(row["Piece"]),
                            ActSalePrice = OESalePrice.Equals(0) ? 1 : Convert.ToDecimal(OESalePrice),//Convert.ToDecimal(row["ActSalePrice"]),实际销售价
                            Specs = Convert.ToString(row["Specs"]),
                            Figure = Convert.ToString(row["Figure"]),
                            GoodsCode = Convert.ToString(row["GoodsCode"]),
                            Batch = Convert.ToString(row["Batch"]),
                            LoadIndex = Convert.ToString(row["LoadIndex"]),
                            SpeedLevel = Convert.ToString(row["SpeedLevel"]),
                            Model = Convert.ToString(row["Model"]),
                            Born = "0",
                            CostPrice = Convert.ToDecimal(row["PurchasePrice"]),
                            PurchaserID = Convert.ToInt32(Request["PurchaserID"]),
                            PurchaserName = Convert.ToString(Request["PurchaserName"]),
                            DeliveryBoss = Convert.ToString(Request["DeliveryBoss"]),
                            DeliveryAddress = Convert.ToString(Request["DeliveryAddress"]),
                            DeliveryCellphone = Convert.ToString(Request["DeliveryCellphone"]),
                            OP_ID = UserInfor.LoginName.Trim()
                        });
                        PrepieceSum += Convert.ToInt32(row["Piece"]);
                        PretrFee += OESalePrice.Equals(0) ? 1 : Convert.ToDecimal(OESalePrice) * Convert.ToInt32(row["Piece"]);


                        #endregion
                    }
                    cargoOrder.Piece = PrepieceSum;
                    cargoOrder.OutHouseName = houseEnt.Name;
                    cargoOrder.TransportFee = PretrFee;
                    cargoOrder.TotalCharge = cargoOrder.TransportFee + cargoOrder.TransitFee + cargoOrder.OtherFee - cargoOrder.InsuranceFee;

                    cargoOrder.IsMakeSure = 0;
                    cargoOrder.PostponeShip = "1";
                    cargoOrder.AwbStatus = "0";
                    cargoOrder.OrderType = "0";
                    cargoOrder.HouseID = pHouse.HouseID;
                    cargoOrder.HouseName = houseEnt.Name;
                    cargoOrder.goodsList = entDest;
                    cargoOrder.productsList = productList;
                    cargoOrder.ModifyPriceStatus = "0";
                    cargoOrder.OutCargoID = outID;

                    OrderBus.AddPreOrderInfo(cargoOrder, new List<CargoContainerShowEntity>(), log);

                    cargoFactoryOrderBus.UpdateGtmcProOrderStatus(new CargoGtmcProOrderEntity { TID = ent.TID, OrderNo = cargoOrder.OrderNo, OrderStatus = "1", AutoOrderHandleStatus = "1", OrderHandleType = "0", AutoOrderHandleTime = DateTime.Now, OrderAlloType = "2" });
                    List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
                    OrderBus.InsertCargoOrderPush(new CargoOrderPushEntity
                    {
                        OrderNo = cargoOrder.OrderNo,
                        Dep = cargoOrder.Dep,
                        Dest = cargoOrder.Dest,
                        Piece = cargoOrder.Piece,
                        TransportFee = cargoOrder.TransportFee,
                        ClientNum = cargoOrder.ClientNum.ToString(),
                        AcceptAddress = cargoOrder.AcceptAddress,
                        AcceptCellphone = cargoOrder.AcceptCellphone,
                        AcceptTelephone = cargoOrder.AcceptTelephone,
                        AcceptPeople = cargoOrder.AcceptPeople,
                        AcceptUnit = cargoOrder.AcceptUnit,
                        HouseID = cargoOrder.HouseID.ToString(),
                        HouseName = house.QueryCargoHouseByID(cargoOrder.HouseID).Name,// orderEnt.HouseID.Equals(65) || orderEnt.HouseID.Equals(84) ? houseEnt.Name : orderEnt.OutHouseName,
                        OP_ID = UserInfor.UserName,
                        HLYSendUnit = cargoOrder.HouseID.Equals(65) ? productList[0].ProductName : productList[0].ProductName,
                        PushType = "0",
                        PushStatus = "0",
                        LogisID = cargoOrder.LogisID,
                        BusinessID = cargoOrder.BusinessID
                    }, log);
                }


            }

            if (ent.PurchaseType.Equals("1") && (ent.PurchaseInStoreType.Equals("0") || ent.PurchaseInStoreType.Equals("1")))
            {

                //市场采购单  入仓单
                foreach (var pur in purchaseHouseEntities)
                {
                    List<CargoFactoryOrderEntity> factoryList = new List<CargoFactoryOrderEntity>();
                    List<CargoProductEntity> productList = new List<CargoProductEntity>();
                    CargoContainerEntity containerEntity = house.QueryTopOneContainer(new CargoContainerEntity { HouseID = pur.HouseID });
                    CargoHouseEntity che = house.QueryCargoHouse(new CargoHouseEntity { HouseID = pur.HouseID });
                   
                    foreach (var gent in pur.orderGoodsEntities)
                    {
                        string batch = gent.Batch;
                        int bYear = 0, bWeek = 0;
                        if (batch.Length == 4)
                        {
                            bWeek = Convert.ToInt32(batch.Substring(0, 2));
                            bYear = Convert.ToInt32(batch.Substring(2, 2));
                        }
                        else if (batch.Length == 3)
                        {
                            bWeek = Convert.ToInt32(batch.Substring(0, 1));
                            bYear = Convert.ToInt32(batch.Substring(1, 2));
                        }
                        CargoProductSpecEntity cpse = productBus.GetProductSpecByProductCode(gent.ProductCode);
                        CargoProductBasicPriceEntity priceEntity = priceBus.QueryBasicPriceData(new CargoProductBasicPriceEntity { ProductCode = cpse.ProductCode, GoodsCode = cpse.GoodsCode, Specs= cpse.Specs });
                        factoryList.Add(new CargoFactoryOrderEntity
                        {
                            HouseID = pur.HouseID,//到仓仓库
                            FacOrderNo = ent.PurOrderNo,//以采购单号做来货单号
                            Source = 9,
                            SourceName = "市场采购",
                            ProductName = che.CargoDepart,
                            GoodsName = cpse.ProductName,
                            TypeID = gent.TypeID,
                            TypeName = gent.TypeName,
                            OrderType = Convert.ToInt32(ent.PurchaseInStoreType),
                            Model = cpse.Model,
                            BelongMonth = DateTime.Now.ToString("yyyyMM"),
                            Born = "0",
                            Assort = cpse.Assort,
                            Specs = cpse.Specs,
                            LoadIndex = cpse.LoadIndex,
                            SpeedLevel = cpse.SpeedLevel,
                            Figure = cpse.Figure,
                            GoodsCode = cpse.GoodsCode,
                            ProductCode = cpse.ProductCode,
                            ProductUnit = cpse.ProductUnit,
                            BundleNum = cpse.BundleNum,
                            HubDiameter = cpse.HubDiameter,
                            Batch = batch,
                            BatchWeek = bWeek,
                            BatchYear = bYear,
                            OrderNum = gent.Piece,
                            ReplyNumber = gent.ReplyPiece,
                            InPiece = ent.PurchaseInStoreType.Equals("1") ? gent.Piece : 0,
                            InCargoStatus = ent.PurchaseInStoreType.Equals("1") ? 1 : 0,
                            SalePrice = priceEntity==null?0: priceEntity.SalePrice,
                            InHousePrice = priceEntity == null ? 0 : priceEntity.InHousePrice,
                            UnitPrice = Convert.ToDouble(gent.PurchasePrice),
                            CostPrice = Convert.ToDouble(gent.PurchasePrice),
                            TaxCostPrice = Convert.ToDouble(gent.PurchasePrice),
                            NoTaxCostPrice = Convert.ToDouble(gent.PurchasePrice),
                            TradePrice = priceEntity == null ? 0 : priceEntity.TradePrice,
                            NextDayPrice = priceEntity == null ? 0 : priceEntity.NextDayPrice,
                            WholesalePrice = priceEntity == null ? 0 : priceEntity.WholesalePrice,
                            WhetherTax = Convert.ToInt32(Request["WhetherTax"]),
                            SaleMoney = 0,
                            ReceiveName = che.Name,
                            ReceiveCity = che.DepCity,
                            ReceiveMobile = "",
                            SourceHouse = "",
                            SpecsType = "4",
                            OP_Name = UserInfor.UserName,
                            BelongDepart = "1",
                            Company = "0",
                            ContainerID = containerEntity.ContainerID,
                            InCargoID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString(),
                            Supplier = ent.PurDepart,
                            SuppClientNum = Convert.ToString(ent.PurDepartID),
                            SupplierAddress = "",
                            BusinessID = Convert.ToString(Request["BusinessID"]),
                            OwnerShip = Convert.ToString(Request["OwnerShip"]),
                            GoodsClass = "0",
                            PushStatus = "0"
                        });

                        productList.Add(new CargoProductEntity
                        {
                            TypeID = gent.TypeID,
                            InPiece = gent.Piece,
                            Specs = cpse.Specs,
                            Figure = cpse.Figure,
                            Model = cpse.Model,
                            GoodsCode = cpse.GoodsCode,
                            ProductCode = cpse.ProductCode,
                            LoadIndex = cpse.LoadIndex,
                            SpeedLevel = cpse.SpeedLevel,
                            Born = "0",
                            Batch = gent.Batch,
                            UnitPrice = gent.PurchasePrice,
                            CostPrice = gent.PurchasePrice,
                            TaxCostPrice = gent.PurchasePrice,
                            NoTaxCostPrice = gent.PurchasePrice,
                            TradePrice = priceEntity == null ? 0 : Convert.ToDecimal(priceEntity.TradePrice),
                            SalePrice = priceEntity == null ? 0 : Convert.ToDecimal(priceEntity.SalePrice),
                            InHousePrice = priceEntity == null ? 0 : Convert.ToDecimal(priceEntity.InHousePrice),
                            NextDayPrice = priceEntity == null ? 0 : Convert.ToDecimal(priceEntity.NextDayPrice),
                            WholesalePrice = priceEntity == null ? 0 : Convert.ToDecimal(priceEntity.WholesalePrice),
                            OPID = UserInfor.LoginName.ToString(),
                            PackageNum = cpse.BundleNum,
                            Package = cpse.ProductUnit,
                            SuppClientNum = ent.PurDepartID,
                            Supplier = ent.PurDepart,
                            GoodsName = cpse.ProductName,
                            OwnerShip = Convert.ToString(Request["OwnerShip"]),
                            GoodsClass = "0"
                        });
                    }
                    bus.AddPurchaseOrderInfo(ent, productList, factoryList, log);
                }
            }

        ERROR:
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Clear();
            Response.Write(ress);
            Response.End();
        }

        /// <summary>
        /// 删除采购单
        /// </summary>
        public void DelPurchaseOrder()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoRealFactoryPurchaseOrderEntity> list = new List<CargoRealFactoryPurchaseOrderEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoRealFactoryPurchaseOrderBus cargoReal = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.Status = "0";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoRealFactoryPurchaseOrderEntity orderEnt = cargoReal.QueryCargoRealFactoryPurchaseEntity(new CargoRealFactoryPurchaseOrderEntity { PurOrderID = Convert.ToInt64(row["PurOrderID"]) });
                    if (!orderEnt.CheckStatus.Equals("0"))
                    {
                        msg.Message = "采购单：" + orderEnt.PurOrderNo + "不是未结算状态，不允许删除";
                        msg.Result = false;
                        goto ERROR;
                    }
                    if (orderEnt.ApplyStatus.Equals("1"))
                    {
                        msg.Message = "采购单：" + orderEnt.PurOrderNo + "审批中，不允许删除";
                        msg.Result = false;
                        goto ERROR;
                    }
                    if (orderEnt.ApplyStatus.Equals("3"))
                    {
                        msg.Message = "采购单：" + orderEnt.PurOrderNo + "审批结束，不允许删除";
                        msg.Result = false;
                        goto ERROR;
                    }
                    list.Add(new CargoRealFactoryPurchaseOrderEntity
                    {
                        PurOrderID = Convert.ToInt64(row["PurOrderID"]),
                        PurOrderNo = Convert.ToString(row["PurOrderNo"]),
                    });
                }
                if (msg.Result)
                {
                    cargoReal.DelPurchaseOrder(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        ERROR:
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }


        /// <summary>
        /// 采购导入Excel文件
        /// </summary>
        public void saveFile()
        {
            System.Web.HttpFileCollection files = this.Request.Files;
            if (files == null || files.Count == 0) return;
            string attachmentId = Guid.NewGuid().ToString();
            DataTable data = ToExcel.ImportExcelData(files);

            CargoImportEntity import = new CargoImportEntity();
            import.Result = true;
            import.Data = "";
            import.Message = "";
            import.Type = 0;
            import.ExistCount = 0;
            string msg = "";

            //List<CargoFactoryOrderEntity> ent = new List<CargoFactoryOrderEntity>();

            CargoFactoryOrderBus FacBus = new CargoFactoryOrderBus();

            //验证上传excel文件列数是否有效
            if (data.Columns.Count != 18)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "模板有误或缺少列，请重新下载模板";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }
            //清空table中的空行
            removeEmpty(data);


            if (data.Rows.Count <= 0)
            {
                import.Result = false;
                import.Type = 1;
                import.Message = "Excel无有效数据，请检查导入数据";
                String abnormalJson = JSON.Encode(import);
                Response.Clear();
                Response.Write(abnormalJson);
                Response.End();
                return;
            }


            //获取所有采购供应商信息
            CargoClientBus clientBus = new CargoClientBus();
            List<CargoPurchaserEntity> listPurchaser = clientBus.AutoCompletePurchaser(new CargoPurchaserEntity { DelFlag = 0 });
            Dictionary<string, int> dicPurchaser = new Dictionary<string, int>();
            foreach (var item in listPurchaser)
            {
                if (!dicPurchaser.ContainsKey(item.PurchaserName))
                {
                    dicPurchaser.Add(item.PurchaserName, Convert.ToInt32(item.PurchaserID));
                }
            }

            //获取需求部门
            List<CargoClientEntity> purDepartList = clientBus.QueryCargoClientList(new CargoClientEntity { ClientType = "4", UpClientID = 1 });
            Dictionary<string, int> dicPurDepart = new Dictionary<string, int>();
            foreach (var item in purDepartList)
            {
                if (!dicPurDepart.ContainsKey(item.ClientName))
                {
                    dicPurDepart.Add(item.ClientName, item.ClientNum);
                }
            }

            //获取所有可用品牌用于表格显示品牌
            CargoProductBasicPriceBus spescBus = new CargoProductBasicPriceBus();
            List<CargoProductTypeEntity> allSpesc = spescBus.QueryAllBrand();
            Dictionary<int, string> dicSpesc = new Dictionary<int, string>();
            foreach (var item in allSpesc)
            {
                if (!dicSpesc.ContainsKey(item.TypeID))
                {
                    dicSpesc.Add(item.TypeID, item.TypeName);
                }
            }

            //获取所有仓库用于表格显示仓库
            List<CargoProductBasicPriceEntity> allHouse = new List<CargoProductBasicPriceEntity>();
            Dictionary<string, int> dicHouse = new Dictionary<string, int>();
            foreach (var item in UserInfor.CargoList)
            {
                if (!dicHouse.ContainsKey(item.Name))
                {
                    dicHouse.Add(item.Name, item.ID);
                }
            }

            //获取所有产品来源
            CargoProductBus productBus = new CargoProductBus();
            List<CargoProductSourceEntity> allProductSourcelist = new List<CargoProductSourceEntity>();
            allProductSourcelist = productBus.QueryAllProductSource();
            Dictionary<string, int> dicScource = new Dictionary<string, int>();
            foreach (var item in allProductSourcelist)
            {
                if (!dicScource.ContainsKey(item.SourceName))
                {
                    dicScource.Add(item.SourceName, item.Source);
                }
            }
            //获取产品规格
            DataTable ProductBasicDt = new DataTable();
            ProductBasicDt = spescBus.QueryProductSpecDate(new CargoProductEntity { });

            DataTable newData = new DataTable();
            newData.Columns.Add("HouseID");
            newData.Columns.Add("HouseName");
            newData.Columns.Add("PurOrderNo");
            newData.Columns.Add("Source");
            newData.Columns.Add("SourceName");
            newData.Columns.Add("ProductName");
            newData.Columns.Add("TypeID");
            newData.Columns.Add("TypeName");
            newData.Columns.Add("Batch");
            newData.Columns.Add("Born");
            newData.Columns.Add("Model");
            newData.Columns.Add("Specs");
            newData.Columns.Add("LoadIndex");
            newData.Columns.Add("SpeedLevel");
            newData.Columns.Add("Figure");
            newData.Columns.Add("GoodsCode");
            newData.Columns.Add("ProductCode");

            newData.Columns.Add("Piece");
            newData.Columns.Add("TotalCharge");
            newData.Columns.Add("WhetherTax");
            newData.Columns.Add("Remark");
            newData.Columns.Add("PurchaserID");
            newData.Columns.Add("PurchaserName");

            newData.Columns.Add("PurchaseType");
            newData.Columns.Add("PurchaseInStoreType");
            newData.Columns.Add("ApproveID");
            newData.Columns.Add("BusinessID");
            newData.Columns.Add("GtmcNo");

            newData.Columns.Add("TID");
            newData.Columns.Add("DAID");
            newData.Columns.Add("PurDepartID");
            newData.Columns.Add("PurDepartName");

            newData.Columns.Add("DeliveryAddress");
            newData.Columns.Add("DeliveryTelephone");
            newData.Columns.Add("DeliveryBoss");
            newData.Columns.Add("DeliveryCellphone");

            newData.Columns.Add("PurchaserBoss");
            newData.Columns.Add("PurchaserAddress");
            newData.Columns.Add("PurchaserCellphone");
            newData.Columns.Add("PaymentMethod");
            newData.Columns.Add("TransferAccount");
            int abnormalCount = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                //验证Excel表格指定列是否缺少数据
                bool isContinue = false;
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    if (string.IsNullOrEmpty((data.Rows[i][j]).ToString()))
                    {
                        if (j == 13 || j == 15 || j == 16 || j == 17)
                        {

                        }
                        else
                        {
                            isContinue = true;
                            break;
                        }
                    }
                }
                if (isContinue)
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据有空值\r\n";
                    continue;
                }
                //验证导入仓库是否在数据库中有效，无效则跳过
                int HouseID = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][1]).Trim()))
                {
                    if (!dicHouse.ContainsKey(Convert.ToString(data.Rows[i][1]).Trim()))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行所属仓库不存在\r\n";
                        continue;
                    }
                    else
                    {
                        dicHouse.TryGetValue(Convert.ToString(data.Rows[i][1]).Trim(), out HouseID);
                    }
                }
                //验证导入产品来源是否在数据中有效，无效则跳过
                int Source = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][2]).Trim()))
                {
                    if (!dicScource.ContainsKey(Convert.ToString(data.Rows[i][2]).Trim()))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据产品来源不存在\r\n";
                        continue;
                    }
                    else
                    {
                        dicScource.TryGetValue(Convert.ToString(data.Rows[i][2]).Trim(), out Source);
                    }
                }

                //验证导入需求部门是否在数据中有效，无效则跳过
                int PurDepartID = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][7]).Trim()))
                {
                    if (!dicPurDepart.ContainsKey(Convert.ToString(data.Rows[i][7]).Trim()))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据需求部门不存在\r\n";
                        continue;
                    }
                    else
                    {
                        dicPurDepart.TryGetValue(Convert.ToString(data.Rows[i][7]).Trim(), out PurDepartID);
                    }
                }
                //验证导入供应商是否在数据中有效，无效则跳过
                int PurchaserID = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i][9]).Trim()))
                {
                    if (!dicPurchaser.ContainsKey(Convert.ToString(data.Rows[i][9]).Trim()))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据供应商不存在\r\n";
                        continue;
                    }
                    else
                    {
                        dicPurchaser.TryGetValue(Convert.ToString(data.Rows[i][9]).Trim(), out PurchaserID);
                    }
                }
                //验证采购数量是否为数字
                if (!IsNumber(data.Rows[i][4].ToString()))
                {
                    if (!IsNumber(data.Rows[i][4].ToString().Trim()))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据采购数量有误\r\n";
                        continue;
                    }
                    data.Rows[i][4] = data.Rows[i][4].ToString().Trim();
                }
                //验证采购价格是否为数字
                if (!string.IsNullOrEmpty(data.Rows[i][5].ToString().Trim()))
                {
                    if (!isNumeric(data.Rows[i][5].ToString()))
                    {
                        string str = data.Rows[i][5].ToString().Trim();
                        str = str.Replace("_", "");
                        str = str.Replace("*", "");
                        if (!isNumeric(str))
                        {
                            abnormalCount++;
                            msg += "第" + (i + 2) + "行数据采购价格有误\r\n";
                            continue;
                        }
                        data.Rows[i][5] = str;
                    }
                }
                else
                {
                    data.Rows[i][5] = 0;
                }
                //验证批次是否合法
                if (!IsNumber(data.Rows[i][6].ToString()))
                {
                    if (!IsNumber(data.Rows[i][6].ToString().Trim().Replace("_", "")))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据周期批次有误\r\n";
                        continue;
                    }
                    data.Rows[i][6] = data.Rows[i][6].ToString().Trim().Replace("_", "");
                }
                else if (data.Rows[i][6].ToString().Length > 4)
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据周期批次长度有误\r\n";
                    continue;

                }
                //string ProductCode = Convert.ToString(data.Rows[i][3]).Trim();
                string GoodsCode = Convert.ToString(data.Rows[i][3]).Trim();
                string Specs = string.Empty;
                string Figure = string.Empty;
                string LoadIndex = string.Empty;
                string SpeedLevel = string.Empty;
                string ProductName = string.Empty;
                string TypeID = string.Empty;
                string TypeName = string.Empty;
                string Born = string.Empty;
                string Model = string.Empty;
                string ProductCode = string.Empty;

                DataRow[] rows = ProductBasicDt.Select("GoodsCode='" + GoodsCode + "'");
                if (rows.Count() <= 0)
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据基础规格不存在\r\n";
                    continue;
                }
                else
                {
                    DataRow row = rows[0];
                    //ProductName = Convert.ToString(data.Rows[i][7]).Trim();
                    ProductName = string.IsNullOrEmpty(Convert.ToString(row[16]).Trim()) ? "" : Convert.ToString(row[16]).Trim();
                    TypeID = Convert.ToString(row[1]).Trim();
                    if (dicSpesc.ContainsKey(Convert.ToInt32(TypeID)))
                    {
                        TypeName = dicSpesc[Convert.ToInt32(TypeID)].Trim();
                    }
                    Specs = Convert.ToString(row[4]).Trim();
                    Figure = Convert.ToString(row[5]).Trim();
                    LoadIndex = Convert.ToString(row[9]).Trim();
                    SpeedLevel = Convert.ToString(row[10]).Trim().Length > 1 ? Convert.ToString(row[10]).Trim().Substring(0, 1) : Convert.ToString(row[10]).Trim();
                    Born = Convert.ToString(row[11]).Trim();
                    Model = Convert.ToString(row[2]).Trim();
                    ProductCode = Convert.ToString(row[28]).Trim();

                }

                string PaymentMethod = string.Empty;
                string TransferAccount = string.Empty;
                switch (Convert.ToString(data.Rows[i][16]))
                {
                    case "月结":
                        PaymentMethod = "0";
                        break;
                    case "周结":
                        PaymentMethod = "1";
                        break;
                    case "现结":
                        PaymentMethod = "2";
                        break;
                }
                switch (Convert.ToString(data.Rows[i][17]))
                {
                    case "对公":
                        TransferAccount = "0";
                        break;
                    case "对私":
                        TransferAccount = "1";
                        break;
                }
                int PurchaseType = 0;
                switch (Convert.ToString(data.Rows[i][10]))
                {
                    case "工厂采购单":
                        PurchaseType = 0;
                        break;
                    case "市场采购单":
                        PurchaseType = 1;
                        break;
                }
                int PurchaseInStoreType = 0;
                switch (Convert.ToString(data.Rows[i][11]))
                {
                    case "入仓单":
                        PurchaseInStoreType = 0;
                        break;
                    case "调货单":
                        PurchaseInStoreType = 1;
                        break;
                    case "提送单":
                        PurchaseInStoreType = 2;
                        break;
                }
                int ApproveID = 16;
                switch (Convert.ToString(data.Rows[i][12]))
                {
                    case "RE采购审批流程":
                        ApproveID = 16;
                        break;
                    case "OE采购审批流程":
                        ApproveID = 17;
                        break;
                }
                int WhetherTax = 0;
                switch (Convert.ToString(data.Rows[i][8]))
                {
                    case "否":
                        WhetherTax = 0;
                        break;
                    case "是":
                        WhetherTax = 1;
                        break;
                }
                int BusinessID = 0;
                switch (Convert.ToString(data.Rows[i][14]))
                {
                    case "狄乐汽服RE业务":
                        BusinessID = 12;
                        break;
                    case "狄乐汽服OE业务":
                        BusinessID = 13;
                        break;
                }

                //判断供货商地址、关联ID号  条件：市场采购+提送单

                if (PurchaseType == 0 && PurchaseInStoreType == 2)
                {
                    abnormalCount++;
                    msg += "第" + (i + 2) + "行数据工厂采购不能添加提送单\r\n";
                    continue;
                }
                //关联ID号
                string TID = string.Empty;
                //供应商地址
                string DAID = string.Empty;
                string DeliveryAddress = string.Empty;
                string DeliveryTelephone = string.Empty;
                string DeliveryBoss = string.Empty;
                string DeliveryCellphone = string.Empty;
                //提送信息
                string PurchaserBoss = string.Empty;
                string PurchaserAddress = string.Empty;
                string PurchaserCellphone = string.Empty;
                if (PurchaseType == 1 && PurchaseInStoreType == 2)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(data.Rows[i][13])))
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据关联ID号不能为空\r\n";
                        continue;
                    }
                    //查询关联信息
                    CargoGtmcProOrderEntity proOrder = FacBus.QueryExterOrderDataList(new CargoGtmcProOrderEntity { OrderStatus = "0", GoodsCode = GoodsCode, GtmcNo = Convert.ToString(data.Rows[i][13]), StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now }).FirstOrDefault();
                    if (proOrder == null)
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据关联ID号未获取到正确数据\r\n";
                        continue;
                    }
                    TID = proOrder.TID.ToString();

                    //查询供应商地址信息
                    CargoPurchaserDeliveryAddressEntity addressEntity = clientBus.QueryDeliveryAddressByPurchaserID(new CargoPurchaserDeliveryAddressEntity { PurchaserID = PurchaserID }).FirstOrDefault();
                    if (addressEntity == null)
                    {
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行供应商地址为空\r\n";
                        continue;
                    }
                    DAID = addressEntity.DAID.ToString();
                    DeliveryAddress = addressEntity.DeliveryAddress;
                    DeliveryTelephone = addressEntity.DeliveryTelephone;
                    DeliveryBoss = addressEntity.DeliveryBoss;
                    DeliveryCellphone = addressEntity.DeliveryCellphone;

                    //查询提送信息
                    CargoPurchaserEntity purchaserEntity = listPurchaser.Where(w => w.PurchaserID == PurchaserID).FirstOrDefault();
                    if (purchaserEntity != null)
                    {
                        PurchaserBoss = purchaserEntity.Boss;
                        PurchaserAddress = purchaserEntity.Address;
                        PurchaserCellphone = purchaserEntity.Cellphone;
                    }
                }



                DataRow[] NewRows = newData.Select("GoodsCode='" + GoodsCode + "' and PurOrderNo='" + Convert.ToString(data.Rows[i][0]).Trim() + "' and Source='" + Source + "'");
                DataRow drNew = newData.NewRow();
                if (NewRows.Count() <= 0)
                {
                    object[] objs = {
                            HouseID,
                            Convert.ToString(data.Rows[i][1]).Trim(),
                            Convert.ToString(data.Rows[i][0]).Trim(),
                            Source,
                            Convert.ToString(data.Rows[i][2]).Trim(),
                            ProductName,
                            TypeID,
                            TypeName,
                            data.Rows[i][6].ToString(),
                            Born,
                            Model,
                            Specs,
                            LoadIndex,
                            SpeedLevel,
                            Figure,
                            GoodsCode,
                            ProductCode,
                            Convert.ToInt32(data.Rows[i][4]),
                            Convert.ToDecimal(data.Rows[i][5]),
                            WhetherTax,
                            Convert.ToString(data.Rows[i][15]).Trim(),

                            PurchaserID,
                            Convert.ToString(data.Rows[i][9]).Trim(),
                            PurchaseType,
                            PurchaseInStoreType,
                            ApproveID,
                            BusinessID,
                            Convert.ToString(data.Rows[i][13]).Trim(),
                            TID,
                        DAID,
                        PurDepartID,
                        Convert.ToString(data.Rows[i][7]).Trim(),
                        DeliveryAddress ,
                    DeliveryTelephone ,
                    DeliveryBoss ,
                    DeliveryCellphone ,
                     PurchaserBoss ,
                    PurchaserAddress ,
                    PurchaserCellphone ,
                    PaymentMethod,
                    TransferAccount,
                };
                    drNew.ItemArray = objs;
                    newData.Rows.Add(drNew);
                }
                else
                {
                    for (int j = 0; j < NewRows.Length; j++)
                    {
                        DataRow drEmployee = NewRows[j];
                        drEmployee.BeginEdit();
                        drEmployee["Piece"] = Convert.ToInt32(NewRows[0][17]) + Convert.ToInt32(data.Rows[i][4]);
                        drEmployee.EndEdit();
                        abnormalCount++;
                        msg += "第" + (i + 2) + "行数据出现重复已自动合并\r\n";
                    }
                }
            }
            //生成采购单号
            Dictionary<string, string> dicPurOrderNo = new Dictionary<string, string>();
            for (int i = 0; i < newData.Rows.Count; i++)
            {
                string number = Convert.ToString(newData.Rows[i][2]).Trim();
                if (!dicPurOrderNo.ContainsKey(number))
                {
                    dicPurOrderNo.Add(number, Common.GetMaxPurchaseOrderNum());
                }
            }
            //赋值表格
            List<CargoRealFactoryPurchaseOrderEntityFile> entityFiles = new List<CargoRealFactoryPurchaseOrderEntityFile>();
            if (newData.Rows.Count > 0)
            {
                for (int i = 0; i < newData.Rows.Count; i++)
                {
                    CargoRealFactoryPurchaseOrderEntityFile ent = new CargoRealFactoryPurchaseOrderEntityFile();
                    ent.HouseID = Convert.ToInt32(newData.Rows[i][0]);
                    ent.HouseName = Convert.ToString(newData.Rows[i][1]).Trim();
                    ent.PurOrderNo = dicPurOrderNo[Convert.ToString(newData.Rows[i][2]).Trim()];
                    ent.Source = Convert.ToInt32(newData.Rows[i][3]);
                    ent.SourceName = Convert.ToString(newData.Rows[i][4]).Trim();
                    ent.ProductName = Convert.ToString(newData.Rows[i][5]).Trim();
                    ent.TypeID = Convert.ToInt32(newData.Rows[i][6]);
                    ent.TypeName = Convert.ToString(newData.Rows[i][7]).Trim();
                    ent.Batch = Convert.ToInt32(newData.Rows[i][8]);
                    ent.Born = Convert.ToString(newData.Rows[i][9]);
                    ent.Model = Convert.ToString(newData.Rows[i][10]).Trim();
                    ent.Specs = Convert.ToString(newData.Rows[i][11]).Trim();
                    ent.LoadIndex = Convert.ToString(newData.Rows[i][12]).Trim();
                    ent.SpeedLevel = Convert.ToString(newData.Rows[i][13]).Trim();
                    ent.Figure = Convert.ToString(newData.Rows[i][14]).Trim();
                    ent.GoodsCode = Convert.ToString(newData.Rows[i][15]).Trim();
                    ent.ProductCode = Convert.ToString(newData.Rows[i][16]).Trim();
                    ent.Piece = Convert.ToInt32(newData.Rows[i][17]);
                    ent.TotalCharge = Convert.ToDecimal(newData.Rows[i][18]);
                    ent.WhetherTax = Convert.ToInt32(newData.Rows[i][19]);
                    ent.Remark = Convert.ToString(newData.Rows[i][20]);
                    ent.PurchaserID = Convert.ToInt32(newData.Rows[i][21]);
                    ent.PurchaserName = Convert.ToString(newData.Rows[i][22]);
                    ent.PurchaseType = Convert.ToInt32(newData.Rows[i][23]);
                    ent.PurchaseInStoreType = Convert.ToInt32(newData.Rows[i][24]);
                    ent.ApproveID = Convert.ToInt32(newData.Rows[i][25]);
                    ent.BusinessID = Convert.ToInt32(newData.Rows[i][26]);
                    ent.GtmcNo = Convert.ToString(newData.Rows[i][27]);
                    ent.TID = Convert.ToString(newData.Rows[i][28]);
                    ent.DAID = Convert.ToString(newData.Rows[i][29]);
                    ent.PurDepartID = Convert.ToString(newData.Rows[i][30]);
                    ent.PurDepartName = Convert.ToString(newData.Rows[i][31]);
                    ent.DeliveryAddress = Convert.ToString(newData.Rows[i][32]);
                    ent.DeliveryTelephone = Convert.ToString(newData.Rows[i][33]);
                    ent.DeliveryBoss = Convert.ToString(newData.Rows[i][34]);
                    ent.DeliveryCellphone = Convert.ToString(newData.Rows[i][35]);
                    ent.PurchaserBoss = Convert.ToString(newData.Rows[i][36]);
                    ent.PurchaserAddress = Convert.ToString(newData.Rows[i][37]);
                    ent.PurchaserCellphone = Convert.ToString(newData.Rows[i][38]);
                    ent.PaymentMethod = Convert.ToString(newData.Rows[i][39]);
                    ent.TransferAccount = Convert.ToString(newData.Rows[i][40]);
                    entityFiles.Add(ent);
                }
            }
            String json = JSON.Encode(entityFiles);

            if (abnormalCount > 0)
            {
                import.Type = 1;
                import.Message = msg;
            }
            import.Result = true;
            import.Data = json;
            json = JSON.Encode(import);
            Response.Clear();
            Response.Write(json);
            Response.End();

        }

        /// <summary>
        /// 保存导入的数据
        /// </summary>
        public void SaveImportData()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;

            CargoFactoryOrderBus cargoFactoryOrderBus = new CargoFactoryOrderBus();
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();

            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购管理";
            log.Status = "0";
            log.NvgPage = "采购管理导入";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            try
            {
                List<CargoRealFactoryPurchaseOrderEntityFile> entityFiles = new List<CargoRealFactoryPurchaseOrderEntityFile>();
                foreach (Hashtable row in rows)
                {
                    CargoRealFactoryPurchaseOrderEntityFile ent = new CargoRealFactoryPurchaseOrderEntityFile();
                    ent.HouseID = Convert.ToInt32(row["HouseID"]);
                    ent.HouseName = Convert.ToString(row["HouseName"]).Trim();
                    ent.PurOrderNo = Convert.ToString(row["PurOrderNo"]).Trim();
                    ent.Source = Convert.ToInt32(row["Source"]);
                    ent.SourceName = Convert.ToString(row["SourceName"]).Trim();
                    ent.ProductName = Convert.ToString(row["ProductName"]).Trim();
                    ent.TypeID = Convert.ToInt32(row["TypeID"]);
                    ent.TypeName = Convert.ToString(row["TypeName"]).Trim();
                    ent.Batch = Convert.ToInt32(row["Batch"]);
                    ent.Born = Convert.ToString(row["Born"]);
                    ent.Model = Convert.ToString(row["Model"]).Trim();
                    ent.Specs = Convert.ToString(row["Specs"]).Trim();
                    ent.LoadIndex = Convert.ToString(row["LoadIndex"]).Trim();
                    ent.SpeedLevel = Convert.ToString(row["SpeedLevel"]).Trim();
                    ent.Figure = Convert.ToString(row["Figure"]).Trim();
                    ent.GoodsCode = Convert.ToString(row["GoodsCode"]).Trim();
                    ent.ProductCode = Convert.ToString(row["ProductCode"]).Trim();
                    ent.Piece = Convert.ToInt32(row["Piece"]);
                    ent.TotalCharge = Convert.ToDecimal(row["TotalCharge"]);
                    ent.WhetherTax = Convert.ToInt32(row["WhetherTax"]);
                    ent.Remark = Convert.ToString(row["Remark"]);
                    ent.PurchaserID = Convert.ToInt32(row["PurchaserID"]);
                    ent.PurchaserName = Convert.ToString(row["PurchaserName"]);
                    ent.PurchaseType = Convert.ToInt32(row["PurchaseType"]);
                    ent.PurchaseInStoreType = Convert.ToInt32(row["PurchaseInStoreType"]);
                    ent.ApproveID = Convert.ToInt32(row["ApproveID"]);
                    ent.BusinessID = Convert.ToInt32(row["BusinessID"]);
                    ent.GtmcNo = Convert.ToString(row["GtmcNo"]);
                    ent.TID = Convert.ToString(row["TID"]);
                    ent.DAID = Convert.ToString(row["DAID"]);
                    ent.PurDepartID = Convert.ToString(row["PurDepartID"]);
                    ent.PurDepartName = Convert.ToString(row["PurDepartName"]);
                    ent.DeliveryAddress = Convert.ToString(row["DeliveryAddress"]);
                    ent.DeliveryTelephone = Convert.ToString(row["DeliveryTelephone"]);
                    ent.DeliveryBoss = Convert.ToString(row["DeliveryBoss"]);
                    ent.DeliveryCellphone = Convert.ToString(row["DeliveryCellphone"]);
                    ent.PurchaserBoss = Convert.ToString(row["PurchaserBoss"]);
                    ent.PurchaserAddress = Convert.ToString(row["PurchaserAddress"]);
                    ent.PurchaserCellphone = Convert.ToString(row["PurchaserCellphone"]);
                    ent.PaymentMethod = Convert.ToString(row["PaymentMethod"]);
                    ent.TransferAccount = Convert.ToString(row["TransferAccount"]);
                    entityFiles.Add(ent);
                }
                if (entityFiles.Count == 0)
                {
                    msg.Message = "无可保存采购单数据";
                    msg.Result = false;
                    goto ERROR;
                }
                //采购单分类
                List<string> PurOrderNoList = entityFiles.Select(w => w.PurOrderNo).Distinct().ToList();

                //添加每一笔采购单
                foreach (string PurOrderNo in PurOrderNoList)
                {

                    CargoRealFactoryPurchaseOrderEntityFile orderFiles = entityFiles.Where(w => w.PurOrderNo == PurOrderNo).FirstOrDefault();
                    #region 组织采购单数据
                    CargoRealFactoryPurchaseOrderEntity ent = new CargoRealFactoryPurchaseOrderEntity();
                    List<CargoRealFactoryPurchaseHouseEntity> purchaseHouseEntities = new List<CargoRealFactoryPurchaseHouseEntity>();
                    List<CargoRealFactoryPurchaseOrderGoodsEntity> orderGoodsEntities = new List<CargoRealFactoryPurchaseOrderGoodsEntity>();
                    List<QyOrderUpdateGoodsEntity> goods = new List<QyOrderUpdateGoodsEntity>();

                    int pieceSum = 0;
                    decimal trFee = 0.00M;
                    ent.PurOrderNo = orderFiles.PurOrderNo;
                    ent.WhetherTax = Convert.ToString(orderFiles.WhetherTax);
                    ent.CheckStatus = "0";
                    ent.CreateAwbID = UserInfor.LoginName;
                    ent.CreateAwb = UserInfor.UserName;
                    ent.CreateDate = DateTime.Now;
                    ent.PurDepartID = Convert.ToInt32(orderFiles.PurDepartID);//采购部门编码
                    ent.PurDepart = orderFiles.PurDepartName;//采购部门名称
                    ent.PurchaserID = orderFiles.PurchaserID;
                    ent.DeliveryAddress = orderFiles.DeliveryAddress;
                    ent.DeliveryTelephone = orderFiles.DeliveryTelephone;
                    ent.DeliveryBoss = orderFiles.DeliveryBoss;
                    ent.DeliveryCellphone = orderFiles.DeliveryCellphone;
                    ent.PurchaseType = Convert.ToString(orderFiles.PurchaseType);//采购类型0:工厂采购单1:市场采购单
                    ent.PurchaseInStoreType = Convert.ToString(orderFiles.PurchaseInStoreType);//入库类型 0:入仓单1:调货单2:提送单
                    ent.OP_ID = UserInfor.LoginName;
                    ent.OP_DATE = DateTime.Now;
                    ent.ApplyID = UserInfor.LoginName;
                    ent.ApplyName = UserInfor.UserName;
                    ent.ApplyDate = DateTime.Now;
                    ent.ApplyStatus = orderFiles.PurchaseType.Equals(1) ? "3" : "0";
                    ent.Remark = orderFiles.Remark;
                    if (!string.IsNullOrEmpty(orderFiles.PaymentMethod)) ent.PaymentMethod = Convert.ToInt32(orderFiles.PaymentMethod);
                    if (!string.IsNullOrEmpty(orderFiles.TransferAccount)) ent.TransferAccount = Convert.ToInt32(orderFiles.TransferAccount);
                    ent.BusinessID = Convert.ToString(orderFiles.BusinessID);
                    ent.TID = string.IsNullOrEmpty(Convert.ToString(orderFiles.TID)) ? 0 : Convert.ToInt64(orderFiles.TID);
                    List<CargoGtmcProOrderEntity> gtmcProOrderEntities = new List<CargoGtmcProOrderEntity>();
                    //int HouseID = 0;
                    if (ent.PurchaseType.Equals("1") && ent.PurchaseInStoreType.Equals("2"))
                    {
                        gtmcProOrderEntities = cargoFactoryOrderBus.QueryExterOrderDataList(new CargoGtmcProOrderEntity { TID = ent.TID });
                    }

                    //同一个采购单的所有数据
                    List<CargoRealFactoryPurchaseOrderEntityFile> files = entityFiles.Where(w => w.PurOrderNo == PurOrderNo).ToList();
                    foreach (CargoRealFactoryPurchaseOrderEntityFile row in files)
                    {
                        orderGoodsEntities.Add(new CargoRealFactoryPurchaseOrderGoodsEntity
                        {
                            Piece = Convert.ToInt32(row.Piece),
                            ReplyPiece = Convert.ToInt32(row.Piece),
                            PurchasePrice = Convert.ToDecimal(row.TotalCharge),
                            TypeID = Convert.ToInt32(row.TypeID),
                            TypeName = Convert.ToString(row.TypeName),
                            HouseID = Convert.ToInt32(row.HouseID),
                            ProductCode = Convert.ToString(row.ProductCode),
                            Batch = Convert.ToString(row.Batch),
                        });

                        QyOrderUpdateGoodsEntity resGoods = new QyOrderUpdateGoodsEntity();
                        resGoods.ModifyPrice = Convert.ToDecimal(row.TotalCharge);//采购价格
                        resGoods.OrderNum = Convert.ToInt32(row.Piece);//采购数量
                        resGoods.OrderPrice = Convert.ToDecimal(row.TotalCharge);//原价格
                        resGoods.ProductCode = Convert.ToString(row.ProductCode);//产品编码
                        resGoods.HouseName = Convert.ToString(row.HouseName);//入库仓库

                        goods.Add(resGoods);
                        pieceSum += Convert.ToInt32(row.Piece);
                        trFee += (string.IsNullOrEmpty(Convert.ToString(row.TotalCharge)) ? 0 : Convert.ToDecimal(row.TotalCharge)) * Convert.ToInt32(row.Piece);
                    }

                    ent.Piece = pieceSum;
                    ent.ReplyNum = pieceSum;
                    ent.TransportFee = trFee;
                    ent.TotalCharge = ent.TransportFee + ent.OtherFee;

                    // 按 HouseID 分组并计算采购数量总和
                    var groupedData = orderGoodsEntities.GroupBy(entity => entity.HouseID).Select(group => new
                    {
                        HouseID = group.Key,
                        TotalPiece = group.Sum(entity => entity.Piece),
                    });
                    foreach (var group in groupedData)
                    {
                        purchaseHouseEntities.Add(new CargoRealFactoryPurchaseHouseEntity
                        {
                            HouseID = group.HouseID,
                            OrderNum = group.TotalPiece,
                            ReplyNum = group.TotalPiece,
                            orderGoodsEntities = orderGoodsEntities.Where(c => c.HouseID.Equals(group.HouseID)).ToList(),
                        });
                    }
                    ent.purchaseHouseEntities = purchaseHouseEntities;

                    #endregion

                    if (ent.PurchaseType.Equals("0"))
                    {
                        //工厂采购单 //提交审批 
                        string approveID = Convert.ToString(orderFiles.ApproveID);
                        if (string.IsNullOrEmpty(approveID))
                        {
                            msg.Result = false;
                            msg.Message = "请选择审批流程";
                            goto ERROR;
                        }

                        #region 推送给相关领导审批改价申请
                        CargoOrderBus orderbus = new CargoOrderBus();
                        CargoFinanceBus financeBus = new CargoFinanceBus();
                        CargoExpenseApproveRoutEntity approveRoute = new CargoExpenseApproveRoutEntity();
                        //CargoOrderEntity order = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = ent.OrderNo });
                        //推送给相关领导审批改价申请
                        QyOrderUpdatePriceEntity result = new QyOrderUpdatePriceEntity();
                        result.HouseID = 93;
                        result.ApplyID = UserInfor.LoginName;
                        result.ApplyName = UserInfor.UserName;
                        result.ApplyDate = DateTime.Now;
                        result.OrderNo = ent.PurOrderNo;
                        result.ApplyStatus = "0";
                        result.OrderType = "1";
                        //result.OrderID = order.OrderID;
                        result.OrderCheckType = "7";
                        string ApproveType = Convert.ToString(orderFiles.ApproveID);//16:RE采购审批流程 17:OE采购审批流程

                        //1.查询审批设置表获得审批流程
                        QiyeBus qiye = new QiyeBus();
                        CargoFinanceBus f = new CargoFinanceBus();
                        //CargoApproveSetEntity AppSet = f.QueryApproveSetByID(33);
                        CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
                        //审批流程
                        List<CargoApproveSetEntity> aset = f.QueryApproveSet(new CargoApproveSetEntity { ApproveType = ApproveType, DelFlag = "0", HouseID = 93 });
                        if (aset.Count > 0) { AppSet = aset[0]; }
                        else
                        {
                            aset = f.QueryApproveSet(new CargoApproveSetEntity { ApproveType = "16", DelFlag = "0", HouseID = 93 });
                            if (aset.Count > 0) { AppSet = aset[0]; }
                        }
                        List<QyUserEntity> qyUser = new List<QyUserEntity>();
                        //2.审批流程的每一级和当前人的审批角色相匹配
                        if (AppSet.OneCheckID.Equals("3"))
                        {
                            SystemUserEntity entity = new SystemUserEntity();
                            entity.LoginName = UserInfor.LoginName;
                            SystemUserEntity bossLeader = orderbus.QueryBossLeaderByLoginName(entity);
                            if (bossLeader != null)
                            {
                                //申请人有部门负责人
                                if (!string.IsNullOrEmpty(bossLeader.LoginName))
                                {
                                    if (bossLeader.LoginName != UserInfor.LoginName)
                                    {
                                        qyUser.Add(new QyUserEntity { UserID = bossLeader.LoginName, WxName = bossLeader.UserName });
                                        result.CheckID = AppSet.OneCheckID;
                                        result.CheckName = AppSet.OneCheckName;
                                    }
                                    //申请人部门负责人是自己，推送给下一级
                                    else
                                    {
                                        qyUser = qiye.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = "93" });
                                        result.CheckID = AppSet.TwoCheckID;
                                        result.CheckName = AppSet.TwoCheckName;
                                    }
                                }
                                //申请人没有部门负责人，推送给下一级
                                else
                                {
                                    qyUser = qiye.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = "93" });
                                    result.CheckID = AppSet.TwoCheckID;
                                    result.CheckName = AppSet.TwoCheckName;
                                }
                            }
                            //申请人信息跳过操作记录错误
                            else
                            {
                                Common.WriteTextLog("推送失败：申请人未查询到数据");
                                msg.Result = true;
                                return;
                            }
                        }
                        else
                        {
                            qyUser = qiye.QueryUserList(new QyUserEntity { CheckRole = AppSet.OneCheckID, CheckHouseID = "93" });
                            result.CheckID = AppSet.OneCheckID;
                            result.CheckName = AppSet.OneCheckName;
                        }

                        ent.NextCheckID = result.CheckID;
                        ent.NextCheckName = result.CheckName;

                        result.Reason = ent.Remark;
                        result.SaleManName = UserInfor.UserName;
                        result.SaleManID = UserInfor.LoginName;
                        result.UpdatePriceGoodsList = goods;
                        long OID = qiye.AddOrderUpdatePrice(result, log);
                        approveRoute.ExID = OID;
                        approveRoute.UserID = Convert.ToString(UserInfor.UserID);
                        approveRoute.UserName = UserInfor.UserName;
                        approveRoute.Opera = "3";
                        approveRoute.Result = ent.Remark;
                        approveRoute.ApproveType = ApproveType;//16:RE采购审批流程 17:OE采购审批流程
                        financeBus.AddExpenseApproveRout(approveRoute);
                        //try
                        //{
                        //    string PurchaseType = ent.PurchaseType.Equals("0") ? "工厂采购单" : "市场采购单";
                        //    string PurchaserName = Convert.ToString(Request["PurchaserName"]);
                        //    QySendInfoEntity send = new QySendInfoEntity();
                        //    send.title = "采购单申请";
                        //    send.msgType = msgType.textcard;
                        //    //send.toTag = strTag;
                        //    send.content = "<div></div><div>采购单号：" + ent.PurOrderNo + "</div><div>采购类型：" + PurchaseType + "</div><div>供应商：" + PurchaserName + "</div><div>采购数量：" + ent.Piece.ToString() + "  " + ent.TotalCharge.ToString("F2") + "</div><div>申请人：" + result.ApplyName + "</div><div>申请时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</div><div>申请原因：" + result.Reason + "</div><div></div><div class=\"highlight\">请点击本通知进行审批！</div>";
                        //    send.url = "http://dlt.neway5.com/QY/qyApprovePurchaseCheck.aspx?OrderNo=" + ent.PurOrderNo + "&OID=" + OID + "&OrderType=1&OrderCheckType=7&ty=0&ApproveType=" + ApproveType;
                        //    foreach (var it in qyUser)
                        //    {
                        //        send.toUser = it.UserID;
                        //        WxQYSendHelper.PushInfo("0", "2", send);
                        //        Common.WriteTextLog("推送成功：" + it.WxName + ent.PurOrderNo);
                        //    }

                        //}
                        //catch (ApplicationException ex)
                        //{
                        //    Common.WriteTextLog("推送失败：" + ent.PurOrderNo);
                        //    msg.Result = true;
                        //}

                        #endregion

                    }

                    //保存采购单
                    bus.AddRealPurchaseOrder(ent, log);

                    CargoHouseBus house = new CargoHouseBus();
                    CargoClientBus clientBus = new CargoClientBus();
                    CargoProductBus productBus = new CargoProductBus();
                    CargoOrderBus OrderBus = new CargoOrderBus();
                    if (ent.PurchaseType.Equals("1") && ent.PurchaseInStoreType.Equals("2"))
                    {
                        //多仓库添加订单
                        foreach (var pHouse in purchaseHouseEntities)
                        {
                            //市场采购单并且是提送单
                            //根据ent.TID查询客户的一级仓库，订单信息
                            //系统自动生成订单、产品、入库、关联销售订单号
                            List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();

                            CargoClientEntity clientEntity = clientBus.QueryCargoClient(new CargoClientEntity { ShopCode = gtmcProOrderEntities[0].SourceCode, HouseIDStr = "65" });

                            CargoOrderEntity cargoOrder = new CargoOrderEntity();
                            cargoOrder.BusinessID = gtmcProOrderEntities[0].BusinessID;
                            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(pHouse.HouseID);

                            cargoOrder.ShopCode = gtmcProOrderEntities[0].SourceCode;
                            cargoOrder.HAwbNo = gtmcProOrderEntities[0].GtmcNo;

                            cargoOrder.Dep = Convert.ToString(houseEnt.DepCity);
                            cargoOrder.Dest = gtmcProOrderEntities[0].City;
                            cargoOrder.InsuranceFee = cargoOrder.TransitFee = cargoOrder.DeliveryFee = cargoOrder.OtherFee = cargoOrder.Rebate = 0;
                            cargoOrder.HouseID = pHouse.HouseID;
                            //cargoOrder.CheckOutType = Convert.ToString(orderFiles.CheckOutType);
                            cargoOrder.CheckOutType = "";
                            cargoOrder.TrafficType = "0";
                            cargoOrder.DeliveryType = "2";
                            cargoOrder.AcceptAddress = clientEntity.Address;
                            cargoOrder.AcceptPeople = clientEntity.Boss;

                            cargoOrder.IsPrintPrice = 0;
                            cargoOrder.AcceptUnit = clientEntity.ClientName;
                            cargoOrder.AcceptTelephone = clientEntity.Telephone;
                            cargoOrder.AcceptCellphone = clientEntity.Cellphone;
                            cargoOrder.CreateAwb = UserInfor.UserName;
                            cargoOrder.CreateAwbID = UserInfor.LoginName;
                            cargoOrder.CreateDate = DateTime.Now;// Convert.ToDateTime(Request["CreateDate"]);
                            cargoOrder.OP_ID = UserInfor.LoginName.Trim();
                            cargoOrder.SaleManID = clientEntity.UserID;
                            cargoOrder.SaleManName = clientEntity.UserName;
                            //cargoOrder.SaleCellPhone = Convert.ToString(Request["SaleCellPhone"]);
                            cargoOrder.Remark = ent.Remark + " " + clientEntity.ShopCode;
                            cargoOrder.ThrowGood = "12";
                            cargoOrder.LogisID = clientEntity.LogisLineLogisID;
                            cargoOrder.ClientNum = clientEntity.ClientNum;
                            cargoOrder.PayClientNum = clientEntity.ClientNum;
                            cargoOrder.PayClientName = clientEntity.ClientName;
                            cargoOrder.PurchaseHouseID = pHouse.HouseID;
                            cargoOrder.PurchaserID = Convert.ToString(orderFiles.PurchaserID);
                            cargoOrder.PurchaserName = Convert.ToString(orderFiles.PurchaserName);
                            cargoOrder.CheckStatus = "0";
                            decimal PretrFee = 0.00M;
                            int PrepieceSum = 0;
                            string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号

                            //CargoAreaEntity cargoAreaEntity = house.QueryAreaByAreaID(new CargoAreaEntity { AreaID = gtmcProOrderEntities[0].FirstLevelAreaID });

                            int OrderNum = 0;
                            cargoOrder.OrderNo = Common.GetMaxOrderNumByCurrentDate(pHouse.HouseID, houseEnt.HouseCode, out OrderNum);
                            cargoOrder.OrderNum = OrderNum;//最新订单顺序号

                            List<CargoProductEntity> productList = new List<CargoProductEntity>();
                            foreach (CargoRealFactoryPurchaseOrderEntityFile row in files)
                            {
                                if (Convert.ToInt32(row.HouseID) != pHouse.HouseID)
                                {
                                    continue;
                                }

                                List<CargoProductTypeEntity> pType = productBus.QueryProductType(new CargoProductTypeEntity { TypeID = Convert.ToInt32(row.TypeID), ParentID = -1 });
                                //根据产品规格信息查询价格
                                string proYear, proMonth;
                                string BelongMonth = DateTime.Now.ToString("yyyyMM");
                                proYear = BelongMonth.Substring(0, 4);
                                proMonth = BelongMonth.Substring(4, 2);
                                CargoProductBasicPriceEntity basicPriceEntity = new CargoProductBasicPriceEntity();
                                basicPriceEntity.ProductCode = Convert.ToString(row.ProductCode);
                                basicPriceEntity.GoodsCode = Convert.ToString(row.GoodsCode);
                                basicPriceEntity.ProYear = proYear;
                                basicPriceEntity.ProMonth = proMonth;
                                basicPriceEntity.HouseID = pHouse.HouseID;
                                basicPriceEntity.TypeID = pType.Count > 0 ? -1 : pType[0].TypeID;
                                basicPriceEntity.Born = -1;
                                double OESalePrice = 0;
                                CargoProductBasicPriceEntity basicPrice = house.QueryProductBasicPrice(basicPriceEntity);
                                if (basicPrice != null && basicPrice.PID != 0)
                                {
                                    OESalePrice = basicPrice.OESalePrice;
                                }

                                if (pType.Count > 0)
                                {
                                    CargoContainerEntity containerEntity = house.QueryTopOneContainer(new CargoContainerEntity { HouseID = pHouse.HouseID });
                                    CargoProductEntity entProduct = new CargoProductEntity();
                                    entProduct.ParentID = pType[0].ParentID;
                                    entProduct.ProductName = house.QueryCargoHouse(new CargoHouseEntity { HouseID = pHouse.HouseID }).CargoDepart;
                                    entProduct.GoodsName = Convert.ToString(row.ProductName);//产品名称
                                    entProduct.TypeID = pType[0].TypeID;
                                    entProduct.TypeName = pType[0].TypeName;
                                    entProduct.Model = Convert.ToString(row.Model);
                                    entProduct.GoodsCode = Convert.ToString(row.GoodsCode);
                                    entProduct.Figure = Convert.ToString(row.Figure);
                                    entProduct.Specs = Convert.ToString(row.Specs);
                                    entProduct.Batch = Convert.ToString(row.Batch);
                                    entProduct.TreadWidth = 0;
                                    entProduct.FlatRatio = 0;
                                    entProduct.Meridian = "R";
                                    entProduct.HubDiameter = 0;
                                    entProduct.SpeedLevel = Convert.ToString(row.SpeedLevel);
                                    entProduct.LoadIndex = Convert.ToString(row.LoadIndex);
                                    entProduct.UnitPrice = Convert.ToDecimal(row.TotalCharge);
                                    entProduct.CostPrice = Convert.ToDecimal(row.TotalCharge);
                                    entProduct.TaxCostPrice = Convert.ToDecimal(row.TotalCharge);
                                    entProduct.NoTaxCostPrice = Convert.ToDecimal(row.TotalCharge);
                                    entProduct.Numbers = Convert.ToInt32(row.Piece);
                                    entProduct.SalePrice = OESalePrice.Equals(0) ? 1 : Convert.ToDecimal(OESalePrice);
                                    entProduct.TradePrice = 0;
                                    entProduct.PackageNum = 0;
                                    entProduct.PackageWeight = 0;
                                    entProduct.Source = "9";
                                    entProduct.Born = Convert.ToString(row.Born);
                                    entProduct.BelongDepart = "1";
                                    entProduct.HouseID = pHouse.HouseID;
                                    entProduct.Company = "1";
                                    entProduct.AreaID = containerEntity.AreaID;
                                    entProduct.ContainerID = containerEntity.ContainerID;
                                    entProduct.ContainerCode = containerEntity.ContainerCode;
                                    entProduct.InCargoID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//入库单号
                                    entProduct.BelongMonth = DateTime.Now.ToString("yyyyMM");
                                    entProduct.SourceOrderNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    entProduct.Assort = "OER";
                                    entProduct.InHouseType = "0";
                                    entProduct.OrderNo = cargoOrder.OrderNo;
                                    entProduct.OPID = UserInfor.LoginName;
                                    entProduct.Supplier = "狄乐汽服OE";
                                    entProduct.SupplierAddress = "广东广州白云区东平横岗东街好来运B栋3楼";
                                    entProduct.SuppClientNum = 551098;
                                    entProduct.ProductCode = Convert.ToString(row.ProductCode);

                                    entProduct.PurchaserID = Convert.ToInt32(orderFiles.PurchaserID);
                                    entProduct.PurchaserName = Convert.ToString(orderFiles.PurchaserName);
                                    entProduct.DeliveryBoss = Convert.ToString(orderFiles.DeliveryBoss);
                                    entProduct.DeliveryAddress = Convert.ToString(orderFiles.DeliveryAddress);

                                    cargoOrder.PurchaserBoss = Convert.ToString(orderFiles.PurchaserBoss);
                                    cargoOrder.PurchaserAddress = Convert.ToString(orderFiles.PurchaserAddress);
                                    cargoOrder.PurchaserCellphone = Convert.ToString(orderFiles.PurchaserCellphone);
                                    productList.Add(entProduct);
                                }

                                #region 订单详情

                                entDest.Add(new CargoOrderGoodsEntity
                                {
                                    OrderNo = cargoOrder.OrderNo,
                                    HouseID = pHouse.HouseID,
                                    TypeID = Convert.ToInt32(row.TypeID),
                                    Piece = Convert.ToInt32(row.Piece),
                                    ActSalePrice = OESalePrice.Equals(0) ? 1 : Convert.ToDecimal(OESalePrice),//Convert.ToDecimal(row["ActSalePrice"]),实际销售价
                                    Specs = Convert.ToString(row.Specs),
                                    Figure = Convert.ToString(row.Figure),
                                    GoodsCode = Convert.ToString(row.GoodsCode),
                                    Batch = Convert.ToString(row.Batch),
                                    LoadIndex = Convert.ToString(row.LoadIndex),
                                    SpeedLevel = Convert.ToString(row.SpeedLevel),
                                    Model = Convert.ToString(row.Model),
                                    Born = "0",
                                    CostPrice = Convert.ToDecimal(row.TotalCharge),
                                    PurchaserID = Convert.ToInt32(orderFiles.PurchaserID),
                                    PurchaserName = Convert.ToString(orderFiles.PurchaserName),
                                    DeliveryBoss = Convert.ToString(orderFiles.DeliveryBoss),
                                    DeliveryAddress = Convert.ToString(orderFiles.DeliveryAddress),
                                    DeliveryCellphone = Convert.ToString(orderFiles.DeliveryCellphone),
                                    OP_ID = UserInfor.LoginName.Trim()
                                });
                                PrepieceSum += Convert.ToInt32(row.Piece);
                                PretrFee += OESalePrice.Equals(0) ? 1 : Convert.ToDecimal(OESalePrice) * Convert.ToInt32(row.Piece);


                                #endregion
                            }
                            cargoOrder.Piece = PrepieceSum;
                            cargoOrder.OutHouseName = houseEnt.Name;
                            cargoOrder.TransportFee = PretrFee;
                            cargoOrder.TotalCharge = cargoOrder.TransportFee + cargoOrder.TransitFee + cargoOrder.OtherFee - cargoOrder.InsuranceFee;

                            cargoOrder.IsMakeSure = 0;
                            cargoOrder.PostponeShip = "1";
                            cargoOrder.AwbStatus = "0";
                            cargoOrder.OrderType = "0";
                            cargoOrder.HouseID = pHouse.HouseID;
                            cargoOrder.HouseName = houseEnt.Name;
                            cargoOrder.goodsList = entDest;
                            cargoOrder.productsList = productList;
                            cargoOrder.ModifyPriceStatus = "0";
                            cargoOrder.OutCargoID = outID;

                            OrderBus.AddPreOrderInfo(cargoOrder, new List<CargoContainerShowEntity>(), log);

                            cargoFactoryOrderBus.UpdateGtmcProOrderStatus(new CargoGtmcProOrderEntity { TID = ent.TID, OrderNo = cargoOrder.OrderNo, OrderStatus = "1", AutoOrderHandleStatus = "1", OrderHandleType = "0", AutoOrderHandleTime = DateTime.Now, OrderAlloType = "2" });
                            List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
                            OrderBus.InsertCargoOrderPush(new CargoOrderPushEntity
                            {
                                OrderNo = cargoOrder.OrderNo,
                                Dep = cargoOrder.Dep,
                                Dest = cargoOrder.Dest,
                                Piece = cargoOrder.Piece,
                                TransportFee = cargoOrder.TransportFee,
                                ClientNum = cargoOrder.ClientNum.ToString(),
                                AcceptAddress = cargoOrder.AcceptAddress,
                                AcceptCellphone = cargoOrder.AcceptCellphone,
                                AcceptTelephone = cargoOrder.AcceptTelephone,
                                AcceptPeople = cargoOrder.AcceptPeople,
                                AcceptUnit = cargoOrder.AcceptUnit,
                                HouseID = cargoOrder.HouseID.ToString(),
                                HouseName = house.QueryCargoHouseByID(cargoOrder.HouseID).Name,// orderEnt.HouseID.Equals(65) || orderEnt.HouseID.Equals(84) ? houseEnt.Name : orderEnt.OutHouseName,
                                OP_ID = UserInfor.UserName,
                                HLYSendUnit = cargoOrder.HouseID.Equals(65) ? productList[0].ProductName : productList[0].ProductName,
                                PushType = "0",
                                PushStatus = "0",
                                LogisID = cargoOrder.LogisID,
                                BusinessID = cargoOrder.BusinessID
                            }, log);
                        }


                    }

                    if (ent.PurchaseType.Equals("1") && (ent.PurchaseInStoreType.Equals("0") || ent.PurchaseInStoreType.Equals("1")))
                    {

                        //市场采购单  入仓单
                        foreach (var pur in purchaseHouseEntities)
                        {
                            List<CargoFactoryOrderEntity> factoryList = new List<CargoFactoryOrderEntity>();
                            List<CargoProductEntity> productList = new List<CargoProductEntity>();
                            CargoContainerEntity containerEntity = house.QueryTopOneContainer(new CargoContainerEntity { HouseID = pur.HouseID });
                            CargoHouseEntity che = house.QueryCargoHouse(new CargoHouseEntity { HouseID = pur.HouseID });
                            foreach (var gent in pur.orderGoodsEntities)
                            {
                                string batch = gent.Batch;
                                int bYear = 0, bWeek = 0;
                                if (batch.Length == 4)
                                {
                                    bWeek = Convert.ToInt32(batch.Substring(0, 2));
                                    bYear = Convert.ToInt32(batch.Substring(2, 2));
                                }
                                else if (batch.Length == 3)
                                {
                                    bWeek = Convert.ToInt32(batch.Substring(0, 1));
                                    bYear = Convert.ToInt32(batch.Substring(1, 2));
                                }
                                CargoProductSpecEntity cpse = productBus.GetProductSpecByProductCode(gent.ProductCode);
                                factoryList.Add(new CargoFactoryOrderEntity
                                {
                                    HouseID = pur.HouseID,//到仓仓库
                                    FacOrderNo = ent.PurOrderNo,//以采购单号做来货单号
                                    Source = 9,
                                    SourceName = "市场采购",
                                    ProductName = che.CargoDepart,
                                    GoodsName = cpse.ProductName,
                                    TypeID = gent.TypeID,
                                    TypeName = gent.TypeName,
                                    OrderType = Convert.ToInt32(ent.PurchaseInStoreType),
                                    Model = cpse.Model,
                                    BelongMonth = DateTime.Now.ToString("yyyyMM"),
                                    Born = "0",
                                    Assort = cpse.Assort,
                                    Specs = cpse.Specs,
                                    LoadIndex = cpse.LoadIndex,
                                    SpeedLevel = cpse.SpeedLevel,
                                    Figure = cpse.Figure,
                                    GoodsCode = cpse.GoodsCode,
                                    ProductCode = cpse.ProductCode,
                                    ProductUnit = cpse.ProductUnit,
                                    BundleNum = cpse.BundleNum,
                                    HubDiameter = cpse.HubDiameter,
                                    Batch = batch,
                                    BatchWeek = bWeek,
                                    BatchYear = bYear,
                                    OrderNum = gent.Piece,
                                    ReplyNumber = gent.ReplyPiece,
                                    InPiece = ent.PurchaseInStoreType.Equals("1") ? gent.Piece : 0,
                                    InCargoStatus = ent.PurchaseInStoreType.Equals("1") ? 1 : 0,
                                    SalePrice = 0,
                                    UnitPrice = Convert.ToDouble(gent.PurchasePrice),
                                    CostPrice = Convert.ToDouble(gent.PurchasePrice),
                                    TaxCostPrice = Convert.ToDouble(gent.PurchasePrice),
                                    NoTaxCostPrice = Convert.ToDouble(gent.PurchasePrice),
                                    TradePrice = 0,
                                    WhetherTax = Convert.ToInt32(Request["WhetherTax"]),
                                    SaleMoney = 0,
                                    ReceiveName = che.Name,
                                    ReceiveCity = che.DepCity,
                                    ReceiveMobile = "",
                                    SourceHouse = "",
                                    SpecsType = "4",
                                    OP_Name = UserInfor.UserName,
                                    BelongDepart = "1",
                                    Company = "0",
                                    ContainerID = containerEntity.ContainerID,
                                    InCargoID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString(),
                                    Supplier = ent.PurDepart,
                                    SuppClientNum = Convert.ToString(ent.PurDepartID),
                                    SupplierAddress = "",
                                    BusinessID = Convert.ToString(Request["BusinessID"]),
                                    PushStatus = "0"
                                });

                                productList.Add(new CargoProductEntity
                                {
                                    TypeID = gent.TypeID,
                                    InPiece = gent.Piece,
                                    Specs = cpse.Specs,
                                    Figure = cpse.Figure,
                                    Model = cpse.Model,
                                    GoodsCode = cpse.GoodsCode,
                                    ProductCode = cpse.ProductCode,
                                    LoadIndex = cpse.LoadIndex,
                                    SpeedLevel = cpse.SpeedLevel,
                                    Born = "0",
                                    Batch = gent.Batch,
                                    UnitPrice = gent.PurchasePrice,
                                    CostPrice = gent.PurchasePrice,
                                    TaxCostPrice = gent.PurchasePrice,
                                    NoTaxCostPrice = gent.PurchasePrice,
                                    TradePrice = 1,
                                    SalePrice = 1,
                                    OPID = UserInfor.LoginName.ToString(),
                                    PackageNum = cpse.BundleNum,
                                    Package = cpse.ProductUnit,
                                    SuppClientNum = ent.PurDepartID,
                                    Supplier = ent.PurDepart,
                                    GoodsName = cpse.ProductName,
                                });
                            }
                            bus.AddPurchaseOrderInfo(ent, productList, factoryList, log);
                        }
                    }

                }
                if (msg.Result)
                {
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
        ERROR:
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
        }

        /// <summary>
        /// 删除导入的table中的空行
        /// </summary>
        /// <param name="dt"></param>
        protected void removeEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowdataisnull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {

                        rowdataisnull = false;
                    }
                }
                if (rowdataisnull)
                {
                    removelist.Add(dt.Rows[i]);
                }

            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }
        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }
        /// <summary>
        /// 判断字符串是否为整数或小数
        /// </summary>
        /// <param name="value">验证字符串</param>
        /// <returns></returns>
        public static bool isNumeric(String value)
        {
            return Regex.IsMatch(value, "^(\\-|\\+)?\\d+(\\.\\d+)?$");
        }

        #region 采购单审批 

        /// <summary>
        /// 判断是否可进行审批
        /// </summary>
        public void IsApprovalProcess()
        {

            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            //CargoRealFactoryPurchaseHouseEntity ent = new CargoRealFactoryPurchaseHouseEntity();
            //CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";

            try
            {
                //CargoRealFactoryPurchaseHouseEntity ent = new CargoRealFactoryPurchaseHouseEntity();
                //ent.PurOrderNo = Request["PurOrderNo"];
                //string applyID = Request["CreateAwbID"];
                //string applyName = Request["CreateAwb"];

                //CargoFinanceBus financeBus = new CargoFinanceBus();
                ////查询当前订单的审批路径
                //List<CargoExpenseApproveRoutEntity> finance = financeBus.QueryExpenseRoutAtOrderNo(new CargoOrderEntity { OrderNo = Convert.ToString(Request["PurOrderNo"]) });
                ////查询当前订单的审批流程
                //CargoApproveSetEntity approveSet = financeBus.QueryApproveSet(new CargoApproveSetEntity { ApproveType = finance.FirstOrDefault().ApproveType, DelFlag = "0" }).FirstOrDefault();
                //List<string> CheckIDList = new List<string>();
                ////映射
                //Type type = typeof(CargoApproveSetEntity);
                //foreach (PropertyInfo property in type.GetProperties())
                //{
                //    // 如果属性名包含 "CheckID"
                //    if (property.Name.Contains("CheckID"))
                //    {
                //        // 获取属性值
                //        string value = property.GetValue(approveSet, null) as string;
                //        // 如果值不为 null 或空字符串，则添加到列表中
                //        if (!string.IsNullOrEmpty(value))
                //        {
                //            CheckIDList.Add(value);
                //        }
                //    }
                //}

                //QiyeBus qiye = new QiyeBus();
                ////查询登录人的审批角色
                //QyUserEntity user = qiye.QueryUser(new QyUserEntity { UserID = UserInfor.LoginName.Trim() });
                //user.CheckRole = "3";
                //if (string.IsNullOrEmpty(user.CheckRole) || !CheckIDList.Any(w => w == user.CheckRole))
                //{
                //    msg.Result = false;
                //    msg.Message = "无审批权限";
                //    goto ERROR;
                //}

                //if (finance.FirstOrDefault().Opera != user.CheckRole)
                //{
                //    msg.Result = false;
                //    msg.Message = "非当前可审批人";
                //    goto ERROR;
                //}

            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
        ERROR:
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        public void IsApprovalProcessRevoked()
        {

            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoRealFactoryPurchaseOrderEntity ent = new CargoRealFactoryPurchaseOrderEntity();
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";

            try
            {
                ent.PurOrderNo = Request["PurOrderNo"];
                var list = bus.QueryCargoRealFactoryData(ent);

                msg.Result = list.Count == 0;
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //JSON
            String json = JSON.Encode(msg);
            Response.Clear();
            Response.Write(json);
        }

        ///// <summary>
        ///// 审批操作
        ///// </summary>
        public void UpdateApprovalProcessNo()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            //CargoRealFactoryPurchaseHouseEntity ent = new CargoRealFactoryPurchaseHouseEntity();
            //CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";

            try
            {

                CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
                CargoFactoryOrderBus busF = new CargoFactoryOrderBus();
                CargoHouseBus house = new CargoHouseBus();

                //添加来货导入信息
                CargoRealFactoryPurchaseOrderGoodsEntity queryEntity = new CargoRealFactoryPurchaseOrderGoodsEntity();
                queryEntity.PurOrderID = Convert.ToInt64(Request["PurOrderID"]);
                List<CargoRealFactoryPurchaseOrderGoodsEntity> list = bus.cargoRealFactoryPurchaseOrderGoodsFactory(queryEntity);

                foreach (var item in list)
                {
                    CargoFactoryOrderEntity ent = new CargoFactoryOrderEntity();
                    ent.HouseID = item.HouseID;//到仓仓库
                    ent.FacOrderNo = item.PurOrderNo;//以采购单号做来货单号
                    ent.Source = 100;
                    ent.SourceName = "工厂采购";
                    ent.ProductName = item.ProductName;
                    ent.TypeID = item.TypeID;
                    ent.TypeName = item.TypeName;
                    ent.OrderType = 0;

                    ent.OrderType = item.PurchaseInStoreType;  //订单类型 0内部订单  1外部订单

                    ent.BelongMonth = DateTime.Now.ToString("yyyyMM");
                    ent.Batch = DateTime.Now.Year.ToString().Substring(2, 2);//2025 取 25


                    ent.Model = item.Model;
                    ent.Specs = item.Specs;
                    ent.LoadIndex = item.LoadIndex;
                    ent.SpeedLevel = item.SpeedLevel;
                    ent.Figure = item.Figure;
                    ent.GoodsCode = item.GoodsCode;
                    ent.ProductCode = item.ProductCode;
                    ent.OrderNum = item.Piece;
                    ent.ReplyNumber = item.Piece;
                    ent.Supplier = item.Supplier;
                    ent.SuppClientNum = item.SuppClientNum;
                    ent.SupplierAddress = item.SupplierAddress;
                    ent.UnitPrice = Convert.ToDouble(item.PurchasePrice);
                    ent.WhetherTax = item.WhetherTax;
                    ent.ReceiveName = item.ReceiveName;
                    ent.ReceiveCity = item.ReceiveCity;
                    ent.ReceiveMobile = item.ReceiveMobile;

                    ent.Born = item.Born;//0国产1进口

                    ent.Assort = item.SuppClientNum == "551098" ? "OER" : "REP";//OE  RE
                    ent.BelongDepart = item.SuppClientNum == "551098" ? "1" : "0";//0:RE渠道销售部1:OE渠道销售部

                    ent.SpecsType = "4";
                    ent.Company = "0";

                    ent.OP_Name = UserInfor.UserName.Trim();

                    //价格查询
                    string proYear, proMonth;
                    proYear = ent.BelongMonth.Substring(0, 4);
                    proMonth = ent.BelongMonth.Substring(4, 2);
                    CargoProductBasicPriceEntity basicPriceEntity = new CargoProductBasicPriceEntity();
                    basicPriceEntity.ProductCode = item.ProductCode;
                    basicPriceEntity.ProYear = proYear;
                    basicPriceEntity.ProMonth = proMonth;
                    basicPriceEntity.HouseID = item.HouseID;
                    basicPriceEntity.TypeID = item.TypeID;
                    basicPriceEntity.Born = -1;
                    CargoProductBasicPriceEntity basicPrice = house.QueryProductBasicPrice(basicPriceEntity);
                    if (basicPrice != null && basicPrice.PID != 0)
                    {
                        if (!basicPrice.TradePrice.Equals(0))
                        {
                            ent.TradePrice = Convert.ToDouble(basicPrice.TradePrice);
                            ent.SalePrice = Convert.ToDouble(basicPrice.SalePrice);
                        }

                    }

                    //外部订单
                    if (item.PurchaseInStoreType.Equals(1))
                    {


                        ent.ProductID = "";
                    }

                    //添加来货导入信息
                    busF.InsertFactoryOrderData(ent, log);
                }

                //更改审批状态
                bus.UpdatePurchaseOrderApproval(new CargoRealFactoryPurchaseOrderEntity { ApplyStatus = "3", CheckResult = Request["CheckResult"], PurOrderID = Convert.ToInt64(Request["PurOrderID"]) }, log);

            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        public void RevokedApprovalProcessNo()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            //CargoRealFactoryPurchaseHouseEntity ent = new CargoRealFactoryPurchaseHouseEntity();
            //CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";

            try
            {
                String json = Request["data"];
                if (String.IsNullOrEmpty(json)) return;
                CargoRealFactoryPurchaseOrderGoodsEntity rows = JsonConvert.DeserializeObject<CargoRealFactoryPurchaseOrderGoodsEntity>(json);

                CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
                CargoFactoryOrderBus busF = new CargoFactoryOrderBus();
                CargoHouseBus house = new CargoHouseBus();

                CargoRealFactoryPurchaseOrderGoodsEntity queryEntity = new CargoRealFactoryPurchaseOrderGoodsEntity();
                queryEntity.PurOrderID = Convert.ToInt64(Convert.ToString(rows.PurOrderID));
                //更改审批状态
                bus.UpdatePurchaseOrderApproval(new CargoRealFactoryPurchaseOrderEntity { ApplyStatus = "0", CheckResult = "订单撤销审批", PurOrderID = (queryEntity.PurOrderID) }, log);

            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }

        /// <summary>
        /// 通过审批
        /// </summary>
        public void PurchaseCheckOk()
        {
            string Reason = Convert.ToString(Request["Reason"]);
            string OrderNo = Convert.ToString(Request["OrderNo"]);//订单号，商城订单号和电脑端订单号
            long OID = Convert.ToInt64(Request["OID"]);//修改表主键ID
            string Name = Convert.ToString(Request["Name"]);//收货人姓名 
            string Cellphone = Convert.ToString(Request["Cellphone"]);
            string OrderType = Convert.ToString(Request["OrderType"]);//订单类型0：商城订单1：电脑订单
            QyOrderUpdatePriceEntity result = new QyOrderUpdatePriceEntity();
            ErrMessage msg = new ErrMessage(); msg.Message = ""; msg.Result = true;
            result.HouseID = UserInfor.HouseID;
            result.CheckID = UserInfor.LoginName;
            result.CheckName = UserInfor.UserName;
            result.CheckTime = DateTime.Now;
            result.OrderNo = OrderNo;
            result.CheckResult = Reason;
            result.OID = OID;
            List<CargoExpenseApproveRoutEntity> approveRouteList = new List<CargoExpenseApproveRoutEntity>();
            CargoExpenseApproveRoutEntity approveRoute = new CargoExpenseApproveRoutEntity();
            approveRoute.ExID = OID;
            approveRoute.UserID = UserInfor.LoginName;
            approveRoute.UserName = UserInfor.UserName;
            approveRoute.ApproveType = "1";
            approveRoute.Result = Reason;
            approveRoute.Opera = "0";
            approveRouteList.Add(approveRoute);
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "报销审批";
            log.NvgPage = "修改价格审批";
            log.UserID = UserInfor.LoginName;
            log.Status = "0";
            log.Operate = "U";
            QiyeBus q = new QiyeBus();
            QyOrderUpdatePriceEntity qup = q.QueryOrderUpdatePriceEntity(result);
            //if (qup.ApplyStatus.Equals("3"))
            //{
            //    msg.Result = false;
            //    msg.Message = "该订单已审批结束，请勿重复审批";
            //}
            //if (!UserInfor.CheckRole.Contains(qup.CheckID))
            //{
            //    msg.Result = false;
            //    msg.Message = "流程未到您审批";
            //}
            bool IsEnd = false;
            if (msg.Result)
            {
                string copy = string.Empty;
                //1.查询审批设置表获得审批流程
                CargoFinanceBus f = new CargoFinanceBus();
                List<QyUserEntity> qyUser = new List<QyUserEntity>();
                QySendInfoEntity send = new QySendInfoEntity();
                CargoOrderBus o = new CargoOrderBus();
                CargoApproveSetEntity AppSet = new CargoApproveSetEntity();
                string ApproveType = "3";
                switch (qup.OrderCheckType)
                {
                    case "2": ApproveType = "9"; break;
                    case "3": ApproveType = "10"; break;
                    case "4": ApproveType = "11"; break;
                    case "5": ApproveType = "12"; break;
                    case "6": ApproveType = "13"; break;
                    default: ApproveType = "3"; break;
                }
                List<CargoApproveSetEntity> aset = f.QueryApproveSet(new CargoApproveSetEntity { ApproveType = ApproveType, DelFlag = "0", HouseID = qup.HouseID });
                if (aset.Count > 0) { AppSet = aset[0]; }
                else
                {
                    aset = f.QueryApproveSet(new CargoApproveSetEntity { ApproveType = "3", DelFlag = "0", HouseID = qup.HouseID });
                    if (aset.Count > 0) { AppSet = aset[0]; }
                }
                AppSet.EnSafe();
                //2.审批流程的每一级和当前人的审批角色相匹配
                if (AppSet.OneCheckID.Equals(qup.CheckID))
                {
                    #region 第一级
                    send.title = AppSet.OneCheckName + "审批通过";
                    result.ApplyStatus = "1";
                    result.CheckID = AppSet.TwoCheckID;
                    result.CheckName = AppSet.TwoCheckName;
                    if (string.IsNullOrEmpty(AppSet.TwoCheckID))
                    {
                        send.title = "订单改价申请审批结束";
                        //表示是最后一级
                        IsEnd = true;
                        result.ApplyStatus = "3";
                        result.CheckID = "";// AppSet.OneCheckID;
                        result.CheckName = "";// AppSet.OneCheckName;
                        approveRoute.Opera = "2";
                        //approveRoute.Opera = "0";
                        //approveRouteList.Add(approveRoute);
                    }
                    else
                    {
                        //判断下一级是不是分公司领导，如果是则查询申请人的上级领导
                        if (AppSet.TwoCheckID.Equals("3"))
                        {
                            List<SystemUserEntity> Bosslist = o.QueryUserBossLoginName(new SystemUserEntity { LoginName = qup.ApplyID });
                            if (Bosslist.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(Bosslist[0].LoginName))
                                {
                                    qyUser.Add(new QyUserEntity { UserID = Bosslist[0].LoginName, WxName = Bosslist[0].UserName });
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.ThreeCheckID, CheckHouseID = qup.HouseID.ToString() });
                                }
                            }
                        }
                        else
                        {
                            qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.TwoCheckID, CheckHouseID = qup.HouseID.ToString() });
                        }
                        if (!string.IsNullOrEmpty(AppSet.AutoPassID))
                        {
                            bool IsAuto = false;
                            //20221218 增加处理自动审批功能
                            if (AppSet.AutoPassID.Contains(AppSet.TwoCheckID))
                            {
                                IsAuto = true;
                                //说明第二级自动过
                                result.CheckID = AppSet.ThreeCheckID;
                                result.CheckName = AppSet.ThreeCheckName;
                                CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                                approveTwoRoute.ExID = OID;
                                approveTwoRoute.UserID = qyUser[0].UserID;
                                approveTwoRoute.UserName = qyUser[0].WxName;
                                approveTwoRoute.ApproveType = "1";
                                approveTwoRoute.Result = "自动跳过审批";
                                approveTwoRoute.Opera = "0";
                                approveRouteList.Add(approveTwoRoute);
                                //下一级为空表示审批结束
                                if (string.IsNullOrEmpty(AppSet.ThreeCheckID))
                                {
                                    IsAuto = false;
                                    send.title = "订单改价申请审批结束";
                                    //表示是最后一级
                                    IsEnd = true;
                                    result.ApplyStatus = "3";
                                    result.CheckID = "";// AppSet.OneCheckID;
                                    result.CheckName = "";// AppSet.OneCheckName;
                                    approveRoute.Opera = "2";
                                    //approveRoute.Opera = "0";
                                    approveRouteList.Add(approveRoute);
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.ThreeCheckID, CheckHouseID = qup.HouseID.ToString() });
                                }
                            }
                            if (AppSet.AutoPassID.Contains(AppSet.ThreeCheckID) && !string.IsNullOrEmpty(AppSet.ThreeCheckID) && IsAuto)
                            {
                                //说明第三级自动过
                                result.CheckID = AppSet.FourCheckID;
                                result.CheckName = AppSet.FourCheckName;
                                CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                                approveTwoRoute.ExID = OID;
                                approveTwoRoute.UserID = qyUser[0].UserID;
                                approveTwoRoute.UserName = qyUser[0].WxName;
                                approveTwoRoute.ApproveType = "1";
                                approveTwoRoute.Result = "自动跳过审批";
                                approveTwoRoute.Opera = "0";
                                approveRouteList.Add(approveTwoRoute);
                                //下一级为空表示审批结束
                                if (string.IsNullOrEmpty(AppSet.FourCheckID))
                                {
                                    IsAuto = false;
                                    send.title = "订单改价申请审批结束";
                                    //表示是最后一级
                                    IsEnd = true;
                                    result.ApplyStatus = "3";
                                    result.CheckID = "";// AppSet.OneCheckID;
                                    result.CheckName = "";// AppSet.OneCheckName;
                                    approveRoute.Opera = "2";
                                    //approveRoute.Opera = "0";
                                    approveRouteList.Add(approveRoute);
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.FourCheckID, CheckHouseID = qup.HouseID.ToString() });
                                }
                            }
                            if (AppSet.AutoPassID.Contains(AppSet.FourCheckID) && !string.IsNullOrEmpty(AppSet.FourCheckID) && IsAuto)
                            {
                                //说明第三级自动过
                                result.CheckID = AppSet.FiveCheckID;
                                result.CheckName = AppSet.FiveCheckName;
                                CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                                approveTwoRoute.ExID = OID;
                                approveTwoRoute.UserID = qyUser[0].UserID;
                                approveTwoRoute.UserName = qyUser[0].WxName;
                                approveTwoRoute.ApproveType = "1";
                                approveTwoRoute.Result = "自动跳过审批";
                                approveTwoRoute.Opera = "0";
                                approveRouteList.Add(approveTwoRoute);
                                //下一级为空表示审批结束
                                if (string.IsNullOrEmpty(AppSet.FiveCheckID))
                                {
                                    IsAuto = false;
                                    send.title = "订单改价申请审批结束";
                                    //表示是最后一级
                                    IsEnd = true;
                                    result.ApplyStatus = "3";
                                    result.CheckID = "";// AppSet.OneCheckID;
                                    result.CheckName = "";// AppSet.OneCheckName;
                                    approveRoute.Opera = "2";
                                    //approveRoute.Opera = "0";
                                    approveRouteList.Add(approveRoute);
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.FiveCheckID, CheckHouseID = qup.HouseID.ToString() });
                                }
                            }
                            if (AppSet.AutoPassID.Contains(AppSet.FiveCheckID) && !string.IsNullOrEmpty(AppSet.FiveCheckID) && IsAuto)
                            {
                                //说明第三级自动过
                                result.CheckID = AppSet.SixCheckID;
                                result.CheckName = AppSet.SixCheckName;
                                CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                                approveTwoRoute.ExID = OID;
                                approveTwoRoute.UserID = qyUser[0].UserID;
                                approveTwoRoute.UserName = qyUser[0].WxName;
                                approveTwoRoute.ApproveType = "1";
                                approveTwoRoute.Result = "自动跳过审批";
                                approveTwoRoute.Opera = "0";
                                approveRouteList.Add(approveTwoRoute);
                                //下一级为空表示审批结束
                                if (string.IsNullOrEmpty(AppSet.SixCheckID))
                                {
                                    IsAuto = false;
                                    send.title = "订单改价申请审批结束";
                                    //表示是最后一级
                                    IsEnd = true;
                                    result.ApplyStatus = "3";
                                    result.CheckID = "";// AppSet.OneCheckID;
                                    result.CheckName = "";// AppSet.OneCheckName;
                                    approveRoute.Opera = "2";
                                    //approveRoute.Opera = "0";
                                    approveRouteList.Add(approveRoute);
                                }
                                else
                                {
                                    qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SixCheckID, CheckHouseID = qup.HouseID.ToString() });
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (AppSet.TwoCheckID.Equals(qup.CheckID))
                {
                    #region 第二级
                    send.title = AppSet.TwoCheckName + "审批通过";
                    result.ApplyStatus = "1";
                    result.CheckID = AppSet.ThreeCheckID;
                    result.CheckName = AppSet.ThreeCheckName;
                    if (string.IsNullOrEmpty(AppSet.ThreeCheckID))
                    {
                        send.title = "订单改价申请审批结束";
                        //表示是最后一级
                        IsEnd = true;
                        result.ApplyStatus = "3";
                        result.CheckID = "";//AppSet.TwoCheckID;
                        result.CheckName = "";// AppSet.TwoCheckName;
                        approveRoute.Opera = "2";
                        //approveRouteList.Add(approveRoute);
                    }
                    else
                    {
                        qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.ThreeCheckID, CheckHouseID = qup.HouseID.ToString() });
                    }
                    if (!string.IsNullOrEmpty(AppSet.AutoPassID))
                    {
                        bool IsAuto = false;
                        if (!string.IsNullOrEmpty(AppSet.ThreeCheckID) && AppSet.AutoPassID.Contains(AppSet.ThreeCheckID))
                        {
                            IsAuto = true;
                            //说明第三级自动过
                            result.CheckID = AppSet.FourCheckID;
                            result.CheckName = AppSet.FourCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.FourCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.FourCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                        if (!string.IsNullOrEmpty(AppSet.FourCheckID) && AppSet.AutoPassID.Contains(AppSet.FourCheckID) && IsAuto)
                        {
                            //说明第三级自动过
                            result.CheckID = AppSet.FiveCheckID;
                            result.CheckName = AppSet.FiveCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.FiveCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.FiveCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                        if (!string.IsNullOrEmpty(AppSet.FiveCheckID) && AppSet.AutoPassID.Contains(AppSet.FiveCheckID) && IsAuto)
                        {
                            //说明第三级自动过
                            result.CheckID = AppSet.SixCheckID;
                            result.CheckName = AppSet.SixCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.SixCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SixCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                    }
                    #endregion
                }
                else if (AppSet.ThreeCheckID.Equals(qup.CheckID))
                {
                    #region 第三级
                    send.title = AppSet.ThreeCheckName + "审批通过";
                    result.ApplyStatus = "1";
                    result.CheckID = AppSet.FourCheckID;
                    result.CheckName = AppSet.FourCheckName;
                    if (string.IsNullOrEmpty(AppSet.FourCheckID))
                    {
                        send.title = "订单改价申请审批结束";
                        //表示是最后一级
                        IsEnd = true;
                        result.ApplyStatus = "3";
                        result.CheckID = "";//AppSet.ThreeCheckID;
                        result.CheckName = "";// AppSet.ThreeCheckName;
                        approveRoute.Opera = "2";
                        //approveRouteList.Add(approveRoute);
                    }
                    else
                    {
                        qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.FourCheckID, CheckHouseID = qup.HouseID.ToString() });
                    }
                    if (!string.IsNullOrEmpty(AppSet.AutoPassID))
                    {
                        bool IsAuto = false;
                        if (!string.IsNullOrEmpty(AppSet.FourCheckID) && AppSet.AutoPassID.Contains(AppSet.FourCheckID))
                        {
                            IsAuto = true;
                            //说明第三级自动过
                            result.CheckID = AppSet.FiveCheckID;
                            result.CheckName = AppSet.FiveCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.FiveCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.FiveCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                        if (!string.IsNullOrEmpty(AppSet.FiveCheckID) && AppSet.AutoPassID.Contains(AppSet.FiveCheckID) && IsAuto)
                        {
                            //说明第三级自动过
                            result.CheckID = AppSet.SixCheckID;
                            result.CheckName = AppSet.SixCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.SixCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SixCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                        if (!string.IsNullOrEmpty(AppSet.SixCheckID) && AppSet.AutoPassID.Contains(AppSet.SixCheckID) && IsAuto)
                        {
                            //说明第三级自动过
                            result.CheckID = AppSet.SevenCheckID;
                            result.CheckName = AppSet.SevenCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.SevenCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SevenCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                    }
                    #endregion
                }
                else if (AppSet.FourCheckID.Equals(qup.CheckID))
                {
                    #region 第四级
                    send.title = AppSet.FourCheckName + "审批通过";
                    result.ApplyStatus = "1";
                    result.CheckID = AppSet.FiveCheckID;
                    result.CheckName = AppSet.FiveCheckName;
                    if (string.IsNullOrEmpty(AppSet.FiveCheckID))
                    {
                        send.title = "订单改价申请审批结束";
                        //表示是最后一级
                        IsEnd = true;
                        result.ApplyStatus = "3";
                        result.CheckID = "";//AppSet.FourCheckID;
                        result.CheckName = "";//AppSet.FourCheckName;
                        approveRoute.Opera = "2";
                        //approveRouteList.Add(approveRoute);
                    }
                    else
                    {
                        qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.FiveCheckID, CheckHouseID = qup.HouseID.ToString() });
                    }
                    if (!string.IsNullOrEmpty(AppSet.AutoPassID))
                    {
                        bool IsAuto = false;
                        if (!string.IsNullOrEmpty(AppSet.FiveCheckID) && AppSet.AutoPassID.Contains(AppSet.FiveCheckID))
                        {
                            IsAuto = true;
                            //说明第三级自动过
                            result.CheckID = AppSet.SixCheckID;
                            result.CheckName = AppSet.SixCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.SixCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;

                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SixCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                        if (!string.IsNullOrEmpty(AppSet.SixCheckID) && AppSet.AutoPassID.Contains(AppSet.SixCheckID) && IsAuto)
                        {
                            //说明第三级自动过
                            result.CheckID = AppSet.SevenCheckID;
                            result.CheckName = AppSet.SevenCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.SevenCheckID))
                            {
                                IsAuto = false;
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                CargoExpenseApproveRoutEntity endRoute = new CargoExpenseApproveRoutEntity();
                                endRoute.ExID = OID;
                                endRoute.UserID = UserInfor.LoginName;
                                endRoute.UserName = UserInfor.UserName;
                                endRoute.ApproveType = "1";
                                endRoute.Result = Reason;
                                endRoute.Opera = "2";
                                approveRouteList.Add(endRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SevenCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                    }
                    #endregion
                }
                else if (AppSet.FiveCheckID.Equals(qup.CheckID))
                {
                    #region 第五级
                    send.title = AppSet.FiveCheckName + "审批通过";
                    result.ApplyStatus = "1";
                    result.CheckID = AppSet.SixCheckID;
                    result.CheckName = AppSet.SixCheckName;
                    if (string.IsNullOrEmpty(AppSet.SixCheckID))
                    {
                        send.title = "订单改价申请审批结束";
                        //表示是最后一级
                        IsEnd = true;
                        result.ApplyStatus = "3";
                        result.CheckID = "";// AppSet.FiveCheckID;
                        result.CheckName = "";//AppSet.FiveCheckName;
                        approveRoute.Opera = "2";
                        //approveRouteList.Add(approveRoute);
                    }
                    else
                    {
                        qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SixCheckID, CheckHouseID = qup.HouseID.ToString() });
                    }
                    if (!string.IsNullOrEmpty(AppSet.AutoPassID))
                    {
                        if (!string.IsNullOrEmpty(AppSet.SixCheckID) && AppSet.AutoPassID.Contains(AppSet.SixCheckID))
                        {
                            //说明第三级自动过
                            result.CheckID = AppSet.SevenCheckID;
                            result.CheckName = AppSet.SevenCheckName;
                            CargoExpenseApproveRoutEntity approveTwoRoute = new CargoExpenseApproveRoutEntity();
                            approveTwoRoute.ExID = OID;
                            approveTwoRoute.UserID = qyUser[0].UserID;
                            approveTwoRoute.UserName = qyUser[0].WxName;
                            approveTwoRoute.ApproveType = "1";
                            approveTwoRoute.Result = "自动跳过审批";
                            approveTwoRoute.Opera = "0";
                            approveRouteList.Add(approveTwoRoute);
                            //下一级为空表示审批结束
                            if (string.IsNullOrEmpty(AppSet.SevenCheckID))
                            {
                                send.title = "订单改价申请审批结束";
                                //表示是最后一级
                                IsEnd = true;
                                result.ApplyStatus = "3";
                                result.CheckID = "";// AppSet.OneCheckID;
                                result.CheckName = "";// AppSet.OneCheckName;
                                approveRoute.Opera = "2";
                                //approveRoute.Opera = "0";
                                approveRouteList.Add(approveRoute);
                            }
                            else
                            {
                                qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SevenCheckID, CheckHouseID = qup.HouseID.ToString() });
                            }
                        }
                    }
                    #endregion
                }
                else if (AppSet.SixCheckID.Equals(qup.CheckID))
                {
                    #region 第六级
                    send.title = AppSet.SixCheckName + "审批通过";
                    result.ApplyStatus = "1";
                    result.CheckID = AppSet.SevenCheckID;
                    result.CheckName = AppSet.SevenCheckName;
                    if (string.IsNullOrEmpty(AppSet.SevenCheckID))
                    {
                        send.title = "订单改价申请审批结束";
                        //表示是最后一级
                        IsEnd = true;
                        result.ApplyStatus = "3";
                        result.CheckID = "";// AppSet.FiveCheckID;
                        result.CheckName = "";//AppSet.FiveCheckName;
                        approveRoute.Opera = "2";
                        //approveRouteList.Add(approveRoute);
                    }
                    else
                    {
                        qyUser = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.SevenCheckID, CheckHouseID = qup.HouseID.ToString() });
                    }
                    #endregion
                }

                q.CheckUpdatePrice(result, log);
                foreach (var aroute in approveRouteList)
                {
                    f.AddExpenseApproveRout(aroute);

                }
                //f.AddExpenseApproveRout(approveRoute);
                //添加申请单与审批人关系数据
                Common.WriteTextLog("添加申请单与审批人关系数据" + OID.ToString() + UserInfor.UserID + UserInfor.UserName);
                if (!f.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = OID, CheckUserID = UserInfor.LoginName, CheckType = "0", ApproveType = "1" }))
                {
                    Common.WriteTextLog("判断正确" + OID.ToString() + UserInfor.UserID + UserInfor.UserName);
                    f.AddApproveCheck(new CargoApproveCheckEntity
                    {
                        ApproveID = OID,
                        CheckUserID = UserInfor.LoginName,
                        CheckName = UserInfor.UserName,
                        CheckType = "0",
                        ReadStatus = "1",
                        ApproveType = "1"
                    }, log);
                    Common.WriteTextLog("操作完成" + OID.ToString() + UserInfor.UserID + UserInfor.UserName);

                }
                //如果 是最后审批人，就结束
                if (IsEnd)
                {
                    #region 修改数据
                    if (OrderType.Equals("0"))
                    {
                        //修改商城订单价格
                        q.UpdateWxMallOrderPrice(result, log);
                    }
                    else
                    {
                        //修改电脑订单价格
                        CargoOrderBus obus = new CargoOrderBus();
                        CargoOrderEntity orderEnt = obus.QueryOrderInfo(new CargoOrderEntity { OrderNo = OrderNo });
                        obus.UpdateCargoOrderModifyPriceStatus(new CargoOrderEntity { OrderNo = OrderNo, ModifyPriceStatus = "0" }, log);
                        CargoHouseBus house = new CargoHouseBus();
                        CargoHouseEntity houseEnt = house.QueryCargoHouseByID(orderEnt.HouseID);
                        if (orderEnt.HouseID.Equals(9) && orderEnt.ThrowGood.Equals("15"))
                        {
                            houseEnt.Name = "新陆城配";
                            ////速配单
                            //obus.InsertCargoOrderPush(new CargoOrderPushEntity
                            //{
                            //    OrderNo = orderEnt.OrderNo,
                            //    Dep = orderEnt.Dep,
                            //    Dest = orderEnt.Dest,
                            //    Piece = orderEnt.Piece,
                            //    TransportFee = orderEnt.TransportFee,
                            //    ClientNum = orderEnt.ClientNum.ToString(),
                            //    AcceptAddress = orderEnt.AcceptAddress,
                            //    AcceptCellphone = orderEnt.AcceptCellphone,
                            //    AcceptTelephone = orderEnt.AcceptTelephone,
                            //    AcceptPeople = orderEnt.AcceptPeople,
                            //    AcceptUnit = orderEnt.AcceptUnit,
                            //    HouseID = orderEnt.HouseID.ToString(),
                            //    HouseName = "新陆城配",
                            //    OP_ID = qup.ApplyName,
                            //    PushType = "0",
                            //    PushStatus = "0",
                            //    LogisID = orderEnt.LogisID
                            //}, log);
                        }
                        //if (orderEnt.HouseID.Equals(9) || orderEnt.HouseID.Equals(15) || orderEnt.HouseID.Equals(49) || orderEnt.HouseID.Equals(50) || orderEnt.HouseID.Equals(51) || orderEnt.HouseID.Equals(52) || orderEnt.HouseID.Equals(53) || orderEnt.HouseID.Equals(54) || orderEnt.HouseID.Equals(13) || orderEnt.HouseID.Equals(14) || orderEnt.HouseID.Equals(15) || orderEnt.HouseID.Equals(23) || orderEnt.HouseID.Equals(30) || orderEnt.HouseID.Equals(93))
                        //{
                        //    //判断必须选择好来运速递
                        //    if (orderEnt.LogisID.Equals(34))
                        //    {
                        //        //广州仓库
                        //        if (orderEnt.HouseID.Equals(9))
                        //        {
                        //            //等通知发货的订单不推送
                        //            if (orderEnt.PostponeShip != "1")
                        //            {
                        //                List<CargoContainerShowEntity> outHouseList = obus.QueryOrderByOrderNo(new CargoOrderEntity { OrderNo = orderEnt.OrderNo });
                        //                //内部订单
                        //                obus.SaveHlyOrderData(outHouseList, orderEnt);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            List<CargoContainerShowEntity> outHouseList = obus.QueryOrderByOrderNo(new CargoOrderEntity { OrderNo = orderEnt.OrderNo });
                        //            //内部订单
                        //            obus.SaveHlyOrderData(outHouseList, orderEnt);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        obus.InsertCargoOrderPush(new CargoOrderPushEntity
                        {
                            OrderNo = orderEnt.OrderNo,
                            Dep = orderEnt.Dep,
                            Dest = orderEnt.Dest,
                            Piece = orderEnt.Piece,
                            TransportFee = orderEnt.TransportFee,
                            ClientNum = orderEnt.ClientNum.ToString(),
                            AcceptAddress = orderEnt.AcceptAddress,
                            AcceptCellphone = orderEnt.AcceptCellphone,
                            AcceptTelephone = orderEnt.AcceptTelephone,
                            AcceptPeople = orderEnt.AcceptPeople,
                            AcceptUnit = orderEnt.AcceptUnit,
                            HouseID = orderEnt.HouseID.ToString(),
                            HouseName = houseEnt.Name,
                            OP_ID = qup.ApplyName,
                            PushType = "0",
                            PushStatus = "0",
                            LogisID = orderEnt.LogisID,
                            BusinessID = orderEnt.BusinessID
                        }, log);
                    }
                    //}
                    #endregion

                    List<QyUserEntity> cQyU = new List<QyUserEntity>();
                    if (!string.IsNullOrEmpty(AppSet.CopyCheck))
                    {
                        cQyU = q.QueryUserList(new QyUserEntity { CheckRole = AppSet.CopyCheck });
                    }
                    foreach (var c in cQyU)
                    {
                        //添加申请单与审批人关系数据
                        if (!f.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = OID, CheckUserID = c.UserID, ApproveType = "1" }))
                        {
                            f.AddApproveCheck(new CargoApproveCheckEntity
                            {
                                ApproveID = OID,
                                CheckUserID = c.UserID,
                                CheckName = c.WxName,
                                CheckType = "1",
                                ReadStatus = "1",
                                ApproveType = "1"
                            }, log);
                        }
                        if (!copy.Contains(c.UserID))
                        {
                            copy += c.UserID + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(AppSet.CopyUserID) && !string.IsNullOrEmpty(AppSet.CopyCheckName))
                    {
                        string[] uS = AppSet.CopyUserID.Split(',');
                        string[] uName = AppSet.CopyUserName.Split(',');
                        if (uS.Length > 0)
                        {
                            for (int i = 0; i < uS.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(uS[i]))
                                {
                                    //添加申请单与审批人关系数据
                                    if (!f.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = OID, CheckUserID = uS[i], ApproveType = "1" }))
                                    {
                                        f.AddApproveCheck(new CargoApproveCheckEntity
                                        {
                                            ApproveID = OID,
                                            CheckUserID = uS[i],
                                            CheckName = uName[i],
                                            CheckType = "1",
                                            ReadStatus = "1",
                                            ApproveType = "1"
                                        }, log);
                                    }
                                    if (!copy.Contains(uS[i]))
                                    {
                                        copy += uS[i] + ",";
                                    }
                                }
                            }
                        }
                    }
                    try
                    {
                        if (OrderType.Equals("0"))
                        {
                            CargoOrderBus obus = new CargoOrderBus();
                            string wxopenID = obus.QueryWeixinOpenIDByOrderNo(OrderNo);
                            //推送客户改价成功通知
                            WxCustomSend wxsend = new WxCustomSend();
                            string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
                            wxsend.SendWeiXinInfo(token, wxopenID, new QySendInfoEntity { content = "您的订单：" + OrderNo + "改价申请已通过，请到我的订单进行支付，谢谢！", msgType = msgType.text });
                        }
                    }
                    catch (ApplicationException exx)
                    {
                        msg.Result = true;
                        Common.WriteTextLog(OrderNo + "推送客户通知失败");
                    }
                }
                if (qyUser.Count > 0 || IsEnd)
                {
                    #region 推送通知
                    try
                    {
                        //推送给提交人
                        send.msgType = msgType.textcard;
                        send.toUser = qup.ApplyID;
                        //send.toUser = ou.ApplyID;
                        //send.toTag = strTag;
                        send.content = "<div></div><div>订单号：" + OrderNo + "</div><div>收货人：" + Name + "  " + Cellphone + "</div><div>审批人：" + UserInfor.UserName + "</div><div>审批时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</div><div>审批意见：" + result.CheckResult + "</div><div></div>";
                        send.url = "http://dlt.neway5.com/QY/qyApproveCheck.aspx?OrderNo=" + OrderNo + "&OID=" + OID + "&OrderType=" + OrderType;
                        WxQYSendHelper.PushInfo("0", "3", send);
                    }
                    catch (Exception)
                    {
                        msg.Result = true;
                    }
                    //推送给下一审批人
                    foreach (var it in qyUser)
                    {
                        try
                        {
                            send.toUser = it.UserID;
                            WxQYSendHelper.PushInfo("0", "2", send);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(copy))
                    {
                        copy = copy.Substring(0, copy.Length - 1);
                        try
                        {
                            send.toUser = copy;
                            WxQYSendHelper.PushInfo("0", "2", send);
                        }
                        catch (Exception ex)
                        {
                            msg.Result = true;
                        }
                    }
                    #endregion
                }
                #region 创建群聊窗口和推送信息
                ////创建群聊窗口和推送信息
                //if (!qup.HouseID.Equals(64))
                //{
                //    try
                //    {
                //        string oU = "http://dlt.neway5.com/QY/qyApproveCheck.aspx?OrderNo=" + OrderNo + "&OID=" + OID + "&OrderType=" + OrderType;
                //        QyGroupChatEntity chat = q.QueryGroupChatEntity(new QyGroupChatEntity { HouseID = qup.HouseID, ChatType = "0" });
                //        if (chat.ID.Equals(0))
                //        {
                //            //string[] ul = { "0007", "2081", "1079" };
                //            List<string> lis = new List<string>();
                //            lis.Add("0007");
                //            lis.Add("2081");
                //            lis.Add("1079");
                //            //lis.Add("1000");
                //            //lis.Add("2076");
                //            //lis.Add(qup.ApplyID);
                //            string hN = string.Empty;
                //            switch (qup.HouseID)
                //            {
                //                case 1: hN = "湖南迪乐泰改价审批群"; lis.Add("1006"); break;
                //                case 3: hN = "湖北迪乐泰改价审批群"; lis.Add("1061"); break;
                //                //case 9: hN = "广州迪乐泰改价审批群"; lis.Add("2096"); break;
                //                case 11: hN = "西安迪乐泰改价审批群"; lis.Add("1061"); break;
                //                //case 34: hN = "海南迪乐泰改价审批群"; lis.Add("2096"); break;
                //                //case 12: hN = "梅州迪乐泰改价审批群"; lis.Add("2096"); break;
                //                //case 44: hN = "揭阳迪乐泰改价审批群"; lis.Add("2096"); break;
                //                //case 45: hN = "广东仓库改价审批群"; lis.Add("2096"); break;
                //                case 46: hN = "四川迪乐泰改价审批群"; break;
                //                case 48: hN = "重庆迪乐泰改价审批群"; lis.Add("1061"); break;
                //                default:
                //                    break;
                //            }
                //            string ChatID = DateTime.Now.ToString("yyMMddHHmmss");

                //            //表示没有群聊，要新增
                //            q.AddGroupChat(new QyGroupChatEntity { ChatName = hN, HouseID = qup.HouseID, ChatType = "0", Owner = "0007", ChatID = ChatID }, log);
                //            WxQYSendHelper.CreateChat(ChatID, hN, "0007", lis.ToArray(), "1000003");

                //            //向群聊里推送审批通知
                //            QySendInfoEntity qysend = new QySendInfoEntity();
                //            qysend.agentID = "1000003";
                //            qysend.msgType = msgType.markdown;
                //            qysend.ChatID = ChatID;
                //            qysend.content = "订单号**" + qup.OrderNo + "**改价审批已通过\n>**审批意见** \n>\n><font color=\"info\">" + Reason + "</font>\n>\n>审批人：" + qup.CheckName + "\n>\n>查看审批流程请点击：[审批流程](" + oU + ")";
                //            WxQYSendHelper.SendChatInfo(qysend);
                //        }
                //        else
                //        {
                //            //向群聊里推送审批通知
                //            QySendInfoEntity qysend = new QySendInfoEntity();
                //            qysend.agentID = "1000003";
                //            qysend.msgType = msgType.markdown;
                //            qysend.ChatID = chat.ChatID;
                //            qysend.content = "订单号**" + qup.OrderNo + "**改价审批已通过\n>**审批意见** \n>\n><font color=\"info\">" + Reason + "</font>\n>\n>审批人：" + qup.CheckName + "\n>\n>查看审批流程请点击：[审批流程](" + oU + ")";
                //            WxQYSendHelper.SendChatInfo(qysend);
                //        }
                //    }
                //    catch (ApplicationException ex)
                //    {
                //        msg.Result = true;
                //    }
                //}
                #endregion
            }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        /// <summary>
        /// 拒绝审批
        /// </summary>
        public void PurchaseCheckNo()
        {
            string Reason = Convert.ToString(Request["Reason"]);
            string OrderNo = Convert.ToString(Request["OrderNo"]);
            long OID = Convert.ToInt64(Request["OID"]);
            string Name = Convert.ToString(Request["Name"]);//收货人姓名 
            string Cellphone = Convert.ToString(Request["Cellphone"]);
            string OrderType = Convert.ToString(Request["OrderType"]);//订单类型0：商城订单1：电脑订单
            QyOrderUpdatePriceEntity result = new QyOrderUpdatePriceEntity();
            ErrMessage msg = new ErrMessage(); msg.Message = ""; msg.Result = true;
            result.HouseID = UserInfor.HouseID;
            result.CheckID = "";// UserInfor.UserID;
            result.CheckName = "";// UserInfor.UserName;
            result.CheckTime = DateTime.Now;
            result.OrderNo = OrderNo;
            result.ApplyStatus = "2";
            result.CheckResult = Reason;
            result.OID = OID;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "报销审批";
            log.NvgPage = "修改价格审批";
            log.UserID = UserInfor.LoginName;
            log.Status = "0";
            log.Operate = "U";
            QiyeBus bus = new QiyeBus();
            QyOrderUpdatePriceEntity qup = bus.QueryOrderUpdatePriceEntity(result);
            //if (qup.ApplyStatus.Equals("3"))
            //{
            //    msg.Result = false;
            //    msg.Message = "该订单已审批结束，请勿重复审批";
            //}
            //if (!UserInfor.CheckRole.Contains(qup.CheckID))
            //{
            //    msg.Result = false;
            //    msg.Message = "流程未到您审批";
            //}
            if (msg.Result)
            {
                CargoExpenseApproveRoutEntity approveRoute = new CargoExpenseApproveRoutEntity();
                approveRoute.ExID = OID;
                approveRoute.UserID = UserInfor.LoginName;
                approveRoute.UserName = UserInfor.UserName;
                approveRoute.ApproveType = "1";
                approveRoute.Result = Reason;
                approveRoute.Opera = "1";//拒审

                CargoFinanceBus f = new CargoFinanceBus();
                bus.CheckUpdatePrice(result, log);
                f.AddExpenseApproveRout(approveRoute);
                //添加申请单与审批人关系数据
                if (!f.IsExistApproveCheck(new CargoApproveCheckEntity { ApproveID = OID, CheckUserID = UserInfor.LoginName, CheckType = "0", ApproveType = "1" }))
                {
                    f.AddApproveCheck(new CargoApproveCheckEntity
                    {
                        ApproveID = OID,
                        CheckUserID = UserInfor.LoginName,
                        CheckName = UserInfor.UserName,
                        CheckType = "0",
                        ReadStatus = "1",
                        ApproveType = "1"
                    }, log);
                }
                if (OrderType.Equals("1"))
                {
                    //电脑订单，删除订单
                    CargoOrderBus obus = new CargoOrderBus();
                    CargoOrderEntity orderEnt = obus.QueryOrderInfoByOrderID(qup.OrderID);
                    orderEnt.DeleteID = UserInfor.LoginName;
                    orderEnt.DeleteName = UserInfor.UserName;
                    List<CargoOrderEntity> ol = new List<CargoOrderEntity>();
                    ol.Add(orderEnt);

                    //仓库同步缓存
                    CargoHouseBus house = new CargoHouseBus();
                    List<CargoProductEntity> syncProduct = house.SyncTypeOrderNo(OrderNo);
                    foreach (CargoProductEntity product in syncProduct)
                    {

                        if (product.SyncType == "2" || (product.SyncType == "1" && product.TypeID == 34))
                        {
                            RedisHelper.HashSet("OpenSystemStockSyc", "" + product.HouseID + "_" + product.TypeID + "_" + product.ProductCode + "", product.GoodsCode);
                        }
                    }

                    obus.DeleteOrderInfo(ol, log);
                }
                try
                {
                    QySendInfoEntity send = new QySendInfoEntity();
                    send.title = "订单改价申请拒审";
                    send.msgType = msgType.textcard;
                    send.toUser = qup.ApplyID;
                    //send.toTag = strTag;
                    send.content = "<div></div><div>订单号：" + OrderNo + "</div><div>收货人：" + Name + "  " + Cellphone + "</div><div>审批人：" + result.CheckName + "</div><div>审批时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</div><div>审批意见：" + result.CheckResult + "</div><div>审批结果：未通过</div>";
                    send.url = "http://dlt.neway5.com/QY/qyApproveCheck.aspx?OrderNo=" + OrderNo + "&OID=" + OID + "&OrderType=" + OrderType;
                    WxQYSendHelper.PushInfo("0", "3", send);
                    //send.toUser = "0007";
                    //WxQYSendHelper.PushInfo("0", "2", send);
                }
                catch (ApplicationException ex)
                {
                    msg.Result = true;
                }
                ////创建群聊窗口和推送信息
                //if (!qup.HouseID.Equals(64))
                //{
                //    try
                //    {
                //        string oU = "http://dlt.neway5.com/QY/qyApproveCheck.aspx?OrderNo=" + OrderNo + "&OID=" + OID + "&OrderType=" + OrderType;
                //        QyGroupChatEntity chat = bus.QueryGroupChatEntity(new QyGroupChatEntity { HouseID = qup.HouseID, ChatType = "0" });
                //        if (!chat.ID.Equals(0))
                //        {
                //            //向群聊里推送审批通知
                //            QySendInfoEntity qysend = new QySendInfoEntity();
                //            qysend.agentID = "1000003";
                //            qysend.msgType = msgType.markdown;
                //            qysend.ChatID = chat.ChatID;
                //            qysend.content = "订单号**" + OrderNo + "**改价审批拒绝\n>**拒审意见** \n>\n><font color=\"info\">" + Reason + "</font>\n>\n>审批人：" + qup.CheckName + "\n>\n>查看审批流程请点击：[审批流程](" + oU + ")";
                //            WxQYSendHelper.SendChatInfo(qysend);
                //        }
                //    }
                //    catch (ApplicationException ex)
                //    {
                //        msg.Result = true;
                //    }
                //}
            }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }

        #endregion

        #region 更改转账类型

        ///// <summary>
        ///// 审批操作
        ///// </summary>
        public void UpdateTransferType()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            //CargoRealFactoryPurchaseHouseEntity ent = new CargoRealFactoryPurchaseHouseEntity();
            //CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购单管理";
            log.NvgPage = "采购单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            string ress = string.Empty;
            try
            {
                CargoRealFactoryPurchaseOrderEntity OrderEntity = new CargoRealFactoryPurchaseOrderEntity();
                CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();

                OrderEntity.PurOrderNo = Convert.ToString(Request["TTPurOrderNO"]);
                OrderEntity.PurOrderID = long.Parse(Convert.ToString(Request["TTPurOrderID"]));
                OrderEntity.PaymentMethod = Convert.ToInt32(Request["PaymentMethod"]);
                OrderEntity.TransferAccount = Convert.ToInt32(Request["TransferAccount"]);

                if (OrderEntity.PaymentMethod == null)
                {
                    msg.Result = false;
                    msg.Message = "保存失败，未获取到[付款方式]数据！";
                    ress = JSON.Encode(msg);
                    Response.Write(ress);
                    return;
                }

                if (OrderEntity.TransferAccount == null)
                {
                    msg.Result = false;
                    msg.Message = "保存失败，未获取到[转账账户]数据！";
                    ress = JSON.Encode(msg);
                    Response.Write(ress);
                    return;
                }

                bus.UpdatePurchaseOrderTransferType(OrderEntity, log);

                //更改审批状态
                //bus.UpdatePurchaseOrderApproval(new CargoRealFactoryPurchaseOrderEntity { ApplyStatus = "3", CheckResult = Request["CheckResult"], PurOrderID = Convert.ToInt64(Request["PurOrderID"]) }, log);

            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            ress = JSON.Encode(msg);
            Response.Write(ress);
        }

        #endregion

        #region 导出
        public List<CargoRealFactoryPurchaseOrderGoodsEntity> QueryAPList
        {
            get
            {
                if (Session["QueryAPList"] == null)
                {
                    Session["QueryAPList"] = new List<CargoRealFactoryPurchaseOrderGoodsEntity>();
                }
                return (List<CargoRealFactoryPurchaseOrderGoodsEntity>)(Session["QueryAPList"]);
            }
            set
            {
                Session["QueryAPList"] = value;
            }
        }
        public void QueryAPForExport()
        {
            CargoRealFactoryPurchaseOrderEntity queryEntity = new CargoRealFactoryPurchaseOrderEntity();
            CargoRealFactoryPurchaseOrderGoodsEntity queryGoodsEntity = new CargoRealFactoryPurchaseOrderGoodsEntity();

            #region 获取主表数据

            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }

            queryEntity.PurOrderNo = Convert.ToString(Request["PurOrderNo"]);
            if (!string.IsNullOrEmpty(Request["PurchaserName"])) queryEntity.PurchaserName = Convert.ToString(Request["PurchaserName"]);
            if (!string.IsNullOrEmpty(Request["PurDepartID"]))
            {
                queryEntity.PurDepartID = Convert.ToInt32(Request["PurDepartID"]);
            }
            if (Request["ApplyStatus"] != "-1") { queryEntity.ApplyStatus = Convert.ToString(Request["ApplyStatus"]); }
            if (Request["PurchaseType"] != "-1") { queryEntity.PurchaseType = Convert.ToString(Request["PurchaseType"]); }
            if (Request["PurchaseInStoreType"] != "-1") { queryEntity.PurchaseInStoreType = Convert.ToString(Request["PurchaseInStoreType"]); }
            if (Request["APurchaseInStoreState"] != "-1") { queryEntity.PurchaseInStoreState = Convert.ToString(Request["APurchaseInStoreState"]); }
            if (Request["APurchaseUploadDoc"] != "-1") { queryEntity.PurchaseUploadDoc = Convert.ToString(Request["APurchaseUploadDoc"]); }
            if (!string.IsNullOrEmpty(Request["ATypeName"])) queryEntity.TypeName = Convert.ToString(Request["ATypeName"]);
            if (!string.IsNullOrEmpty(Request["ASpec"])) queryEntity.Specs = Convert.ToString(Request["ASpec"]);
            if (!string.IsNullOrEmpty(Request["AGoodsName"])) queryEntity.GoodsCode = Convert.ToString(Request["AGoodsName"]);
            if (!string.IsNullOrEmpty(Request["AFigure"])) queryEntity.Figure = Convert.ToString(Request["AFigure"]);
            //分页
            int pageIndex = 1;
            int pageSize = 100000;
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            Hashtable list = bus.QueryCargoRealPurchase(pageIndex, pageSize, queryEntity);
            var orderList = (List<CargoRealFactoryPurchaseOrderEntity>)list["rows"];
            #endregion

            #region 获取子表数据
            var orderIds = orderList.Select(a => long.Parse(a.PurOrderID.ToString())).ToList();
            queryGoodsEntity.PurOrderArrID = orderIds;
            //分页
            pageIndex = 1;
            pageSize = 100000;
            List<CargoRealFactoryPurchaseOrderGoodsEntity> Goodslist = bus.cargoRealFactoryPurchaseOrderGoodsExport(queryGoodsEntity);
            #endregion

            string err = "OK";
            if (orderList.Count > 0) { QueryAPList = Goodslist; }
            else { err = "没有数据可以进行导出，请重新查询！"; }
            //JSON
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
        }

        public void GetHouseData()
        {

            CargoHouseBus bus = new CargoHouseBus();

            var list = bus.QueryALLHouse();

            var HouseID = Convert.ToInt32(Request["HouseID"]);

            var house = list.FirstOrDefault(a => a.HouseID == HouseID);

            //JSON
            String json = JSON.Encode(house);
            Response.Clear();
            Response.Write(json);

        }

        #endregion

        #region 采购账单管理
        /// <summary>
        /// 查询采购单
        /// </summary>
        public void QueryCargoRealPurchaseAccount()
        {
            CargoRealFactoryPurchaseOrderEntity queryEntity = new CargoRealFactoryPurchaseOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.PurOrderNo = Convert.ToString(Request["PurOrderNo"]);
            if (!string.IsNullOrEmpty(Request["PurchaserID"]))
            {
                queryEntity.PurchaserID = Convert.ToInt32(Request["PurchaserID"]);
            }
            queryEntity.CheckStatus = "0";//未结算
            queryEntity.ApplyStatus= "3";//已结束
            //分页
            int pageIndex = 1;
            int pageSize = 10000;
            CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
            Hashtable list = bus.QueryCargoRealPurchase(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);

        }
        /// <summary>
        /// 保存分账账单
        /// </summary>
        public void savePurchaseBillAccount() {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "采购账单管理";
            log.NvgPage = "保存分账账单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            log.Status = "0";

            try
            {
                List<CargoRealPurchaseAccountGoodsEntity> accountGoods = new List<CargoRealPurchaseAccountGoodsEntity>();
                //账单明细
                int PurchaserID = 0;
                string PurchaserName ="";
                foreach (Hashtable grid in GridRows)
                {
                    PurchaserID= Convert.ToInt32(grid["PurchaserID"]);
                    PurchaserName = Convert.ToString(grid["PurchaserName"]);
                    CargoRealPurchaseAccountGoodsEntity goods = new CargoRealPurchaseAccountGoodsEntity();
                    goods.PurOrderID = Convert.ToInt32(grid["PurOrderID"]);
                    goods.PurOrderNo = Convert.ToString(grid["PurOrderNo"]);
                    goods.TransportFee = Convert.ToDecimal(grid["TransportFee"]);
                    goods.OtherFee = Convert.ToDecimal(grid["OtherFee"]);
                    goods.TotalCharge = Convert.ToDecimal(grid["TotalCharge"]);
                    goods.AccountType = 0;
                    accountGoods.Add(goods);
                }


                CargoRealPurchaseAccountEntity bill = new CargoRealPurchaseAccountEntity();
                bill.AccountNO= Common.GetMaxAccountNoNum("BP");
                bill.AccountTitle = $"{PurchaserName}{DateTime.Now.ToString("yyyyMM")}采购分账账单-手工";
                bill.CreateDate = DateTime.Now;
                bill.PurchaserID = PurchaserID;
                bill.PurchaserName = PurchaserName;
                bill.ARMoney = accountGoods.Sum(w => w.TotalCharge);//本期订总金额
                bill.TransportFee = accountGoods.Sum(w => w.TransportFee);//本期订总金额
                bill.OtherFee = accountGoods.Sum(w => w.OtherFee);//其他费用
                bill.Total = accountGoods.Sum(w => w.TotalCharge);//账单金额
                
                bill.Memo = Convert.ToString(Request["Memo"]);
                bill.CheckStatus = 0;
                bill.OPID = UserInfor.LoginName;
                bill.OPDATE = DateTime.Now;
                bill.goodsList = accountGoods;
                
                //添加账单信息
                CargoRealFactoryPurchaseOrderBus bus = new CargoRealFactoryPurchaseOrderBus();
                bus.AddPurchaseBillAccount(bill, log);
                if (msg.Result)
                {
                    msg.Result = true;
                    msg.Message = "新增账单成功";
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
        ERROR:
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        #endregion
    }
}