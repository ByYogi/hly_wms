<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMyAddress.aspx.cs" Inherits="Cargo.Weixin.AddMyAddress" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>添加新地址</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/demos.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <script src="WeUI/JS/jweixin-1.6.0.js"></script>
</head>

<body ontouchstart>
    <div class="weui-cells weui-cells_form" style="font-size: 13px;">
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label" style="color: red;">收货人</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" id="Name" type="text" value="<%=xm %>" placeholder="请输入收货人姓名" />
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label" style="color: red;">手机号码</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" id="Cellphone" type="number" value="<%=dh %>" pattern="[0-9]*" placeholder="请输入手机号码" />
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label for="name" class="weui-label" style="color: red;">所在地区</label>
            </div>
            <div class="weui-cell__bd">
                <input class="weui-input" id="start" type="text" value="<%=city %>" />
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__bd">
                <textarea class="weui-textarea" id="Address" placeholder="详细地址" rows="3"><%=address %></textarea>
                <div class="weui-textarea-counter"><span>0</span>/200</div>
            </div>
        </div>
        <div class="weui-cell weui-cell_switch">
            <div class="weui-cell__bd">设为默认地址</div>
            <div class="weui-cell__ft">
                <input class="weui-switch" id="IsDefault" type="checkbox" />
            </div>
        </div>
        <input hidden="hidden" id="btn" name="btn1" type="radio" value="<%=isde %>" checked="checked" />
        <input hidden="hidden" id="addressID" type="text" value="<%=ID %>" />
    </div>

    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" id="btnSubmit">保存此地址</a>
        <a href="javascript:DelAddress();" class="weui-btn weui-btn_warn">删除此地址</a>
    </div>
    <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script src="WeUI/JS/city-picker.js"></script>
    <script charset="utf-8" src="http://map.qq.com/api/js?v=2.exp&key=7U4BZ-YDV3D-54E4H-PDUXL-G3TM3-EUBWZ" type="text/javascript"></script>

    <script type="text/javascript">
        //删除此地址
        function DelAddress() {
            var ID = $('#addressID').val();
            $.showLoading();
            $.confirm("您确定要删除该地址?", "确认删除", function () {
                $.ajax({
                    url: 'myAPI.aspx?method=DeleteAddress',
                    type: 'post', dataType: 'json', data: { data: ID },
                    success: function (text) {
                        $.hideLoading();
                        if (text.Result == true) {
                            $.alert("删除成功!");
                            window.location.href = "myAddress.aspx";
                        }
                        else {
                            $.alert('删除失败：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });
        }
        var geocoder = null;

        $(function () {
            var isc = "<%=isde%>";
            var el = document.getElementById('IsDefault');
            if (isc == "1") {
                el.checked = true;
            } else {
                el.checked = false;
            }
            //地址和经纬度之间进行转换服务
            geocoder = new qq.maps.Geocoder();
            //设置服务请求成功的回调函数
            geocoder.setComplete(function (result) {
                //alert(result);
                //alert(result.detail.location);
                var province;
                if (result.detail.addressComponents.province.indexOf("省")) {
                    province = result.detail.addressComponents.province.split("省")[0]
                } else if (result.detail.addressComponents.province.indexOf("市")) {
                    province = result.detail.addressComponents.province.split("市")[0]
                }
                else if (result.detail.addressComponents.province.indexOf("内蒙古")) {
                    province = "内蒙古";
                } else if (result.detail.addressComponents.province.indexOf("广西")) {
                    province = "广西";
                } else if (result.detail.addressComponents.province.indexOf("西藏")) {
                    province = "西藏";
                } else if (result.detail.addressComponents.province.indexOf("宁夏")) {
                    province = "宁夏";
                } else if (result.detail.addressComponents.province.indexOf("新疆")) {
                    province = "新疆";
                } else if (result.detail.addressComponents.province.indexOf("香港")) {
                    province = "香港";
                } else if (result.detail.addressComponents.province.indexOf("澳门")) {
                    province = "澳门";
                }
                $("#start").val(province + " " + result.detail.addressComponents.city + " " + result.detail.addressComponents.district);
                $("#Address").val(result.detail.addressComponents.town + result.detail.addressComponents.street + result.detail.addressComponents.streetNumber)
                //$("#Address").val(result.detail.address);town
            });
            //若服务请求失败，则运行以下函数
            geocoder.setError(function () {
                alert("出错了，请输入正确的经纬度！！！");
            });
            var ID = "<%=ID%>";
            if (ID == undefined || ID == "") {
                configjssdk();
            }
        });
            $("#start").cityPicker({
                title: "选择所在地区",
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
                    $("#Name").focus();
                    return;
                }
                if (Cellphone == undefined || Cellphone == "") {
                    $.toptip('手机号码不能为空', 'warning');
                    $("#Cellphone").focus();
                    return;
                }
                if (start == undefined || start == "") {
                    $.toptip('所在地区不能为空', 'warning');
                    $("#start").focus();
                    return;
                }
                if (Address == undefined || Address == "") {
                    $.toptip('详细地址不能为空', 'warning');
                    return;
                }
                $.showLoading();
                var str = { ID: ID, Name: Name, Cellphone: Cellphone, start: start, Address: Address, IsDefault: IsDefault }
                var json = JSON.stringify([str])
                $.ajax({
                    url: 'myAPI.aspx?method=AddAddress',
                    type: 'post', dataType: 'json', data: { data: json },
                    success: function (text) {
                        $.hideLoading();
                        if (text.Result == true) {
                            $.alert("保存成功!");
                            window.location.href = "myAddress.aspx";
                        }
                        else {
                            $.alert('保存失败：' + text.Message);
                        }
                    }
                });
            });

            function configjssdk() {
                var weixinUrl = location.href.split('#')[0];//;
                //配置jssdk
                $.ajax({
                    type: "post", dataType: "json", data: { Url: weixinUrl },
                    url: "myAPI.aspx?method=configJssdk",
                    cache: false, ifModified: true, async: false,
                    success: function (msg) {
                        var json = eval(msg);
                        var config = {};
                        config.beta = true;
                        config.appId = json.appId;
                        config.nonceStr = json.nonceStr;
                        config.signature = json.signature;
                        config.debug = false;        // 添加你需要的JSSDK的权限
                        config.jsApiList = ['getLocation', 'updateAppMessageShareData', 'updateTimelineShareData', 'onMenuShareTimeline', 'onMenuShareAppMessage'];
                        config.timestamp = parseInt(json.timestamp);
                        wx.config(config);
                        wx.ready(function () {
                            //alert("jssdk配置成功");
                            //wx.config(config);
                            wx.getLocation({
                                type: 'wgs84',
                                success: function (res) {
                                    latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                                    longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                                    var speed = res.speed; // 速度，以米/每秒计
                                    var accuracy = res.accuracy; // 位置精度
                                    if (latitude == "" || longitude == "") {
                                        $.toptip('请打开手机的位置信息功能,然后刷新本页面', 'warning');
                                        return false;
                                    };
                                    var latLng = new qq.maps.LatLng(latitude, longitude);
                                    //对指定经纬度进行解析
                                    geocoder.getAddress(latLng);
                                }
                            });
                        });
                        wx.error(function (res) {
                            alert(JSON.stringify(res));
                        });
                    }
                })
            }
    </script>
</body>
</html>
