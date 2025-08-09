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
    public partial class OrderInfo : WXBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string orderNo = "190425112249158";// Request.QueryString["orderNo"];
            string orderNo = Request.QueryString["orderNo"];
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(orderNo))
                {
                    return;
                }
                CargoWeiXinBus bus = new CargoWeiXinBus();

                List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 5, new WXOrderEntity { OrderNo = orderNo });
                #region 全部订单
                if (result.Count > 0)
                {
                    foreach (var it in result)
                    {
                        #region 地址数据
                        ltlAddress.Text = "<div class='weui-media-box__bd'><h4 class='address-name'><span>" + it.Name + "</span><span>" + it.Cellphone + "</span></h4><div class='address-txt'>" + it.Province + "&nbsp;" + it.City + "&nbsp;" + it.Country + "&nbsp;" + it.Address + "</div></div>";
                        #endregion
                        #region 订单商品数据
                        string fk = string.Empty;
                        string jyzt = string.Empty;
                        string coo = string.Empty;
                        //if (it.PayWay.Equals("0"))
                        //{

                        //}
                        //if (it.PayStatus.Equals("1"))
                        //{
                        //    fk = "已付款";
                        //}
                        //else
                        //{
                        //    fk = "未付款";

                        //    //coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:PayMoney(" + it.OrderNo + ");' class='ords-btn-pay'>付款</a>&nbsp;&nbsp;<a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                        //    //coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                        //}
                        switch (it.OrderStatus)
                        {
                            case "0":
                                jyzt = "待确认";
                                coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>订单正在确认中，请您耐心等待....<a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                                break;
                            case "1":
                                jyzt = "已确认";
                                coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中</div>";
                                break;
                            case "2":
                                jyzt = "待收货";
                                coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                                break;
                            case "3":
                                jyzt = "待收货";
                                coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                                break;
                            case "4": jyzt = "交易成功";
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
                                //else if (pro.SaleType.Equals("3")) { saleType = "【限】"; }
                                //else if (pro.SaleType.Equals("4")) { saleType = "【积】"; }
                                //else { saleType = "【正】"; }
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
                        #endregion
                    }
                }
                #endregion
            }
        }
    }
}