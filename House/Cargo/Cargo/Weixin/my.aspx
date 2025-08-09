<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="my.aspx.cs" Inherits="Cargo.Weixin.my" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>我的中心</title>
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
    <div class='weui-content'>
        <div class="wy-center-top">
            <div class="weui-media-box weui-media-box_appmsg">
                <div class="weui-media-box__hd">
                    <img class="weui-media-box__thumb radius" src="<%=wxUser.AvatarBig %>" alt="">
                </div>
                <div class="weui-media-box__bd">
                    <h4 class="weui-media-box__title user-name">公司名：<%=wxUser.Name %></h4>
                    <p class="user-grade">店代码：<%=wxUser.ClientNum %></p>
                    <%--<p class="user-grade">等级：普通会员</p>--%>
                    <%--   <p class="user-integral">待返还金额：<em class="num">500.0</em>元</p>--%>
                </div>
            </div>
            <!--    <div class="xx-menu weui-flex">
      <div class="weui-flex__item"><div class="xx-menu-list"><em>987</em><p>账户余额</p></div></div>
      <div class="weui-flex__item"><div class="xx-menu-list"><em>459</em><p>我的蓝豆</p></div></div>
      <div class="weui-flex__item"><div class="xx-menu-list"><em>4</em><p>收藏商品</p></div></div>
    </div>-->
        </div>
    </div>

    <div class="weui-panel weui-panel_access">
        <div class="weui-panel__hd">
            <a href="myOrder.aspx" class="weui-cell weui-cell_access center-alloder">
                <div class="weui-cell__bd wy-cell">
                    <div class="weui-cell__hd">
                        <img src="WeUI/image/center-icon-order-all.png" alt="" class="center-list-icon">
                    </div>
                    <div class="weui-cell__bd weui-cell_primary">
                        <p class="center-list-txt">全部订单</p>
                    </div>
                </div>
                <span class="weui-cell__ft"></span>
            </a>
        </div>
        <div class="weui-panel__bd">
            <div class="weui-flex">
                <div class="weui-flex__item">
                    <a href="myOrder.aspx" class="center-ordersModule">
                        <span class="weui-badge" style="position: absolute; top: 5px; right: 10px; font-size: 10px;" id="unPay"></span>
                        <div class="imgicon">
                            <img src="WeUI/image/center-icon-order-dfk.png" />
                        </div>
                        <div class="name">待确认</div>
                    </a>
                </div>
                <div class="weui-flex__item">
                    <a href="myOrder.aspx" class="center-ordersModule">
                        <span class="weui-badge" style="position: absolute; top: 5px; right: 10px; font-size: 10px;" id="UnSend"></span>
                        <div class="imgicon">
                            <img src="WeUI/image/center-icon-order-dfh.png" />
                        </div>
                        <div class="name">待发货</div>
                    </a>
                </div>
                <div class="weui-flex__item">
                    <a href="myOrder.aspx" class="center-ordersModule">
                        <span class="weui-badge" style="position: absolute; top: 5px; right: 10px; font-size: 10px;" id="UnAccept"></span>
                        <div class="imgicon">
                            <img src="WeUI/image/center-icon-order-dsh.png" />
                        </div>
                        <div class="name">待收货</div>
                    </a>
                </div>
                <div class="weui-flex__item">
                    <a href="orders.html" class="center-ordersModule">
                        <%-- <span class="weui-badge" style="position: absolute; top: 5px; right: 10px; font-size: 10px;"></span>--%>
                        <div class="imgicon">
                            <img src="WeUI/image/center-icon-order-dpj.png" />
                        </div>
                        <div class="name">待评价</div>
                    </a>
                </div>
            </div>
        </div>

        <%--先隐藏客户充值平台<div class="weui-panel weui-panel_access">
            <div class="weui-panel__hd">
                <a href="myburse.html" class="weui-cell weui-cell_access center-alloder">
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
                        <a href="myburse.html" class="center-ordersModule">
                            <div class="center-money"><em>800.0</em></div>
                            <div class="name">账户总额</div>
                        </a>
                    </div>
                    <div class="weui-flex__item">
                        <a href="myburse.html" class="center-ordersModule">
                            <div class="center-money"><em>50.0</em></div>
                            <div class="name">返现金额</div>
                        </a>
                    </div>
                    <div class="weui-flex__item">
                        <a href="myburse.html" class="center-ordersModule">
                            <div class="center-money"><em>550.0</em></div>
                            <div class="name">待返还</div>
                        </a>
                    </div>
                    <div class="weui-flex__item">
                        <a href="myburse.html" class="center-ordersModule">
                            <div class="center-money"><em>165</em></div>
                            <div class="name">蓝豆</div>
                        </a>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="weui-panel">
            <div class="weui-panel__bd">
                <div class="weui-media-box weui-media-box_small-appmsg">
                    <div class="weui-cells">
                        <%--  <a class="weui-cell weui-cell_access" href="record.html">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-jyjl.png" alt="" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">交易记录</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>--%>
                         <a class="weui-cell weui-cell_access" href="myAccount.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon_nav_account.png" alt="我的账单" class="center-list-icon" />
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的账单</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="carTyreMatch.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon_nav_msg.png" alt="车型适配" class="center-list-icon" />
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">车型适配</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="myConsume.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-sc.png" alt="我的积分" class="center-list-icon" />
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的积分</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="myAddress.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-dz.png" alt="地址管理" class="center-list-icon" />
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">地址管理</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="services.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon-kefu.png" alt="联系客服" class="center-list-icon" />
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">联系客服</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="help.aspx">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon_nav_article.png" alt="帮助中心" class="center-list-icon" />
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">帮助中心</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="secKillStatis.aspx?Company=3" id="sec">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon-statis.png" alt="汽修店活动统计" class="center-list-icon" />
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">汽修店活动统计</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="OnlyMy.aspx" id="tao">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/icon-statis.png" alt="淘宝购物返利" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">淘宝购物返利</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <%-- <a class="weui-cell weui-cell_access" href="card.html">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-yhk.png" alt="" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">我的银行卡</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="password.html">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-dlmm.png" alt="" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">密码修改</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="login.html">
                            <div class="weui-cell__hd">
                                <img src="WeUI/image/center-icon-out.png" alt="" class="center-list-icon">
                            </div>
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="center-list-txt">退出账号</p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>--%>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div class="weui-tabbar">
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
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script>
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

                $('#gwc').html("<span class=\"weui-badge\" style=\"position: absolute; top: -.4em; right: 1em;\">" + carcount + "</span>");
            }
            var admin = "<%=wxUser.wxOpenID%>";
            if (admin == "okt9_5uZ8gcEHOaMFbOmrpvorAZI" || admin == "okt9_5oTsjUBEelCCPrBNqBgqoHE" || admin == "okt9_5rlmb0wRZtvr_5cgHX9eAZM") {
                $('#sec').show();
                $('#tao').show();

            } else {
                $('#sec').hide();
                $('#tao').hide();
            }
            $.ajax("myAPI.aspx?method=QueryWeixinUserOrderInfo", {
                async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                timeout: 15000, //15秒超时
                success: function (obj) {
                    $('#unPay').html(obj.UnConfirmNum);
                    $('#UnSend').html(obj.UnSendNum);
                    $('#UnAccept').html(obj.UnAcceptNum);
                }
            });
        });
    </script>
</body>
</html>
