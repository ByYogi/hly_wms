using House.Business.Cargo;
using House.Entity.Cargo;
using House.Entity;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Cargo.QY;
using Senparc.Weixin.HttpUtility;
using Memcached.ClientLibrary;
using System.Data;
using Org.BouncyCastle.Tsp;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Runtime.Remoting.Contexts;
using NPOI.HSSF.Record.Formula.Functions;

namespace Cargo.Interface
{
    public partial class AdvanceUnionPaySuccess : System.Web.UI.Page
    {
        //protected MemcachedClient mc = new MemcachedClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            /**
             * 此处注意,因为通联收银宝以后可能增加字段,所以,这里一定要动态遍历获取所有的请求参数
             * 
             *  {
             *     "acct": "oUu146xxl9wwSeFLEKe5m4SJJYOs",//交易账号
             *     "accttype": "99",       //借贷标识  00-借记卡  02 - 信用卡 99 - 其他（花呗 / 余额等）
             *     "appid": "00328929",// 收银宝APPID
             *     "bankcode": "OTHERS",//发卡行
             *     "chnlid": "205299480",//渠道号
             *     "chnltrxid": "4200002453202411195236140850",  //渠道流水号  如支付宝，微信平台订单号  WXPayOrderNo
             *     "cmid": "709229030",// 渠道子商户号
             *     "cusid": "660581055322T0Z",//商户编号
             *     "cusorderid": "241119090514193", //业务流水  统一下单对应的reqsn订单号       OrderNo
             *     "initamt": "1",   //原始下单金额   与请求trxamt值一致  分
             *     "outtrxid": "241119090514193",//第三方交易号
             *     "paytime": "20241119090542",//交易完成时间
             *     "sign": "lR2iiIBc7wsE+0pJUoC+ZHIZvrbegPCzyAAmUtTYveKITzuV6SwyA6IoiL9SqP5ud/peLtB/gipNMZMzK6c+CjTfD6YcwFYcSkZjbrtpYHkmee0OwJMrkOnFm4Fv8a8cqT46yQcHzQvyffwIFwfadbTu+s2jkzhAI55nJvvzC6U=",//签名
             *     "signtype": "RSA",//签名方式
             *     "termauthno": "OTHERS",//终端授权码
             *     "termrefnum": "4200002453202411195236140850",//终端参考号
             *     "termtraceno": "0",//终端流水号
             *     "trxamt": "1",//交易金额  分
             *     "trxcode": "VSP681", //交易类型 VSP681  微信订单预消费
             *     "trxdate": "20241119",//时间  yyyymmdd
             *     "trxid": "241119118328536472",     //收银宝交易单号
             *     "trxstatus": "0000"   //交易结果码
             * }
             * */
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "慧采云仓";
            log.Status = "0";
            log.NvgPage = "小程序付款";
            log.UserID = "MiniPro";
            log.Operate = "U";
            Dictionary<String, String> reqParams = new Dictionary<String, String>();
            for (int i = 0; i < Request.Form.Count; i++)
            {
                reqParams.Add(Request.Form.Keys[i], Request.Form[i].ToString());
            }
            string req = JsonConvert.SerializeObject(reqParams);
            Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：" + req);
            if (!reqParams.ContainsKey("sign"))//如果不包含sign,则不进行处理
            {
                Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：未包含sign");
                Response.Write("error");
                Response.End();
                return;
            }

            if (Convert.ToInt32(reqParams["trxamt"]) != Convert.ToInt32(reqParams["initamt"]))
            {
                Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：下单金额与交易金额不一致");
                Response.Write("error");
                Response.End();
                return;
            }
            //var payment_type = reqParams["payment_type"];
            //if (string.IsNullOrEmpty(payment_type))
            //{
            //    Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：未获取到支付状态");
            //    Response.Write("error");
            //    Response.End();
            //    return;
            //}


            CargoWeiXinBus bus = new CargoWeiXinBus();
            CargoOrderBus orderBus = new CargoOrderBus();
            CargoClientBus clientBus = new CargoClientBus();
            string out_trade_no = Convert.ToString(reqParams["cusorderid"]);//我的系统生成的订单号


            List<WXOrderEntity> orderList = bus.QueryWeixinReserveOrderInfo(1, 100, new WXOrderEntity { AccountNo = out_trade_no });

