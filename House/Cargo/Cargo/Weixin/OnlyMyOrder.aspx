<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlyMyOrder.aspx.cs" Inherits="Cargo.Weixin.OnlyMyOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的订单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />

</head>
<body ontouchstart>
    <div class='weui-content'>
        <asp:Literal ID="ltlUnPay" runat="server"></asp:Literal>
        <%-- <div class="weui-tab">
            <div class="weui-navbar" style="position: fixed; top: 0px; left: 0; right: 0; height: 44px; background: #fff;">
                <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab1">未付款</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab2">已付款</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab3">已签收</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab4">异常</a>
                <%-- <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab5">待评价</a>
            </div>
            <div class="weui-tab__bd proinfo-tab-con" style="padding-top: 43px;">
                <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <asp:Literal ID="ltlUnPay" runat="server"></asp:Literal>
                </div>
                <div id="tab2" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlPay" runat="server"></asp:Literal>
                </div>
                <div id="tab3" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlSign" runat="server"></asp:Literal>
                </div>
                <div id="tab4" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlAbnormal" runat="server"></asp:Literal>
                </div>
                 <div id="tab5" class="weui-tab__bd-item">
                </div>
            </div>
        </div>--%>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
</body>
</html>
