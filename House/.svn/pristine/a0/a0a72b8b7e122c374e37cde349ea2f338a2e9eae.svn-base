using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class myAccount : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoWeiXinBus bus = new CargoWeiXinBus();

                List<WXOrderManagerEntity> result = bus.QueryWxOrderInfo(new WXOrderManagerEntity { WXID = WxUserInfo.ID });
                //未付款
                List<WXOrderManagerEntity> noCheck = result.Where(c => c.CheckStatus.Equals("0")).ToList();
                #region 未结账
                if (noCheck.Count > 0)
                {
                    string noCheckStr = string.Empty;
                    foreach (var it in noCheck)
                    {
                        string payw = "微信付款";
                        if (it.PayWay.Equals("1"))
                        {
                            payw = "额度付款";
                        }
                        else if (it.PayWay.Equals("2"))
                        {
                            payw = "积分兑换";
                        }
                        noCheckStr += "<div class='weui-panel weui-panel_access'><div class='weui-panel__bd'><div class='weui-media-box_appmsg'><div class='weui-media-box__hd check-w weui-cells_checkbox' style='height:62px;'><label class='weui-check__label' for='cart-pto" + it.ID + "'><div class='weui-cell__hd cat-check' style='margin-top:20px;'><input type='checkbox' class='weui-check' name='cartpro' id='cart-pto" + it.ID + "' data-p='" + it.TotalCharge + "' data-id='" + it.ID + "' /><i class='weui-icon-checked'></i></div></label></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='OrderInfo.aspx?orderNo=" + it.WXOrderNo + "' class='ord-pro-link'>订单号：" + it.WXOrderNo + "，订单时间：" + it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "</a></h1><p class='weui-media-box__desc'>数量：<span>" + it.Piece + "</span>条，总金额：<span>" + it.TotalCharge.ToString("F2") + "元</span>，支付方式：<span>" + payw + "</span></p></div></div></div></div>";
                    }
                    noCheckStr += "<div class='foot-black'></div>";
                    ltlUnPay.Text = noCheckStr;
                }
                #endregion

                List<WXOrderManagerEntity> Check = result.Where(c => c.CheckStatus.Equals("1")).ToList();
                #region 已结账
                if (Check.Count > 0)
                {
                    string CheckStr = string.Empty;
                    foreach (var it in Check)
                    {
                        string payw = "微信付款";
                        if (it.PayWay.Equals("1")) { payw = "额度付款"; }
                        else if (it.PayWay.Equals("2"))
                        {
                            payw = "积分兑换";
                        }
                        CheckStr += "<div class='weui-panel weui-panel_access'><div class='weui-panel__bd' style='margin-top: 5px;margin-bottom: 5px;margin-left:5px;'><div class='weui-media-box_appmsg'><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='OrderInfo.aspx?orderNo=" + it.WXOrderNo + "' class='ord-pro-link'>订单号：" + it.WXOrderNo + "，订单时间：" + it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "</a></h1><p class='weui-media-box__desc'>数量：<span>" + it.Piece + "</span>条，总金额：<span>" + it.TotalCharge.ToString("F2") + "元</span>，支付方式：<span>" + payw + "</span></p></div></div></div></div>";
                    }
                    CheckStr += "<div class='foot-black'></div>";
                    ltlPay.Text = CheckStr;
                }
                #endregion

                if (result.Count > 0)
                {
                    string all = string.Empty;
                    foreach (var it in result)
                    {
                        string payw = "微信付款";
                        if (it.PayWay.Equals("1"))
                        {
                            if (it.CheckStatus.Equals("0"))
                            {
                                payw = "<span style='color:red;'>额度付款（未结算）</span>";
                            }
                            else
                            {
                                payw = "额度付款（已结算）";

                            }
                        }
                        else if (it.PayWay.Equals("0"))
                        {
                            if (it.CheckStatus.Equals("0"))
                            {
                                payw = "<span style='color:red;'>微信付款（未结算）</span>";
                            }
                            else
                            {
                                payw = "微信付款（已结算）";

                            }
                        }
                        all += "<div class='weui-panel weui-panel_access'><div class='weui-panel__bd' style='margin-top: 5px;margin-bottom: 5px;margin-left:5px;'><div class='weui-media-box_appmsg'><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='OrderInfo.aspx?orderNo=" + it.WXOrderNo + "' class='ord-pro-link'>订单号：" + it.WXOrderNo + "，订单时间：" + it.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "</a></h1><p class='weui-media-box__desc'>数量：<span>" + it.Piece + "</span>条，总金额：<span>" + it.TotalCharge.ToString("F2") + "元</span>，支付方式：" + payw + "</p></div></div></div></div>";
                    }
                    all += "<div class='foot-black'></div>";
                    ltlAllOrder.Text = all;
                }
            }
        }
    }
}