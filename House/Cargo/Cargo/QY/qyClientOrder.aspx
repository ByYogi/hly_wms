<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qyClientOrder.aspx.cs" Inherits="Cargo.QY.qyClientOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的客户订单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
</head>
<body ontouchstart>
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0; z-index: 200; height: 30px;">
        <div class="weui-flex">
            <div class="weui-flex__item">
                <input type="search" class="search-input" id='search' placeholder='请输入客户姓名' />
            </div>
            <div>
                <button class="weui-btn weui-btn_mini weui-btn_primary" id="btnCar"><i class="icon icon-4"></i></button>
            </div>
        </div>
    </header>
    <div class='weui-content'>
        <div id="divALL"></div>

    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            $(document).on("click", "#btnCar", function () {
                var searchKey = $('#search').val();
                $.showLoading();
                $.ajax({
                    url: 'qyServices.aspx?method=QueryWeixinOrderInfo',
                    cache: false, dataType: 'json', data: { key: searchKey },
                    success: function (s) {
                        $.hideLoading();
                        var ALL = '';
                        for (var j = 0; j < s.length; j++) {
                            var OrderType = '电脑下单';
                            if (s[j].OrderType == "1") { OrderType = "企业号下单"; }
                            else if (s[j].OrderType == "2") { OrderType = "商城订单"; }
                            ALL += "<div class='weui-panel weui-panel_access' style='margin-top:2px;'><div class='weui-panel__hd' style='font-size:14px;color:black;padding:0px;'><span>订单号：" + s[j].OrderNo + "&nbsp;&nbsp;&nbsp;" + s[j].Country + "</span><br/><span>" + s[j].Name + "&nbsp;&nbsp;" + s[j].Cellphone + "&nbsp;&nbsp;" + DateTimeFormatter(s[j].CreateDate) + "</span><br/><span style='font-size: 14px; font-weight: bold; color: #FD6C24;'>总数：" + s[j].Piece + "&nbsp;条&nbsp;&nbsp;&nbsp;总金额：" + s[j].TotalCharge + "&nbsp;元</span><a href='javascript:PickOrder(\"" + s[j].OrderNo + "\");' class='ords-btn-pay' style='margin-botton:5px;'>改价</a></div></div>";
                        }
                        $('#divALL').html(ALL);
                    }
                });
            });
        });
        function PickOrder(no) {
            $.confirm("确定修改价格?", "确认", function () {
                //跳转到修改订单价格界面
                window.location.href = "qyModifyPrice.aspx?OrderNo=" + no;
            }, function () {
                //取消操作
            });
        }
        function DateTimeFormatter(val) {
            if (val == null || val == '') {
                return ''
            }
            var dt = parseToDate(getDate(val));
            if (dt.format("yyyy-MM-dd") == "1901-01-01") {
                return ""
            }
            if (dt.format("yyyy-MM-dd") == "1900-01-01") {
                return ""
            }
            if (dt.format("yyyy-MM-dd") == "0001-01-01") {
                return ""
            }
            if (dt.format("yyyy-MM-dd") == "1-01-01") {
                return ""
            }
            return dt.format("yyyy-MM-dd hh:mm:ss")
        }
        function getDate(strDate) {
            if (strDate instanceof Date) {
                return strDate
            } else {
                var date = eval('new Date(' + strDate.replace(/\d+(?=-[^-]+$)/, function (a) {
                    return parseInt(a, 10) - 1
                }).match(/\d+/g) + ')');
                return date
            }
        }
        function parseToDate(value) {
            if (value == null || value == '') {
                return undefined
            }
            var dt;
            if (value instanceof Date) {
                dt = value
            } else {
                if (!isNaN(value)) {
                    dt = new Date(value)
                } else if (value.indexOf('/Date') > -1) {
                    value = value.replace(/\/Date(−?\d+)\//, '$1');
                    dt = new Date();
                    dt.setTime(value)
                } else if (value.indexOf('/') > -1) {
                    dt = new Date(Date.parse(value.replace(/-/g, '/')))
                } else {
                    dt = new Date(value)
                }
            }
            return dt
        }
        Date.prototype.format = function (format) {
            var o = {
                "M+": this.getMonth() + 1,
                "d+": this.getDate(),
                "h+": this.getHours(),
                "m+": this.getMinutes(),
                "s+": this.getSeconds(),
                "q+": Math.floor((this.getMonth() + 3) / 3),
                "S": this.getMilliseconds()
            };
            if (/(y+)/.test(format)) format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(format)) format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            return format
        };
    </script>
</body>
</html>
