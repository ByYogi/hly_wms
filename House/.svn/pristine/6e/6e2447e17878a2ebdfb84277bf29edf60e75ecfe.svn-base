<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qyApplicationCheck.aspx.cs" Inherits="Cargo.QY.qyApplicationCheck" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0; z-index: 200;">
        <div class="wy-header-title"><%=WXORDER %></div>
    </header>
    <div class="weui-content" style="padding-left: 0px; padding-right: 0px;">
        <ul class="weui-payrec">
            <div class="weui-pay-m">
                <li class="weui-pay-order">
                    <asp:Literal ID="ltlApprove" runat="server"></asp:Literal>
                </li>
            </div>
        </ul>
        <%-- 关联数据 --%>
        <asp:Literal ID="ltlRelate" runat="server"></asp:Literal>
        <%-- 附件资料 --%>
        <asp:Literal ID="ltlFile" runat="server"></asp:Literal>
        <div class="weui-cells weui-cells_form" style="font-size: 12px;">
            <div class="weui-cell" id="spyj">
                <div class="weui-cell__bd">
                    <textarea class="weui-textarea" id="Reason" placeholder="请输入审批意见" rows="2" onkeyup="textarea(this);"></textarea>
                    <div class="weui-textarea-counter"><span>0</span>/<i>100</i></div>
                </div>
                <i class="weui-icon-clear" onclick="cleararea(this)"></i>
            </div>
        </div>
        <%-- 审批流程 --%>
        <asp:Literal ID="ltlRoute" runat="server"></asp:Literal>
    </div>
    <div>
        <a href="javascript:checkOk();" id="btnOk" class="weui-btn weui-btn_primary" style="width: 45%; float: left; margin-left: 20px;">通过审批</a>
        <a href="javascript:checkNo();" id="btnNo" class="weui-btn weui-btn_warn" style="width: 45%;">拒绝审批</a>
    </div>
    <div style="margin-top: 10px;">
        <a href="javascript:Close();" id="btnClose" class="weui-btn weui-btn_warn">关闭</a>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        function textarea(input) {
            var content = $(input);
            var max = content.next().find('i').text();
            var value = content.val();
            if (value.length > 0) {

                value = value.replace(/\n|\r/gi, "");
                var len = value.length;
                content.next().find('span').text(len);
                if (len > max) {
                    content.next().addClass('f-red');
                } else {
                    content.next().removeClass('f-red');
                }
            }
        }
        function cleararea(obj) {
            $(obj).prev().find('.weui-textarea').val("");
            return false;
        }
        $(function () {
            $('#btnClose').show();
            $('#btnOk').show();
            $('#btnNo').show();
            $('#spyj').show();
            $('#cResult').hide();
            FastClick.attach(document.body);
            var odn = "<%=approveEntity.ID%>";
            if (odn == undefined || odn == "") {
                $.alert("该申请单不存在，请核实");
                return;
            }
            var ApplyStatus = "<%=approveEntity.ApplyStatus%>";
            var ln = "<%=QyUserInfo.UserID%>";
            var IsCheck = "<%=IsCheck%>";

            if (ApplyStatus != "0" && ApplyStatus != "1") {
                $('#btnClose').show();
                $('#btnOk').hide();
                $('#btnNo').hide();
                $('#spyj').hide();
                $('#cResult').show();
            } else {
                $('#btnClose').show();
                $('#btnOk').show();
                $('#btnNo').show();
                $('#spyj').show();
                $('#cResult').show();
            }
            if (IsCheck == "0") {
                $('#btnClose').show();
                $('#btnOk').hide();
                $('#btnNo').hide();
                $('#spyj').hide();
                $('#cResult').show();
            }
        });
        function Close() {
            WeixinJSBridge.call('closeWindow');
        }
        //通过审批
        function checkOk() {
            var ty = "<%=ty%>";
            var OID = "<%=approveEntity.ID%>";
            var Reason = $('#Reason').val();
            if (Reason == undefined || Reason == "") {
                $.alert("审批意见不能为空");
                return;
            }
            $.confirm("", "您确定通过审批?", function () {
                $.showLoading();
                $.ajax("qyServices.aspx?method=ApplicationCheckOk&Reason=" + Reason + "&ID=" + OID + "&ty=0", {
                    async: true, type: 'POST', dataType: 'json', //服务器返回json格式数据
                    success: function (text) {
                        $.hideLoading();
                        if (text.Result == true) {
                            $.alert("审批成功", "操作提示", function () {
                                if (ty == "1") {
                                    location.href = "qyMyCheck.aspx";
                                } else {
                                    WeixinJSBridge.call('closeWindow');
                                }
                            });
                        }
                        else {
                            $.alert('审批失败：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });

        }
        //拒绝审批
        function checkNo() {
            var ty = "<%=ty%>";
            var OID = "<%=approveEntity.ID%>";

            var Reason = $('#Reason').val();
            if (Reason == undefined || Reason == "") {
                $.alert("拒审意见不能为空");
                return;
            }
            $.confirm("", "您确定要拒审吗?", function () {
                $.showLoading();
                $.ajax("qyServices.aspx?method=ApplicationCheckOk&Reason=" + Reason + "&ID=" + OID + "&ty=1", {
                    async: true, type: 'POST', dataType: 'json', //服务器返回json格式数据
                    success: function (text) {
                        $.hideLoading();
                        if (text.Result == true) {
                            $.alert("拒审成功", "操作提示", function () {
                                if (ty == "1") {
                                    location.href = "qyMyCheck.aspx";
                                } else {
                                    WeixinJSBridge.call('closeWindow');
                                }
                            });
                        }
                        else {
                            $.alert('拒审失败：' + text.Message);
                        }
                    }
                });
            }, function () {
                //取消操作
            });

        }

    </script>
</body>
</html>
