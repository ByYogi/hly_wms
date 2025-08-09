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
    public partial class OnlyMyOrder : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoWeiXinBus bus = new CargoWeiXinBus();
                WXTaobaoEntity entity = new WXTaobaoEntity();
                string QTY = Convert.ToString(Request["qty"]);
                int rate = 0;
                if (QTY.Equals("0"))
                {
                    //查询的的订单
                    entity.ID = WxUserInfo.ID;
                    rate = 4;
                }
                else if (QTY.Equals("1"))
                {
                    //查询的下级订单
                    entity.ParentID = WxUserInfo.ID;
                    rate = 10;
                }
                List<WXTaobaoEntity> tao = bus.QueryTaobaoOrderInfo(entity);
                ltlUnPay.Text = "无订单数据";
                #region 所有订单
                if (tao.Count > 0)
                {
                    string unpay = string.Empty;
                    foreach (var it in tao)
                    {
                        string jyzt = string.Empty;
                        string fl = string.Empty;
                        if (it.payment.Equals(0))
                        {
                            fl = "0";
                            it.status = "请等待订单导入...";
                        }
                        else
                        {
                            fl = (it.num * rate).ToString();
                        }
                        unpay += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>淘宝订单号：" + it.TaobaoID + "</span><span class='ord-status-txt-ts fr' style='color:red;font-weight:bolder;'>返利：" + fl + "元</span></div><div class='weui-media-box__bd  pd-5'><div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd' style='width:5px;height:50px'><img class='weui-media-box__thumb' src='" + it.pic_path + "' alt=''></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'>买家信息：" + it.buyer_nick + "，" + it.receiver_mobile + "，" + it.receiver_address + "</h1><p class='weui-media-box__desc'>宝贝数量：<span>" + it.num.ToString() + "&nbsp;瓶</span>&nbsp;&nbsp;<span>实际支付：" + it.payment.ToString() + "&nbsp;元</span></p></div></div><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>" + it.status + "</div></div></div>";
                    }
                    ltlUnPay.Text = unpay;

                }
                #endregion
                //List<WXTaobaoEntity> result = tao.Where(c => c.status == "0").ToList();
                //#region 未付款订单
                //if (result.Count > 0)
                //{
                //    string unpay = string.Empty;
                //    foreach (var it in result)
                //    {
                //        string jyzt = string.Empty;
                //        string fl = string.Empty;
                //        switch (it.status)
                //        {
                //            case "0": jyzt = "未付款"; break;
                //            case "1": jyzt = "已付款"; break;
                //            case "2": jyzt = "已签收"; break;
                //            case "3": jyzt = "异常"; break;
                //            default:
                //                break;
                //        }
                //        //fl = (Convert.ToDouble(it.payment) * 0.2).ToString("F2");
                //        fl = (it.num * rate).ToString();
                //        unpay += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>淘宝订单号：" + it.TaobaoID + "</span><span class='ord-status-txt-ts fr'>" + jyzt + "</span></div><div class='weui-media-box__bd  pd-5'><div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd' style='width:50px;height:50px'><img class='weui-media-box__thumb' src='" + it.pic_path + "' alt=''></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'>买家信息：" + it.buyer_nick + "，" + it.receiver_mobile + "，" + it.receiver_address + "</h1><p class='weui-media-box__desc'>数量：<span>" + it.num.ToString() + "</span>&nbsp;<span>单价：" + it.price.ToString() + "</span>&nbsp;<span style='color:red;'>实际支付：" + it.payment.ToString() + "</span>&nbsp;<span style='color:red;'>返利：" + fl + "</span></p></div></div></div></div>";
                //    }
                //    ltlUnPay.Text = unpay;
                //}
                //#endregion
                ////已付款
                //List<WXTaobaoEntity> PayResult = tao.Where(c => c.status == "1").ToList();
                //#region 已付款订单
                //if (PayResult.Count > 0)
                //{
                //    string pay = string.Empty;
                //    foreach (var it in PayResult)
                //    {
                //        string jyzt = string.Empty;
                //        string fl = string.Empty;
                //        switch (it.status)
                //        {
                //            case "0": jyzt = "未付款"; break;
                //            case "1": jyzt = "已付款"; break;
                //            case "2": jyzt = "已签收"; break;
                //            case "3": jyzt = "异常"; break;
                //            default:
                //                break;
                //        }
                //        //fl = (Convert.ToDouble(it.payment) * 0.2).ToString("F2");
                //        fl = (it.num * rate).ToString();
                //        pay += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>淘宝订单号：" + it.TaobaoID + "</span><span class='ord-status-txt-ts fr'>" + jyzt + "</span></div><div class='weui-media-box__bd  pd-5'><div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd' style='width:50px;height:50px'><img class='weui-media-box__thumb' src='" + it.pic_path + "' alt=''></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'>买家信息：" + it.buyer_nick + "，" + it.receiver_mobile + "，" + it.receiver_address + "</h1><p class='weui-media-box__desc'>数量：<span>" + it.num.ToString() + "</span>&nbsp;<span>单价：" + it.price.ToString() + "</span>&nbsp;<span style='color:red;'>实际支付：" + it.payment.ToString() + "</span>&nbsp;<span style='color:red;'>返利：" + fl + "</span></p></div></div></div></div>";
                //    }
                //    ltlPay.Text = pay;
                //}
                //#endregion
                ////已签收
                //List<WXTaobaoEntity> signResult = tao.Where(c => c.status == "2").ToList();
                //#region 已签收订单
                //if (signResult.Count > 0)
                //{
                //    string sign = string.Empty;
                //    foreach (var it in signResult)
                //    {
                //        string jyzt = string.Empty;
                //        string fl = string.Empty;
                //        switch (it.status)
                //        {
                //            case "0": jyzt = "未付款"; break;
                //            case "1": jyzt = "已付款"; break;
                //            case "2": jyzt = "已签收"; break;
                //            case "3": jyzt = "异常"; break;
                //            default:
                //                break;
                //        }
                //        //fl = (Convert.ToDouble(it.payment) * 0.2).ToString("F2");
                //        fl = (it.num * rate).ToString();
                //        sign += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>淘宝订单号：" + it.TaobaoID + "</span><span class='ord-status-txt-ts fr'>" + jyzt + "</span></div><div class='weui-media-box__bd  pd-5'><div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd' style='width:50px;height:50px'><img class='weui-media-box__thumb' src='" + it.pic_path + "' alt=''></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'>买家信息：" + it.buyer_nick + "，" + it.receiver_mobile + "，" + it.receiver_address + "</h1><p class='weui-media-box__desc'>数量：<span>" + it.num.ToString() + "</span>&nbsp;<span>单价：" + it.price.ToString() + "</span>&nbsp;<span style='color:red;'>实际支付：" + it.payment.ToString() + "</span>&nbsp;<span style='color:red;'>返利：" + fl + "</span></p></div></div></div></div>";
                //    }
                //    ltlSign.Text = sign;
                //}
                //#endregion
                ////异常ltlUnAccept 
                //List<WXTaobaoEntity> abnormalResult = tao.Where(c => c.status == "3").ToList();
                //#region 异常订单
                //if (abnormalResult.Count > 0)
                //{
                //    string abn = string.Empty;
                //    foreach (var it in abnormalResult)
                //    {
                //        string jyzt = string.Empty;
                //        string fl = string.Empty;
                //        switch (it.status)
                //        {
                //            case "0": jyzt = "未付款"; break;
                //            case "1": jyzt = "已付款"; break;
                //            case "2": jyzt = "已签收"; break;
                //            case "3": jyzt = "异常"; break;
                //            default:
                //                break;
                //        }
                //        //fl = (Convert.ToDouble(it.payment) * 0.2).ToString("F2");
                //        fl = (it.num * rate).ToString();
                //        abn += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>淘宝订单号：" + it.TaobaoID + "</span><span class='ord-status-txt-ts fr'>" + jyzt + "</span></div><div class='weui-media-box__bd  pd-5'><div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd' style='width:50px;height:50px'><img class='weui-media-box__thumb' src='" + it.pic_path + "' alt=''></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'>买家信息：" + it.buyer_nick + "，" + it.receiver_mobile + "，" + it.receiver_address + "</h1><p class='weui-media-box__desc'>数量：<span>" + it.num.ToString() + "</span>&nbsp;<span>单价：" + it.price.ToString() + "</span>&nbsp;<span style='color:red;'>实际支付：" + it.payment.ToString() + "</span>&nbsp;<span style='color:red;'>返利：" + fl + "</span></p></div></div></div></div>";
                //    }
                //    ltlAbnormal.Text = abn;
                //}
                //#endregion
            }
        }
    }
}