<%@ Page Title="我的工作台" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="MyWorkFloor.aspx.cs" Inherits="Cargo.QY.MyWorkFloor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #txIMG {
            height: 60px;
            width: 60px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--    <header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">我的资料</h1>
    </header>--%>

    <div class="mui-content">
        <div class="">
            <ul class="mui-table-view mui-table-view-chevron">
                <li class="mui-table-view-cell">
                    <img id="txIMG" class="mui-pull-left" src="<%=ent.AvatarSmall %>" />
                    <div style="width: 3%; min-height: 50px;" class=" mui-pull-left">
                    </div>
                    <div class="mui-pull-right" style="font-size: 14px; color: #CCCCCC;">
                        <asp:Literal ID="ltlCurDate" runat="server"></asp:Literal>
                    </div>
                    <div style="font-size: 14px; min-height: 30px;"><%=ent.WxName %></div>

                    <div style="font-size: 14px;">系统账号：<%=ent.UserID %></div>
                </li>
                <li class="mui-table-view-cell" style="padding-right: 10px;">
                    <span class="mui-icon-extra mui-icon-extra-like mui-pull-left" style="font-size: 24px; color: #FFBF00;"></span>
                    <div class="mui-pull-left" style="width: 3%; min-height: 25px;">
                    </div>
                    <div class="mui-pull-left" style="font-size: 14px; line-height: 28px; color: #000000;">
                        订单统计
                    </div>

                    <div style="min-height: 30px;">
                    </div>
                    <div class=" mui-pull-left" style="background: #26A3D5; width: 49%; border-radius: 3px;">
                        <div style="font-size: 14px; color: #FFFFFF; text-align: center;">
                            今日订单：<%=ent.TodayPiece %>条，<%=ent.TodayCharge %>元
                        </div>
                    </div>
                    <div class=" mui-pull-right" style="background: #3EA9DB; width: 49%; border-radius: 3px;">
                        <div style="font-size: 14px; color: #FFFFFF; text-align: center;">本月订单：<%=ent.MonthPiece %>条，<%=ent.MonthCharge %>元</div>
                    </div>
                </li>
            </ul>
        </div>

        <div style="min-height: 10px;">
        </div>
        <ul class="mui-table-view">
            <li class="mui-table-view-cell">
                <a class="mui-navigate-right" href="TypeStockQuery.aspx" target="_self">
                    <span class="mui-icon-extra mui-icon-extra-outline" style="color: #10AEFF"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;轮胎库存查询
                </a>
            </li>
            <li class="mui-table-view-cell">
                <a class="mui-navigate-right" href="qyQueryMyOrder.aspx" target="_self">
                    <span class="mui-icon-extra mui-icon-extra-topic" style="color: #10AEFF"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我的订单
                </a>
            </li>
            <li class="mui-table-view-cell">
                <a class="mui-navigate-right" href="qyMyClientOrder.aspx" target="_self">
                    <span class="mui-icon-extra mui-icon-extra-addpeople" style="color: #10AEFF"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我的客户订单
                </a>
            </li>
            <li class="mui-table-view-cell">
                <a class="mui-navigate-right" href="qyLogicTrack.aspx" target="_self">
                    <span class="mui-icon-extra mui-icon-extra-express" style="color: #10AEFF"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;物流状态跟踪
                </a>
            </li>

            <%--<li class="mui-table-view-cell">
                <a class="mui-navigate-right" href="Integral.aspx" target="_self">
                    <span class=" mui-badge mui-badge-success">52</span>
                    <span class="mui-icon-extra mui-icon-extra-peoples" style="color: #10AEFF;"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;我的客户
                </a>
            </li>--%>
            <%--    <li class="mui-table-view-cell">
                <a class="mui-navigate-right" href="BankCard.aspx" target="_self"><span class="mui-icon-extra mui-icon-extra-card" style="color: #FFC000"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;银行卡号管理
                </a>
            </li>
               <li class="mui-table-view-cell">
                <a class="mui-navigate-right" href="ReCharge.aspx" target="_self">
                       <span class=" mui-badge mui-badge-success"><%=Money %></span>
                    <span class="mui-icon-extra mui-icon-extra-prech" style="color:#FFC000"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;运费充值
                </a>
            </li>--%>
        </ul>

    </div>
    <script>
        mui.init({
            swipeBack: true //启用右滑关闭功能
        });
    </script>
</asp:Content>
