<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumeInfo.aspx.cs" Inherits="Cargo.Weixin.ConsumeInfo" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>积分兑换礼品详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
</head>
<body ontouchstart>
    <!--主体-->
    <div class="weui-content">
        <!--产品详情-->
        <div class="weui-tab">
            <div class="weui-navbar" style="position: fixed; top: 0; left: 0; right: 0; height: 44px;">
                <a class="weui-navbar__item proinfo-tab-tit weui-bar__item--on" href="#tab1">商品</a>
                <a class="weui-navbar__item proinfo-tab-tit" href="#tab2">详情</a>
            </div>
            <div class="weui-tab__bd proinfo-tab-con">
                <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <!--主图轮播-->
                    <div class="swiper-container swiper-zhutu">
                        <div class="swiper-wrapper">
                            <asp:Literal ID="ltlzhutu" runat="server"></asp:Literal>
                        </div>
                        <div class="swiper-pagination swiper-zhutu-pagination"></div>
                    </div>
                    <div class="wy-media-box-nomg weui-media-box_text">
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
                    </div>
                    <%--   <div class="wy-media-box2 weui-media-box_text">
                        <asp:Literal ID="ltlProduct" runat="server"></asp:Literal>
                    </div>--%>
                    <div class="wy-media-box2 txtpd weui-media-box_text">
                        <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">运费</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-message clear">
                                    <span class="promotion-item-text">免运费<!--<div class="wy-pro-pri">¥<span class="num">11.00</span></div>--></span>
                                </div>
                            </div>
                        </div>
                        <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">商家</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-message clear">
                                    <span class="promotion-item-text">迪乐泰贸易有限公司</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tab2" class="weui-tab__bd-item ">
                    <div class="pro-detail">
                        <asp:Literal ID="ltlDetail" runat="server"></asp:Literal>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!--底部导航-->
    <div class="foot-black"></div>
    <div class="weui-tabbar wy-foot-menu">
        <a href="mall.aspx" class="promotion-foot-menu-items">
            <div class="weui-tabbar__icon promotion-foot-menu-kefu"></div>
            <p class="weui-tabbar__label">商城</p>
        </a>
        <a href="shoppingCart.aspx" class="promotion-foot-menu-items">
            <div id="gwc"></div>
            <div class="weui-tabbar__icon promotion-foot-menu-cart"></div>
            <p class="weui-tabbar__label">购物车</p>
        </a>
        <%--  <asp:Literal ID="ltlAddCart" runat="server"></asp:Literal>--%>
        <asp:Literal ID="ltlBuy" runat="server"></asp:Literal>
    </div>

    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);

        });
    </script>
    <script src="WeUI/JS/swiper.js"></script>
    <script>
        $(".swiper-zhutu").swiper({
            loop: true,
            paginationType: 'fraction',
            autoplay: 5000
        });
    </script>
    <script>

        function saveOrder() {
            var sid = '<%=pid%>';
            var ProductID = '<%=ProductID%>';
            $.showLoading();
            $.ajax({
                url: 'myAPI.aspx?method=ConsumeConvert',
                cache: false, dataType: 'json', data: { ShelveID: sid, ProductID: ProductID },
                success: function (text) {
                    $.hideLoading();
                    if (text.Result == true) {
                        $.toast("积分兑换成功!");
                    }
                    else {
                        $.toast('积分兑换失败：' + text.Message);
                    }
                }
            });
        }
        $(function () {
            $(".promotion-sku li").click(function () {
                $(this).addClass("active").siblings("li").removeClass("active");
            })
        })
    </script>
</body>
</html>
