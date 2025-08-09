<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyScanTag.aspx.cs" Inherits="Cargo.QY.MyScanTag" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>扫描标签查询</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <script src="JS/jweixin-1.2.0.js"></script>
    <style type="text/css">
    body {
        color: #666;
        font: 12px/150% Arial,Verdana,"宋体";
        font-family: arial;
    }

    #mohe-kuaidi_new .mh-icon {
        background: url("http://p9.qhimg.com/d/inn/f2e20611/kuaidi_new.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
    }

    p, form, ol, ul, li, h3, menu {
        list-style: outside none none;
    }

    #mohe-kuaidi_new .mh-list-wrap .mh-list {
        border-top: 1px solid #eee;
        margin: 0 15px;
        padding: 15px 0;
    }

        #mohe-kuaidi_new .mh-list-wrap .mh-list ul {
            max-height: 255px;
            _height: 255px;
            padding-left: 75px;
            padding-right: 20px;
            --overflow: auto;
            height: 626px;
        }

        #mohe-kuaidi_new .mh-list-wrap .mh-list li {
            position: relative;
            border-bottom: 1px solid #f5f5f5;
            margin-bottom: 8px;
            padding-bottom: 8px;
            color: #666;
        }

            #mohe-kuaidi_new .mh-list-wrap .mh-list li.first {
                color: #3eaf0e;
            }

            #mohe-kuaidi_new .mh-list-wrap .mh-list li p {
                line-height: 20px;
            }

            #mohe-kuaidi_new .mh-list-wrap .mh-list li .before {
                position: absolute;
                left: -13px;
                top: 2.2em;
                height: 82%;
                width: 0;
                border-left: 2px solid #ddd;
            }

            #mohe-kuaidi_new .mh-list-wrap .mh-list li .after {
                position: absolute;
                left: -16px;
                top: 1.2em;
                width: 8px;
                height: 8px;
                background: #ddd;
                border-radius: 6px;
            }

            #mohe-kuaidi_new .mh-list-wrap .mh-list li.first .after {
                background: #3eaf0e;
            }

    #mohe-kuaidi_new .mh-icon-new {
        position: absolute;
        left: -20px;
        top: 1.5em;
        width: 41px;
        height: 18px;
        margin-left: -41px;
        margin-top: -9px;
        background-position: 0 -58px;
    }
