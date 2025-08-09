using House.Business.Cargo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cargo.QY
{
    public partial class qyLogicTrack : QYBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargoOrderBus bus = new CargoOrderBus();

                Hashtable list = bus.QueryOrderInfo(1, 200, new global::House.Entity.Cargo.CargoOrderEntity { CargoPermisID = QyUserInfo.HouseID.ToString(), AwbStatus = "0", OutHouseName = QyUserInfo.HeadHouseName, StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now });
                List<global::House.Entity.Cargo.CargoOrderEntity> UnPick = list["rows"] as List<global::House.Entity.Cargo.CargoOrderEntity>;
                #region 待拣货订单
                //if (UnPick.Count > 0)
                //{
                //    foreach (var it in UnPick)
                //    {

                //        ltlUnPick.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd' style='font-size:15px;color:black;'><span>订单号：" + it.OrderNo + "&nbsp;&nbsp;" + it.Dep + "&nbsp;&nbsp;" + it.Dest + "&nbsp;&nbsp;物流公司：" + it.LogisticName + "</span><br/><span>收货人：" + it.AcceptPeople + "&nbsp;&nbsp;" + it.AcceptCellphone + "&nbsp;&nbsp;" + it.AcceptAddress + "</span><br/><span>条数：" + it.Piece.ToString() + "条&nbsp;&nbsp;业务员：" + it.SaleManName + "</span><a href='javascript:PickOrder(" + it.OrderID.ToString() + ");' class='ords-btn-pay'>已拣货</a></div></div>";
                //    }
                //}
                #endregion

                //待出库
                //Hashtable outlist = bus.QueryOrderInfo(1, 200, new global::House.Entity.Cargo.CargoOrderEntity { CargoPermisID = QyUserInfo.HouseID.ToString(), AwbStatus = "6" });
                //List<global::House.Entity.Cargo.CargoOrderEntity> Out = outlist["rows"] as List<global::House.Entity.Cargo.CargoOrderEntity>;
                //#region 待出库订单
                //if (Out.Count > 0)
                //{
                //    foreach (var it in Out)
                //    {

                //        ltlUnOut.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd' style='font-size:15px;color:black;'><span>订单号：" + it.OrderNo + "&nbsp;&nbsp;" + it.Dep + "&nbsp;&nbsp;" + it.Dest + "&nbsp;&nbsp;物流公司：" + it.LogisticName + "</span><br/><span>收货人：" + it.AcceptPeople + "&nbsp;&nbsp;" + it.AcceptCellphone + "&nbsp;&nbsp;" + it.AcceptAddress + "</span><br/><span>条数：" + it.Piece.ToString() + "条&nbsp;&nbsp;业务员：" + it.SaleManName + "</span><a href='javascript:OutOrder(" + it.OrderID.ToString() + ");' class='ords-btn-pay'>已出库</a></div></div>";
                //    }
                //}
                //#endregion

                //待装车
                Hashtable arrivelist = bus.QueryOrderInfo(1, 200, new global::House.Entity.Cargo.CargoOrderEntity { CargoPermisID = QyUserInfo.HouseID.ToString(), AwbStatus = "2", OutHouseName = QyUserInfo.HeadHouseName, StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now });
                List<global::House.Entity.Cargo.CargoOrderEntity> arrive = arrivelist["rows"] as List<global::House.Entity.Cargo.CargoOrderEntity>;
                arrive.AddRange(UnPick);

                #region 待到达订单
                if (arrive.Count > 0)
                {
                    foreach (var it in arrive)
                    {

                        ltlUnArrive.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd' style='font-size:15px;color:black;'><span>订单号：" + it.OrderNo + "&nbsp;&nbsp;" + it.Dep + "&nbsp;&nbsp;" + it.Dest + "&nbsp;&nbsp;物流公司：" + it.LogisticName + "</span><br/><span>收货人：" + it.AcceptPeople + "&nbsp;&nbsp;" + it.AcceptCellphone + "&nbsp;&nbsp;" + it.AcceptAddress + "</span><br/><span>条数：" + it.Piece.ToString() + "条&nbsp;&nbsp;业务员：" + it.SaleManName + "</span><a href='javascript:SignOrder(" + it.OrderID.ToString() + ");' class='ords-btn-pay'>上传签收</a></div></div>";
                    }
                }
                #endregion

                //待签收
                Hashtable signlist = bus.QueryOrderInfo(1, 200, new global::House.Entity.Cargo.CargoOrderEntity { CargoPermisID = QyUserInfo.HouseID.ToString(), AwbStatus = "5", OutHouseName = QyUserInfo.HeadHouseName, StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now });
                List<global::House.Entity.Cargo.CargoOrderEntity> sign = signlist["rows"] as List<global::House.Entity.Cargo.CargoOrderEntity>;
                #region 待签收订单
                if (sign.Count > 0)
                {
                    foreach (var it in sign)
                    {

                        ltlUnSign.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd' style='font-size:15px;color:black;'><span>订单号：" + it.OrderNo + "&nbsp;&nbsp;" + it.Dep + "&nbsp;&nbsp;" + it.Dest + "&nbsp;&nbsp;物流公司：" + it.LogisticName + "</span><br/><span>收货人：" + it.AcceptPeople + "&nbsp;&nbsp;" + it.AcceptCellphone + "&nbsp;&nbsp;" + it.AcceptAddress + "</span><br/><span>条数：" + it.Piece.ToString() + "条&nbsp;&nbsp;业务员：" + it.SaleManName + "</span><a class='ords-btn-pay'>已签收</a></div></div>";
                    }
                }
                #endregion
                ////待出库
                //List<WXOrderEntity> unSendResult = bus.QueryWeixinOrderInfo(1, 3000, new WXOrderEntity { WXID = WxUserInfo.ID, PayStatus = "1", OrderStatus = "1" });
                //#region 待发货订单
                //if (unSendResult.Count > 0)
                //{
                //    foreach (var it in unSendResult)
                //    {
                //        ltlUnSend.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>单号：" + it.OrderNo + "</span><span class='ord-status-txt-ts fr'>待发货</span></div><div class='weui-media-box__bd  pd-10'>";
                //        if (it.productList.Count > 0)
                //        {
                //            foreach (var pro in it.productList)
                //            {
                //                ltlUnSend.Text += "<div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "'><img class='weui-media-box__thumb' src='" + pro.FileName + "' alt=''></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "' class='ord-pro-link'>" + pro.Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + pro.Specs + "</span>，<span>" + pro.HubDiameter.ToString() + "寸</span></p><div class='clear mg-t-10'><div class='wy-pro-pri fl'>¥<em class='num font-15'>" + pro.OrderPrice.ToString() + "</em></div><div class='pro-amount fr'><span class='font-13'>数量×<em class='name'>" + pro.OrderNum.ToString() + "</em></span></div></div></div></div>";
                //            }
                //        }
                //        ltlUnSend.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中，请您耐心等待....</div></div></div>";
                //        //<a href='comment.html' class='ords-btn-com'>评价</a>
                //    }
                //}
                //#endregion
                ////待收货ltlUnAccept 
                //List<WXOrderEntity> unAcceptResult = bus.QueryWeixinOrderInfo(1, 3000, new WXOrderEntity { WXID = WxUserInfo.ID, PayStatus = "1", OrderStatus = "2" });
                //#region 待收货订单
                //if (unAcceptResult.Count > 0)
                //{
                //    foreach (var it in unAcceptResult)
                //    {
                //        ltlUnAccept.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>单号：" + it.OrderNo + "</span><span class='ord-status-txt-ts fr'>待收货</span></div><div class='weui-media-box__bd  pd-10'>";
                //        if (it.productList.Count > 0)
                //        {
                //            foreach (var pro in it.productList)
                //            {
                //                ltlUnAccept.Text += "<div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "'><img class='weui-media-box__thumb' src='" + pro.FileName + "' alt=''></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "' class='ord-pro-link'>" + pro.Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + pro.Specs + "</span>，<span>" + pro.HubDiameter.ToString() + "寸</span></p><div class='clear mg-t-10'><div class='wy-pro-pri fl'>¥<em class='num font-15'>" + pro.OrderPrice.ToString() + "</em></div><div class='pro-amount fr'><span class='font-13'>数量×<em class='name'>" + pro.OrderNum.ToString() + "</em></span></div></div></div></div>";
                //            }
                //        }
                //        ltlUnAccept.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'><div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(" + it.OrderNo + ");' class='ords-btn-com receipt'>确认收货</a></div></div></div>";
                //        //<a href='comment.html' class='ords-btn-com'>评价</a>
                //    }
                //}
                //#endregion

                //List<WXOrderEntity> result = bus.QueryWeixinOrderInfo(1, 30, new WXOrderEntity { WXID = WxUserInfo.ID });
                //#region 全部订单
                //if (result.Count > 0)
                //{
                //    foreach (var it in result)
                //    {
                //        string jyzt = string.Empty;
                //        string coo = string.Empty;
                //        if (it.PayStatus.Equals("1"))
                //        {
                //            jyzt = "已付款";
                //            switch (it.OrderStatus)
                //            {
                //                case "0":
                //                    jyzt = "待发货";
                //                    coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中，请您耐心等待....</div>";
                //                    break;
                //                case "1":
                //                    jyzt = "待发货";
                //                    coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'>商品正在打包中，请您耐心等待....</div>";
                //                    break;
                //                case "2":
                //                    jyzt = "待收货";
                //                    coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                //                    break;
                //                case "3":
                //                    jyzt = "待收货";
                //                    coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:sure(\"" + it.OrderNo + "\");' class='ords-btn-com receipt'>确认收货</a></div>";
                //                    break;
                //                case "4": jyzt = "交易成功";
                //                    break;
                //                default: break;
                //            }
                //        }
                //        else
                //        {
                //            jyzt = "待确认";
                //            //coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:PayMoney(" + it.OrderNo + ");' class='ords-btn-pay'>付款</a>&nbsp;&nbsp;<a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                //            coo = "<div class='weui-cell weui-cell_access weui-cell_link oder-opt-btnbox'><a href='javascript:DeleteOrder(" + it.ID.ToString() + ");' class='ords-btn-dele'>删除订单</a></div>";
                //        }
                //        ltlAllOrder.Text += "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd'><span>单号：" + it.OrderNo + "</span><span class='ord-status-txt-ts fr'>" + jyzt + "</span></div><div class='weui-media-box__bd  pd-10'>";
                //        if (it.productList.Count > 0)
                //        {
                //            foreach (var pro in it.productList)
                //            {
                //                ltlAllOrder.Text += "<div class='weui-media-box_appmsg ord-pro-list'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "'><img class='weui-media-box__thumb' src='" + pro.FileName + "' alt=''></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + pro.ID.ToString() + "' class='ord-pro-link'>" + pro.Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + pro.Specs + "</span>，<span>" + pro.HubDiameter.ToString() + "寸</span></p><div class='clear mg-t-10'><div class='wy-pro-pri fl'>¥<em class='num font-15'>" + pro.OrderPrice.ToString() + "</em></div><div class='pro-amount fr'><span class='font-13'>数量×<em class='name'>" + pro.OrderNum.ToString() + "</em></span></div></div></div></div>";
                //            }
                //        }
                //        ltlAllOrder.Text += "</div><div class='ord-statistics'><span>共<em class='num'>" + it.Piece.ToString() + "</em>件商品，</span><span class='wy-pro-pri'>总金额：¥<em class='num font-15'>" + it.TotalCharge.ToString() + "</em></span></div><div class='weui-panel__ft'>";
                //        ltlAllOrder.Text += coo + "</div></div>";
                //        //<a href='comment.html' class='ords-btn-com'>评价</a>
                //    }
                //}
                //#endregion
                //待付款

            }
        }
    }
}