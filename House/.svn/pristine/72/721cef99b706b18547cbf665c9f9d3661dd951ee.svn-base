<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HLYEagle.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>飞鹰报表统计系统</title>
    <link href="CSS/style2.0.css" rel="stylesheet" />
    <style type="text/css">
        ul li {
            font-size: 30px;
            color: #2ec0f6;
        }

        .tyg-div {
            z-index: -1000;
            float: left;
            position: absolute;
            left: 5%;
            top: 20%;
        }

        .tyg-p {
            font-size: 14px;
            font-family: 'microsoft yahei';
            position: absolute;
            top: 135px;
            left: 60px;
        }

        .tyg-div-denglv {
            z-index: 1000;
            float: right;
            position: absolute;
            right: 12%;
            top: 10%;
        }

        .tyg-div-form {
            background-color: #23305a;
            width: 300px;
            height: auto;
            margin: 120px auto 0 auto;
            color: #2ec0f6;
        }

            .tyg-div-form form {
                padding: 10px;
            }
                /*.tyg-div-form form input[type="text"]{*/
                /*width: 270px;*/
                /*height: 30px;
	    margin: 25px 10px 0px 0px;
	}*/
               #dologin1 {
                    cursor: pointer;
                    width: 270px;
                    height: 44px;
                    margin-top: 25px;
                    padding: 0;
                    background: #2ec0f6;
                    -moz-border-radius: 6px;
                    -webkit-border-radius: 6px;
                    border-radius: 6px;
                    border: 1px solid #2ec0f6;
                    -moz-box-shadow: 0 15px 30px 0 rgba(255,255,255,.25) inset, 0 2px 7px 0 rgba(0,0,0,.2);
                    -webkit-box-shadow: 0 15px 30px 0 rgba(255,255,255,.25) inset, 0 2px 7px 0 rgba(0,0,0,.2);
                    box-shadow: 0 15px 30px 0 rgba(255,255,255,.25) inset, 0 2px 7px 0 rgba(0,0,0,.2);
                    font-family: 'PT Sans', Helvetica, Arial, sans-serif;
                    font-size: 14px;
                    font-weight: 700;
                    color: #fff;
                    text-shadow: 0 1px 2px rgba(0,0,0,.1);
                    -o-transition: all .2s;
                    -moz-transition: all .2s;
                    -webkit-transition: all .2s;
                    -ms-transition: all .2s;
                }
    </style>
    <link href="JS/easy/css/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="JS/easy/css/icon.css" rel="stylesheet" type="text/css" />
    <script src="JS/easy/js/jquery.min.1.13.js" type="text/javascript"></script>
    <script src="JS/easy/js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="JS/easy/js/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();
        $(document).ready(function () {
            $('#uname').textbox('setValue', '<%=un %>');
                    $('#upwd').textbox('setValue', '<%=pw %>');
                    $("input[name='chkSave']").attr("checked", true);
                });
    </script>

