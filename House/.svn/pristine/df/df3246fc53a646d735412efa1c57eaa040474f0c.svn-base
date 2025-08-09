<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="secKill.aspx.cs" Inherits="Cargo.Weixin.secKill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>全民战“疫”爱车杀菌消毒在行动</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="全民战“疫”爱车杀菌消毒在行动" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <script src="WeUI/JS/jweixin-1.6.0.js"></script>

    <style type="text/css">
        table {
            border-collapse: collapse;
            margin: 0 auto;
            text-align: center;
            width: 100%;
            font-size: 16px;
        }

            table td, table th {
                border: 0px solid #cad9ea;
                color: #666;
                height: 30px;
            }

            table th {
                background-color: #CCE8EB;
                width: 100px;
            }

            table tr:nth-child(odd) {
                background: #fff;
            }

            table tr:nth-child(even) {
                background: #F5FAFA;
            }

        .weui-share {
            position: fixed;
            left: 0;
            top: 0;
            z-index: 9999;
            width: 100%;
            height: 100%;
            cursor: pointer;
            background: rgba(0, 0, 0, 0.75);
        }

            .weui-share .weui-share-box {
                position: absolute;
                top: 65px;
                right: 20px;
                width: 140px;
                padding: 10px;
                color: #fff;
                font-size: 20px;
                line-height: 30px;
                border: 2px solid;
                -webkit-border-radius: 10px;
                border-radius: 10px;
            }

            .weui-share i {
                position: absolute;
                background: url(WeUI/image/wxShare.png) no-repeat 90% 5px;
                -webkit-background-size: 56px 61px;
                background-size: 56px 61px;
                width: 56px;
                height: 61px;
                top: -63px;
                right: 23px;
            }
    </style>
