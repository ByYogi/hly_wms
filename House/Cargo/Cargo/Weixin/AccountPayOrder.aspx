<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPayOrder.aspx.cs" Inherits="Cargo.Weixin.AccountPayOrder" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>账单付款</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
</head>
<body>
    <!--主体-->
    <%--<header class="wy-header">
         <div class="wy-header-icon-back"><span></span></div>
        <div class="wy-header-title">微信付款</div>
    </header>--%>
    <div class="weui-content">
        <div class="wy-media-box weui-media-box_text">
            <asp:Literal ID="ltlOrder" runat="server"></asp:Literal>
            <div class="mg10-0"><a href="javascript:callpay();" class="weui-btn weui-btn_primary">微信付款</a></div>
        </div>
    </div>

    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script type="text/javascript">
        function callpay() {
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
                            //localStorage.removeItem("CART");
                            //localStorage.removeItem("ORDER");
                            $.toast("付款成功!");
                            window.location.href = "my.aspx";
                            //清空缓存
                        } else {
                            $.toast("付款失败!");
                        }
                        //WeixinJSBridge.log(res.err_msg);
                        //alert(res.err_code + res.err_desc + res.err_msg);
                    }
                    );
       }
       $(function () {
           FastClick.attach(document.body);
       });
    </script>
    <script src="WeUI/JS/jquery-weui.js"></script>
</body>
</html>
