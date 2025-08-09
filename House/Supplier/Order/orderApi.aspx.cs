using House.Business.Cargo;
using House.Entity.Cargo;
using House.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using House.Business;
using System.Reflection;
using House.Entity.Cargo.Order;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.TenPayLibV3;
using System.Web.Script.Serialization;
using NPOI.HSSF.Record.Formula.Functions;

namespace Supplier.Order
{
    public partial class orderApi : BasePage
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
        #region 次日达订单


        public void refund()
        {
            string WXPayOrderNo = Request["WXPayOrderNo"];
            string ActRefMoney = Convert.ToString(Request["ActRefMoney"]);
            //string HouseID = Request["HouseID"].ToString();
            CargoOrderEntity entity = new CargoOrderEntity();
            if (!string.IsNullOrEmpty(WXPayOrderNo))
            {
                entity.WXPayOrderNo = WXPayOrderNo;
            }
            CargoOrderBus cargoOrderBus = new CargoOrderBus();
            CargoFinanceBus bus = new CargoFinanceBus();
            CargoOrderEntity res = bus.QueryOrder(entity);
            entity.CreateAwb = UserInfor.UserName;
            entity.CreateAwbID = UserInfor.LoginName;
            entity.WXOrderNo = res.WXOrderNo;
            //entity.HouseID = 83;
            decimal refund = 0;
            if (res.RefundType.Equals("0"))
            {
                refund = string.IsNullOrEmpty(ActRefMoney) ? res.TotalCharge * 100 : Convert.ToDecimal(ActRefMoney) * 100;
            }
            else
            {
                refund = string.IsNullOrEmpty(ActRefMoney) ? (res.TotalCharge - res.TransitFee) * 100 : Convert.ToDecimal(ActRefMoney) * 100;
            }

            //通联支付退款流程
            SybWxPayService sybService = new SybWxPayService();
            //重新微信订单信息
            CargoWeiXinBus wxbus = new CargoWeiXinBus();
            WXOrderEntity wo = wxbus.QueryWeixinOrderByOrderNo(new WXOrderEntity() { OrderNo = res.WXOrderNo });
            Dictionary<String, String> rsp = sybService.refund(Convert.ToInt64(refund), DateTime.Now.ToFileTime().ToString(), wo.Trxid, wo.OrderNo);


            string jsonString = String.Join(",", rsp.Select(kvp => kvp.Key + "=" + kvp.Value));
            Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：" + jsonString);
            //微信退款流程
            //string result = wxHttpUtility.WxHttpPost(res.WXPayOrderNo, res.WXOrderNo, refund, res.TotalCharge * 100);
            //int OrderNum = 0;
            //string OrderNo = Common.GetMaxOrderNumByCurrentDate(res.HouseID, "JC", out OrderNum);
            //entity.OrderNum = OrderNum;
            //entity.OrderNo = OrderNo;
            LogEntity logEntity = new LogEntity();
            logEntity.StartDate = DateTime.Now;
            logEntity.UserID = entity.CreateAwbID;
            logEntity.NvgPage = "用户退款";
            logEntity.Memo = jsonString;
            logEntity.Moudle = "小程序次日达";
            logEntity.Operate = "U";
            logEntity.Status = "0";
            logEntity.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            if (rsp["retcode"] == "SUCCESS")
            {
                bus.UpdatePayStatus(entity, logEntity);
                CargoOrderEntity cargoOrder = cargoOrderBus.QueryOrderInfo(new CargoOrderEntity { OrderNo = res.OrderNoStr });
                //删除订单
                cargoOrderBus.DeleteWxOrderInfo(cargoOrder, logEntity);
            }

            Response.Write(rsp["retcode"]);
            Response.End();
        }
        public void QueryWxOrder()
        {
            CargoOrderEntity entity = new CargoOrderEntity();
            string WXOrderNo = Request["OrderNo"].ToString();
            //string HouseID = Request["HouseID"].ToString();
            if (!string.IsNullOrWhiteSpace(WXOrderNo))
            {
                entity.WXOrderNo = WXOrderNo;
            }
            //if (!string.IsNullOrWhiteSpace(HouseID))
            //{
            //    entity.HouseID = Convert.ToInt32(HouseID);
            //}
            CargoFinanceBus bus = new CargoFinanceBus();
            CargoWeiXinBus weiXinBus = new CargoWeiXinBus();
            CargoOrderEntity res = bus.QueryOrder(entity);

            List<WXCouponEntity> wXCoupons = weiXinBus.QueryCouponData(new WXCouponEntity { FromOrderNO = WXOrderNo, UseStatus = "1" });

            res.InsuranceFee = wXCoupons.Sum(c => c.Money);
            ErrMessage result = new ErrMessage();
            result.Result = false;
            if (!res.PayStatus.Equals("2"))
            {
                result.Message = "未提交申请退款无法退款";
            }
            else if (!res.RefundCheckStatus.Equals("1"))
            {
                result.Message = "退款状态还未审核无法退款";
            }
            else if (res.PayStatus.Equals("2") && res.RefundCheckStatus.Equals("1"))
            {
                result.Result = true;
                result.Message = JSON.Encode(res);
            }
            String json = JSON.Encode(result);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 次日达订单发货
        /// </summary>
        public void UpdateNextDayOrdersyStatus()
        {
            string json = Request["data"];
            ArrayList rows = (ArrayList)JSON.Decode(json);
            List<CargoOrderStatusEntity> list = new List<CargoOrderStatusEntity>();
            CargoOrderBus bus = new CargoOrderBus();
            ErrMessage msg = new ErrMessage(); msg.Message = "";
            msg.Result = true;
            LogEntity log = new LogEntity()
            {
                IPAddress = Common.GetUserIP(HttpContext.Current.Request),
                Moudle = "订单管理",
                NvgPage = "次日达订单",
                Memo = "发货成功",
                UserID = UserInfor.LoginName.Trim(),
                Status = "0",
                Operate = "U",
            };
            try
            {
                foreach (Hashtable row in rows)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(row["OrderNo"])))
                    {
                        msg.Message = "订单号为空，系统错误！";
                        msg.Result = false;
                        break;
                    }
                    if (!Convert.ToString(row["PayStatus"]).Equals("1"))
                    {
                        msg.Message = "请仔细检查订单支付状态！";
                        msg.Result = false;
                        break;
                    }
                    CargoOrderEntity cargoOrderEntity = bus.QueryCargoInfoByWxOrderNo(new CargoOrderEntity { WXOrderNo = Convert.ToString(row["OrderNo"]) });
                    if (cargoOrderEntity == null)
                    {
                        msg.Message = "订单:" + cargoOrderEntity.WXOrderNo + "不存在！";
                        msg.Result = false;
                        break;
                    }
                    if (cargoOrderEntity.OrderID.Equals(0))
                    {
                        msg.Message = "订单:" + cargoOrderEntity.WXOrderNo + "不存在！";
                        msg.Result = false;
                        break;
                    }
                    CargoOrderEntity order = bus.QueryOrderInfoByOrderID(cargoOrderEntity.OrderID);
                    if (order == null)
                    {
                        msg.Message = "订单:" + order.OrderNo + "为空，系统错误！";
                        msg.Result = false;
                        break;
                    }
                    if (order.AwbStatus.Equals("5"))//已签收不允许再修改状态
                    {
                        msg.Message = "订单:" + order.OrderNo + "已签收！";
                        msg.Result = false;
                        break;
                    }
                    CargoOrderStatusEntity entity = new CargoOrderStatusEntity()
                    {
                        OrderID = order.OrderID,
                        OrderNo = order.OrderNo,
                        DetailInfo = Convert.ToString(row["DetailInfo"]),
                        OrderStatus = "2",//运单状态2已出库
                        WXOrderNo = order.WXOrderNo,
                        OP_ID = UserInfor.LoginName,
                        OP_Name = UserInfor.UserName,
                        OP_DATE = DateTime.Now,
                    };
                    if (Convert.ToString(row["OrderStatus"]).Equals("5"))
                    {
                        entity.Signer = UserInfor.UserName;
                        entity.SignTime = DateTime.Now;
                        entity.SignImage = "";
                    }
                    if (msg.Result)
                    {
                        if (!string.IsNullOrEmpty(entity.OrderNo))
                        {
                            bus.SaveOrderStatus(entity, log);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message; msg.Result = false;
            }
            //返回处理结果
            string res = JSON.Encode(msg);
            Response.Write(res);
            Response.End();
        }
        /// <summary>
        /// 查询次日达订单
        /// </summary>
        public void QueryNextDayOrdersInfo()
        {
            WXOrderEntity queryEntity = new WXOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderType = "4";//微信小程序下单
            queryEntity.ThrowGood = "23";//次日达订单
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            //queryEntity.CargoPermisID = UserInfor.SettleHouseID.ToString();
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            if (Convert.ToString(Request["AwbStatus"]) != "-1")
            {
                queryEntity.OrderStatus = Convert.ToString(Request["AwbStatus"]);//订单状态
            }
            if (Convert.ToString(Request["PayStatus"]) != "-1")
            {
                queryEntity.PayStatus = Convert.ToString(Request["PayStatus"]);//付款状态
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AcceptPeople"])))
            {
                queryEntity.Name = Convert.ToString(Request["AcceptPeople"]);//收货人
            }
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryNextDayOrderInfo(pageIndex, pageSize, queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询订单明细
        /// </summary>
        public void QueryNextDayOrderDetails()
        {
            WXOrderEntity queryEntity = new WXOrderEntity();
            CargoOrderBus bus = new CargoOrderBus();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderID"])))
            {
                queryEntity.OrderID = Convert.ToInt64(Request["OrderID"]);
            }
            List<CargoProductShelvesEntity> list = bus.QueryNextDayOrderGoodsList(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 次日达订单List
        /// </summary>
        public List<CargoOrderEntity> NextDayOrdersEntityList
        {
            get
            {
                if (Session["NextDayOrdersEntityList"] == null)
                {
                    Session["NextDayOrdersEntityList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["NextDayOrdersEntityList"]);
            }
            set
            {
                Session["NextDayOrdersEntityList"] = value;
            }
        }

        /// <summary>
        /// 导出次日达订单
        /// </summary>
        public void ExportNextDayOrders()
        {
            string err = "OK";
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            #region 查询条件
            //分页
            int pageIndex = 1;
            int pageSize = 10000;
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderType = "4";//微信小程序下单
            queryEntity.ThrowGood = "23";//次日达订单
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.SettleHouseID.ToString();
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            if (Convert.ToString(Request["AwbStatus"]) != "-1")
            {
                queryEntity.AwbStatus = Convert.ToString(Request["AwbStatus"]);//订单状态
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AcceptPeople"])))
            {
                queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);//收货人
            }
            #endregion
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryOrderInfo(pageIndex, pageSize, queryEntity);
            List<CargoOrderEntity> awbList = list["rows"] as List<CargoOrderEntity>;
            if (awbList.Count > 0) { NextDayOrdersEntityList = awbList; } else { err = "没有数据可以进行导出，请重新查询！"; }
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 次日达订单明细表List
        /// </summary>
        public List<CargoOrderGoodsEntity> NextDayOrderGoodsEntityList
        {
            get
            {
                if (Session["NextDayOrderGoodsEntityList"] == null)
                {
                    Session["NextDayOrderGoodsEntityList"] = new List<CargoOrderGoodsEntity>();
                }
                return (List<CargoOrderGoodsEntity>)(Session["NextDayOrderGoodsEntityList"]);
            }
            set
            {
                Session["NextDayOrderGoodsEntityList"] = value;
            }
        }
        /// <summary>
        /// 导出次日达商品明细
        /// </summary>
        public void ExportNextDayOrderGoods()
        {
            string err = "OK";
            CargoOrderGoodsEntity queryEntity = new CargoOrderGoodsEntity();
            CargoOrderBus bus = new CargoOrderBus();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderNo"])))
            {
                queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            }
            List<CargoOrderGoodsEntity> list = bus.QueryOrderGoodsInfo(queryEntity);
            if (list.Count > 0) { NextDayOrderGoodsEntityList = list; } else { err = "没有数据可以进行导出，请重新查询！"; }
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        #endregion
        #region 即日达订单
        /// <summary>
        /// 查询即日达订单
        /// </summary>
        public void QueryToDayOrdersInfo()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderType = "4";//微信小程序下单
            //queryEntity.ThrowGood = "22";//即日达订单
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.SettleHouseID.ToString();
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            if (Convert.ToString(Request["AwbStatus"]) != "-1")
            {
                queryEntity.AwbStatus = Convert.ToString(Request["AwbStatus"]);//订单状态
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AcceptPeople"])))
            {
                queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);//收货人
            }

            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryOrderInfo(pageIndex, pageSize, queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询订单明细
        /// </summary>
        public void QueryToDayOrderDetails()
        {
            CargoOrderGoodsEntity queryEntity = new CargoOrderGoodsEntity();
            CargoOrderBus bus = new CargoOrderBus();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderNo"])))
            {
                queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            }
            List<CargoOrderGoodsEntity> list = bus.QueryOrderGoodsInfo(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 即日达订单List
        /// </summary>
        public List<CargoOrderEntity> ToDayOrdersEntityList
        {
            get
            {
                if (Session["ToDayOrdersEntityList"] == null)
                {
                    Session["ToDayOrdersEntityList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["ToDayOrdersEntityList"]);
            }
            set
            {
                Session["ToDayOrdersEntityList"] = value;
            }
        }

        /// <summary>
        /// 导出即日达订单
        /// </summary>
        public void ExportToDayOrders()
        {
            string err = "OK";
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            #region 查询条件
            //分页
            int pageIndex = 1;
            int pageSize = 10000;
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderType = "4";//微信小程序下单
            //queryEntity.ThrowGood = "22";//即日达订单
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.SettleHouseID.ToString();
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            if (Convert.ToString(Request["AwbStatus"]) != "-1")
            {
                queryEntity.AwbStatus = Convert.ToString(Request["AwbStatus"]);//订单状态
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["AcceptPeople"])))
            {
                queryEntity.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);//收货人
            }
            #endregion
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryOrderInfo(pageIndex, pageSize, queryEntity);
            List<CargoOrderEntity> awbList = list["rows"] as List<CargoOrderEntity>;
            if (awbList.Count > 0) { ToDayOrdersEntityList = awbList; } else { err = "没有数据可以进行导出，请重新查询！"; }
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 即日达订单明细表List
        /// </summary>
        public List<CargoOrderGoodsEntity> ToDayOrderGoodsEntityList
        {
            get
            {
                if (Session["ToDayOrderGoodsEntityList"] == null)
                {
                    Session["ToDayOrderGoodsEntityList"] = new List<CargoOrderGoodsEntity>();
                }
                return (List<CargoOrderGoodsEntity>)(Session["ToDayOrderGoodsEntityList"]);
            }
            set
            {
                Session["ToDayOrderGoodsEntityList"] = value;
            }
        }
        /// <summary>
        /// 导出即日达商品明细
        /// </summary>
        public void ExportToDayOrderGoods()
        {
            string err = "OK";
            CargoOrderGoodsEntity queryEntity = new CargoOrderGoodsEntity();
            CargoOrderBus bus = new CargoOrderBus();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderNo"])))
            {
                queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            }
            List<CargoOrderGoodsEntity> list = bus.QueryOrderGoodsInfo(queryEntity);
            if (list.Count > 0) { ToDayOrderGoodsEntityList = list; } else { err = "没有数据可以进行导出，请重新查询！"; }
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        #endregion
        #region 渠道订单
        /// <summary>
        /// 查询订单数据
        /// </summary>
        public void QueryOrderInfo()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.SettleHouseID.ToString();
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            if (Convert.ToString(Request["AwbStatus"]) != "-1")
            {
                queryEntity.AwbStatus = Convert.ToString(Request["AwbStatus"]);
            }
            queryEntity.ThrowGood = "24";

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

        /// <summary>
        /// 保存订单
        /// </summary>
        public void saveOrderData()
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
            log.NvgPage = "新增渠道订单";
            log.UserID = UserInfor.LoginName.Trim();
            log.Operate = "A";
            log.Status = "0";

            CargoOrderEntity ent = new CargoOrderEntity();
            List<CargoOrderGoodsEntity> entDest = new List<CargoOrderGoodsEntity>();
            List<CargoContainerShowEntity> outHouseList = new List<CargoContainerShowEntity>();

            try
            {
                int HouseID = Convert.ToInt32(Request["HouseID"]);
                CargoHouseEntity houseEnt = house.QueryCargoHouseByID(HouseID);
                string HouseCode = houseEnt.HouseCode;
                string HouseName = houseEnt.Name;

                #region 赋值
                ent.Dep = houseEnt.DepCity;
                ent.Dest = Convert.ToString(Request["AcceptCity"]);
                ent.Piece = string.IsNullOrEmpty(Convert.ToString(Request["Piece"])) ? 0 : Convert.ToInt32(Request["Piece"]);
                ent.Weight = 0; ent.Volume = 0;
                ent.DeliveryFee = 0;
                ent.TransportFee = string.IsNullOrEmpty(Convert.ToString(Request["TransportFee"])) ? 0 : Convert.ToDecimal(Request["TransportFee"]);
                ent.TransitFee = ent.Piece * 10;//渠道订单配送费
                ent.OtherFee = 0;
                ent.Rebate = 0;
                ent.TotalCharge = ent.TransportFee + ent.TransitFee;
                ent.HouseID = HouseID;
                ent.HouseName = HouseName;
                ent.OutHouseName = HouseName;
                ent.CheckOutType = "0";
                ent.TrafficType = "0";
                ent.DeliveryType = Convert.ToString(Request["DeliveryType"]);
                ent.AcceptAddress = Convert.ToString(Request["AcceptAddress"]);
                ent.AcceptPeople = Convert.ToString(Request["AcceptPeople"]);
                ent.AcceptUnit = string.IsNullOrEmpty(Convert.ToString(Request["AcceptUnit"])) ? ent.AcceptPeople : Convert.ToString(Request["AcceptUnit"]);
                ent.AcceptTelephone = Convert.ToString(Request["AcceptTelephone"]);
                ent.AcceptCellphone = Convert.ToString(Request["AcceptCellphone"]);
                ent.CreateAwb = UserInfor.UserName;
                ent.CreateAwbID = UserInfor.LoginName;
                ent.CreateDate = DateTime.Now;
                ent.OP_ID = UserInfor.LoginName;
                ent.SaleManID = UserInfor.SaleManID;
                ent.SaleManName = UserInfor.SaleManName;
                ent.SaleCellPhone = Convert.ToString(Request["SaleCellPhone"]);
                ent.Remark = Convert.ToString(Request["Remark"]);
                ent.ThrowGood = "24";
                ent.IsPrintPrice = string.IsNullOrEmpty(Convert.ToString(Request["IsPrintPrice"])) ? 0 : 1;
                ent.TranHouse = "";
                ent.PostponeShip = "0";
                ent.LogisID = 62;
                ent.ClientNum = Convert.ToInt32(UserInfor.LoginName);
                ent.PayClientNum = Convert.ToInt32(UserInfor.LoginName);
                ent.PayClientName = UserInfor.UserUnit;//付款人客户姓名
                ent.ClientID = UserInfor.UserID;
                ent.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
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
                            Figure = Convert.ToString(ht["Figure"]),
                            ActSalePrice = Convert.ToDecimal(ht["ActSalePrice"])
                        });
                    }
                }
                ent.UserRulePiece = 0;
                //int pieceSum = 0;
                foreach (var itt in goodList)
                {
                    pieceSum += itt.StockNum;
                    int piece = itt.StockNum;
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
                        BatchYear = itt.BatchYear,
                        SuppClientNum = UserInfor.LoginName,
                        SpecsType = "4"
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

                ent.FinanceSecondCheck = "1";
                ent.FinanceSecondCheckName = UserInfor.UserName;
                ent.FinanceSecondCheckDate = DateTime.Now;
                ent.BusinessID = "22";
                if (msg.Result)
                {
                    bus.AddOrderInfo(ent, outHouseList, log);
                    bus.InsertCargoOrderPush(new CargoOrderPushEntity
                    {
                        OrderNo = ent.OrderNo,
                        Dep = ent.Dep,
                        Dest = ent.Dest,
                        Piece = ent.Piece,
                        TransportFee = ent.TransportFee,
                        ClientNum = ent.ClientNum.ToString(),
                        AcceptAddress = ent.AcceptAddress,
                        AcceptCellphone = ent.AcceptCellphone,
                        AcceptTelephone = ent.AcceptTelephone,
                        AcceptPeople = ent.AcceptPeople,
                        AcceptUnit = ent.AcceptUnit,
                        HouseID = ent.HouseID.ToString(),
                        HouseName = HouseName,
                        OP_ID = UserInfor.UserName,
                        PushType = "0",
                        PushStatus = "0",
                        LogisID = ent.LogisID
                    }, log);
                    msg.Message = ent.OrderNo + "/" + outID;//订单号和出库单号
                }
            }
            catch (ApplicationException ex) { msg.Message = ex.Message; msg.Result = false; }
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
            log.NvgPage = "新增渠道订单";
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
                        DeleteName = UserInfor.UserName,
                        LogisID = Convert.ToInt32(row["LogisID"])
                    });
                }
                if (msg.Result)
                {
                    bus.DeleteOrderInfo(list, log);
                    foreach (var it in list)
                    {
                        bus.UpdateCargoOrderPush(new CargoOrderPushEntity { OrderNo = it.OrderNo, PushType = "2", PushStatus = "0", Dest = it.Dest, Piece = it.Piece, AcceptUnit = it.AcceptUnit, AcceptAddress = it.AcceptAddress, AcceptPeople = it.AcceptPeople, AcceptTelephone = it.AcceptTelephone, AcceptCellphone = it.AcceptCellphone, ClientNum = it.ClientNum.ToString(), LogisID = it.LogisID }, log);
                    }
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
            Response.End();
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
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
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
            log.NvgPage = "渠道订单";
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
            log.NvgPage = "渠道订单";
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

                    entity.Specs = Convert.ToString(row["Specs"]);
                    entity.Model = Convert.ToString(row["Model"]);
                    entity.GoodsCode = Convert.ToString(row["GoodsCode"]);
                    entity.Figure = Convert.ToString(row["Figure"]);
                    entity.OutCargoID = Convert.ToString(row["OutCargoID"]);
                    entity.ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]);
                    entity.SupplySalePrice = entity.ActSalePrice;

                    CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
                    queryEntity.Specs = entity.Specs;
                    queryEntity.Figure = entity.Figure;
                    queryEntity.SpeedLevel = entity.SpeedLevel;
                    queryEntity.LoadIndex = entity.LoadIndex;
                    queryEntity.TradePrice = entity.TradePrice;
                    queryEntity.HouseID = entity.HouseID;
                    queryEntity.GoodsCode = entity.GoodsCode;
                    queryEntity.TypeID = entity.TypeID;
                    queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
                    queryEntity.SpecsType = "4";

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
        /// 查询所有在库的产品数据
        /// </summary>
        public void QueryALLHouseData()
        {
            CargoContainerShowEntity queryEntity = new CargoContainerShowEntity();
            CargoHouseBus bus = new CargoHouseBus();
            #region 查询条件
            if (!string.IsNullOrEmpty(Request["HouseID"]))
            {
                queryEntity.CargoPermisID = Convert.ToString(Request["HouseID"]);
            }
            else
            {
                queryEntity.CargoPermisID = UserInfor.SettleHouseID;
            }
            if (!string.IsNullOrEmpty(Request["SID"]))
            {
                queryEntity.TypeID = Convert.ToInt32(Request["SID"]);
            }
            else
            {
                queryEntity.TypeIDs = UserInfor.ClientTypeID;
            }
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
            queryEntity.GoodsCode = Convert.ToString(Request["GoodsCode"]);
            queryEntity.IsLockStock = "0";
            queryEntity.SpecsType = "4";
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            #endregion
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
        /// 查询客户所有收货地址
        /// </summary>
        public void AutoCompleteClientAcceptPeople()
        {
            CargoClientAcceptAddressEntity queryEntity = new CargoClientAcceptAddressEntity();
            queryEntity.ClientNum = Convert.ToInt32(UserInfor.LoginName);
            CargoClientBus bus = new CargoClientBus();
            List<CargoClientAcceptAddressEntity> list = new List<CargoClientAcceptAddressEntity>();
            list = bus.QueryAcceptAddress(queryEntity);

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
                    entity.ThrowGood = ord.ThrowGood;

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
                        queryEntity.SpecsType = "4";

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
                        entity.TransitFee = piece * 10;
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
                            ent.TransitFee = entity.TransitFee;
                            ent.ThrowGood = entity.ThrowGood;

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
                        entity.TransitFee = entity.Piece * 10;
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
        /// 修改订单中产品价格
        /// </summary>
        public void UpdateOrderSalePrice()
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
                CargoOrderBus bus = new CargoOrderBus();
                foreach (Hashtable row in rows)
                {
                    CargoOrderEntity ord = bus.QueryOrderInfo(new CargoOrderEntity { OrderNo = Convert.ToString(row["OrderNo"]) });
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
                    entity.ProductID = Convert.ToInt64(row["ProductID"]);
                    entity.ContainerID = Convert.ToInt32(row["ContainerID"]);
                    entity.TypeID = Convert.ToInt32(row["TypeID"]);
                    entity.OrderNo = Convert.ToString(row["OrderNo"]);
                    entity.AreaID = Convert.ToInt32(row["AreaID"]);
                    entity.ContainerCode = Convert.ToString(row["ContainerCode"]);
                    entity.ActSalePrice = Convert.ToDecimal(row["ActSalePrice"]);//新价格
                    entity.SalePrice = Convert.ToDecimal(row["SalePrice"]);//旧价格
                    entity.OutCargoID = Convert.ToString(row["OutCargoID"]);

                    entity.Batch = Convert.ToString(row["Batch"]);
                    entity.ThrowGood = ord.ThrowGood;
                    entity.Piece = Convert.ToInt32(row["Piece"]);
                    entity.SupplySalePrice = entity.ActSalePrice;

                }
                if (msg.Result)
                {
                    bus.UpdateOrderSalePrice(entity, log);
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
        #endregion
        #region 退货订单
        public void QueryReturnOrdersInfo()
        {
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ThrowGood"])))
            {
                queryEntity.ThrowGood = Convert.ToString(Request["ThrowGood"]);
            }
            queryEntity.OrderType = "4";//微信小程序下单
            queryEntity.OrderModel = "1";
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.SettleHouseID.ToString();
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            //分页
            int pageIndex = Convert.ToInt32(Request["page"]);
            int pageSize = Convert.ToInt32(Request["rows"]);
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryOrderInfo(pageIndex, pageSize, queryEntity);
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 查询订单明细
        /// </summary>
        public void QueryReturnOrderDetails()
        {
            CargoOrderGoodsEntity queryEntity = new CargoOrderGoodsEntity();
            CargoOrderBus bus = new CargoOrderBus();
            if (!string.IsNullOrEmpty(Convert.ToString(Request["OrderNo"])))
            {
                queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            }
            List<CargoOrderGoodsEntity> list = bus.QueryOrderGoodsInfo(queryEntity);
            //JSON
            String json = JSON.Encode(list);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        public void ExportReturnOrdersInfo()
        {
            string err = "OK";
            CargoOrderEntity queryEntity = new CargoOrderEntity();
            #region 查询条件
            //分页
            int pageIndex = 1;
            int pageSize = 10000;

            if (!string.IsNullOrEmpty(Convert.ToString(Request["StartDate"]))) { queryEntity.StartDate = Convert.ToDateTime(Request["StartDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["EndDate"]))) { queryEntity.EndDate = Convert.ToDateTime(Request["EndDate"]); }
            if (!string.IsNullOrEmpty(Convert.ToString(Request["ThrowGood"])))
            {
                queryEntity.ThrowGood = Convert.ToString(Request["ThrowGood"]);
            }
            queryEntity.OrderType = "4";//微信小程序下单
            queryEntity.OrderModel = "1";
            queryEntity.OrderNo = Convert.ToString(Request["OrderNo"]);
            queryEntity.CargoPermisID = UserInfor.SettleHouseID.ToString();
            queryEntity.SuppClientNum = Convert.ToInt32(UserInfor.LoginName);
            #endregion
            CargoOrderBus bus = new CargoOrderBus();
            Hashtable list = bus.QueryOrderInfo(pageIndex, pageSize, queryEntity);
            List<CargoOrderEntity> awbList = list["rows"] as List<CargoOrderEntity>;
            if (awbList.Count > 0) { ReturnOrdersInfoList = awbList; } else { err = "没有数据可以进行导出，请重新查询！"; }
            String json = JSON.Encode(err);
            Response.Clear();
            Response.Write(json);
            Response.End();
        }
        /// <summary>
        /// 
        /// </summary>
        public List<CargoOrderEntity> ReturnOrdersInfoList
        {
            get
            {
                if (Session["ReturnOrdersInfoList"] == null)
                {
                    Session["ReturnOrdersInfoList"] = new List<CargoOrderEntity>();
                }
                return (List<CargoOrderEntity>)(Session["ReturnOrdersInfoList"]);
            }
            set
            {
                Session["ReturnOrdersInfoList"] = value;
            }
        }
        #endregion
    }
}