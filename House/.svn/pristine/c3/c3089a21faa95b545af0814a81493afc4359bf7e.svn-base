<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyHouse.aspx.cs" Inherits="Cargo.QY.MyHouse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>仓库管理</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <script src="JS/jweixin-1.2.0.js"></script>
</head>
<body ontouchstart>
    <div class="weui-cells" style="margin-top: 0px;">
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>所属仓库：</p>
            </div>
            <input class="weui-input" id="HouseID" type="text" style="width: 80%; text-align: right;" value="" />
        </div>
    </div>
    <h3>入库操作</h3>
    <div class="weui-grids">
        <a href="MyScanInHouse.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon-conti.png" alt="马牌扫描入库" />
            </div>
            <p class="weui-grid__label">
                马牌扫描入库
            </p>
        </a>
        <a href="MyScanTag.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon_nav_msg.png" alt="标签查询" />
            </div>
            <p class="weui-grid__label">
                标签查询
            </p>
        </a>
    </div>
    <h3>出库操作</h3>
    <div class="weui-grids">
        <a href="MyOutHouse.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon-outScan.png" alt="出库扫描" />
            </div>
            <p class="weui-grid__label">
                出库扫描
            </p>
        </a>
        <%-- <a href="BluetoothTest.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon_nav_article.png" alt="声音测试">
            </div>
            <p class="weui-grid__label">
                声音测试
            </p>
        </a>--%>
    </div>
    <h3>移库操作</h3>
    <div class="weui-grids">
        <a href="MyMoveOrder.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon-moveScan.png" alt="移库扫描" />
            </div>
            <p class="weui-grid__label">
                移库出库扫描
            </p>
        </a>

        <a href="MyScanMoveOrder.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon-inScan.png" alt="扫描入库" />
            </div>
            <p class="weui-grid__label">
                移库入库扫描
            </p>
        </a>

    </div>
    <h3>库内操作</h3>
    <div class="weui-grids">
        <a href="MyMoveContainer.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon_nav_article.png" alt="移库移货位" />
            </div>
            <p class="weui-grid__label">
                库内货位移动
            </p>
        </a>
        <a href="MyInventory.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/icon-jifen.png" alt="仓库盘点" />
            </div>
            <p class="weui-grid__label">
                盘点扫描
            </p>
        </a>
    </div>
    <h3>订单签收</h3>
    <div class="weui-grids">
        <a href="qyLogicTrack.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/SignImage.png" alt="订单签收" />
            </div>
            <p class="weui-grid__label">
                订单签收
            </p>
        </a>
        <a href="MyDeliveryInfo.aspx" class="grid">
            <div class="weui-grid__icon">
                <img src="../Weixin/WeUI/image/services.png" alt="提货上传" />
            </div>
            <p class="weui-grid__label">
                提货上传
            </p>
        </a>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#HouseID').val("<%=QyUserInfo.HouseName%>");
        })
    </script>

</body>
</html>
