<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myAccount.aspx.cs" Inherits="Cargo.Weixin.myAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的账单</title>
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
        <div class="weui-tab">
            <div class="weui-navbar" style="position: fixed; top: 0px; left: 0; right: 0; height: 44px; background: #fff;">
                <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab2">未结账</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab3">已结账</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab1">全部</a>
            </div>
            <div class="weui-tab__bd proinfo-tab-con" style="padding-top: 43px;">
                <div id="tab2" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <asp:Literal ID="ltlUnPay" runat="server"></asp:Literal>
                    <div class="weui-tabbar wy-foot-menu">
                        <div class="npd cart-foot-check-item weui-cells_checkbox allselect">
                            <label class="weui-cell allsec-well weui-check__label" for="all">
                                <div class="weui-cell__hd">
                                    <input type="checkbox" class="weui-check" name="all-sec" id="all">
                                    <i class="weui-icon-checked"></i>
                                </div>
                                <div class="weui-cell__bd">
                                    <p class="font-14">全选</p>
                                </div>
                            </label>
                        </div>
                        <div class="weui-tabbar__item  npd">
                            <p class="cart-total-txt">合计：<i>￥</i><em class="num font-16" id="zongji">0.00</em></p>
                        </div>
                        <a href="javascript:saveOrder();" class="red-color npd w-90 t-c" id="saveOr">
                            <p class="promotion-foot-menu-label">微信付款</p>
                        </a>

                    </div>
                </div>
                <div id="tab3" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlPay" runat="server"></asp:Literal>
                </div>
                <div id="tab1" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlAllOrder" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <%--<div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>--%>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        //提交到新界面生成微信支付界面
        function saveOrder() {
            var listBox = $("[name='cartpro']:checked");
            var total = 0;//总金额
            if (listBox.length == 0) {
                $.toast("请勾选待支付的订单"); return;
            }
            var idstr = '';//勾选订单id集合
            for (var i = 0; i < listBox.length; i++) {
                var p = listBox.eq(i).attr("data-p");//总金额
                var id = listBox.eq(i).attr("data-id");//订单ID
                var m = Math.formatFloat(p, 2);
                total = Math.formatFloat(total + m, 2);
                idstr += id + "/";
            }
            $('#saveOr').removeAttr('href');//去掉a标签中的href属性
            window.location.href = "AccountPayOrder.aspx?totalFee=" + $('#zongji').html() + "&id=" + idstr;
        }
        $(function () {
            FastClick.attach(document.body);
        });
        //多选框的改变事件
        $(document).on('change', '[name=cartpro]', function () {
            jsTotal();//算出总额
        });
        $(document).ready(function () {
            $(".allselect").click(function () {
                if ($(this).find("input[name=all-sec]").prop("checked")) {
                    $("input[name=cartpro]").each(function () {
                        $(this).prop("checked", true);
                    });
                }
                else {
                    $("input[name=cartpro]").each(function () {
                        if ($(this).prop("checked")) {
                            $(this).prop("checked", false);
                        } else {
                            $(this).prop("checked", true);
                        }
                    });
                }
                jsTotal();//计算总额
            });
        });
        //算出总额
        function jsTotal() {
            var listBox = $("[name='cartpro']:checked");
            var total = 0;
            if (listBox.length == 0) {
                $("#total").text(total);
            }
            for (var i = 0; i < listBox.length; i++) {
                var p = listBox.eq(i).attr("data-p");//总金额
                var id = listBox.eq(i).attr("data-id");//订单ID
                var m = Math.formatFloat(p, 2);
                total = Math.formatFloat(total + m, 2);
            }
            $("#zongji").text(total);
        }
        Math.formatFloat = function (f, digit) {
            var m = Math.pow(10, digit);
            return parseInt(f * m, 10) / m;
        }
    </script>
</body>
</html>
