<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyInventory.aspx.cs" Inherits="Cargo.QY.MyInventory" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>盘点扫描</title>
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
        <source src="../upload/audio/OutSuc.mp3" type="audio/mpeg" />
    </audio>
    <audio id="failAudio">
        <source src="../upload/audio/OutFail.mp3" type="audio/mpeg" />
    </audio>
    <audio id="ScanfailAudio">
        <source src="../upload/audio/fail.mp3" type="audio/mpeg" />
    </audio>
    <div class="weui-cells" style="margin-top: 0px;">
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>盘点单号：</p>
            </div>
            <input class="weui-input" id="StockID" type="text" style="width: 80%; text-align: right;" value="" />
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>盘点货位：</p>
            </div>
            <input class="weui-input" id="ContainerCode" type="text" style="width: 80%; text-align: right;" value="" />
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>盘点单库存总数：</p>
            </div>
            <div class="weui-cell__ft" id="StockNum"></div>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>盘点货位库存数：</p>
            </div>
            <div class="weui-cell__ft" id="ContainerNum"></div>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>盘点状态：</p>
            </div>
            <div class="weui-cell__ft" id="TakeStatus"></div>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>提交状态：</p>
            </div>
            <div class="weui-cell__ft" id="TakeSubmitStatus"></div>
        </div>
    </div>
    <div style="margin-top: 1px; margin-bottom: 1px;">
        <a href="javascript:TakeSubmit();" class="weui-btn bg-blue" style="width: 30%; float: left;">提交</a>
        <a href="javascript:ScanMove();" class="weui-btn bg-green" style="width: 60%; margin-top: 2px;">扫描标签</a>
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
        var StockList = new Array();
        var ContainerList = new Array();
        var StID;
        var ContainerCode;
        var mn;
        var reslist;
        var conList;
        $(function () {
            //FastClick.attach(document.body);
            configjssdk();
            BindHouseData();
            //ajaxpage("");

        });
        function BindHouseData() {

            $.ajax({
                url: 'qyServices.aspx?method=QueryStockTakeList',
                type: 'post', dataType: 'json', data: { StartDate: 10 },
                beforeSend: function () { $.showLoading(); },
                success: function (rs) {
                    $.hideLoading();
                    if (rs.length > 0) {
                        reslist = rs;
                        for (var j = 0; j < rs.length; j++) {
                            var info = { "value": rs[j].StockID, "title": rs[j].StockID };
                            StockList.push(info);
                        }

                        $("#StockID").select({
                            title: "请选择盘点单号",
                            items: StockList,
                            onChange: function (d) { },
                            onClose: function (d) {
                                if (d.data.values != undefined && d.data.values != "") {
                                    StID = d.data.values;
                                    for (var i = 0; i < reslist.length; i++) {
                                        if (reslist[i].StockID == Number(StID)) {
                                            $('#StockNum').html(reslist[i].StockNum + "条");
                                            var takeStatus;
                                            if (reslist[i].TakeStatus == "0") {
                                                takeStatus = "已开单";
                                            }
                                            else if (reslist[i].TakeStatus == "1") {
                                                takeStatus = "盘点中";
                                            }
                                            else if (reslist[i].TakeStatus == "2") {
                                                takeStatus = "已完成";
                                            }
                                            $('#TakeStatus').html(takeStatus);
                                            conList = reslist[i].StockTakeContainerList;
                                            //获取货位数据列表
                                            for (var h = 0; h < reslist[i].StockTakeContainerList.length; h++) {
                                                var cont = { "value": reslist[i].StockTakeContainerList[h].ContainerCode, "title": reslist[i].StockTakeContainerList[h].ContainerCode };
                                                ContainerList.push(cont);
                                            }
                                            //获取货位数据列表

                                            $("#ContainerCode").select({
                                                title: "请选择盘点货位",
                                                items: ContainerList,
                                                onChange: function (d) { },
                                                onClose: function (d) {
                                                    if (d.data.values != undefined && d.data.values != "") {
                                                        //alert(d.data.values);
                                                        for (var k = 0; k < conList.length; k++) {
                                                            if (conList[k].ContainerCode == d.data.values) {
                                                                ContainerCode = conList[k].ContainerCode;
                                                                $('#ContainerNum').html(conList[k].StockNum + "条");
                                                                var tsStatus;
                                                                if (conList[k].TakeSubmitStatus == "0") {
                                                                    tsStatus = "未提交";
                                                                }
                                                                else if (conList[k].TakeSubmitStatus == "1") {
                                                                    tsStatus = "已提交";
                                                                }
                                                                $('#TakeSubmitStatus').html(tsStatus);
                                                                QueryHouseOrder();
                                                            }
                                                        }
                                                    }
                                                },
                                                onOpen: function () { },
                                            });

                                        }
                                    }
                                }
                            },
                            onOpen: function () { },
                        });
                    }
                    else {
                        $.toast("无盘点单", 'warning')
                    }

                }
            });


            //CurHID = ";
            //QueryHouseOrder();
            //$('#StockID').val("");
        }
        //查询该盘点货位的标签详细数据
        function QueryHouseOrder() {
            $.ajax({
                url: 'qyServices.aspx?method=QueryCargoStockTagList',
                type: 'post', dataType: 'json', data: { StockID: StID, ContainerCode: ContainerCode },
                beforeSend: function () { $.showLoading(); },
                success: function (rs) {
                    $.hideLoading();
                    var zN = 0, sN = 0, nN = 0;
                    if (rs.length > 0) {
                        zN = Number(rs.length);
                        var tsStatus;
                        if (rs[0].TakeSubmitStatus == "0") {
                            tsStatus = "未提交";
                        }
                        else if (rs[0].TakeSubmitStatus == "1") {
                            tsStatus = "已提交";
                        }
                        $('#TakeSubmitStatus').html(tsStatus);
                        for (var j = 0; j < rs.length; j++) {
                            var lun = "<div class=\"weui-cell\"><div class=\"weui-cell__bd weui-cell_primary\"><div class=\"weui_cell_title\" style=\"font-size: 12px;\">产品ID：" + rs[j].ProductID + "&nbsp;标签号：<span style=\"font-weight:bold;\">" + rs[j].TagCode + "</span></div><div class=\"weui_cell_info\" style=\"font-size: 12px;\">批次：<span style=\"font-weight:bold;\">" + rs[j].Batch + "</span></div></div><div class=\"weui-cell__ft\"><div class=\"weui_cell_info\" id=\"" + rs[j].ContainerID + "\">";

                            if (rs[j].ScanStatus == "1") {
                                sN++;
                                lun += "<span style=\"font-size: 15px; color: green;\">已扫描</span>";

                            }
                            else {
                                lun += "<span style=\"font-size: 15px; color: red;\">未扫描</span>";

                            }
                            lun += "</div></div></div>";
                            $('#ltlUnCheck').append(lun);
                        }
                    }
                    nN = zN - sN;
                    $('#ZScan').html("未扫描：<span style=\"font-weight: bold; color: red; font-size: 20px;\">" + nN + "</span>&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;已扫描：<span style=\"font-weight: bold; color: darkgreen; font-size: 20px;\">" + sN + "</span>&nbsp;条");
                }
            });
            //查询该盘点货位的标签详细数据

        }

        function TEST() {
            $.ajax({
                url: 'qyServices.aspx?method=SaveStockTakeTagScan',
                type: 'post', dataType: 'json', data: { StockID: StID, ContainerCode: ContainerCode, TagCode: '750003862' },
                success: function (res) {
                    if (res.Result) {
                        $('#chatAudio')[0].play();
                        var zN = 0, sN = 0, nN = 0;
                        var lun;
                        //操作成功
                        var g = eval(res.Message);
                        zN = Number(g.length);
                        for (var i = 0; i < g.length; i++) {
                            if (g[i].ScanStatus == "1") {
                                sN++;
                                lun = "<span style=\"font-size: 15px; color: green;\">已扫描</span>";

                            }
                            else {
                                lun = "<span style=\"font-size: 15px; color: red;\">未扫描</span>";

                            }
                            $('#' + g[i].ContainerID + '').html(lun);
                        }
                        nN = zN - sN;
                        $('#ZScan').html("未扫描：<span style=\"font-weight: bold; color: red; font-size: 17px;\">" + nN + "</span>&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;已扫描：<span style=\"font-weight: bold; color: darkgreen; font-size: 17px;\">" + sN + "</span>&nbsp;条");
                    }
                    else {
                        $('#failAudio')[0].play();
                        $.toptip(res.Message, 'warning')
                        //$.toast(res.Message, "forbidden");
                    }
                }
            });
        }
        //提交货位盘点
        function TakeSubmit() {
            $.ajax({
                url: 'qyServices.aspx?method=TakeScanSubmit',
                type: 'post', dataType: 'json', data: { StockID: StID, ContainerCode: ContainerCode },
                success: function (res) {
                    if (res.Result) {
                        //提交成功
                        $.toast("提交成功");
                        //$.toptip("提交成功", 'warning')
                    }
                    else {
                        $.toptip(res.Message, 'warning')
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
                    if (res.resultStr == "" || res.resultStr == undefined) {
                        //mui.alert('扫描标签条码有误', '系统提示', function () { });
                        $('#ScanfailAudio')[0].play();
                        $.toast("扫描标签条码有误", "forbidden");
                        return;
                    }
                    var TagCode = res.resultStr;
                    //alert(TagCode);
                    $.ajax({
                        url: 'qyServices.aspx?method=SaveStockTakeTagScan',
                        type: 'post', dataType: 'json', data: { StockID: StID, ContainerCode: ContainerCode, TagCode: TagCode },
                        success: function (res) {
                            if (res.Result) {
                                $('#chatAudio')[0].play();
                                var zN = 0, sN = 0, nN = 0;
                                var lun;
                                //操作成功
                                var g = eval(res.Message);
                                zN = Number(g.length);
                                for (var i = 0; i < g.length; i++) {
                                    if (g[i].ScanStatus == "1") {
                                        sN++;
                                        lun = "<span style=\"font-size: 15px; color: green;\">已扫描</span>";

                                    }
                                    else {
                                        lun = "<span style=\"font-size: 15px; color: red;\">未扫描</span>";

                                    }
                                    $('#' + g[i].ContainerID + '').html(lun);
                                }
                                nN = zN - sN;
                                $('#ZScan').html("未扫描：<span style=\"font-weight: bold; color: red; font-size: 17px;\">" + nN + "</span>&nbsp;条&nbsp;&nbsp;&nbsp;&nbsp;已扫描：<span style=\"font-weight: bold; color: darkgreen; font-size: 17px;\">" + sN + "</span>&nbsp;条");
                            }
                            else {
                                $('#failAudio')[0].play();
                                $.toptip(res.Message, 'warning')
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
        function ajaxpage(HID, OrderNo) {
            $('#ltlUnCheck').html("");
            $.ajax({
                type: "POST", dataType: "json", data: { HouseID: HID, OrderNo: OrderNo, OrderModel: "0", StartDate: 13 },
                url: 'qyServices.aspx?method=QueryOrderData',
                beforeSend: function () { $.showLoading(); },
                success: function (rs) {
                    $.hideLoading();
                    for (var i = 0; i < rs.length; i++) {
                        $('#OrderNo').html(rs[0].OrderNo);
                        $('#Dest').html(rs[0].Dep + "---" + rs[0].Dest);
                        $('#AcceptName').html(rs[0].AcceptPeople + " " + rs[0].AcceptCellphone);
                        $('#Piece').html(rs[0].Piece);
                        $('#Remark').html(rs[0].Remark);
                        var zN = 0, sN = 0, nN = 0;
                        if (rs[i].ContainerShowEntity.length > 0) {
                            for (var j = 0; j < rs[i].ContainerShowEntity.length; j++) {
                                zN += Number(rs[i].ContainerShowEntity[j].Piece);
                                sN += Number(rs[i].ContainerShowEntity[j].OldPiece);
                                var ws = Number(rs[i].ContainerShowEntity[j].Piece) - Number(rs[i].ContainerShowEntity[j].OldPiece);
                                $('#ltlUnCheck').append("<div class=\"weui-cell\"><div class=\"weui-cell__bd weui-cell_primary\"><div class=\"weui_cell_title\" style=\"font-size: 12px;\">产品ID：" + rs[0].ContainerShowEntity[j].ProductID + "&nbsp;规格：<span style=\"font-weight:bold;\">" + rs[0].ContainerShowEntity[j].Specs + "  " + rs[0].ContainerShowEntity[j].Figure + " " + rs[0].ContainerShowEntity[j].LoadIndex + rs[0].ContainerShowEntity[j].SpeedLevel + "</span></div><div class=\"weui_cell_info\" style=\"font-size: 12px;\">货位：<span style=\"font-weight:bold;\">" + rs[0].ContainerShowEntity[j].ContainerCode + "</span>&nbsp;批次：<span style=\"font-weight:bold;\">" + rs[0].ContainerShowEntity[j].Batch + "</span></div></div><div class=\"weui-cell__ft\"><div class=\"weui_cell_title\" style=\"font-size: 13px;\">出库数量：" + rs[0].ContainerShowEntity[j].Piece + "条</div><div class=\"weui_cell_info\" id=\"" + rs[0].ContainerShowEntity[j].ContainerNum + "\"><span style=\"font-size: 15px; color: red;\">未：" + ws + "条</span>&nbsp;&nbsp;<span style=\"font-size: 15px; color: green;\">已：" + rs[0].ContainerShowEntity[j].OldPiece + "条</span></div></div></div>");
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
