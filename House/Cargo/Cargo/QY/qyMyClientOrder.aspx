<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qyMyClientOrder.aspx.cs" Inherits="Cargo.QY.qyMyClientOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的客户订单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
</head>
<body ontouchstart>
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0; z-index: 200; height: 30px;">
        <div class="weui-flex">
            <div class="weui-flex__item">
                <input type="search" class="search-input" id='search' placeholder='请输入客户姓名' />
            </div>
            <div>
                <button class="weui-btn weui-btn_mini weui-btn_primary" id="btnCar"><i class="icon icon-4"></i></button>
            </div>
        </div>
    </header>
    <div class='weui-content'>
        <div class="weui-tab">
            <div class="weui-navbar" style="position: fixed; top: 35px; left: 0; right: 0; height: 44px; background: #fff;">
                <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab1" id="nav1">
                    <img src="../Weixin/WeUI/image/center-icon-order-dfk.png" width="16px" height="16px" style="margin-right: 5px;" />未结账</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab3" id="nav2">
                    <img src="../Weixin/WeUI/image/icon_dfk.png" width="16px" height="16px" style="margin-right: 5px;" />已结账</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab4">
                    <img src="../Weixin/WeUI/image/icon_dfk.png" width="16px" height="16px" style="margin-right: 5px;" />全部</a>
            </div>
            <div class="weui-tab__bd proinfo-tab-con" style="padding-top: 46px;">
                <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <div id="divNoPay"></div>
                </div>
                <div id="tab3" class="weui-tab__bd-item">
                    <div id="divPay"></div>
                </div>
                <div id="tab4" class="weui-tab__bd-item">
                    <div id="divALL"></div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            $(document).on("click", "#btnCar", function () {
                var searchKey = $('#search').val();
                if (searchKey == "" || searchKey == undefined) {
                    $.alert("请输入查询条件", "消息提示", function () { });
                    $('#search').focus();
                    return;
                }
                $.showLoading();
                $.ajax({
                    url: 'qyServices.aspx?method=QueryMyClientOrder',
                    cache: false, dataType: 'json', data: { key: searchKey },
                    success: function (s) {
                        $.hideLoading();
                        var noPay = '', Pay = '', ALL = '';
                        for (var j = 0; j < s.length; j++) {
                            var OrderType = '电脑下单';
                            var check = "未结账";
                            if (s[j].OrderType == "1") { OrderType = "企业号下单"; }
                            else if (s[j].OrderType == "2") { OrderType = "商城下单"; }

                            if (s[j].CheckStatus == "1") {
                                check = "已结账";
                                //已支付
                                Pay += "<div class='weui-panel weui-panel_access' style='margin-top:2px;'><div class='weui-panel__hd' style='font-size:12px;color:black;padding:0px;'><span>订单号：" + s[j].OrderNo + "&nbsp;&nbsp;&nbsp;" + s[j].Dep + "---" + s[j].Dest + "&nbsp;&nbsp;&nbsp;" + s[j].LogisticName + "&nbsp;&nbsp;&nbsp;" + OrderType + "</span><br/><span>收货人：" + s[j].AcceptPeople + "&nbsp;&nbsp;&nbsp;" + s[j].AcceptCellphone + "</span><br/><span style='font-size: 12px; font-weight: bold; color: #FD6C24;'>总数：" + s[j].Piece + "&nbsp;条&nbsp;&nbsp;&nbsp;销售额：" + s[j].TotalCharge + "&nbsp;元</span></div></div>";
                            } else {
                                //未支付
                                noPay += "<div class='weui-panel weui-panel_access' style='margin-top:2px;'><div class='weui-panel__hd' style='font-size:12px;color:black;padding:0px;'><span>订单号：" + s[j].OrderNo + "&nbsp;&nbsp;&nbsp;" + s[j].Dep + "---" + s[j].Dest + "&nbsp;&nbsp;&nbsp;" + s[j].LogisticName + "&nbsp;&nbsp;&nbsp;" + OrderType + "</span><br/><span>收货人：" + s[j].AcceptPeople + "&nbsp;&nbsp;&nbsp;" + s[j].AcceptCellphone + "</span><br/><span style='font-size: 12px; font-weight: bold; color: #FD6C24;'>总数：" + s[j].Piece + "&nbsp;条&nbsp;&nbsp;&nbsp;销售额：" + s[j].TotalCharge + "&nbsp;元</span></div></div>";
                            }
                            ALL += "<div class='weui-panel weui-panel_access' style='margin-top:2px;'><div class='weui-panel__hd' style='font-size:12px;color:black;padding:0px;'><span>订单号：" + s[j].OrderNo + "&nbsp;&nbsp;&nbsp;" + s[j].Dep + "---" + s[j].Dest + "&nbsp;&nbsp;&nbsp;" + s[j].LogisticName + "&nbsp;&nbsp;&nbsp;" + OrderType + "</span><br/><span>收货人：" + s[j].AcceptPeople + "&nbsp;&nbsp;&nbsp;" + s[j].AcceptCellphone + "</span><br/><span style='font-size: 12px; font-weight: bold; color: #FD6C24;'>总数：" + s[j].Piece + "&nbsp;条&nbsp;&nbsp;&nbsp;销售额：" + s[j].TotalCharge + "&nbsp;元&nbsp;&nbsp;&nbsp;" + check + "</span></div></div>";
                        }
                        $('#divNoPay').html(noPay);
                        $('#divPay').html(Pay);
                        $('#divALL').html(ALL);
                        if (noPay == '' && Pay != '') {

                            $("#nav1").removeClass('weui-bar__item--on');
                            $("#nav2").addClass('weui-bar__item--on');

                            //内容切换
                            $("#tab1").removeClass('weui-tab__bd-item--active');
                            $("#tab2").addClass("weui-tab__bd-item--active");
                        }
                    }
                });
            });

        });

    </script>
</body>
</html>
