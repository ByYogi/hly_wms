<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlyMyQrcode.aspx.cs" Inherits="Cargo.Weixin.OnlyMyQrcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的专属二维码</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <script src="WeUI/JS/jweixin-1.6.0.js"></script>

    <style type="text/css">
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
    <div class='weui-content'>
        <div class="wy-center-top">
            <div class="weui-media-box weui-media-box_appmsg">
                <div class="weui-media-box__hd">
                    <img class="weui-media-box__thumb radius" src="<%=WxUserInfo.AvatarBig %>" alt="">
                </div>
                <div class="weui-media-box__bd">
                    <h4 class="weui-media-box__title user-name">编号：<%=WxUserInfo.ID %></h4>
                    <h4 class="weui-media-box__title user-name">微信名：<%=WxUserInfo.wxName %></h4>
                    <%--<p class="user-grade">等级：普通会员</p>--%>
                    <%--   <p class="user-integral">待返还金额：<em class="num">500.0</em>元</p>--%>
                </div>
            </div>
        </div>
    </div>
    <div style="text-align: center; padding-top: 20px;">
        <img id="qrc" src="" width="50%" height="40%" />
    </div>
    <div class="weui-tabbar wy-foot-menu">
        <a href='javascript:;' class='weui-tabbar__item yellow-color open-popup' onclick="share()">
            <p class='promotion-foot-menu-label'>分享给好友</p>
        </a>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        //分享
        function share() {
            var pid = "<%=WxUserInfo.ID%>";
            wx.onMenuShareAppMessage({
                title: '我的专属二维码', // 分享标题
                desc: '关注迪乐泰轮胎公众号购物返利', // 分享描述
                link: "http://dlt.neway5.com/Weixin/wxQRCode.aspx?PID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/fanli.jpg', // 分享图标
                success: function () {
                    //alert('朋友');
                }
            });
            wx.onMenuShareTimeline({
                title: '我的专属二维码', // 分享标题
                desc: '关注迪乐泰轮胎公众号购物返利', // 分享描述
                link: "http://dlt.neway5.com/Weixin/wxQRCode.aspx?PID=" + pid, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: 'http://dlt.neway5.com/Weixin/WeUI/image/fanli.jpg', // 分享图标
                success: function () {
                    // 设置成功 分享朋友圈
                    //alert('朋友圈');
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
        $(document).ready(function () {
            var BID = "<%=BID%>";
            var url = "http://dlt.neway5.com/Weixin/wxQRCode.aspx?PID=" + BID;
            //alert(url);
            $('#qrc').attr('src', "myAPI.aspx?method=MakeQRCode&Code=" + url);
            configjssdk();
        });
    </script>
</body>
</html>
