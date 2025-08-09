<%@ Page Title="我的订单" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyQueryMyOrder.aspx.cs" Inherits="Cargo.QY.qyQueryMyOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            queryWxUserOrderInfo();//查询该用户的订单数据
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="mui-bar mui-bar-tab">
        <a class="mui-tab-item" href="../QY/MyWorkFloor.aspx">
            <span class="mui-icon mui-icon-home-filled"></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">我的工作台</span>
        </a>
        <a class="mui-tab-item" href="../QY/qyShoppingCart.aspx">
            <span class="mui-icon-extra mui-icon-extra-cart"><span id="gwc" style="font-size: 10px; line-height: 1.4; position: absolute; top: 2px; margin-left: 1px; padding: 1px 5px; color: #fff; background: red; border-radius: 100px; display: inline-block;">0</span></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">购物车</span>
        </a>
        <a class="mui-tab-item  mui-active" href="../QY/qyQueryMyOrder.aspx">
            <span class="mui-icon mui-icon-bars"></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">我的订单</span>
        </a>
    </nav>
    <div class="mui-content">
        <%--  <div class="mui-input-row mui-search" style="margin-bottom: -10px; margin-top: 3px;">
            <input type="search" id="searchInput" onkeyup="enterSearch(event)" class="mui-input-clear" placeholder="请输入规格和花纹">
        </div>--%>
        <div id="divOrder"></div>
    </div>

    <script type="text/javascript">
        //查询该微信用户的订单数据
        function queryWxUserOrderInfo() {
            mui.ajax("../QY/qyServices.aspx?method=queryWxUserOrderInfo", {
                async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                timeout: 15000, //15秒超时
                success: function (obj) {
                    var s = obj;
                    var str = "<ul class=\"mui-table-view mui-table-view-chevron\">";
                    for (var j = 0; j < s.length; j++) {
                        str += "<li class='mui-table-view-cell' style='padding-right: 5px; padding-left: 5px;'>" +
                            "  <div style='font-size: 12px;'>订单号：" + s[j].OrderNo + "&nbsp;&nbsp;&nbsp;" + s[j].Dep + "---" + s[j].Dest + "&nbsp;&nbsp;&nbsp;" + s[j].LogisticName + "</div>" +
                            "  <div class=\"mui-pull-right\" style=\"color: #FD6C24;font-size:14px;\"><a href='qyQueryMyOrderInfo.aspx?OrderNo=" + s[j].OrderNo + "'>查看订单明细</a></div>" +
                            " <div style='font-size: 12px;'>收货人：" + s[j].AcceptPeople + "&nbsp;&nbsp;&nbsp;" + s[j].AcceptCellphone + "</div>" +
                            " <div style='font-size: 12px; font-weight: bold; color: #FD6C24;'>总数：" + s[j].Piece + "&nbsp;条&nbsp;&nbsp;&nbsp;销售额：" + s[j].TotalCharge + "&nbsp;元</div>" +
                            "</div>";
                    }
                    str += "</ul>";
                    $("#divOrder").append(str);
                },
                error: function (xhr, type, errorThrown) {
                    mui.toast('系统错误');
                }
            });
        }
        mui('body').on('tap', 'a', function () { document.location.href = this.href; });

    </script>
</asp:Content>
