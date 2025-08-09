<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddGtmcOrder.aspx.cs" Inherits="Cargo.GTMC.AddGtmcOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>扫描开单</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/demos.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <script src="../Weixin/WeUI/JS/jweixin-1.6.0.js"></script>
</head>

<body ontouchstart>
    <a class="weui-btn weui-btn_primary" id="btnScan">点击扫描条码</a>
    <div class="weui-cells weui-cells_form" style="font-size: 15px;">
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">纳入番号</label>
            </div>
            <div class="weui-cell__bd">
                <div id="TakeNo"></div>
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">广丰单号</label>
            </div>
            <div class="weui-cell__bd">
                <div id="GtmcNo"></div>
            </div>
        </div>
        <div class="weui-cell">
            <div class="weui-cell__hd">
                <label class="weui-label">看板箱数</label>
            </div>
            <div class="weui-cell__bd">
                <div id="Piece"></div>
            </div>
        </div>
        <div id="GDetail"></div>
    </div>
    <input hidden="hidden" id="TID" type="text" />
    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" id="btnSubmit">保存开单</a>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>

    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script src="../Weixin/WeUI/JS/city-picker.js"></script>
    <script type="text/javascript">

        $(function () {

            //configjssdk();

        });
        $('#btnScan').on('click', function () {
            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是条形码（一维码），默认二者都有
                success: function (res) {
                    if (res.resultStr == "" || res.resultStr == undefined) {
                        $.toptip('条码扫描有误', 'warning');
                        return;
                    }
                    // $.showLoading("扫描数据中...", "div");
                    var tagcode = '';
                    var rs = res.resultStr.split(',');
                    if (rs.length == 1) { tagcode = rs[0]; }
                    if (rs.length == 2) { tagcode = rs[1]; }
                    $('#TakeNo').html(tagcode);
                    $.ajax({
                        url: 'GTMCApi.aspx?method=QueryGTMCOrderByID',
                        type: 'post', dataType: 'json', data: { TakeNo: tagcode },
                        success: function (res) {
                            $.hideLoading();//隐藏后的回调函数  
                            if (res != "") {
                                $('#TID').val(res.TID);
                                $('#TakeNo').html(res.TakeNo);
                                $('#GtmcNo').html(res.GtmcNo);
                                $('#Piece').html(res.Piece);
                                var hn = "<table><thead><tr><th width='150px'>品番</th><th>背番</th><th>看板张数</th><th>A仓库存</th><th>其它库存</th></tr></thead><tbody style='text-align:center;'>";
                                for (var k = 0; k < res.orderDetail.length; k++) {
                                    hn += "<tr><td>" + res.orderDetail[k].GoodsCode + "</td><td>" + res.orderDetail[k].Specs + "</td><td>" + res.orderDetail[k].Piece + "</td><td>" + res.orderDetail[k].Stock + "</td><td>" + res.orderDetail[k].NoStock + "</td></tr>";

                                }
                                hn += "</tbody></table>";
                                $('#GDetail').html(hn);
                            }
                        }
                    });
                },
                error: function (res) {
                    if (res.errMsg.indexOf('function_not_exist') > 0) {
                        //alert('版本过低请升级')
                        mui.alert('版本过低请升级', '系统提示', function () { });
                    }
                }
            });
        });
        //保存地址
        $('#btnSubmit').on('click', function () {
            var TID = $('#TID').val();
            $.showLoading();
            $.ajax({
                url: 'GTMCApi.aspx?method=ReSaveGTMCImportData',
                type: 'post', dataType: 'json', data: { TID: TID },
                success: function (text) {
                    $.hideLoading();
                    if (text.Result == true) {
                        $.alert("保存成功!");
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
                url: "GTMCApi.aspx?method=configJssdk",
                cache: false, ifModified: true, async: false,
                success: function (msg) {
                    var json = eval(msg);
                    var config = {};
                    config.beta = true;
                    config.appId = json.appId;
                    config.nonceStr = json.nonceStr;
                    config.signature = json.signature;
                    config.debug = false;        // 添加你需要的JSSDK的权限
                    config.jsApiList = ['getLocation', 'chooseImage', 'openLocation', 'scanQRCode', 'uploadImage', 'downloadImage', 'getLocalImgData', 'previewImage'];
                    config.timestamp = parseInt(json.timestamp);
                    wx.config(config);
                    wx.ready(function () {
                        //alert("jssdk配置成功");
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
