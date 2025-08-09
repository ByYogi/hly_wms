<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mall.aspx.cs" Inherits="Cargo.Weixin.mall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商城</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <style type="text/css">
        .placeholder {
            margin: 1px;
            padding: 5px 0px;
            height: 45px;
            text-align: center;
            font-size: 12px;
        }
        /*首页产品列表*/
        .product-list-border:nth-child(1) {
            margin-top: 0;
        }

        .product-list-border:nth-child(2) {
            margin-top: 0;
        }

        .product-list-border {
            background-color: #fff;
            width: calc((100% - 5px*1)/ 2) !important;
            padding: 5px;
            margin-top: 5px;
        }

            .product-list-border img {
                width: 100%;
                min-height: 150px;
                max-height: 180px;
            }

        .product-name {
            color: #000;
            font-size: 12px;
            overflow: hidden;
            text-overflow: ellipsis;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
        }

        .product-price {
            color: #e60d1e;
            font-size: 14px;
            margin: 1px auto;
        }

        .divLink {
            padding: 0;
            margin-bottom: 1px;
        }

        .indSearch {
            box-sizing: content-box;
            width: 100%;
            height: 30px;
            display: inline-block;
            border: 0;
            -webkit-appearance: none;
            appearance: none;
            border-radius: 10px;
            font-family: inherit;
            color: #000000;
            font-size: 13px;
            font-weight: normal;
            padding: 0 5px;
            background-color: #fff;
            border: 1px solid #2dd1e6;
            opacity: .9;
        }
    </style>

</head>