</style>
</head>
<body>
    <audio id="chatAudio">
        <source src="../upload/audio/success.mp3" type="audio/mpeg" />
    </audio>
    <audio id="failAudio">
        <source src="../upload/audio/fail.mp3" type="audio/mpeg" />
    </audio>

    <div style="margin-top: 1px; margin-bottom: 1px;">
        <a href="javascript:ScanMove();" class="weui-btn bg-green">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;扫描标签&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
    </div>
    <div style="text-align: center; font-weight: bold; margin-top: 5px" id="ZScan"></div>
    <div class="weui-cells" style="margin-top: 0px;">
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>所属仓库：</p>
            </div>
            <input class="weui-input" id="HouseID" type="text" style="width: 80%; text-align: right;" value="" />
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>轮胎条码：</p>
            </div>
            <input class="weui-input" id="TyreCode" type="text" style="width: 80%; text-align: right;" value="" />
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>轮胎信息：</p>
            </div>
            <div class="weui-cell__ft" style="color: black;" id="Tyre"></div>
        </div>

        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>入库信息：</p>
            </div>
            <div class="weui-cell__ft" style="color: black;" id="Dest"></div>
        </div>

        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>出库信息：</p>
            </div>
            <div class="weui-cell__ft" style="color: black;" id="OutInfo"></div>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>订单信息：</p>
            </div>
            <div class="weui-cell__ft" style="color: black;" id="AcceptName"></div>
        </div>
        <div class="weui-cell" style="font-size: 13px;">
            <div class="weui-cell__bd">
                <p>签收信息：</p>
            </div>
            <div class="weui-cell__ft" style="color: black;" id="DeliveryType"></div>
        </div>

    </div>
    <div style="text-align: left; position: absolute; right: 50%; left: 50%; transform: translateX(-50%); width: 80%;">
        <div data-mohe-type="kuaidi_new" class="g-mohe" id="mohe-kuaidi_new">
            <div>
                <div class="mohe-wrap mh-wrap">
                    <div class="mh-cont mh-list-wrap mh-unfold">
                        <div class="mh-list" id="lblStatusTrack">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            configjssdk();
            //$("#lblStatusTrack").html('');

            //$.ajax({
            //    url: 'qyServices.aspx?method=ScanTagCode',
            //    type: 'post', dataType: 'json', data: { TagCode: "750107225" },
            //    success: function (res) {
            //        if (res.HouseName == null || res.HouseName == "") {
            //            //$.toast(res.Message, '标签不存在或未入库')
            //            $('#ZScan').html("标签不存在或未入库");
            //        }
            //        else {
            //            $('#ZScan').html(res.TagCode);
            //            $('#HouseID').val(res.HouseName);
            //            $('#TyreCode').val(res.TyreCode);

            //            var ics = res.InCargoStatus == "1" ? "已入库" : "未入库";

            //            $('#Tyre').html(res.TypeName + "&nbsp;&nbsp;&nbsp;" + res.Specs + "  " + res.Figure + "  " + res.LoadIndex + res.SpeedLevel + "&nbsp;&nbsp;&nbsp;" + res.Batch);
            //            $('#Dest').html(ics + "&nbsp;&nbsp;&nbsp;" + res.InHouseTime.replace("T", " "));

            //            var ocs = "未出库"; var odo = "";
            //            if (res.OutCargoStatus == "0") {
            //                ocs = "未出库";
            //            } else {
            //                ocs = "已出库";
            //                if (res.OrderNo != "") {
            //                    odo = res.StartDate.replace("T", " ");
            //                    $('#AcceptName').html(res.OrderNo + "&nbsp;&nbsp;&nbsp;" + res.Company + "&nbsp;&nbsp;&nbsp;" + res.Supplier);

            //                    var sgo = "运输在途";
            //                    if (res.MoveStatus == "5") {
            //                        sgo = "已签收" + "&nbsp;&nbsp;&nbsp;" + res.EndDate.replace("T", " ");
            //                    }
            //                    $('#DeliveryType').html(sgo);


            //                } else {
            //                    ocs = "移库";
            //                    odo = res.SourceOrderNo;
            //                }
            //            }
            //            $('#OutInfo').html(ocs + "&nbsp;&nbsp;&nbsp;" + odo);
            //            $('#lblStatusTrack').html(res.Memo);

            //        }
            //    }
            //});
        });
        function ScanMove() {

            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是条形码（一维码），默认二者都有
                success: function (res) {
                    $('#ZScan').html("");
                    $('#HouseID').val("");
                    $('#TyreCode').val("");
                    $('#Tyre').html("");
                    $('#Dest').html("");
                    $('#AcceptName').html("");
                    $('#DeliveryType').html("");
                    $('#OutInfo').html("");
                    $("#lblStatusTrack").html('');
                    // 回调
                    //alert(res.resultStr);
                    if (res.resultStr == "" || res.resultStr == undefined) {
                        $('#failAudio')[0].play();//扫描失败
                        //$.alert("扫描标签条码有误", "系统提示", function () { });
                        return;
                    }
                    var strs = new Array(); //定义一数组
                    strs = res.resultStr.split(","); //字符分割
                    var TagCode;
                    if (strs.length > 0) {
                        TagCode = strs[strs.length - 1];
                    }
                    $.ajax({
                        url: 'qyServices.aspx?method=ScanTagCode',
                        type: 'post', dataType: 'json', data: { TagCode: TagCode },
                        success: function (res) {
                            if (res.HouseName == null || res.HouseName == "") {
                                $('#failAudio')[0].play();//扫描失败
                                //$.toast(res.Message, '标签不存在或未入库')
                                $('#ZScan').html("标签不存在或未入库");
                            }
                            else {
                                $('#chatAudio')[0].play();//扫描成功
                                $('#ZScan').html(res.TagCode);
                                $('#HouseID').val(res.HouseName);
                                $('#TyreCode').val(res.TyreCode);
                                var ics = res.InCargoStatus == "1" ? "已入库" : "未入库";

                                $('#Tyre').html(res.TypeName + "&nbsp;&nbsp;&nbsp;" + res.Specs + "  " + res.Figure + "  " + res.LoadIndex + res.SpeedLevel + "&nbsp;&nbsp;&nbsp;" + res.Batch);
                                $('#Dest').html(ics + "&nbsp;&nbsp;&nbsp;" + res.InHouseTime.replace("T", " "));

                                var ocs = "未出库"; var odo = "";
                                if (res.OutCargoStatus == "0") {
                                    ocs = "未出库";
                                } else {
                                    ocs = "已出库";
                                    if (res.OrderNo != "") {
                                        odo = res.StartDate.replace("T", " ");
                                        $('#AcceptName').html(res.OrderNo + "&nbsp;&nbsp;&nbsp;" + res.Company + "&nbsp;&nbsp;&nbsp;" + res.Supplier);

                                        var sgo = "运输在途";
                                        if (res.MoveStatus == "5") {
                                            sgo = "已签收" + "&nbsp;&nbsp;&nbsp;" + res.EndDate.replace("T", " ");
                                        }
                                        $('#DeliveryType').html(sgo);


                                    } else {
                                        ocs = "移库";
                                        odo = res.SourceOrderNo;
                                    }
                                }
                                $('#OutInfo').html(ocs + "&nbsp;&nbsp;&nbsp;" + odo);
                                $('#lblStatusTrack').html(res.Memo);
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
