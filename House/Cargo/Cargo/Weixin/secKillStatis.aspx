<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="secKillStatis.aspx.cs" Inherits="Cargo.Weixin.secKillStatis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <title>活动统计界面</title>
    <style type="text/css">
        table {
            border-collapse: collapse;
            margin: 0 auto;
            text-align: center;
            width: 100%;
            font-size: 25px;
        }

            table td, table th {
                border: 1px solid #cad9ea;
                color: #666;
                height: 30px;
            }

            table th {
                background-color: #CCE8EB;
                width: 100px;
                color: #e54531;
                font-size: 27px;
            }

            table tr:nth-child(odd) {
                background: #fff;
            }

            table tr:nth-child(even) {
                background: #F5FAFA;
            }
    </style>
</head>
<body>

    <asp:Literal ID="ltlSta" runat="server"></asp:Literal>
    <p>
    <a href="secKillStatis.aspx?Company=1">信达汽修店活动统计</a>
    <p>
    <a href="secKillStatis.aspx?Company=2">长沙迪乐泰活动统计</a>
    <p>
    <a href="secKillStatis.aspx?Company=3">广州车轮馆活动统计</a>
</body>
</html>
