<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myOrder.aspx.cs" Inherits="Cargo.Weixin.myOrder" %>

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
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0; z-index: 200;">
        <a href="my.aspx">
            <div class="wy-header-icon-back"><span></span></div>
        </a>
        <div class="wy-header-title">订单管理</div>
    </header>

    <div class='weui-content'>
        <div class="weui-tab">
            <div class="weui-navbar" style="position: fixed; top: 44px; left: 0; right: 0; height: 44px; background: #fff;">
                <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab1">全部</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab2">待确认</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab3">待发货</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab4">待收货</a>
                <%-- <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab5">待评价</a>--%>
            </div>
            <div class="weui-tab__bd proinfo-tab-con" style="padding-top: 87px;">
                <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <asp:Literal ID="ltlAllOrder" runat="server"></asp:Literal>
                </div>
                <div id="tab2" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlUnPay" runat="server"></asp:Literal>
                </div>
                <div id="tab3" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlUnSend" runat="server"></asp:Literal>
                </div>
                <div id="tab4" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlUnAccept" runat="server"></asp:Literal>
                </div>
                <div id="tab5" class="weui-tab__bd-item">
                </div>
            </div>
        </div>
    </div>
  <%--   <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>--%>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);
        });
        //$(document).on("click", ".ords-btn-dele", function () {
        //    $.confirm("您确定要删除此订单吗?", "确认删除?", function () {
        //        $.toast("订单已经删除!");
        //    }, function () {
        //        //取消操作
        //    });
        //});
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
                            //window.location.reload();
                            refresh();
                        }
                        else {
                            $.alert('删除失败：' + text.Message);
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
        //$(document).on("click", ".receipt", function () {
        //    $.alert("五星好评送蓝豆哦，赶快去评价吧！", "收货完成！");
        //});
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
                        $.alert('收货失败：' + text.Message);
                    }
                }
            });
        }
    </script>
</body>
</html>
