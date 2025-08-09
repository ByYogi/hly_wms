using House.Business.Cargo;
using House.Entity;
using House.Entity.Cargo;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
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
    public partial class AccountPayOrder : WXBasePage
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
        public string appId = string.Empty;
        public string timeStamp = string.Empty;
        public string nonceStr = string.Empty;
        public string package = string.Empty;
        public string paySign = string.Empty;
        public static string wxJsApiParam { get; set; }//微信支付JSApi参数
        //这里通过官方的一个实体，用户自行使用，我这里是直接读取的CONFIG文件
        private static TenPayV3Info tenPayV3 = new TenPayV3Info(ConfigurationManager.AppSettings["dltAPPID"], ConfigurationManager.AppSettings["dltAppSecret"], ConfigurationManager.AppSettings["dltMachID"]
                            , ConfigurationManager.AppSettings["dltWxPayKey"], ConfigurationManager.AppSettings["dltWxPayTranUrl"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            string zj = Request.QueryString["totalFee"];//总费用
            string id = Request.QueryString["id"];//待支付id集合
            if (string.IsNullOrEmpty(id)) { return; }
            if (!IsPostBack)
            {
                string orderno = GetOrderNumber();
                decimal wxZJ = Convert.ToDecimal(zj) * 100;

                ltlOrder.Text = "<div class='mg10-0 t-c'>订单号：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + orderno + "</em></span></div><div class='mg10-0 t-c'>总金额：<span class='wy-pro-pri mg-tb-5'><em class='num font-20'>" + zj + "</em>元</span></div>";
                CargoWeiXinBus bus = new CargoWeiXinBus();
                LogEntity log = new LogEntity();
                log.IPAddress = Common.GetUserIP(HttpContext.Current.Request);
                log.Moudle = "微信服务号";
                log.Status = "0";
                log.NvgPage = "微信付款";
                log.UserID = WxUserInfo.wxOpenID;
                log.Operate = "U";
                List<WXOrderEntity> orderList = new List<WXOrderEntity>();
                WriteTextLog(id);
                string[] idArr = id.Split('/');
                for (int i = 0; i < idArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(idArr[i]))
                    {
                        orderList.Add(new WXOrderEntity { ID = Convert.ToInt64(idArr[i]), AccountNo = orderno });
                    }
                }
                bus.UpdateWxOrderAccountByID(orderList, log);
                WriteTextLog("修改成功");
                string prepayID = PayInfo("", "迪乐泰", WxUserInfo.wxOpenID, wxZJ.ToString("F0"), orderno);

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
            string prepayId = string.Empty;
            RequestHandler requestHandler = new RequestHandler(HttpContext.Current);
            //微信分配的公众账号ID（企业号corpid即为此appId）
            requestHandler.SetParameter("appid", tenPayV3.AppId);
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