</head>
<body>
    <div class="tyg-div">
        <ul>
            <li>让</li>
            <li>
                <div style="margin-left: 20px;">数</div>
            </li>
            <li>
                <div style="margin-left: 40px;">据</div>
            </li>
            <li>
                <div style="margin-left: 60px;">改</div>
            </li>
            <li>
                <div style="margin-left: 80px;">变</div>
            </li>
            <li>
                <div style="margin-left: 100px;">生</div>
            </li>
            <li>
                <div style="margin-left: 120px;">活</div>
            </li>
        </ul>
    </div>
    <div id="contPar" class="contPar">
        <div id="page1" style="z-index: 1; height: 937px;" class="cont_1">
            <div class="title0">飞鹰报表统计系统</div>
            <div class="title1">轮胎、机油、用品、汽车配件、大数据</div>
            <div class="imgGroug">
                <ul>
                    <li>
                        <img alt="" class="img0 png" src="CSS/img/page1_0.png" /></li>
                    <li>
                        <img alt="" class="img1 png" src="CSS/img/page1_1.png" style="margin-left: -83.2225px; margin-top: -85.741px;" /></li>
                    <li>
                        <img alt="" class="img2 png" src="CSS/img/page1_2.png" style="margin-left: -83.2225px; margin-top: -85.741px;" /></li>
                </ul>
            </div>
            <img alt="" class="img3 png" src="CSS/img/page1_3.jpg" style="margin-left: -368.11px; margin-top: -407.036px;" />
        </div>
    </div>
    <div class="tyg-div-denglv">
        <div class="tyg-div-form">
            <form>
                <h2 style="font-size: 20px">登录</h2>
                <p class="tyg-p">欢迎访问  飞鹰报表统计系统</p>
                <div style="margin: 20px 0px;">
                    <input id="uname" class="easyui-textbox" type="text" style="width: 270px; height: 30px; margin: 25px 10px 0px 0px" data-options="prompt:'请输入用户名',iconCls:'icon-man',iconWidth:28">
                </div>
                <div style="margin: 20px 0px;">
                    <input id="upwd" class="easyui-textbox" type="password" style="width: 270px; height: 30px; margin: 25px 10px 0px 0px" data-options="prompt:'请输入密码',iconCls:'icon-lock',iconWidth:28">
                </div>
                <div style="margin: 0px 0px;">
                    <input type="text" style="width: 100px; height: 30px" placeholder="请输入验证码..." id="icode" class="easyui-textbox" />
                    <img id="img" src="../VateImage.aspx" onclick="f_refreshtype()" alt="" style="vertical-align: middle;" />
                    <input id="chkSave" name="chkSave" value="1" type="checkbox" /><label for="chkSave">记住密码</label>
                </div>
                     <input type="button" id="dologin1" onclick="dologin()" value="登陆/Enter" />
            </form>
        </div>
    </div>




</body>
</html>


<script type="text/javascript" src="JS/Com.js"></script>
<script type="text/javascript">

    function f_refreshtype() {
        var Image1 = document.getElementById("img");
        if (Image1 != null) { Image1.src = Image1.src + "?"; }
    }
    function dologin() {
        debugger;
        var txtUserName = $('#uname').textbox("getValue");
        var txtPassword = $('#upwd').textbox("getValue");
        var txtCode = $('#icode').textbox("getValue");
        //alert(txtUserName)
        //      alert(txtPassword)
        //      alert(txtCode)
        var chk = "0";
        if ($("input[name='chkSave']:checked").val() == "1") { chk = "1"; }
        if (txtUserName == "") {
            $.messager.alert('<%= HLYEagle.Common.GetSystemNameAndVersion()%>', '请输入用户名！', 'warning');
            return false;
        }
        if (txtPassword == "") {
            $.messager.alert('<%= HLYEagle.Common.GetSystemNameAndVersion()%>', '请输入密码！', 'warning');
            return false;
        }
        if (txtCode == "") {
            if ('<%=isDebug %>' != 'True') {
                $.messager.alert('<%= HLYEagle.Common.GetSystemNameAndVersion()%>', '请输入验证码！', 'warning');
                return false;
            }
        }
        //提交数据
        $.ajax({
            url: "FormService.aspx?method=Login",
            type: "post",
            data: { uname: txtUserName, upwd: txtPassword, ucode: txtCode, chkSave: chk },
            success: function (text) {
                var res = eval('(' + text + ')');
                if (res.Result == false) {
                    $.messager.alert('<%= HLYEagle.Common.GetSystemNameAndVersion()%>', res.Message, 'warning');
                    f_refreshtype();
                    return false;
                }
                else {
                    $.messager.progress({ title: '<%= HLYEagle.Common.GetSystemNameAndVersion()%>', text: '登录成功，马上转到系统...', interval: '1000' });
                    setTimeout(function () {
                        $.messager.progress('close');
                        window.location = "Main.aspx";
                    }, 1500)
                        ;
                }
            }
        });
    }
</script>
