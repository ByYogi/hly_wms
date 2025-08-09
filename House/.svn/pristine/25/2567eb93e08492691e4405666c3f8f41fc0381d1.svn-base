<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qyExpenseCheck.aspx.cs" Inherits="Cargo.QY.qyExpenseCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的审批</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
</head>
<body ontouchstart>
    <div class='weui-content'>
        <div class="weui-tab">
            <div class="weui-navbar" style="position: fixed; top: 0px; left: 0; right: 0; height: 40px; background: #fff;">
                <a class="weui-navbar__item proinfo-tab-tit font-14 weui-bar__item--on" href="#tab1">未审批</a>
                <a class="weui-navbar__item proinfo-tab-tit font-14" href="#tab2">已审批</a>
            </div>
            <div class="weui-tab__bd proinfo-tab-con" style="padding-top: 41px;">
                <div id="tab1" class="weui-tab__bd-item weui-tab__bd-item--active">
                    <div class="weui-row" id="ltlUnCheck">
                    </div>
                    <div class="weui-panel__ft">
                        <a href="javascript:void(0);" class="weui-cell weui-cell_access weui-cell_link">
                            <div class="weui-cell__bd" id="getmore">点击查看更多<i id="loading" class="weui-loading"></i></div>
                        </a>
                    </div>
                    <div class="weui-panel__ft">&nbsp;</div>
                </div>
                <div id="tab2" class="weui-tab__bd-item">
                    <div class="weui-row" id="ltlCheck">
                    </div>
                    <div class="weui-panel__ft">
                        <a href="javascript:void(0);" class="weui-cell weui-cell_access weui-cell_link">
                            <div class="weui-cell__bd" id="getmoreCheck">点击查看更多<i id="loadingCheck" class="weui-loading"></i></div>
                        </a>
                    </div>
                    <div class="weui-panel__ft">&nbsp;</div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            FastClick.attach(document.body);
        });
        $('#loading').hide();
        function ajaxpage(page) {
            $.ajax({
                type: "POST", dataType: "json",
                url: 'qyServices.aspx?method=QueryUpExpenseCheckInfo',
                data: { page: page, rows: 10 },
                beforeSend: function () {
                    $("#loading").show();
                },
                success: function (rs) {
                    $('#loading').hide();
                    for (var i = 0; i < rs.rows.length; i++) {
                        $('#ltlUnCheck').append("<div class=\"weui-panel__bd\" style=\"width:100%;border-bottom: #e4dfdf 1px solid;\"><div class=\"weui-media-box weui-media-box_text\" style=\"padding-top: 3px;padding-bottom: 3px;\"><h4 class=\"weui-media-box__title\" style=\"font-size:15px;margin-bottom: 3px;\"><a href=\"../QY/qyExpenseApprove.aspx?ExID=" + rs.rows[i].ExID + "&ty=1\">所属仓库：<em class=\"num\">" + rs.rows[i].HouseName + "</em></a></h4><p class=\"weui-media-box__desc\">报销单号：" + rs.rows[i].ExID + "</p><p class=\"weui-media-box__desc\">报销人：" + rs.rows[i].ExName + "</p><p class=\"weui-media-box__desc\">报销原因：" + rs.rows[i].Reason + "</p><p class=\"weui-media-box__desc\">报销时间：" + rs.rows[i].ExDate + " " + DateTimeFormatter(rs.rows[i].ApplyDate) + "</p></div></div>")
                    }
                    var maxpage = Math.ceil(rs.total / 10);
                    sessionStorage['maxpage'] = maxpage;
                }
            });
        }

        $('#loadingCheck').hide();
        function getCheck(page) {
            $.ajax({
                type: "POST", dataType: "json",
                url: 'qyServices.aspx?method=QueryExpenseCheckInfo',
                data: { page: page, rows: 10 },
                beforeSend: function () {
                    $("#loadingCheck").show();
                },
                success: function (rs) {
                    $('#loadingCheck').hide();
                    for (var i = 0; i < rs.rows.length; i++) {
                        var cs = "";
                        if (rs.rows[i].CheckType == "1") { cs = "(抄送)"; }
                        var ApplyStatus = "通过";
                        var lc = "<div class=\"weui-panel__bd\" style=\"width:100%;border-bottom: #e4dfdf 1px solid;\">";
                        if (rs.rows[i].ApplyStatus == "2") {
                            ApplyStatus = "拒审";
                            lc = "<div class=\"weui-panel__bd\" style=\"width:100%;border-bottom: #e4dfdf 1px solid;background-color:#e0676740\">";
                        }
                        if (rs.rows[i].ApplyStatus == "3") {
                            ApplyStatus = "结束";
                            lc = "<div class=\"weui-panel__bd\" style=\"width:100%;border-bottom: #e4dfdf 1px solid;background-color:#57ce574f\">";
                        }
                        lc += "<div class=\"weui-media-box weui-media-box_text\" style=\"padding-top: 3px;padding-bottom: 3px;\"><h4 class=\"weui-media-box__title\" style=\"font-size:15px;margin-bottom: 3px;\"><a href=\"../QY/qyExpenseApprove.aspx?ExID=" + rs.rows[i].ExID + "&ty=1\">所属仓库：<em class=\"num\">" + rs.rows[i].HouseName + cs + "</em></a></h4><p class=\"weui-media-box__desc\">报销单号：" + rs.rows[i].ExID + "</p><p class=\"weui-media-box__desc\">报销人：" + rs.rows[i].ExName + "</p><p class=\"weui-media-box__desc\">报销原因：" + rs.rows[i].Reason + "</p><p class=\"weui-media-box__desc\">报销时间：" + rs.rows[i].ExDate + " " + DateTimeFormatter(rs.rows[i].ApplyDate) + "</p><p class=\"weui-media-box__desc\">审批状态：" + ApplyStatus + "</p><p class=\"weui-media-box__desc\">审批时间：" +  DateTimeFormatter(rs.rows[i].CheckTime) + "</p></div></div>";
                        $('#ltlCheck').append(lc)
                    }
                    var maxpage = Math.ceil(rs.total / 10);
                    sessionStorage['Checkmaxpage'] = maxpage;
                }
            });
        }
        $(function () {
            var page = 2;
            var Chcekpage = 2;
            var maxpage;
            $('#getmore').on('click', function () {
                maxpage = sessionStorage['maxpage'];
                if (page <= maxpage) {
                    ajaxpage(page);
                    page++;
                }
            });
            $('#getmoreCheck').on('click', function () {
                maxpage = sessionStorage['Checkmaxpage'];
                if (Chcekpage <= maxpage) {
                    getCheck(Chcekpage);
                    Chcekpage++;
                }
            });
            getCheck(1);
            ajaxpage(1);
        })
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