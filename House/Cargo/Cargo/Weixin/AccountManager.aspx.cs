using House.Business.Cargo;
using House.Entity.Cargo;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Cargo.Weixin
{
    public partial class AccountManager : WXBasePage
    {
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
            if (!IsPostBack)
            {
                string clientNum = Convert.ToString(Request["ClientNum"]);
                if (string.IsNullOrEmpty(clientNum) || string.IsNullOrEmpty(Request["StartDate"]))
                {
                    ltlTop.Text = "非法请求！";
                    return;
                }
                if (clientNum.Equals("0"))
                {
                    ltlTop.Text = "客户不存在！";
                    return;
                }
                DateTime dt = Convert.ToDateTime(Request["StartDate"]);
                //DateTime dt = Convert.ToDateTime("2021-03-05");

                CargoOrderEntity queryEntity = new CargoOrderEntity();
                queryEntity.PayClientNum = Convert.ToInt32(clientNum);
                queryEntity.StartDate = dt.AddDays(-(dt.Day - 1)).AddMonths(-3);
                queryEntity.EndDate = dt.AddMonths(1).AddDays(-dt.Day);
                CargoOrderBus bus = new CargoOrderBus();
                List<CargoOrderEntity> result = bus.QueryOrderAccount(queryEntity);
                string Boss = string.Empty;
                string CurOrder = string.Empty;
                string CurReturn = string.Empty;
                int oP = 0, rP = 0;
                decimal oC = 0.00M, rC = 0.00M, checkC = 0.00M, unCheckC = 0.00M;
                #region 账单
                if (result.Count > 0)
                {
                    CurOrder += "<div class='content'><div class='toggle'>";
                    CurReturn += "<div class='content'><div class='toggle'>";
                    foreach (var it in result)
                    {
                        Boss = it.PayClientName;
                        string zf = "未支付";
                        if (it.CheckStatus.Equals("1")) { zf = "已结清"; checkC += it.TotalCharge; }
                        else if (it.CheckStatus.Equals("2")) { zf = "未结清"; }
                        if (it.OrderModel.Equals("0"))
                        {
                            oP += it.Piece;
                            oC += it.TotalCharge;
                            CurOrder += "<dl><dt>订单号：<em style='font-weight: bold;'>" + it.OrderNo + "</em>&nbsp;&nbsp;收货人：" + it.AcceptPeople + "&nbsp;&nbsp;业务员：" + it.SaleManName + "<br />订单数量：" + it.Piece.ToString() + "条&nbsp;&nbsp;订单金额：" + it.TotalCharge.ToString("F2") + "元<br />开单时间：" + it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<em style='color: red; font-size: 15px;'>" + zf + "</em></dt><dd><table border='1' cellspacing='0' cellpadding='0' style='table-layout: fixed; border: 1px solid rgb(221, 221, 221); width: 100%; text-align: center;font-size:11px;'><tr><td style='width:25%'>规格</td><td style='width:25%'>花纹</td><td style='width:12%'>载重</td><td style='width:12%'>周期</td><td style='width:15%'>数量</td><td style='width:16%'>单价</td></tr>";
                            foreach (var ie in it.ContainerShowEntity)
                            {
                                CurOrder += "<tr><td>" + ie.Specs + "</td><td>" + ie.Figure + "</td><td>" + ie.LoadIndex.ToString() + ie.SpeedLevel + "</td><td>" + ie.Batch + "</td><td>" + ie.Piece.ToString() + "</td><td>" + ie.ActSalePrice.ToString("F2") + "</td></tr>";
                            }
                            CurOrder += "</table></dd></dl>";
                        }
                        else if (it.OrderModel.Equals("1"))
                        {
                            rP += it.Piece;
                            rC += it.TotalCharge;
                            //退货单
                            CurReturn += "<dl><dt>退单号：<em style='font-weight: bold;'>" + it.OrderNo + "</em>&nbsp;&nbsp;收货人：" + it.AcceptPeople + "&nbsp;&nbsp;业务员：" + it.SaleManName + "<br />退单数量：" + it.Piece.ToString() + "条&nbsp;&nbsp;退单金额：" + it.TotalCharge.ToString("F2") + "元<br />退单时间：" + it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<em style='color: red; font-size: 15px;'>" + zf + "</em></dt><dd><table border='1' cellspacing='0' cellpadding='0' style='table-layout: fixed; border: 1px solid rgb(221, 221, 221); width: 100%; text-align: center;font-size:11px;'><tr><td style='width:25%'>规格</td><td style='width:25%'>花纹</td><td style='width:12%'>载重</td><td style='width:12%'>周期</td><td style='width:15%'>数量</td><td style='width:16%'>单价</td></tr>";
                            foreach (var ie in it.ContainerShowEntity)
                            {
                                CurReturn += "<tr><td>" + ie.Specs + "</td><td>" + ie.Figure + "</td><td>" + ie.LoadIndex.ToString() + ie.SpeedLevel + "</td><td>" + ie.Batch + "</td><td>" + ie.Piece.ToString() + "</td><td>" + ie.ActSalePrice.ToString("F2") + "</td></tr>";
                            }
                            CurReturn += "</table></dd></dl>";
                        }
                    }
                    CurReturn += "</div></div>";
                    CurOrder += "</div></div>";
                }
                #endregion
                //本月订单
                #region 本月订单
                ltlCurOrder.Text = CurOrder;
                ltlCurReturn.Text = CurReturn;
                unCheckC = oC - checkC - rC;
                decimal RebateMoney = 0.00M;
                CargoClientBus client = new CargoClientBus();
                Hashtable ht = client.QueryPreRecord(1, 10000, new CargoClientPreRecordEntity { ClientNum = queryEntity.PayClientNum });
                List<CargoClientPreRecordEntity> preR = ht["rows"] as List<CargoClientPreRecordEntity>;
                string rebStr = string.Empty;
                if (preR.Count > 0)
                {
                    RebateMoney = preR[0].RebateMoney;
                    rebStr = "<div class='weui-cells' style='margin:0px;font-size:11px;'>";
                    foreach (var ri in preR)
                    {
                        if (ri.YBMoney > 0)
                        {
                            rebStr += "<div class='weui-cell'><div class='weui-cell__bd'><p>" + ri.RebateMonth + "月延保返利</p></div>  <div class='weui-cell__ft'>" + ri.YBMoney.ToString("F2") + "元</div></div>";
                        }
                        if (ri.BTMoney > 0)
                        {
                            rebStr += "<div class='weui-cell'><div class='weui-cell__bd'><p>" + ri.RebateMonth + "月补贴返利</p></div>  <div class='weui-cell__ft'>" + ri.BTMoney.ToString("F2") + "元</div></div>";
                        }
                        if (ri.HPMoney > 0)
                        {
                            rebStr += "<div class='weui-cell'><div class='weui-cell__bd'><p>" + ri.RebateMonth + "月好评返利</p></div>  <div class='weui-cell__ft'>" + ri.HPMoney.ToString("F2") + "元</div></div>";
                        }
                        if (ri.ROSSMoney > 0)
                        {
                            rebStr += "<div class='weui-cell'><div class='weui-cell__bd'><p>" + ri.RebateMonth + "月ROSS返利</p></div>  <div class='weui-cell__ft'>" + ri.ROSSMoney.ToString("F2") + "元</div></div>";
                        }
                    }
                    rebStr += "</div>";

                }
                #endregion

                ltlRebate.Text = rebStr;
                ltlTop.Text = "<div class=\"weui-media-box__bd\"><h4 class=\"weui-media-box__title user-name\" style=\"font-size: 22px; margin-top: -10px;\">" + Boss + "&nbsp;" + queryEntity.EndDate.ToString("yyyy-MM") + "月对账单</h4><p class=\"user-grade\">未付订单：" + oC.ToString("F2") + "元-----" + oP + "条</p><p class=\"user-grade\">本月退货：" + rC.ToString("F2") + "元-----" + rP + "条</p><p class=\"user-grade\">剩余返利：" + RebateMoney.ToString("F2") + "元</p><p class=\"user-integral\">已支付：<em class=\"num\">" + checkC.ToString("F2") + "</em>元</p><p class=\"user-integral\">待支付：<em class=\"num\">" + unCheckC.ToString("F2") + "</em>元</p></div>";

                if (WxUserInfo.HouseID.Equals(3) && WxUserInfo.PushAccount.Equals("1"))
                {
                    //ltlTop.Text = "<div class=\"weui-media-box__bd\"><h4 class=\"weui-media-box__title user-name\" style=\"font-size: 22px; margin-top: -10px;\">" + Boss + "&nbsp;" + queryEntity.EndDate.ToString("yyyy-MM") + "月对账单</h4><p class=\"user-grade\">未付订单：" + oC.ToString("F2") + "元-----" + oP + "条</p><p class=\"user-grade\">本月退货：" + rC.ToString("F2") + "元-----" + rP + "条</p><p class=\"user-grade\">剩余返利：" + RebateMoney.ToString("F2") + "元</p><p class=\"user-integral\">已支付：<em class=\"num\">" + checkC.ToString("F2") + "</em>元</p><p class=\"user-integral\">待支付：<em class=\"num\">" + unCheckC.ToString("F2") + "</em>元&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:callpay();\" style=\"position: relative;margin-left: auto;margin-right: auto;padding-left: 14px;padding-right: 14px;box-sizing: border-box;font-size: 18px;text-align: center;text-decoration: none;color: #fff;border-radius: 5px;background-color: #1aad19;\">微信支付</a></p></div>";
                    //生成微信支付
                    string orderno = GetOrderNumber();
                    decimal wxZJ = Convert.ToDecimal(oC.ToString("F2")) * 100;

                    //bus.UpdateWxOrderAccountByID(orderList, log);
                    //WriteTextLog("修改成功");
                    string prepayID = PayInfo("2", "迪乐泰", WxUserInfo.wxOpenID, wxZJ.ToString("F0"), orderno);
                    //string prepayID = string.Empty;

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