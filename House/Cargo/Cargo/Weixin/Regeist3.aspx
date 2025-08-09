<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regeist3.aspx.cs" Inherits="Cargo.Weixin.Regeist3" %>

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
    <script src="WeUI/JS/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="WeUI/JS/fastclick.js" type="text/javascript"></script>
    <script src="WeUI/JS/jquery-weui.js" type="text/javascript"></script>
    <script src="WeUI/JS/lrz.min.js" type="text/javascript"></script>
</head>

<body ontouchstart>
    <header class="wy-header">
        <a href="mall.aspx">
            <div class="wy-header-icon-home"><span></span></div>
        </a>
        <div class="wy-header-title">客户注册</div>
        <%--<div class="wy-header-right">客户登陆</div>--%>
    </header>

    <div class="weui-content">
        <div class="weui-uploader">
            <div class="weui-uploader__hd">
                <p class="weui-uploader__title" style="text-align: center;">请上传营业执照<span style="color: red;">(必填)</span></p>
            </div>
            <div class="weui-uploader__bd">
                <ul class="weui-uploader__files">
                </ul>
                <div class="weui-uploader__input-box">
                    <input class="weui-uploader__input" accept="image/*" multiple="" type="file" onchange="uploadLicense(this)" />
                </div>
            </div>
            <script type="text/javascript">
                function removeimg(obj, ty) {
                    $.confirm('您确定要删除吗?', '确认删除?', function () {
                        $(obj).remove();
                        setStorg('', '0', '1');
                    });
                    return false;
                }
                function uploadLicense(obj) {
                    lrz(obj.files[0], { width: 750, fieldName: "file" }).then(function (data) {
                        $.post("myAPI.aspx?method=SaveBusLicense", { imgbase64: data.base64 }, function (rs) {
                            $(obj).parent().prev().html('<li onclick="removeimg(this)" class="weui-uploader__file" style="background-image:url(../Weixin/UploadFile/' + rs.Message + ')"><input value="' + rs.Message + '"  type="hidden"  name="file" /></li>');
                            setStorg('../Weixin/UploadFile/' + rs.Message, '0', '0');
                        }, 'json');

                    }).then(function (data) {

                    }).catch(function (err) {
                        console.log(err);
                    });
                }

                function setStorg(img, ty, del) {
                    if (localStorage.getItem("WXREG") != null) {

                        var cart = localStorage.getItem("WXREG");
                        var cartjson = JSON.parse(cart);
                        if (del == "0") {
                            if (ty == "0") {//营业执照
                                cartjson[0].BusLicenseImg = img;
                            } else if (ty == "1") {//身份证正面
                                cartjson[0].IDCardImg = img;
                            } else {//身份证反面
                                cartjson[0].IDCardBackImg = img;
                            }
                        } else {
                            if (ty == "0") {
                                cartjson[0].BusLicenseImg = '';
                            } else if (ty == "1") {
                                cartjson[0].IDCardImg = '';
                            } else {
                                cartjson[0].IDCardBackImg = '';
                            }
                        }

                        localStorage.setItem("WXREG", JSON.stringify(cartjson));
                    } else {
                        $.alert("错误请求", "系统提示", function () { });
                        return false;
                    }
                }

            </script>
        </div>
        <div class="weui-uploader">
            <div class="weui-uploader__hd">
                <p class="weui-uploader__title" style="text-align: center;">请上传身份证正面照片<span style="color: red;">(必填)</span></p>
            </div>
            <div class="weui-uploader__bd">
                <ul class="weui-uploader__files">
                </ul>
                <div class="weui-uploader__input-box">
                    <input class="weui-uploader__input" accept="image/*" multiple="" type="file" onchange="uploadIDimg(this)" />
                </div>
            </div>
            <script>
                function removeIDimg(obj) {
                    $.confirm('您确定要删除吗?', '确认删除?', function () {
                        $(obj).remove();
                        setStorg('', '1', '1');
                    });
                    return false;
                }
                function uploadIDimg(obj) {
                    lrz(obj.files[0], { width: 750, fieldName: "file" }).then(function (data) {
                        $.post("myAPI.aspx?method=SaveBusLicense", { imgbase64: data.base64 }, function (rs) {
                            $(obj).parent().prev().html('<li onclick="removeIDimg(this)" class="weui-uploader__file" style="background-image:url(../Weixin/UploadFile/' + rs.Message + ')"><input value="' + rs.Message + '"  type="hidden"  name="file" /></li>');

                            setStorg('../Weixin/UploadFile/' + rs.Message, '1', '0');

                        }, 'json');

                    }).then(function (data) {

                    }).catch(function (err) {
                        console.log(err);
                    });
                }
            </script>
        </div>
        <div class="weui-uploader">
            <div class="weui-uploader__hd">
                <p class="weui-uploader__title" style="text-align: center;">请上传身份证反面照片<span style="color: red;">(必填)</span></p>
            </div>
            <div class="weui-uploader__bd">
                <ul class="weui-uploader__files">
                </ul>
                <div class="weui-uploader__input-box">
                    <input class="weui-uploader__input" accept="image/*" multiple="" type="file" onchange="uploadIDBackimg(this)" />
                </div>
            </div>
            <script>
                function removeIDBackimg(obj) {
                    $.confirm('您确定要删除吗?', '确认删除?', function () {
                        $(obj).remove();
                        setStorg('', '2', '1');
                    });
                    return false;
                }
                function uploadIDBackimg(obj) {
                    lrz(obj.files[0], { width: 750, fieldName: "file" }).then(function (data) {
                        $.post("myAPI.aspx?method=SaveBusLicense", { imgbase64: data.base64 }, function (rs) {
                            $(obj).parent().prev().html('<li onclick="removeIDBackimg(this)" class="weui-uploader__file" style="background-image:url(../Weixin/UploadFile/' + rs.Message + ')"><input value="' + rs.Message + '"  type="hidden"  name="file" /></li>');

                            setStorg('../Weixin/UploadFile/' + rs.Message, '2', '0');

                        }, 'json');

                    }).then(function (data) {

                    }).catch(function (err) {
                        console.log(err);
                    });
                }
            </script>
        </div>
        <div class="weui-btn-area"><a href="javascript:Bind();" class="weui-btn weui-btn_primary">提交注册</a></div>
    </div>
  <%--  <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>--%>

    <script type="text/javascript">
        //绑定店代码
        function Bind() {
            if (localStorage.getItem("WXREG") != null) {
                var cart = localStorage.getItem("WXREG");
                var cartjson = JSON.parse(cart);
                //if (cartjson[0].BusLicenseImg == "" || cartjson[0].BusLicenseImg ==undefined) {
                //    $.alert("请上传营业执照照片", "系统提示", function () { });
                //    return false;
                //}
                //if (cartjson[0].IDCardImg == "" || cartjson[0].IDCardImg == undefined) {
                //    $.alert("请上传身份证正面照片", "系统提示", function () { });
                //    return false;
                //}
                $.ajax("myAPI.aspx?method=saveRegeist&data=" + cart, {
                    async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                    timeout: 15000, //15秒超时
                    success: function (text) {
                        if (text.Result == true) {
                            $.alert("注册成功，请等待管理员审核！", "系统提示", function () {
                                localStorage.removeItem("WXREG");
                                WeixinJSBridge.call('closeWindow');
                            });
                        }
                        else {
                            $.alert(text.Message, "错误提示");
                        }
                    }
                });
            } else {
                $.alert("错误请求", "系统提示", function () { });
                return false;
            }
        }
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
</body>
</html>
