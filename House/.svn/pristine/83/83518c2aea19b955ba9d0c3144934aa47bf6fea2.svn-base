<%@ Page Title="我的订单明细" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyQueryMyOrderInfo.aspx.cs" Inherits="Cargo.QY.qyQueryMyOrderInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .otd {
            border-color: #ccc;
            border-style: dotted;
            border-width: 0 1px 1px 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mui-content">
        <ul class="mui-table-view mui-table-view-chevron">
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">订单号</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000"><%=OrderEntity.OrderNo %></div>
            </li>
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">收货人</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000"><%=OrderEntity.AcceptPeople %></div>
            </li>
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">总件数</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000"><%=OrderEntity.Piece %>条</div>
            </li>
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">总收入</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000"><%=OrderEntity.TransportFee %>元</div>
            </li>
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">业务员</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000"><%=OrderEntity.SaleManName %></div>
            </li>
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">开单时间</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000">
                    <asp:Literal ID="ltlApproveDate" runat="server"></asp:Literal>
                </div>
            </li>
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">付款方式</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000"><%=CheckOutType %></div>
            </li>
            <li class="mui-table-view-cell" style="padding-right: 20px;">
                <div class="mui-pull-left" style="font-size: 12px; color: #666666;">开单员</div>
                <div class="mui-pull-right" style="font-size: 14px; color: #000"><%=OrderEntity.CreateAwb %></div>
            </li>
        </ul>
        <%-- 关联数据 --%>
        <asp:Literal ID="ltlRelate" runat="server"></asp:Literal>
        <div class="mui-button-row">
            <button id="btnClose" type="button" class="mui-btn mui-btn-warning btnsize_default" style="width: 30%">返回</button>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#btnClose').on('click', function () { location.href = "javascript:history.go(-1)" });
            //$('#btnClose').on('click', function () { WeixinJSBridge.call('closeWindow'); });
        });

    </script>
</asp:Content>
