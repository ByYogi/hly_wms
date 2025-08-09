<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Supplier.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%= Supplier.Common.GetSystemNameAndVersion()%></title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <link rel="shortcut icon" type="image/x-icon" href="CSS/image/jh.ico" media="screen" />
    <link href="JS/easy/css/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="JS/easy/css/icon.css" rel="stylesheet" type="text/css" />
    <link href="CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="CSS/default1.css" rel="stylesheet" />
    <script src="JS/easy/js/jquery.min.js" type="text/javascript"></script>

    <script src="JS/easy/js/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="JS/json/json2.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            var ua = window.navigator.userAgent.toLowerCase();
            if (ua.match(/MicroMessenger/i) == 'micromessenger' || ua.match(/_SQ_/i) == '_sq_') {
                $("#dologin").val("请不要在微信中使用本系统");
                $("#uname").textbox('disable')
                $("#upwd").textbox('disable')
                //$("#icode").textbox('disable')
                $("#chkSave").attr("disabled", "true");
                $("#img").attr("disabled", "true");
                $("#dologin").removeAttr("onclick");
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '系统无法在微信中打开使用，请将链接复制进浏览器中打开！', 'warning');
                window.open("about:blank", "", "");
            } else {
                $('#upwd').textbox('textbox').keydown(function (e) { if (e.keyCode == 13) { dologin(); } });
            }
        });
        function f_refreshtype() {
            var Image1 = document.getElementById("img");
            if (Image1 != null) { Image1.src = Image1.src + "?"; }
        }

        function loadTopWindow() {
            if (window.top != null && window.top.document.URL != document.URL) {
                window.top.location = document.URL; //这样就可以让登陆窗口显示在整个窗口了 
            }
        }
    </script>

</head>
<body onload="loadTopWindow()">
    <div class="login-box">
        <div id="loginForm">
            <div id="cc" style="padding:50px 0 0 0px">
                <br />
                <br />
                <br />
                <br />
                <br />
                <div id="wrapper" style="width:600px">
                    <div id="login" class="animate form">
                        <h1>供应商登陆</h1>
                        <table style="100%">
                            <tr>
                                <td style="font-size: 14px; text-align: right;">登陆账号:
                                </td>
                                <td>
                                    <input id="uname" class="easyui-textbox" type="text" style="width: 200px; height: 30px; padding: 8px"
                                        data-options="prompt:'请输入登陆账号',iconCls:'icon-man',iconWidth:28">
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 14px; text-align: right;">登陆密码:
                                </td>
                                <td>
                                    <input id="upwd" class="easyui-textbox" type="password" style="width: 200px; height: 30px; padding: 8px"
                                        data-options="prompt:'请输入登陆密码',iconCls:'icon-lock',iconWidth:28">
                                </td>
                            </tr>
                        </table>
                        <p class="login button">
                            <input id="chkSave" name="chkSave" value="1" type="checkbox" /><label for="chkSave">记住密码</label>
                            <input type="submit" id="dologin" onclick="dologin()" value="登陆/Enter" style="position: absolute;top: 99px;left: 310px;height: 64px;" />
                        </p>
                        <p class="change_link">
                            供应商管理系统
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--Footer-->
    <div class="footer-container">
        <div class="container-fluid eg-footer-new" style="background: #f7f7f7; padding-top: 6px; padding-left: 0; padding-right: 0; min-width: 1190px;">

            <hr style="height: 1px; background: #999;" class="container">

            <div class="container-fluid" style="padding: 0; font-size: 12px; padding-bottom: 20px;">
                <div style="width: 100%; text-align: center; margin-top: 2px;">
                    Copyright © 2023 供应商管理系统 All Rights Reserved
           
                </div>

                <div class="clearfix"></div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#uname').textbox('setValue', '<%=un %>');
            $('#upwd').textbox('setValue', '<%=pw %>');
            $("input[name='chkSave']").attr("checked", true);
        });
        function dologin() {
            var txtUserName = $('#uname').textbox("getValue");
            var txtPassword = $('#upwd').textbox("getValue");
            var chk = "0";
            if ($("input[name='chkSave']:checked").val() == "1") { chk = "1"; }
            if (txtUserName == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请输入用户名！', 'warning');
                return false;
            }
            if (txtPassword == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请输入密码！', 'warning');
                return false;
            }
            //提交数据

            $.ajax({
                url: "../FormService.aspx?method=Login",
                type: 'post', dataType: 'text', data: { uname: txtUserName, upwd: txtPassword, chkSave: chk },
                success: function (text) {
                    var res = eval('(' + text + ')');
                    if (res.Result == false) {
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', res.Message, 'warning');
                        f_refreshtype();
                        return false;
                    }
                    else {
                        $.messager.progress({ title: '<%= Supplier.Common.GetSystemNameAndVersion()%>', text: '登录成功，马上转到系统...', interval: '1000' });
                        setTimeout(function () {
                            $.messager.progress('close');
                            window.location = "Main.aspx";
                        }, 1500);
                    }
                }
            });
        }
    </script>

</body>
</html>
