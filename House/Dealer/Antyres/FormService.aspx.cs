using House.Business.Cargo;
using House.Entity.Cargo;
using House.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using House.Entity.House;
using System.Collections;
using House.Business;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using System.Security.Cryptography;
using NPOI.HSSF.Record.Formula.Functions;
using NPOI.HSSF.Model;
using static NPOI.HSSF.Util.HSSFColor;
using System.Web.Routing;
using System.Data;
using System.Text.RegularExpressions;
using NPOI.POIFS.FileSystem;
using System.Drawing;

namespace Dealer.Antyres
{
    public partial class FormService : BasePage
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
                log.UserID = UserInfor == null ? "" : UserInfor.LoginName.Trim();
                log.Memo = methodName + " " + ex.Message + " " + ex.StackTrace;
                bus.InsertLog(log);
            }
        }

        /// <summary>
        /// 根据订单号查询订单的跟踪状态 
        /// </summary>
        public void QueryOrderStatus()
        {
            string OrderNo = Request["OrderNo"];
            string LogisAwbNo = Request["LogisAwbNo"];
            string res = string.Empty;
            CargoOrderBus bus = new CargoOrderBus();
            OrderStatusReturnEntity returnEntity = new OrderStatusReturnEntity();
            CargoOrderStatusEntity queryEntity = new CargoOrderStatusEntity();
            queryEntity.OrderNo = OrderNo;
            returnEntity.Status = -1;
            List<CargoOrderStatusEntity> result = bus.QueryOrderStatus(queryEntity);
            if (result.Count > 0)
            {
                if (!string.IsNullOrEmpty(LogisAwbNo))
                {
                    res = "<div style='font-size:14px;font-weight:bold'>订单号：" + result[0].OrderNo + " 物流单号：" + LogisAwbNo + " 的物流跟踪状态";
                }
                else
                {
                    res = "<div style='font-size:14px;font-weight:bold'>订单号：" + result[0].OrderNo + "  的跟踪记录";
                }
                res += "</div>";
                res += "<table class='commTblStyle_8' border='0' width='100%' style='border-spacing:0;border-collapse:collapse;font-size:14px;'><tbody><tr><th>操作时间</th><th>操作人</th><th>订单状态</th></tr>";
                foreach (var it in result)
                {
                    string tf = string.Empty;
                    string img = string.Empty;
                    switch (it.OrderStatus.Trim())
                    {
                        case "0":
                            returnEntity.StartLatitude = it.Latitude;
                            returnEntity.StartLongitude = it.Longitude;
                            returnEntity.StartTime = it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 0;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>已下单</span>"; break;
                        case "1":
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 1;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>出库中</span>"; break;
                        case "2":
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 2;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>已出库</span>"; break;
                        case "3":
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 3;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>运输在途</span>"; break;
                        case "4":
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 4;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>已到达</span>"; break;
                        case "5":
                            returnEntity.EndTime = it.SignTime.ToString("yyyy-MM-dd HH:mm:ss");

                            tf = "<span style='color:red;font-weight:bold;'>已签收</span>"; it.OP_Name = it.Signer;
                            string[] im = it.SignImage.Split('|');
                            if (im.Length > 0)
                            {
                                for (int i = 0; i < im.Length; i++)
                                {
                                    if (string.IsNullOrEmpty(im[i])) { continue; }
                                    img += "<img onclick=download(\"" + im[i] + "\") style='width:30px; height:20px;margin-left:3px;' src='" + im[i] + "' title='点击查看图片'/>";
                                }
                            }
                            break;
                        case "6":
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 6;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>已拣货</span>"; break;
                        case "8":
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 8;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>已接单</span>"; break;
                        case "9":
                            if (returnEntity.Status == -1)
                            {
                                returnEntity.Status = 9;
                            }
                            tf = "<span style='color:red;font-weight:bold;'>已提货</span>"; break;
                        default:
                            break;
                    }
                    res += "<tr><td>" + it.OP_DATE.ToString("yyyy-MM-dd HH:mm:ss") + "</td><td>" + it.OP_Name + "</td><td>" + tf + it.DetailInfo + img + "</td></tr>";
                }
                res += "</tbody><table>";
                returnEntity.HtmlStr = res;
            }
            String json = JSON.Encode(returnEntity);
            Response.Clear();
            Response.Write(json);
            Response.End();

        }
        /// <summary>
        /// 查询订单数据
        /// </summary>
        public void QueryOrderInfo()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.HouseID.ToString();
            queryEntity.PayClientNum = Convert.ToInt32(UserInfor.LoginName);
            if (Convert.ToString(Request["AwbStatus"]) != "-1")
            {
                queryEntity.AwbStatus = Convert.ToString(Request["AwbStatus"]);
            }
            if (Convert.ToString(Request["CheckStatus"]) != "-1")
            {
                queryEntity.CheckStatus = Convert.ToString(Request["CheckStatus"]);
            }
            if (Convert.ToString(Request["OrderModel"]) != "-1")
            {
                queryEntity.OrderModel = Convert.ToString(Request["OrderModel"]);
            }
            if (Convert.ToString(Request["ThrowGood"]) != "-1")
            {
                queryEntity.ThrowGood = Convert.ToString(Request["ThrowGood"]);
            }
            //2021-01-30 添加客户名称查询条件
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AcceptPeople"]))) { queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]); }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryOrderInfo(pageIndex, pageSize, queryEntity);
            //if (list != null) { QueryOrderInfoList = list; }
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public Hashtable QueryOrderInfoList
        {
            get
            {
                if (Session["QueryOrderInfoList"] == null)
                {
                    Session["QueryOrderInfoList"] = new Hashtable();
                }
                return (Hashtable)(Session["QueryOrderInfoList"]);
            }
            set
            {
                Session["QueryOrderInfoList"] = value;
            }
        }
        /// <summary>
        /// 查询订单数据导出
        /// </summary>
        public void QueryOrderInfoExport()
        {
            string err = "OK";
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.HouseID.ToString();
            queryEntity.PayClientNum = Convert.ToInt32(UserInfor.LoginName);
            if (Convert.ToString(Request["AwbStatus"]) != "-1")
            {
                queryEntity.AwbStatus = Convert.ToString(Request["AwbStatus"]);
            }
            if (Convert.ToString(Request["CheckStatus"]) != "-1")
            {
                queryEntity.CheckStatus = Convert.ToString(Request["CheckStatus"]);
            }
            if (Convert.ToString(Request["OrderModel"]) != "-1")
            {
                queryEntity.OrderModel = Convert.ToString(Request["OrderModel"]);
            }
            if (Convert.ToString(Request["ThrowGood"]) != "-1")
            {
                queryEntity.ThrowGood = Convert.ToString(Request["ThrowGood"]);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AcceptPeople"]))) { queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]); }
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryOrderInfo(1, 10000, queryEntity);
            if (list != null) { QueryOrderInfoList = list; }

            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 简易开单保存方法
        /// </summary>
        public void saveSimpleOrderData()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            CargoHouseBus house = new CargoHouseBus();
            msg.Result = true;
            string json = Request["submitData"];
            ArrayList goods = (ArrayList)JSON.Decode(json);
            if (goods.Count <= 0)
            {
                msg.Message = "没有产品数据";
                msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
                Response.End();
                return;
            }

            CargoOrderBus bus = new CargoOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.NvgPage = "新增销售单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            log.Status = "0";

            CargoOrderEntity ent = new CargoOrderEntity();
            List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
            List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
            //List<QyOrderUpdateGoodsEntity> goods = new List<QyOrderUpdateGoodsEntity>();

            try
            {
                int HouseID = Convert.ToInt32(Request["HouseID"]);
                CargoHouseEntity houseEnt = house.QueryCargoHouseByID(HouseID);
                string HouseCode = houseEnt.HouseCode;
                string HouseName = houseEnt.Name;

                #region 赋值
                ent.HAwbNo = Convert.ToString(Request["HAwbNo"]);//关联订单号

                ent.Dep = houseEnt.DepCity;
                ent.Dest = Convert.ToString(Request["AAcceptCity"]);
                ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);
                ent.Weight = 0; ent.Volume = 0; ent.TransitFee = 0;
                ent.DeliveryFee = 0; ent.OtherFee = 0;
                ent.TotalCharge = string.IsNullOrEmpty(Convert.ToString(Request["TotalCharge"])) ? 0 : Convert.ToDecimal(Request["TotalCharge"]);
                ent.TransportFee = ent.TotalCharge;
                ent.Rebate = 0;
                ent.HouseID = HouseID;
                ent.HouseName = HouseName;
                ent.OutHouseName = HouseName;
                ent.CheckOutType = "0";
                ent.TrafficType = "0";// 内部订单
                ent.DeliveryType = "0";//Convert.ToString(Request["DeliveryType"]);
                ent.AcceptAddress = Convert.ToString(Request["AcceptAddress"]);
                ent.AcceptPeople = Convert.ToString(Request["AAcceptPeople"]);
                ent.AcceptUnit = string.IsNullOrEmpty(Convert.ToString(Request["AAcceptUnit"])) ? ent.AcceptPeople : Convert.ToString(Request["AAcceptUnit"]);
                ent.AcceptTelephone = Convert.ToString(Request["AAcceptTelephone"]);
                ent.AcceptCellphone = Convert.ToString(Request["AAcceptCellphone"]);
                ent.CreateAwb = UserInfor.UserName;
                ent.CreateAwbID = UserInfor.LoginName;
                ent.CreateDate = DateTime.Now;
                ent.OP_ID = UserInfor.LoginName;
                ent.SaleManID = UserInfor.SaleManID;
                ent.SaleManName = UserInfor.SaleManName;
                ent.SaleCellPhone = Convert.ToString(Request["SaleCellPhone"]);
                ent.Remark = Convert.ToString(Request["Remark"]);
                ent.ThrowGood = string.IsNullOrEmpty(Convert.ToString(Request["ThrowGood"])) ? "0" : Convert.ToString(Request["ThrowGood"]);
                if (ent.ThrowGood == "17")
                {
                    //急送单
                    //ent.DeliveryFee = (ent.Piece == 1 ? 50 : ent.Piece == 2 ? 35 : 25) * ent.Piece;
                    ent.TransportFee = ent.TotalCharge = (ent.Piece == 1 ? 40 : ent.Piece == 2 ? 25 : 15) * ent.Piece;
                }
                
                ent.IsPrintPrice = string.IsNullOrEmpty(Convert.ToString(Request["IsPrintPrice"])) ? 0 : 1;
                ent.TranHouse = "";
                ent.PostponeShip = "1";
                ent.ClientNum = Convert.ToInt32(UserInfor.LoginName);
                ent.PayClientNum = Convert.ToInt32(UserInfor.LoginName);
                ent.PayClientName = UserInfor.UserName;//付款人客户姓名
                ent.ClientID = UserInfor.UserID;
                #endregion
                int pieceSum = 0;
                string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
                int OrderNum = 0;
                ent.OrderNo = Common.GetMaxOrderNumByCurrentDate(HouseID, HouseCode, out OrderNum);
                ent.OrderNum = OrderNum;//最新订单顺序号
                CargoInterfaceBus interBus = new CargoInterfaceBus();
                CargoProductBus product = new CargoProductBus();
                List<CargoInterfaceEntity> goodList = new List<CargoInterfaceEntity>();
                for (int i = 0; i < goods.Count; i++)
                {
                    Hashtable ht = (Hashtable)goods[i];
                    //如果件数为空或为0则忽略处理
                    if (string.IsNullOrEmpty(Convert.ToString(ht["Piece"])))
                    {
                        continue;
                    }
                    if ((Convert.ToInt32(ht["InPiece"]) - Convert.ToInt32(ht["Piece"])).Equals(0))
                    {
                        continue;
                    }
                    else
                    {
                        goodList.Add(new CargoInterfaceEntity
                        {
                            ProductName = Convert.ToString(ht["ProductName"]),
                            GoodsCode = Convert.ToString(ht["GoodsCode"]),
                            TypeID = Convert.ToInt32(ht["TypeID"]),
                            Model = Convert.ToString(ht["Model"]),
                            Specs = Convert.ToString(ht["Specs"]),
                            //Batch = Convert.ToString(ht["Batch"]),
                            BatchYear = Convert.ToInt32(ht["BatchYear"]),
                            StockNum = Convert.ToInt32(ht["InPiece"]) - Convert.ToInt32(ht["Piece"]),
                            Figure = Convert.ToString(ht["Figure"])
                        });
                    }
                }
                ent.UserRulePiece = 0;
                //int pieceSum = 0;
                foreach (var itt in goodList)
                {
                    pieceSum += itt.StockNum;
                    int piece = itt.StockNum;
                    //如果周期为空，产品类型是轮胎，则判断当前日期是本年的第几周，不能出一年以前周期轮胎
                    int BatckWeek = 0;
                    int parentID = 0;
                    List<CargoProductTypeEntity> ptype = product.QueryProductType(new CargoProductTypeEntity { TypeID = itt.TypeID, ParentID = -1 });
                    if (ptype.Count > 0) { parentID = ptype[0].ParentID; }
                    CargoInterfaceEntity queryEntity = new CargoInterfaceEntity
                    {
                        HouseID = ent.HouseID,
                        TypeID = itt.TypeID,
                        ParentID = parentID,
                        Specs = itt.Specs,
                        Figure = itt.Figure,
                        SpeedLevel = itt.SpeedLevel,
                        LoadIndex = itt.LoadIndex,
                        GoodsCode = itt.GoodsCode,
                        BatchYear = itt.BatchYear
                    };
                    List<CargoInterfaceEntity> stockList = interBus.queryCargoStock(queryEntity);
                    if (stockList.Count <= 0)
                    {
                        msg.Message = "规格：" + queryEntity.Specs + "，花纹：" + queryEntity.Figure + "库存为空";
                        msg.Result = false;
                        //返回处理结果
                        string res = JSON.Encode(msg);
                        Response.Write(res);
                        Response.End();
                        return;
                    }
                    if (stockList.Sum(c => c.StockNum) < piece)
                    {
                        msg.Message = "规格：" + queryEntity.Specs + "，花纹：" + queryEntity.Figure + "库存不足，库存只剩：" + stockList.Sum(c => c.StockNum).ToString();
                        msg.Result = false;
                        //返回处理结果
                        string res = JSON.Encode(msg);
                        Response.Write(res);
                        Response.End();
                        return;
                    }
                  
                    //减库存规则，一周期早的先出先进先出，二数量和库存数刚好一样的先出
                    foreach (var it in stockList)
                    {
                        if (it.StockNum <= 0) { continue; }
                        CargoContainerShowEntity cargo = new CargoContainerShowEntity();
                        cargo.OrderNo = ent.OrderNo;//订单号
                        cargo.OutCargoID = outID;
                        cargo.ContainerID = it.ContainerID;
                        cargo.TypeID = it.TypeID;
                        cargo.ProductID = it.ProductID;

                        cargo.ID = it.ContainerGoodsID;//库存表ID

                        cargo.HouseName = it.HouseName;
                        cargo.ProductName = it.ProductName;
                        cargo.Model = it.Model;
                        cargo.Specs = it.Specs;
                        cargo.Figure = it.Figure;
                        cargo.GoodsCode = it.GoodsCode;
                        cargo.Batch = it.BatchYear.ToString();
                        cargo.ThrowGood = ent.ThrowGood;
                        cargo.DeliveryFee = ent.DeliveryFee;
                        #region 减库存逻辑
                        if (piece < it.StockNum)
                        {
                            //部分出
                            entDest.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = ent.OrderNo,
                                ProductID = it.ProductID,
                                HouseID = ent.HouseID,
                                AreaID = it.AreaID,
                                Piece = piece,
                                ActSalePrice = itt.ActSalePrice,
                                ContainerCode = it.ContainerCode,
                                OutCargoID = outID,
                                OP_ID = log.UserID
                            });
                            cargo.Piece = piece;
                            cargo.InPiece = it.StockNum;

                            outHouseList.Add(cargo);
                            break;
                        }
                        if (piece.Equals(it.StockNum))
                        {
                            //要出库件数和第一条库存件数刚刚好，则就全部出
                            entDest.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = ent.OrderNo,
                                ProductID = it.ProductID,
                                HouseID = ent.HouseID,
                                AreaID = it.AreaID,
                                Piece = piece,
                                ActSalePrice = itt.ActSalePrice,
                                ContainerCode = it.ContainerCode,
                                OutCargoID = outID,
                                OP_ID = log.UserID
                            });
                            cargo.Piece = piece;
                            cargo.InPiece = it.StockNum;

                            outHouseList.Add(cargo);
                            break;
                        }
                        if (piece > it.StockNum)
                        {
                            //全部出
                            entDest.Add(new CargoOrderGoodsEntity
                            {
                                OrderNo = ent.OrderNo,
                                ProductID = it.ProductID,
                                HouseID = ent.HouseID,
                                AreaID = it.AreaID,
                                Piece = it.StockNum,
                                ActSalePrice = itt.ActSalePrice,
                                ContainerCode = it.ContainerCode,
                                OutCargoID = outID,
                                OP_ID = log.UserID
                            });
                            cargo.Piece = it.StockNum;
                            cargo.InPiece = it.StockNum;

                            outHouseList.Add(cargo);
                            piece = piece - it.StockNum;
                            continue;
                        }
                        #endregion
                    }
                }
                if (!ent.Piece.Equals(pieceSum))
                {
                    msg.Message = "出库件数不一致";
                    msg.Result = false;
                    //返回处理结果
                    string res = JSON.Encode(msg);
                    Response.Write(res);
                    Response.End();
                    return;
                }
                ent.AwbStatus = "0";
                ent.OrderType = "0";
                ent.goodsList = entDest;
                ent.LogisID = 34;
                //ent.LogisAwbNo = ent.OrderNo;
                ent.FinanceSecondCheck = "0";
                if (msg.Result)
                {
                    ////仓库同步缓存
                    //foreach (CargoContainerShowEntity time in outHouseList)
                    //{
                    //    CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());
                    //    //34 马牌  1 同步马牌  2 同步全部品牌
                    //    if (syncProduct.SyncType == "2" || (syncProduct.SyncType == "1" && syncProduct.TypeID == 34))
                    //    {
                    //        RedisHelper.HashSet("OpenSystemStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.GoodsCode);
                    //    }
                    //}
                    bus.AddOrderInfo(ent, outHouseList, log);
                    if (UserInfor.HouseID == 10)
                    {
                        //bus.InsertCargoOrderPush(new CargoOrderPushEntity
                        //{
                        //    OrderNo = ent.OrderNo,
                        //    Dep = ent.Dep,
                        //    Dest = ent.Dest,
                        //    Piece = ent.Piece,
                        //    TransportFee = ent.TransportFee,
                        //    ClientNum = ent.ClientNum.ToString(),
                        //    AcceptAddress = ent.AcceptAddress,
                        //    AcceptCellphone = ent.AcceptCellphone,
                        //    AcceptTelephone = ent.AcceptTelephone,
                        //    AcceptPeople = ent.AcceptPeople,
                        //    AcceptUnit = ent.AcceptUnit,
                        //    HouseName = HouseName,
                        //    OP_ID = UserInfor.LoginName,
                        //    PushType = "0",
                        //    PushStatus = "0"
                        //}, log);

                        bus.SaveHlyOrderData(outHouseList, ent);
                    }
                    
                    msg.Message = ent.OrderNo + "/" + outID;//订单号和出库单号
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
#if !DEBUG
            try
            {
                if (msg.Result)
                {
                    if (ent.HouseID.Equals(10) && ent.ThrowGood == "17")
                    {
                        int tag = 36;
                        string good = string.Empty;
                        //微信企业号推送
                        QySendInfoEntity send = new QySendInfoEntity();
                        send.title = "出库通知";
                        send.msgType = msgType.textcard;
                        send.toTag = Convert.ToString(tag);
                        send.content = "<div>出库仓库：" + ent.OutHouseName + "</div><div>订单号码：" + ent.OrderNo + " " + ent.Dest + "</div><div>订单数量：" + ent.Piece.ToString()+" 条" + "</div><div>公司名称：" + ent.AcceptUnit + "</div><div>收货人：" + ent.AcceptPeople + " " + ent.AcceptCellphone + "</div><div>收货地址：" + ent.AcceptAddress + "</div><div>业务员：" + ent.SaleManName + "</div><div>订单备注：" + ent.Remark + "</div>";
                        send.url = "http://dlt.neway5.com";
                        QyConfigEntity agentEnt = new QyConfigEntity();
                        agentEnt.AgentID = "1000022";
                        agentEnt.AgentSecret = "-ohxtwczR68LIyyo9wYILfV4UKtGcCaes3VZYqpsYyE";
                        WxQYSendHelper.HLYQYPushInfo(agentEnt, send);
                    }
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
#endif
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
            Response.End();
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        public void DelOrder()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoOrderEntity> list = new List<CargoOrderEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoOrderBus bus = new CargoOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.Status = "0";
            log.NvgPage = "订单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            int HouseID = 0;

            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoOrderEntity ord = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = Convert.ToString(row["OrderNo"]) });
                    HouseID = ord.HouseID;
                    if (HouseID != 0)
                    {
                        if (ord.CheckStatus.Equals("1"))
                        {
                            msg.Message = Convert.ToString(row["OrderNo"]) + "已结清，无法删除";
                            msg.Result = false;
                            break;
                        }
                        if (ord.CheckStatus.Equals("2"))
                        {
                            msg.Message = Convert.ToString(row["OrderNo"]) + "结算中，无法删除";
                            msg.Result = false;
                            break;
                        }
                        if (ord.AwbStatus != "0")
                        {
                            if (UserInfor.LoginName != "1000" && UserInfor.LoginName != "2076")
                            {
                                msg.Message = Convert.ToString(row["OrderNo"]) + "已出库，无法删除";
                                msg.Result = false;
                                break;
                            }
                        }
                        if (ord.HouseID == 10)
                        {
                            CargoOrderEntity hlyOrder = bus.QueryHlyOrderData(ord.OrderNo);
                            if (!string.IsNullOrEmpty(hlyOrder.AwbStatus) && hlyOrder.AwbStatus.Equals("已开单"))
                            {
                                msg.Message = Convert.ToString(row["OrderNo"]) + "仓库已确认订单无法删除";
                                msg.Result = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (row["HouseID"].ToString() != "47")
                        {
                            msg.Message = Convert.ToString(row["OrderNo"]) + "未查询到订单数据";
                            msg.Result = false;
                            break;
                        }
                    }
                    list.Add(new CargoOrderEntity
                    {
                        OrderID = Convert.ToInt64(row["OrderID"]),
                        OrderNo = Convert.ToString(row["OrderNo"]),
                        Dep = Convert.ToString(row["Dep"]),
                        Dest = Convert.ToString(row["Dest"]),
                        Piece = Convert.ToInt32(row["Piece"]),
                        TransportFee = Convert.ToDecimal(row["TransportFee"]),
                        TransitFee = Convert.ToDecimal(row["TransitFee"]),
                        DeliveryFee = Convert.ToDecimal(row["DeliveryFee"]),
                        InsuranceFee = Convert.ToDecimal(row["InsuranceFee"]),
                        OtherFee = Convert.ToDecimal(row["OtherFee"]),
                        TotalCharge = Convert.ToDecimal(row["TotalCharge"]),
                        AcceptUnit = Convert.ToString(row["AcceptUnit"]),
                        AcceptPeople = Convert.ToString(row["AcceptPeople"]),
                        AcceptTelephone = Convert.ToString(row["AcceptTelephone"]),
                        AcceptCellphone = Convert.ToString(row["AcceptCellphone"]),
                        AcceptAddress = Convert.ToString(row["AcceptAddress"]),
                        SaleManName = Convert.ToString(row["SaleManName"]),
                        CreateAwb = Convert.ToString(row["CreateAwb"]),
                        CreateAwbID = UserInfor.LoginName,
                        CreateDate = Convert.ToDateTime(row["CreateDate"]),
                        LogisAwbNo = Convert.ToString(row["LogisAwbNo"]),
                        LogisticName = Convert.ToString(row["LogisticName"]),
                        WXOrderNo = Convert.ToString(row["WXOrderNo"]),
                        OutHouseName = Convert.ToString(row["OutHouseName"]),
                        ThrowGood = Convert.ToString(row["ThrowGood"]),
                        PayClientNum = Convert.ToInt32(row["PayClientNum"]),
                        Remark = Convert.ToString(row["Remark"]),
                        HouseID = HouseID,
                        TrafficType = Convert.ToString(row["TrafficType"]),
                        DeleteID = UserInfor.LoginName,
                        PostponeShip = Convert.ToString(row["PostponeShip"]),
                        DeleteName = UserInfor.UserName
                    });
                }
                if (msg.Result)
                {
                    bus.DeleteOrderInfo(list, log);
                    bus.DelHlyOrderData(list);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
#if !DEBUG
            try
            {
                if (msg.Result)
                {
                    foreach (Hashtable row in rows)
                    {
                        if (Convert.ToString(row["HouseID"])=="10" && Convert.ToString(row["ThrowGood"]) == "17")
                        {
                            int tag = 36;
                            string good = string.Empty;
                            //微信企业号推送
                            QySendInfoEntity send = new QySendInfoEntity();
                            send.title = "订单删除通知";
                            send.msgType = msgType.textcard;
                            send.toTag = Convert.ToString(tag);
                            send.content = "<div>出库仓库：" + Convert.ToString(row["OutHouseName"]) + "</div><div>订单号码：" + Convert.ToString(row["OrderNo"]) + " " + Convert.ToString(row["Dest"]) + "</div><div>订单数量：" + Convert.ToString(row["Piece"]) + " 条" + "</div><div>公司名称：" + Convert.ToString(row["AcceptUnit"]) + "</div><div>收货人：" + Convert.ToString(row["AcceptPeople"]) + " " + Convert.ToString(row["AcceptCellphone"]) + "</div><div>收货地址：" + Convert.ToString(row["AcceptAddress"]) + "</div><div>订单已删除，请留意订单产品状态！！！</div>";
                            send.url = "http://dlt.neway5.com";
                            QyConfigEntity agentEnt = new QyConfigEntity();
                            agentEnt.AgentID = "1000022";
                            agentEnt.AgentSecret = "-ohxtwczR68LIyyo9wYILfV4UKtGcCaes3VZYqpsYyE";
                            WxQYSendHelper.HLYQYPushInfo(agentEnt, send);
                        }
                    }
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
#endif
        ERROR:
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        public void updateOrderData()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            if (GridRows.Count <= 0)
            {
                msg.Message = "没有产品数据";
                msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
                return;
            }
            CargoOrderEntity ent = new CargoOrderEntity();
            List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
            List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();
            CargoOrderBus bus = new CargoOrderBus();
            CargoHouseBus house = new CargoHouseBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.NvgPage = "修改订单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            log.Status = "0";
            string source = string.Empty;
            CargoOrderEntity hlyOrder = new CargoOrderEntity();
            CargoOrderEntity oldOrder = new CargoOrderEntity();
            CargoOrderEntity ord = new CargoOrderEntity();
            try
            {
                ent.OrderID = Convert.ToInt64(Request["OrderID"]);
                ent.OrderNo = Convert.ToString(Request["OrderNo"]);
                ent.ThrowGood = "21";
                ord = bus.QueryOrderInfoByOrderID(ent.OrderID);
                if (ord.CheckStatus.Equals("1"))
                {
                    msg.Message = ent.OrderNo + "已结清，无法修改"; msg.Result = false;
                }
                if (ord.CheckStatus.Equals("2"))
                {
                    msg.Message = ent.OrderNo + "结算中，无法修改"; msg.Result = false;
                }

                if (msg.Result)
                {
                    #region 赋值
                    ent.HAwbNo = Convert.ToString(Request["HAwbNo"]);
                    ent.Dep = UserInfor.DepCity;
                    ent.Dest = Convert.ToString(Request["AcceptCity"]);
                    ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);
                    ent.Weight = string.IsNullOrEmpty(Convert.ToString(Request["Weight"])) ? 0 : Convert.ToDecimal(Request["Weight"]);
                    ent.Volume = string.IsNullOrEmpty(Convert.ToString(Request["Volume"])) ? 0 : Convert.ToDecimal(Request["Volume"]);
                    ent.InsuranceFee = string.IsNullOrEmpty(Convert.ToString(Request["AInsuranceFee"])) ? 0 : Convert.ToDecimal(Request["AInsuranceFee"]);
                    ent.TransitFee = string.IsNullOrEmpty(Convert.ToString(Request["TransitFee"])) ? 0 : Convert.ToDecimal(Request["TransitFee"]);
                    ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                    ent.DeliveryFee = string.IsNullOrEmpty(Convert.ToString(Request["DeliveryFee"])) ? 0 : Convert.ToDecimal(Request["DeliveryFee"]);
                    ent.OtherFee = string.IsNullOrEmpty(Convert.ToString(Request["OtherFee"])) ? 0 : Convert.ToDecimal(Request["OtherFee"]);
                    ent.TotalCharge = string.IsNullOrEmpty(Convert.ToString(Request["TotalCharge"])) ? 0 : Convert.ToDecimal(Request["TotalCharge"]);
                    ent.Rebate = string.IsNullOrEmpty(Convert.ToString(Request["Rebate"])) ? 0 : Convert.ToDecimal(Request["Rebate"]);

                    ent.CheckOutType = Convert.ToString(Request["CheckOutType"]);
                    //ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(Request["ReturnAwb"])) ? 0 : Convert.ToInt32(Request["ReturnAwb"]);
                    ent.TrafficType = Convert.ToString(Request["TrafficType"]);
                    ent.DeliveryType = "0";//Convert.ToString(Request["DeliveryType"]);
                    ent.AcceptUnit = Convert.ToString(Request["AcceptUnit"]);
                    ent.AcceptAddress = Convert.ToString(Request["AcceptAddress"]);
                    ent.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
                    ent.AcceptPeople = ent.AcceptPeople.Split(',')[0];
                    ent.AcceptTelephone = Convert.ToString(Request["AcceptTelephone"]);
                    ent.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
                    ent.CreateAwb = Convert.ToString(Request["CreateAwb"]);
                    ent.CreateDate = Convert.ToDateTime(Request["CreateDate"]);
                    ent.OP_ID = UserInfor.LoginName.Trim();
                    ent.SaleManID = Convert.ToString(Request["SaleManID"]);
                    ent.SaleManName = Convert.ToString(Request["SaleManName"]);
                    ent.SaleCellPhone = Convert.ToString(Request["SaleCellPhone"]);
                    ent.Remark = Convert.ToString(Request["Remark"]);
                    ent.LogisID = Convert.ToInt32(Request["LogisID"]);
                    ent.LogisAwbNo = Convert.ToString(Request["LogisAwbNo"]);
                    ent.ClientNum = Convert.ToInt32(UserInfor.LoginName);
                    ent.IsPrintPrice = string.IsNullOrEmpty(Convert.ToString(Request["IsPrintPrice"])) ? 0 : 1;
                    //ent.TranHouse = Convert.ToString(Request["TranHouse"]);
                    ent.TranHouse = ent.ThrowGood.Equals("0") ? "" : Convert.ToString(Request["TranHouse"]);

                    #endregion
                    decimal trFee = 0.00M;
                    int pieceN = 0, pieceTotal = 0;
                    string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//出库单号
                    //新的订单产品数据
                    foreach (Hashtable row in GridRows)
                    {
                        source = Convert.ToString(row["Source"]);
                        if (Convert.ToInt32(row["InPiece"]).Equals(0) && Convert.ToInt32(row["Piece"]).Equals(0))
                        {
                            continue;
                        }
                        entDest.Add(new CargoOrderGoodsEntity
                        {
                            OrderNo = ent.OrderNo,
                            ProductID = Convert.ToInt64(row["ProductID"]),
                            HouseID = Convert.ToInt32(row["HouseID"]),
                            AreaID = Convert.ToInt32(row["AreaID"]),
                            Piece = Convert.ToInt32(row["InPiece"]) == 0 ? Convert.ToInt32(row["Piece"]) : Convert.ToInt32(row["InPiece"]) - Convert.ToInt32(row["Piece"]),
                            ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]),
                            ContainerCode = Convert.ToString(row["ContainerCode"]),
                            OutCargoID = string.IsNullOrEmpty(Convert.ToString(row["OutCargoID"])) ? outID : Convert.ToString(row["OutCargoID"]),
                            ID = Convert.ToInt64(row["ID"]),
                            RuleType = Convert.ToString(row["RuleType"]).Trim(),
                            RuleID = Convert.ToString(row["RuleID"]).Trim(),
                            RuleTitle = Convert.ToString(row["RuleTitle"]).Trim(),
                            OP_ID = UserInfor.LoginName.Trim()
                        });

                        pieceN = Convert.ToInt32(row["InPiece"]) == 0 ? Convert.ToInt32(row["Piece"]) : Convert.ToInt32(row["InPiece"]) - Convert.ToInt32(row["Piece"]);
                        pieceTotal += pieceN;
                        trFee += pieceN * Convert.ToDecimal(row["ActSalePrice"]);
                        CargoContainerShowEntity cargo = new CargoContainerShowEntity();

                        cargo.OutCargoID = string.IsNullOrEmpty(Convert.ToString(row["OutCargoID"])) ? outID : Convert.ToString(row["OutCargoID"]);
                        cargo.ContainerID = Convert.ToInt32(row["ContainerID"]);
                        cargo.TypeID = Convert.ToInt32(row["TypeID"]);
                        cargo.ProductID = Convert.ToInt64(row["ProductID"]);
                        cargo.Piece = Convert.ToInt32(row["InPiece"]) == 0 ? Convert.ToInt32(row["Piece"]) : Convert.ToInt32(row["InPiece"]) - Convert.ToInt32(row["Piece"]);//出库件数
                        cargo.ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]);
                        cargo.InPiece = Convert.ToInt32(row["InPiece"]);
                        cargo.ID = Convert.ToInt64(row["ID"]);

                        cargo.TypeName = Convert.ToString(row["TypeName"]).Trim();
                        cargo.HouseName = Convert.ToString(row["HouseName"]).Trim();
                        cargo.AreaName = Convert.ToString(row["AreaName"]).Trim();
                        cargo.ProductName = Convert.ToString(row["ProductName"]).Trim();
                        cargo.Model = Convert.ToString(row["Model"]).Trim();
                        cargo.Specs = Convert.ToString(row["Specs"]).Trim();
                        cargo.Figure = Convert.ToString(row["Figure"]).Trim();
                        cargo.Batch = Convert.ToString(row["Batch"]).Trim();
                        cargo.GoodsCode = Convert.ToString(row["GoodsCode"]).Trim();
                        cargo.OrderNo = ent.OrderNo;
                        outHouseList.Add(cargo);
                    }
                    ent.TransportFee = trFee;
                    ent.TotalCharge = ent.TransportFee + ent.TransitFee + ent.OtherFee - ent.InsuranceFee;

                    ent.Piece = pieceTotal;
                    ent.HouseID = entDest[0].HouseID;
                    ent.goodsList = entDest;

                    ent.PayClientNum = Convert.ToInt32(UserInfor.LoginName);
                    ent.PayClientName = UserInfor.UserName;

                    if (msg.Result)
                    {
                        ////仓库同步缓存
                        //foreach (CargoContainerShowEntity time in outHouseList)
                        //{
                        //    CargoProductEntity syncProduct = house.SyncTypeProduct(time.ProductID.ToString());
                        //    //34 马牌  1 同步马牌  2 同步全部品牌
                        //    if (syncProduct.SyncType == "2" || (syncProduct.SyncType == "1" && syncProduct.TypeID == 34))
                        //    {
                        //        RedisHelper.HashSet("OpenSystemStockSyc", syncProduct.HouseID + "_" + syncProduct.TypeID + "_" + syncProduct.ProductCode, syncProduct.GoodsCode);
                        //    }
                        //}
                        bus.UpdateOrderInfo(ent, outHouseList, log);
                        msg.Result = true;
                        msg.Message = ent.OrderNo + "/" + outID;//订单号和出库单号
                    }
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }

        ERROR:
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
            Response.End();
        }

        /// <summary>
        /// 拉上订单
        /// </summary>
        public void DrawUpOrder()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            List<CargoContainerShowEntity> entityList = new List<CargoContainerShowEntity>();
            msg.Result = true;

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.Status = "0";
            log.NvgPage = "订单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            try
            {
                CargoContainerShowEntity entity = new CargoContainerShowEntity();

                foreach (Hashtable row in rows)
                {
                    entity.ID = Convert.ToInt64(row["ID"]);
                    entity.ProductID = Convert.ToInt64(row["ProductID"]);
                    entity.ContainerID = Convert.ToInt32(row["ContainerID"]);
                    entity.ContainerCode = Convert.ToString(row["ContainerCode"]);
                    entity.AreaID = Convert.ToInt32(row["AreaID"]);
                    entity.TypeID = Convert.ToInt32(row["TypeID"]);
                    entity.Piece = Convert.ToInt32(row["Piece"]);//新数量
                    entity.InPiece = Convert.ToInt32(row["InPiece"]);//旧数量
                    entity.HouseID = Convert.ToInt32(row["HouseID"]);
                    entity.HouseName = Convert.ToString(row["HouseName"]);
                    entity.OrderNo = Convert.ToString(row["OrderNo"]);
                    entity.RuleType = Convert.ToString(row["RuleType"]).Trim();
                    entity.RuleID = Convert.ToString(row["RuleID"]).Trim();
                    entity.RuleTitle = Convert.ToString(row["RuleTitle"]).Trim();
                    entity.SuitClientNum = Convert.ToString(row["SuitClientNum"]).Trim();

                    entity.Specs = Convert.ToString(row["Specs"]);
                    entity.Model = Convert.ToString(row["Model"]);
                    entity.GoodsCode = Convert.ToString(row["GoodsCode"]);
                    entity.Figure = Convert.ToString(row["Figure"]);
                    entity.OutCargoID = Convert.ToString(row["OutCargoID"]);
                    entity.ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]);

                    CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
                    queryEntity.Specs = entity.Specs;
                    queryEntity.Figure = entity.Figure;
                    queryEntity.SpeedLevel = entity.SpeedLevel;
                    queryEntity.LoadIndex = entity.LoadIndex;
                    queryEntity.TradePrice = entity.TradePrice;
                    queryEntity.HouseID = entity.HouseID;
                    queryEntity.GoodsCode = entity.GoodsCode;
                    queryEntity.TypeID = entity.TypeID;

                    CargoHouseBus house = new CargoHouseBus();
                    List<CargoContainerShowEntity> list = house.QueryALLHouseData(queryEntity);
                    int sumPiece = list.Sum(x => x.Piece);
                    int diffPiece = entity.Piece - entity.OldPiece;
                    if (sumPiece < diffPiece)
                    {
                        msg.Result = false;
                        msg.Message = entity.Specs + "在库库存不足";
                        if (row["HouseID"].ToString() == "47")
                        {
                            msg.Message = row["ProductName"].ToString() + "在库库存不足";
                        }
                        break;
                    }
                    int piece = diffPiece;
                    foreach (var item in list)
                    {
                        if (piece == 0) { break; }
                        CargoContainerShowEntity ent = new CargoContainerShowEntity();
                        ent.Specs = entity.Specs;
                        ent.Figure = entity.Figure;
                        ent.SpeedLevel = entity.SpeedLevel;
                        ent.LoadIndex = entity.LoadIndex;
                        ent.TradePrice = entity.TradePrice;
                        ent.OldPiece = entity.OldPiece;
                        ent.PayClientNum = Convert.ToInt32(UserInfor.LoginName.Trim());
                        ent.OrderNo = entity.OrderNo;
                        ent.OutCargoID = entity.OutCargoID;
                        ent.HouseID = entity.HouseID;
                        ent.ActSalePrice = entity.ActSalePrice;

                        if (item.Piece < piece)
                        {
                            ent.ProductID = item.ProductID;
                            ent.TypeID = item.TypeID;
                            ent.Piece = item.Piece;
                            ent.ContainerCode = item.ContainerCode;
                            ent.ContainerID = item.ContainerID;
                            ent.AreaID = item.AreaID;
                            entityList.Add(ent);
                            piece = piece - item.Piece;
                            continue;
                        }
                        if (item.Piece == piece)
                        {
                            ent.ProductID = item.ProductID;
                            ent.TypeID = item.TypeID;
                            ent.Piece = item.Piece;
                            ent.ContainerCode = item.ContainerCode;
                            ent.ContainerID = item.ContainerID;
                            ent.AreaID = item.AreaID;
                            entityList.Add(ent);
                            piece = piece - item.Piece;
                            continue;
                        }
                        if (item.Piece > piece)
                        {
                            ent.ProductID = item.ProductID;
                            ent.TypeID = item.TypeID;
                            ent.Piece = piece;
                            ent.ContainerCode = item.ContainerCode;
                            ent.ContainerID = item.ContainerID;
                            ent.AreaID = item.AreaID;
                            entityList.Add(ent);
                            piece = piece - piece;
                            continue;
                        }
                    }
                }
                if (msg.Result)
                {
                    CargoOrderBus bus = new CargoOrderBus();
                    bus.DrawUpOrder(entityList, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 查询所有一级分类
        /// </summary>
        public void QueryALLOneProductType()
        {
            //查询条件
            int PID = string.IsNullOrEmpty(Request["PID"]) ? 0 : Convert.ToInt32(Request["PID"]);
            CargoProductBus bus = new CargoProductBus();
            List<CargoProductTypeEntity> list = bus.QueryProductType(new CargoProductTypeEntity { ParentID = PID });
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询所有在库的产品数据
        /// </summary>
        public void QueryALLHouseData()
        {
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            CargoHouseBus bus = new CargoHouseBus();
            string spe = Convert.ToString(Request["Specs"]).ToUpper();
            if (spe.Contains("/") || spe.Contains("R") || spe.Contains("."))
            {
                queryEntity.Specs = spe;
            }
            else
            {
                if (spe.Length <= 3)
                {
                    queryEntity.Specs = spe;
                }
                if (spe.Length > 3 && spe.Length <= 5)
                {
                    queryEntity.Specs = spe.Substring(0, 3) + "/" + spe.Substring(3, spe.Length - 3);
                }
                if (spe.Length > 5)
                {
                    queryEntity.Specs = spe.Substring(0, 3) + "/" + spe.Substring(3, 2) + "R" + spe.Substring(5, spe.Length - 5);
                }
            }

            queryEntity.ProductName = Convert.ToString(Request["ProductName"]);
            queryEntity.Model = Convert.ToString(Request["Model"]);
            queryEntity.GoodsCode = Convert.ToString(Request["GoodsCode"]);
            if (Request["Born"] != "-1")
            {
                queryEntity.Born = Convert.ToString(Request["Born"]);
            }
            queryEntity.Figure = Convert.ToString(Request["Figure"]);//花纹
            queryEntity.LoadIndex = Convert.ToString(Request["LoadIndex"]);
            if (!string.IsNullOrEmpty(Request["HouseID"]))//一级分类
            {
                queryEntity.CargoPermisID = Convert.ToString(Request["HouseID"]);//所属仓库ID
            }
            queryEntity.IsLockStock = "0";
            queryEntity.CargoPermisID = UserInfor.HouseID.ToString();
            queryEntity.TypeID = 258;
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            List<CargoContainerShowEntity> list = bus.QueryALLHouseData(queryEntity);
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            while (list.Count > 0)
            {
                CargoContainerShowEntity m = list[0];
                list.RemoveAt(0);
                if (m.Piece > 0)
                {
                    List<CargoContainerShowEntity> list2 = new List<CargoContainerShowEntity>();
                    foreach (CargoContainerShowEntity item in list)
                    {
                        if (item.Specs.Equals(m.Specs) && item.Figure.Equals(m.Figure) && item.SpeedLevel.Equals(m.SpeedLevel) && item.LoadIndex.Equals(m.LoadIndex) && item.BatchYear.Equals(m.BatchYear) && item.GoodsCode.Equals(m.GoodsCode))
                        {
                            if (item.Piece > 0)
                            {
                                m.Piece += item.Piece;
                                m.InPiece += item.InPiece;
                            }
                        }
                        else
                        {
                            list2.Add(item);
                        }
                    }

                    list = list2;
                    result.Add(m);
                }
            }
            list = result;
            list.OrderBy(x => x.Specs);

            List<CargoContainerShowEntity> footlist = new List<CargoContainerShowEntity>();
            footlist.Add(new CargoContainerShowEntity
            {
                HouseName = "汇总：",
                Piece = list.Sum(c => c.Piece),
                InCargoStatus = ""
            });
            Hashtable resHT = new Hashtable();
            resHT["rows"] = list;
            resHT["total"] = list.Count();
            resHT["footer"] = footlist;
            //JSON
            String json = JSON.Encode(resHT);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询城市 
        /// </summary>
        public void QueryCityData()
        {
            CargoHouseBus bus = new CargoHouseBus();
            int PID = string.IsNullOrEmpty(Request["PID"]) ? 0 : Convert.ToInt32(Request["PID"]);
            List<CargoCityEntity> list = bus.QueryCityData(new CargoCityEntity { ParentID = PID });
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询客户的所有收货地址
        /// </summary>
        public void QueryAcceptAddress()
        {
            CargoClientAcceptAddressEntity queryEntity = new CargoClientAcceptAddressEntity();
            queryEntity.ClientNum = Convert.ToInt32(UserInfor.LoginName);

            queryEntity.AcceptCompany = Convert.ToString(Request["AcceptCompany"]);
            queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
            queryEntity.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoClientBus bus = new CargoClientBus();
            Hashtable list = bus.QueryAcceptAddress(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 修改订单中的产品数量
        /// </summary>
        public void UpdateOrderPiece()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.Status = "0";
            log.NvgPage = "订单管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "U";
            try
            {
                CargoContainerShowEntity entity = new CargoContainerShowEntity();
                CargoHouseBus house = new CargoHouseBus();
                CargoOrderBus bus = new CargoOrderBus();
                List<CargoContainerShowEntity> entityList = new List<CargoContainerShowEntity>();
                decimal LimitMoney = 0;

                foreach (Hashtable row in rows)
                {
                    CargoOrderEntity ord = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = Convert.ToString(row["OrderNo"]) });
                    if (!ord.AwbStatus.Equals("0"))
                    {
                        msg.Message = "订单非已开单状态，不允许修改";
                        msg.Result = false;
                        break;
                    }
                    if (ord.CheckStatus.Equals("1"))
                    {
                        msg.Message = Convert.ToString(row["OrderNo"]) + "已结清，不允许修改";
                        msg.Result = false;
                        break;
                    }
                    if (ord.CheckStatus.Equals("2"))
                    {
                        msg.Message = Convert.ToString(row["OrderNo"]) + "结算中，不允许修改";
                        msg.Result = false;
                        break;
                    }
                    entity.Specs = Convert.ToString(row["Specs"]);
                    entity.Figure = Convert.ToString(row["Figure"]);
                    entity.SpeedLevel = Convert.ToString(row["SpeedLevel"]);
                    entity.LoadIndex = Convert.ToString(row["LoadIndex"]);
                    entity.TradePrice = Convert.ToDecimal(row["TradePrice"]);
                    entity.GoodsCode = Convert.ToString(row["GoodsCode"]);

                    entity.Piece = Convert.ToInt32(row["Piece"]);//新数量
                    entity.OldPiece = Convert.ToInt32(row["oldPiece"]);//旧数量

                    entity.PayClientNum = ord.PayClientNum;
                    entity.OrderNo = Convert.ToString(row["OrderNo"]);
                    entity.OutCargoID = Convert.ToString(row["OutCargoID"]);
                    entity.HouseID = Convert.ToInt32(row["HouseID"]);
                    entity.TypeID = Convert.ToInt32(row["TypeID"]);
                    entity.ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]);

                    if (entity.Piece.Equals(entity.OldPiece))
                    {
                        msg.Result = false;
                        msg.Message = "未修改数量";
                        break;
                    }
                    //拉上的
                    if (entity.Piece > entity.OldPiece)
                    {
                        int diffPiece = entity.Piece - entity.OldPiece;

                        CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
                        queryEntity.Specs = entity.Specs;
                        queryEntity.Figure = entity.Figure;
                        queryEntity.SpeedLevel = entity.SpeedLevel;
                        queryEntity.LoadIndex = entity.LoadIndex;
                        queryEntity.TradePrice = entity.TradePrice;
                        queryEntity.GoodsCode = entity.GoodsCode;
                        queryEntity.HouseID = entity.HouseID;
                        queryEntity.TypeID = entity.TypeID;

                        List<CargoContainerShowEntity> list = house.QueryALLHouseData(queryEntity);
                        int sumPiece = list.Sum(x => x.Piece);
                        if (sumPiece < diffPiece)
                        {
                            msg.Result = false;
                            msg.Message = entity.Specs + "在库库存不足";
                            if (row["HouseID"].ToString() == "47")
                            {
                                msg.Message = row["ProductName"].ToString() + "在库库存不足";
                            }
                            break;
                        }
                        int piece = diffPiece;
                        foreach (var item in list)
                        {
                            if (piece == 0) { break; }
                            CargoContainerShowEntity ent = new CargoContainerShowEntity();
                            ent.Specs = entity.Specs;
                            ent.Figure = entity.Figure;
                            ent.SpeedLevel = entity.SpeedLevel;
                            ent.LoadIndex = entity.LoadIndex;
                            ent.GoodsCode = entity.GoodsCode;
                            ent.TradePrice = entity.TradePrice;
                            ent.OldPiece = entity.OldPiece;
                            ent.PayClientNum = entity.PayClientNum;
                            ent.OrderNo = entity.OrderNo;
                            ent.OutCargoID = entity.OutCargoID;
                            ent.HouseID = entity.HouseID;
                            ent.ActSalePrice = entity.ActSalePrice;

                            if (item.Piece < piece)
                            {
                                ent.ProductID = item.ProductID;
                                ent.TypeID = item.TypeID;
                                ent.Piece = item.Piece;
                                ent.ContainerCode = item.ContainerCode;
                                ent.ContainerID = item.ContainerID;
                                ent.AreaID = item.AreaID;
                                entityList.Add(ent);
                                piece = piece - item.Piece;
                                continue;
                            }
                            if (item.Piece == piece)
                            {
                                ent.ProductID = item.ProductID;
                                ent.TypeID = item.TypeID;
                                ent.Piece = item.Piece;
                                ent.ContainerCode = item.ContainerCode;
                                ent.ContainerID = item.ContainerID;
                                ent.AreaID = item.AreaID;
                                entityList.Add(ent);
                                piece = piece - item.Piece;
                                continue;
                            }
                            if (item.Piece > piece)
                            {
                                ent.ProductID = item.ProductID;
                                ent.TypeID = item.TypeID;
                                ent.Piece = piece;
                                ent.ContainerCode = item.ContainerCode;
                                ent.ContainerID = item.ContainerID;
                                ent.AreaID = item.AreaID;
                                entityList.Add(ent);
                                piece = piece - piece;
                                continue;
                            }
                        }
                    }
                    else
                    {
                        entity.Piece = entity.Piece - entity.OldPiece;
                        entityList.Add(entity);
                    }
                }
                if (msg.Result)
                {
                    bus.UpdateOrderPiece(entityList, LimitMoney, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 通过订单号查询订单数据
        /// </summary>
        public void QueryOrderByOrderNo()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            //查询条件
            string key = Request["OrderNo"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            queryEntity.OrderNo = key.Trim();
            CargoOrderBus bus = new CargoOrderBus();
            List<CargoContainerShowEntity> list = bus.QueryOrderByOrderNo(queryEntity);
            List<CargoContainerShowEntity> result = new List<CargoContainerShowEntity>();
            while (list.Count > 0)
            {
                CargoContainerShowEntity m = list[0];
                list.RemoveAt(0);

                List<CargoContainerShowEntity> list2 = new List<CargoContainerShowEntity>();
                foreach (CargoContainerShowEntity item in list)
                {
                    if (item.Specs.Equals(m.Specs) && item.Figure.Equals(m.Figure) && item.SpeedLevel.Equals(m.SpeedLevel) && item.LoadIndex.Equals(m.LoadIndex) && item.BatchYear.Equals(m.BatchYear) && item.GoodsCode.Equals(m.GoodsCode))
                    {
                        m.Piece += item.Piece;
                        m.InPiece += item.InPiece;
                    }
                    else
                    {
                        list2.Add(item);
                    }
                }
                list = list2;
                result.Add(m);
            }
            list = result;
            if (list != null) { QueryOrderGoodsList = list; }
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();

        }
        public List<CargoContainerShowEntity> QueryOrderGoodsList
        {
            get
            {
                if (Session["QueryOrderGoodsList"] == null)
                {
                    Session["QueryOrderGoodsList"] = new List<CargoContainerShowEntity>();
                }
                return (List<CargoContainerShowEntity>)(Session["QueryOrderGoodsList"]);
            }
            set
            {
                Session["QueryOrderGoodsList"] = value;
            }
        }
        /// <summary>
        /// 查询客户所有收货地址
        /// </summary>
        public void AutoCompleteClientAcceptPeople()
        {
            CargoClientAcceptAddressEntity queryEntity = new CargoClientAcceptAddressEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ClientNum"])))
            {
                queryEntity.ClientNum = Convert.ToInt32(Request["ClientNum"]);
            }
            else
            {
                return;
            }
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientAcceptAddressEntity> list = new List<CargoClientAcceptAddressEntity>();
            list = bus.QueryAcceptAddress(queryEntity);

            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 保存客户收货地址
        /// </summary>
        public void SaveAcceptAddress()
        {
            CargoClientAcceptAddressEntity ent = new CargoClientAcceptAddressEntity();
            CargoClientBus bus = new CargoClientBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.NvgPage = "收货地址管理";
            log.Status = "0";
            log.UserID = UserInfor.LoginName.Trim();
            String id = Request["ADID"] != null ? Request["ADID"].ToString() : "";
            try
            {
                CargoClientEntity clientEntity = bus.QueryCargoClient(Convert.ToInt32(UserName));
                ent.ClientID = clientEntity.ClientID;
                ent.ClientNum = Convert.ToInt32(UserInfor.LoginName);
                ent.AcceptCompany = Convert.ToString(Request["AcceptCompany"]).Trim();
                ent.AcceptPeople = Convert.ToString(Request["AcceptPeople"]).Trim();
                ent.AcceptProvince = Convert.ToString(Request["AProvince"]);
                ent.AcceptCity = Convert.ToString(Request["ACity"]);
                ent.AcceptCountry = Convert.ToString(Request["ACountry"]);
                ent.AcceptAddress = Convert.ToString(Request["AcceptAddress"]).Trim();
                ent.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
                ent.AcceptTelephone = Convert.ToString(Request["AcceptTelephone"]).Trim();
                ent.HouseID = UserInfor.HouseID;

                ent.TargetNum = string.IsNullOrEmpty(Convert.ToString(Request["TargetNum"])) ? 0 : Convert.ToInt32(Request["TargetNum"]);
                if (id == "")
                {
                    log.Operate = "A";
                    bus.AddAcceptAddress(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
                else
                {
                    ent.ADID = Convert.ToInt64(id);
                    log.Operate = "U";
                    bus.UpdateAcceptAddress(ent, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 删除客户收货地址
        /// </summary>
        public void DelAcceptAddress()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoClientAcceptAddressEntity> list = new List<CargoClientAcceptAddressEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = false;
            CargoClientBus bus = new CargoClientBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "客户管理";
            log.Status = "0";
            log.NvgPage = "收货地址管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    list.Add(new CargoClientAcceptAddressEntity
                    {
                        ADID = Convert.ToInt64(row["ADID"]),
                        ClientID = Convert.ToInt64(row["ClientID"]),
                        ClientNum = Convert.ToInt32(UserInfor.LoginName),
                        AcceptPeople = Convert.ToString(row["AcceptPeople"]),
                        AcceptAddress = Convert.ToString(row["AcceptAddress"]),
                        AcceptCellphone = Convert.ToString(row["AcceptCellphone"])
                    });
                }
                bus.DelAcceptAddress(list, log);
                msg.Result = true;
                msg.Message = "成功";
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }

        public void QueryShortageListByClientNum()
        {
            ShortageListEntity queryEntity = new ShortageListEntity();
            queryEntity.ClientNum = UserInfor.LoginName;
            CargoOrderBus bus = new CargoOrderBus();
            List<ShortageListEntity> list = bus.QueryShortageListByClientNum(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();

        }

        public void QueryALLProductSpecPieceData()
        {

            CargoProductEntity queryEntity = new CargoProductEntity();
            CargoHouseBus bus = new CargoHouseBus();
            string spe = Convert.ToString(Request["Specs"]).ToUpper();
            if (spe.Contains("/") || spe.Contains("R") || spe.Contains("."))
            {
                queryEntity.Specs = spe;
            }
            else
            {
                if (spe.Length <= 3)
                {
                    queryEntity.Specs = spe;
                }
                if (spe.Length > 3 && spe.Length <= 5)
                {
                    queryEntity.Specs = spe.Substring(0, 3) + "/" + spe.Substring(3, spe.Length - 3);
                }
                if (spe.Length > 5)
                {
                    queryEntity.Specs = spe.Substring(0, 3) + "/" + spe.Substring(3, 2) + "R" + spe.Substring(5, spe.Length - 5);
                }
            }
            queryEntity.Figure = Convert.ToString(Request["Figure"]);//花纹
            queryEntity.Model = Convert.ToString(Request["Model"]);
            queryEntity.GoodsCode = Convert.ToString(Request["GoodsCode"]);
            if (Request["Born"] != "-1")
            {
                queryEntity.Born = Convert.ToString(Request["Born"]);
            }
            queryEntity.HouseID = UserInfor.HouseID;
            queryEntity.TypeID = 163;
            List<CargoProductEntity> list = bus.QueryALLProductSpecPieceData(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void InsertShortageList()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.Status = "0";
            log.NvgPage = "缺货订单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            try
            {
                ShortageListEntity entity = new ShortageListEntity();
                foreach (Hashtable row in rows)
                {
                    entity.HouseID = UserInfor.HouseID;
                    entity.ClientNum = UserInfor.LoginName;
                    entity.TypeID = Convert.ToInt32(row["TypeID"]);
                    entity.TypeName = Convert.ToString(row["TypeName"]);
                    entity.Specs = Convert.ToString(row["Specs"]);
                    entity.Figure = Convert.ToString(row["Figure"]);
                    entity.Model = Convert.ToString(row["Model"]);
                    entity.GoodsCode = Convert.ToString(row["GoodsCode"]);
                    entity.LoadIndex = Convert.ToString(row["LoadIndex"]);
                    entity.SpeedLevel = Convert.ToString(row["SpeedLevel"]);
                    entity.Piece = Convert.ToInt32(row["Piece"]);
                    entity.OP_ID = UserInfor.LoginName;
                }
                if (msg.Result)
                {
                    CargoOrderBus bus = new CargoOrderBus();
                    bus.InsertShortageList(entity, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        public void DeleteShortageList()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;

            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.Status = "0";
            log.NvgPage = "缺货订单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                ShortageListEntity entity = new ShortageListEntity();
                foreach (Hashtable row in rows)
                {
                    entity.HouseID = UserInfor.HouseID;
                    entity.ClientNum = UserInfor.LoginName;
                    entity.TypeID = Convert.ToInt32(row["TypeID"]);
                    entity.TypeName = Convert.ToString(row["TypeName"]);
                    entity.Specs = Convert.ToString(row["Specs"]);
                    entity.Figure = Convert.ToString(row["Figure"]);
                    entity.Model = Convert.ToString(row["Model"]);
                    entity.GoodsCode = Convert.ToString(row["GoodsCode"]);
                    entity.LoadIndex = Convert.ToString(row["LoadIndex"]);
                    entity.SpeedLevel = Convert.ToString(row["SpeedLevel"]);
                    entity.Piece = Convert.ToInt32(row["Piece"]);
                    entity.Born = Convert.ToString(row["Born"]);
                    entity.OP_ID = UserInfor.LoginName;
                }
                if (msg.Result)
                {
                    CargoOrderBus bus = new CargoOrderBus();
                    bus.DeleteShortageList(entity, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        public void QueryCargoNotice()
        {
            CargoNoticeEntity queryEntity = new CargoNoticeEntity();
            queryEntity.Title = Request["Title"];
            if (Request["DelFlag"] != "-1")
            {
                queryEntity.DelFlag = Convert.ToString(Request["DelFlag"]);
            }
            if (Request["NoticeType"] != "-1")
            {
                queryEntity.NoticeType = Convert.ToString(Request["NoticeType"]);
            }
            if (!string.IsNullOrEmpty(Request["HID"]))
            {
                queryEntity.HouseID = Convert.ToInt32(Request["HID"]);
            }
            queryEntity.Title = Convert.ToString(Request["Title"]);
            queryEntity.ClientNum = Convert.ToInt32(UserInfor.LoginName);

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoStaticBus bus = new CargoStaticBus();
            Hashtable list = bus.QueryCargoNotice(pageIndex, pageSize, queryEntity);

            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void QueryNoticeByID()
        {
            CargoNoticeEntity queryEntity = new CargoNoticeEntity();
            //查询条件
            string key = Request["ID"];
            if (string.IsNullOrEmpty(key)) { Response.Clear(); Response.Write(JSON.Encode(queryEntity)); return; }
            queryEntity.ID = Convert.ToInt32(key);
            CargoStaticBus bus = new CargoStaticBus();
            CargoNoticeEntity list = bus.QueryNoticeByID(queryEntity);
            bus.UpdateNoticeReadStatus(new CargoNoticeEntity { ReadStatus = 1, ID = queryEntity.ID });
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询订单数据用以退货单
        /// </summary>
        public void QueryOrderInfoForReturn()
        {
            CargoOrderReturnOrderEntity queryEntity = new CargoOrderReturnOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["Piece"]))) { queryEntity.Piece = Convert.ToInt32(Request["Piece"]); }
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
            queryEntity.Dep = Convert.ToString(Request["Dep"]);
            queryEntity.Dest = Convert.ToString(Request["Dest"]);
            if (Convert.ToString(Request["OrderType"]) != "-1")
            {
                queryEntity.OrderType = Convert.ToString(Request["OrderType"]);
            }
            queryEntity.CargoPermisID = UserInfor.HouseID.ToString();
            queryEntity.ClientNum = Convert.ToInt32(UserInfor.LoginName);
            queryEntity.SaleManID = Convert.ToString(Request["SaleManID"]);
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderModel"])))
            {
                queryEntity.OrderModel = Convert.ToString(Request["OrderModel"]);
            }
            if (Request["BelongDepart"] != "-1") { queryEntity.BelongDepart = Convert.ToString(Request["BelongDepart"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["Specs"])))
            {
                string spe = Convert.ToString(Request["Specs"]);

                if (spe.Contains("/") || spe.Contains("R") || spe.Contains("."))
                {
                    queryEntity.Specs = spe;
                }
                else
                {
                    if (spe.Length <= 3)
                    {
                        queryEntity.Specs = spe;
                    }
                    if (spe.Length > 3 && spe.Length <= 5)
                    {
                        queryEntity.Specs = spe.Substring(0, 3) + "/" + spe.Substring(3, spe.Length - 3);
                    }
                    if (spe.Length > 5)
                    {
                        queryEntity.Specs = spe.Substring(0, 3) + "/" + spe.Substring(3, 2) + "R" + spe.Substring(5, spe.Length - 5);
                    }
                }
            }
            CargoOrderBus bus = new CargoOrderBus();
            List<CargoOrderReturnOrderEntity> list = bus.QueryOrderInfoForReturn(queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 保存退货单数据
        /// </summary>
        public void saveReturn()
        {
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            string json = Request["submitData"];
            ArrayList GridRows = (ArrayList)JSON.Decode(json);
            if (GridRows.Count <= 0)
            {
                msg.Message = "没有产品数据";
                msg.Result = false;
                //返回处理结果
                string res = JSON.Encode(msg);
                Response.Write(res);
                Response.End();
                return;
            }
            CargoOrderEntity ent = new CargoOrderEntity();
            List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
            CargoOrderBus bus = new CargoOrderBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.NvgPage = "新增退货单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            log.Status = "0";

            try
            {
                string orderNo = Convert.ToString(Request["OrderNo"]);
                CargoOrderEntity orderEnt = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = orderNo });
                #region 赋值
                int OrderNum = 0;
                ent.OrderNo = Common.GetMaxOrderNumByCurrentDate(Convert.ToInt32(Request["HouseID"]), Convert.ToString(Request["HouseCode"]), out OrderNum); // Convert.ToString(Request["OrderNo"]);//生成最新顺序订单号
                //int RetryCount = 0;
                //while (OrderNoList.Contains(ent.OrderNo))
                //{
                //    RetryCount++;
                //    ent.OrderNo = Common.GetMaxOrderNumByCurrentDate(Convert.ToInt32(Request["HouseID"]), Convert.ToString(Request["HouseCode"]), out OrderNum, RetryCount);
                //}
                //OrderNoList.Add(ent.OrderNo);
                ent.OrderNum = OrderNum;//最新订单顺序号
                //ent.HAwbNo = Convert.ToString(Request["HAwbNo"]);

                ent.Dep = orderEnt.Dest.Trim();
                ent.Dest = orderEnt.Dep.Trim();

                ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);

                ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                ent.TotalCharge = ent.TransportFee;

                ent.CheckOutType = "0";//Convert.ToString(Request["CheckOutType"]);
                //ent.ReturnAwb = string.IsNullOrEmpty(Convert.ToString(Request["ReturnAwb"])) ? 0 : Convert.ToInt32(Request["ReturnAwb"]);
                ent.TrafficType = "0";// Convert.ToString(Request["TrafficType"]);
                ent.DeliveryType = "0";//Convert.ToString(Request["DeliveryType"]);
                ent.AcceptUnit = orderEnt.AcceptUnit;
                ent.AcceptAddress = orderEnt.AcceptAddress;
                ent.AcceptPeople = orderEnt.AcceptPeople;
                ent.AcceptTelephone = orderEnt.AcceptTelephone;
                ent.AcceptCellphone = orderEnt.AcceptCellphone;
                ent.ClientNum = orderEnt.ClientNum;
                ent.PayClientNum = orderEnt.PayClientNum;
                ent.PayClientName = orderEnt.PayClientName;
                ent.BelongHouse = orderEnt.BelongHouse;
                ent.OutHouseName = orderEnt.OutHouseName;
                ent.CreateAwb = UserInfor.UserName;
                ent.CreateAwbID = UserInfor.LoginName;
                ent.CreateDate = DateTime.Now;
                ent.OP_ID = UserInfor.LoginName.Trim();
                ent.SaleManID = Convert.ToString(Request["SaleManID"]);
                ent.SaleManName = Convert.ToString(Request["SaleManName"]);
                ent.Remark = Convert.ToString(Request["Remark"]);


                //ent.OutHouseName = UserInfor.HouseID.ToString();

                #endregion
                string outID = DateTime.Now.ToString("yyMMdd") + Common.GetRandomFourNumString();//入库单号
                CargoHouseBus houseBus = new CargoHouseBus();
                List<CargoProductEntity> list = houseBus.QueryALLProductData(new CargoProductEntity {TypeID= 258,CommType="1" });
                foreach (Hashtable row in GridRows)
                {
                    if (list.Where(x => x.Specs == Convert.ToString(row["Specs"]) && x.Figure == Convert.ToString(row["Figure"]) && x.GoodsCode == Convert.ToString(row["GoodsCode"]) && (string.IsNullOrWhiteSpace(x.SpeedLevel) || x.SpeedLevel == Convert.ToString(row["SpeedLevel"])) && (string.IsNullOrWhiteSpace(x.LoadIndex) || x.LoadIndex == Convert.ToString(row["LoadIndex"]))).Count() > 0)
                    {
                        msg.Message = "产品" + Convert.ToString(row["Specs"]) + " " + Convert.ToString(row["GoodsCode"]) + "为常用规格无法退货";
                        msg.Result = false;
                        //返回处理结果
                        string res = JSON.Encode(msg);
                        Response.Write(res);
                        Response.End();
                        return;
                    }
                    entDest.Add(new CargoOrderGoodsEntity
                    {
                        OrderNo = ent.OrderNo,
                        ProductID = Convert.ToInt64(row["ProductID"]),
                        HouseID = Convert.ToInt32(row["HouseID"]),
                        AreaID = Convert.ToInt32(row["AreaID"]),
                        Piece = Convert.ToInt32(row["Piece"]),
                        ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]),
                        ContainerCode = Convert.ToString(row["ContainerCode"]),
                        ContainerID = Convert.ToInt32(row["ContainerID"]),
                        TypeID = Convert.ToInt32(row["TypeID"]),
                        TypeName = Convert.ToString(row["TypeName"]),
                        OutCargoID = outID,
                        RelateOrderNo = Convert.ToString(row["OrderNo"]),
                        OP_ID = UserInfor.LoginName.Trim(),
                        Born = Convert.ToString(row["Born"]),
                        Batch = Convert.ToString(row["Batch"]),
                        Model = Convert.ToString(row["Model"]),
                        Specs = Convert.ToString(row["Specs"]),
                        LoadIndex = Convert.ToString(row["LoadIndex"]),
                        SpeedLevel = Convert.ToString(row["SpeedLevel"]),
                        Figure = Convert.ToString(row["Figure"]),
                        GoodsCode = Convert.ToString(row["GoodsCode"]),
                        UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                        SalePrice = Convert.ToDecimal(row["SalePrice"]),
                        CostPrice = Convert.ToDecimal(row["CostPrice"]),
                        TradePrice = Convert.ToDecimal(row["TradePrice"]),
                        Supplier = Convert.ToString(row["Supplier"])
                    });
                }
                ent.AwbStatus = "0";
                ent.OrderType = "0";
                ent.OrderModel = "1";//退货单
                ent.ThrowGood = "5";//退货单
                //ent.HouseID = entDest[0].HouseID;
                ent.HouseID = Convert.ToInt32(Request["HouseID"]);
                ent.goodsList = entDest;
                ent.UserInfor = UserInfor;

                if (msg.Result)
                {
                    bus.AddReturnOrderInfo(ent, log);
                    msg.Message = ent.OrderNo + "/" + outID;//订单号和重新入库单号
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
            //返回处理结果
            string ress = JSON.Encode(msg);
            Response.Write(ress);
            Response.End();
        }
        /// <summary>
        /// 删除退货单
        /// </summary>
        public void DelReturnOrder()
        {
            String json = Request["data"];
            if (String.IsNullOrEmpty(json)) return;
            List<CargoOrderEntity> list = new List<CargoOrderEntity>();
            ArrayList rows = (ArrayList)JSON.Decode(json);
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            CargoOrderBus bus = new CargoOrderBus();
            CargoHouseBus house = new CargoHouseBus();
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "订单管理";
            log.Status = "0";
            log.NvgPage = "退货管理";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "D";
            try
            {
                foreach (Hashtable row in rows)
                {
                    CargoOrderEntity ord = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = Convert.ToString(row["OrderNo"]) });
                    if (ord.FinanceSecondCheck.Equals("1"))
                    {
                        msg.Message = "财务已审核，不允许删除";
                        msg.Result = false;
                        break;
                    }
                    if (ord.CheckStatus.Equals("1"))
                    {
                        msg.Message = Convert.ToString(row["OrderNo"]) + "已结清，不允许删除";
                        msg.Result = false;
                        break;
                    }
                    if (ord.CheckStatus.Equals("2"))
                    {
                        msg.Message = Convert.ToString(row["OrderNo"]) + "结算中，不允许删除";
                        msg.Result = false;
                        break;
                    }
                    List<CargoOrderGoodsEntity> goodsList = bus.QueryOrderGoodsInfo(new CargoOrderGoodsEntity { OrderNo = Convert.ToString(row["OrderNo"]) });
                    foreach (var good in goodsList)
                    {
                        CargoContainerEntity contain = house.QueryContainerEntityByCode(good.ContainerCode, good.HouseID);
                        //根据货位ID和产品ID判断是否存在此货位
                        CargoContainerGoodsEntity cgood = house.IsExistCargoContainerGoods(new CargoContainerGoodsEntity { ContainerID = contain.ContainerID, ProductID = good.ProductID });
                        if (cgood == null || cgood.ID.Equals(0))
                        {
                            msg.Result = false;
                            msg.Message = "货位不存在";
                            break;
                        }
                    }
                    if (!msg.Result)
                    {
                        break;
                    }

                    list.Add(new CargoOrderEntity
                    {
                        OrderID = Convert.ToInt64(row["OrderID"]),
                        OrderNo = Convert.ToString(row["OrderNo"]),
                        Dep = Convert.ToString(row["Dep"]),
                        Dest = Convert.ToString(row["Dest"]),
                        Piece = Convert.ToInt32(row["Piece"]),
                        TransportFee = Convert.ToDecimal(row["TransportFee"]),
                        TotalCharge = Convert.ToDecimal(row["TotalCharge"]),
                        AcceptUnit = Convert.ToString(row["AcceptUnit"]),
                        AcceptPeople = Convert.ToString(row["AcceptPeople"]),
                        AcceptTelephone = Convert.ToString(row["AcceptTelephone"]),
                        AcceptCellphone = Convert.ToString(row["AcceptCellphone"]),
                        AcceptAddress = Convert.ToString(row["AcceptAddress"]),
                        SaleManName = Convert.ToString(row["SaleManName"]),
                        CreateAwb = Convert.ToString(row["CreateAwb"]),
                        CreateAwbID = UserInfor.LoginName,
                        HouseID = Convert.ToInt32(row["HouseID"]),
                        CreateDate = Convert.ToDateTime(row["CreateDate"])
                    });
                }
                if (msg.Result)
                {
                    bus.DelReturnOrder(list, log);
                    msg.Result = true;
                    msg.Message = "成功";
                }
            }
            catch (ApplicationException ex)
            {
                msg.Message = ex.Message;
                msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
    }
}