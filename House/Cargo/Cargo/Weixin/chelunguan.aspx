<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chelunguan.aspx.cs" Inherits="Cargo.Weixin.chelunguan" %>

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
            font-size: 14px;
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
    <%--  <div class="weui-content" style="background-image: url(wxPic/bg.jpg)">--%>
    <div class="weui-content">
        <img src="WeUI/image/topkill1.jpg" style="width: 100%" />
        <%-- <img src="wxPic/ling.jpg" style="width: 100%;" />--%>
        <div style="width: 100%; font-size: 25px; text-align: center; border-bottom: dotted 1px;">登记信息领取优惠</div>
        <table border="1px">
            <tr>
                <td style="width: 40%; text-align: right; height: 40px;">车牌号码：</td>
                <td>
                    <input class="weui-input" id="CarNum" type="text" value="" placeholder="请输入车牌号码" /></td>
            </tr>

            <tr>
                <td style="width: 40%; text-align: right; height: 40px;">手机号码：</td>
                <td>
                    <input class="weui-input" id="Cellphone" type="number" value="" pattern="[0-9]*" placeholder="请输入手机号码" /></td>
            </tr>

        </table>
        <%--<img src="wxPic/men.jpg" style="width: 100%;" />--%>
        <div style="width: 100%; font-size: 25px; text-align: center; border-bottom: dashed 0px; margin-top: 10px;">门店地址</div>
        <table border="1px">
            <tr>
                <td width='20%'>店名：</td>
                <td>广州车轮馆&nbsp;&nbsp;<a href='https://apis.map.qq.com/tools/poimarker?type=1&keyword=广州车轮馆汽车服务有限公司&center=23.258037,113.317513&radius=1000&key=7U4BZ-YDV3D-54E4H-PDUXL-G3TM3-EUBWZ&referer=myapp' style='border: 1px solid #d9d9d9; background-color: #6ceab4; cursor: pointer; padding-left: 3px; padding-right: 3px; color: black; font-size: 12px;'>打开地图</a></td>
            </tr>
            <tr>
                <td>电话：</td>
                <td><a href='tel:15986386161'>15986386161</a>，<a href='tel:13631442958'>13631442958</a></td>
            </tr>
            <tr>
                <td>地址：</td>
                <td>广东省广州市白云区东平横岗东路自编3号</td>
            </tr>
        </table>
        <%--<img src="wxPic/chezhu.jpg" style="width: 100%;" />--%>
        <div style="width: 100%; font-size: 25px; text-align: center; border-bottom: dotted 1px; margin-top: 10px;">已登记车主</div>
        <table>
            <tr>
                <th>手机号码</th>
                <th>车牌号码</th>
                <th>登记状态</th>
                <th>登记时间</th>
            </tr>
        </table>

        <div id="header_demo" style="overflow: hidden; height: 150px;">
            <div id="mov">
            </div>
            <div id="header_demo2"></div>
        </div>
        <%-- <img src="wxPic/yao.jpg" style="width: 100%;" />--%>
        <div style="width: 100%; font-size: 25px; text-align: center; border-bottom: dotted 1px; margin-top: 10px;">我的邀请</div>
        <table>
            <tr>
                <td style="text-align: center; font-size: 14px; width: 50%">已登记</td>
                <td style="text-align: center; font-size: 14px; width: 50%">已领取</td>
            </tr>

            <tr>
                <td style="font-size: 25px; color: red">
                    <asp:Literal ID="ltlReg" runat="server"></asp:Literal>&nbsp;人</td>
                <td style="font-size: 25px; color: red">
                    <asp:Literal ID="ltlGet" runat="server"></asp:Literal>&nbsp;人</td>
            </tr>
        </table>



        <div style="width: 100%; font-size: 25px; text-align: center; border-bottom: dotted 1px;">活动照片</div>
        <%--  <img src="wxPic/huodong.jpg" style="width: 100%;" />--%>
        <img src="wxPic/huo1.jpg" style="width: 100%" /><img src="wxPic/huo2.jpg" style="width: 100%" /><img src="wxPic/huo3.jpg" style="width: 100%" /><img src="wxPic/huo4.jpg" style="width: 100%" />
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

            //车轮馆
            wx.onMenuShareAppMessage({
                title: '来车轮馆，抢大礼，千元大礼等你来！', // 分享标题
                desc: '全民抗疫，车轮馆礼品免费送。到店咨询，更多精彩活动送不停！', // 分享描述
                link: "http://dlt.neway5.com/Weixin/chelunguan.aspx?Company=3&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/clg.jpg', // 分享图标
                success: function () {
                    // 设置成功
                    //分享给好友
                    $.ajax({
                        type: "POST", dataType: "json", data: { Type: "0", Company: "3" },
                        url: 'myAPI.aspx?method=AddSecStatisData',
                        success: function (rs) { }
                    });
                    $.alert("分享成功", "消息提示", function () { });
                    //$.toast("分享成功!");
                    //isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                }
            });
            wx.onMenuShareTimeline({
                title: '来车轮馆，抢大礼，千元大礼等你来！', // 分享标题
                desc: '全民抗疫，车轮馆礼品免费送。到店咨询，更多精彩活动送不停！', // 分享描述
                link: "http://dlt.neway5.com/Weixin/chelunguan.aspx?Company=3&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/clg.jpg', // 分享图标
                success: function () {
                    // 设置成功 分享朋友圈
                    //alert('朋友圈');
                    $.ajax({
                        type: "POST", dataType: "json", data: { Type: "1", Company: "3" },
                        url: 'myAPI.aspx?method=AddSecStatisData',
                        success: function (rs) { }
                    });
                    $.alert("分享成功", "消息提示", function () { });
                    //$.toast("分享成功!");
                    //isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                }
            });


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
            loadu();
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

        function loadu() {
            var company = "<%=company%>";
            var pid = "<%=WxUserInfo.ID%>";

            //车轮馆
            wx.onMenuShareAppMessage({
                title: '来车轮馆，抢大礼，千元大礼等你来！', // 分享标题
                desc: '全民抗疫，车轮馆礼品免费送。到店咨询，更多精彩活动送不停！', // 分享描述
                link: "http://dlt.neway5.com/Weixin/chelunguan.aspx?Company=3&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/clg.jpg', // 分享图标
                success: function () {
                    // 设置成功
                    //分享给好友
                    $.ajax({
                        type: "POST", dataType: "json", data: { Type: "0", Company: "3" },
                        url: 'myAPI.aspx?method=AddSecStatisData',
                        success: function (rs) { }
                    });
                    $.alert("分享成功", "消息提示", function () { });
                    //$.toast("分享成功!");
                    //isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                }
            });
            wx.onMenuShareTimeline({
                title: '来车轮馆，抢大礼，千元大礼等你来！', // 分享标题
                desc: '全民抗疫，车轮馆礼品免费送。到店咨询，更多精彩活动送不停！', // 分享描述
                link: "http://dlt.neway5.com/Weixin/chelunguan.aspx?Company=3&ParentID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/clg.jpg', // 分享图标
                success: function () {
                    // 设置成功 分享朋友圈
                    //alert('朋友圈');
                    $.ajax({
                        type: "POST", dataType: "json", data: { Type: "1", Company: "3" },
                        url: 'myAPI.aspx?method=AddSecStatisData',
                        success: function (rs) { }
                    });
                    $.alert("分享成功", "消息提示", function () { });
                    //$.toast("分享成功!");
                    //isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                }
            });
        }

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
                //$.toptip('您的车牌号不能为空', 'warning');
                $.alert("您的车牌号不能为空", "消息提示", function () { });
                return;
            }
            if (Cellphone == undefined || Cellphone == "") {
                $.alert("您的手机号码不能为空", "消息提示", function () { });
                //$.toptip('您的手机号码不能为空', 'warning');
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
                        //$.toast("登记成功!");
                        $.alert("报名登记成功", "消息提示", function () { });
                        //share();
                        isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                    }
                    else {
                        //$.toast('登记失败：' + text.Message);
                        $.alert("登记失败", "消息提示", function () { });
                    }
                }
            });
        }
    </script>
</body>
</html>
