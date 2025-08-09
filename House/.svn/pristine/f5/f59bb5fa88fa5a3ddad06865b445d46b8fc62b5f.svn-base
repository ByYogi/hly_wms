<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScanQrPayOrder.aspx.cs" Inherits="Cargo.Weixin.ScanQrPayOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扫描二维码订单付款</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="WeUI/CSS/weuix.min.css" rel="stylesheet" />
    
</head>
<body>
    <%-- <header class="wy-header">
        <div class="wy-header-icon-back"><span></span></div>
        <div class="wy-header-title">订单付款</div>
    </header>--%>
    <div class="weui-pay">
        <h1 class="weui-payselect-title">订 单 金 额</h1>
        <%--<p class="weui-pay-num">￥15.00</p>--%>
        <asp:Literal ID="ltlCharge" runat="server"></asp:Literal>
        <div class="weui-loadmore weui-loadmore_line weui-loadmore_dot" style="margin-top: 0px; margin-bottom: 0px;">
            <span class="weui-loadmore__tips"></span>
        </div>
        <ul class="weui-pay-u">
            <asp:Literal ID="ltlOrder" runat="server"></asp:Literal>
        </ul>
        <div class="pay-div" id="pay">
            <a href="javascript:callpay();" class="weui-btn weui-btn_primary">立即支付</a>
        </div>
    </div>
    <%--  <div class="weui-content">
        <div class="wy-media-box weui-media-box_text">
            <asp:Literal ID="ltlOrder" runat="server"></asp:Literal>
            <div class="mg10-0"><a href="javascript:callpay();" class="weui-btn weui-btn_primary">微信付款</a></div>
            <br />
            <div class="mg10-0" id="limitdiv"><a href="javascript:limitPay();" class="weui-btn weui-btn_primary">信用额度付款</a></div>
        </div>
    </div>--%>

    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        //调用微信JS api 支付
        function jsApiCall() {
            WeixinJSBridge.invoke(
                'getBrandWCPayRequest', {
                    "appId": '<%=appId%>',     //公众号名称，由商户传入     
                    "timeStamp": '<%=timeStamp%>',         //时间戳，自1970年以来的秒数     
                    "nonceStr": '<%=nonceStr%>', //随机串     
                    "package": '<%=package%>',
                    "signType": "MD5",         //微信签名方式：     
                    "paySign": '<%=paySign%>' //微信签名 
                },
                    function (res) {
                        if (res.err_msg == "get_brand_wcpay_request:ok") {
                            // 使用以上方式判断前端返回,微信团队郑重提示：
                            //res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                            //$.alert("付款成功!");
                            $.toast("付款成功!");
                            isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
                            //WeixinJSBridge.call('closeWindow');
                            //window.location.href = "my.aspx";
                            //清空缓存
                        } else {
                            $.alert("付款失败!");
                        }
                    });
            }
            $(function () {
                FastClick.attach(document.body);
                configjssdk();
                var Lm = "<%=Lm%>";
                $('#pay').show();
                if (Lm == "1") { $('#pay').hide(); }
                //isfollowqr('http://open.weixin.qq.com/qr/code?username=dltchinatyre');
            });
            function callpay() {
                //window.location.href = "my.aspx";
                if (typeof WeixinJSBridge == "undefined") {
                    if (document.addEventListener) {
                        document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                    }
                    else if (document.attachEvent) {
                        document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                        document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                    }
                }
                else {
                    jsApiCall();
                }
            }
            function isfollowqr(url) {//是否关注二维码
                var div = document.createElement('div');
                div.style = 'position: fixed; left:0; top:0; background: rgba(0,0,0,0.7); filter:alpha(opacity=70); width: 100%; height:100%; z-index: 100;';
                div.innerHTML = '<p style="text-align:center; margin-top:20%;padding:0 5%;"><img style="max-width:100%;width: 50%;" src="' + url + '" alt="请您关注后浏览"></p><p style="text-align:center;line-height:20px; color:#fff;margin:0px;padding:0px;">长按二维码选择识别二维码,关注本公众号</p>';
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
    </script>
</body>
</html>