<body ontouchstart>
    <div class="weui-tab">

        <div class="weui-tab__bd">
            <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active" style="background-color: #FAFAFA">
                <div class="weui-search-bar" style="height: 40px;">
                    <input type="search" class="indSearch" id='searchInput' placeholder='请输入轮胎规格 例：2055516' onkeyup="enterSearch(event)" onclick="go()" />
                    <a href="services.aspx">
                        <img src="WeUI/image/icon-kefu.png" style="width: 40px; height: 40px;" /></a>
                </div>

                <!--图标分类-->
                <img src="WeUI/image/index-middled.jpg" style="width: 100%; height: 70px;" usemap="#Map" />
                <map name="Map" id="Map">
                    <area shape="rect" coords="3,2,150,126" href="SpecialSale.aspx" target="_self" alt="天天特价" />
                    <area shape="rect" coords="121,3,244,126" href="AdvertFixTime.aspx" target="_self" alt="限时促销" />
                    <area shape="rect" coords="248,4,372,107" href="ConsumeShop.aspx" target="_blank"  alt="积分兑换商城"/>
                </map>
                <!--限时推广销售-->
                <asp:Literal ID="ltlLimitTimeSale" runat="server"></asp:Literal>
                <!--限时推广销售-->
                <div class="divLink">
                    <a href="category.aspx?ProductTypeID=9">
                        <img src="wxPic/home-1.png" style="width: 100%; height: 100%" /></a>
                </div>
                <div class="divLink">
                    <a href="category.aspx?ProductTypeID=66">
                        <img src="wxPic/home-2.png" style="width: 100%; height: 100%" /></a>
                </div>
                 <div class="divLink">
                    <a href="category.aspx?ProductTypeID=171">
                        <img src="wxPic/home-3.png" style="width: 100%; height: 100%" /></a>
                </div>
                <div class="divLink">
                    <a href="category.aspx?ProductTypeID=18">
                        <img src="wxPic/home-4.png" style="width: 100%; height: 100%" /></a>
                </div>
                <div class="divLink">
                    <a href="category.aspx?ProductTypeID=31">
                        <img src="wxPic/home-5.png" style="width: 100%; height: 100%" /></a>
                </div>
                    <div class="divLink">
                    <a href="category.aspx?ProductTypeID=34">
                        <img src="wxPic/home-7.png" style="width: 100%; height: 100%" /></a>
                </div>
                <div class="divLink">
                    <img src="wxPic/home-6.png" style="width: 100%; height: 100%" />
                </div>
                <!--顶部轮播-->
                <div class="swiper-container swiper-banner" style="height: 150px;">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                            <a href="#">
                                <img src="WeUI/image/swiper-1.jpg" /></a>
                        </div>
                        <div class="swiper-slide">
                            <a href="#">
                                <img src="WeUI/image/swiper-3.jpg" /></a>
                        </div>
                    </div>
                    <div class="swiper-pagination"></div>
                </div>
                <div class="weui-panel__ft">&nbsp;</div>
                <div class="weui-panel__ft">&nbsp;</div>
            </div>
        </div>

        <div class="weui-tabbar">
            <a href="mall.aspx" class="weui-tabbar__item weui-bar__item--on">
                <div class="weui-tabbar__icon">
                    <%--<img src=" WeUI/image/icon_nav_button.png" alt="" />--%>
                    <img src=" WeUI/image/footer01.png" alt="" />
                </div>
                <p class="weui-tabbar__label" style="color: red;">商城</p>
            </a>
            <a href="category.aspx" class="weui-tabbar__item">
                <div class="weui-tabbar__icon">
                    <%--   <img src=" WeUI/image/icon_nav_msg.png" alt="" />--%>
                    <img src=" WeUI/image/footer02.png" alt="" />
                </div>
                <p class="weui-tabbar__label">分类</p>
            </a>
            <a href="shoppingCart.aspx" class="weui-tabbar__item">
                <div id="gwc"></div>
                <div class="weui-tabbar__icon">
                    <%-- <img src=" WeUI/image/icon_nav_article.png" alt="" />--%>
                    <img src=" WeUI/image/footer03.png" alt="" />
                </div>
                <p class="weui-tabbar__label">购物车</p>
            </a>
            <a href="my.aspx" class="weui-tabbar__item weui-bar__item">
                <div class="weui-tabbar__icon">
                    <%--<img src=" WeUI/image/icon_nav_cell.png" alt="" />--%>
                    <img src=" WeUI/image/footer04.png" alt="" />
                </div>
                <p class="weui-tabbar__label">个人中心</p>
            </a>
        </div>
    </div>

    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var AdvertEndDate = "<%=AdvertEndDate%>";
            if (AdvertEndDate == null || AdvertEndDate == "") {
                msg = "暂无限时推广销售轮胎";
                $('#fixT').html(msg);
                return;
            }
            var endTime = new Date(AdvertEndDate); // 最终时间
            setInterval(clock, 1000);
            function clock() {
                var nowTime = new Date();
                var maxTime = parseInt((endTime.getTime() - nowTime.getTime()) / 1000);
                if (maxTime >= 0) {
                    var days = parseInt(maxTime / 60 / 60 / 24, 10); //计算剩余的天数
                    var hours = parseInt(maxTime / 60 / 60 % 24, 10); //计算剩余的小时
                    var minutes = parseInt(maxTime / 60 % 60, 10); //计算剩余的分钟
                    var seconds = parseInt(maxTime % 60, 10); //计算剩余的秒数
                    msg = days + "天" + hours + ":" + minutes + ":" + seconds;
                    $('#fixT').html(msg);
                } else {
                    //自动下架所有推广产品
                    $.ajax({
                        url: 'myAPI.aspx?method=AutoUnShelveAdvertProduct',
                        dataType: 'json', cache: false, data: {},
                        success: function (text) { }
                    });
                }
            }
        }
        function Await() {
            $.alert("功能正在开发中，敬请期待...");
        }
        function go() {
            window.location.href = "Search.aspx";
        }
        $(function () {
            FastClick.attach(document.body);
            if (localStorage.getItem("CART") != null) {
                var cart = localStorage.getItem("CART");
                var cartjson = JSON.parse(cart);
                var carcount = 0;
                for (var i = 0; i < cartjson.length; i++) {
                    carcount += Number(cartjson[i].PC);
                }
                //document.getElementById("gwc").innerHTML = carcount;
                //$('#gwc').html(carcount);
                $('#gwc').html("<span class=\"weui-badge\" style=\"position: absolute; top: -.4em; right: 1em;\">" + carcount + "</span>");
            }
        });
    </script>
    <script src=" WeUI/JS/swiper.js"></script>
    <script type="text/javascript">
        function enterSearch(e) {
            if (e.keyCode == 13) {
                window.location.href = " category.aspx?ProductTypeID=9&searchText=" + escape($('#searchInput').val());
            }
        }
        $(".swiper-banner").swiper({
            loop: true,
            autoplay: 3000
        });
        $(".swiper-jingxuan").swiper({
            pagination: '.swiper-pagination',
            loop: true,
            paginationType: 'fraction',
            slidesPerView: 3,
            paginationClickable: true,
            spaceBetween: 2
        });
        var pagesize = 8;//每页数据条数
        var page = 1;
        var maxpage;
        $('#loading').hide();
        function ajaxpage(page) {
            $.ajax({
                type: "POST", dataType: "json",
                url: 'myAPI.aspx?method=QueryInHouseProductData',
                data: { page: page, rows: pagesize, onShelves: "0" },
                beforeSend: function () {
                    $("#loading").show();
                },
                success: function (rs) {
                    for (var i = 0; i < rs.rows.length; i++) {
                        $('#rank-list').append("<div class='weui-col-50 product-list-border' style='border-radius:5px;'><a href='productInfo.aspx?ID=" + rs.rows[i].OnSaleID + "'><img src='" + rs.rows[i].FileName + "' /><p class='product-name'>" + rs.rows[i].Specs + " " + rs.rows[i].LoadIndex + rs.rows[i].SpeedLevel + " " + rs.rows[i].Figure + "</p><p class='product-price'>￥" + rs.rows[i].TradePrice + "</p></a></div>")
                    }
                    var maxpage = Math.ceil(rs.total / pagesize);
                    sessionStorage['maxpage'] = maxpage;
                    $('#loading').hide();
                },
                timeout: 15000
            });
        }
    </script>
</body>
</html>
