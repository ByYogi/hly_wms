<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regeist2.aspx.cs" Inherits="Cargo.Weixin.Regeist2" %>

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
    <script src="WeUI/JS/jweixin-1.6.0.js"></script>
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
        <div class="weui-cells weui-cells_form wy-address-edit" style="margin-top: 1px;">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label wy-lab"><span style="color: red; font-weight: bolder;">*</span>公司名称</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" id="CompanyName" type="text" placeholder="请输入公司名称" style="text-align: right;" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label wy-lab" style="width: 80px;"><span style="color: red; font-weight: bolder;">*</span>公司联系人</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" id="Name" type="text" placeholder="请输入公司联系人" style="text-align: right;" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="name" class="weui-label wy-lab"><span style="color: red; font-weight: bolder;">*</span>所在区域</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" id="start" type="text" value="" style="text-align: right;" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label wy-lab"><span style="color: red; font-weight: bolder;">*</span>公司地址</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" id="Address" type="text" placeholder="请输入公司地址" style="text-align: right;" />
                </div>
            </div>
        </div>
        <div class="weui-btn-area"><a href="javascript:Bind();" class="weui-btn weui-btn_primary">下一步</a></div>
    </div>

   <%-- <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>--%>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/city-picker.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script charset="utf-8" src="https://map.qq.com/api/js?v=2.exp&key=7U4BZ-YDV3D-54E4H-PDUXL-G3TM3-EUBWZ" type="text/javascript"></script>
    <script type="text/javascript">
        //绑定店代码
        function Bind() {
            var CompanyName = $("#CompanyName").val();
            if (CompanyName == '') {
                $.alert("请输入公司名称", "系统提示", function () {
                    $('#CompanyName').focus();
                });
                return false;
            }
            var Name = $("#Name").val();
            if (Name == "") {
                $.alert("请输入公司联系人", "系统提示", function () {
                    $('#Name').focus();
                });
                return false;
            }
            var start = $("#start").val();
            if (start == undefined || start == "") {
                $.alert("请选择所在区域", "系统提示", function () {
                    $('#start').focus();
                });
                return false;
            }
            var Address = $("#Address").val();
            if (Address == "") {
                $.alert("请输入公司地址", "系统提示", function () {
                    $('#Address').focus();
                });
                return false;
            }

            if (localStorage.getItem("WXREG") != null) {

                var cart = localStorage.getItem("WXREG");
                var cartjson = JSON.parse(cart);
                var str = [{ Cellphone: cartjson[0].Cellphone, CompanyName: CompanyName, Name: Name, Address: Address, start: start,BusLicenseImg:'',IDCardImg:'' }];

                localStorage.removeItem("WXREG");
                localStorage.setItem("WXREG", JSON.stringify(str));
            } else {
                $.alert("错误请求", "系统提示", function () { });
                return false;
            }
            window.location.href = "Regeist3.aspx";
        }
    </script>
    <script type="text/javascript">
        var geocoder = null;
        $(function () {
            FastClick.attach(document.body);
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

            configjssdk();
        });
        $("#start").cityPicker({
            title: "选择区域",
            onChange: function (picker, values, displayValues) {
                console.log(values, displayValues);
            }
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
