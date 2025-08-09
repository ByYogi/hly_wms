<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMoveContainer.aspx.cs" Inherits="Cargo.QY.MyMoveContainer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>仓库管理</title>
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
    <audio id="MoveContainerSuc">
        <source src="../upload/audio/MoveContainerSuc.mp3" type="audio/mpeg" />
    </audio>
    <audio id="MoveContainerFail">
        <source src="../upload/audio/MoveContainerFail.mp3" type="audio/mpeg" />
    </audio>
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
             configjssdk();
        });
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
                        $('#failAudio')[0].play();//扫描失败
                        //$.alert("扫描标签条码有误", "系统提示", function () { });
                        return;
                    }
                    var ContainerCode = $('#ContainerCode').val();
                    if (ContainerCode == "" || ContainerCode == undefined) {
                        $('#MoveContainerFail')[0].play();//移库失败
                        $.toast('请先扫描目标货位', "forbidden");
                        return;
                    }
                    var strs = new Array(); //定义一数组
                    strs = res.resultStr.split(","); //字符分割
                    var TagCode;
                    if (strs.length > 0) {
                        TagCode = strs[strs.length - 1];
                    }
                    $.ajax({
                        url: 'qyServices.aspx?method=ScanMoveContainer',
                        type: 'post', dataType: 'json', data: { ContainerCode: ContainerCode, TagCode: TagCode },
                        success: function (res) {
                            if (res.Result) {
                                $('#MoveContainerSuc')[0].play();
                                var tar = "<div class=\"weui-cell\"><div class=\"weui-cell__bd\"><p>" + TagCode + "</p></div><div class=\"weui-cell__ft\">移库成功</div></div>";
                                var uc = $('#ltlUnCheck').html();
                                $('#ltlUnCheck').html(tar + uc);
                            }
                            else {
                                $('#MoveContainerFail')[0].play();//移库失败
                                $.toast(res.Message, 'forbidden')
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