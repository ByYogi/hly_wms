<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CusSerAddress.aspx.cs" Inherits="Cargo.Weixin.CusSerAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客服人员专用添加新地址</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/demos.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../JS/Autocomplete/main.css" rel="stylesheet" />

</head>
<body ontouchstart>
    <div class="field">
        <input type="text" name="nope" id="nope" placeholder="输入收货人名字模糊查询" maxlength="40" />
    </div>
    <div class="weui-cells weui-cells_form" style="font-size: 13px; margin-top: 5px;">

        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">收货人</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" id="Name" type="text" value="" placeholder="请输入收货人姓名" />
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">手机号码</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" id="Cellphone" type="number" value="" pattern="[0-9]*" placeholder="请输入手机号码" />
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label for="name" class="weui-label">所在地区</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" id="start" type="text" value="" />
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__bd">
                <textarea class="weui-textarea" id="Address" placeholder="详细地址" rows="3"></textarea>
                <div class="weui-textarea-counter"><span>0</span>/200</div>
            </div>
        </div>
        <div class="weui-cell weui-cell_switch">
            <div class="weui-cell__bd">设为默认地址</div>
            <div class="weui-cell__ft">
                <input class="weui-switch" id="IsDefault" type="checkbox" />
            </div>
        </div>
        <input hidden="hidden" id="btn" name="btn1" type="radio" value="" checked="checked" />
        <input hidden="hidden" id="addressID" type="text" value="" />
    </div>

    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" id="btnSubmit">保存此地址</a>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../JS/Autocomplete/jquery.autocompleter.js"></script>
    <script src="../JS/Autocomplete/Client.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script src="WeUI/JS/city-picker.js"></script>
    <script type="text/javascript">
        $("#start").cityPicker({
            title: "选择出发地",
            onChange: function (picker, values, displayValues) {
                console.log(values, displayValues);
            }
        });
        $("#IsDefault").bind("click", function () {
            if ($("#btn").val() == "0") {
                $("#btn").val("1");
            } else {
                $("#btn").val("0");
            }
        });
        //保存地址
        $('#btnSubmit').on('click', function () {
            var Name = $("#Name").val();
            var Cellphone = $("#Cellphone").val();
            var start = $("#start").val();
            var Address = $("#Address").val();
            var IsDefault = $('#btn').val();
            var ID = $('#addressID').val();
            if (Name == undefined || Name == "") {
                $.toptip('收货人姓名不能为空', 'warning');
                return;
            }
            if (Cellphone == undefined || Cellphone == "") {
                $.toptip('手机号码不能为空', 'warning');
                return;
            }
            if (start == undefined || start == "") {
                $.toptip('所在地区不能为空', 'warning');
                return;
            }
            if (Address == undefined || Address == "") {
                $.toptip('详细地址不能为空', 'warning');
                return;
            }
            $.showLoading();
            var str = { ID: '', Name: Name, Cellphone: Cellphone, start: start, Address: Address, IsDefault: IsDefault }
            var json = JSON.stringify([str])
            $.ajax({
                url: 'myAPI.aspx?method=AddAddress',
                type: 'post', dataType: 'json', data: { data: json },
                success: function (text) {
                    $.hideLoading();
                    if (text.Result == true) {
                        $.toast("保存成功!");
                        window.location.href = "address.aspx";
                    }
                    else {
                        $.toast('保存失败 失败原因：' + text.Message);
                    }
                }
            });
        });
    </script>
</body>
</html>
