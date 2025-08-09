<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" Inherits="Cargo.Weixin.OrderInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单详细</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
</head>
<body>
    <div class="weui-content">
        <!--客户收货地址-->
        <div class="wy-media-box weui-media-box_text address-select">
            <div class="weui-media-box_appmsg">
                <div class="weui-media-box__hd proinfo-txt-l" style="width: 20px;">
                    <span class="promotion-label-tit">
                        <img src="WeUI/image/icon_nav_city.png" /></span>
                </div>
                
                <asp:Literal ID="ltlAddress" runat="server"></asp:Literal>
                <div class="weui-media-box__hd proinfo-txt-l" style="width: 16px;">
                    <div class="weui-cell_access"><span class="weui-cell__ft"></span></div>
                </div>
            </div>
        </div>
        <asp:Literal ID="ltlAllOrder" runat="server"></asp:Literal>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);
        });
        function PayMoney(no) {
            window.location.href = "AgainPayOrder.aspx?OrderNo=" + no;
        }
        function DeleteOrder(no) {
            $.confirm("您确定要删除此订单吗?", "确认删除?", function () {
                $.ajax({
                    url: 'myAPI.aspx?method=DeleteWeixinOrder',
                    type: 'post', dataType: 'json', data: { data: no },
                    success: function (text) {
                        if (text.Result == true) {
                            $.alert("删除成功!");
                            refresh();
                        }
                        else {
                            $.alert('删除失败 失败原因：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });
        }
        function refresh() {
            var random = Math.floor((Math.random() * 10000) + 1);
            var url = decodeURI(window.location.href);
            if (url.indexOf('?') < 0) {
                url = url + "?random" + random;
            } else {
                url = url.substr(0, url.indexOf('?random')) + "?random" + random;
            }
            window.location.href = url;
        }
        //确认收货
        function sure(no) {
            $.ajax({
                url: 'myAPI.aspx?method=setWeixinOrderOk',
                type: 'post', dataType: 'json', data: { data: no },
                success: function (text) {
                    if (text.Result == true) {
                        $.alert("收货完成!");
                        window.location.reload();
                    }
                    else {
                        $.alert('收货失败 失败原因：' + text.Message);
                    }
                }
            });
        }
    </script>
    <script src="WeUI/JS/jquery-weui.js"></script>
</body>
</html>
