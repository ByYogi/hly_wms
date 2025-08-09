<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxQRCode.aspx.cs" Inherits="Cargo.Weixin.wxQRCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>关注迪乐泰 轮胎公众号</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href=" WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href=" WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href=" WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
</head>
<body>
    <div style="position: fixed; left: 0; top: 0; background: rgba(0,0,0,0.7); filter: alpha(opacity=70); width: 100%; height: 100%;">
        <p style="text-align: center; margin-top: 20%; padding: 0 5%;">
            <img style="max-width: 100%;" src="http://open.weixin.qq.com/qr/code?username=dltchinatyre" alt="请您识别二维码关注微信公众号">
        </p>
        <p style="text-align: center; line-height: 20px; color: #fff; margin: 0px; padding: 0px;">长按二维码选择识别二维码,关注迪乐泰轮胎公众号</p>
    </div>
    <script type="text/javascript">
        var us = "<%=user%>";
        var IsE = "<%=IsE%>";
        if (IsE == "1") {
            $.toptip('不能绑定自己为好友关系', 'warning');
        } else {
            $.toptip('您已与' + us + '绑定好友关系', 'warning');
        }
        //var url = "http://open.weixin.qq.com/qr/code?username=dltchinatyre";
        //var div = document.createElement('div');
        //div.style = '';
        //div.innerHTML = '<p style="text-align:center; margin-top:20%;padding:0 5%;"><img style="max-width:100%;" src="' + url + '" alt="请您关注后浏览"></p><p style="text-align:center;line-height:20px; color:#fff;margin:0px;padding:0px;">长按二维码选择识别二维码,关注迪乐泰轮胎公众号</p>';
        //document.body.appendChild(div);

    </script>
</body>
</html>
