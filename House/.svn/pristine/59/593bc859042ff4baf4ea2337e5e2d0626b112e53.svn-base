<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDeliveryInfo.aspx.cs" Inherits="Cargo.QY.MyDeliveryInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传提货照片</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <script src="JS/jweixin-1.2.0.js"></script>
</head>
<body ontouchstart>
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0; z-index: 200;">
        <div class="wy-header-title" style="margin: 0px;">
            <input type="search" style="box-sizing: content-box; width: 70%; line-height: 2.1; display: inline-block; border: 0; -webkit-appearance: none; appearance: none; border-radius: 10px; font-family: inherit; color: #000000; font-size: 16px; font-weight: normal; padding: 0 2px; margin-bottom: 2px; background-color: #fff; border: 1px solid #2d99e6; opacity: .9;"
                id='searchInput' placeholder='请输入订单号' onkeyup="enterSearch(event)" />
            <a href="javascript:ScanMove();">
                <img src="Image/scan.png" alt="点击扫描订单" /></a>
        </div>
    </header>

    <div class='weui-content' style="padding-top: 47px;">
        <div id="tab4" class="weui-tab__bd-item">
        </div>
    </div>

    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        function enterSearch(e) {
            if (e.keyCode == 13) {
                var searchKey = $('#searchInput').val();
                if (searchKey == "") {
                    alert('请输入订单号');
                    return;
                }
                QueryPro();
            }
        }
        function QueryPro() {
            var searchKey = $('#searchInput').val();
            $.showLoading();
            $.ajax({
                url: 'qyServices.aspx?method=QueryUnSignOrderList',
                cache: false, dataType: 'json', data: { key: searchKey },
                success: function (text) {
                    $.hideLoading();
                    var it = text;
                    var str = "<div class='weui-panel weui-panel_access'><div class='weui-panel__hd' style='font-size:15px;color:black;'><span>订单号：" + it.OrderNo + "&nbsp;&nbsp;" + it.Dep + "&nbsp;&nbsp;" + it.Dest + "</span><br/><span>收货人：" + it.AcceptPeople + "&nbsp;&nbsp;" + it.AcceptCellphone + "&nbsp;&nbsp;" + it.AcceptAddress + "</span><br/><span>条数：" + it.Piece + "条&nbsp;&nbsp;业务员：" + it.SaleManName + "</span></div></div>";
                    str += "<div>";
                    if (it.AwbStatus == "0" || it.AwbStatus == "1" || it.AwbStatus == "2" || it.AwbStatus == "3" || it.AwbStatus == "4") {
                        str += "<a href = \"javascript:PickOrder(" + it.OrderID + "," + it.CreateAwbID + ");\" id = \"btnOk\"  class=\"weui-btn weui-btn_primary bg-gray f-black\" style = \"width: 45%; float: left; margin-left: 20px;\">上传提货照片</a>";
                    }
                    //if (it.AwbStatus == "8") {
                    //    str += "<a href = \"javascript:ArriveOrder(" + it.OrderID + ");\" id = \"btnStartSend\"  class=\"weui-btn weui-btn_primary bg-gray f-black\" style = \"width: 45%; float: left; margin-left: 20px;\">发&nbsp;&nbsp;车</a>";
                    //}
                    if (it.AwbStatus != "5") {
                        str += "<a href=\"javascript:SignOrder(" + it.OrderID + ");\" id=\"btnNo\" class=\"weui-btn weui-btn_primary bg-gray f-black\" style=\"width: 45%;\">上传签收</a>";
                    }
                    str += "</div>";
                    $('#tab4').html(str);
                }
            });
        }
        $(function () {
            configjssdk();
            FastClick.attach(document.body);
        });
        //点击打开摄像头扫描二维码
        function ScanMove() {
            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是条形码（一维码），默认二者都有
                success: function (res) {
                    if (res.resultStr == "" || res.resultStr == undefined) { $.toast("扫描有误", "forbidden"); return; }
                    var strs = new Array(); //定义一数组
                    strs = res.resultStr.split(","); //字符分割
                    var TagCode;
                    if (strs.length > 0) {
                        TagCode = strs[strs.length - 1];
                    }
                    //var OrderNo = res.resultStr;
                    $('#searchInput').val(TagCode);
                    QueryPro();
                },
                error: function (res) {
                    if (res.errMsg.indexOf('function_not_exist') > 0) { $.toast("版本过低请升级", "forbidden"); }
                }
            });
        }
        function configjssdk() {
            var weixinUrl = location.href.split('#')[0];//;
            //配置jssdk, secret: "VkkRCESh5hxT8FStrYa0jWjIg0ux--M670SoFFyuimM" 
            $.ajax({
                type: "post", dataType: "json", cache: false, ifModified: true, async: false,
                url: "qyServices.aspx?method=configJssdk",
                data: { Url: weixinUrl },
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
        //确定上传提货照片
        function PickOrder(no, CreateAwbID) {
           <%-- if (CreateAwbID !=<%=QyUserInfo.UserID%>) {
                alert('只允许开单员上传');
                return;
            }--%>
            $.confirm("确定上传提货照片?", "确认", function () {
                window.location.href = "qyOrderDeliveryPic.aspx?OrderID=" + no;
            }, function () {
                //取消操作
            });
        }
        function OutOrder(no) {
            $.confirm("确认出库完成?", "确认", function () {
                $.ajax({
                    url: 'qyServices.aspx?method=SetOrderStatus&AwbStatus=2',
                    type: 'post', dataType: 'json', data: { data: no },
                    success: function (text) {
                        if (text.Result == true) {
                            $.toast("出库完成!");
                            window.location.reload();
                        }
                        else {
                            $.toast('出库失败 失败原因：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });
        }
        //发车
        function ArriveOrder(no) {
            $.confirm("确认发车?", "确认", function () {
                $.ajax({
                    url: 'qyServices.aspx?method=SetOrderStatus&AwbStatus=3',
                    type: 'post', dataType: 'json', data: { data: no },
                    success: function (text) {
                        if (text.Result == true) {
                            $.toast("发车完成!");
                            window.location.reload();
                        }
                        else {
                            $.toast('失败原因：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });
        }
        function SignOrder(no) {
            window.location.href = "qyOrderSign.aspx?OrderID=" + no;
        }

    </script>
</body>
</html>
