<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qyLogicTrack.aspx.cs" Inherits="Cargo.QY.qyLogicTrack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单状态跟踪</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />

</head>
<body ontouchstart>
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0; z-index: 200;">
      <%--  <a href="MyHouse.aspx">
            <div class="wy-header-icon-back"><span></span></div>
        </a>--%>
        <div class="wy-header-title">   <input type="search" style="box-sizing: content-box; width: 80%; line-height: 2.1; display: inline-block; border: 0; -webkit-appearance: none; appearance: none; border-radius: 10px; font-family: inherit; color: #000000; font-size: 16px; font-weight: normal; padding: 0 2px; margin-bottom: 2px; background-color: #fff; border: 1px solid #2d99e6; opacity: .9;"
                id='searchInput' placeholder='请输入订单号' onkeyup="enterSearch(event)" /></div>
    </header>

    <div class='weui-content'>
        <div class="weui-tab">
            <div class="weui-navbar" style="position: fixed; top: 44px; left: 0; right: 0; height: 44px; background: #fff;">
                <%--   <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab1">
                    <img src="Image/Unpick.png" style="margin-right: 5px;" />待拣货</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab3">
                    <img src="Image/Out.png" style="margin-right: 5px;" />待出库</a>--%>
                <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab4">
                    <img src="Image/Arrive.png" style="margin-right: 5px;" />未签收</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab5">
                    <img src="Image/Sign.png" style="margin-right: 5px;" />已签收</a>
                <%-- <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab6">全部</a>--%>
                <%-- <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab5">待评价</a>--%>
            </div>
            <div class="weui-tab__bd proinfo-tab-con" style="padding-top: 87px;">

                <%--  <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <asp:Literal ID="ltlUnPick" runat="server"></asp:Literal>
                </div>
                <div id="tab3" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlUnOut" runat="server"></asp:Literal>
                </div>--%>
                <div id="tab4" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <asp:Literal ID="ltlUnArrive" runat="server"></asp:Literal>
                </div>
                <div id="tab5" class="weui-tab__bd-item">
                    <asp:Literal ID="ltlUnSign" runat="server"></asp:Literal>
                </div>
                <%-- <div id="tab6" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <asp:Literal ID="ltlAllOrder" runat="server"></asp:Literal>
                </div>--%>
            </div>
        </div>
    </div>

    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        function enterSearch(e) {
            if (e.keyCode == 13) {
                QueryPro();
            }
        }
        function QueryPro() {
            var searchKey = $('#searchInput').val();
            $.showLoading();
            $.ajax({
                url: 'qyServices.aspx?method=QueryUnSignOrderList',
                cache: false, dataType: 'json', data: { key: searchKey},
                success: function (text) {
                    $.hideLoading();
                    var it = text;
                    var str = "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd' style='font-size:15px;color:black;'><span>订单号：" + it.OrderNo + "&nbsp;&nbsp;" + it.Dep + "&nbsp;&nbsp;" + it.Dest + "&nbsp;&nbsp;物流公司：" + it.LogisticName + "</span><br/><span>收货人：" + it.AcceptPeople + "&nbsp;&nbsp;" + it.AcceptCellphone + "&nbsp;&nbsp;" + it.AcceptAddress + "</span><br/><span>条数：" + it.Piece + "条&nbsp;&nbsp;业务员：" + it.SaleManName + "</span><a href='javascript:SignOrder(" + it.OrderID + ");' class='ords-btn-pay'>上传签收</a></div></div>";

                    $('#tab4').html(str);
                }
            });
        }
        $(function () {
            FastClick.attach(document.body);
        });
        function PickOrder(no) {
            $.confirm("确认拣货完成?", "确认", function () {
                $.ajax({
                    url: 'qyServices.aspx?method=SetOrderStatus&AwbStatus=6',
                    type: 'post', dataType: 'json', data: { data: no },
                    success: function (text) {
                        if (text.Result == true) {
                            $.toast("拣货完成!");
                            window.location.reload();
                        }
                        else {
                            $.toast('拣货失败 失败原因：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });
        }
        function OutOrder(no) {
            $.confirm("确认出库完成?", "确认", function () {
                $.ajax({
                    url: 'qyServices.aspx?method=SetOrderStatus&AwbStatus=2',
                    type: 'post', dataType: 'json', data: { data: no },
                    success: function (text) {
                        if (text.Result == true) {
                            $.toast("出库完成!");
                            window.location.reload();
                        }
                        else {
                            $.toast('出库失败 失败原因：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });
        }
        function ArriveOrder(no) {
            $.confirm("确认装车完成?", "确认", function () {
                $.ajax({
                    url: 'qyServices.aspx?method=SetOrderStatus&AwbStatus=3',
                    type: 'post', dataType: 'json', data: { data: no },
                    success: function (text) {
                        if (text.Result == true) {
                            $.toast("装车完成!");
                            window.location.reload();
                        }
                        else {
                            $.toast('到达失败 失败原因：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });
        }
        function SignOrder(no) {
            window.location.href = "qyOrderSign.aspx?OrderID=" + no;
            //$.confirm("确认签收?", "确认", function () {
            //    $.ajax({
            //        url: 'qyServices.aspx?method=SetOrderStatus&AwbStatus=5',
            //        type: 'post', dataType: 'json', data: { data: no },
            //        success: function (text) {
            //            if (text.Result == true) {
            //                $.toast("签收成功!");
            //                window.location.reload();
            //            }
            //            else {
            //                $.toast('签收失败 失败原因：' + text.Message);
            //            }
            //        }
            //    });
            //}, function () {
            //    //取消操作
            //});
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
                        $.toast("收货完成!");
                        window.location.reload();
                    }
                    else {
                        $.toast('收货失败 失败原因：' + text.Message);
                    }
                }
            });
        }
    </script>
</body>
</html>
