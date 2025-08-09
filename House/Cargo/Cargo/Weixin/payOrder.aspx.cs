using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Cargo.Weixin
{
    public partial class payOrder : WXBasePage
    {
        public static void WriteTextLog(string strMessage)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"System\Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + DateTime.Now.ToString("yyyy-MM-dd") + ".System.txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + DateTime.Now.ToString() + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }
        public int Lm = 0;
        public int HouseID = 0;
        public string orderNo = string.Empty;
        public static string wxJsApiParam { get; set; }//微信支付JSApi参数
        public string appId = string.Empty;
        public string timeStamp = string.Empty;
        public string nonceStr = string.Empty;
        public string package = string.Empty;
        public string paySign = string.Empty;
        //这里通过官方的一个实体，用户自行使用，我这里是直接读取的CONFIG文件
        private static TenPayV3Info tenPayV3 = new TenPayV3Info(Common.GetdltAPPID(), Common.GetdltAppSecret(), ConfigurationManager.AppSettings["dltMachID"], ConfigurationManager.AppSettings["dltWxPayKey"], ConfigurationManager.AppSettings["dltWxPayTranUrl"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            appId = Common.GetdltAPPID();
            HouseID = WxUserInfo.HouseID;
            string zj = Request.QueryString["totalFee"];
            string piece = Request.QueryString["piece"];
            string cart = Request.QueryString["cart"];
            string address = Request.QueryString["address"];
            string logicname = Request.QueryString["LogicName"];
            string memo = Request.QueryString["Memo"];
            if (string.IsNullOrEmpty(Request.QueryString["HouseID"])) { return; }
            int houseID = Convert.ToInt32(Request["HouseID"]);

            decimal hzj = 0.00M;
            decimal wlfy = 0.00M;
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(piece)) { WriteTextLog("数量有误"); return; }
                if (string.IsNullOrEmpty(zj)) { WriteTextLog("金额有误"); return; }
                ArrayList rows = (ArrayList)JSON.Decode(cart);
                List<CargoProductShelvesEntity> pro = new List<CargoProductShelvesEntity>();
                int oNum = 0;
                foreach (Hashtable row in rows)
                {
                    if (Convert.ToInt32(row["PC"]) <= 0) { continue; }
                    pro.Add(new CargoProductShelvesEntity
                    {
                        ID = Convert.ToInt64(row["ID"]),
                        OrderNum = Convert.ToInt32(row["PC"]),
                        OrderPrice = Convert.ToDecimal(row["PRICE"]),
                        ModifyPrice = Convert.ToDecimal(row["PRICE"])
                    });
                    oNum += Convert.ToInt32(row["PC"]);
                    hzj += Convert.ToDecimal(row["PC"]) * Convert.ToDecimal(row["PRICE"]);
                }
                if (houseID.Equals(46) && WxUserInfo.LogisID.Equals(34))
                {
                    //四川仓库特殊处理运费
                    wlfy = oNum * WxUserInfo.LogisFee;
                }
                if (!Convert.ToDecimal(zj).Equals(hzj + wlfy))
                {
                    WriteTextLog("金额不相等,付款金额：" + (hzj + wlfy).ToString() + ",回传金额：" + zj);
                    return;
                }
                //if (!Convert.ToDecimal(zj).Equals(hzj))
                //{
                //    WriteTextLog("实际付款金额与传回金额不等" + hzj.ToString() + ",回传金额：" + zj);
                //    return;
                //}
                WXUserAddressEntity addressEnt = new WXUserAddressEntity();
                ArrayList addre = (ArrayList)JSON.Decode(address);
                foreach (Hashtable dz in addre)
                {
                    addressEnt.Name = Convert.ToString(dz["Name"]);
                    addressEnt.Province = Convert.ToString(dz["Province"]);
                    addressEnt.City = Convert.ToString(dz["City"]);
                    addressEnt.Country = Convert.ToString(dz["Country"]);
                    addressEnt.Address = Convert.ToString(dz["Address"]);
                    addressEnt.Cellphone = Convert.ToString(dz["Cellphone"]);
                }
                CargoWeiXinBus wbus = new CargoWeiXinBus();
                WXUserEntity wxUser = wbus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = WxUserInfo.wxOpenID });
                //WriteTextLog("开始支付");
                //生成预支付订单ID
                string orderno = GetOrderNumber();
                //zj = "1";
                //WriteTextLog(logicname + "|" + memo);
                decimal wxZJ = Convert.ToDecimal(zj) * 100;
                //WriteTextLog(WxUserInfo.wxOpenID);
                //WriteTextLog(wxZJ + "|" + zj);
                //WriteTextLog(wxUser.LimitMoney.ToString());
                if (wxUser.QuotaLimit.Equals("1"))
                {
                    //额度付款被冻结
                    Lm = 2;
                    ltlOrder.Text = "<div class='mg10-0 t-c'>订单号：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + orderno + "</em></span></div><div class='mg10-0 t-c'>总金额：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + zj + "</em>元</span></div>";
                }
                else
                {
                    if (wxUser.LimitMoney > Convert.ToDecimal(zj))
                    {
                        Lm = 1;
                        //如果透支金额大于订单金额可以使用透支额度付款
                        ltlOrder.Text = "<div class='mg10-0 t-c'>订单号：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + orderno + "</em></span></div><div class='mg10-0 t-c'>总金额：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + zj + "</em>元</span></div><div class='mg10-0 t-c'>可透支额度：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + wxUser.LimitMoney.ToString("F2") + "元</em></span></div>";
                    }
                    else
                    {
                        ltlOrder.Text = "<div class='mg10-0 t-c'>订单号：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + orderno + "</em></span></div><div class='mg10-0 t-c'>总金额：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + zj + "</em>元</span></div>";
                    }
                }
                WriteTextLog(orderno);
                string prepayID = PayInfo("", "迪乐泰轮胎", WxUserInfo.wxOpenID, wxZJ.ToString("F0"), orderno);
                WriteTextLog(prepayID);
                orderNo = orderno;
                CargoLogisticEntity logicEnt = new CargoLogisticEntity();

                if (houseID.Equals(9))
                {
                    logicEnt.ID = 34;
                    logicEnt.LogisticName = "好来运速递";
                }
                else if (houseID.Equals(34))
                {
                    logicEnt.ID = 62;
                    logicEnt.LogisticName = "新陆程物流";
                }
                else
                {
                    if (WxUserInfo.LogisID.Equals(0))
                    {
                        if (string.IsNullOrEmpty(logicname))
                        {
                            if (houseID.Equals(1))
                            {
                                logicEnt.ID = 34;
                                logicEnt.LogisticName = "好来运速递";
                            }
                        }
                        else
                        {
                            CargoStaticBus staticbus = new CargoStaticBus();
                            List<CargoLogisticEntity> logicEntity = staticbus.QueryAllLogistic(new CargoLogisticEntity { LogisticName = logicname });
                            if (logicEntity.Count > 0)
                            {
                                logicEnt = logicEntity[0];
                            }
                        }
                    }
                    else
                    {
                        if (!logicname.Equals(WxUserInfo.LogicName))
                        {
                            CargoStaticBus staticbus = new CargoStaticBus();
                            List<CargoLogisticEntity> logicEntity = staticbus.QueryAllLogistic(new CargoLogisticEntity { LogisticName = logicname });
                            if (logicEntity.Count > 0)
                            {
                                logicEnt = logicEntity[0];
                            }
                        }
                        else
                        {
                            logicEnt.ID = WxUserInfo.LogisID;
                            logicEnt.LogisticName = WxUserInfo.LogicName;
                        }
                    }
                }
                //推送给客户的所属业务员消息
                WXUserAddressEntity clerkEntity = wbus.QueryWxAreaClient(new WXUserAddressEntity { Province = addressEnt.Province, City = addressEnt.City });
                //WriteTextLog(prepayID);
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "微信服务号";
                log.Status = "0";
                log.NvgPage = "微信付款";
                log.UserID = WxUserInfo.wxOpenID;
                log.Operate = "A";
                wbus.SaveWeixinOrder(new WXOrderEntity
                {
                    OrderNo = orderno,
                    TotalCharge = Convert.ToDecimal(zj),
                    TransitFee = wlfy,
                    WXID = WxUserInfo.ID,
                    PayStatus = "0",
                    OrderStatus = "0",
                    PayWay = "0",
                    OrderType = "2",//微信商城下单
                    Piece = Convert.ToInt32(piece),
                    Address = addressEnt.Address,
                    Cellphone = addressEnt.Cellphone,
                    City = addressEnt.City,
                    Province = addressEnt.Province,
                    Country = addressEnt.Country,
                    Name = addressEnt.Name,
                    HouseID = houseID,
                    SaleManID = clerkEntity.LoginName,
                    Memo = memo,
                    LogisID = logicEnt.ID,
                    LogicName = logicEnt.LogisticName,
                    productList = pro
                }, log);
                if (WxUserInfo.LogisID.Equals(0))
                {
                    wbus.UpdateWxUserLogicName(new WXUserEntity { ID = WxUserInfo.ID, wxName = WxUserInfo.wxName, wxOpenID = WxUserInfo.wxOpenID, LogicName = logicEnt.LogisticName, LogisID = logicEnt.ID }, log);
                }

                SendTempleteMessage send = new SendTempleteMessage();
                //推送客户消息
                //TemplateMsg tmMsg = new TemplateMsg
                //{
                //    first = new TemplateDataItem("尊敬的迪乐泰客户，您已下单成功！", "#173177"),
                //    keyword1 = new TemplateDataItem(piece + "条", "#173177"),
                //    keyword2 = new TemplateDataItem(zj + "元", "#173177"),
                //    keyword3 = new TemplateDataItem("", "#173177"),
                //    keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "#173177"),
                //    keyword5 = new TemplateDataItem(addressEnt.Province + " " + addressEnt.City + " " + addressEnt.Country + " " + addressEnt.Address, "#173177"),
                //    remark = new TemplateDataItem("点击查看订单详情!", "#173177")
                //};
                //string token = AccessTokenContainer.TryGetAccessToken(tenPayV3.AppId, tenPayV3.AppSecret, true);
                //WriteTextLog(token);
                string token = Common.GetWeixinToken(Common.GetdltAPPID(), Common.GetdltAppSecret());
                //oo1LEt_z6Yhx-g_qdgrYhLaYaazE//测试
                //string errmsg = send.SendMessage(token, WxUserInfo.wxOpenID, "kAbX9tpdCB8Sz3TLYwDqk7k4ytQ7ZJLUG3RcdofXp6A", "http://dlt.neway5.com/Weixin/OrderInfo.aspx?orderNo=" + orderno, tmMsg);
                //WriteTextLog("02");
                //推送给客户的所属业务员消息
                //WXUserAddressEntity clerkEntity = bus.QueryWxAreaClient(new WXUserAddressEntity { Province = addressEnt.Province, City = addressEnt.City });
                try
                {
                    if (!string.IsNullOrEmpty(clerkEntity.wxOpenID))
                    {
                        TemplateMsg bustmMsg = new TemplateMsg
                        {
                            first = new TemplateDataItem("客户新订单，您的客户" + addressEnt.Name + "已下单成功！", "#173177"),
                            keyword1 = new TemplateDataItem(piece + "条", "#173177"),
                            keyword2 = new TemplateDataItem(zj + "元", "#173177"),
                            keyword3 = new TemplateDataItem("", "#173177"),
                            keyword4 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "#173177"),
                            keyword5 = new TemplateDataItem(addressEnt.Province + " " + addressEnt.City + " " + addressEnt.Country + " " + addressEnt.Address, "#173177"),
                            remark = new TemplateDataItem("点击查看订单详情并确认订单!", "#173177")
                        };
                        string buserrmsg = send.SendMessage(token, clerkEntity.wxOpenID, "kAbX9tpdCB8Sz3TLYwDqk7k4ytQ7ZJLUG3RcdofXp6A", "http://dlt.neway5.com/Weixin/OrderInfoManager.aspx?orderNo=" + orderno, bustmMsg);
                    }
                }
                catch (Exception) { }
                string noticeArray = string.Empty;
                if (houseID.Equals(1))
                {
                    //湖南仓库
                    noticeArray = Common.GetdltGetOrderNotice();
                }
                else if (houseID.Equals(3))
                {
                    //湖北仓库
                    noticeArray = Common.GetdltGetHuBeiOrderNotice();
                }
                else if (houseID.Equals(11))
                {
                    //西安迪乐泰 
                    noticeArray = Common.GetdltGetXiAnOrderNotice();
                }
                else if (houseID.Equals(12))
                {
                    //梅州 揭阳仓库
                    noticeArray = Common.GetdltGetMeiZhouOrderNotice();
                }
                else if (houseID.Equals(9))
                {
                    //广州仓库
                    noticeArray = Common.GetdltGetGuangZhouOrderNotice();
                }
                else if (houseID.Equals(34))
                {
                    //广州仓库
                    noticeArray = Common.GetdltGetHaiNanOrderNotice();
                }
                else if (houseID.Equals(44))
                {
                    //揭阳仓库
                    noticeArray = Common.GetdltGetJieYangOrderNotice();
                }
                else if (houseID.Equals(46))
                {
                    //四川仓库
                    noticeArray = Common.GetdltGetSiChuanOrderNotice();
                }

                if (!string.IsNullOrEmpty(noticeArray))
                {
                    string[] notice = noticeArray.Split('/');
                    for (int i = 0; i < notice.Length; i++)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(notice[i]))
                            {
                                //推送客户消息
                                TemplateMsg tmMs = new TemplateMsg
                                {
                                    first = new TemplateDataItem("有新订单,请尽快确认订单并拣货出库发货！", "#173177"),
                                    keyword1 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "#173177"),
                                    keyword2 = new TemplateDataItem(piece + "条", "#173177"),
                                    keyword3 = new TemplateDataItem(zj + "元", "#173177"),
                                    keyword4 = new TemplateDataItem(addressEnt.Name + " " + addressEnt.Cellphone + " " + addressEnt.Province + " " + addressEnt.City + " " + addressEnt.Country + " " + addressEnt.Address, "#173177"),
                                    keyword5 = new TemplateDataItem("", "#173177"),
                                    remark = new TemplateDataItem("点击查看订单详情并确认订单!", "#173177")
                                };

                                string err = send.SendMessage(token, notice[i], "tocpvplR_z6_K6R8stAbMHhEeYhVm_BaQ3MF4iC9vDw", "http://dlt.neway5.com/Weixin/OrderInfoManager.aspx?orderNo=" + orderno, tmMs);
                            }
                        }
                        catch (ApplicationException ex) { continue; }
                    }
                }
                //设置支付参数
                RequestHandler paySignReqHandler = new RequestHandler();
                paySignReqHandler.SetParameter("appId", tenPayV3.AppId);
                paySignReqHandler.SetParameter("timeStamp", TenPayV3Util.GetTimestamp());
                paySignReqHandler.SetParameter("nonceStr", TenPayV3Util.GetNoncestr());
                paySignReqHandler.SetParameter("package", string.Format("prepay_id={0}", prepayID));
                paySignReqHandler.SetParameter("signType", "MD5");
                paySign = paySignReqHandler.CreateMd5Sign("key", tenPayV3.Key);

                appId = tenPayV3.AppId;
                timeStamp = TenPayV3Util.GetTimestamp();
                nonceStr = TenPayV3Util.GetNoncestr();
                package = "prepay_id=" + prepayID;
                //paysign = paySign;

                //wxJsApiParam = "{";
                //wxJsApiParam += " \"appId\": \"" + tenPayV3.AppId + "\", ";
                //wxJsApiParam += " \"timeStamp\": \"" + TenPayV3Util.GetTimestamp() + "\", ";
                //wxJsApiParam += " \"nonceStr\": \"" + TenPayV3Util.GetNoncestr() + "\", ";
                //wxJsApiParam += " \"package\": \"" + string.Format("prepay_id={0}", prepayID) + "\", ";
                //wxJsApiParam += " \"signType\": \"MD5\", ";
                //wxJsApiParam += " \"paySign\": \"" + paySign + "\"";
                //wxJsApiParam += "}";
            }
        }

        public string GetOrderNumber()
        {
            string Number = DateTime.Now.ToString("yyMMddHHmmss");
            return Number + Next(1000, 1).ToString();
        }
        private static int Next(int numSeeds, int length)
        {
            byte[] buffer = new byte[length];
            System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();
            Gen.GetBytes(buffer);
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)buffer[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds);
        }
        /// <summary>
        /// 微信预支付 返回预支付 ID
        /// </summary>
        /// <param name="attach"></param>
        /// <param name="body"></param>
        /// <param name="openid"></param>
        /// <param name="price"></param>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        private string PayInfo(string attach, string body, string openid, string price, string orderNum)
        {
            WriteTextLog("tenPayV3.AppId" + tenPayV3.AppId);
            WriteTextLog("Common.GetdltAPPID()" + Common.GetdltAPPID());
            string prepayId = string.Empty;
            RequestHandler requestHandler = new RequestHandler(HttpContext.Current);
            //微信分配的公众账号ID（企业号corpid即为此appId）
            requestHandler.SetParameter("appid", Common.GetdltAPPID());
            //附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
            requestHandler.SetParameter("attach", attach);
            //商品或支付单简要描述
            requestHandler.SetParameter("body", body);
            //微信支付分配的商户号
            requestHandler.SetParameter("mch_id", tenPayV3.MchId);
            //随机字符串，不长于32位。
            requestHandler.SetParameter("nonce_str", TenPayV3Util.GetNoncestr());
            //接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数。
            requestHandler.SetParameter("notify_url", tenPayV3.TenPayV3Notify);
            //trade_type=JSAPI，此参数必传，用户在商户公众号appid下的唯一标识。
            requestHandler.SetParameter("openid", openid);
            //商户系统内部的订单号,32个字符内、可包含字母，自己生成
            requestHandler.SetParameter("out_trade_no", "JSAPI" + orderNum);
            //APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。
            //requestHandler.SetParameter("spbill_create_ip", "127.0.0.1");
            //订单总金额，单位为分，做过银联支付的朋友应该知道，代表金额为12位，末位分分
            requestHandler.SetParameter("total_fee", price);
            //取值如下：JSAPI，NATIVE，APP，我们这里使用JSAPI
            requestHandler.SetParameter("trade_type", Senparc.Weixin.MP.TenPayV3Type.JSAPI.ToString());
            //设置KEY
            //requestHandler.SetKey(tenPayV3.Key);
            string sign = requestHandler.CreateMd5Sign("key", tenPayV3.Key);
            requestHandler.SetParameter("sign", sign);

            string data = requestHandler.ParseXML();
            requestHandler.GetDebugInfo();

            //获取并返回预支付XML信息
            string result = TenPayV3.Unifiedorder(data);
            var res = XDocument.Parse(result);
            prepayId = res.Element("xml").Element("prepay_id").Value;
            return prepayId;
        }

    }
}