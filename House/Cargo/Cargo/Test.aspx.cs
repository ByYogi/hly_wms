
using Cargo.QY;
using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using House.Entity.Cargo.Interface;
using House.Entity.Dto.Order.CargoRpl;
using iText.Kernel.Pdf;
using Memcached.ClientLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.Record.Formula.Functions;
using NPOI.HSSF.Util;
using NPOI.POIFS.Properties;
using NPOI.POIFS.Storage;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.TenPayLibV3;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NPOI.HSSF.Record.Formula.AttrPtg;
using static System.Net.WebRequestMethods;

namespace Cargo
{
    using Microsoft.Win32;

    public partial class Test : System.Web.UI.Page
    {

        private string GetContiUrl()
        {
            return ConfigurationSettings.AppSettings["ContiUrl"];
        }
        private string GetContiStockSync()
        {
            return ConfigurationSettings.AppSettings["ContiStockSync"];
        }
        private string GetContiStockDOTSync()
        {
            return ConfigurationSettings.AppSettings["ContiStockDOTSync"];
        }
        public string OrderPushTime_Test()
        {
            return "1";
        }
        protected MemcachedClient mc = new MemcachedClient();


        public string EncodePassword(string password)
        {
            if (password.Length < 8)
            {
                return "";
            }
            string passOne = password.Substring(0, 2);
            string passTwo = password.Substring(6, 2);
            string passThree = password.Substring(2, 4);
            string passFour = password.Substring(8, password.Length - 8);
            string result = passTwo + passThree + passOne + passFour;

            byte[] pass = Encoding.Unicode.GetBytes(result);
            //byte[] saltByte = Convert.FromBase64String(salt);
            //byte[] dst = new byte[pass.Length + saltByte.Length];
            byte[] returnPassword = null;

            //Buffer.BlockCopy(pass, 0, dst, 0, pass.Length);
            //Buffer.BlockCopy(saltByte, 0, dst, pass.Length, saltByte.Length);

            SHA256 sha = SHA256Cng.Create();
            //SHA256 sha = new SHA256Managed();
            returnPassword = sha.ComputeHash(pass);
            sha.Clear();

            return Convert.ToBase64String(returnPassword);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "{\"HouseName\":\"从化云仓\",\"OrderNo\":\"24112516415039\",\"ProductInfo\":\"普利司通 215/55R17 T005A 94V \",\"OrderNum\":\"1\",\"DeliveryName\":\"自提\",\"ClientInfo\":\"李生 18144817082 广东省广州市从化区河东南路151-1号\",\"ReceivePeople\":\"\"}";
            try
            {
                RedisHelper.SetString("NewOrderNotice_1000", str);
                RedisHelper.SetString("Test", "Test001");

            }
            catch (ApplicationException ex)
            {

                Common.WriteTextLog("写入缓存失败:" + ex.Message);
            }

        }
        private void contisysc()
        {
            CargoInterfaceBus nwBus = new CargoInterfaceBus();
            CargoHouseBus houseBus = new CargoHouseBus();
            CargoClientBus clientBus = new CargoClientBus();
            CargoOrderBus bus = new CargoOrderBus();
            string contistr = "{\"code\":10000,\"message\":\"操作成功\",\"data\":{\"saleOrderStatusOpenQueryResp\":{\"currentUpdateTime\":\"2024-12-06 09:41:13\",\"nextUpdateTime\":\"2024-12-06 09:42:04\",\"tenantId\":2268,\"companyId\":113757,\"size\":2},\"saleOpenInfoResp\":[{\"orderCode\":\"DXSA241206000244\",\"orderSource\":2,\"orderType\":4,\"orderLabel\":null,\"orderLabelStr\":null,\"deliveryType\":0,\"deliveryTypeDesc\":\"\",\"customerName\":\"深圳市龙华区大浪街道兴旺汽车服务中心\",\"customerCode\":\"R831695\",\"customerSubCode\":\"S31695C\",\"naCustomerName\":null,\"orderStatus\":30,\"orderStatusStr\":\"待确认\",\"sellerName\":\"雷鸣\",\"businessName\":\"Continental-广东\",\"nowTotalAmount\":630.00,\"rebateAmount\":630.00,\"freightAmount\":null,\"onlinePaidAmount\":0.00,\"creditAmount\":0.00,\"warehouseName\":\"深圳宝安南山仓库\",\"warehouseCode\":\"WHA00000760\",\"warehouseProvince\":\"广东省\",\"warehouseCity\":\"深圳市\",\"warehouseDistrict\":\"宝安区\",\"warehouseAddress\":\"深圳市中泓机电有限公司\",\"saleOpenPromotionAmountList\":[{\"couponAmount\":0.00,\"deductionPromotionAmount\":0,\"cashPromotionAmount\":0}],\"submitTime\":\"2024-12-06 09:41:13\",\"remark\":\"\",\"consigneeAddress\":\"深圳市龙华区大浪街道高峰社区华荣路39号科源大厦106\",\"consigneeName\":\"庄娜容\",\"consigneeMobile\":\"13823387738\",\"distributorSoldTo\":\"7715822\",\"deliveryTime\":null,\"saleSkuOpenInfoResp\":[{\"skuName\":\"215/55R17 94W FR UC7 #\",\"skuCode\":\"0312050\",\"sellUnitName\":\"条\",\"skuNum\":1.00,\"nowUnitPrice\":630.00,\"originalUnitPrice\":630.00,\"totalAmount\":630.00}],\"saleShippingOpenInfoResp\":[]},{\"orderCode\":\"DXSA241206000248\",\"orderSource\":5,\"orderType\":6,\"orderLabel\":\"3\",\"orderLabelStr\":\"京东T+1\",\"deliveryType\":1,\"deliveryTypeDesc\":\"经销商配送\",\"customerName\":\"大陆马牌轮胎（中国）有限公司\",\"customerCode\":\"CSR110809\",\"customerSubCode\":\"ZKHA00038396\",\"naCustomerName\":\"京东-【京东养车】升平大道店\",\"orderStatus\":40,\"orderStatusStr\":\"待发货\",\"sellerName\":\"\",\"businessName\":\"Continental-广东\",\"nowTotalAmount\":2080.01,\"rebateAmount\":0.00,\"freightAmount\":null,\"onlinePaidAmount\":0.00,\"creditAmount\":0.00,\"warehouseName\":\"南沙仓库\",\"warehouseCode\":\"WHA00000340\",\"warehouseProvince\":\"广东省\",\"warehouseCity\":\"广州市\",\"warehouseDistrict\":\"南沙区\",\"warehouseAddress\":\"好来运物流有限公司\",\"saleOpenPromotionAmountList\":[{\"couponAmount\":0,\"deductionPromotionAmount\":0,\"cashPromotionAmount\":0}],\"submitTime\":\"2024-12-06 09:42:03\",\"remark\":\"\",\"consigneeAddress\":\"广东珠海市金湾区平沙镇升平大道东328号六号厂房一楼101号\",\"consigneeName\":\"黄志华+1******2159\",\"consigneeMobile\":\"17820056307-3259(消费者)/18165552225(门店)\",\"distributorSoldTo\":\"7715822\",\"deliveryTime\":null,\"saleSkuOpenInfoResp\":[{\"skuName\":\"205/60R16 96V XL FR UC7 #\",\"skuCode\":\"0312038\",\"sellUnitName\":\"条\",\"skuNum\":4.00,\"nowUnitPrice\":520.00,\"originalUnitPrice\":520.00,\"totalAmount\":2080.00}],\"saleShippingOpenInfoResp\":[{\"shippingCode\":\"FXSA241206000477\",\"shippingStatus\":0,\"shippingStatusStr\":\"待发货\",\"warehouseName\":\"南沙仓库\",\"warehouseCode\":\"WHA00000340\",\"logisticsNo\":\"\",\"receiveTime\":null,\"shippingRemark\":\"\",\"shippingTime\":null,\"tmsArrivalTime\":null,\"shippingMethods\":0,\"shippingMethodsDesc\":\"\",\"saleShippingOpenInfoItemResp\":[{\"skuName\":\"205/60R16 96V XL FR UC7 #\",\"skuCode\":\"0312038\",\"sellUnitName\":\"条\",\"applyNum\":4.00,\"actualNum\":0.00}]}]}]}}";
            ContiOrderEntity contiOrder = JsonConvert.DeserializeObject<ContiOrderEntity>(contistr);
            //机器人账号
            LogEntity log = new LogEntity();
            log.IPAddress = "";
            log.Moudle = "服务管理";
            log.Status = "0";
            log.NvgPage = "定单推送服务";
            log.UserID = "2069";
            log.Operate = "A";
            List<saleOpenInfoResp> conti = new List<saleOpenInfoResp>();
            if (contiOrder.code.Equals(10000))
            {
                if (contiOrder.data.saleOpenInfoResp.Count > 0)
                {
                    foreach (var saleorder in contiOrder.data.saleOpenInfoResp)
                    {
                        if (!saleorder.orderType.Equals("1") && !saleorder.orderType.Equals("3") && !saleorder.orderType.Equals("6") && !saleorder.orderType.Equals("4"))
                        {
                            // 1   普通订单 3   调拨订单 6   NA订单  4   加急配送订单 5   长尾订单 7   resell订单
                            continue;
                        }
                        //1. 根据仓库代码查询该仓库的库存并生成该仓库订单
                        //2. 根据客户编码查询客户数据是否存在，不存在新增。
                        //saleorder.customerCode
                        //saleorder.warehouseCode
                        if (string.IsNullOrEmpty(saleorder.warehouseCode))
                        {
                            //写日志 没有出库仓库。
                            //LogHelper.WriteLog(saleorder.orderCode + " " + saleorder.warehouseName + "无出库仓库");
                            continue;
                        }
                        CargoAreaEntity areaEntity = houseBus.QueryHouseByDeliveryArea(new CargoAreaEntity { ContiHouseCode = saleorder.warehouseCode });
                        if (areaEntity.AreaID.Equals(0))
                        {
                            //写日志 没有配置仓库
                            //LogHelper.WriteLog(saleorder.orderCode + " " + saleorder.warehouseName + "无配置出库仓库");
                            continue;
                        }
                        List<CargoClientEntity> clientEntities = clientBus.QueryCargoClientList(new CargoClientEntity { TryeClientCode = saleorder.customerCode, DelFlag = "0", HouseIDStr = "" });
                        if (clientEntities.Count > 0)
                        {
                            saleorder.BidClientNum = clientEntities[0].ClientNum;
                            saleorder.BidClientName = clientEntities[0].ClientName;
                        }
                        saleorder.AreaID = areaEntity.AreaID;
                        saleorder.HouseID = areaEntity.HouseID;
                        saleorder.InCreateStatus = "0";
                        saleorder.OrderPiece = saleorder.saleSkuOpenInfoResp.Sum(c => c.skuNum);
                        saleorder.freightAmount = string.IsNullOrEmpty(saleorder.freightAmount) ? "0" : saleorder.freightAmount;
                        if (saleorder.saleOpenPromotionAmountList.Count > 0)
                        {
                            saleorder.couponAmount = saleorder.saleOpenPromotionAmountList[0].couponAmount;
                        }
                        conti.Add(saleorder);
                    }
                }
                nwBus.AddContiSaleOrderInfo(conti, log);

                nwBus.AddContiOrderPushInfo(new saleOrderStatusOpenQueryResp { currentUpdateTime = contiOrder.data.saleOrderStatusOpenQueryResp.currentUpdateTime, companyId = contiOrder.data.saleOrderStatusOpenQueryResp.companyId, tenantId = contiOrder.data.saleOrderStatusOpenQueryResp.tenantId, nextUpdateTime = contiOrder.data.saleOrderStatusOpenQueryResp.nextUpdateTime, ResJson = contistr }, log);
            }
        }
        private void sds()
        {
            CargoInterfaceBus interBus = new CargoInterfaceBus();
            string contiUrl = "https://cdms.continental-tires.cn/api/openapi/stock/dotSync";
            //马牌仓库名称字典
            Dictionary<string, string> dicContiHouseName = new Dictionary<string, string>();

            dicContiHouseName.Add("138", "肇庆仓库");

            foreach (var conti in dicContiHouseName)
            {
                CargoContiStockSyncEntity stockSync = new CargoContiStockSyncEntity();
                stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                List<CargoContiStockListEntity> stockList = new List<CargoContiStockListEntity>();
                List<CargoContiStockSKUEntity> sKUEntities = new List<CargoContiStockSKUEntity>();
                if (conti.Key.Equals("45"))
                {
                    stockSync = new CargoContiStockSyncEntity();
                    stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                    stockList = new List<CargoContiStockListEntity>();
                    sKUEntities = new List<CargoContiStockSKUEntity>();
                    List<CargoSafeStockEntity> stockEntities = interBus.QueryContiStockData(new CargoSafeStockEntity { HouseID = conti.Key });
                    if (stockEntities.Count > 0)
                    {
                        foreach (var stock in stockEntities)
                        {
                            if (string.IsNullOrEmpty(stock.GoodsCode) || stock.GoodsCode.Length < 7) { continue; }
                            if (stock.AreaID.Equals(3446))
                            {
                                //众汇前置仓马牌和维京
                                if (sKUEntities.Exists(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7))))
                                {
                                    CargoContiStockSKUEntity existEntity = sKUEntities.Find(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7)));
                                    existEntity.dots.Add(new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum });
                                }
                                else
                                {
                                    sKUEntities.Add(new CargoContiStockSKUEntity
                                    {
                                        skuCode = stock.GoodsCode.Substring(0, 7),
                                        dots = new List<CargoContiStockDOTEntity> { new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum } }
                                    }); ;
                                }
                            }
                        }
                    }
                    //众汇前置仓
                    stockList.Add(new CargoContiStockListEntity
                    {

                        warehouseName = conti.Value,// "深圳龙岗众汇前置仓",
                        skuQtyList = sKUEntities
                    });

                    stockSync.stockList = stockList;
                    String body = JSON.Encode(stockSync);
                    try
                    {
                        string res = wxHttpUtility.ContiSendPostHttpRequest(contiUrl, "application/json", body);
                        //LogHelper.WriteLog("众汇前置仓和深圳马牌维京库存同步成功");
                    }
                    catch (ApplicationException ex)
                    {
                        //LogHelper.WriteLog("众汇前置仓和深圳马牌维京库存同步失败");
                    }
                }
                else
                {

                    //广州仓,东平云仓，南沙，从化，花都，肇庆,106,107,129,131
                    stockSync = new CargoContiStockSyncEntity();
                    stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                    stockList = new List<CargoContiStockListEntity>();
                    sKUEntities = new List<CargoContiStockSKUEntity>();
                    List<CargoSafeStockEntity> hnStock = interBus.QueryGZContiStockData(new CargoSafeStockEntity { HouseIDStr = conti.Key });
                    if (hnStock.Count > 0)
                    {
                        foreach (var stock in hnStock)
                        {
                            if (string.IsNullOrEmpty(stock.GoodsCode) || stock.GoodsCode.Length < 7) { continue; }
                            //广州仓马牌

                            if (sKUEntities.Exists(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7))))
                            {
                                CargoContiStockSKUEntity existEntity = sKUEntities.Find(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7)));
                                existEntity.dots.Add(new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum });
                                //existEntity.qty += stock.StockNum;
                            }
                            else
                            {
                                sKUEntities.Add(new CargoContiStockSKUEntity
                                {
                                    skuCode = stock.GoodsCode.Substring(0, 7),
                                    dots = new List<CargoContiStockDOTEntity> { new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum } }
                                    //qty = stock.StockNum
                                });
                            }
                        }
                    }
                    stockList.Add(new CargoContiStockListEntity
                    {
                        warehouseName = conti.Value,//"广州仓",
                        skuQtyList = sKUEntities
                    });
                    stockSync.stockList = stockList;
                    string body = JSON.Encode(stockSync);
                    try
                    {
                        string res = wxHttpUtility.ContiSendPostHttpRequest(contiUrl, "application/json", body);
                        //LogHelper.WriteLog(conti.Value + "马牌维京库存同步成功");
                    }
                    catch (ApplicationException ex)
                    {
                        //LogHelper.WriteLog(conti.Value + "马牌维京库存同步失败");
                    }
                }

            }


        }
        private void AddTMSAwbNo(CargoOrderPushEntity it)
        {
            LogEntity log = new LogEntity();
            log.IPAddress = "";
            log.Moudle = "服务管理";
            log.Status = "0";
            log.NvgPage = "定单推送服务";
            log.UserID = "2009";
            log.Operate = "A";
            CargoOrderBus order = new CargoOrderBus();

            CargoOrderEntity OEnt = order.QueryOrderInfo(new CargoOrderEntity { OrderNo = it.OrderNo });
            if (OEnt.OrderID.Equals(0)) { return; }
            if (OEnt.OutHouseName.Equals("狄乐云仓")) { return; }

            List<CargoContainerShowEntity> OEntGoods = order.QueryOrderByOrderNo(new CargoOrderEntity { OrderNo = it.OrderNo });
            string ShipperName = string.Empty;
            string ShipperUnit = string.Empty;
            string ShipperCellphone = string.Empty;
            string ShipperAddress = string.Empty;
            string Belong = "CP";
            decimal UnitP = 0.00M;
            string De = "";
            UnitP = 15;
            ShipperName = "邱小彬";
            ShipperUnit = "新陆城配";
            ShipperCellphone = "13631442958";
            ShipperAddress = "广州市白云区东平北路";
            string ClientNum = "";
            if (it.OrderNo.Contains("NG"))
            {
                //南宁批发仓 推送到南宁分公司TMS系统 12元一条
                Belong = "NN";
                De = "南宁";
                UnitP = 0;
                ShipperName = "南宁分公司";
                ShipperUnit = "南宁分公司";
                ShipperCellphone = "13113333081";
                ShipperAddress = "广西南宁市西乡塘区南武大道永宁小学背后容顺物流园右侧一栋5-6号";
                ClientNum = "132208";
            }
            else if (it.OrderNo.Contains("YZ"))
            {
                //增城云仓
                De = "广州";
                string[] strings = { "揭阳", "汕头", "潮州", "梅州", "汕尾", "普宁", "河源", "茂名", "湛江", "阳江" };

                if (!OEnt.Province.Equals("广东"))
                {
                    De = OEnt.Dest;
                }
                UnitP = 0;
                ShipperName = "广州迪乐泰";
                ShipperUnit = "广州迪乐泰";
                ShipperCellphone = "13265180164";
                ShipperAddress = "增城区广百骏盈现代物流园";
            }

            decimal hz = Convert.ToDecimal(UnitP);//轮胎运输费用汇总


            #region 明细
            string go = "[";
            if (OEntGoods.Count > 0)
            {
                foreach (var itt in OEntGoods)
                {
                    //go += "{\"Goods\":\"" + itt.TypeName + itt.Specs + itt.Figure + "\",\"Piece\":\"" + itt.Piece.ToString() + "\",\"OPDATE\":\"" + itt.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss") + "\"},";

                    go += "{\"Goods\":\"" + itt.TypeName + " " + itt.Specs + " " + itt.Figure + " " + itt.LoadIndex + itt.SpeedLevel + "\",\"Package\":\"无\",\"Piece\":\"" + itt.Piece.ToString() + "\",\"PiecePrice\":\"" + UnitP + "\",\"OP_DATE\":\"" + itt.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"ProductName\":\"" + itt.ProductName + "\",\"Model\":\"" + itt.Model + "\",\"GoodsCode\":\"" + itt.GoodsCode + "\",\"Specs\":\"" + itt.Specs + "\",\"Figure\":\"" + itt.Figure + "\",\"LoadIndex\":\"" + itt.LoadIndex + "\",\"SpeedLevel\":\"" + itt.SpeedLevel + "\",\"Batch\":\"" + itt.Batch + "\",\"ContainerCode\":\"" + itt.ContainerCode + "\",\"TypeName\":\"" + itt.TypeName + "\"},";
                }
                go = go.Substring(0, go.Length - 1);
            }
            go += "]";
            #endregion

            string ss = "{\"HAwbNo\":\"" + OEnt.OrderNo + "\",\"Dep\":\"" + OEnt.Dep + "\",\"Dest\":\"" + De + "\",\"Transit\":\"" + OEnt.Dest + "\",\"Piece\":\"" + OEnt.Piece.ToString() + "\",\"AwbPiece\":\"" + OEnt.Piece.ToString() + "\",\"InsuranceFee\":\"0\",\"TransportFee\":\"" + hz.ToString("F2") + "\",\"OtherFee\":\"0\",\"TotalCharge\":\"" + hz.ToString("F2") + "\",\"ShipperName\":\"" + ShipperName + "\",\"ShipperUnit\":\"" + ShipperUnit + "\",\"ShipperTelephone\":\"" + ShipperCellphone + "\",\"ShipperCellphone\":\"" + ShipperCellphone + "\",\"ShipperAddress\":\"" + ShipperAddress + "\",\"AcceptUnit\":\"" + OEnt.AcceptUnit + "\",\"AcceptPeople\":\"" + OEnt.AcceptPeople + "\",\"AcceptTelephone\":\"" + OEnt.AcceptTelephone + "\",\"AcceptCellphone\":\"" + OEnt.AcceptCellphone + "\",\"AcceptAddress\":\"" + OEnt.AcceptAddress + "\",\"HandleTime\":\"" + OEnt.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"Remark\":\"" + OEnt.Remark + "\",\"ClientNum\":\"" + ClientNum + "\",\"Goods\":" + go + "}";

            string data = "cmd=SaveTmsAwbNoData&OrderList=[" + ss + "]&Belong=" + Belong;
            string xlcawbno = wxHttpUtility.SendHttpRequest("http://tms.hlyex.com/webSer/tmsAPI.ashx", data);
            if (!string.IsNullOrEmpty(xlcawbno))
            {
                ArrayList GridRows = (ArrayList)JSON.Decode("[" + xlcawbno + "]");
                foreach (Hashtable row in GridRows)
                {
                    if (Convert.ToBoolean(row["Result"]))
                    {
                        //更新新陆程 运单 号回仓储系统订单
                        if (OEnt.LogisID.Equals(283) || OEnt.LogisID.Equals(383))
                        {
                            order.UpdateLogisAwbNo(it.OrderNo, Convert.ToString(row["Message"]), "", log);
                        }
                        else
                        {
                            order.UpdateHAwbNo(it.OrderNo, Convert.ToString(row["Message"]), log);
                        }
                        order.UpdateCargoOrderPushAwbNo(new CargoOrderPushEntity { OrderNo = it.OrderNo, AwbNo = Convert.ToString(row["Message"]), PushStatus = "1" }, log);
                    }
                }

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CargoOrderBus ordrBus = new CargoOrderBus();

            //销售单创建后库存变少，检查是否需要创建补货单
            List<UpdateOOSGoodsParam> rplGoodsList = new List<UpdateOOSGoodsParam>();
            UpdateOOSParam oosParams = new UpdateOOSParam()
            {
                UserID = "3345",
                UserName = "胡忠俊",
                ReasonTag = "SO", //销售单标记
                SrcType = 1,
                SrcID = 1371432,
                SrcCode = "YJ250929082",
                Rows = new List<UpdateOOSGoodsParam>()
                {
                    new UpdateOOSGoodsParam()
                    {
                        ProductID = 938921,
                        AreaID = 3677,
                    },
                    new UpdateOOSGoodsParam()
                    {
                        ProductID = 941319,
                        AreaID = 3677,
                    },
                    new UpdateOOSGoodsParam()
                    {
                        ProductID = 943204,
                        AreaID = 3677,
                    },
                }
            };
            ordrBus.TryUpdateOutOfStock(oosParams);
            return;

            ordrBus.GenerateDailySalesSnapshot();
            return;

            // RedisHelper.HashSet("HCYCHouseStockSyc", "93_34_LTCT215551603", "LTCT215551603");
            // RedisHelper.HashSet("HCYCHouseStockSyc", "93_34_LTCT225551801", "LTCT225551801");
            CargoInterfaceBus bus = new CargoInterfaceBus();
            List<CargoProductEntity> testList = bus.QueryNextDayStockSync(new CargoProductEntity {  });

            bus.SaveNextDayProductData(testList);
            return;




            RedisValue[] redisValues = RedisHelper.HashKeys("NextDayOrderShareSync");
            foreach (RedisValue redisValue in redisValues)
            {
                string key = redisValue.ToString();//ST250729043_101_93   订单key
                string goodsList = Convert.ToString(RedisHelper.HashGet("NextDayOrderShareSync", key));//订单明细

                // #region 增量同步共享仓库存至前置仓
                // //以TypeID为Key，ProductCode为Value
                // Dictionary<int, string> DPdicCass = new Dictionary<int, string>();
                // Dictionary<int, string> SJdicCass = new Dictionary<int, string>();
                // string StProductCode = string.Empty;
                // RedisValue[] HCYCredisValues = RedisHelper.HashKeys("HCYCHouseStockSyc");
                // foreach (RedisValue redisValue in HCYCredisValues)
                // {
                //     string key = redisValue.ToString();//93_34_LTCT245452005
                //     string ProductCodeV = Convert.ToString(RedisHelper.HashGet("HCYCHouseStockSyc", key));
                //     int HouseID = Convert.ToInt32(key.Split('_')[0]);
                //     //品牌ID
                //     int TypeID = Convert.ToInt32(key.Split('_')[1]);
                //     //产品编码
                //     string ProductCode = Convert.ToString(key.Split('_')[2]);

                //     //处理东平云仓和顺捷云仓库存数据
                //     if (HouseID.Equals(93))
                //     {
                //         if (DPdicCass.ContainsKey(TypeID))
                //         {
                //             // 获取当前Key的内容
                //             string currentValue = DPdicCass[TypeID];

                //             // 更新内容
                //             string updatedValue = currentValue + "','" + ProductCode;
                //             DPdicCass[TypeID] = updatedValue;
                //         }
                //         else
                //         {
                //             DPdicCass.Add(TypeID, ProductCode);
                //         }
                //     }
                //     else if (HouseID.Equals(100))
                //     {
                //         if (SJdicCass.ContainsKey(TypeID))
                //         {
                //             // 获取当前Key的内容
                //             string currentValue = SJdicCass[TypeID];

                //             // 更新内容
                //             string updatedValue = currentValue + "','" + ProductCode;
                //             SJdicCass[TypeID] = updatedValue;
                //         }
                //         else
                //         {
                //             SJdicCass.Add(TypeID, ProductCode);
                //         }
                //     }
                // }

                // //开始同步东平仓增量库存
                // foreach (var cass in DPdicCass)
                // {
                //     List<CargoProductEntity> entities = bus.QueryNextDayStockSync(new CargoProductEntity { HouseID = 93, TypeID = cass.Key, ProductCode = cass.Value });
                //     bus.SaveNextDayProductData(entities);
                // }
                // //开始同步顺捷仓增量库存
                // foreach (var cass in SJdicCass)
                // {
                //     List<CargoProductEntity> entities = bus.QueryNextDayStockSync(new CargoProductEntity { HouseID = 100, TypeID = cass.Key, ProductCode = cass.Value });
                //     bus.SaveNextDayProductData(entities);
                // }

                // RedisHelper.KeyDelete("HCYCHouseStockSyc");
                //// LogHelper.WriteLog("HCYCHouseStockSyc缓存删除成功");
                // #endregion

                //ScanContiQRCode();
                //double distance = Common.CalculateDistance(Convert.ToDouble(108.33921460623363), Convert.ToDouble(22.873639278749174), Convert.ToDouble(116.35922045622952), Convert.ToDouble(40.06789579773766));
                //string ss = ";";
                ////机器人账号
                //LogEntity log = new LogEntity();
                //log.IPAddress = "";
                //log.Moudle = "服务管理";
                //log.Status = "0";
                //log.NvgPage = "定单推送服务";
                //log.UserID = "2029";
                //log.Operate = "A";





                List<CargoProductEntity> entities = bus.QueryNextDayStockSync(new CargoProductEntity { HouseID = 93 });

                bus.SaveNextDayProductData(entities);

                string sss = ";";

                //RedisValue[] redisValues = RedisHelper.HashKeys("NextDayOrderShareSync");
                //foreach (RedisValue redisValue in redisValues)
                //{
                //    string key = redisValue.ToString();//ST250729043_101_93   订单key
                //    string goodsList = Convert.ToString(RedisHelper.HashGet("NextDayOrderShareSync", key));//订单明细

                //    string oriOrderNo = Convert.ToString(key.Split('_')[0]);
                //    int oriHouseID = Convert.ToInt32(key.Split('_')[1]);
                //    int ShareHouseID = Convert.ToInt32(key.Split('_')[2]);
                //    //保存生成仓库订单
                //    List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
                //    List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();

                //    CargoOrderEntity ent = orderBus.QueryOrderInfo(new CargoOrderEntity { OrderNo = oriOrderNo });

                //    if (!ent.CheckStatus.Equals("1"))
                //    {
                //        //如果次日达订单没有支付就跳过
                //        continue;
                //    }

                //    int OrderNum = 0;

                //    CargoHouseEntity houseEnt = house.QueryCargoHouseByID(ShareHouseID);
                //    ent.HouseID = ShareHouseID;
                //    ent.OrignHouseID = oriHouseID;
                //    ent.OrignHouseName = "";
                //    ent.ShareHouseID = 0;
                //    ent.ShareHouseName = "";
                //    ent.Dep = houseEnt.DepCity;
                //    ent.OpenOrderNo = oriOrderNo;
                //    ent.LogisID = houseEnt.HouseID.Equals(97) || houseEnt.HouseID.Equals(95) || houseEnt.HouseID.Equals(101) ? 62 : houseEnt.HouseID.Equals(136) ? 383 : 34;
                //    ent.CreateDate = DateTime.Now;
                //    string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
                //    ent.OrderNo = Common.GetMaxOrderNumByCurrentDate(houseEnt.HouseID, houseEnt.HouseCode, out OrderNum);
                //    ent.OutHouseName = houseEnt.Name;
                //    ent.OrderNum = OrderNum;



                //    List<CargoInterfaceEntity> goods = new List<CargoInterfaceEntity>();
                //    ArrayList rows = (ArrayList)JSON.Decode(goodsList);
                //    decimal originalPrice = 0M;
                //    foreach (Hashtable row in rows)
                //    {
                //        int piece = Convert.ToInt32(row["StockNum"]);
                //        decimal ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]);
                //        CargoInterfaceEntity queryEntity = new CargoInterfaceEntity
                //        {
                //            ProductCode = Convert.ToString(row["ProductCode"]),
                //            HouseID = houseEnt.HouseID,
                //            //TypeID = productBasic[0].TypeID,
                //            BatchYear = Convert.ToInt32(row["BatchYear"]),
                //            ParentID = 1,
                //            SuppClientNum = ent.SuppClientNum.ToString(),
                //            SpecsType = "4",
                //        };
                //        List<CargoInterfaceEntity> stockList = bus.queryCargoStock(queryEntity);
                //        if (stockList.Count <= 0)
                //        {
                //            //LogHelper.WriteLog("商品库存不足");
                //        }
                //        if (stockList.Sum(c => c.StockNum) < piece)
                //        {
                //            //LogHelper.WriteLog(productBasic[0].Specs + " " + productBasic[0].Figure + "库存不足");
                //        }
                //        //减库存规则，一周期早的先出先进先出，二数量和库存数刚好一样的先出
                //        foreach (var it in stockList)
                //        {
                //            if (it.StockNum <= 0) { continue; }

                //            CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                //            cargo.OrderNo = ent.OrderNo;//订单号
                //            cargo.OutCargoID = outID;
                //            cargo.ContainerID = it.ContainerID;
                //            cargo.TypeID = it.TypeID;
                //            cargo.ProductID = it.ProductID;

                //            cargo.ID = it.ContainerGoodsID;//库存表ID

                //            //cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
                //            cargo.HouseName = houseEnt.Name;
                //            //cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
                //            cargo.ProductName = it.ProductName;
                //            cargo.Model = it.Model;
                //            cargo.Specs = it.Specs;
                //            cargo.Figure = it.Figure;
                //            int inHouseDay = (int)(DateTime.Now - it.InHouseTime).TotalDays;
                //            int OverDay = 0;
                //            decimal OnlyOverDayFee = 0;

                //            #region 减库存逻辑
                //            if (piece < it.StockNum)
                //            {
                //                //部分出
                //                entDest.Add(new CargoOrderGoodsEntity
                //                {
                //                    OrderNo = ent.OrderNo,
                //                    ProductID = it.ProductID,
                //                    HouseID = ent.HouseID,
                //                    AreaID = it.AreaID,
                //                    Piece = piece,
                //                    //ActSalePrice = it.SalePrice,
                //                    ActSalePrice = ActSalePrice,
                //                    SupplySalePrice = it.InHousePrice,
                //                    ContainerCode = it.ContainerCode,
                //                    OutCargoID = outID,
                //                    OP_ID = log.UserID,
                //                    OverDayNum = OverDay,
                //                    OverDueFee = OnlyOverDayFee,
                //                });

                //                cargo.Piece = piece;
                //                cargo.InPiece = it.StockNum;
                //                originalPrice += piece * it.InHousePrice;//计算成本价
                //                outHouseList.Add(cargo);
                //                break;
                //            }
                //            if (piece.Equals(it.StockNum))
                //            {
                //                //要出库件数和第一条库存件数刚刚好，则就全部出
                //                entDest.Add(new CargoOrderGoodsEntity
                //                {
                //                    OrderNo = ent.OrderNo,
                //                    ProductID = it.ProductID,
                //                    HouseID = ent.HouseID,
                //                    AreaID = it.AreaID,
                //                    Piece = piece,
                //                    //ActSalePrice = it.SalePrice,
                //                    SupplySalePrice = it.InHousePrice,
                //                    ActSalePrice = ActSalePrice,
                //                    ContainerCode = it.ContainerCode,
                //                    OutCargoID = outID,

                //                    OP_ID = log.UserID,
                //                    OverDayNum = OverDay,
                //                    OverDueFee = OnlyOverDayFee,
                //                });
                //                cargo.Piece = piece;
                //                cargo.InPiece = it.StockNum;
                //                originalPrice += piece * it.InHousePrice;//计算成本价

                //                outHouseList.Add(cargo);
                //                break;
                //            }
                //            if (piece > it.StockNum)
                //            {
                //                //全部出
                //                entDest.Add(new CargoOrderGoodsEntity
                //                {
                //                    OrderNo = ent.OrderNo,
                //                    ProductID = it.ProductID,
                //                    HouseID = ent.HouseID,
                //                    AreaID = it.AreaID,
                //                    Piece = it.StockNum,
                //                    //ActSalePrice = it.SalePrice,
                //                    SupplySalePrice = it.InHousePrice,
                //                    ActSalePrice = ActSalePrice,
                //                    ContainerCode = it.ContainerCode,
                //                    OutCargoID = outID,

                //                    OP_ID = log.UserID,
                //                    OverDayNum = OverDay,
                //                    OverDueFee = OnlyOverDayFee,
                //                });
                //                cargo.Piece = it.StockNum;
                //                cargo.InPiece = it.StockNum;
                //                originalPrice += it.StockNum * it.InHousePrice;//计算成本价
                //                outHouseList.Add(cargo);
                //                piece = piece - it.StockNum;
                //                continue;
                //            }
                //            #endregion
                //        }
                //    }

                //    ent.OpenOrderSource = "1";
                //    ent.goodsList = entDest;

                //    //保存生成仓库出库订单
                //    orderBus.AddOrderInfo(ent, outHouseList, log);


                //    //更新原仓库订单的关联订单
                //    bus.UpdateOrderOpenNo(new CargoOrderEntity { HouseID = oriHouseID, OrderNo = oriOrderNo, OpenOrderNo = ent.OrderNo });

                //    //删除已经保存订单成功的Key
                //    RedisHelper.HashDelete("NextDayOrderShareSync", key);

                //    //LogHelper.WriteLog("订单：" + oriOrderNo + "保存成功");
                //}



                //#region 南宁云仓库存同步开思
                //try
                //{
                //    List<StockApiEntity> res = bus.QueryYunCangStockSync(new CargoProductEntity { HouseID = 136 });
                //    StockApiResponseEntity entity = new StockApiResponseEntity();
                //    entity.Params = new Params();
                //    entity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                //    entity.data = new List<Data>();

                //    if (res.Count > 0)
                //    {
                //        entity.Params.firstData = true;
                //        entity.Params.lastData = false;
                //        entity.Params.updateMode = "ALL";
                //        long tpsBatchId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                //        string strRes = string.Empty;
                //        entity.Params.tpsBatchId = tpsBatchId;
                //        int cou = 0;
                //        for (int i = 0; i < res.Count; i++)
                //        {
                //            if (cou == 500)
                //            {
                //                cou = 0;
                //                strRes = wxHttpUtility.PostHttpRequest("https://api.cassmall.com/api", "application/json", JSON.Encode(entity).Replace("Params", "params").Replace("thirdPartySystemparams", "thirdPartySystemParams"), "2b2b36889e304645b28f94bfdd2ead9e");

                //                entity = new StockApiResponseEntity();
                //                entity.Params = new Params();
                //                entity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                //                entity.data = new List<Data>();
                //                if (strRes.IndexOf("200") >= 0)
                //                {
                //                    //tpsBatchId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                //                    entity.Params.tpsBatchId = tpsBatchId;
                //                    entity.Params.firstData = false;
                //                    entity.Params.lastData = false;
                //                    entity.Params.updateMode = "ALL";
                //                }
                //            }

                //            string casscode = res[i].CassProductCode;
                //            if (res[i].TypeID.Equals(34) || res[i].TypeID.Equals(345) || res[i].TypeID.Equals(163))
                //            {
                //                casscode = res[i].GoodsCode;
                //            }
                //            else
                //            {
                //                casscode = string.IsNullOrEmpty(res[i].CassProductCode) ? res[i].ProductCode : res[i].CassProductCode;

                //            }
                //            string typename = res[i].TypeName;
                //            if (res[i].TypeID.Equals(9))
                //            {
                //                typename = "横滨/优科豪马";
                //            }
                //            else if (res[i].TypeID.Equals(66))
                //            {
                //                typename = "固铂轮胎";
                //            }
                //            decimal saleprice = 0.00M;

                //            //按百分比
                //            saleprice = Math.Ceiling(res[i].SalePrice * (1 + 1 / 100));

                //            entity.data.Add(new Data
                //            {
                //                CassProductCode = casscode,
                //                ProductName = res[i].ProductName,
                //                TypeName = typename,
                //                SalePrice = saleprice,
                //                StockNum = res[i].StockNum,
                //                Batch = res[i].Batch,
                //                HouseID = res[i].HouseID,
                //            });
                //            //LogHelper.WriteLog("南宁云仓开思全量推送ID:" + casscode + "，库存：" + res[i].StockNum.ToString() + "，周期：" + res[i].Batch + ",本次推送金额:" + saleprice.ToString());
                //            cou++;

                //        }
                //        entity.Params.lastData = true;
                //        strRes = wxHttpUtility.PostHttpRequest("https://api.cassmall.com/api", "application/json", JSON.Encode(entity).Replace("Params", "params").Replace("thirdPartySystemparams", "thirdPartySystemParams"), "2b2b36889e304645b28f94bfdd2ead9e");

                //        if (strRes.IndexOf("200") >= 0)
                //        {
                //            //bus.UpdateStatus(res);
                //            //LogHelper.WriteLog("南宁云仓开思推送成功!共计" + res.Count + "条数据");
                //        }
                //    }

                //}
                //catch (Exception ex)
                //{
                //    //LogHelper.WriteLog("南宁云仓开思推送出现错误!Message" + ex.Message);
                //    throw;
                //}
                //#endregion


                //SendBillMessage();
                //Common.SendRefundModelMsg("1232", "250105165949189", 12,"beizhu");
                //Common.SendRePlaceAnOrderMsg("542207", "test001", "Order001", "戴尔笔记本",8000,20);

                //SendTemplateMessage();

                //SyncStock();

                //GetDongShunTyreData();

                //CargoInterfaceBus interBus = new CargoInterfaceBus();

                //CargoContiStockSyncEntity stockSync = new CargoContiStockSyncEntity();
                //stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                //List<CargoContiStockListEntity> stockList = new List<CargoContiStockListEntity>();
                //List<CargoContiStockSKUEntity> LHYPsKUEntities = new List<CargoContiStockSKUEntity>();//龙华云配仓
                ////深圳龙华云配仓库
                //stockSync = new CargoContiStockSyncEntity();
                //stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                //stockList = new List<CargoContiStockListEntity>();
                //List<CargoSafeStockEntity> ypStock = interBus.QueryGZContiStockData(new CargoSafeStockEntity { HouseIDStr = "9,93" });
                //if (ypStock.Count > 0)
                //{
                //    foreach (var stock in ypStock)
                //    {
                //        if (string.IsNullOrEmpty(stock.GoodsCode) || stock.GoodsCode.Length < 7) { continue; }
                //        if (LHYPsKUEntities.Exists(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7))))
                //        {
                //            CargoContiStockSKUEntity existEntity = LHYPsKUEntities.Find(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7)));
                //            existEntity.dots.Add(new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum });
                //            //existEntity.qty += stock.StockNum;
                //        }
                //        else
                //        {
                //            LHYPsKUEntities.Add(new CargoContiStockSKUEntity
                //            {
                //                skuCode = stock.GoodsCode.Substring(0, 7),
                //                dots = new List<CargoContiStockDOTEntity> { new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum } }
                //                //qty = stock.StockNum
                //            });
                //        }
                //    }
                //}
                //stockList.Add(new CargoContiStockListEntity
                //{
                //    warehouseName = "广州仓",
                //    skuQtyList = LHYPsKUEntities
                //});
                ////stockList.Add(new CargoContiStockListEntity
                ////{
                ////    warehouseName = "维京广州仓",
                ////    skuQtyList = gzWjEntity
                ////});
                //stockSync.stockList = stockList;
                //string body = JSON.Encode(stockSync);
                //try
                //{
                //    string contiUrlss = "https://cdms.continental-tires.cn/api/openapi/stock/dotSync";
                //    string res = wxHttpUtility.ContiSendPostHttpRequest(contiUrlss, "application/json", body);
                //    // LogHelper.WriteLog("深圳龙华云配仓库马牌维京库存同步成功");
                //}
                //catch (ApplicationException ex)
                //{
                //    // LogHelper.WriteLog("深圳龙华云配仓库马牌维京库存同步失败");
                //}



                //RedisHelper.HashSet("OpenSystemStockSyc", "93_34_LTCT245452005", "03118970000");



                //int djjg = (int)Math.Floor(154 / Convert.ToDecimal(1.009));

                //LogEntity log = new LogEntity();
                //log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                //log.Moudle = "慧采云仓";
                //log.Status = "0";
                //log.NvgPage = "小程序付款";
                //log.UserID = "MiniPro";
                //log.Operate = "U";
                //CargoWeiXinBus bus = new CargoWeiXinBus();
                //CargoOrderBus orderBus = new CargoOrderBus();
                //CargoClientBus clientBus = new CargoClientBus();
                //string out_trade_no = "24101209303138";
                //if (!bus.IsExistWeixinOrderPay(new WXOrderEntity { OrderNo = out_trade_no, PayStatus = "1" }))
                //{
                //    List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 5, new WXOrderEntity { OrderNo = out_trade_no });
                //    //1. 修改订单支付状态
                //    bus.UpdateWeixinOrderPayStatus(new WXOrderEntity { OrderNo = out_trade_no, WXPayOrderNo = "24101209161425511111", PayStatus = "1", CargoOrderNo = result[0].CargoOrderNo }, log);
                //    //扫描运单二维码支付成功返回的请求
                //    //1.修改订单的支付状态
                //    CargoFinanceBus fina = new CargoFinanceBus();
                //    //先审核
                //    List<CargoOrderEntity> oeL = new List<CargoOrderEntity>();
                //    oeL.Add(new CargoOrderEntity
                //    {
                //        OrderID = result[0].OrderID,
                //        OrderNo = result[0].CargoOrderNo,
                //        FinanceSecondCheck = "1",
                //        FinanceSecondCheckName = result[0].Name,
                //        FinanceSecondCheckDate = DateTime.Now
                //    });
                //    fina.plSecondCheckOrder(oeL, log);
                //    CargoOrderEntity ord = orderBus.QueryOrderInfo(new CargoOrderEntity { OrderNo = result[0].CargoOrderNo });

                //    //if (!ord.HouseID.Equals(93))
                //    //{
                //    //    orderBus.InsertCargoOrderPush(new CargoOrderPushEntity
                //    //    {
                //    //        OrderNo = ord.OrderNo,
                //    //        Dep = ord.Dep,
                //    //        Dest = ord.Dest,
                //    //        Piece = ord.Piece,
                //    //        TransportFee = ord.TransportFee,
                //    //        ClientNum = ord.ClientNum.ToString(),
                //    //        AcceptAddress = ord.AcceptAddress,
                //    //        AcceptCellphone = ord.AcceptCellphone,
                //    //        AcceptTelephone = ord.AcceptTelephone,
                //    //        AcceptPeople = ord.AcceptPeople,
                //    //        AcceptUnit = ord.AcceptUnit,
                //    //        HouseID = ord.HouseID.ToString(),
                //    //        HouseName = ord.OutHouseName,
                //    //        OP_ID = ord.OP_ID,
                //    //        PushType = "0",
                //    //        PushStatus = "0",
                //    //        LogisID = ord.LogisID
                //    //    }, log);
                //    //}

                //    //再支付
                //    string awbidlist = string.Empty;
                //    List<string> entOrderNo = new List<string>();
                //    //如果客户是微信付款，并已支付成功，则修改订单状态为已结算
                //    CargoCashierEntity ent = new CargoCashierEntity();
                //    ent.WxID = 337;//科技公司的微信商城微信支付收款账号ID
                //    ent.AffectWX = Convert.ToInt32(175) / 100;//微信 支付的金额

                //    ent.OP_ID = log.UserID;
                //    ent.UserName = log.UserID;
                //    ent.RType = "0";//收支类型，0收入1支出
                //    ent.FromTO = "0";//按订单号收款
                //    ent.TradeType = "3";//微信商城付款
                //    ent.CheckStatus = "1";
                //    ent.WxOrderNo = out_trade_no;
                //    entOrderNo.Add(result[0].CargoOrderNo);
                //    awbidlist += result[0].CargoOrderNo + ",";

                //    ent.ClientNum = result[0].ClientNum;
                //    ent.OrderNo = entOrderNo;
                //    ent.AffectAwbNO = awbidlist;
                //    fina.SaveCash(ent, log);
                //    Common.WriteTextLog("慧采云仓小程序 收款完成" + out_trade_no + "，订单号：" + result[0].CargoOrderNo);

                //    try
                //    {
                //        if (!ord.CheckOutType.Equals("3"))
                //        {
                //            string proStr = string.Empty;
                //            if (result[0].productList.Count > 0)
                //            {
                //                proStr = result[0].productList[0].ProductName;
                //            }

                //            CargoHouseBus house = new CargoHouseBus();
                //            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(ord.HouseID);
                //            string fkfs = ord.CheckOutType.Equals("3") ? "货到付款" : "现付";
                //            string shf = ord.DeliveryType.Equals("0") ? "急送" : ord.DeliveryType.Equals("1") ? "自提" : "次日达";
                //            string tit = ord.ThrowGood.Equals("22") ? "急速达" : "次日达";
                //            QySendInfoEntity send = new QySendInfoEntity();
                //            send.title = tit + " 有新订单";
                //            //推送给提交人
                //            send.msgType = msgType.textcard;
                //            send.agentID = "1000003";//消息通知的应用
                //            send.AgentSecret = "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM";
                //            //send.toUser = qup.ApplyID;<div>订单金额：" + ord.TotalCharge.ToString("F2") + "</div>
                //            send.toTag = houseEnt.HCYCOrderPushTagID.ToString();
                //            //send.toTag = "19";
                //            send.content = "<div></div><div>出库仓库：" + houseEnt.Name + "</div><div>商城订单号：" + ord.WXOrderNo + "</div><div>出库订单号：" + ord.OrderNo + "</div><div>订单数量：" + result[0].Piece.ToString() + "</div><div>货物信息：" + proStr + "</div><div>付款方式：" + fkfs + "</div><div>送货方式：" + shf + "</div><div>门店名称：" + ord.AcceptUnit + "</div><div>收货信息：" + ord.AcceptPeople + " " + ord.AcceptCellphone + "</div><div>收货地址：" + ord.AcceptAddress + "</div><div>请仓管人员留意尽快出库！</div>";
                //            send.url = "http://dlt.neway5.com/QY/qyScanOrderSign.aspx?OrderNo=" + ord.OrderNo;
                //            WxQYSendHelper.DLTQYPushInfo(send);


                //            #region 推送好来运系统

                //            if (houseEnt.HouseID.Equals(93) || houseEnt.HouseID.Equals(101))
                //            {
                //                List<CargoContainerShowEntity> outHouseList = orderBus.QueryOrderByOrderNo(new CargoOrderEntity { OrderNo = ord.OrderNo });
                //                //内部订单
                //                orderBus.SaveHlyOrderData(outHouseList, ord);
                //            }
                //            else
                //            {
                //                orderBus.InsertCargoOrderPush(new CargoOrderPushEntity
                //                {
                //                    OrderNo = ord.OrderNo,
                //                    Dep = ord.Dep,
                //                    Dest = ord.Dest,
                //                    Piece = ord.Piece,
                //                    TransportFee = ord.TransportFee,
                //                    ClientNum = ent.ClientNum.ToString(),
                //                    AcceptAddress = ord.AcceptAddress,
                //                    AcceptCellphone = ord.AcceptCellphone,
                //                    AcceptTelephone = ord.AcceptTelephone,
                //                    AcceptPeople = ord.AcceptPeople,
                //                    AcceptUnit = ord.AcceptUnit,
                //                    HouseID = houseEnt.HouseID.ToString(),
                //                    HouseName = ord.OutHouseName,
                //                    OP_ID = ord.CreateAwb,
                //                    PushType = "0",
                //                    PushStatus = "0",
                //                    LogisID = ord.HouseID.Equals(97) || ord.HouseID.Equals(95) || ord.HouseID.Equals(101) ? 62 : ord.DeliveryType.Equals("1") ? 46 : 34
                //                }, log);

                //            }

                //            #endregion


                //            CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
                //            cargoNewOrder.HouseName = houseEnt.Name;
                //            cargoNewOrder.OrderNo = ord.WXOrderNo;
                //            cargoNewOrder.OrderNum = result[0].Piece.ToString();
                //            cargoNewOrder.ClientInfo = ord.AcceptPeople + " " + ord.AcceptCellphone + " " + ord.AcceptAddress;// "泰乐 华笙 广东省广州市白云区东平加油站左侧";
                //            cargoNewOrder.ProductInfo = proStr;// "马牌 215/55R16 CC6 98V";
                //            cargoNewOrder.DeliveryName = shf;// "自提";
                //            cargoNewOrder.ReceivePeople = "";
                //            string hcno = JSON.Encode(cargoNewOrder);
                //            List<CargoVoiceBroadEntity> voiceBroadList = house.GetVoiceBroadList(new CargoVoiceBroadEntity { HouseID = houseEnt.HouseID });
                //            foreach (var voice in voiceBroadList)
                //            {
                //                mc.Add("NewOrderNotice_" + voice.LoginName, hcno);
                //            }
                //            //RedisHelper.SetString("NewOrderNotice", JSON.Encode(cargoNewOrder));
                //        }

                //        //支付成功，向客户发放优惠券根据订单明细返回的优惠规则ID，查询该规则的促销优惠内容，并向客户发放优惠券
                //        long RuleID = result[0].productList[0].RuleID;
                //        CargoPriceBus price = new CargoPriceBus();
                //        CargoRuleBankEntity mrule = price.QueryRuleBank(RuleID);
                //        //如果是发放优惠券的规则，则向客户发放优惠券
                //        if (mrule != null && mrule.IssueCoupon == 1)
                //        {
                //            int couponNum = result[0].Piece / mrule.FullEntry;
                //            for (int i = 0; i < couponNum; i++)
                //            {
                //                clientBus.AddCoupon(new WXCouponEntity { WXID = result[0].WXID, Money = mrule.CutEntry, UseStatus = "0", GainDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(mrule.ServiceTime), TypeID = mrule.UseTypeID, TypeName = mrule.UseTypeName, CouponType = mrule.CouponType.ToString(), SuppClientNum = mrule.SuppClientNum, IsSuperPosition = mrule.IsSuperPosition.ToString(), FromOrderNO = ord.OrderNo }, log);
                //            }
                //        }
                //    }
                //    catch (ApplicationException ex)
                //    {

                //    }

                //    //WriteTextLog("扫描支付 收款完成");
                //}




                //#region 缓存
                //string[] serverlist = ConfigurationSettings.AppSettings["memcachedServer"].Split('/');
                //SockIOPool pool = SockIOPool.GetInstance(ConfigurationSettings.AppSettings["PoolName"]);
                //pool.SetServers(serverlist);
                //pool.InitConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["InitConnections"]);//连接池初始容量
                //pool.MinConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MinConnections"]);//最小容量
                //pool.MaxConnections = Convert.ToInt32(ConfigurationSettings.AppSettings["MaxConnections"]);//最大容量
                //pool.SocketConnectTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketConnectTimeout"]);//数据读取超时时间
                //pool.SocketTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["SocketTimeout"]);//Socket连接超时时间
                //pool.MaintenanceSleep = Convert.ToInt64(ConfigurationSettings.AppSettings["MaintenanceSleep"]);//线程池维护线程之间的休眠时间
                //pool.Failover = Convert.ToBoolean(ConfigurationSettings.AppSettings["Failover"]);//使用缓存服务器自动切换功能，当一台服务器死了可以自动切换到另外一台查找缓存

                //pool.Nagle = Convert.ToBoolean(ConfigurationSettings.AppSettings["Nagle"]);//禁用Nagle算法
                //pool.Initialize();
                //mc.PoolName = ConfigurationSettings.AppSettings["PoolName"];
                //mc.EnableCompression = true;
                //mc.CompressionThreshold = 10240;
                //#endregion
                //CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
                //cargoNewOrder.HouseName = "东平云仓";
                //cargoNewOrder.OrderNo = "201401010001";
                //cargoNewOrder.OrderNum = "2";
                //cargoNewOrder.ClientInfo = "泰乐 华笙 广东省广州市白云区东平加油站左侧";
                //cargoNewOrder.ProductInfo = "马牌 215/55R16 CC6 98V";
                //cargoNewOrder.DeliveryName = "自提";
                //cargoNewOrder.ReceivePeople = "";

                //string hcno = JSON.Encode(cargoNewOrder);
                //mc.Add("NewOrderNotice", hcno);
                //string res = (string)mc.Get("NewOrderNotice");
                //Response.Write(res);
                //RedisHelper.SetString("NewOrderNotice", json);

                //CargoOrderBus order = new CargoOrderBus();
                ////查询所有云仓仓库
                //CargoHouseBus houseBus = new CargoHouseBus();
                //List<CargoHouseEntity> houseList = houseBus.QueryALLHouse(new CargoHouseEntity { BelongHouse = "6" });
                //string cper = "119";
                //foreach (var it in houseList)
                //{
                //    cper += "," + it.HouseID.ToString();
                //}
                //CargoOrderEntity entity = new CargoOrderEntity();
                //entity.BelongHouse = "0";
                //entity.CargoPermisID = cper;
                //entity.StartDate = DateTime.Now.AddDays(-10);
                //entity.LogisID = 34;
                //order.SetOrderSignByHLY(entity);
                //LogHelper.WriteLog("同步好来运签收数据成功");

                //CargoInterfaceBus interBus = new CargoInterfaceBus();
                //CargoContiStockSyncEntity stockSync = new CargoContiStockSyncEntity();
                //stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                //List<CargoContiStockListEntity> stockList = new List<CargoContiStockListEntity>();
                //List<CargoContiStockSKUEntity> sKUEntities = new List<CargoContiStockSKUEntity>();//东莞改为众汇前置仓20231227
                //List<CargoContiStockSKUEntity> SZsKUEntities = new List<CargoContiStockSKUEntity>();//深圳
                //List<CargoContiStockSKUEntity> JYsKUEntities = new List<CargoContiStockSKUEntity>();//揭阳
                //List<CargoContiStockSKUEntity> GZsKUEntities = new List<CargoContiStockSKUEntity>();//广州
                //List<CargoContiStockSKUEntity> HNsKUEntities = new List<CargoContiStockSKUEntity>();//华南二仓
                //List<CargoContiStockSKUEntity> LHYPsKUEntities = new List<CargoContiStockSKUEntity>();//龙华云配仓
                //List<CargoContiStockSKUEntity> DPCPsKUEntities = new List<CargoContiStockSKUEntity>();//东平城配仓
                //List<CargoContiStockSKUEntity> STsKUEntities = new List<CargoContiStockSKUEntity>();//汕头云仓
                ////汕头仓库
                //stockSync = new CargoContiStockSyncEntity();
                //stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                //stockList = new List<CargoContiStockListEntity>();
                //List<CargoSafeStockEntity> stStock = interBus.QueryContiStockData(new CargoSafeStockEntity { HouseID = "91" });
                //if (stStock.Count > 0)
                //{
                //    foreach (var stock in stStock)
                //    {

                //        if (string.IsNullOrEmpty(stock.GoodsCode) || stock.GoodsCode.Length < 7) { continue; }
                //        if (STsKUEntities.Exists(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7))))
                //        {
                //            CargoContiStockSKUEntity existEntity = STsKUEntities.Find(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7)));
                //            existEntity.dots.Add(new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum });
                //            //existEntity.qty += stock.StockNum;
                //        }
                //        else
                //        {
                //            STsKUEntities.Add(new CargoContiStockSKUEntity
                //            {
                //                skuCode = stock.GoodsCode.Substring(0, 7),
                //                dots = new List<CargoContiStockDOTEntity> { new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum } }
                //                //qty = stock.StockNum
                //            });
                //        }
                //    }
                //}
                //stockList.Add(new CargoContiStockListEntity
                //{
                //    warehouseName = "深圳龙华云配仓库",
                //    skuQtyList = STsKUEntities
                //});

                //stockList.Add(new CargoContiStockListEntity
                //{
                //    warehouseName = "维京广州仓",
                //    skuQtyList = gzWjEntity
                //});
                //stockSync.stockList = stockList;
                //string body = JSON.Encode(stockSync);
                //try
                //{
                //    string contiUrl = "https://cdms.continental-tires.cn/api/openapi/stock/dotSync";
                //    string res = wxHttpUtility.ContiSendPostHttpRequest(contiUrl, "application/json", body);
                //    //LogHelper.WriteLog("汕头仓库马牌维京库存同步成功");
                //}
                //catch (ApplicationException ex)
                //{
                //    //LogHelper.WriteLog("汕头仓库马牌维京库存同步失败");
                //}


                //CargoHouseBus busss = new CargoHouseBus();

                //CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
                //queryEntity.HouseID = 93;
                //queryEntity.Specs = "2055516";
                //queryEntity.IsLockStock = "0";//非锁定的库存
                //queryEntity.TypeParentID = 1; //查询分类是轮胎的品牌产品库存

                //int pageIndex = 1;//查询条件 分页 第几页
                //int pageSize = 20; //查询条件 分页 每页数量
                //List<CargoContainerShowEntity> list = busss.QueryHouseStockDataMiniPro(pageIndex, pageSize, queryEntity);

                ////机器人账号
                //LogEntity log = new LogEntity();
                //log.IPAddress = "";
                //log.Moudle = "服务管理";
                //log.Status = "0";
                //log.NvgPage = "定单推送服务";
                //log.UserID = "2029";
                //log.Operate = "A";
                //CargoInterfaceBus bus = new CargoInterfaceBus();

                ////string contiOutUrl = "https://cdms.continental-tires.cn/api/remote/openWms/getWmsDoHeaderInfoPage";
                ////string outbody = "{\"updateStartTime\": \"" + DateTime.Now.AddHours(-10).ToString("yyyy-MM-dd HH:mm:ss") + "\",	\"updateEndTime\": \"" + DateTime.Now.AddHours(-5).ToString("yyyy-MM-dd HH:mm:ss") + "\",	\"pageNum\": 1,\"pageSize\": 100}";
                ////string contistr = wxHttpUtility.ContiSendPostHttpRequest(contiOutUrl, "application/json", outbody);
                ////contiOutOrderEntity contiOrder = JsonConvert.DeserializeObject<contiOutOrderEntity>(contistr);
                ////if (contiOrder.code.Equals(10000))
                ////{
                ////    bus.UpdateContiOrderOutData(contiOrder.data.list, log);
                ////}


                ////CargoContiStockSyncEntity stockSync = new CargoContiStockSyncEntity();
                ////stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                ////List<CargoContiStockListEntity> stockList = new List<CargoContiStockListEntity>();
                ////List<CargoContiStockSKUEntity> LHYPsKUEntities = new List<CargoContiStockSKUEntity>();//龙华云配仓
                //////深圳龙华云配仓库
                ////stockSync = new CargoContiStockSyncEntity();
                ////stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                ////stockList = new List<CargoContiStockListEntity>();
                ////List<CargoSafeStockEntity> ypStock = bus.QueryGZContiStockData(new CargoSafeStockEntity { HouseID = "91" });
                ////if (ypStock.Count > 0)
                ////{
                ////    foreach (var stock in ypStock)
                ////    {
                ////        if (string.IsNullOrEmpty(stock.GoodsCode) || stock.GoodsCode.Length < 7) { continue; }
                ////        if (LHYPsKUEntities.Exists(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7))))
                ////        {
                ////            CargoContiStockSKUEntity existEntity = LHYPsKUEntities.Find(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7)));
                ////            existEntity.dots.Add(new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum });
                ////            //existEntity.qty += stock.StockNum;
                ////        }
                ////        else
                ////        {
                ////            LHYPsKUEntities.Add(new CargoContiStockSKUEntity
                ////            {
                ////                skuCode = stock.GoodsCode.Substring(0, 7),
                ////                dots = new List<CargoContiStockDOTEntity> { new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum } }
                ////                //qty = stock.StockNum
                ////            });
                ////        }
                ////    }
                ////}
                ////stockList.Add(new CargoContiStockListEntity
                ////{
                ////    warehouseName = "深圳龙华云配仓库",
                ////    skuQtyList = LHYPsKUEntities
                ////});
                //////stockList.Add(new CargoContiStockListEntity
                //////{
                //////    warehouseName = "维京广州仓",
                //////    skuQtyList = gzWjEntity
                //////});
                ////stockSync.stockList = stockList;
                ////string  body = JSON.Encode(stockSync);
                ////try
                ////{
                ////    string contiUrl = "https://cdms.continental-tires.cn/api/openapi/stock/dotSync";
                ////    string res = wxHttpUtility.ContiSendPostHttpRequest(contiUrl, "application/json", body);
                ////   // LogHelper.WriteLog("深圳龙华云配仓库马牌维京库存同步成功");
                ////}
                ////catch (ApplicationException ex)
                ////{
                ////   // LogHelper.WriteLog("深圳龙华云配仓库马牌维京库存同步失败");
                ////}







                //saleOpenInfoResp entity = new saleOpenInfoResp();
                //entity.HouseIDStr = "9,45,84,83,91,93,95,97,101";
                //entity.StartDate = DateTime.Now.AddDays(-5);
                //entity.InCreateStatus = "1";
                //entity.IsPushOutTag = "0";
                //List<saleOpenInfoResp> infoResps = bus.QueryContiOutOrderInfo(entity);
                //if (infoResps.Count > 0)
                //{
                //    foreach (var res in infoResps)
                //    {
                //        OutOrderStatus outOrder = new OutOrderStatus();
                //        outOrder.doNo = res.doNo;
                //        outOrder.deliveryType = 4;
                //        outOrder.contractName = res.SaleManName;
                //        outOrder.contractMobile = res.SaleCellphone;
                //        List<OutDetail> outs = new List<OutDetail>();
                //        List<CargoProductTagEntity> result = bus.QueryContiOutOrderDetail(new saleOpenInfoResp { CargoOrderNo = res.CargoOrderNo });
                //        if (result.Count > 0)
                //        {
                //            var groupedItems = result.GroupBy(item => item.GoodsCode).Select(group => new { GoodsCode = group.Key, Count = group.Count() });
                //            foreach (var group in groupedItems)
                //            {
                //                OutDetail outD = new OutDetail();
                //                outD.outNum = group.Count;
                //                outD.skuCode = group.GoodsCode.Substring(0, 7);

                //                List<BarCodeEntity> barCodeEntities = new List<BarCodeEntity>();
                //                List<CargoProductTagEntity> cpt = result.Where(c => c.GoodsCode == group.GoodsCode).ToList();
                //                foreach (var cp in cpt)
                //                {
                //                    barCodeEntities.Add(new BarCodeEntity
                //                    {
                //                        barCode = cp.TyreCode,
                //                        dot = cp.Batch,
                //                        scanTime = cp.OutCargoTime.ToString("yyyy-MM-dd HH:mm:ss")
                //                    });
                //                }
                //                outD.barCodeList = barCodeEntities;
                //                outs.Add(outD);
                //            }
                //        }
                //        outOrder.doDetails = outs;

                //        //处理Json推送到马牌系统
                //        string postJson = JsonConvert.SerializeObject(outOrder);
                //        string contiUrl = "https://cdms.continental-tires.cn/api/openapi/out/sync";
                //        string oks = wxHttpUtility.ContiSendPostHttpRequest(contiUrl, "application/json", postJson);
                //        if (oks.Contains("10000"))
                //        {
                //            //成功
                //            bus.UpdateContiOutOrderPushStatus(new saleOpenInfoResp { IsPushOutTag = "1", orderCode = res.orderCode }, log);
                //        }
                //    }
                //}
            }
        }

        private void SyncStock()
        {
            CargoInterfaceBus nwBus = new CargoInterfaceBus();

            //马牌仓库名称字典
            Dictionary<string, string> dicContiHouseName = new Dictionary<string, string>();
            dicContiHouseName.Add("45", "深圳龙岗众汇前置仓");
            dicContiHouseName.Add("83", "揭阳仓");//智能系统揭阳城配仓
            dicContiHouseName.Add("120,132", "深圳宝安南山仓库");
            dicContiHouseName.Add("132,120", "深圳宝安南山仓库");
            dicContiHouseName.Add("120", "深圳宝安南山仓库");
            dicContiHouseName.Add("132", "深圳宝安南山仓库");
            dicContiHouseName.Add("93,9", "广州仓");
            dicContiHouseName.Add("9,93", "广州仓");
            dicContiHouseName.Add("93", "广州仓");
            dicContiHouseName.Add("9", "广州仓");
            dicContiHouseName.Add("84", "华南二仓");
            dicContiHouseName.Add("91", "深圳龙华云配仓库");
            dicContiHouseName.Add("101", "汕头仓库");



            //马牌库存字典
            Dictionary<int, string> dicConti = new Dictionary<int, string>();
            Dictionary<string, string> dicContiComp = new Dictionary<string, string>();
            //汕头,龙华云配仓开思库存字典
            Dictionary<int, string> dicCass = new Dictionary<int, string>();
            string StProductCode = string.Empty;
            RedisValue[] redisValues = RedisHelper.HashKeys("OpenSystemStockSyc");
            foreach (RedisValue redisValue in redisValues)
            {
                string key = redisValue.ToString();//93_34_LTCT245452005
                string goodscode = Convert.ToString(RedisHelper.HashGet("OpenSystemStockSyc", key));//货品代码：03118970000
                //仓库ID
                int HouseID = Convert.ToInt32(key.Split('_')[0]);
                //品牌ID
                int TypeID = Convert.ToInt32(key.Split('_')[1]);
                //产品编码
                string ProductCode = Convert.ToString(key.Split('_')[2]);
                //处理马牌库存数据
                if (dicConti.ContainsKey(HouseID))
                {
                    if (TypeID.Equals(34) || TypeID.Equals(345))
                    {
                        // 获取当前Key的内容
                        string currentValue = dicConti[HouseID];

                        // 更新内容
                        string updatedValue = currentValue + "','" + goodscode;
                        dicConti[HouseID] = updatedValue;
                    }

                }
                else
                {
                    dicConti.Add(HouseID, goodscode);
                }

                //处理龙华云配仓和汕头开思库存数据
                if (HouseID.Equals(91) || HouseID.Equals(101))
                {
                    if (dicCass.ContainsKey(HouseID))
                    {
                        // 获取当前Key的内容
                        string currentValue = dicCass[HouseID];

                        // 更新内容
                        string updatedValue = currentValue + "','" + ProductCode;
                        dicCass[HouseID] = updatedValue;
                    }
                    else
                    {
                        dicCass.Add(HouseID, ProductCode);
                    }
                }

            }


            //开始同步开思库存
            foreach (var cass in dicCass)
            {
                //调用汕头开思接口同步库存
                if (cass.Key.Equals(101))
                {
                    #region 汕头仓库存同步开思
                    ///汕头仓Session：67d89f8d06ca4360ae0358279b7d283c
                    try
                    {
                        List<StockApiEntity> res = nwBus.QueryYunCangStockSync(new CargoProductEntity { HouseID = cass.Key, ProductCode = cass.Value });
                        //StockApiResponseEntity STentity = new StockApiResponseEntity();
                        //STentity.Params = new Params();
                        //STentity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                        //STentity.data = new List<Data>();
                        StockApiResponseEntity entity = new StockApiResponseEntity();
                        entity.Params = new Params();
                        entity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                        entity.data = new List<Data>();

                        if (res.Count > 0)
                        {
                            entity.Params.firstData = true;
                            entity.Params.lastData = true;
                            entity.Params.updateMode = "CHANGE";
                            long tpsBatchId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                            string strRes = string.Empty;
                            entity.Params.tpsBatchId = tpsBatchId;
                            int cou = 0;
                            for (int i = 0; i < res.Count; i++)
                            {
                                if (cou == 500)
                                {
                                    // LogHelper.WriteLog("汕头仓云配开思推送ID:" + tpsBatchId + ",本次推送条数:" + cou);
                                    cou = 0;
                                    strRes = wxHttpUtility.PostHttpRequest("https://api.cassmall.com/api", "application/json", JSON.Encode(entity).Replace("Params", "params").Replace("thirdPartySystemparams", "thirdPartySystemParams"), "67d89f8d06ca4360ae0358279b7d283c");
                                    //string cpStrRes500 = wxHttpUtility.SendPostHttpRequest("https://cp.neway5.com/Interface/OpenApi.ashx?cmd=StockDataSync", "application/json", JsonConvert.SerializeObject(STentity));

                                    //STentity = new StockApiResponseEntity();
                                    //STentity.Params = new Params();
                                    //STentity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                                    //STentity.data = new List<Data>();

                                    entity = new StockApiResponseEntity();
                                    entity.Params = new Params();
                                    entity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                                    entity.data = new List<Data>();
                                    if (strRes.IndexOf("200") >= 0)
                                    {
                                        tpsBatchId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                                        entity.Params.tpsBatchId = tpsBatchId;
                                        entity.Params.firstData = true;
                                        entity.Params.lastData = true;
                                        entity.Params.updateMode = "CHANGE";
                                    }
                                }

                                //STentity.data.Add(new Data
                                //{
                                //    CassProductCode = res[i].ProductCode,
                                //    ProductName = res[i].ProductName,
                                //    TypeName = res[i].TypeName,
                                //    SalePrice = Convert.ToDecimal(res[i].SalePrice) + 10,
                                //    StockNum = res[i].StockNum,
                                //    Batch = res[i].Batch,
                                //    HouseID = res[i].HouseID,//汕头仓+ Convert.ToDecimal(GetContiPrice())
                                //});
                                string casscode = res[i].CassProductCode;
                                if (res[i].TypeID.Equals(34) || res[i].TypeID.Equals(345) || res[i].TypeID.Equals(163))
                                {
                                    casscode = res[i].GoodsCode;
                                }
                                else
                                {
                                    casscode = string.IsNullOrEmpty(res[i].CassProductCode) ? res[i].ProductCode : res[i].CassProductCode;

                                }
                                string typename = res[i].TypeName;
                                if (res[i].TypeID.Equals(9))
                                {
                                    typename = "横滨/优科豪马";
                                }
                                else if (res[i].TypeID.Equals(66))
                                {
                                    typename = "固铂轮胎";
                                }
                                entity.data.Add(new Data
                                {
                                    CassProductCode = casscode,
                                    ProductName = res[i].ProductName,
                                    TypeName = typename,
                                    SalePrice = Convert.ToDecimal(res[i].SalePrice) + Convert.ToDecimal(10),
                                    StockNum = res[i].StockNum,
                                    Batch = res[i].Batch,
                                    HouseID = res[i].HouseID,//汕头仓+ Convert.ToDecimal(GetContiPrice())
                                });
                                //LogHelper.WriteLog("汕头仓云配开思推送ID:" + casscode + "，库存：" + res[i].StockNum.ToString() + "，周期：" + res[i].Batch + ",本次推送金额:" + Convert.ToDecimal(res[i].SalePrice) + Convert.ToDecimal(GetContiPrice()) + ",加价金额:10");
                                cou++;

                            }
                            strRes = wxHttpUtility.PostHttpRequest("https://api.cassmall.com/api", "application/json", JSON.Encode(entity).Replace("Params", "params").Replace("thirdPartySystemparams", "thirdPartySystemParams"), "67d89f8d06ca4360ae0358279b7d283c");

                            //string data = JsonConvert.SerializeObject(STentity);

                            //string cpStrRes = wxHttpUtility.SendPostHttpRequest("https://cp.neway5.com/Interface/OpenApi.ashx?cmd=StockDataSync", "application/json", data);

                            // LogHelper.WriteLog("汕头仓云配开思推送ID:" + tpsBatchId + ",本次推送条数:" + cou);
                            if (strRes.IndexOf("200") >= 0)
                            {
                                // bus.UpdateStatus(res);
                                //LogHelper.WriteLog("汕头仓云配开思推送成功!共计" + res.Count + "条数据");
                            }
                            //if (cpStrRes.IndexOf("1000") >= 0)
                            //{
                            //    //bus.UpdateStatus(res);
                            //    LogHelper.WriteLog("汕头仓城配快送推送成功!共计" + res.Count + "条数据");
                            //}
                        }

                    }
                    catch (Exception ex)
                    {
                        // LogHelper.WriteLog("汕头仓云配开思推送出现错误!Message" + ex.Message);
                        throw;
                    }
                    #endregion
                }
                else if (cass.Key.Equals(91))
                {
                    #region 龙华云仓库存同步开思
                    try
                    {
                        List<StockApiEntity> res = nwBus.QueryYunCangStockSync(new CargoProductEntity { HouseID = cass.Key, ProductCode = cass.Value });

                        StockApiResponseEntity entity = new StockApiResponseEntity();
                        entity.Params = new Params();
                        entity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                        entity.data = new List<Data>();

                        if (res.Count > 0)
                        {
                            entity.Params.firstData = true;
                            entity.Params.lastData = true;
                            entity.Params.updateMode = "CHANGE";
                            long tpsBatchId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                            string strRes = string.Empty;
                            entity.Params.tpsBatchId = tpsBatchId;
                            int cou = 0;
                            for (int i = 0; i < res.Count; i++)
                            {
                                if (cou == 500)
                                {
                                    // LogHelper.WriteLog("汕头仓开思推送ID:" + tpsBatchId + ",本次推送条数:" + cou);
                                    cou = 0;
                                    strRes = wxHttpUtility.PostHttpRequest("https://api.cassmall.com/api", "application/json", JSON.Encode(entity).Replace("Params", "params").Replace("thirdPartySystemparams", "thirdPartySystemParams"), "7b39a4ed11e24289b1b7a42aea766802");

                                    entity = new StockApiResponseEntity();
                                    entity.Params = new Params();
                                    entity.Params.thirdPartySystemParams = new ThirdPartySystemParams();
                                    entity.data = new List<Data>();
                                    if (strRes.IndexOf("200") >= 0)
                                    {
                                        tpsBatchId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                                        entity.Params.tpsBatchId = tpsBatchId;
                                        entity.Params.firstData = true;
                                        entity.Params.lastData = true;
                                        entity.Params.updateMode = "CHANGE";
                                    }
                                }
                                string casscode = res[i].CassProductCode;
                                if (res[i].TypeID.Equals(34) || res[i].TypeID.Equals(345) || res[i].TypeID.Equals(163))
                                {
                                    casscode = res[i].GoodsCode;
                                }
                                else
                                {
                                    casscode = string.IsNullOrEmpty(res[i].CassProductCode) ? res[i].ProductCode : res[i].CassProductCode;

                                }
                                string typename = res[i].TypeName;
                                if (res[i].TypeID.Equals(9))
                                {
                                    typename = "横滨/优科豪马";
                                }
                                else if (res[i].TypeID.Equals(66))
                                {
                                    typename = "固铂轮胎";
                                }
                                entity.data.Add(new Data
                                {
                                    CassProductCode = casscode,
                                    ProductName = res[i].ProductName,
                                    TypeName = typename,
                                    SalePrice = Convert.ToDecimal(res[i].SalePrice) + 20,
                                    StockNum = res[i].StockNum,
                                    Batch = res[i].Batch,
                                    HouseID = res[i].HouseID,//汕头仓+ Convert.ToDecimal(GetContiPrice())
                                });
                                // LogHelper.WriteLog("龙华云仓开思增量推送ID:" + casscode + "，库存：" + res[i].StockNum.ToString() + "，周期：" + res[i].Batch + ",本次推送金额:" + Convert.ToDecimal(res[i].SalePrice) + ",加价金额:" + GetContiPrice().ToString());
                                cou++;

                            }
                            strRes = wxHttpUtility.PostHttpRequest("https://api.cassmall.com/api", "application/json", JSON.Encode(entity).Replace("Params", "params").Replace("thirdPartySystemparams", "thirdPartySystemParams"), "7b39a4ed11e24289b1b7a42aea766802");


                            //LogHelper.WriteLog("龙华云仓开思增量推送ID:" + tpsBatchId + ",本次推送条数:" + cou);
                            if (strRes.IndexOf("200") >= 0)
                            {
                                nwBus.UpdateStatus(res);
                                //LogHelper.WriteLog("龙华云仓开思增量推送成功!共计" + res.Count + "条数据");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        //LogHelper.WriteLog("龙华云仓开思增量推送出现错误!Message" + ex.Message);
                        continue;
                    }
                    #endregion
                }
            }
            return;
            string contiUrl = "https://cdms.continental-tires.cn/api/openapi/stock/dotSync";
            // string contiUrl = GetContiUrl() + GetContiStockDOTSync();
            List<CargoContiStockListEntity> stockList = new List<CargoContiStockListEntity>();
            CargoContiStockSyncEntity stockSync = new CargoContiStockSyncEntity();

            List<CargoContiStockSKUEntity> sKUEntities = new List<CargoContiStockSKUEntity>();//东莞改为众汇前置仓20231227

            //南山云仓库存同步120,光明云仓132处理仓库合并
            if (dicConti.ContainsKey(120) && dicConti.ContainsKey(132))
            {
                dicContiComp.Add("120,132", dicConti[120] + "','" + dicConti[132]);
                dicConti.Remove(120);
                dicConti.Remove(132);
            }
            //广州仓,东平云仓，南沙，从化，花都，肇庆,106,107,129,131 处理仓库合并
            if (dicConti.ContainsKey(9) && dicConti.ContainsKey(93))
            {
                dicContiComp.Add("9,93", dicConti[9] + "','" + dicConti[93]);
                dicConti.Remove(9);
                dicConti.Remove(93);
            }



            foreach (var conti in dicConti)
            {
                dicContiComp.Add(conti.Key.ToString(), conti.Value);
            }

            //同步马牌库存
            foreach (var ct in dicContiComp)
            {
                if (ct.Key.Equals("45"))
                {
                    stockSync = new CargoContiStockSyncEntity();
                    stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                    stockList = new List<CargoContiStockListEntity>();
                    sKUEntities = new List<CargoContiStockSKUEntity>();
                    List<CargoSafeStockEntity> stockEntities = nwBus.QueryContiStockData(new CargoSafeStockEntity { HouseID = "45", GoodsCode = dicContiComp[ct.Key] });
                    if (stockEntities.Count > 0)
                    {
                        foreach (var stock in stockEntities)
                        {
                            if (string.IsNullOrEmpty(stock.GoodsCode) || stock.GoodsCode.Length < 7) { continue; }
                            if (stock.AreaID.Equals(3446))
                            {
                                //众汇前置仓马牌和维京
                                if (sKUEntities.Exists(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7))))
                                {
                                    CargoContiStockSKUEntity existEntity = sKUEntities.Find(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7)));
                                    existEntity.dots.Add(new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum });
                                    //existEntity.qty += stock.StockNum;
                                }
                                else
                                {
                                    sKUEntities.Add(new CargoContiStockSKUEntity
                                    {
                                        skuCode = stock.GoodsCode.Substring(0, 7),
                                        dots = new List<CargoContiStockDOTEntity> { new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum } }
                                    }); ;
                                }

                            }
                        }
                    }
                    //众汇前置仓
                    stockList.Add(new CargoContiStockListEntity
                    {
                        warehouseName = dicContiHouseName[ct.Key],// "深圳龙岗众汇前置仓",
                        skuQtyList = sKUEntities
                    });

                    stockSync.stockList = stockList;
                    String contibody = JSON.Encode(stockSync);
                    try
                    {
                        string res = wxHttpUtility.ContiSendPostHttpRequest(contiUrl, "application/json", contibody);
                        //LogHelper.WriteLog("众汇前置仓和深圳马牌维京库存同步成功");
                    }
                    catch (ApplicationException ex)
                    {
                        //LogHelper.WriteLog("众汇前置仓和深圳马牌维京库存同步失败");
                    }
                }
                else
                {
                    //广州仓,东平云仓，南沙，从化，花都，肇庆,106,107,129,131
                    stockSync = new CargoContiStockSyncEntity();
                    stockSync.outNo = Guid.NewGuid().ToString().Replace("-", "");
                    stockList = new List<CargoContiStockListEntity>();
                    sKUEntities = new List<CargoContiStockSKUEntity>();
                    List<CargoSafeStockEntity> hnStock = nwBus.QueryGZContiStockData(new CargoSafeStockEntity { HouseIDStr = ct.Key, GoodsCode = dicContiComp[ct.Key] });
                    if (hnStock.Count > 0)
                    {
                        foreach (var stock in hnStock)
                        {
                            if (string.IsNullOrEmpty(stock.GoodsCode) || stock.GoodsCode.Length < 7) { continue; }
                            //广州仓马牌

                            if (sKUEntities.Exists(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7))))
                            {
                                CargoContiStockSKUEntity existEntity = sKUEntities.Find(c => c.skuCode.Equals(stock.GoodsCode.Substring(0, 7)));
                                existEntity.dots.Add(new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum });
                                //existEntity.qty += stock.StockNum;
                            }
                            else
                            {
                                sKUEntities.Add(new CargoContiStockSKUEntity
                                {
                                    skuCode = stock.GoodsCode.Substring(0, 7),
                                    dots = new List<CargoContiStockDOTEntity> { new CargoContiStockDOTEntity { dot = stock.Batch, qty = stock.StockNum } }
                                    //qty = stock.StockNum
                                });
                            }
                        }
                    }
                    stockList.Add(new CargoContiStockListEntity
                    {
                        warehouseName = dicContiHouseName[ct.Key],//"广州仓",
                        skuQtyList = sKUEntities
                    });
                    stockSync.stockList = stockList;
                    string contibody = JSON.Encode(stockSync);
                    try
                    {
                        string res = wxHttpUtility.ContiSendPostHttpRequest(contiUrl, "application/json", contibody);
                        //LogHelper.WriteLog("广州马牌维京库存同步成功");
                    }
                    catch (ApplicationException ex)
                    {
                        //LogHelper.WriteLog("广州马牌维京库存同步失败");
                    }
                }
            }


        }
        private void GetDongShunTyreData()
        {
            //{"action":"003","openid":"ojE6s5CTng_fZyjEVjBCSHu_htSc"}获取轮胎品牌数据
            string brandstr = "{\"code\":0,\"data\":{\"respdata\":[{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007002\",\"ishaveChild\":null,\"typeCode\":\"02\",\"typeName\":\"倍耐力\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007003\",\"ishaveChild\":null,\"typeCode\":\"03\",\"typeName\":\"马牌\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007004\",\"ishaveChild\":null,\"typeCode\":\"04\",\"typeName\":\"普利司通\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007005\",\"ishaveChild\":null,\"typeCode\":\"05\",\"typeName\":\"邓禄普\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007006\",\"ishaveChild\":null,\"typeCode\":\"06\",\"typeName\":\"固特异\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007008\",\"ishaveChild\":null,\"typeCode\":\"08\",\"typeName\":\"韩泰\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007007\",\"ishaveChild\":null,\"typeCode\":\"09\",\"typeName\":\"优科豪马\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007009\",\"ishaveChild\":null,\"typeCode\":\"11\",\"typeName\":\"玛吉斯\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007021004\",\"ishaveChild\":null,\"typeCode\":\"15\",\"typeName\":\"佳通\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007021005\",\"ishaveChild\":null,\"typeCode\":\"16\",\"typeName\":\"玲珑\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007021002\",\"ishaveChild\":null,\"typeCode\":\"17\",\"typeName\":\"朝阳\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007021010\",\"ishaveChild\":null,\"typeCode\":\"18\",\"typeName\":\"正新\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007011\",\"ishaveChild\":null,\"typeCode\":\"10\",\"typeName\":\"固铂\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007021011\",\"ishaveChild\":null,\"typeCode\":\"32\",\"typeName\":\"万力\"},{\"childTypeSort\":null,\"flagNotWebShow\":false,\"innerCode\":\"1007001\",\"ishaveChild\":null,\"typeCode\":\"01\",\"typeName\":\"米其林\"}]},\"msg\":null}";

            TyreEntity brandList = JsonConvert.DeserializeObject<TyreEntity>(brandstr);

            List<respdata> list = new List<respdata>();

            string url = "https://nngs.sihuiyun.com.cn/WXProgramService/api/WxService";

            foreach (var itt in brandList.data.respdata)
            {
                for (int i = 1; i < 200; i++)
                {
                    string poststr = "{\"typeInnerCode\":\"" + itt.innerCode + "\",\"search\":\"\",\"page\":" + i + ",\"pagesize\":100,\"openid\":\"ojE6s5CTng_fZyjEVjBCSHu_htSc\",\"action\":\"004\",\"flag\":\"0\"}";
                    string result = wxHttpUtility.SendPostHttpRequest(url, "application/json", poststr);

                    TyreEntity tyreEntity = JsonConvert.DeserializeObject<TyreEntity>(result);
                    if (tyreEntity.data.respdata.Count <= 0)
                    {
                        break;
                    }
                    if (tyreEntity.code.Equals(0))
                    {
                        list.AddRange(tyreEntity.data.respdata);
                    }
                    Thread.Sleep(1000);
                }
            }

            DataTable table = new DataTable();
            table.Columns.Add("序号", typeof(int));
            table.Columns.Add("品牌代码", typeof(string));
            table.Columns.Add("商品名称", typeof(string));
            table.Columns.Add("车型", typeof(string));
            table.Columns.Add("周期", typeof(string));
            table.Columns.Add("东盟仓库存", typeof(string));
            table.Columns.Add("西仓库存", typeof(string));
            table.Columns.Add("原价", typeof(string));
            table.Columns.Add("销售价", typeof(string));
            table.Columns.Add("对外价格1", typeof(string));
            table.Columns.Add("对外价格2", typeof(string));
            table.Columns.Add("对外价格3", typeof(string));
            table.Columns.Add("对外价格4", typeof(string));
            table.Columns.Add("对外价格5", typeof(string));
            table.Columns.Add("对外价格6", typeof(string));
            int j = 0;
            foreach (var it in list)
            {
                j++;
                DataRow newRows = table.NewRow();
                newRows["序号"] = j;
                newRows["品牌代码"] = it.typeInnerCode;
                newRows["商品名称"] = it.partName;
                newRows["车型"] = it.carType;
                newRows["周期"] = it.brand;
                newRows["东盟仓库存"] = it.storageAmount1;
                newRows["西仓库存"] = it.storageAmount2;
                newRows["原价"] = it.yuanPrice;
                newRows["销售价"] = it.price;
                newRows["对外价格1"] = it.outPrice1;
                newRows["对外价格2"] = it.outPrice2;
                newRows["对外价格3"] = it.outPrice3;
                newRows["对外价格4"] = it.outPrice4;
                newRows["对外价格5"] = it.outPrice5;
                newRows["对外价格6"] = it.outPrice6;

                table.Rows.Add(newRows);
            }
            ToExcel.DataTableToExcel(table, "", "库存数据表");

        }

        public static string PostHttpRequest(string url, string contentType, string Data)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            TimeSpan timeSpan = DateTime.UtcNow.AddMinutes(-5) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime = DateTime.Now;
            long timestamp = (long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string sign = "38476BFFE6274DBCAB38647884D467C6" + "cassec.sand.passive.data" + "gzshly60be866dbe52" + "7b39a4ed11e24289b1b7a42aea766802" + timestamp;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(sign);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                sign = sb.ToString().ToUpper();
            }
            request.Headers.Add("apiName", "cassec.sand.passive.data");
            request.Headers.Add("appKey", "gzshly60be866dbe52");
            request.Headers.Add("sign", sign);
            request.Headers.Add("session", "7b39a4ed11e24289b1b7a42aea766802");
            request.Headers.Add("timestamp", timestamp.ToString());
            request.Method = "POST";
            request.ContentType = contentType;
            byte[] postBytes = null;
            postBytes = Encoding.UTF8.GetBytes(Data);
            request.ContentLength = postBytes.Length;
            using (Stream outstream = request.GetRequestStream())
            {
                outstream.Write(postBytes, 0, postBytes.Length);
            }
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }

                }
            }
            return result;


        }
        private void ScanContiQRCode()
        {
            string TagCode = "QRB03155080000002220324820241204";//马牌二维码
            string[] tArr = TagCode.Split('=');
            if (tArr.Length > 1)
            {
                TagCode = tArr[1];
            }
            string QrText = "";// "8476063989";//轮胎内侧码
            string QRcode = string.IsNullOrEmpty(QrText) ? TagCode : QrText;//产品唯一编码标签码
            //调用马牌接口查询轮胎数据
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string tms = Convert.ToInt64(ts.TotalSeconds).ToString();
            string sign = string.Empty;
            //二维码转条码 通过扫描轮胎上二维码查询该条轮胎具体信息

            string ss = "QRcode=" + QRcode + "&appKey=" + Common.GetContiappKey() + "&noise=" + Common.GetContinoise() + "&timestamp=" + tms + "&appSecret=" + Common.GetContiappSecret();
            if (!QRcode.ToUpper().Contains("R"))
            {
                ss = "appKey=" + Common.GetContiappKey() + "&barcodes=%5B" + QRcode + "%5D" + "&noise=" + Common.GetContinoise() + "&timestamp=" + tms + "&appSecret=" + Common.GetContiappSecret();
            }
            string reValue = string.Empty;
            //MD5加密
            using (MD5 md5Hash = MD5.Create())
            {
                sign = Common.GetMd5Hash(md5Hash, ss);
            }
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("appKey", Common.GetContiappKey());
            nvc.Add("noise", Common.GetContinoise());
            nvc.Add("timestamp", tms);
            nvc.Add("sign", sign);
            //if (!QRcode.ToUpper().Contains("R"))
            if (!QRcode.ToUpper().Contains("R"))
            {
                nvc.Add("barcodes", "%5B" + QRcode + "%5D");
                reValue = wxHttpUtility.PostForm("https://contiid.continental-tires.cn/api/2.0/TTP/getBarcodeInfo", nvc, null);
                ArrayList GridRows = (ArrayList)JSON.Decode("[" + reValue + "]");
                foreach (Hashtable item in GridRows)
                {
                    //数据返回正确
                    if (Convert.ToString(item["code"]).Equals("1000"))
                    {
                        ArrayList datalit = (ArrayList)JSON.Decode("[" + JSON.Encode(item["data"]) + "]");
                        foreach (Hashtable ri in datalit)
                        {
                            ArrayList datalitt = (ArrayList)JSON.Decode("[" + JSON.Encode(ri["" + QRcode + ""]) + "]");
                            //foreach (Hashtable rit in datalitt)
                            //{
                            //    string[] description = Convert.ToString(rit["Description"]).Split(' ');
                            //    for (int i = 2; i < description.Length; i++)
                            //    {
                            //        Figure += description[i];//花纹
                            //    }
                            //    Specs = description[0];
                            //    string loadSpeed = description[1].Replace("(", "").Replace(")", "");
                            //    LoadIndex = loadSpeed.Substring(0, loadSpeed.Length - 1);
                            //    SpeedLevel = loadSpeed.Substring(loadSpeed.Length - 1);
                            //    TyreCode = Convert.ToString(rit["barcode"]);
                            //    GoodsCode = Convert.ToString(rit["material"]);
                            //    if (GoodsCode.Length > 6)
                            //    {
                            //        GoodsCode = GoodsCode + "0000";
                            //    }
                            //    else if (GoodsCode.Length == 5)
                            //    {
                            //        GoodsCode = "0" + GoodsCode + "0000";
                            //    }
                            //    Batch = Convert.ToString(rit["DOT"]);
                            //    string singleBrand = Convert.ToString(rit["brand"]);
                            //    if (singleBrand.ToUpper().Contains("VIKING"))
                            //    {
                            //        TypeID = 345;
                            //        ProductName = "维京轮胎";
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        //msg.Result = false;
                        //msg.Message = Convert.ToString(item["code"].ToString() + " " + item["data"].ToString());
                        //goto ERR;
                    }
                }
            }
            else
            {
                nvc.Add("QRcode", QRcode);
                //二维码转条码 接口
                reValue = wxHttpUtility.PostForm("https://contiid.continental-tires.cn/api/2.0/TTP/QRgetBarcode", nvc, null);
                ArrayList GridRows = (ArrayList)JSON.Decode("[" + reValue + "]");
                foreach (Hashtable item in GridRows)
                {
                    //数据返回正确
                    if (Convert.ToString(item["code"]).Equals("1000"))
                    {
                        ArrayList datalit = (ArrayList)JSON.Decode("[" + JSON.Encode(item["data"]) + "]");
                        //foreach (Hashtable ri in datalit)
                        //{
                        //    string[] description = Convert.ToString(ri["Description"]).Split(' ');
                        //    for (int i = 2; i < description.Length; i++)
                        //    {
                        //        Figure += description[i];//花纹
                        //    }
                        //    Specs = description[0];
                        //    string loadSpeed = description[1].Replace("(", "").Replace(")", "");
                        //    LoadIndex = loadSpeed.Substring(0, loadSpeed.Length - 1);
                        //    SpeedLevel = loadSpeed.Substring(loadSpeed.Length - 1);
                        //    TyreCode = Convert.ToString(ri["barcode"]);
                        //    GoodsCode = Convert.ToString(ri["material"]);
                        //    if (GoodsCode.Length > 6)
                        //    {
                        //        GoodsCode = GoodsCode + "0000";
                        //    }
                        //    else if (GoodsCode.Length == 5)
                        //    {
                        //        GoodsCode = "0" + GoodsCode + "0000";
                        //    }
                        //    Batch = Convert.ToString(ri["DOT"]);
                        //    string singleBrand = Convert.ToString(ri["singleBrand"]);
                        //    if (singleBrand.ToUpper().Contains("VIKING"))
                        //    {
                        //        TypeID = 345;
                        //        ProductName = "维京轮胎";
                        //    }
                        //}
                    }
                    else
                    {
                        //msg.Result = false;
                        //msg.Message = Convert.ToString(item["code"].ToString() + " " + item["data"].ToString());
                        //goto ERR;
                    }
                }
            }

        }
        public void ScanQRCodeInCargo()
        {
            //string ContainerCode = Convert.ToString(Request["ContainerCode"]);//货位代码
            string ContainerCode = "T1B0006SHL";

            //string QRcode = Qcode;//Convert.ToString(Request["TagCode"]);
            string QRcode = "QRA0006202575";
            string trCode = "";


            string UserID = "1000";
            string ProductName = "马牌轮胎";//产品名称
            string GoodsCode = string.Empty; ;//货品代码
            int HouseID = 45;//仓库代码ID
            int TypeID = 34;//产品类型ID
            string Specs = string.Empty; ;//规格
            string Figure = string.Empty;//花纹

            string Model = string.Empty; ;//型号
            string Batch = string.Empty; ;//周期
            string TagCode = QRcode;//产品唯一编码标签码
            string LoadIndex = string.Empty; ;//载重指数
            string SpeedLevel = string.Empty; ;//速度级别
            string Born = "0";//生产地 0：国产1：进口
            string Assort = "";//OER  REP 胎
            string TyreCode = string.Empty; ;//轮胎编码
            string InCargoNum = "1";//入库数量
            string Source = "14";//产品来源
            string SourceOrderNo = string.Empty; ;//工厂订单号
            string BelongMonth = DateTime.Now.ToString("yyyyMM");//订单归属月
            string OrderType = "0";//订单类型ID
            string HLYReturnID = string.Empty;//好来运ID

            ErrMessage msg = new ErrMessage(); msg.Message = ""; msg.Result = true;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "产品管理";
            log.NvgPage = "扫描入库";
            log.UserID = UserID;//限定某一账号
            log.Operate = "A";
            log.Status = "0";
            //调用马牌接口查询轮胎数据
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string tms = Convert.ToInt64(ts.TotalSeconds).ToString();
            string sign = string.Empty;
            //二维码转条码 通过扫描轮胎上二维码查询该条轮胎具体信息

            //string ss = "QRcode=" + QRcode + "&appKey=" + Common.GetContiappKey() + "&noise=" + Common.GetContinoise() + "&timestamp=" + tms + "&appSecret=" + Common.GetContiappSecret();
            string ss = "appKey=" + Common.GetContiappKey() + "&barcodes=%5B" + trCode + "%5D" + "&noise=" + Common.GetContinoise() + "&timestamp=" + tms + "&appSecret=" + Common.GetContiappSecret();
            //MD5加密
            using (MD5 md5Hash = MD5.Create())
            {
                sign = Common.GetMd5Hash(md5Hash, ss);
            }
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("appKey", Common.GetContiappKey());
            nvc.Add("noise", Common.GetContinoise());
            nvc.Add("timestamp", tms);
            nvc.Add("sign", sign);
            //nvc.Add("QRcode", trCode);
            nvc.Add("barcodes", "%5B" + trCode + "%5D");
            string reValue = wxHttpUtility.PostForm("https://contiid.continental-tires.cn/api/2.0/TTP/getBarcodeInfo", nvc, null);
            ArrayList GridRows = (ArrayList)JSON.Decode("[" + reValue + "]");
            foreach (Hashtable item in GridRows)
            {
                //数据返回正确
                if (Convert.ToString(item["code"]).Equals("1000"))
                {
                    ArrayList datalit = (ArrayList)JSON.Decode("[" + JSON.Encode(item["data"]) + "]");
                    foreach (Hashtable ri in datalit)
                    {
                        ArrayList datalitt = (ArrayList)JSON.Decode("[" + JSON.Encode(ri["" + trCode + ""]) + "]");
                        foreach (Hashtable rit in datalitt)
                        {
                            string[] description = Convert.ToString(rit["Description"]).Split(' ');
                            for (int i = 2; i < description.Length; i++)
                            {
                                Figure += description[i];//花纹
                            }
                            Specs = description[0];
                            LoadIndex = description[1].Substring(0, description[1].Length - 1);
                            SpeedLevel = description[1].Substring(description[1].Length - 1);
                            TyreCode = Convert.ToString(rit["barcode"]);
                            GoodsCode = Convert.ToString(rit["material"]);
                            if (GoodsCode.Length > 6)
                            {
                                GoodsCode = GoodsCode + "0000";
                            }
                            else if (GoodsCode.Length == 5)
                            {
                                GoodsCode = "0" + GoodsCode + "0000";
                            }
                            Batch = Convert.ToString(rit["DOT"]);
                        }
                    }
                }
                else
                {
                    msg.Result = false;
                    msg.Message = Convert.ToString(item["code"]);
                    goto ERR;
                }
            }
            //二维码转条码 接口
            //string reValue = wxHttpUtility.PostForm("https://contiid.continental-tires.cn/api/2.0/TTP/QRgetBarcode", nvc, null);
            //string reValue = "{\"code\":1000,\"data\":{\"barcode\":\"8463495774\",\"material\":\"0356828\",\"Description\":\"225/45R17 91W SC5SSR*BMW\",\"DOT\":\"0521\",\"size\":\"17\",\"singleBrand\":\"CONTINENTAL\",\"singlebrandcode\":\"11\"}}";
            //ArrayList GridRows = (ArrayList)JSON.Decode("[" + reValue + "]");
            //foreach (Hashtable item in GridRows)
            //{
            //    //数据返回正确
            //    if (Convert.ToString(item["code"]).Equals("1000"))
            //    {
            //        ArrayList datalit = (ArrayList)JSON.Decode("[" + JSON.Encode(item["data"]) + "]");
            //        foreach (Hashtable ri in datalit)
            //        {
            //            string[] description = Convert.ToString(ri["Description"]).Split(' ');
            //            for (int i = 2; i < description.Length; i++)
            //            {
            //                Figure += description[i];//花纹
            //            }
            //            Specs = description[0];
            //            LoadIndex = description[1].Substring(0, description[1].Length - 1);
            //            SpeedLevel = description[1].Substring(description[1].Length - 1);
            //            TyreCode = Convert.ToString(ri["barcode"]);
            //            GoodsCode = Convert.ToString(ri["material"]);
            //            if (GoodsCode.Length > 6)
            //            {
            //                GoodsCode = GoodsCode + "0000";
            //            }
            //            else if (GoodsCode.Length == 5)
            //            {
            //                GoodsCode = "0" + GoodsCode + "0000";
            //            }
            //            Batch = Convert.ToString(ri["DOT"]);
            //        }
            //    }
            //    else
            //    {
            //        msg.Result = false;
            //        msg.Message = Convert.ToString(item["code"]);
            //        goto ERR;
            //    }
            //}
            //查询原仓库的产品信息
            Model = Specs;

            CargoProductBus bus = new CargoProductBus();
            CargoInterfaceBus inter = new CargoInterfaceBus();
            CargoHouseBus house = new CargoHouseBus();
            CargoOrderBus order = new CargoOrderBus();
            List<CargoFactoryOrderBarcodeEntity> barlist = order.QueryBarcodeInfo(new CargoFactoryOrderBarcodeEntity { Barcode = TyreCode });
            if (barlist.Count > 0)
            {
                SourceOrderNo = barlist[0].VehicleNo;
            }
            #region 通用数据检验

            if (string.IsNullOrEmpty(GoodsCode))
            {
                msg.Result = false;
                msg.Message = "货品代码不能为空";
                goto ERR;
            }
            if (string.IsNullOrEmpty(ContainerCode))
            {
                msg.Result = false;
                msg.Message = "货位代码不能为空";
                goto ERR;
            }
            if (string.IsNullOrEmpty(TagCode))
            {
                msg.Result = false;
                msg.Message = "产品编码不能为空";
                goto ERR;
            }
            if (string.IsNullOrEmpty(Batch))
            {
                msg.Result = false;
                msg.Message = "周期批次不能为空";
                goto ERR;
            }
            #endregion
            List<CargoProductTypeEntity> ptype = bus.QueryProductType(new CargoProductTypeEntity { TypeID = TypeID, ParentID = -1 });
            int parentID = 0;
            if (ptype.Count > 0)
            {
                parentID = ptype[0].ParentID;
            }
            int bYear = 0, bWeek = 0, flatRadio = 0, HubDiameter = 0, TreadWidth = 0;
            switch (parentID)
            {
                case 1:
                    #region 轮胎
                    if (string.IsNullOrEmpty(Specs))
                    {
                        msg.Result = false;
                        msg.Message = "规格不能为空";
                        goto ERR;
                    }
                    if (string.IsNullOrEmpty(Figure))
                    {
                        msg.Result = false;
                        msg.Message = "花纹不能为空";
                        goto ERR;
                    }
                    if (!Common.IsInt(LoadIndex))
                    {
                        msg.Result = false;
                        msg.Message = "载重指数数据有误";
                        goto ERR;
                    }
                    if (string.IsNullOrEmpty(SpeedLevel))
                    {
                        msg.Result = false;
                        msg.Message = "速度级别不能为空";
                        goto ERR;
                    }
                    if (Batch.Length != 4)
                    {
                        msg.Result = false;
                        msg.Message = "周期批次输入有误";
                        goto ERR;
                    }
                    if (!Common.IsInt(Batch))
                    {
                        msg.Result = false;
                        msg.Message = "周期批次输入有误";
                        goto ERR;
                    }
                    if (Batch.Length == 4)
                    {
                        bWeek = Convert.ToInt32(Batch.Substring(0, 2));
                        bYear = Convert.ToInt32(Batch.Substring(2, 2));
                    }
                    else if (Batch.Length == 3)
                    {
                        bWeek = Convert.ToInt32(Batch.Substring(0, 1));
                        bYear = Convert.ToInt32(Batch.Substring(1, 2));
                    }
                    string[] sp = Specs.Split('/');
                    if (sp.Length > 1)
                    {
                        if (Common.IsInt(sp[0]))
                        {
                            TreadWidth = Convert.ToInt32(sp[0]);
                        }
                        string gg = Convert.ToString(sp[1]);
                        if (Common.IsInt(gg.Substring(0, 2)))
                        {
                            flatRadio = Convert.ToInt32(gg.Substring(0, 2));
                        }
                        if (Common.IsInt(gg.Substring(3, 2)))
                        {
                            HubDiameter = Convert.ToInt32(gg.Substring(3, 2));
                        }
                        //HubDiameter = Convert.ToInt32(gg.Substring(gg.Length - 2, 2));
                    }
                    #endregion
                    InCargoNum = "1";
                    Figure = Figure.Replace(" ", "");
                    break;
                default:
                    break;
            }
            #region 验证
            CargoContainerEntity contain = house.QueryContainerEntityByCode(ContainerCode, HouseID);
            if (contain == null || contain.ContainerID.Equals(0))
            {
                msg.Result = false;
                msg.Message = "货位代码不存在";
                goto ERR;
            }
            if (bus.IsExistCargoProductTag(new CargoProductTagEntity { TagCode = TagCode }))
            {
                msg.Result = false;
                msg.Message = "已经存在相同的产品编码：" + TagCode;
                goto ERR;
            }
            #endregion

            #region 获取基础价格数据
            //根据产品类型ID、规格、花纹、速度指数、载重指数、订单月
            Decimal unitPrice = 0.00M, finalCostPrice = 0.00M, costPrice = 0.00M, taxCostPrice = 0.00M, noTaxCostPrice = 0.00M, tradePrice = 0.00M, salePrice = 0.00M;
            string SpecsType = string.Empty;
            if (BelongMonth.Length.Equals(6))
            {
                //获取单价
                CargoFactoryOrderBus ProductUnitPrice = new CargoFactoryOrderBus();
                CargoFactoryOrderEntity factoryOrderEntity = new CargoFactoryOrderEntity();
                factoryOrderEntity.FacOrderNo = SourceOrderNo;
                factoryOrderEntity.HouseID = HouseID;
                factoryOrderEntity.TypeID = TypeID;
                factoryOrderEntity.GoodsCode = GoodsCode;
                CargoFactoryOrderEntity productUnitPrice = ProductUnitPrice.QueryProductUnitPrice(factoryOrderEntity);
                if (productUnitPrice != null && productUnitPrice.ID != 0)
                {
                    unitPrice = Convert.ToDecimal(productUnitPrice.UnitPrice);
                    salePrice = Convert.ToDecimal(productUnitPrice.SalePrice);
                    tradePrice = Convert.ToDecimal(productUnitPrice.SalePrice);

                    costPrice = Convert.ToDecimal(productUnitPrice.CostPrice);
                    taxCostPrice = Convert.ToDecimal(productUnitPrice.TaxCostPrice);
                    noTaxCostPrice = Convert.ToDecimal(productUnitPrice.NoTaxCostPrice);
                    SpecsType = productUnitPrice.SpecsType;
                }

                //获取基础价格成本价
                //外采订单类型不匹配基础价格
                if (OrderType != "3")
                {
                    string proYear, proMonth;
                    proYear = BelongMonth.Substring(0, 4);
                    proMonth = BelongMonth.Substring(4, 2);
                    CargoProductBasicPriceEntity basicPriceEntity = new CargoProductBasicPriceEntity();
                    basicPriceEntity.HouseID = HouseID;
                    basicPriceEntity.TypeID = TypeID;
                    basicPriceEntity.GoodsCode = GoodsCode;
                    basicPriceEntity.ProYear = proYear;
                    basicPriceEntity.ProMonth = proMonth;
                    CargoProductBasicPriceEntity basicPrice = house.QueryProductBasicPrice(basicPriceEntity);
                    if (basicPrice != null && basicPrice.PID != 0)
                    {
                        finalCostPrice = Convert.ToDecimal(basicPrice.FinalCostPrice);
                        if (costPrice.Equals(0))
                        {
                            costPrice = Convert.ToDecimal(basicPrice.CostPrice);
                            taxCostPrice = Convert.ToDecimal(basicPrice.TaxCostPrice);
                            noTaxCostPrice = Convert.ToDecimal(basicPrice.NoTaxCostPrice);
                        }
                        if (!basicPrice.TradePrice.Equals(0))
                        {
                            tradePrice = Convert.ToDecimal(basicPrice.TradePrice);
                            salePrice = Convert.ToDecimal(basicPrice.TradePrice);
                        }
                    }
                }
            }
            #endregion

            inter.scanTagInCargo(new CargoProductEntity { ProductName = ProductName, GoodsCode = GoodsCode, HouseID = HouseID, TypeID = TypeID, Specs = Specs, Figure = Figure, Model = Model, Batch = Batch, ContainerCode = ContainerCode, TagCode = TagCode, BatchYear = bYear, BatchWeek = bWeek, InCargoID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString(), OPID = UserID, LoadIndex = LoadIndex, SpeedLevel = SpeedLevel, Meridian = "R", TreadWidth = TreadWidth, FlatRatio = flatRadio, HubDiameter = HubDiameter, InCargoStatus = "1", InHouseTime = DateTime.Now, OperaType = "1", Born = Born, Assort = Assort, TyreCode = TyreCode, Numbers = Convert.ToInt32(InCargoNum), ParentID = parentID, Source = Source, SourceOrderNo = SourceOrderNo, BelongMonth = BelongMonth, UnitPrice = unitPrice, FinalCostPrice = finalCostPrice, CostPrice = costPrice, TaxCostPrice = taxCostPrice, NoTaxCostPrice = noTaxCostPrice, TradePrice = tradePrice, SalePrice = salePrice, HLYReturnID = HLYReturnID, SpecsType = SpecsType }, log);
        ERR:
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
        }
        public static string PostForm(string requestUri, NameValueCollection postData, CookieContainer cookie)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "post";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json;charset=UTF-8";

            request.CookieContainer = cookie;

            StringBuilder stringBuilder = new StringBuilder();
            foreach (string key in postData.Keys)
            {
                stringBuilder.AppendFormat("&{0}={1}", key, postData.Get(key));
            }
            byte[] buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString().Trim('&'));
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();

        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受 
        }
        public static string PostFormMultipart(string requestUri, NameValueCollection postData, CookieContainer cookie)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            string boundary = string.Format("-----{0}", DateTime.Now.Ticks.ToString("x"));
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(requestUri);
            //webrequest.CookieContainer = cookie;
            webrequest.Timeout = 120000;
            webrequest.Method = "post";
            webrequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);

            Stream requestStream = webrequest.GetRequestStream();
            foreach (string key in postData.Keys)
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("--{0}", boundary);
                strBuilder.AppendFormat("\r\nContent-Disposition: form-data; name=\"{0}\"", key);

                if (System.IO.File.Exists(postData.Get(key)))
                {
                    strBuilder.AppendFormat(";filename=\"{0}\"\r\nContent-Type: multipart/form-data\r\n\r\n", Path.GetFileName(postData.Get(key)));
                    byte[] buffer = Encoding.UTF8.GetBytes(strBuilder.ToString());
                    requestStream.Write(buffer, 0, buffer.Length);
                    //获取图片流
                    FileStream fileStream = new FileStream(postData.Get(key), FileMode.Open, FileAccess.Read);
                    BinaryReader binaryReader = new BinaryReader(fileStream);
                    byte[] fileBuffer = binaryReader.ReadBytes((int)fileStream.Length);
                    binaryReader.Close();
                    fileStream.Close();
                    requestStream.Write(fileBuffer, 0, fileBuffer.Length);
                }
                else
                {
                    strBuilder.AppendFormat("\r\n\r\n{0}\r\n", postData.Get(key));
                    byte[] buff = Encoding.UTF8.GetBytes(strBuilder.ToString());
                    requestStream.Write(buff, 0, buff.Length);
                }
            }

            byte[] boundaryBuffer = Encoding.UTF8.GetBytes(string.Format("\r\n--{0}\r\n", boundary));
            requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            requestStream.Close();

            WebResponse response = webrequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }

        private void dd()
        {
            LogEntity log = new LogEntity();
            log.IPAddress = "";
            log.Moudle = "服务管理";
            log.Status = "0";
            log.NvgPage = "定时服务";
            log.UserID = "2029";
            log.Operate = "A";

            List<CargoMonthStatisEntity> monthStatisList = new List<CargoMonthStatisEntity>();
            CargoHouseBus house = new CargoHouseBus();
            //List<CargoHouseEntity> houselist = house.QueryALLHouse();
            //湖南，湖北，广东，揭阳，广通慧采，狄乐汽服仓，华南配件仓，华南枢纽，佛山云配，中山云配
            string houseArr = "64,65,82,84,86,87";
            CargoLeaderCookpitEntity coo = new CargoLeaderCookpitEntity();
            coo.StartDate = DateTime.Now.AddMonths(-1);
            coo.EndDate = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            coo.CargoPermisID = houseArr;
            //LogHelper.WriteLog("Begin 开始取数据");
            monthStatisList.AddRange(queryByProductType(coo));
            monthStatisList.AddRange(queryBySaleMan(coo));
            monthStatisList.AddRange(queryByClient(coo));
            //LogHelper.WriteLog("End 结束取数据");
            CargoReportBus report = new CargoReportBus();
            report.saveMonthStatis(monthStatisList, log);
            //LogHelper.WriteLog("End 保存数据");

            coo.StartDate = DateTime.Now.AddMonths(-3);
            coo.EndDate = DateTime.Now.AddMonths(-2).AddDays(1 - DateTime.Now.AddMonths(-2).Day);
            //LogHelper.WriteLog("Begin 开始取3月前数据");
            monthStatisList.AddRange(queryByProductType(coo));
            monthStatisList.AddRange(queryBySaleMan(coo));
            monthStatisList.AddRange(queryByClient(coo));
            //LogHelper.WriteLog("End 结束取3月前数据");
            report.updateMonthStatis(monthStatisList, log);
            //LogHelper.WriteLog("End 保存3月前数据");
        }
        /// <summary>
        /// 按客户统计 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<CargoMonthStatisEntity> queryByClient(CargoLeaderCookpitEntity entity)
        {
            List<CargoMonthStatisEntity> monthStatisList = new List<CargoMonthStatisEntity>();
            CargoReportBus report = new CargoReportBus();
            List<CargoLeaderCookpitEntity> saleList = report.QueryByClient(new CargoLeaderCookpitEntity
            {
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CargoPermisID = entity.CargoPermisID
            });
            if (saleList.Count > 0)
            {
                foreach (var it in saleList)
                {
                    CargoMonthStatisEntity sta = new CargoMonthStatisEntity();
                    sta.Title = it.AcceptPeople;
                    sta.HouseID = it.HouseID;
                    sta.SaleNum = it.Piece;
                    sta.SaleIncome = it.TotalCharge;
                    sta.StatisType = "3";
                    sta.Year = DateTime.Now.AddMonths(-1).Year;
                    sta.Month = DateTime.Now.AddMonths(-1).Month;
                    monthStatisList.Add(sta);
                }
            }
            return monthStatisList;
        }

        /// <summary>
        /// 按业务员统计 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<CargoMonthStatisEntity> queryBySaleMan(CargoLeaderCookpitEntity entity)
        {
            List<CargoMonthStatisEntity> monthStatisList = new List<CargoMonthStatisEntity>();
            CargoReportBus report = new CargoReportBus();
            List<CargoLeaderCookpitEntity> saleList = report.QueryBySaleMan(new CargoLeaderCookpitEntity
            {
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CargoPermisID = entity.CargoPermisID
            });
            if (saleList.Count > 0)
            {
                foreach (var it in saleList)
                {
                    CargoMonthStatisEntity sta = new CargoMonthStatisEntity();
                    sta.Title = it.SaleManName;
                    sta.HouseID = it.HouseID;
                    sta.SaleNum = it.Piece;
                    sta.SaleIncome = it.TotalCharge;
                    sta.StatisType = "2";
                    sta.Year = DateTime.Now.AddMonths(-1).Year;
                    sta.Month = DateTime.Now.AddMonths(-1).Month;
                    monthStatisList.Add(sta);
                }
            }
            return monthStatisList;
        }
        /// <summary>
        /// 按产品类型统计 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<CargoMonthStatisEntity> queryByProductType(CargoLeaderCookpitEntity entity)
        {
            List<CargoMonthStatisEntity> monthStatisList = new List<CargoMonthStatisEntity>();
            CargoReportBus report = new CargoReportBus();
            List<CargoLeaderCookpitEntity> typeList = report.QueryByProductType(new CargoLeaderCookpitEntity
            {
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CargoPermisID = entity.CargoPermisID
            });

            if (typeList.Count > 0)
            {
                foreach (var it in typeList)
                {
                    if (it.ParentID.Equals(20))//去掉赠品 
                    {
                        continue;
                    }
                    CargoMonthStatisEntity sta = new CargoMonthStatisEntity();
                    sta.Title = it.TypeName;
                    sta.HouseID = it.HouseID;
                    sta.SaleNum = it.Piece;
                    sta.SaleIncome = it.TotalCharge;
                    sta.StatisType = "1";
                    sta.Year = DateTime.Now.AddMonths(-1).Year;
                    sta.Month = DateTime.Now.AddMonths(-1).Month;
                    monthStatisList.Add(sta);
                }

                var total = typeList.Where(c => c.ParentID != 20).GroupBy(c => c.HouseID).Select(g => new
                {
                    ID = g.Key,
                    Piece = g.Sum(c => c.Piece),
                    TotalCharge = g.Sum(c => c.TotalCharge)
                }).ToList();
                foreach (var it in total)
                {
                    CargoMonthStatisEntity sta = new CargoMonthStatisEntity();
                    sta.Title = "汇总";
                    sta.HouseID = it.ID;
                    sta.SaleNum = it.Piece;
                    sta.SaleIncome = it.TotalCharge;
                    sta.StatisType = "0";
                    sta.Year = DateTime.Now.AddMonths(-1).Year;
                    sta.Month = DateTime.Now.AddMonths(-1).Month;
                    monthStatisList.Add(sta);
                }
            }

            return monthStatisList;
        }

        #region 服务号 模板信息推送
        //Requests为一个通用的get或post请求方法，RequestUrl为请求的地址，RequestMethod请求的get或post
        public static string ServiceRequests(string RequestUrl, string RequestMethod)
        {
            string RequestUrls = RequestUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestUrls);
            request.Method = RequestMethod;
            request.ContentType = "textml;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string jsonData = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            string jsonString = jsonData;
            return jsonString;
        }
        /// <summary>
        /// 服务号 销售情况通知
        /// </summary>
        private void SendSalesMessage()
        {
            CargoWeiXinBus cargoWeiXinBus = new CargoWeiXinBus();

            string strReturn = "";
            try
            {
                //获取 access_token
                string AppID = Common.GetdltAPPID();
                string AppSecret = Common.GetdltAppSecret();
                string AccessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);//构建请求的url，获取access_token
                string AccessToken = ServiceRequests(AccessTokenUrl, "get");
                JObject json1 = JObject.Parse(AccessToken);
                var accessToken = json1["access_token"];

                //LogHelper.WriteLog("服务号模板信息推送 access_token:" + accessToken);
                if (accessToken != null)
                {
                    //获取指定天数内销售情况
                    string days = "1"; //天数
                    days = string.IsNullOrEmpty(days) ? "1" : days;
                    var SupplierSalesData = cargoWeiXinBus.QueryWxSupplierSalesData(null, Convert.ToInt32(days));

                    //获取供应商数据 ，获取推送者的 openid
                    var SupplierList = cargoWeiXinBus.QueryWxSupplierOpenID();
                    foreach (var item in SupplierSalesData)
                    {
                        foreach (var item2 in SupplierList.Where(a => item.SuppClientNum.ToString() == a.ClientNum))
                        {
                            string openId = item2.wxOpenID;
                            //string templateId = GetdltSalesModelID(); //设置的模板ID
                            string templateId = "oCGTDH6IfLYlRC-TaEan_LN1E_Ffn9auGmU5LkAGga4"; //设置的模板ID
                                                                                               //string toUrl = GetdltServiceToUrl(); //跳转url
                            string toUrl = "http://dlt.neway5.com/Weixin/wxSupplierDataDisplay.aspx"; //跳转url
                            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
                            //设置推送的模板，包括推送的openid、模板ID-templateId 、推送的信息：dt1.Rows[i]["XXX"]等
                            string temp = "{\"touser\": \"" + openId + "\"," +

                                       "\"template_id\": \"" + templateId + "\", " +

                                       "\"topcolor\": \"#00ffea\", " +

                                        "\"url\": \"" + toUrl + "\", " +

                                       "\"data\": " +

                                       "{\"thing2\": {\"value\": \"" + (item.HouseName.Length >= 15 ? item.HouseName.Substring(0, 13) + "..." : item.HouseName) + "\"}," +

                                       "\"thing3\": {\"value\": \"" + (item.GoodsName.Length >= 15 ? item.GoodsName.Substring(0, 13) + "..." : item.GoodsName) + "\"}," +

                                       "\"character_string4\": { \"value\": \"" + (item.Piece) + "\"}," +

                                       "\"time5\": {\"value\": \"" + Convert.ToDateTime(DateTime.Now) + "\" }}}";
                            //核心：进行推送请求，并返回相应的反馈信息，如：{"errcode":0,"errmsg":"ok","msgid":XXXXX}
                            string results = GetResponseData(temp, url);
                            strReturn = item2.Name + "——" + openId + "——" + "推送成功";
                            Console.WriteLine(strReturn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
                //Console.WriteLine(strReturn);
            }

        }
        /// <summary>
        /// 服务号 账单推送
        /// </summary>
        private void SendBillMessage()
        {
            CargoWeiXinBus cargoWeiXinBus = new CargoWeiXinBus();

            string strReturn = "";
            try
            {
                //获取 access_token
                string AppID = Common.GetdltAPPID();
                string AppSecret = Common.GetdltAppSecret();
                string AccessTokenUrl = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID, AppSecret);//构建请求的url，获取access_token
                string AccessToken = ServiceRequests(AccessTokenUrl, "get");
                JObject json1 = JObject.Parse(AccessToken);
                var accessToken = json1["access_token"];

                //LogHelper.WriteLog("服务号模板信息推送 access_token:" + accessToken);
                if (accessToken != null)
                {
                    //获取供应商数据 ，获取推送者的 openid
                    var SupplierList = cargoWeiXinBus.QueryWxSupplierOpenID();
                    for (int j = 0; j < SupplierList.Count; j++)
                    {
                        //获取账单数据
                        CargoOrderEntity entity = new CargoOrderEntity();
                        var SupplierSalesData = cargoWeiXinBus.QueryWxSupplierBillData(SupplierList[j].wxOpenID).FirstOrDefault();

                        if (SupplierSalesData == null) continue;

                        string openId = SupplierList[j].wxOpenID;
                        string templateId = Common.GetdltBillModelID(); //设置的模板ID
                        //string toUrl = Common.GetdltServiceBillToUrl(); //跳转url
                        string toUrl = "http://dlt.neway5.com/Weixin/wxDetail.aspx"; //跳转url
                        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accessToken;
                        //设置推送的模板，包括推送的openid、模板ID-templateId 、推送的信息：dt1.Rows[i]["XXX"]等
                        string temp = "{\"touser\": \"" + openId + "\"," +

                                      "\"template_id\": \"" + templateId + "\", " +

                                      "\"topcolor\": \"#00ffea\", " +

                                       "\"url\": \"" + toUrl + "\", " +

                                      "\"data\": " +

                                      "{\"character_string11\": {\"value\": \"" + (string.IsNullOrEmpty(SupplierSalesData.AccountNO) ? "-" : SupplierSalesData.AccountNO) + "\"}," +

                                      "\"amount2\": { \"value\": \"" + (SupplierSalesData.Total) + "\"}," +

                                      "\"time3\": {\"value\": \"" + Convert.ToDateTime(DateTime.Now) + "\" }}}";
                        //核心：进行推送请求，并返回相应的反馈信息，如：{"errcode":0,"errmsg":"ok","msgid":XXXXX}
                        string results = GetResponseData(temp, url);
                        strReturn = SupplierList[j].Name + "——" + openId + "——" + "推送成功";
                        Console.WriteLine(strReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                strReturn = ex.Message;
                //Console.WriteLine(strReturn);
            }

        }

        //获取请求的数据并返回相应的提示信息
        public static string GetResponseData(string JSONData, string Url)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);
            //声明一个HttpWebRequest请求
            request.Timeout = 90000;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string strResult = streamReader.ReadToEnd();
            streamReceive.Dispose();
            streamReader.Dispose();
            return strResult;
        }

        #endregion
    }

    public class LYEntity
    {
        public string ak { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string company { get; set; }

    }

    public class TyreEntity
    {
        public int code { get; set; }
        public string msg { get; set; }

        public TyreData data { get; set; }
    }
    public class TyreData
    {
        public List<respdata> respdata { get; set; }

    }
    public class respdata
    {
        public string typeName { get; set; }
        public string HBPrice { get; set; }
        public string amount { get; set; }
        public string article { get; set; }
        public string brand { get; set; }
        public string carType { get; set; }
        public string clientPrice { get; set; }
        public string commodityDetails { get; set; }
        public string commonCarType { get; set; }
        public string commonSpecialType { get; set; }
        public string cuXiaoPrice { get; set; }
        public string cxTitle { get; set; }
        public string flagNotWebShow { get; set; }
        public string img { get; set; }
        public string innerCode { get; set; }
        public string iscx { get; set; }
        public string onlineIntegral { get; set; }
        public string outPrice1 { get; set; }
        public string outPrice2 { get; set; }
        public string outPrice3 { get; set; }
        public string outPrice4 { get; set; }
        public string outPrice5 { get; set; }
        public string outPrice6 { get; set; }
        public string partCode { get; set; }
        public string partName { get; set; }
        public string pics { get; set; }
        public string price { get; set; }
        public string spec { get; set; }
        public string specialType { get; set; }
        public string storageAmount1 { get; set; }
        public string storageAmount2 { get; set; }
        public string storageIsExist { get; set; }
        public string storageName1 { get; set; }
        public string storageName2 { get; set; }
        public string typeInnerCode { get; set; }
        public string unit { get; set; }
        public string yuanPrice { get; set; }
    }


    public partial class YouZanEntity
    {
        public string source_order_no { get; set; }
        public string create_time { get; set; }
        public string retail_source { get; set; }
        public int operate_type { get; set; }
        public string creator { get; set; }
        public string warehouse_code { get; set; }
        public string remark { get; set; }
        public List<YouZanAdjustEntity> order_items { get; set; }

        public long admin_id { get; set; }
        public int page_no { get; set; }
        public int type { get; set; }
        public int page_size { get; set; }
    }
    public partial class YouZanAdjustEntity
    {
        public string sku_code { get; set; }
        public string quantity { get; set; }
        public string supplier_code { get; set; }
    }
}