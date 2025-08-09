<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="address.aspx.cs" Inherits="Cargo.Weixin.address" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>地址管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />

</head>

<body ontouchstart>
    <header class="wy-header">
        <div class="wy-header-icon-back"><span></span></div>
        <div class="wy-header-title">地址管理</div>
        <div class="wy-header-right"><a href="AddAddress.aspx" style="font-size:14px;text-decoration:dashed;">添加收货地址</a></div>
    </header>

    <div class="weui-content">
        <div class="weui-panel address-box">
            <asp:Literal ID="ltlShipAddress" runat="server"></asp:Literal>
        </div>
        <%--   <div class="weui-btn-area">
            <a class="weui-btn weui-btn_primary" href="AddAddress.aspx" id="showTooltips">添加收货地址</a>
        </div>--%>
        <div class="weui-btn-area">
            <a class="weui-btn weui-btn_primary" href="CusSerAddress.aspx" id="ClientAdd">内部使用添加新地址</a>
        </div>
    </div>

    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
            var cn = '<%=cn%>';
            if (cn == "666666" || cn == "666667" || cn == "683881" || cn == "833727") {
                $('#ClientAdd').show();
            } else {
                $('#ClientAdd').hide();

            }
        });
    </script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        //绑定客户地址信息
        function inputAddSession(cid) {
            var client = cid.split('/');
            var storC = localStorage.getItem("CLIENT");
            localStorage.removeItem("CLIENT");
            var json = [{ ClientID: client[0], Address: client[1], Name: client[2], Cellphone: client[3], Province: client[4], City: client[5], Country: client[6] }];
            localStorage.setItem("CLIENT", JSON.stringify(json));
            window.location.href = "myOrderSubmit.aspx";
        }

    </script>
</body>
</html>
