<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeShow.aspx.cs" Inherits="Cargo.NoticeShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <title>公告详情</title>
    <link href="Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
</head>

<body ontouchstart>
    <div class="weui-content">
        <article class="weui-article">
            <h1 style="font-weight:bold;"><%=ent.Title %></h1>
            <h4 class="wy-news-time"><%=ent.OP_DATE %></h4>
            <section class="wy-news-info">
                <%=ent.Memo %>
            </section>
        </article>

    </div>
    <script src="Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="Weixin/WeUI/JS/fastclick.js"></script>
    <script>
        $(function () {
            FastClick.attach(document.body);
        });
    </script>

    <script src="Weixin/WeUI/JS/jquery-weui.js"></script>
</body>
</html>
