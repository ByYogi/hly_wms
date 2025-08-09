<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myConsume.aspx.cs" Inherits="Cargo.Weixin.myConsume" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的积分</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/demos.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
</head>
<body>
    <header class='demos-header' style="background-color: green; background-image: url(WeUI/image/jfbg.png)">
        <h1 class="demos-title" style="color: white;" id="fs"></h1>
    </header>
    <div class="weui-form-preview__ft" style="font-size: 14px; margin-bottom: -20px;">
        <a class="weui-form-preview__btn weui-form-preview__btn_default" href="#">
            <img src="WeUI/image/icon_nav_button.png" alt="积分商城" style="width: 25px; margin-bottom: -5px; margin-right: 10px;" />积分商城</a>
        <a class="weui-form-preview__btn weui-form-preview__btn_default" href="#">
            <img src="WeUI/image/icon-jifen.png" alt="积分明细" style="width: 25px; margin-bottom: -5px; margin-right: 10px;" />积分明细</a>
    </div>

    <div class="weui-cells" style="font-size: 14px;">
        <div class="weui-cell">
            <div class="weui-cell__bd">
                <p>签到</p>
            </div>
            <div class="weui-cell__ft" style="color: #FF7F24">+5</div>
            <div class="weui-cell__hd" style="margin-left: 100px; height: 25px;"><a href="javascript:sign();" class="weui-btn weui-btn_mini weui-btn_primary" id="sign">签到</a></div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__bd">
                <p>购物</p>
            </div>
            <div class="weui-cell__ft" style="color: #FF7F24">+订单金额积分</div>
            <div class="weui-cell__hd" style="margin-left: 100px; height: 25px;"><a href="category.aspx" class="weui-btn weui-btn_mini weui-btn_primary">去购物</a></div>
        </div>
        <div style="height:300px;overflow-y:scroll;">
        <asp:Literal ID="ltlPoint" runat="server"></asp:Literal>
            </div>
    </div>
      <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#fs').html(<%= WxUserInfo.ConsumerPoint%> + "分");
            var isg = '<%= WxUserInfo.IsSign%>';
            if (isg == "True") {
                $('#sign').html("已签到");
                $('#sign').css("background-color", "red");
                $('#sign').attr("href", "javascript:;");
            } else {
                $('#sign').html("&nbsp;&nbsp;签到&nbsp;");
                $('#sign').css("background-color", "");
                $('#sign').attr("href", "javascript:sign();");
            }
        });
        function sign() {
            $.ajax({
                url: 'myAPI.aspx?method=TodaySign',
                type: 'post', dataType: 'json', data: { Point: 5 },
                success: function (text) {
                    if (text.Result == true) {
                        $.toptip('签到成功', 'success');
                        var jf = Number(<%= WxUserInfo.ConsumerPoint%>);
                        $('#fs').html(jf + 5 + "分");
                        $('#sign').html("已签到");
                        $('#sign').css("background-color", "red");
                        $('#sign').attr("href", "javascript:;");
                    }
                    else {
                        $.toptip('签到失败', 'error');
                    }
                }
            });
        }

    </script>
</body>
</html>
