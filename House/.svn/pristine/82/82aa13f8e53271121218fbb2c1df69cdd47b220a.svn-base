<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regeist1.aspx.cs" Inherits="Cargo.Weixin.Regeist1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>客户注册</title>
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
        <a href="mall.aspx">
            <div class="wy-header-icon-home"><span></span></div>
        </a>
        <div class="wy-header-title">客户注册</div>
        <a href="WeiXinBindingClient.aspx">
            <div class="wy-header-right">店代码绑定</div>
        </a>
        <%--<div class="wy-header-right">客户登陆</div>--%>
    </header>

    <div class="weui-content">
        <div class="weui-cells weui-cells_form wy-address-edit" style="margin-top: 1px;">
            <div class="weui-cell weui-cell_vcode">
                <div class="weui-cell__hd">
                    <label class="weui-label wy-lab">手机号</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" id="Cellphone" type="tel" placeholder="请输入手机号码" />
                </div>
                <div class="weui-cell__ft">
                    <button class="weui-vcode-btn" id="btnGetCode">获取验证码</button>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label wy-lab">验证码</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" id="CellCheckCode" type="number" placeholder="请输入短信验证码" />
                </div>
            </div>
        </div>
        <div class="weui-btn-area"><a href="javascript:Bind();" class="weui-btn weui-btn_primary">下一步</a></div>
    </div>
  <%--  <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>--%>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script type="text/javascript">
        //获取验证码
        $('#btnGetCode').on('click', function () {
            var phone = $("#Cellphone").val();
            if (phone == '') {
                $.alert("请输入手机号", "系统提示", function () {
                    $('#Cellphone').focus();
                });
                return false;
            }
            if (!phone.match(/^1[3|4|5|6|7|8|9][0-9]{9}$/)) {
                $.alert("请输入正确的手机号", "系统提示", function () {
                    $('#Cellphone').focus();
                });
                return false;
            }
            i = wait;
            //提交手机号码
            $.ajax({
                type: "post",
                url: "myAPI.aspx?method=SendCode",
                data: { Mobile: phone }, cache: false, ifModified: true,
                success: function (msg) {

                    $("#btnGetCode").attr("disabled", "disabled");
                    $("#btnGetCode").html("发送成功");
                    intervalId = setInterval("exit()", 1000);
                }
            })
        });
        var wait = 120;//倒计时120秒
        var intervalId;//定时
        var i = wait;//倒计时递减 1
        function exit() {
            $("#btnGetCode").html(i + "秒后重新发送").addClass("disabled");
            i = i - 1;
            if (i <= -1) {
                clearInterval(intervalId);
                $("#btnGetCode").removeAttr("disabled");
                $("#btnGetCode").html("发送验证码").removeClass("disabled");
            }
        }
        //绑定店代码
        function Bind() {
            var phone = $("#Cellphone").val();
            if (phone == '') {
                $.alert("请输入手机号", "系统提示", function () {
                    $('#Cellphone').focus();
                });
                return false;
            }
            if (!phone.match(/^1[3|4|5|6|7|8|9][0-9]{9}$/)) {
                $.alert("请输入正确的手机号", "系统提示", function () {
                    $('#Cellphone').focus();
                });
                return false;
            }
            var CellCheckCode = $("#CellCheckCode").val();
            if (CellCheckCode == "") {
                $.alert("请输入验证码", "系统提示", function () {
                    $('#CellCheckCode').focus();
                });
                return false;
            }

            var str = { CellCheckCode: CellCheckCode, Cellphone: phone }
            var json = JSON.stringify([str])
            $.ajax({
                url: 'myAPI.aspx?method=wxUserRegeist',
                type: 'post', dataType: 'json', data: { data: json },
                success: function (text) {
                    if (text.Result == true) {
                        //$.toast("绑定成功!");
                        var json = [{ Cellphone: phone }];
                        localStorage.setItem("WXREG", JSON.stringify(json));
                        window.location.href = "Regeist2.aspx";
                    }
                    else {
                        $.alert(text.Message, "错误提示");
                    }
                }
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
    <script src="WeUI/JS/jquery-weui.js"></script>
</body>
</html>
