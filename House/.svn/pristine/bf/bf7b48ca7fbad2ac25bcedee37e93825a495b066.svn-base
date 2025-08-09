<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payOrder.aspx.cs" Inherits="Cargo.Weixin.payOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单付款</title>
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
    <header class="wy-header">
        <div class="wy-header-icon-back"><span></span></div>
        <div class="wy-header-title">订单付款</div>
    </header>
    <div class="weui-content">
        <div class="wy-media-box weui-media-box_text">
            <asp:Literal ID="ltlOrder" runat="server"></asp:Literal>
            <div class="mg10-0"><a href="javascript:callpay();" class="weui-btn weui-btn_primary">微信付款</a></div>
            <br />
            <div class="mg10-0" id="limitdiv"><a href="javascript:limitPay();" class="weui-btn weui-btn_primary">信用额度付款</a></div>
        </div>
    </div>

    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script type="text/javascript">
        function limitPay() {
            var order = '<%=orderNo%>';
            $.confirm("您是否使用额度支付?", "额度支付", function () {
                $.ajax({
                    url: 'myAPI.aspx?method=saveWxOrderPayStatus',
                    type: 'post', dataType: 'json', data: { data: order },
                    success: function (text) {
                        if (text.Result == true) {
                            $.alert("付款成功!");
                            localStorage.removeItem("CART");
                            localStorage.removeItem("ORDER");
                            window.location.href = "my.aspx";
                        }
                        else {
                            //$.toast("付款失败!");
                            $.alert('付款失败：' + text.Message);
                        }
                    }
                });
            }, function () { });
        }
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
                            localStorage.removeItem("CART");
                            localStorage.removeItem("ORDER");
                            $.alert("付款成功!");
                            window.location.href = "my.aspx";
                            //清空缓存
                        } else {
                            $.alert("付款失败!");
                        }
                    });
            }
            $(function () {
                FastClick.attach(document.body);
                var Lm = '<%=Lm%>';
                var HID = '<%=HouseID%>';
                if (HID == '9' || HID == '12' || HID == '34') {
                    $('#limitdiv').hide();
                } else {
                    if (Lm == '1') {
                        $('#limitdiv').show();
                    } else if (Lm == '2') {
                        $.toptip('您本月尚有未结清账单，额度支付已冻结！请尽快结清账单，谢谢！', 10000, 'warning');
                        $('#limitdiv').hide();
                    }
                    else {
                        $('#limitdiv').hide();
                    }
                }
            });
    </script>
    <script src="WeUI/JS/jquery-weui.js"></script>
</body>
</html>
