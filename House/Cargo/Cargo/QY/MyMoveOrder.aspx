<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMoveOrder.aspx.cs" Inherits="Cargo.QY.MyMoveOrder" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>扫描移库</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <script src="JS/jweixin-1.2.0.js"></script>
</head>
<body ontouchstart>
    <audio id="chatAudio">
        <source src="../upload/audio/success.mp3" type="audio/mpeg" />
    </audio>
    <audio id="failAudio">
        <source src="../upload/audio/fail.mp3" type="audio/mpeg" />

    </audio>

    <div class="weui-cells" style="margin-top: 0px;">
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>移库单号：</p>
            </div>
            <%--  <div class="weui-cell__ft" id="sMoveNo"></div>--%>
            <input class="weui-input" id="sMoveNo" type="text" style="width: 80%; text-align: right;" value="">
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>移库数量：</p>
            </div>
            <div class="weui-cell__ft" id="MoveNum"></div>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>原仓库：</p>
            </div>
            <div class="weui-cell__ft" id="OldHouseName"></div>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>目标仓库：</p>
            </div>
            <div class="weui-cell__ft" id="NewHouseName"></div>
        </div>
    </div>
    <div style="margin-top: 1px;margin-bottom:1px;">
        <a href="javascript:ScanMove();" class="weui-btn bg-green">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;扫描标签&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div style="text-align: center; margin-top: 5px" id="ZScan"></div>
    <div class="weui-cells" style="margin-top: 5px;">
        <div id="ltlUnCheck">
        </div>
    </div>

    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        var MList = new Array();
        var mn;
        $(function () {
            //FastClick.attach(document.body);
            configjssdk();
            $.ajax({
                url: 'qyServices.aspx?method=QueryMoveOrderData',
                type: 'post', dataType: 'json', data: { MoveNo: "", MoveStatus: "0" },
                beforeSend: function () { $("#loading").show(); },
                success: function (rs) {
                    $('#loading').hide();
                    if (rs.length > 0) {
                        mn = rs[0].MoveNo;
                        $('#MoveNo').html(rs[0].MoveNo);
                        $('#MoveNum').html(rs[0].MoveNum);
                        $('#OldHouseName').html(rs[0].OldHouseName);
                        $('#NewHouseName').html(rs[0].NewHouseName);
                        var zN = 0, sN = 0, nN = 0;
                        if (rs[0].MoveGoodsList.length > 0) {
                            for (var j = 0; j < rs[0].MoveGoodsList.length; j++) {
                                zN += Number(rs[0].MoveGoodsList[j].Piece);
                                sN += Number(rs[0].MoveGoodsList[j].ScanNum);
                                var ws = Number(rs[0].MoveGoodsList[j].Piece) - Number(rs[0].MoveGoodsList[j].ScanNum);
                                $('#ltlUnCheck').append("<div class=\"weui-cell\"><div class=\"weui-cell__bd weui-cell_primary\"><div class=\"weui_cell_title\" style=\"font-size: 12px;\">产品ID：" + rs[0].MoveGoodsList[j].ProductID + "&nbsp;规格：<span style=\"font-weight:bold;\">" + rs[0].MoveGoodsList[j].Specs + "  " + rs[0].MoveGoodsList[j].Figure + " " + rs[0].MoveGoodsList[j].LoadIndex + rs[0].MoveGoodsList[j].SpeedLevel + "</span></div><div class=\"weui_cell_info\" style=\"font-size: 12px;\">货位：<span style=\"font-weight:bold;\">" + rs[0].MoveGoodsList[j].ContainerCode + "</span>&nbsp;批次：<span style=\"font-weight:bold;\">" + rs[0].MoveGoodsList[j].Batch + "</span></div></div><div class=\"weui-cell__ft\"><div class=\"weui_cell_title\" style=\"font-size: 13px;\">移库数量：" + rs[0].MoveGoodsList[j].Piece + "条</div><div class=\"weui_cell_info\" id=\"" + rs[0].MoveGoodsList[j].ContainerGoodsID + "\"><span style=\"font-size: 15px; color: red;\">未：" + ws + "条</span>&nbsp;&nbsp;<span style=\"font-size: 15px; color: green;\">已：" + rs[0].MoveGoodsList[j].ScanNum + "条</span></div></div></div>");
                            }
                        }
                        nN = zN - sN;
                        $('#ZScan').html("总未扫描：<span style=\"font-weight: bold; color: red; font-size: 20px;\">" + nN + "</span>&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;已扫描：<span style=\"font-weight: bold; color: darkgreen; font-size: 20px;\">" + sN + "</span>&nbsp;条");
                    }
                    for (var i = 0; i < rs.length; i++) {
                        MList.push(rs[i].MoveNo);
                    }
                    $("#sMoveNo").select({
                        title: "请选择移库单号",
                        items: MList,
                        onChange: function (d) { },
                        onClose: function (d) {
                            if (d.data.values != undefined && d.data.values != "") {
                                ajaxpage(d.data.values);
                                mn = d.data.values;
                            }
                        },
                        onOpen: function () { },
                    });
                    $('#sMoveNo').val(mn)
                }
            });
            //ajaxpage("");

        });
        function ScanMove() {
            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是条形码（一维码），默认二者都有
                success: function (res) {
                    if (res.resultStr == "" || res.resultStr == undefined) {
                        //mui.alert('扫描标签条码有误', '系统提示', function () { });
                        $('#failAudio')[0].play();
                        //$.toast("扫描标签条码有误", "forbidden");
                        return;
                    }
                    var TagCode = res.resultStr;
                    $.ajax({
                        url: 'qyServices.aspx?method=ScanMoveOrderOutOK',
                        type: 'post', dataType: 'json', data: { MoveNo: mn, TagCode: TagCode },
                        success: function (res) {
                            if (res.Result) {
                                $('#chatAudio')[0].play();
                                var zN = 0, sN = 0, nN = 0;
                                //操作成功
                                var g = eval(res.Message);
                                for (var i = 0; i < g.length; i++) {
                                    zN += Number(g[i].Piece);
                                    sN += Number(g[i].ScanNum);
                                    var ws = Number(g[i].Piece) - Number(g[i].ScanNum);
                                    $('#' + g[i].ContainerGoodsID + '').html("<span style=\"font-size: 15px; color: red;\">未扫描：" + ws + "条</span>&nbsp;&nbsp;<span style=\"font-size: 15px; color: green;\">已扫描：" + g[i].ScanNum + "条</span>");
                                }
                                nN = zN - sN;
                                $('#ZScan').html("未扫描：<span style=\"font-weight: bold; color: red; font-size: 17px;\">" + nN + "</span>&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;已扫描：<span style=\"font-weight: bold; color: darkgreen; font-size: 17px;\">" + sN + "</span>&nbsp;条");
                            }
                            else {
                                $('#failAudio')[0].play();
                                //$.toptip(res.Message, 'warning')
                                //$.toast(res.Message, "forbidden");
                            }
                        }
                    });
                },
                error: function (res) {
                    if (res.errMsg.indexOf('function_not_exist') > 0) {
                        //alert('版本过低请升级')
                        $.toast("版本过低请升级", "forbidden");
                        //mui.alert('版本过低请升级', '系统提示', function () { });
                    }
                }
            });
        }
        $('#loading').hide();
        function ajaxpage(MoveNo) {
            $('#ltlUnCheck').html("");
            $.ajax({
                type: "POST", dataType: "json", data: { MoveNo: MoveNo, MoveStatus: "0" },
                url: 'qyServices.aspx?method=QueryMoveOrderData',
                beforeSend: function () { $("#loading").show(); },
                success: function (rs) {
                    $('#loading').hide();
                    for (var i = 0; i < rs.length; i++) {
                        $('#MoveNo').html(rs[i].MoveNo);
                        $('#MoveNum').html(rs[i].MoveNum);
                        $('#OldHouseName').html(rs[i].OldHouseName);
                        $('#NewHouseName').html(rs[i].NewHouseName);
                        var zN = 0, sN = 0, nN = 0;
                        if (rs[i].MoveGoodsList.length > 0) {
                            for (var j = 0; j < rs[i].MoveGoodsList.length; j++) {
                                zN += Number(rs[i].MoveGoodsList[j].Piece);
                                sN += Number(rs[i].MoveGoodsList[j].ScanNum);
                                var ws = Number(rs[i].MoveGoodsList[j].Piece) - Number(rs[i].MoveGoodsList[j].ScanNum);
                                $('#ltlUnCheck').append("<div class=\"weui-cell\"><div class=\"weui-cell__bd weui-cell_primary\"><div class=\"weui_cell_title\" style=\"font-size: 12px;\">产品ID：" + rs[i].MoveGoodsList[j].ProductID + "&nbsp;规格：<span style=\"font-weight:bold;\">" + rs[i].MoveGoodsList[j].Specs + "  " + rs[i].MoveGoodsList[j].Figure + " " + rs[i].MoveGoodsList[j].LoadIndex + rs[i].MoveGoodsList[j].SpeedLevel + "</span></div><div class=\"weui_cell_info\" style=\"font-size: 12px;\">货位：<span style=\"font-weight:bold;\">" + rs[0].MoveGoodsList[j].ContainerCode + "</span>&nbsp;批次：<span style=\"font-weight:bold;\">" + rs[i].MoveGoodsList[j].Batch + "</span></div></div><div class=\"weui-cell__ft\"><div class=\"weui_cell_title\" style=\"font-size: 13px;\">移库数量：" + rs[i].MoveGoodsList[j].Piece + "条</div><div class=\"weui_cell_info\" id=\"" + rs[i].MoveGoodsList[j].ContainerGoodsID + "\"><span style=\"font-size: 15px; color: red;\">未：" + ws + "条</span>&nbsp;&nbsp;<span style=\"font-size: 15px; color: green;\">已：" + rs[i].MoveGoodsList[j].ScanNum + "条</span></div></div></div>");
                            }
                        }
                        nN = zN - sN;
                        $('#ZScan').html("总未扫描：<span style=\"font-weight: bold; color: red; font-size: 20px;\">" + nN + "</span>&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;已扫描：<span style=\"font-weight: bold; color: darkgreen; font-size: 20px;\">" + sN + "</span>&nbsp;条");
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
