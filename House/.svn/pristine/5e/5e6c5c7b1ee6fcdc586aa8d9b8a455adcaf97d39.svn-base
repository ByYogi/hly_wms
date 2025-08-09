<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlyMy.aspx.cs" Inherits="Cargo.Weixin.OnlyMy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的专属后台</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />

</head>
<body ontouchstart>

    <div class="weui-panel weui-panel_access">
        <div class="weui-panel weui-panel_access">
            <div class="weui-panel__hd">
                <a href="#" class="weui-cell weui-cell_access center-alloder">
                    <div class="weui-cell__bd wy-cell">
                        <div class="weui-cell__hd">
                            <img src="WeUI/image/center-icon-jk.png" alt="" class="center-list-icon">
                        </div>
                        <div class="weui-cell__bd weui-cell_primary">
                            <p class="center-list-txt">我的小金库</p>
                        </div>
                    </div>
                    <span class="weui-cell__ft"></span>
                </a>
            </div>
            <div class="weui-panel__bd">
                <div class="weui-flex">
                    <div class="weui-flex__item">
                        <a href="#" class="center-ordersModule">
                            <div class="center-money"><em><asp:Literal ID="ltlTodayNum" runat="server"></asp:Literal></em></div>
                            <div class="name">我的订单</div>
                        </a>
                    </div>
                    <div class="weui-flex__item">
                        <a href="#" class="center-ordersModule">
                            <div class="center-money"><em><asp:Literal ID="ltlTC" runat="server"></asp:Literal></em></div>
                            <div class="name">我的提成</div>
                        </a>
                    </div>
                    <div class="weui-flex__item">
                        <a href="#" class="center-ordersModule">
                            <div class="center-money"><em><asp:Literal ID="ltlFX" runat="server"></asp:Literal></em></div>
                            <div class="name">我的返利</div>
                        </a>
                    </div>
                    <%--  <div class="weui-flex__item">
                        <a href="#" class="center-ordersModule">
                            <div class="center-money"><em>1655</em></div>
                            <div class="name">返现总额</div>
                        </a>
                    </div>--%>
                </div>
            </div>
        </div>

        <div class="weui-panel">
            <div class="weui-panel__bd">
                <div class="weui-media-box weui-media-box_small-appmsg">
                    <div class="weui-cells">
                        <a class="weui-cell weui-cell_access" href="OnlyMyQrcode.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon-qrcode.png" alt="我的专属二维码" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的专属二维码</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="taobao.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/taobao.png" alt="输入淘宝订单号" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">输入淘宝订单号</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="OnlyMyOrder.aspx?qty=0">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-order-all.png" alt="我的订单" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的订单</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="OnlyMyOrder.aspx?qty=1">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-jyjl.png" alt="我的下级订单" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的下级订单</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                       <%-- <a class="weui-cell weui-cell_access" href="#">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon_dfk.png" alt="我的返现" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的返现</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="#">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-yhk.png" alt="我的提成" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的提成</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>--%>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <%--<div class="weui-tabbar">
        <a href="mall.aspx" class="weui-tabbar__item weui-bar__item">
            <div class="weui-tabbar__icon">
                <img src=" WeUI/image/footer01.png" alt="">
            </div>
            <p class="weui-tabbar__label" style="font-size: 13px;">商城</p>
        </a>
        <a href="category.aspx" class="weui-tabbar__item">
            <div class="weui-tabbar__icon">
                <img src=" WeUI/image/footer02.png" alt="">
            </div>
            <p class="weui-tabbar__label" style="font-size: 13px;">分类</p>
        </a>
        <a href="shoppingCart.aspx" class="weui-tabbar__item">
            <div id="gwc"></div>

            <div class="weui-tabbar__icon">
                <img src=" WeUI/image/footer03.png" alt="">
            </div>
            <p class="weui-tabbar__label" style="font-size: 13px;">购物车</p>
        </a>
        <a href="my.aspx" class="weui-tabbar__item weui-bar__item--on">
            <div class="weui-tabbar__icon">
                <img src=" WeUI/image/footer04.png" alt="">
            </div>
            <p class="weui-tabbar__label" style="color: red; font-size: 13px;">个人中心</p>
        </a>
    </div>--%>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);

            //if (localStorage.getItem("CART") != null) {
            //    var cart = localStorage.getItem("CART");
            //    var cartjson = JSON.parse(cart);
            //    var carcount = 0;
            //    for (var i = 0; i < cartjson.length; i++) {
            //        carcount += Number(cartjson[i].PC);
            //    }
            //    //document.getElementById("gwc").innerHTML = carcount;

            //    $('#gwc').html("<span class=\"weui-badge\" style=\"position: absolute; top: -.4em; right: 1em;\">" + carcount + "</span>");
            //}
        });
    </script>
</body>
</html>