            if (orderList.Count > 0)
            {
                #region MyRegion
                CargoFinanceBus fina = new CargoFinanceBus();
                string awbidlist = string.Empty;
                List<string> entOrderNo = new List<string>();
                //如果客户是微信付款，并已支付成功，则修改订单状态为已结算
                CargoCashierEntity ent = new CargoCashierEntity();
                ent.WxID = 359;//联通支付微信商城微信支付收款账号ID
                ent.AffectWX = Convert.ToDecimal(Convert.ToInt32(reqParams["trxamt"]) * 0.01);
                ent.OP_ID = log.UserID;
                ent.UserName = log.UserID;
                ent.RType = "0";//收支类型，0收入1支出
                ent.FromTO = "0";//按订单号收款
                ent.TradeType = "3";//微信商城付款
                ent.CheckStatus = "1";
                //批量支付的成功返回
                foreach (var it in orderList)
                {
                    Common.WriteTextLog(it.OrderNo);
                    //修改微信订单支付状态 PayStatus WXPayOrderNo
                    bus.UpdateWeixinOrderPayStatus(new WXOrderEntity { OrderNo = it.OrderNo, WXPayOrderNo = Convert.ToString(reqParams["chnltrxid"]), PayStatus = "1", Trxid = Convert.ToString(reqParams["trxid"]) }, log);

                    entOrderNo.Add(it.CargoOrderNo);
                    awbidlist += it.CargoOrderNo + ",";
                    ent.ClientNum = it.ClientNum;
                    ent.CargoOrderNo = it.CargoOrderNo;
                    ent.WXPayOrderNo = Convert.ToString(reqParams["chnltrxid"]);
                    ent.PaymentType = it.PaymentType;
                    ent.Trxid = Convert.ToString(reqParams["trxid"]);
                }
                ent.OrderNo = entOrderNo;
                ent.AffectAwbNO = awbidlist;
                ent.clientPreRecord = new List<CargoClientPreRecordEntity>();
                Common.WriteTextLog("小程序 开始合并付款" + awbidlist);
                fina.SaveCash_Advance(ent, log);
                Common.WriteTextLog("小程序 SOU");
                #endregion
            }
            else
            {
                Common.WriteTextLog("慧采云仓小程序 单个");
                #region MyRegion
                List<WXOrderEntity> result = bus.QueryWeixinReserveOrderInfo(1, 5, new WXOrderEntity { OrderNo = out_trade_no });
                if (bus.IsExistWeixinOrderPay(new WXOrderEntity { OrderNo = out_trade_no, PayStatus = "0" }) && result.Count > 0)
                {
                    //1. 修改订单支付状态
                    bus.UpdateWeixinOrderPayStatus(new WXOrderEntity { OrderNo = out_trade_no, WXPayOrderNo = Convert.ToString(reqParams["chnltrxid"]), PayStatus = "1", CargoOrderNo = result[0].CargoOrderNo, Trxid = Convert.ToString(reqParams["trxid"]) }, log);
                    //扫描运单二维码支付成功返回的请求
                    //1.修改订单的支付状态
                    CargoFinanceBus fina = new CargoFinanceBus();
                    //先审核
                    //List<CargoOrderEntity> oeL = new List<CargoOrderEntity>();
                    //oeL.Add(new CargoOrderEntity
                    //{
                    //    OrderID = result[0].OrderID,
                    //    OrderNo = result[0].CargoOrderNo,
                    //    FinanceSecondCheck = "1",
                    //    FinanceSecondCheckName = result[0].Name,
                    //    FinanceSecondCheckDate = DateTime.Now
                    //});
                    //fina.plSecondCheckOrder(oeL, log);
                    CargoOrderEntity ord = orderBus.QueryOrderInfo(new CargoOrderEntity { OrderNo = result[0].CargoOrderNo });

                    //再支付
                    string awbidlist = string.Empty;
                    List<string> entOrderNo = new List<string>();
                    //如果客户是微信付款，并已支付成功，则修改订单状态为已结算
                    CargoCashierEntity ent = new CargoCashierEntity();
                    ent.WxID = 359;//科技公司的微信商城微信支付收款账号ID 337  358：狄乐汽服微信商户号
                    ent.AffectWX = Convert.ToDecimal(Convert.ToInt32(reqParams["trxamt"]) * 0.01);//微信 支付的金额

                    ent.OP_ID = log.UserID;
                    ent.UserName = log.UserID;
                    ent.RType = "0";//收支类型，0收入1支出
                    ent.FromTO = "0";//按订单号收款
                    ent.TradeType = "3";//微信商城付款
                    ent.CheckStatus = "1";
                    ent.WxOrderNo = out_trade_no;
                    entOrderNo.Add(result[0].CargoOrderNo);
                    awbidlist += result[0].CargoOrderNo + ",";

                    ent.ClientNum = result[0].ClientNum;
                    ent.OrderNo = entOrderNo;
                    ent.AffectAwbNO = awbidlist;
                    ent.PaymentType = result[0].PaymentType;
                    fina.SaveCash_Advance(ent, log);
                    Common.WriteTextLog("慧采云仓小程序 收款完成" + out_trade_no + "，订单号：" + result[0].CargoOrderNo);

                    try
                    {
                        if (!ord.CheckOutType.Equals("3"))
                        {
                            string proStr = string.Empty;
                            if (result[0].productList.Count > 0)
                            {
                                proStr = result[0].productList[0].ProductName;
                            }

                            CargoHouseBus house = new CargoHouseBus();

                            CargoHouseEntity houseEnt = house.QueryCargoHouseByID(ord.HouseID);
                            string fkfs = ord.CheckOutType.Equals("3") ? "货到付款" : ord.CheckOutType.Equals("10") ?"预付款": "现付";
                            string shf = ord.DeliveryType.Equals("0") ? "急送" : ord.DeliveryType.Equals("1") ? "自提" : "次日达";
                            string tit = ord.ThrowGood.Equals("22") ? "急速达" : "次日达";
                            string go = ord.ThrowGood.Equals("22") ? "" : "-不出库";
                            QySendInfoEntity send = new QySendInfoEntity();
                            send.title = tit + " 有新订单";
                            //推送给提交人
                            send.msgType = msgType.textcard;
                            send.agentID = "1000003";//消息通知的应用
                            send.AgentSecret = "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM";
                            //send.toUser = qup.ApplyID;<div>订单金额：" + ord.TotalCharge.ToString("F2") + "</div>
                            send.toTag = houseEnt.HCYCOrderPushTagID.ToString();
                            //send.toTag = "19";
                            send.content = "<div></div><div>出库仓库：" + houseEnt.Name + go + "</div><div>商城订单号：" + ord.WXOrderNo + "</div><div>出库订单号：" + ord.OrderNo + "</div><div>订单数量：" + result[0].Piece.ToString() + "</div><div>订单金额：" + ord.TotalCharge.ToString("F2") + "</div><div>货物信息：" + proStr + "</div><div>付款方式：" + fkfs + "</div><div>送货方式：" + shf + "</div><div>门店名称：" + ord.AcceptUnit + "</div><div>收货信息：" + ord.AcceptPeople + " " + ord.AcceptCellphone + "</div><div>收货地址：" + ord.AcceptAddress + "</div><div>请仓管人员留意尽快出库！</div>";
                            send.url = "http://dlt.neway5.com/QY/qyScanOrderSign.aspx?OrderNo=" + ord.OrderNo;
                            if (ord.ThrowGood=="23" && ord.ShareHouseID == 0)
                            {
                                string name = houseEnt.Name + " " + proStr;
                                // 若长度小于等于20，直接返回原字符串；否则截取前20个
                                name= name.Length <= 20 ? name : name.Substring(0, 20);
                                Common.SendRePlaceAnOrderMsg(ord.SuppClientNum.ToString(), ord.OrderNo, ord.OrderNo, name, ord.TotalCharge, ord.Piece);
                            }
                            else
                            {
                                //企业微信通知
                                WxQYSendHelper.DLTQYPushInfo(send);
                            }

                            CargoNewOrderNoticeEntity cargoNewOrder = new CargoNewOrderNoticeEntity();
                            cargoNewOrder.HouseName = houseEnt.Name;
                            cargoNewOrder.OrderNo = ord.WXOrderNo;
                            cargoNewOrder.OrderNum = result[0].Piece.ToString();
                            cargoNewOrder.ClientInfo = ord.AcceptPeople + " " + ord.AcceptCellphone + " " + ord.AcceptAddress;// "泰乐 华笙 广东省广州市白云区东平加油站左侧";
                            cargoNewOrder.ProductInfo = proStr;// "马牌 215/55R16 CC6 98V";
                            cargoNewOrder.DeliveryName = shf;// "自提";
                            cargoNewOrder.ReceivePeople = "";
                            string hcno = JSON.Encode(cargoNewOrder);
                           // Common.WriteTextLog("弹窗提醒：" + hcno);
                            List<CargoVoiceBroadEntity> voiceBroadList = house.GetVoiceBroadList(new CargoVoiceBroadEntity { HouseID = houseEnt.HouseID });
                            foreach (var voice in voiceBroadList)
                            {
                                RedisHelper.SetString("NewOrderNotice_" + voice.LoginName, hcno);
                                //mc.Add("NewOrderNotice_" + voice.LoginName, hcno);
                                //Common.WriteTextLog("弹窗提醒成功：" + voice.LoginName);
                            }
                        }

                    }
                    catch (ApplicationException ex)
                    {
                        Common.WriteTextLog("慧采云仓小程序 通联支付回调信息：" + ex);
                    }

                }
                #endregion
            }

            Response.Write("success");
            Response.End();

        }

    }
}