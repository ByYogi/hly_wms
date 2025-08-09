<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productInfo.aspx.cs" Inherits="Cargo.Weixin.productInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>产品详情</title>
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
                <%--  <a class="weui-navbar__item proinfo-tab-tit" href="#tab3">评价</a>--%>
            </div>
            <div class="weui-tab__bd proinfo-tab-con">
                <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <!--主图轮播-->
                    <div class="swiper-container swiper-zhutu">
                        <div class="swiper-wrapper">
                            <asp:Literal ID="ltlzhutu" runat="server"></asp:Literal>
                            <%--  <div class="swiper-slide">
                                <img src="../WeUI/image/swiper-2.jpg" />
                            </div>
                            <div class="swiper-slide">
                                <img src="../WeUI/image/swiper-3.jpg" />
                            </div>--%>
                        </div>
                        <div class="swiper-pagination swiper-zhutu-pagination"></div>
                    </div>
                    <div class="wy-media-box-nomg weui-media-box_text">
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
                        <%--   <h4 class="wy-media-box__title">德国马牌汽车轮胎MC5 205/55R16适配明锐速腾朗逸马自达6荣威350</h4>
                        <div class="wy-pro-pri mg-tb-5">¥<em class="num font-20">296.00</em></div>
                        <p class="weui-media-box__desc">正品保证 405城万家门店 25仓发货 包安装</p>--%>
                    </div>
                    <%--  <div class="wy-media-box2 weui-media-box_text">
                        <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">优惠</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-message clear">
                                    <i class="yhq"><span class="label-text">优惠券</span></i>
                                    <span class="promotion-item-text">满197.00减40.00</span>
                                </div>
                                <div class="promotion-message clear">
                                    <i class="yhq"><span class="label-text">优惠券</span></i>
                                    <span class="promotion-item-text">满197.00减40.00</span>
                                </div>
                                <div class="yhq-btn clear"><a href="yhq_list.html">去领券</a></div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="wy-media-box2 weui-media-box_text">
                        <%--<div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">尺寸</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-sku clear">
                                    <ul>
                                        <li><a href="javascript:;">14</a></li>
                                        <li><a href="javascript:;">15</a></li>
                                        <li><a href="javascript:;">16</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>--%><asp:Literal ID="ltlProduct" runat="server"></asp:Literal>
                        <%--   <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__bd">
                                <div class="promotion-sku clear" style="font-size:13px;">
                                 商品名称：优科豪马轮胎&nbsp;&nbsp;&nbsp;规格：205/55R16&nbsp;&nbsp;&nbsp;花纹：V551&nbsp;&nbsp;&nbsp;型号：F4932&nbsp;&nbsp;&nbsp;尺寸：16寸&nbsp;&nbsp;&nbsp;速率级别：W
                                </div>
                            </div>
                        </div>--%>

                        <%--  <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">颜色</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-sku clear">
                                    <ul>
                                        <li><a href="javascript:;">黑色</a></li>
                                        <li><a href="javascript:;">红色</a></li>
                                        <li><a href="javascript:;">白色</a></li>
                                        <li><a href="javascript:;">蓝色</a></li>
                                        <li><a href="javascript:;">橘黄色</a></li>
                                        <li><a href="javascript:;">绿色</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <div class="wy-media-box2 txtpd weui-media-box_text">
                        <%--  <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">送至</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-message clear">
                                    <span class="promotion-item-text">江苏</span>
                                    <span class="promotion-item-text">宿迁</span>
                                    <span class="promotion-item-text">洋河新区</span>
                                </div>
                            </div>
                        </div>--%>
                        <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">运费</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-message clear">
                                    <span class="promotion-item-text">运费到付<!--<div class="wy-pro-pri">¥<span class="num">11.00</span></div>--></span>
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
                        <%--  <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">提示</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-message clear">
                                    <span class="promotion-item-text">
                                        <p class="txt-color-ml">支持7天无理由退换货</p>
                                    </span>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <div id="tab2" class="weui-tab__bd-item ">
                    <div class="pro-detail">
                        <asp:Literal ID="ltlDetail" runat="server"></asp:Literal>
                        <%--  <img src="upload/xq1.jpg" /><img src="upload/xq2.jpg" /><img src="upload/xq3.jpg" /><img src="upload/xq4.jpg" /><img src="upload/xq5.jpg" /><img src="upload/xq6.jpg" /><img src="upload/xq7.jpg" /><img src="upload/xq8.jpg" /><img src="upload/xq9.jpg" />--%>
                    </div>
                </div>
                <%-- <div id="tab3" class="weui-tab__bd-item">
                    <!--评价-->
                    <div class="weui-panel__bd">
                        <div class="wy-media-box weui-media-box_text">
                            <div class="weui-cell nopd weui-cell_access">
                                <div class="weui-cell__hd">
                                    <img src="upload/headimg.jpg" alt="" style="width: 20px; margin-right: 5px; display: block">
                                </div>
                                <div class="weui-cell__bd weui-cell_primary">
                                    <p>飞翔的小土豆</p>
                                </div>
                                <span class="weui-cell__time">2017-02-06</span>
                            </div>
                            <div class="comment-item-star"><span class="real-star comment-stars-width5"></span></div>
                            <p class="weui-media-box__desc">面料不错，码数也正常  男朋友穿的很合适。</p>
                            <ul class="weui-uploader__files clear mg-com-img">
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                            </ul>
                        </div>
                    </div>
                    <div class="weui-panel__bd">
                        <div class="wy-media-box weui-media-box_text">
                            <div class="weui-cell nopd weui-cell_access">
                                <div class="weui-cell__hd">
                                    <img src="upload/headimg.jpg" alt="" style="width: 20px; margin-right: 5px; display: block">
                                </div>
                                <div class="weui-cell__bd weui-cell_primary">
                                    <p>爱情海的事故</p>
                                </div>
                                <span class="weui-cell__time">2017-02-06</span>
                            </div>
                            <div class="comment-item-star"><span class="real-star comment-stars-width3"></span></div>
                            <p class="weui-media-box__desc">面料不错，码数也正常  男朋友面料不错，码数也正常  男朋友穿的面料不错，码数也正常  男朋友穿的穿的很合适。</p>
                            <ul class="weui-uploader__files clear mg-com-img">
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                            </ul>
                        </div>
                    </div>
                    <div class="weui-panel__bd">
                        <div class="wy-media-box weui-media-box_text">
                            <div class="weui-cell nopd weui-cell_access">
                                <div class="weui-cell__hd">
                                    <img src="upload/headimg.jpg" alt="" style="width: 20px; margin-right: 5px; display: block">
                                </div>
                                <div class="weui-cell__bd weui-cell_primary">
                                    <p>爱情海的事故</p>
                                </div>
                                <span class="weui-cell__time">2017-02-06</span>
                            </div>
                            <div class="comment-item-star"><span class="real-star comment-stars-width3"></span></div>
                            <p class="weui-media-box__desc">面料不错，码数也正常  男朋友面料不错，码数也正常  男朋友穿的面料不错，码数也正常  男朋友穿的穿的很合适。</p>
                            <ul class="weui-uploader__files clear mg-com-img">
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                            </ul>
                        </div>
                    </div>
                    <div class="weui-panel__bd">
                        <div class="wy-media-box weui-media-box_text">
                            <div class="weui-cell nopd weui-cell_access">
                                <div class="weui-cell__hd">
                                    <img src="upload/headimg.jpg" alt="" style="width: 20px; margin-right: 5px; display: block">
                                </div>
                                <div class="weui-cell__bd weui-cell_primary">
                                    <p>爱情海的事故</p>
                                </div>
                                <span class="weui-cell__time">2017-02-06</span>
                            </div>
                            <div class="comment-item-star"><span class="real-star comment-stars-width3"></span></div>
                            <p class="weui-media-box__desc">面料不错，码数也正常  男朋友面料不错，码数也正常  男朋友穿的面料不错，码数也正常  男朋友穿的穿的很合适。</p>
                            <ul class="weui-uploader__files clear mg-com-img">
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)" onclick="window.location.reload('gallery.html')"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/ban2.jpg)" onclick="window.location.reload('gallery.html')"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)" onclick="window.location.reload('gallery.html')"></li>
                            </ul>
                        </div>
                    </div>
                    <div class="weui-panel__bd">
                        <div class="wy-media-box weui-media-box_text">
                            <div class="weui-cell nopd weui-cell_access">
                                <div class="weui-cell__hd">
                                    <img src="upload/headimg.jpg" alt="" style="width: 20px; margin-right: 5px; display: block">
                                </div>
                                <div class="weui-cell__bd weui-cell_primary">
                                    <p>爱情海的事故</p>
                                </div>
                                <span class="weui-cell__time">2017-02-06</span>
                            </div>
                            <div class="comment-item-star"><span class="real-star comment-stars-width3"></span></div>
                            <p class="weui-media-box__desc">面料不错，码数也正常  男朋友面料不错，码数也正常  男朋友穿的面料不错，码数也正常  男朋友穿的穿的很合适。</p>
                            <ul class="weui-uploader__files clear mg-com-img">
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                                <li class="weui-uploader__file" style="background-image: url(./upload/pro3.jpg)"></li>
                            </ul>
                        </div>
                    </div>
                    <a href="javascript:void(0);" class="weui-cell weui-cell_access weui-cell_link list-more">
                        <div class="weui-cell__bd">查看更多</div>
                        <span class="weui-cell__ft"></span>
                    </a>

                </div>--%>
            </div>
        </div>
    </div>
    <%--<span id="tophovertree" title="返回顶部"></span>--%>
    <!--底部导航-->
    <div class="foot-black"></div>
    <div class="weui-tabbar wy-foot-menu">
        <a href="mall.aspx" class="promotion-foot-menu-items">
            <div class="weui-tabbar__icon promotion-foot-menu-kefu"></div>
            <p class="weui-tabbar__label">商城</p>
        </a>
        <%-- <a href="javascript:;" id='show-toast' class="promotion-foot-menu-items">
            <div class="weui-tabbar__icon promotion-foot-menu-collection"></div>
            <p class="weui-tabbar__label">收藏</p>
        </a>--%>
        <a href="shoppingCart.aspx" class="promotion-foot-menu-items">
            <div id="gwc"></div>
            <div class="weui-tabbar__icon promotion-foot-menu-cart"></div>
            <p class="weui-tabbar__label">购物车</p>
        </a>
        <asp:Literal ID="ltlAddCart" runat="server"></asp:Literal>
        <asp:Literal ID="ltlBuy" runat="server"></asp:Literal>
    </div>
    <div id="join_cart" class='weui-popup__container popup-bottom' style="z-index: 600;">
        <div class="weui-popup__overlay" style="opacity: 1;"></div>
        <div class="weui-popup__modal">
            <div class="modal-content">
                <div class="weui-msg" style="padding-top: 0;">
                    <div class="weui-msg__icon-area"><i class="weui-icon-success weui-icon_msg"></i></div>
                    <div class="weui-msg__text-area">
                        <h2 class="weui-msg__title">成功加入购物车</h2>
                        <p class="weui-msg__desc">亲爱的用户，您的商品已成功加入购物车，为了保证您的商品快速送达，请您尽快到购物车结算。</p>
                    </div>
                    <div class="weui-msg__opr-area">
                        <p class="weui-btn-area">
                            <a href="shoppingCart.aspx?SaleT=<%=SaleT %>" class="weui-btn weui-btn_primary">去购物车结算</a>
                            <a href="javascript:gwcNum();" class="weui-btn weui-btn_default close-popup">不，我再看看</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="selcet_sku" class='weui-popup__container popup-bottom' style="z-index: 600;">
        <div class="weui-popup__overlay" style="opacity: 1;"></div>
        <div class="weui-popup__modal">
            <div class="toolbar">
                <div class="toolbar-inner">
                    <a href="javascript:;" class="picker-button close-popup">关闭</a>
                    <h1 class="title">商品属性</h1>
                </div>
            </div>
            <div class="modal-content">
                <div class="weui-msg" style="padding-top: 0;">
                    <div class="wy-media-box2 weui-media-box_text" style="margin: 0;">
                        <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">尺寸</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-sku clear">
                                    <ul>
                                        <li><a href="javascript:;">14</a></li>
                                        <li><a href="javascript:;">15</a></li>
                                        <li><a href="javascript:;">16</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <%--   <div class="weui-media-box_appmsg">
                            <div class="weui-media-box__hd proinfo-txt-l"><span class="promotion-label-tit">颜色</span></div>
                            <div class="weui-media-box__bd">
                                <div class="promotion-sku clear">
                                    <ul>
                                        <li><a href="javascript:;">黑色</a></li>
                                        <li><a href="javascript:;">红色</a></li>
                                        <li><a href="javascript:;">白色</a></li>
                                        <li><a href="javascript:;">蓝色</a></li>
                                        <li><a href="javascript:;">橘黄色</a></li>
                                        <li><a href="javascript:;">绿色</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <div class="weui-msg__opr-area">
                        <p class="weui-btn-area">
                            <a href="order_info.html" class="weui-btn weui-btn_primary">立即购买</a>
                            <a href="javascript:;" class="weui-btn weui-btn_default close-popup">不，我再看看</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
            gwcNum();
        });
        function gwcNum() {
            if (localStorage.getItem("CART") != null) {
                var cart = localStorage.getItem("CART");
                var cartjson = JSON.parse(cart);
                var carcount = 0;
                for (var i = 0; i < cartjson.length; i++) {
                    carcount += Number(cartjson[i].PC);
                }
                //document.getElementById("shopNum").innerHTML = carcount;
                $('#gwc').html("<span class=\"weui-badge\" style=\"position: absolute; top: -.4em; right: 1em;\">" + carcount + "</span>");
            }
        }
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
        //添加购物车
        function addCart() {
            var pid = '<%=pid%>';
            var SaleType = '<%=SaleType%>';
            var TypeID = '<%=TypeID%>';
            if (localStorage.getItem("CART") != null) {

                var cart = localStorage.getItem("CART");
                var cartjson = JSON.parse(cart);
                var isadd = "0";
                for (var i = 0; i < cartjson.length; i++) {
                    if (pid == cartjson[i].ID) {
                        cartjson[i].PC = Number(cartjson[i].PC) + 1;
                        isadd = "1";
                    }
                }
                if (isadd == "0") {
                    //TJ:0正价  1:特价
                    var addjson = { ID: "<%=pid%>", PC: 1, PRICE: "<%=price%>", TJ: SaleType, RO: "<%=Assort%>", TYID: TypeID };
                    cartjson.push(addjson);
                    localStorage.setItem("CART", JSON.stringify(cartjson));
                } else {
                    localStorage.setItem("CART", JSON.stringify(cartjson));
                }
            } else {
                var json = [{ ID: "<%=pid%>", PC: 1, PRICE: "<%=price%>", TJ: SaleType, RO: "<%=Assort%>", TYID: TypeID }];
                localStorage.setItem("CART", JSON.stringify(json));
            }
        }
        function saveOrder() {
            $.toast("下订单");
            addCart();
            var SaleT = "<%=SaleT%>";
            window.location.href = "shoppingCart.aspx?SaleT=" + SaleT;
        }
        $(function () {
            $(".promotion-sku li").click(function () {
                $(this).addClass("active").siblings("li").removeClass("active");
            })
        })
        $(document).on("click", "#show-toast", function () {
            $.toast("收藏成功", function () {
                console.log('close');
            });
        })
        $(document).on("open", ".weui-popup-modal", function () {
            console.log("open popup");
        }).on("close", ".weui-popup-modal", function () {
            console.log("close popup");
        });
    </script>
</body>
</html>