</head>
<body ontouchstart>
    <div class="weui-content">
        <!--产品详情-->
        <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
            <!--主图轮播-->
            <div class="swiper-container swiper-zhutu">
                <div class="swiper-wrapper">
                    <div class="swiper-slide">
                        <img src="WeUI/image/topkill.jpg" />
                    </div>

                </div>
                <div class="swiper-pagination swiper-zhutu-pagination"></div>
            </div>
            <div class="wy-media-box-nomg weui-media-box_text">

                <h4 class="wy-media-box__title" style="text-align: center; font-weight: bolder; color: red; font-size: 25px;">
                    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h4>
                <%--   <div class="wy-pro-pri mg-tb-5">¥<em class="num font-20">296.00</em></div>
                <p class="weui-media-box__desc">正品保证 405城万家门店 25仓发货 包安装</p>--%>
            </div>

            <div class="wy-media-box2 weui-media-box_text">
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd">
                        <div class="promotion-sku clear" style="font-size: 20px; font-weight: bolder; color: red; text-align: center;">
                            登记信息领取优惠活动
                        </div>
                    </div>
                </div>
                <div class="weui-cells weui-cells_form" style="font-size: 15px;">
                    <div class="weui-cell">
                        <div class="weui-cell__hd">
                            <label class="weui-label">车牌号码：</label>
                        </div>
                        <div class="weui-cell__bd">
                            <input class="weui-input" id="CarNum" type="text" value="" placeholder="请输入车牌号码" />
                        </div>
                    </div>
                    <div class="weui-cell">
                        <div class="weui-cell__hd">
                            <label class="weui-label">手机号码：</label>
                        </div>
                        <div class="weui-cell__bd">
                            <input class="weui-input" id="Cellphone" type="number" value="" pattern="[0-9]*" placeholder="请输入手机号码" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="wy-media-box2 txtpd weui-media-box_text">
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd">
                        <div class="promotion-sku clear" style="font-size: 20px; font-weight: bolder; color: red; text-align: center;">
                            门店信息
                        </div>
                    </div>
                </div>
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd" style="font-size: 17px;">
                        <asp:Literal ID="ltlShop" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="wy-media-box2 txtpd weui-media-box_text">
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd">
                        <div class="promotion-sku clear" style="font-size: 20px; font-weight: bolder; color: red; text-align: center;">
                            活动说明
                        </div>
                    </div>
                </div>
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd" style="font-size: 16px; line-height: 25px;">
                        <asp:Literal ID="ltlActive" runat="server"></asp:Literal>

                    </div>
                </div>
            </div>


            <div class="wy-media-box2 txtpd weui-media-box_text">
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd">
                        <div class="promotion-sku clear" style="font-size: 20px; font-weight: bolder; color: red; text-align: center;">
                            已登记车主
                        </div>
                    </div>
                </div>
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd" style="font-size: 14px;">
                        <table>
                            <tr>
                                <th>手机号码</th>
                                <th>车牌号码</th>
                                <th>登记状态</th>
                                <th>登记时间</th>
                            </tr>
                        </table>

                        <div id="header_demo" style="overflow:hidden;height:150px;">
                            <div id="mov">
                            </div>
                            <div id="header_demo2"></div>
                        </div>
                      <%--  <marquee direction="up" scrollamount="2px" height="150px">
                           <div  id="mov" style="-webkit-overflow-scrolling : touch;overflow:hidden;"></div>

                        </marquee>--%>
                    </div>
                </div>
            </div>
            <div class="wy-media-box2 txtpd weui-media-box_text">
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__bd">
                        <div class="promotion-sku clear" style="font-size: 20px; font-weight: bolder; color: red; text-align: center;">
                            我的邀请
                        </div>
                    </div>
                </div>
                <div class="weui-panel__bd">
                    <div class="weui-flex">
                        <div class="weui-flex__item">
                            <div class="center-ordersModule">
                                <div class="imgicon" style="color: grey;">
                                    已登记
                                </div>
                                <div class="name" style="font-size: 20px;">
                                    <asp:Literal ID="ltlReg" runat="server"></asp:Literal>&nbsp;人
                                </div>
                            </div>
                        </div>
                        <div class="weui-flex__item">
                            <div class="center-ordersModule">
                                <div class="imgicon" style="color: grey;">
                                    已领取
                                </div>
                                <div class="name" style="font-size: 20px;">
                                    <asp:Literal ID="ltlGet" runat="server"></asp:Literal>&nbsp;人
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="foot-black"></div>
    <div class="weui-tabbar wy-foot-menu">
        <%-- <a href="javascript:;" class="weui-btn weui-btn_primary" onclick="isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre')">关注</a>--%>
        <a href='javascript:;' class='weui-tabbar__item yellow-color open-popup' onclick="share()">
            <p class='promotion-foot-menu-label'>分享给好友</p>
        </a>
        <a href='javascript:saveOrder();' class='weui-tabbar__item red-color open-popup'>
            <p class='promotion-foot-menu-label' style="font-size: 18px;">&nbsp;&nbsp;立&nbsp;即&nbsp;报&nbsp;名&nbsp;&nbsp;</p>
        </a>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/swiper.js"></script>
    <script type="text/javascript">
        //分享
        function share() {
            var company = "<%=company%>";
            var pid = "<%=WxUserInfo.ID%>";
            if (company == "1") {
                //信达汽修
                wx.onMenuShareAppMessage({
                    title: '汽车杀菌消毒 助力疫情防控', // 分享标题
                    desc: '1折领取汽车杀菌消毒名额', // 分享描述
                    link: "http://dlt.neway5.com/Weixin/secKill.aspx?Company=1&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/topkill.jpg', // 分享图标
                    success: function () {
                        // 设置成功
                        //分享给好友
                        $.ajax({
                            type: "POST", dataType: "json", data: { Type: "0", Company: "1" },
                            url: 'myAPI.aspx?method=AddSecStatisData',
                            success: function (rs) { }
                        });
                        isfollowqr('wxPic/xdqx.jpg');
                    }
                });
                wx.onMenuShareTimeline({
                    title: '汽车杀菌消毒 助力疫情防控', // 分享标题
                    desc: '1折领取汽车杀菌消毒名额', // 分享描述
                    link: "http://dlt.neway5.com/Weixin/secKill.aspx?Company=1&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/topkill.jpg', // 分享图标
                    success: function () {
                        // 设置成功 分享朋友圈
                        //alert('朋友圈');
                        $.ajax({
                            type: "POST", dataType: "json", data: { Type: "1", Company: "1" },
                            url: 'myAPI.aspx?method=AddSecStatisData',
                            success: function (rs) { }
                        });
                        isfollowqr('wxPic/xdqx.jpg');
                    }
                });
            } else if (company == "2") {
                wx.onMenuShareAppMessage({
                    title: '新老客户注意啦，免费福利来了！', // 分享标题
                    desc: '价值128元的车内杀菌消毒一次，价值60元全车安全检测一次', // 分享描述
                    link: "http://dlt.neway5.com/Weixin/secKill.aspx?Company=2&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/topkill.jpg', // 分享图标
                    success: function () {
                        // 设置成功
                        //分享给好友
                        $.ajax({
                            type: "POST", dataType: "json", data: { Type: "0", Company: "2" },
                            url: 'myAPI.aspx?method=AddSecStatisData',
                            success: function (rs) { }
                        });
                        isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                    }
                });
                wx.onMenuShareTimeline({
                    title: '新老客户注意啦，免费福利来了！', // 分享标题
                    desc: '价值128元的车内杀菌消毒一次，价值60元全车安全检测一次', // 分享描述
                    link: "http://dlt.neway5.com/Weixin/secKill.aspx?Company=2&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/topkill.jpg', // 分享图标
                    success: function () {
                        // 设置成功 分享朋友圈
                        //alert('朋友圈');
                        $.ajax({
                            type: "POST", dataType: "json", data: { Type: "1", Company: "2" },
                            url: 'myAPI.aspx?method=AddSecStatisData',
                            success: function (rs) { }
                        });
                        isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                    }
                });
            } else if (company == "3") {
                //车轮馆
                wx.onMenuShareAppMessage({
                    title: '爱车有我车轮馆，优惠不断等您来！', // 分享标题
                    desc: '免费价值68元车内车外精洗一次，更有6折优惠服务等着您！', // 分享描述
                    link: "http://dlt.neway5.com/Weixin/secKill.aspx?Company=3&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/topkill.jpg', // 分享图标
                    success: function () {
                        // 设置成功
                        //分享给好友
                        $.ajax({
                            type: "POST", dataType: "json", data: { Type: "0", Company: "3" },
                            url: 'myAPI.aspx?method=AddSecStatisData',
                            success: function (rs) { }
                        });
                        isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                    }
                });
                wx.onMenuShareTimeline({
                    title: '爱车有我车轮馆，优惠不断等您来！', // 分享标题
                    desc: '免费价值68元车内车外精洗一次，更有6折优惠服务等着您！', // 分享描述
                    link: "http://dlt.neway5.com/Weixin/secKill.aspx?Company=3&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/topkill.jpg', // 分享图标
                    success: function () {
                        // 设置成功 分享朋友圈
                        //alert('朋友圈');
                        $.ajax({
                            type: "POST", dataType: "json", data: { Type: "1", Company: "3" },
                            url: 'myAPI.aspx?method=AddSecStatisData',
                            success: function (rs) { }
                        });
                        isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                    }
                });
            }

            var sharetpl = '<div class="weui-share" onclick="$(this).remove();">\n' +
       '<div class="weui-share-box">\n' +
       '点击右上角发送给指定朋友或分享到朋友圈 <i></i>\n' +
       '</div>\n' +
       '</div>';
            var sharetpl = $.t7.compile(sharetpl);
            $("body").append(sharetpl());
        }
        function isfollowqr(url) {//是否关注二维码
            var div = document.createElement('div');
            div.style = 'position: fixed; left:0; top:0; background: rgba(0,0,0,0.7); filter:alpha(opacity=70); width: 100%; height:100%; z-index: 100;';
            div.innerHTML = '<p style="text-align:center; margin-top:20%;padding:0 5%;"><img style="max-width:100%;" src="' + url + '" alt="请您关注后浏览"></p><p style="text-align:center;line-height:20px; color:#fff;margin:0px;padding:0px;">长按二维码选择识别二维码,关注本公众号</p>';
            document.body.appendChild(div);
        }
        function configjssdk() {
            var weixinUrl = location.href.split('#')[0];//;
            //配置jssdk
            $.ajax({
                type: "post", dataType: "json", data: { Url: weixinUrl },
                url: "myAPI.aspx?method=configJssdk",
                cache: false, ifModified: true, async: false,
                success: function (msg) {
                    var json = eval(msg);
                    var config = {};
                    config.beta = true;
                    config.appId = json.appId;
                    config.nonceStr = json.nonceStr;
                    config.signature = json.signature;
                    config.debug = false;        // 添加你需要的JSSDK的权限
                    config.jsApiList = ['updateAppMessageShareData', 'updateTimelineShareData', 'onMenuShareTimeline', 'onMenuShareAppMessage'];
                    config.timestamp = parseInt(json.timestamp);
                    wx.config(config);
                    wx.ready(function () {
                        //alert("jssdk配置成功");
                        //wx.config(config);
                    });
                    wx.error(function (res) {
                        alert(JSON.stringify(res));
                    });
                }
            })
        }
        $(function () {
            FastClick.attach(document.body);
            configjssdk();
            QueryQueen();
            //滚动头条
            //滚动效果
            var header_demo = document.getElementById("header_demo");
            var header_demo1 = document.getElementById("mov");
            var header_demo2 = document.getElementById("header_demo2");
            var speed = 30;    //数值越大滚动速度越慢
            header_demo2.innerHTML = header_demo1.innerHTML;

            function Marquee() {
                if (header_demo2.offsetTop - header_demo.scrollTop <= 0)
                    header_demo.scrollTop -= header_demo1.offsetHeight;
                else {
                    header_demo.scrollTop++;
                }
            }
            var MyMar = setInterval(Marquee, speed);
            //header_demo.onmouseover = function () { clearInterval(MyMar) }
            //header_demo.onmouseout = function () { MyMar = setInterval(Marquee, speed) }
        });

        function QueryQueen() {
            var company = "<%=company%>";
            $.ajax({
                type: "POST", dataType: "json", data: { company: company },
                url: 'myAPI.aspx?method=QuerySecKillData',
                success: function (rs) {
                    var ht = "<table>";
                    for (var i = 0; i < rs.length; i++) {
                        ht += "<tr><td>" + rs[i].Cellphone + "</td><td>" + rs[i].CarNum + "</td><td>" + rs[i].UseStatus + "</td><td>" + rs[i].ReceiveTime + "</td></tr>";
                    }
                    ht += "</table>";
                    $('#mov').append(ht);
                }
            });
        }
        $(".swiper-zhutu").swiper({
            loop: false,
            paginationType: 'fraction',
            autoplay: 5000
        });
        function saveOrder() {
            var CarNum = $("#CarNum").val();
            var Cellphone = $("#Cellphone").val();
            if (CarNum == undefined || CarNum == "") {
                $.toptip('您的车牌号不能为空', 'warning');
                return;
            }
            if (Cellphone == undefined || Cellphone == "") {
                $.toptip('您的手机号码不能为空', 'warning');
                return;
            }
            var ParentID = "<%=ParentID%>";
            var company = "<%=company%>";
            var str = { CarNum: CarNum, Cellphone: Cellphone, ParentID: ParentID, Company: company }
            var json = JSON.stringify([str])
            $.ajax({
                url: 'myAPI.aspx?method=SaveSecKill',
                type: 'post', dataType: 'json', data: { data: json },
                success: function (text) {
                    if (text.Result == true) {
                        $.toast("领取成功!");
                        if (company == "1") {
                            isfollowqr('wxPic/xdqx.jpg');
                        } else if (company == "2") {
                            isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                        } else if (company == "3") {
                            isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                        }
                    }
                    else {
                        $.toast('领取失败：' + text.Message);
                    }
                }
            });
        }
    </script>
</body>
</html>
