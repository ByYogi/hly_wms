using House.Business.Cargo;
using House.Entity.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.Weixin
{
    public partial class myOrder : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoWeiXinBus bus = new CargoWeiXinBus();
                CargoOrderBus orderbus = new CargoOrderBus();
                //List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 300, new WXOrderEntity { WXID = WxUserInfo.ID, CreateDate = DateTime.Now.AddDays(-DateTime.Now.DayOfYear) });
                List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 300, new WXOrderEntity { WXID = WxUserInfo.ID, CreateDate = DateTime.Now.AddDays(-30) });
                List<WXOrderEntity> computerOrder = new List<WXOrderEntity>();
                computerOrder = orderbus.QueryMyOrderInfoForAPP(new WXOrderEntity { ClientNum = WxUserInfo.ClientNum, OrderStatus = "2", CreateDate = DateTime.Now.AddDays(-30) });
                result.AddRange(computerOrder);
                #region 全部订单
                if (result.Count > 0)
                {
                    foreach (var it in result)
                    {
                        string jyzt = string.Empty;
                        string coo = string.Empty;
                        //if (it.PayStatus.Equals("1"))
                        //{
                        //    jyzt = "已付款";
                        //    switch (it.OrderStatus)
                        //    {
                        //        case "0":
                        //            jyzt = "待发货";
                        //            coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中，请您耐心等待....</div>";
                        //            break;
                        //        case "1":
                        //            jyzt = "待发货";
                        //            coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中，请您耐心等待....</div>";
                        //            break;
                        //        case "2":
                        //            jyzt = "待收货";
                        //            coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                        //            break;
                        //        case "3":
                        //            jyzt = "待收货";
                        //            coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                        //            break;
                        //        case "4": jyzt = "交易成功";
                        //            break;
                        //        default: break;
                        //    }
                        //}
                        //else
                        //{
                        //    jyzt = "待确认";
                        //    coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:PayMoney(" + it.OrderNo + ");' class='ords-btn-pay'>付款</a>&nbsp;&nbsp;<a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                        //    //coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                        //}

                        switch (it.OrderStatus)
                        {
                            case "0":
                                jyzt = "待确认";
                                if (it.PayStatus.Equals("1"))
                                {
                                    coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                                }
                                else
                                {
                                    coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:PayMoney(" + it.OrderNo + ");' class='ords-btn-pay'>付款</a>&nbsp;&nbsp;<a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                                }
                                break;
                            case "1":
                                jyzt = "待发货";
                                coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中，请您耐心等待....</div>";
                                break;
                            case "2":
                                jyzt = "待收货";
                                coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                                break;
                            case "3":
                                jyzt = "待收货";
                                coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                                break;
                            case "4":
                                jyzt = "交易成功";
                                break;
                            default: break;
                        }
                        ltlAllOrder.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>单号：" + it.OrderNo + "</span><span class='ord-status-txt-ts fr'>" + jyzt + "</span></div><div class='weui-media-box__bd  pd-10'>";
                        if (it.productList.Count > 0)
                        {
                            foreach (var pro in it.productList)
                            {
                                //string saleType = string.Empty;
                                //if (pro.SaleType.Equals("1")) { saleType = "【天】"; }
                                //else if (pro.SaleType.Equals("3"))
                                //{
                                //    saleType = "【限】";
                                //}
                                //else
                                //{
                                //    saleType = "【积】";
                                //}
                                ltlAllOrder.Text += "<div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "'><img class='weui-media-box__thumb' src='" + pro.FileName + "' alt=''></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "' class='ord-pro-link'>" + pro.Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + pro.Specs + "</span>，<span>" + pro.HubDiameter.ToString() + "寸</span></p><div class='clear mg-t-3'><div class='wy-pro-pri fl'>¥<em class='num font-15'>" + pro.OrderPrice.ToString() + "</em></div><div class='pro-amount fr'><span class='font-13'>数量×<em class='name'>" + pro.OrderNum.ToString() + "</em></span></div></div></div></div>";
                            }
                        }
                        string OutHouseName = string.Empty;
                        if (!string.IsNullOrEmpty(it.OutHouseName))
                        {
                            OutHouseName = "出库：" + it.OutHouseName + "&nbsp;&nbsp;";
                        }
                        ltlAllOrder.Text += "</div><div class='ord-statistics'>" + OutHouseName + "<span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'>";
                        ltlAllOrder.Text += coo + "</div></div>";
                        //<a href='comment.html' class='ords-btn-com'>评价</a>
                    }
                }
                #endregion
                //待付款
                //List<WXOrderEntity> unPayResult = bus.QueryWeixinOrderInfo(1, 3000, new WXOrderEntity { WXID = WxUserInfo.ID, PayStatus = "0" });
                List<WXOrderEntity> unPayResult = result.Where(c => c.OrderStatus.Equals("0")).ToList();
                #region 待付款订单
                if (unPayResult.Count > 0)
                {
                    foreach (var it in unPayResult)
                    {

                        ltlUnPay.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>单号：" + it.OrderNo + "</span><span class='ord-status-txt-ts fr'>待确认</span></div><div class='weui-media-box__bd  pd-10'>";
                        if (it.productList.Count > 0)
                        {
                            foreach (var pro in it.productList)
                            {
                                //string saleType = string.Empty;
                                //if (pro.SaleType.Equals("1")) { saleType = "【特价】"; }
                                //else if (pro.SaleType.Equals("3"))
                                //{
                                //    if (pro.TypeID.Equals(34))
                                //    {
                                //        saleType = "【618马牌促销】";
                                //    }
                                //    else
                                //    {
                                //        saleType = "【特价】";
                                //    }
                                //}
                                ltlUnPay.Text += "<div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "'><img class='weui-media-box__thumb' src='" + pro.FileName + "' alt=''></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "' class='ord-pro-link'>" + pro.Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + pro.Specs + "</span>，<span>" + pro.HubDiameter.ToString() + "寸</span></p><div class='clear mg-t-3'><div class='wy-pro-pri fl'>¥<em class='num font-15'>" + pro.OrderPrice.ToString() + "</em></div><div class='pro-amount fr'><span class='font-13'>数量×<em class='name'>" + pro.OrderNum.ToString() + "</em></span></div></div></div></div>";
                            }
                        }
                        if (it.PayStatus.Equals("1"))
                        {
                            ltlUnPay.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div></div></div>";
                        }
                        else
                        {
                            ltlUnPay.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:PayMoney(" + it.OrderNo + ");' class='ords-btn-pay'>付款</a>&nbsp;&nbsp;<a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div></div></div>";
                        }
                        //ltlUnPay.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div></div></div>";
                        //<a href='comment.html' class='ords-btn-com'>评价</a>
                    }
                }
                #endregion
                //待发货
                //List<WXOrderEntity> unSendResult = bus.QueryWeixinOrderInfo(1, 3000, new WXOrderEntity { WXID = WxUserInfo.ID, PayStatus = "1", OrderStatus = "1" });
                List<WXOrderEntity> unSendResult = result.Where(c => c.OrderStatus.Equals("1")).ToList();
                #region 待发货订单
                if (unSendResult.Count > 0)
                {
                    foreach (var it in unSendResult)
                    {
                        ltlUnSend.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>单号：" + it.OrderNo + "</span><span class='ord-status-txt-ts fr'>待发货</span></div><div class='weui-media-box__bd  pd-10'>";
                        if (it.productList.Count > 0)
                        {
                            foreach (var pro in it.productList)
                            {
                                //string saleType = string.Empty;
                                //if (pro.SaleType.Equals("1")) { saleType = "【特价】"; }
                                //else if (pro.SaleType.Equals("3"))
                                //{
                                //    if (pro.TypeID.Equals(34))
                                //    {
                                //        saleType = "【618马牌促销】";
                                //    }
                                //    else
                                //    {
                                //        saleType = "【特价】";
                                //    }
                                //}
                                ltlUnSend.Text += "<div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "'><img class='weui-media-box__thumb' src='" + pro.FileName + "' alt=''></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "' class='ord-pro-link'>" + pro.Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + pro.Specs + "</span>，<span>" + pro.HubDiameter.ToString() + "寸</span></p><div class='clear mg-t-3'><div class='wy-pro-pri fl'>¥<em class='num font-15'>" + pro.OrderPrice.ToString() + "</em></div><div class='pro-amount fr'><span class='font-13'>数量×<em class='name'>" + pro.OrderNum.ToString() + "</em></span></div></div></div></div>";
                            }
                        }
                        ltlUnSend.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中，请您耐心等待....</div></div></div>";
                        //<a href='comment.html' class='ords-btn-com'>评价</a>
                    }
                }
                #endregion
                //待收货ltlUnAccept 
                //List<WXOrderEntity> unAcceptResult = bus.QueryWeixinOrderInfo(1, 3000, new WXOrderEntity { WXID = WxUserInfo.ID, PayStatus = "1", OrderStatus = "2" });
                List<WXOrderEntity> unAcceptResult = result.Where(c => c.OrderStatus.Equals("2")).ToList();
                #region 待收货订单
                if (unAcceptResult.Count > 0)
                {
                    foreach (var it in unAcceptResult)
                    {
                        ltlUnAccept.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>单号：" + it.OrderNo + "</span><span class='ord-status-txt-ts fr'>待收货</span></div><div class='weui-media-box__bd  pd-10'>";
                        if (it.productList.Count > 0)
                        {
                            foreach (var pro in it.productList)
                            {
                                //string saleType = string.Empty;
                                //if (pro.SaleType.Equals("1")) { saleType = "【特价】"; }
                                //else if (pro.SaleType.Equals("3"))
                                //{
                                //    if (pro.TypeID.Equals(34))
                                //    {
                                //        saleType = "【618马牌促销】";
                                //    }
                                //    else
                                //    {
                                //        saleType = "【特价】";
                                //    }
                                //}
                                ltlUnAccept.Text += "<div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "'><img class='weui-media-box__thumb' src='" + pro.FileName + "' alt=''></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "' class='ord-pro-link'>" + pro.Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + pro.Specs + "</span>，<span>" + pro.HubDiameter.ToString() + "寸</span></p><div class='clear mg-t-3'><div class='wy-pro-pri fl'>¥<em class='num font-15'>" + pro.OrderPrice.ToString() + "</em></div><div class='pro-amount fr'><span class='font-13'>数量×<em class='name'>" + pro.OrderNum.ToString() + "</em></span></div></div></div></div>";
                            }
                        }
                        ltlUnAccept.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(" + it.OrderNo + ");' class='ords-btn-com receipt'>确认收货</a></div></div></div>";
                        //<a href='comment.html' class='ords-btn-com'>评价</a>
                    }
                }
                #endregion
            }
        }
    }
}