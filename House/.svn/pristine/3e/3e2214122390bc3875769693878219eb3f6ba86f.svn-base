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
    public partial class ScanQrPayOrder : WXBasePage
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
        public string Lm = "0";
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
            LogEntity log = new LogEntity();
            log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
            log.Moudle = "微信服务号";
            log.Status = "0";
            log.NvgPage = "微信付款";
            log.UserID = WxUserInfo.wxOpenID;
            log.Operate = "A";
            appId = Common.GetdltAPPID();

            string OrderNo = Convert.ToString(Request.QueryString["OrderNo"]);
            if (string.IsNullOrEmpty(OrderNo))
            {
                //订单不存在，请核对
                Response.Write("<script>alert('订单号为空');</script>");
                return;
            }
            CargoOrderBus b = new CargoOrderBus();
            if (!IsPostBack)
            {
                CargoOrderEntity orderEnt = b.QueryOrderInfo(new CargoOrderEntity { OrderNo = OrderNo });
                if (orderEnt.OrderID.Equals(0))
                {
                    //订单不存在，请核对
                    Response.Write("<script>alert('订单不存在，请核对');</script>");
                    return;
                }
                string CheckStatus = "待支付";
                if (orderEnt.CheckStatus.Equals("1")) { CheckStatus = "已结清"; Lm = "1"; } else if (orderEnt.CheckStatus.Equals("2")) { CheckStatus = "未结清"; }
                //前端显示 
                ltlCharge.Text = "<p class=\"weui-pay-num\">￥" + orderEnt.TotalCharge.ToString("F2") + "</p>";
                ltlOrder.Text = "<li style=\"margin-bottom:5px;\"><span class=\"title\" style=\"font-size:15px;\">订单号码：</span><span class=\"content\" style=\"font-size:15px;\">" + orderEnt.OrderNo + "</span></li><li style=\"margin-bottom:5px;\"><span class=\"title\" style=\"font-size:15px;\">收货信息：</span><span class=\"content\" style=\"font-size:15px;\">" + orderEnt.AcceptPeople + "&nbsp;&nbsp;" + orderEnt.AcceptCellphone + "</span></li><li style=\"margin-bottom:5px;\"><span class=\"title\" style=\"font-size:15px;\">订单数量：</span><span class=\"content\" style=\"font-size:15px;\">" + orderEnt.Piece.ToString() + "&nbsp;条</span></li><li style=\"margin-bottom:5px;\"><span class=\"title\" style=\"font-size:15px;\">支付状态：</span><span class=\"content\" style=\"font-size:15px;\">" + CheckStatus + "</span></li>";
                if (Lm.Equals("0"))
                {
                    CargoWeiXinBus wbus = new CargoWeiXinBus();
                    //WXUserEntity wxUser = wbus.QueryWeixinUserByOpendID(new WXUserEntity { wxOpenID = WxUserInfo.wxOpenID });
                    //WriteTextLog("开始支付");
                    //生成预支付订单ID
                    string orderno = GetOrderNumber();

                    //zj = "1";
                    //WriteTextLog(logicname + "|" + memo);
                    decimal wxZJ = orderEnt.TotalCharge * 100;

                    WriteTextLog(orderno);
                    string prepayID = PayInfo("1", "迪乐泰轮胎", WxUserInfo.wxOpenID, wxZJ.ToString("F0"), orderno);
                    //string prepayID = "123";
                    WriteTextLog(prepayID);
                    //推送给客户的所属业务员消息
                    //WXUserAddressEntity clerkEntity = wbus.QueryWxAreaClient(new WXUserAddressEntity { Province = addressEnt.Province, City = addressEnt.City });
                    //WriteTextLog(prepayID);
                    WXOrderEntity wxOrder = new WXOrderEntity();
                    if (!string.IsNullOrEmpty(orderEnt.WXOrderNo))
                    {
                        wxOrder = wbus.QueryWeixinOrderByOrderNo(new WXOrderEntity { OrderNo = orderEnt.WXOrderNo });
                    }
                    if (!wxOrder.ID.Equals(0))
                    {
                        wbus.DeleteWeixinOrder(new WXOrderEntity { ID = wxOrder.ID, PayWay = wxOrder.PayWay }, log);
                    }
                    wbus.SaveWeixinOrder(new WXOrderEntity
                    {
                        OrderNo = orderno,
                        TotalCharge = orderEnt.TotalCharge,
                        TransitFee = 0,
                        WXID = WxUserInfo.ID,
                        PayStatus = "0",
                        OrderStatus = "1",
                        PayWay = "0",
                        OrderType = "4",//微信商城下单
                        Piece = orderEnt.Piece,
                        Address = orderEnt.AcceptAddress,
                        Cellphone = orderEnt.AcceptCellphone,
                        City = "",
                        Province = "",
                        Country = "",
                        Name = orderEnt.AcceptPeople,
                        HouseID = orderEnt.HouseID,
                        SaleManID = orderEnt.SaleManID,
                        Memo = "",
                        LogisID = orderEnt.LogisID,
                        LogicName = orderEnt.LogisticName,
                        productList = new List<CargoProductShelvesEntity>()
                    }, log);
                    //修改微信订单的微信支付关联单号
                    b.UpdateOrderWxNoByOrderNo(new CargoOrderEntity { OrderNo = OrderNo, WXOrderNo = orderno }, log);
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
                }
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