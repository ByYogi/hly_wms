<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyScanInHouse.aspx.cs" Inherits="Cargo.QY.MyScanInHouse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>扫描入库</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <script src="JS/jweixin-1.2.0.js"></script>
</head>
<body>
    <audio id="chatAudio">
        <source src="../upload/audio/success.mp3" type="audio/mpeg" />
    </audio>
    <audio id="failAudio">
        <source src="../upload/audio/fail.mp3" type="audio/mpeg" />
    </audio>
    <audio id="InBoundSuc">
        <source src="../upload/audio/InBoundSuc.mp3" type="audio/mpeg" />
    </audio>
    <audio id="InBoundFail">
        <source src="../upload/audio/InBoundFail.mp3" type="audio/mpeg" />
    </audio>
    <audio id="scanQrCode">
        <source src="../upload/audio/scanQrCode.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV0">
        <source src="../upload/audio/0.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV1">
        <source src="../upload/audio/1.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV2">
        <source src="../upload/audio/2.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV3">
        <source src="../upload/audio/3.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV4">
        <source src="../upload/audio/4.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV5">
        <source src="../upload/audio/5.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV6">
        <source src="../upload/audio/6.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV7">
        <source src="../upload/audio/7.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV8">
        <source src="../upload/audio/8.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV9">
        <source src="../upload/audio/9.mp3" type="audio/mpeg" />
    </audio>
    <div class="weui-cells" style="margin-top: 0px;">
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>所属仓库：</p>
            </div>
            <div class="weui-cell__ft" id="HouseName"><%=QyUserInfo.HouseName %></div>
            <%-- <input class="weui-input" id="OrderNo" type="text" style="width: 80%; text-align: right;" value="">--%>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>产品来源：</p>
            </div>
            <input class="weui-input" id="Source" type="text" style="width: 80%; text-align: right;" />
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>产品产地：</p>
            </div>
            <input class="weui-input" id="Born" type="text" style="width: 80%; text-align: right;" />
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>入库数量：</p>
            </div>
            <div class="weui-cell__ft" id="InNum">0</div>
            <%-- <input class="weui-input" id="OrderNo" type="text" style="width: 80%; text-align: right;" value="">--%>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>二维条码：</p>
            </div>
            <div class="weui-cell__ft" id="QrText"></div>
            <%-- <input class="weui-input" id="OrderNo" type="text" style="width: 80%; text-align: right;" value="">--%>
        </div>
    </div>
    <div>
        <a href="javascript:ScanPosition();" id="btnOk" class="weui-btn weui-btn_primary bg-gray f-black" style="width: 45%; float: left; margin-left: 20px;">&nbsp;&nbsp;&nbsp;&nbsp;扫描货位&nbsp;&nbsp;&nbsp;&nbsp;</a>
        <a href="javascript:ScanMove();" id="btnNo" class="weui-btn bg-green" style="width: 45%;">&nbsp;&nbsp;&nbsp;&nbsp;扫描标签&nbsp;&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div id="dvPosition" style="font-size: 20px;"></div>
    <div class="weui-cells" id="ltlUnCheck">
    </div>
    <input id="ContainerCode" hidden="hidden" />
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            var hid = "<%=QyUserInfo.HouseID%>";
            if (hid == "9") {
                //$.toast('该功能已关闭', "forbidden");
                $.alert('该功能已关闭', '系统提示', function () { window.history.back(-1); });
                return;
            }
            QuerySource();
            configjssdk();
        });
        $('#Born').val("国产");
        $("#Born").select({
            title: "请选择产品产地",
            items: ['国产', '进口'],
            onChange: function (d) { },
            onClose: function (d) {
                if (d.data.values != undefined && d.data.values != "") {
                    if (d.data.values == "进口") { Born = "1"; } else { Born = "0"; }
                }
            },
            onOpen: function () { },
        });
        var MList = new Array();
        var mn;
        var rslist;
        var Born = "0";
        var sourceid = "14";
        function QuerySource() {
            $.ajax({
                url: 'qyServices.aspx?method=QueryAllProductSource',
                type: 'post', dataType: 'json', data: {},
                success: function (rs) {
                    if (rs.length > 0) {
                        rslist = rs;
                        $('#Source').val("马牌轮胎厂家");
                        for (var i = 0; i < rs.length; i++) {
                            MList.push(rs[i].SourceName);
                        }
                        $("#Source").select({
                            title: "请选择产品来源",
                            items: MList,
                            onChange: function (d) { },
                            onClose: function (d) {
                                if (d.data.values != undefined && d.data.values != "") {
                                    mn = d.data.values;
                                    for (var j = 0; j < rslist.length; j++) {
                                        if (rslist[j].SourceName == mn) {
                                            sourceid = rslist[j].Source;
                                        }
                                    }
                                }
                            },
                            onOpen: function () { },
                        });
                    }
                }
            });

        }
        function ScanPosition() {
            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是条形码（一维码），默认二者都有
                success: function (res) {
                    if (res.resultStr == "" || res.resultStr == undefined) {
                        $('#failAudio')[0].play();//入库失败
                        //$.alert("扫描标签条码有误", "系统提示", function () { });
                        return;
                    }
                    $('#chatAudio')[0].play();//扫描成功
                    var strs = new Array(); //定义一数组
                    strs = res.resultStr.split(","); //字符分割
                    var ps;
                    if (strs.length > 0) {
                        ps = strs[strs.length - 1];
                    }
                    $('#dvPosition').html("目标货位：<span style='font-weight:bold;color:red;'>" + ps + "</span>");
                    $('#ContainerCode').val(ps);//记住目标货位号
                },
                error: function (res) {
                    if (res.errMsg.indexOf('function_not_exist') > 0) {
                        //alert('版本过低请升级')
                        $.alert('版本过低请升级', '系统提示', function () { });
                    }
                }
            });
        }
        function ScanMove() {
            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是条形码（一维码），默认二者都有
                success: function (res) {
                    // 回调
                    //alert(res.resultStr);
                    if (res.resultStr == "" || res.resultStr == undefined) {
                        $('#failAudio')[0].play();//入库失败
                        //$.alert("扫描标签条码有误", "系统提示", function () { });
                        return;
                    }
                    var ContainerCode = $('#ContainerCode').val();
                    if (ContainerCode == "" || ContainerCode == undefined) {
                        $('#InBoundFail')[0].play();//入库失败
                        $.toast('请先扫描目标货位', "forbidden");
                        return;
                    }
                    var Source = $('#Source').val();
                    if (Source == "" || Source == undefined) {
                        $('#InBoundFail')[0].play();//入库失败
                        $.toast('请先选择产品来源', "forbidden");
                        return;
                    }

                    var strs = new Array(); //定义一数组
                    strs = res.resultStr.split(","); //字符分割
                    var TagCode;
                    if (strs.length > 0) {
                        TagCode = strs[strs.length - 1];
                    }
                    var strss = new Array(); //定义一数组
                    strss = TagCode.split("=");//字符分割
                    if (strss.length > 0) {
                        TagCode = strss[strss.length - 1];
                    }
                    var QrText = "";
                    if (TagCode.indexOf("QR") == -1) {
                        QrText = $('#QrText').text();
                    }
                    $.ajax({
                        url: 'qyServices.aspx?method=ScanQRCodeInCargo',
                        type: 'post', dataType: 'json', data: { ContainerCode: ContainerCode, TagCode: TagCode, Source: sourceid, QrText: QrText, Born: Born },
                        success: function (res) {
                            if (res.Result) {
                                var InNum = Number($('#InNum').text());
                                $('#InNum').html(InNum + 1);
                                $('#QrText').html("");
                                var tar = "<div class=\"weui-cell\"><div class=\"weui-cell__bd\"><p>" + TagCode.slice(-8) + "&nbsp;&nbsp;&nbsp;" + res.Message + "</p></div><div class=\"weui-cell__ft\">入库成功</div></div>";
                                var uc = $('#ltlUnCheck').html();
                                $('#ltlUnCheck').html(tar + uc);
                                $('#InBoundSuc')[0].play();
                                var B1 = Number(res.Message.substring(0, 1));
                                var B2 = Number(res.Message.substring(1, 2));
                                var B3 = Number(res.Message.substring(2, 3));
                                var B4 = Number(res.Message.substring(3, 4));
                                setTimeout(function () {
                                    if (B1 == 0) { $('#AV0')[0].play(); }
                                    if (B1 == 1) { $('#AV1')[0].play(); }
                                    if (B1 == 2) { $('#AV2')[0].play(); }
                                    if (B1 == 3) { $('#AV3')[0].play(); }
                                    if (B1 == 4) { $('#AV4')[0].play(); }
                                    if (B1 == 5) { $('#AV5')[0].play(); }
                                    if (B1 == 6) { $('#AV6')[0].play(); }
                                    if (B1 == 7) { $('#AV7')[0].play(); }
                                    if (B1 == 8) { $('#AV8')[0].play(); }
                                    if (B1 == 9) { $('#AV9')[0].play(); }
                                }, 1500);

                                setTimeout(function () {
                                    if (B2 == 0) { $('#AV0')[0].play(); }
                                    if (B2 == 1) { $('#AV1')[0].play(); }
                                    if (B2 == 2) { $('#AV2')[0].play(); }
                                    if (B2 == 3) { $('#AV3')[0].play(); }
                                    if (B2 == 4) { $('#AV4')[0].play(); }
                                    if (B2 == 5) { $('#AV5')[0].play(); }
                                    if (B2 == 6) { $('#AV6')[0].play(); }
                                    if (B2 == 7) { $('#AV7')[0].play(); }
                                    if (B2 == 8) { $('#AV8')[0].play(); }
                                    if (B2 == 9) { $('#AV9')[0].play(); }
                                }, 1900);
                                setTimeout(function () {
                                    $('#AV2')[0].play();
                                }, 2300);
                                setTimeout(function () {
                                    if (B4 == 0) { $('#AV0')[0].play(); }
                                    if (B4 == 1) { $('#AV1')[0].play(); }
                                    if (B4 == 2) { $('#AV2')[0].play(); }
                                    if (B4 == 3) { $('#AV3')[0].play(); }
                                }, 2700);
                            }
                            else {
                                if (res.Message.indexOf("1010") > -1) {
                                    $('#scanQrCode')[0].play();//入库失败
                                    $('#QrText').html(TagCode);
                                    $.toast("请继续扫描轮胎内侧条码", 'forbidden')
                                }
                                else {
                                    $('#InBoundFail')[0].play();//入库失败
                                    $.toast(res.Message, 'forbidden')
                                }
                            }
                        }
                    });
                },
                error: function (res) {
                    if (res.errMsg.indexOf('function_not_exist') > 0) {
                        //alert('版本过低请升级')
                        $.alert('版本过低请升级', '系统提示', function () { });
                    }
                }
            });
        }
        function configjssdk() {
            var weixinUrl = location.href.split('#')[0];//;
            //配置jssdk
            $.ajax({
                type: "post",
                url: "qyServices.aspx?method=configJssdk",
                dataType: "json",
                data: { Url: weixinUrl },
                cache: false,
                ifModified: true,
                async: false,
                success: function (msg) {
                    var json = eval(msg);
                    var config = {};
                    config.beta = true;
                    config.appId = json.appId;
                    config.nonceStr = json.nonceStr;
                    config.signature = json.signature;
                    config.debug = false;        // 添加你需要的JSSDK的权限
                    config.jsApiList = ['scanQRCode', 'chooseImage', 'openLocation', 'getLocation', 'uploadImage', 'downloadImage', 'getLocalImgData', 'previewImage'];
                    config.timestamp = parseInt(json.timestamp);
                    wx.config(config);
                    wx.ready(function () {
                        //alert("jssdk配置成功");
                        //console("jssdk配置成功");
                        //wx.config(config);
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
