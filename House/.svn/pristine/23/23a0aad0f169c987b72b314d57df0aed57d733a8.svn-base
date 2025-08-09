<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountManager.aspx.cs" Inherits="Cargo.Weixin.AccountManager" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月对账单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <style type="text/css">
        .content {
            width: 100%;
            overflow: hidden;
        }

        .toggle {
            width: 100%;
        }

            .toggle dl dt {
                background: #f6fbff no-repeat scroll 8px 8px;
                width: 100%;
                font-size: 11px;
                color: #006600;
                cursor: pointer;
                margin: 4px 0;
                padding-left: 5px;
                display: block;
            }

                .toggle dl dt.current {
                    background: #F4FFF4 url('images/bg_toggle_down.gif') no-repeat scroll 8px 8px;
                }

            .toggle dl dd {
                padding-left: 10px;
                line-height: 24px;
            }

                .toggle dl dd h2 {
                    font-size: 15px;
                }

                .toggle dl dd ul {
                    padding-bottom: 12px;
                }

                    .toggle dl dd ul li {
                        list-style: decimal inside none;
                    }
    </style>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".toggle dl dd").hide();
            $(".toggle dl dt").click(function () {
                $(".toggle dl dd").not($(this).next()).hide();
                $(".toggle dl dt").not($(this).next()).removeClass("current");
                $(this).next().slideToggle(500);
                $(this).toggleClass("current");
            });
        });
    </script>
</head>
<body ontouchstart>
    <div class='weui-content'>
        <div class="wy-center-top">
            <div class="weui-media-box weui-media-box_appmsg">
                <asp:Literal ID="ltlTop" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="weui-tab">
            <div class="weui-navbar" style="left: 0; right: 0; background: #fff;">
                <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab1">未付订单</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab2">本月退货</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab3">返利明细</a>
            </div>
            <div class="weui-tab__bd proinfo-tab-con">
                <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <asp:Literal ID="ltlCurOrder" runat="server"></asp:Literal>
                </div>
                <div id="tab2" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlCurReturn" runat="server"></asp:Literal>
                </div>
                <div id="tab3" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlRebate" runat="server"></asp:Literal>
                </div>
            </div>
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
                            window.location.reload();
                            //清空缓存
                        } else {
                            $.toast("付款失败!");
                        }
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
