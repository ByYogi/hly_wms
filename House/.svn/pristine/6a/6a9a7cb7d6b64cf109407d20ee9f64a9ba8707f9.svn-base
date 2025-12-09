<%@ Page Title="我的费用报销" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="myExpense.aspx.cs" Inherits="Cargo.Finance.myExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .commTblStyle_8 {
        }

            .commTblStyle_8 th {
                border: 1px solid rgb(205, 205, 205);
                text-align: center;
                color: rgb(255, 255, 255);
                line-height: 28px;
                background-color: rgb(15, 114, 171);
            }

            .commTblStyle_8 tr {
            }

                .commTblStyle_8 tr.BlankRow td {
                    line-height: 10px;
                }

                .commTblStyle_8 tr td {
                    border: 1px solid rgb(205, 205, 205);
                    text-align: center;
                    line-height: 20px;
                }

                    .commTblStyle_8 tr td.left {
                        text-align: right;
                        padding-right: 10px;
                        font-weight: bold;
                        white-space: nowrap;
                        background-color: rgb(239, 239, 239);
                    }

                    .commTblStyle_8 tr td.right {
                        text-align: left;
                        padding-left: 10px;
                    }

            .commTblStyle_8 .whiteback {
                background-color: rgb(255, 255, 255);
            }

        /*流程图样式*/
        .processBar {
            float: left;
            width: 100px;
            margin-top: 15px;
        }

            .processBar .bar {
                background: rgb(230, 224, 236);
                height: 3px;
                position: relative;
                width: 100px;
                margin-left: 10px;
            }

            .processBar .b-select {
                background: rgb(96, 72, 124);
            }

            .processBar .bar .c-step {
                position: absolute;
                width: 8px;
                height: 8px;
                border-radius: 50%;
                background: rgb(230, 224, 236);
                left: -12px;
                top: 50%;
                margin-top: -4px;
            }

            .processBar .bar .c-select {
                width: 10px;
                height: 10px;
                margin: -5px -1px;
                background: rgb(96, 72, 124);
            }

        .main-hide {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }

        .poetry {
            color: rgb(41, 41, 41);
            font-family: KaiTi_GB2312, KaiTi, STKaiti;
            font-size: 16px;
            background-color: transparent;
            font-weight: bold;
            font-style: normal;
            text-decoration: none;
        }

        button {
            width: 80px;
            line-height: 30px;
            font-size: 11px;
            color: rgb(116, 42, 149);
            text-align: center;
            border-radius: 6px;
            border: 1px solid #e2e2e2;
            cursor: pointer;
            background-color: #fff;
            outline: none;
        }

            button:hover {
                border: 1px solid rgb(179, 161, 200);
            }
    </style>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
    <script type="text/javascript">
        //页面加载显示遮罩层
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //关闭加载中遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }
        window.onload = function () {
            adjustment();

            $('#AReceiveName').combobox({
                onChange: function (newValue, oldValue) {
                    if (newValue != undefined) {
                        $('#HiddenReceiveName').val(newValue);
                    }
                }
            });
            $('#AReceiveNumber').combobox({
                onChange: function (newValue, oldValue) {
                    if (newValue != undefined) {
                        $('#HiddenReceiveNumber').val(newValue);
                    } else {
                        return;
                    }
                    if (newValue != null && newValue != "") {
                        $("#AReceiveNumber").combobox('options').required = true;
                        $("#AReceiveNumber").combobox('textbox').validatebox('options').required = true;
                        $("#AReceiveNumber").combobox('validate');

                        $("#ACardBank").textbox('options').required = true;
                        $("#ACardBank").textbox('textbox').validatebox('options').required = true;
                        $("#ACardBank").textbox('validate');
                    } else {
                        var val = $("input[name='ChargeType']:checked").val();
                        if (val != 1) {
                            $("#AReceiveNumber").combobox('options').required = false;
                            $("#AReceiveNumber").combobox('textbox').validatebox('options').required = false;
                            $("#AReceiveNumber").combobox('validate');

                            $("#ACardBank").textbox('options').required = false;
                            $("#ACardBank").textbox('textbox').validatebox('options').required = false;
                            $("#ACardBank").textbox('validate');
                        }
                    }
                }
            });
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ExID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    {
                        title: '报销单号', field: 'ExID', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '报销人', field: 'ExName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '报销部门', field: 'ExDepart', width: '200px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    { title: '报销时间', field: 'ExDate', width: '80px', formatter: DateFormatter },
                    {
                        title: '报销金额', field: 'ExCharge', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '客户名称', field: 'ClientName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '受款人', field: 'ReceiveName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '受款账号', field: 'ReceiveNumber', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '付款方式', field: 'ChargeType', width: '60px',
                        formatter: function (val, row, index) { if (val == "0") { return "现金"; } else if (val == "1") { return "银行卡"; } else if (val == "2") { return "微信"; } else if (val == "3") { return "油卡"; } else { return ""; } }
                    },
                    {
                        title: '单位', field: 'ChargeUnit', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '报销类别', field: 'CostName', width: '150px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '备注', field: 'Reason', width: '120px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '报销操作人', field: 'OperaName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '上一审批人', field: 'UserName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    { title: '上一审批时间', field: 'CheckTime', width: '120px', formatter: DateTimeFormatter },
                    {
                        title: '下一审批人', field: 'NextCheckName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '审批状态', field: 'Status', width: '60px',
                        formatter: function (val, row, index) { if (val == "0") { return "待审"; } else if (val == "1") { return "审批中"; } else if (val == "2") { return "拒审"; } else if (val == "3") { return "结束"; } else if (val == "4") { return "财务已审批"; } else if (val == "5") { return "领导已审批"; } else { return ""; } }
                    },
                    {
                        title: '结算状态', field: 'CheckStatus', width: '60px',
                        formatter: function (val, row, index) { if (val == "0") { return "未结算"; } else if (val == "1") { return "已结清"; } else if (val == "2") { return "未结清"; } else { return ""; } }
                    },
                    {
                        title: '拒审原因', field: 'DenyReason', width: '90px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#AExDate').datebox('setValue', AllDateTime(datenow));
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Finance/financeApi.aspx?method=QueryMyExpense';
            $('#dg').datagrid('load', {
                ExID: $("#ExID").val(),
                ExName: $("#ExName").val(),
                ExDepart: $("#ExDepart").val(),
                ExCharge: $("#ExCharge").val(),
                ReceiveName: $("#ReceiveName").val(),
                ReceiveNumber: $("#ReceiveNumber").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Status: $("#Status").combobox('getValue')
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">报销单号:
                </td>
                <td>
                    <input id="ExID" class="easyui-textbox" data-options="prompt:'请输入报销单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">报销人:
                </td>
                <td>
                    <input id="ExName" class="easyui-textbox" style="width: 100px">
                </td>
                <td style="text-align: right;">报销部门:
                </td>
                <td>
                    <input id="ExDepart" class="easyui-textbox" style="width: 100px">
                </td>
                <td style="text-align: right;">报销金额:
                </td>
                <td>
                    <input id="ExCharge" class="easyui-textbox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">受款人:
                </td>
                <td>
                    <input id="ReceiveName" class="easyui-textbox" style="width: 100px">
                </td>
                <td style="text-align: right;">受款账号:
                </td>
                <td>
                    <input id="ReceiveNumber" class="easyui-textbox" style="width: 100px">
                </td>

                <td style="text-align: right;">报销状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="Status" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">待审</option>
                        <option value="1">审批中</option>
                        <option value="2">拒审</option>
                        <option value="3">结束</option>
                    </select>
                </td>
                <td style="text-align: right;">报销时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-sitemap_color" plain="false" onclick="Proc()">&nbsp;审批流程图&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 470px; padding: 10px 10px"
        closed="true" buttons="#dlg-buttons">
        <div id="lblTrack">
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <div id="dlgAwbDetail" class="easyui-dialog" style="width: 800px; height: 480px;"
        closed="true" buttons="#dlgAwbDetail-buttons">
        <input id="IsSave" type="hidden" value="0" />
        <form id="fm" class="easyui-form" method="post">
            <input name="ExID" id="AExID" type="hidden" />
            <input name="NextCheckID" id="NextCheckID" type="hidden" />
            <input name="NextCheckName" id="NextCheckName" type="hidden" />
            <input name="AClientID" id="AClientID" type="hidden" />

            <div id="saPanel">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right;">报销人:
                        </td>
                        <td>
                            <input name="ExName" id="AExName" class="easyui-textbox" style="width: 140px" data-options="required:true">
                            <input name="OperaName" type="hidden" readonly="true" id="OperaName" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">报销部门:
                        </td>
                        <td>
                            <%--<input name="ExpenseDate" readonly="true" id="AExpenseDate" class="easyui-datetimebox" style="width: 140px">--%>
                            <input type="hidden" name="ExpenseDate" id="AExpenseDate" />
                            <input name="ExDepart" id="AExDepart" style="width: 150px;" class="easyui-combobox" />
                        </td>
                        <td style="text-align: right;">客户名称:
                        </td>
                        <td>
                            <input name="ClientID" id="ClientID" class="easyui-combobox" style="width: 140px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">受款人:
                        </td>
                        <td>
                            <%--<input name="ReceiveName" id="AReceiveName" class="easyui-textbox" style="width: 100px;" />--%>
                            <input name="ReceiveName" id="AReceiveName" style="width: 140px;" class="easyui-combobox" />
                            <input type="hidden" name="HiddenReceiveName" id="HiddenReceiveName" />
                        </td>
                        <td style="text-align: right;">受款账号:
                        </td>
                        <td>
                            <%--<input name="ReceiveNumber" id="AReceiveNumber" class="easyui-textbox" style="width: 170px;" />--%>
                            <input name="ReceiveNumber" id="AReceiveNumber" style="width: 150px;" class="easyui-combobox" />
                            <input type="hidden" name="HiddenReceiveNumber" id="HiddenReceiveNumber" />
                        </td>
                        <td style="text-align: right;">付款方式:
                        </td>
                        <td>
                            <input name="ChargeType" id="ChargeType0" type="radio" value="0" onclick="radioOnChange()" /><label for="ChargeType0" style="font-size: 14px;">现金</label>
                            <input name="ChargeType" id="ChargeType1" type="radio" value="1" onclick="radioOnChange()" /><label for="ChargeType1" style="font-size: 14px;">银行卡</label>
                            <input name="ChargeType" id="ChargeType2" type="radio" value="2" onclick="radioOnChange()" /><label for="ChargeType2" style="font-size: 14px;">微信</label>
                            <input name="ChargeType" id="ChargeType3" type="radio" value="3" onclick="radioOnChange()" /><label for="ChargeType3" style="font-size: 14px;">油卡</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">报销时间:
                        </td>
                        <td>
                            <input name="ExDate" id="AExDate" class="easyui-datebox" style="width: 140px"
                                data-options="required:true">
                        </td>
                        <td style="text-align: right;">开户行:
                        </td>
                        <td>
                            <input name="CardBank" id="ACardBank" class="easyui-textbox" style="width: 150px">
                        </td>
                        <td style="text-align: right;">审批流程:
                        </td>
                        <td>
                            <input name="ApproveID" id="ApproveID" class="easyui-combobox" style="width: 160px;" data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">备注:
                        </td>
                        <td colspan="3">
                            <textarea name="Reason" id="AReason" rows="3" style="width: 410px;"></textarea>
                        </td>

                        <td style="text-align: right;">单位:
                        </td>
                        <td>
                            <input name="ChargeUnit" id="ChargeUnit" class="easyui-combobox" style="width: 160px;" data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="newRow()">新增详细列表</a>
                        </td>
                        <td>
                            <div id="ApproveInfo"></div>
                        </td>
                        <td style="text-align: right;">报销金额:
                        </td>
                        <td>
                            <input name="ExCharge" id="AExCharge" readonly="true" class="easyui-numberbox" style="width: 100px;"
                                data-options="min:0,precision:2" />
                        </td>
                    </tr>
                </table>
            </div>
            <table id="dgAdd" class="easyui-datagrid">
            </table>
        </form>
    </div>
    <div id="dlgAwbDetail-buttons">
        <a href="#" class="easyui-linkbutton" id="save" iconcls="icon-ok" onclick="saveData()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" id="preprint"
            iconcls="icon-print" onclick="prn1_preview()">&nbsp;打印预览&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;清&nbsp;空&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <script type="text/javascript">
        function radioOnChange() {
            var val = $("input[name='ChargeType']:checked").val();

            if (val == 1) {
                $("#AReceiveNumber").combobox('options').required = true;
                $("#AReceiveNumber").combobox('textbox').validatebox('options').required = true;
                $("#AReceiveNumber").combobox('validate');

                $("#ACardBank").textbox('options').required = true;
                $("#ACardBank").textbox('textbox').validatebox('options').required = true;
                $("#ACardBank").textbox('validate');
            } else {
                $("#AReceiveNumber").combobox('options').required = false;
                $("#AReceiveNumber").combobox('textbox').validatebox('options').required = false;
                $("#AReceiveNumber").combobox('validate');

                $("#ACardBank").textbox('options').required = false;
                $("#ACardBank").textbox('textbox').validatebox('options').required = false;
                $("#ACardBank").textbox('validate');
            }
        }
        function closeDlg() {
            $('#dlgAwbDetail').dialog('close');
            $('#dg').datagrid('reload');
            //editIndex = undefined;
        }
        function reset() {
            $('#fm').form('clear');
            $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
            //editIndex = undefined;
        }

        function ShowDG() {
            $('#dgAdd').datagrid({
                width: '100%',
                height: '230px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'DetailID',
                url: null,
                columns: [[
                    {
                        title: '收入支出', field: 'ZID', width: '60px', formatter: function (value, row) {
                            return row.ZName;
                        },
                        editor: {
                            type: 'combobox',
                            options: {
                                panelHeight: 'auto', valueField: 'ZID', textField: 'ZName', url: 'financeApi.aspx?method=GetZeroSubject', required: true, editable: false,
                                onSelect: function (data) {
                                    var row = $('#dgAdd').datagrid('getSelected');
                                    var rowIndex = $('#dgAdd').datagrid('getRowIndex', row); //获取行号
                                    if (NeweditIndex == undefined) { NeweditIndex = rowIndex; }
                                    var thisTarget = $('#dgAdd').datagrid('getEditor', { 'index': NeweditIndex, 'field': 'ZID' }).target;
                                    var value = thisTarget.combobox('getValue');

                                    var target = $('#dgAdd').datagrid('getEditor', { 'index': NeweditIndex, 'field': 'FID' }).target;
                                    target.combobox('clear'); //清除原来的数据
                                    var url = 'financeApi.aspx?method=GetFIDByZID&id=' + value;
                                    target.combobox('reload', url); //联动下拉列表重载  
                                }
                            }
                        }
                    },
                    {
                        title: '一级科目', field: 'FID', width: '90px', formatter: function (value, row) {
                            return row.FName;
                        },
                        editor: {
                            type: 'combobox',
                            options: {
                                panelHeight: 'auto', valueField: 'FID', textField: 'FName', url: 'financeApi.aspx?method=GetFirstSubject', required: true, editable: false,
                                onSelect: function (data) {
                                    var row = $('#dgAdd').datagrid('getSelected');
                                    var rowIndex = $('#dgAdd').datagrid('getRowIndex', row); //获取行号
                                    if (NeweditIndex == undefined) { NeweditIndex = rowIndex; }
                                    var thisTarget = $('#dgAdd').datagrid('getEditor', { 'index': NeweditIndex, 'field': 'FID' }).target;
                                    var value = thisTarget.combobox('getValue');

                                    var target = $('#dgAdd').datagrid('getEditor', { 'index': NeweditIndex, 'field': 'SID' }).target;
                                    target.combobox('clear'); //清除原来的数据
                                    var url = 'financeApi.aspx?method=GetSIDByFID&id=' + value;
                                    target.combobox('reload', url); //联动下拉列表重载  
                                }
                            }
                        }
                    },
                    {
                        title: '二级科目', field: 'SID', width: '120px', formatter: function (value, row) {
                            return row.SName;
                        },
                        editor: {
                            type: 'combobox',
                            options: { valueField: 'SID', textField: 'SName', required: true, editable: false, url: 'financeApi.aspx?method=GetSecondSubject', panelHeight: '150px' }
                        }
                    },
                    { title: '发生日期', field: 'HappenDate', width: '100px', editor: { type: 'datebox', options: { required: true } }, formatter: DateFormatter },
                    { title: '说明', field: 'Memo', width: '120px', editor: 'textbox' },
                    { title: '对应单号(多个单号用,逗号隔开)', field: 'Summary', width: '180px', editor: 'textbox' },
                    { title: '费用', field: 'DetailCharge', width: '80px', editor: { type: 'numberbox', options: { precision: 2, required: true } } },
                    {
                        field: 'opt', title: '操作', width: '80px', align: 'left',
                        formatter: function (value, rec, index) { var btn = '<a class="delcls" onclick="delExByID(\'' + index + '\')" href="javascript:void(0)">删除</a>'; return btn; }
                    }
                ]],
                onClickRow: function (index, data) {
                    if (OldeditIndex != index) {
                        NeweditIndex = index;
                        if (endEditing()) {
                            $('#dgAdd').datagrid('selectRow', index).datagrid('beginEdit', index);
                            var row = $("#dgAdd").datagrid('getData').rows[index];
                            if (row.ZID != undefined) {
                                var target = $('#dgAdd').datagrid('getEditor', { 'index': index, 'field': 'FID' }).target;
                                target.combobox('clear'); //清除原来的数据
                                var url = 'financeApi.aspx?method=GetFIDByZID&id=' + row.ZID;
                                target.combobox('reload', url); //联动下拉列表重载  
                                target.combobox('setValue', row.FID)
                            }
                            if (row.ZID != undefined) {
                                var setarget = $('#dgAdd').datagrid('getEditor', { 'index': index, 'field': 'SID' }).target;
                                setarget.combobox('clear'); //清除原来的数据
                                var url = 'financeApi.aspx?method=GetSIDByFID&id=' + row.FID;
                                setarget.combobox('reload', url); //联动下拉列表重载  
                                setarget.combobox('setValue', row.SID)
                            }
                        } else {
                            if ($('#dgAdd').datagrid('validateRow', OldeditIndex)) {
                                var editors = $('#dgAdd').datagrid('getEditors', OldeditIndex);
                                if (editors.length > 0) {
                                    var zsn = editors[0];
                                    var fsn = editors[1];
                                    var ssn = editors[2];
                                    var ZName = $(zsn.target).combobox('getText');
                                    var FName = $(fsn.target).combobox('getText');
                                    var SName = $(ssn.target).combobox('getText');

                                    $('#dgAdd').datagrid('getRows')[OldeditIndex]['ZName'] = ZName;
                                    $('#dgAdd').datagrid('getRows')[OldeditIndex]['FName'] = FName;
                                    $('#dgAdd').datagrid('getRows')[OldeditIndex]['SName'] = SName;
                                    $('#dgAdd').datagrid('endEdit', OldeditIndex);
                                }
                            }
                            $('#dgAdd').datagrid('cancelEdit', OldeditIndex);
                        }
                        OldeditIndex = index;
                    }
                },
                onLoadSuccess: function (data) { $('.delcls').linkbutton({ text: '删除', plain: true, iconCls: 'icon-cut' }); },
                onClickCell: function (Index, field, value) {
                    $('#dgAdd').datagrid('selectRow', Index);
                    $('#dgAdd').datagrid('beginEdit', Index);
                },
            });
            //审批流程查询
            $('#ApproveID').combobox({
                url: 'financeApi.aspx?method=QueryApproveSet&ApproveType=7&DelFlag=0',
                valueField: 'ID', textField: 'ApproveName',
                onSelect: function (rec) {
                    var info;
                    if (rec.OneCheckID != undefined && rec.OneCheckID != "") { info = "一级审批：" + rec.OneCheckName; }
                    if (rec.TwoCheckID != undefined && rec.TwoCheckID != "") { info += "<br />二级审批：" + rec.TwoCheckName; }
                    if (rec.ThreeCheckID != undefined && rec.ThreeCheckID != "") { info += "<br />三级审批：" + rec.ThreeCheckName; }
                    if (rec.FourCheckID != undefined && rec.FourCheckID != "") { info += "<br />四级审批：" + rec.FourCheckName; }
                    if (rec.FiveCheckID != undefined && rec.FiveCheckID != "") { info += "<br />五级审批：" + rec.FiveCheckName; }
                    if (rec.SixCheckID != undefined && rec.SixCheckID != "") { info += "<br />六级审批：" + rec.SixCheckName; }
                    //$('#ApproveInfo').html("一级审批：" + rec.SupervisorName + "<br />二级审批：" + rec.FinanceName + "<br />三级审批：" + rec.LeaderName);
                    $('#ApproveInfo').html(info);
                    $('#NextCheckID').val(rec.OneCheckID);//下一审批人
                    $('#NextCheckName').val(rec.OneCheckName);
                }
            });

            //客户名称查询
            $('#ClientID').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                valueField: 'ClientID', textField: 'ClientName',
                onSelect: function (rec) {
                    $('#AClientID').val(rec.ClientID);//受款人
                    $('#AReceiveName').textbox('setValue', rec.Boss);//受款人
                    $('#AReceiveNumber').textbox('setValue', rec.CardNum);//受款账号
                }
            });
            //付款单位
            // 1. 初始化付款单位下拉框（核心配置）
            $('#ChargeUnit').combobox({
                required: true,
                editable: false, // 禁止手动输入，仅下拉选择
                valueField: 'id',
                textField: 'name',
                // 付款单位数据
                data: [
                    { id: '广东雷腾智能光电有限公司增城分公司', name: '广东雷腾智能光电有限公司增城分公司' },
                    { id: '广州市一二三汽车用品有限公司', name: '广州市一二三汽车用品有限公司' },
                    { id: '广州市好来运新零售投资控股有限公司', name: '广州市好来运新零售投资控股有限公司' },
                    { id: '广州市狄祺达贸易有限公司', name: '广州市狄祺达贸易有限公司' },
                    { id: '广州狄乐汽车服务有限公司', name: '广州狄乐汽车服务有限公司' },
                    { id: '广州市一二三轮胎销售有限公司', name: '广州市一二三轮胎销售有限公司' },
                    { id: '湖南省狄乐汽车服务有限公司', name: '湖南省狄乐汽车服务有限公司' },
                    { id: '湖北省狄乐汽车服务有限公司', name: '湖北省狄乐汽车服务有限公司' },
                    { id: '南宁粤信旺宁贸易有限公司', name: '南宁粤信旺宁贸易有限公司' },
                    { id: '广通慧采汽配供应链（广州）有限公司', name: '广通慧采汽配供应链（广州）有限公司' },
                    { id: '广州狄安祺达轮胎有限公司', name: '广州狄安祺达轮胎有限公司' },
                    { id: '新疆正诚昌荣汽车服务有限公司', name: '新疆正诚昌荣汽车服务有限公司' },
                    { id: '乌鲁木齐广通实业发展有限公司', name: '乌鲁木齐广通实业发展有限公司' },
                    { id: '广州富添盛汽车用品有限公司', name: '广州富添盛汽车用品有限公司' },
                    { id: '震亚（广州）汽车用品销售有限公司', name: '震亚（广州）汽车用品销售有限公司' },
                ],
                // 选中变化时的简单回调（可选）
                onChange: function (newId) {
                   // console.log('选中单位：', $(this).combobox('getText'));
                }
            });
            var datenow = new Date();
            //$('#AExpenseDate').datetimebox('setValue', AllDateTime(datenow));
            $('#AExpenseDate').val(AllDateTime(datenow));
            $('#OperaName').val('<%=Un %>')
        }

        NeweditIndex = undefined;
        OldeditIndex = undefined;
        function endEditing() {
            if (OldeditIndex != undefined) {
                if ($('#dgAdd').datagrid('validateRow', OldeditIndex)) {
                    var editors = $('#dgAdd').datagrid('getEditors', OldeditIndex);
                    if (editors.length > 0) {
                        var zsn = editors[0];
                        var fsn = editors[1];
                        var ssn = editors[2];
                        var ZName = $(zsn.target).combobox('getText');
                        var FName = $(fsn.target).combobox('getText');
                        var SName = $(ssn.target).combobox('getText');

                        $('#dgAdd').datagrid('getRows')[OldeditIndex]['ZName'] = ZName;
                        $('#dgAdd').datagrid('getRows')[OldeditIndex]['FName'] = FName;
                        $('#dgAdd').datagrid('getRows')[OldeditIndex]['SName'] = SName;
                    }
                    $('#dgAdd').datagrid('endEdit', OldeditIndex);
                } else {
                    $('#dgAdd').datagrid('cancelEdit', OldeditIndex);
                }
                return true;
            } else {
                return false;
            }
        }
        //新增品名数据
        function newRow() {
            var row = {};
            //1 先取消所有的选中状态
            $('#dgAdd').datagrid('unselectAll');
            //2追加一行
            $('#dgAdd').datagrid('appendRow', row);
            //3获取当前页的行号
            //editIndex = $('#dgAdd').datagrid('getRows').length - 1;
            //4选中并开启编辑状态
            //$('#dgAdd').datagrid('selectRow', editIndex);
            //$('#dgAdd').datagrid('beginEdit', editIndex);
        }
        //新增
        function addItem() {
            $('#ApproveInfo').html("");
            $('#save').linkbutton('enable');
            $('#IsSave').val("0");
            $('#dlgAwbDetail').dialog('open').dialog('setTitle', '新增报销');
            $('#fm').form('clear');
            ShowDG();
            $('#ApproveID').combobox('clear');
            $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });

            $("#AReceiveNumber").combobox('options').required = false;
            $("#AReceiveNumber").combobox('textbox').validatebox('options').required = false;
            $("#AReceiveNumber").combobox('validate');

            $("#ACardBank").textbox('options').required = false;
            $("#ACardBank").textbox('textbox').validatebox('options').required = false;
            $("#ACardBank").textbox('validate');

            //受款人
            $('#AReceiveName').combobox({
                valueField: 'ReceiveName', textField: 'ReceiveName',
                url: '../Finance/financeApi.aspx?method=QueryAllReceiveName',
                onSelect: onReceiveNameSelect,
                required: true
            });
            //报销部门
            $('#AExDepart').combobox({
                valueField: 'Name', textField: 'Name',
                url: '../Finance/financeApi.aspx?method=QuerySystemOrganize',
                onSelect: onReceiveNameSelect,
                required: true
            });
            if ("<%=UserInfor.HouseID%>" == "64") {
                $('#ApproveID').combobox('setValue', '80')
                $('#AExDepart').combobox('setValue', '广通慧采')
            };

        }
        function onReceiveNameSelect(item) {
            $('#AReceiveNumber').combobox({
                valueField: 'ExID', textField: 'ReceiveNumber',
                url: '../Finance/financeApi.aspx?method=QueryAllReceiveNumber&ReceiveName=' + $('#AReceiveName').combobox('getValue'),
                //required: false
                onSelect: function (rec) {
                    $("#HiddenReceiveNumber").val(rec.ReceiveNumber)
                    $('#ACardBank').textbox('setValue', rec.CardBank);
                }
            });
        }
        //修改
        function editItem() {
            $('#IsSave').val("1");
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return; }
            if (row) {
                if (row.CheckStatus == "1") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '已结算的单不能修改！', 'warning'); return; }
                $('#dlgAwbDetail').dialog('open').dialog('setTitle', '修改报销');
                ShowDG();
                $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
                $('#fm').form('clear');
                $("#HiddenReceiveName").val(row.ReceiveName);
                $("#HiddenReceiveNumber").val(row.ReceiveNumber);
                $.ajax({
                    url: "../Finance/financeApi.aspx?method=GetExpenseById&id=" + row.ExID,
                    cache: false,
                    success: function (text) {
                        var o = eval('(' + text + ')');
                        o.ExpenseDate = AllDateTime(o.ExpenseDate);
                        $('#fm').form('load', o);
                        if (o.OneCheckID != undefined && o.OneCheckID != "") { info = "一级审批：" + o.OneCheckName; }
                        if (o.TwoCheckID != undefined && o.TwoCheckID != "") { info += "<br />二级审批：" + o.TwoCheckName; }
                        if (o.ThreeCheckID != undefined && o.ThreeCheckID != "") { info += "<br />三级审批：" + o.ThreeCheckName; }
                        if (o.FourCheckID != undefined && o.FourCheckID != "") { info += "<br />四级审批：" + o.FourCheckName; }
                        if (o.FiveCheckID != undefined && o.FiveCheckID != "") { info += "<br />五级审批：" + o.FiveCheckName; }
                        if (o.SixCheckID != undefined && o.SixCheckID != "") { info += "<br />六级审批：" + o.SixCheckName; }
                        //$('#ApproveInfo').html("一级审批：" + rec.SupervisorName + "<br />二级审批：" + rec.FinanceName + "<br />三级审批：" + rec.LeaderName);
                        $('#ApproveInfo').html(info);

                        //$('#ApproveInfo').html("主管审批：" + o.SupervisorName + "<br />财务审批：" + o.FinanceName + "<br />领导审批：" + o.LeaderName);
                        for (var i = 0; i < o.exDetail.length; i++) {
                            var rows = { HappenDate: o.exDetail[i].HappenDate, Memo: o.exDetail[i].Memo, Summary: o.exDetail[i].Summary, DetailCharge: o.exDetail[i].DetailCharge, DetailID: o.exDetail[i].DetailID, ZID: o.exDetail[i].ZID, ZName: o.exDetail[i].ZName, FName: o.exDetail[i].FName, FID: o.exDetail[i].FID, SID: o.exDetail[i].SID, SName: o.exDetail[i].SName };
                            $('#dgAdd').datagrid('appendRow', rows);
                        }
                        if (o.Status == "0" || o.Status == "2") { $('#save').linkbutton('enable'); } else { $('#save').linkbutton('disable'); }
                    }
                });
                //受款人
                $('#AReceiveName').combobox({
                    valueField: 'ReceiveName', textField: 'ReceiveName',
                    url: '../Finance/financeApi.aspx?method=QueryAllReceiveName',
                    onSelect: onReceiveNameSelect,
                    required: false
                });
                //报销部门
                $('#AExDepart').combobox({
                    valueField: 'Name', textField: 'Name',
                    url: '../Finance/financeApi.aspx?method=QuerySystemOrganize',
                    onSelect: onReceiveNameSelect,
                    required: true
                });
            }
        }
        //修改
        function editItemByID(Did) {
            $('#IsSave').val("1");
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                if (row.CheckStatus == "1") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '已结算的单不能修改！', 'warning'); return; }
                $('#dlgAwbDetail').dialog('open').dialog('setTitle', '修改报销');
                ShowDG();
                $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
                $('#fm').form('clear');
                $("#HiddenReceiveName").val(row.ReceiveName);
                $("#HiddenReceiveNumber").val(row.ReceiveNumber);
                $.ajax({
                    url: "../Finance/financeApi.aspx?method=GetExpenseById&id=" + row.ExID,
                    cache: false,
                    success: function (text) {
                        var o = eval('(' + text + ')');
                        o.ExpenseDate = AllDateTime(o.ExpenseDate);
                        $('#fm').form('load', o);
                        if (o.OneCheckID != undefined && o.OneCheckID != "") { info = "一级审批：" + o.OneCheckName; }
                        if (o.TwoCheckID != undefined && o.TwoCheckID != "") { info += "<br />二级审批：" + o.TwoCheckName; }
                        if (o.ThreeCheckID != undefined && o.ThreeCheckID != "") { info += "<br />三级审批：" + o.ThreeCheckName; }
                        if (o.FourCheckID != undefined && o.FourCheckID != "") { info += "<br />四级审批：" + o.FourCheckName; }
                        if (o.FiveCheckID != undefined && o.FiveCheckID != "") { info += "<br />五级审批：" + o.FiveCheckName; }
                        if (o.SixCheckID != undefined && o.SixCheckID != "") { info += "<br />六级审批：" + o.SixCheckName; }
                        //$('#ApproveInfo').html("一级审批：" + rec.SupervisorName + "<br />二级审批：" + rec.FinanceName + "<br />三级审批：" + rec.LeaderName);
                        $('#ApproveInfo').html(info);
                        for (var i = 0; i < o.exDetail.length; i++) {
                            var rows = { HappenDate: o.exDetail[i].HappenDate, Memo: o.exDetail[i].Memo, Summary: o.exDetail[i].Summary, DetailCharge: o.exDetail[i].DetailCharge, DetailID: o.exDetail[i].DetailID, ZID: o.exDetail[i].ZID, ZName: o.exDetail[i].ZName, FName: o.exDetail[i].FName, FID: o.exDetail[i].FID, SID: o.exDetail[i].SID, SName: o.exDetail[i].SName };
                            $('#dgAdd').datagrid('appendRow', rows);
                        }
                        if (o.Status == "0" || o.Status == "2") { $('#save').linkbutton('enable'); } else { $('#save').linkbutton('disable'); }
                    }
                });
                //受款人
                $('#AReceiveName').combobox({
                    valueField: 'ReceiveName', textField: 'ReceiveName',
                    url: '../Finance/financeApi.aspx?method=QueryAllReceiveName',
                    onSelect: onReceiveNameSelect,
                    required: false
                });
                $('#AReceiveNumber').combobox({
                    valueField: 'ReceiveNumber', textField: 'ReceiveNumber',
                    url: '../Finance/financeApi.aspx?method=QueryAllReceiveNumber&ReceiveName=' + row.ReceiveName
                    //required: false
                });
                //报销部门
                $('#AExDepart').combobox({
                    valueField: 'Name', textField: 'Name',
                    url: '../Finance/financeApi.aspx?method=QuerySystemOrganize',
                    onSelect: onReceiveNameSelect,
                    required: true
                });
            }
        }

        //删除品名数据
        function delExByID(Did) {
            var dr = $('#dgAdd').datagrid('getRows').length;
            if (dr == 1) {
                $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
                $('#AExCharge').numberbox('setValue', 0);
            } else {
                $('#dgAdd').datagrid('deleteRow', Did);
            }
        }
        //删除
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审批中和已结算的单不能删除，确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=DelCargoMyExpense&ty=1',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            if (text.Result == true) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info'); $('#dg').datagrid('reload'); }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //审批流程图
        function Proc() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查看的数据！', 'warning'); return; }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '查看报销单：' + row.ExID + "的审核流程图");
                $.ajax({
                    url: "../Finance/financeApi.aspx?method=QueryExpenseRout&id=" + row.ExID + "&ApproveType=0" + "&ApproveID=" + row.ApproveID + "&applyID=" + row.OperaID + "&ApplyName=" + row.OperaName + "&HouseID=" + row.HouseID,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblTrack");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#ExID').val();
            key[1] = $("#ExName").val();
            key[2] = $("#ExDepart").val();
            key[3] = $("#ExCharge").val();
            key[4] = $("#ReceiveName").val();
            key[5] = $("#ReceiveNumber").val();
            key[6] = $('#StartDate').datebox('getValue');
            key[7] = $('#EndDate').datebox('getValue');
            key[8] = $("#Status").combobox('getValue');
            $.ajax({
                url: "../Finance/financeApi.aspx?method=QueryOnlyMyExpenseForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //保存数据
        function saveData() {
<%--            if ($('#ApproveID').combobox('getValue') == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '必须选择审批流程!', 'info');
                $('#save').linkbutton('enable'); return;
            }--%>
            <%--            if ($('#AExName').textbox('getValue') == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '报销人不能为空!', 'info');
                $('#save').linkbutton('enable'); return;
            }--%>
            if (OldeditIndex != undefined) {
                if ($('#dgAdd').datagrid('validateRow', OldeditIndex)) {
                    var editors = $('#dgAdd').datagrid('getEditors', OldeditIndex);
                    if (editors.length > 0) {
                        var zsn = editors[0];
                        var fsn = editors[1];
                        var ssn = editors[2];
                        var ZName = $(zsn.target).combobox('getText');
                        var FName = $(fsn.target).combobox('getText');
                        var SName = $(ssn.target).combobox('getText');

                        $('#dgAdd').datagrid('getRows')[OldeditIndex]['ZName'] = ZName;
                        $('#dgAdd').datagrid('getRows')[OldeditIndex]['FName'] = FName;
                        $('#dgAdd').datagrid('getRows')[OldeditIndex]['SName'] = SName;
                    }
                    $('#dgAdd').datagrid('endEdit', OldeditIndex);
                } else {
                    $('#dgAdd').datagrid('endEdit', OldeditIndex);
                }
            }
            $('#dgAdd').datagrid('acceptChanges')
            $('#save').linkbutton('disable');
            var trd = $("#fm").form('enableValidation').form('validate');
            if (trd == false) { $('#save').linkbutton('enable'); return; }

            var val = $("input[name='ChargeType']:checked").val();
            if (val == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择付款方式!', 'info');
                $('#save').linkbutton('enable');
                return;
            }
            var row = $('#dgAdd').datagrid('getRows');
            if (row.length <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请完善详细报销列表信息!', 'info');
                $('#save').linkbutton('enable');
                return;
            }

            var json = JSON.stringify(row);
            var formjson = $("#fm").serialize();
            $.ajax({
                async: false,
                url: "../Finance/financeApi.aspx?method=saveExpense&" + formjson,
                type: "post", data: { submitData: json, clientName: $('#ClientID').textbox('getText'), chargeUnit: $('#ChargeUnit').combobox('getText') },
                success: function (msg) {
                    if (msg != "") {
                        var result = eval('(' + msg + ')');
                        if (result.Result) {
                            $('#AExID').val(result.Message);
                            $('#IsSave').val("1");
                            //$.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '操作成功!', 'info');

                            //$('#dg').datagrid('reload');
                            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '操作成功，是否打印报销单？', function (r) {
                                if (r) { prn1_preview(); }
                            });
                            $('#dlgAwbDetail').dialog('close');
                        } else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '操作失败：' + result.Message, 'warning');
                            //$('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
                        }
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败！', 'warning');
                    }
                    $('#save').linkbutton('enable');
                }
            });
        }
        var LODOP; //打印全局变量
        //打印
        function prn1_preview() {
            var iss = $('#IsSave').val();
            if (iss == "0") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先保存报销数据!', 'info'); return;
            }
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateOneFormPage();
            LODOP.PREVIEW();
            //LODOP.PRINT();
        };
        //针式打印机的打印样式
        function CreateOneFormPage() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));

            var hous = '<%= HouseName%>';
            var com = '<%= UserInfor.HouseName%>' + '费用报销单';
            if (hous.indexOf('湖北') != -1) { com = '湖北迪乐泰费用报销单'; }
            if (hous.indexOf('西安') != -1) { com = '西安新陆程费用报销单'; }
            if (hous.indexOf('海南') != -1) { com = '海南迪乐泰费用报销单'; }
            if (hous.indexOf('梅州') != -1) { com = '梅州新陆程费用报销单'; }
            if (hous.indexOf('广州') != -1) { com = '广州市狄祺达费用报销单'; }
            if (hous.indexOf('广东') != -1) { com = '广州市狄祺达费用报销单'; }
            if (hous.indexOf('揭阳') != -1) { com = '揭阳迪乐泰费用报销单'; }
            if (hous.indexOf('四川') != -1) { com = '四川迪乐泰费用报销单'; }
            if (hous.indexOf('重庆') != -1) { com = '重庆迪乐泰费用报销单'; }
            if (hous.indexOf('富添盛') != -1) { com = '广州富添盛费用报销单'; }
            if (hous.indexOf('好来运科技') != -1) { com = "广州好来运科技费用报销单" }
            if (hous.indexOf('海口城配') != -1) { com = "海南新路城配费用报销单" }
            if (hous.indexOf('一二三轮胎') != -1) { com = "一二三轮胎费用报销单" }

            LODOP.PRINT_INITA(10, 10, 750, 450, com);
            LODOP.SET_PRINT_STYLE("FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(8, 219, 285, 30, com);
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 16);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(35, 8, 90, 20, "报销人：");
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(35, 81, 277, 20, $('#AExName').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(54, 8, 90, 25, "受款人：");
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(35, 577, 94, 30, "报销单号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(35, 648, 100, 30, $('#AExID').val()); //报销单号mini.getbyName("ExID").getValue()
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(71, 8, 95, 25, "受款账号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            //LODOP.ADD_PRINT_TEXT(62, 391, 85, 25, "报销日期：");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            //LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(62, 577, 167, 25, "单据及附件共     页");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            //LODOP.ADD_PRINT_TEXT(61, 81, 120, 25, $('#AReceiveName').textbox('getValue')); //受款人
            LODOP.ADD_PRINT_TEXT(53, 81, 490, 25, $('#AReceiveName').combobox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            //LODOP.ADD_PRINT_TEXT(61, 283, 205, 25, $('#AReceiveNumber').textbox('getValue')); //受款账号
            LODOP.ADD_PRINT_TEXT(70, 81, 477, 25, $("#ACardBank").textbox('getValue') + "  " + $('#AReceiveNumber').combobox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            //LODOP.ADD_PRINT_TEXT(59, 472, 100, 25, getNowFormatDate(getDate($('#AExpenseDate').datetimebox('getValue'))));
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            //LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(35, 262, 95, 25, "报销日期：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            //LODOP.ADD_PRINT_TEXT(35, 347, 105, 25, getNowFormatDate(getDate($('#AExpenseDate').datetimebox('getValue')))); //报销时间
            LODOP.ADD_PRINT_TEXT(35, 347, 180, 25, $('#AExpenseDate').val()); //报销时间
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_SHAPE(2, 87, 8, 35, 25, 0, 1, "#000000");
            LODOP.ADD_PRINT_SHAPE(2, 87, 42, 80, 25, 0, 1, "#000000");
            LODOP.ADD_PRINT_SHAPE(2, 87, 121, 90, 25, 0, 1, "#000000");
            LODOP.ADD_PRINT_SHAPE(2, 87, 210, 262, 25, 0, 1, "#000000");
            LODOP.ADD_PRINT_SHAPE(2, 87, 471, 167, 25, 0, 1, "#000000");
            LODOP.ADD_PRINT_SHAPE(2, 87, 637, 100, 25, 0, 1, "#000000");
            LODOP.ADD_PRINT_TEXT(91, 2, 50, 20, "序号");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
            LODOP.ADD_PRINT_TEXT(91, 45, 79, 20, "报销类别");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
            LODOP.ADD_PRINT_TEXT(91, 126, 85, 20, "发生日期");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
            LODOP.ADD_PRINT_TEXT(91, 295, 100, 20, "用 途 说 明");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
            LODOP.ADD_PRINT_TEXT(91, 486, 100, 20, "对应单号");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
            LODOP.ADD_PRINT_TEXT(91, 636, 100, 20, "报销金额(元)");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);

            var griddata = $('#dgAdd').datagrid('getRows');
            var js = 0, hg = 0, wh = 0;
            for (var i = 0; i < griddata.length; i++) {
                LODOP.ADD_PRINT_SHAPE(2, 111 + i * 40 - i, 8, 35, 40, 0, 1, "#000000");
                LODOP.ADD_PRINT_TEXT(122 + i * 40, 10, 33, 20, i + 1);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                LODOP.ADD_PRINT_SHAPE(2, 111 + i * 40 - i, 42, 80, 40, 0, 1, "#000000");
                //var sname = "";
                //$.ajax({
                //    url: '../Finance/financeApi.aspx?method=GetSIDNameBySID&fid=' + griddata[i].FID + '&sid=' + griddata[i].SID,
                //    type: 'post', dataType: 'json', async: false,
                //    success: function (text) {
                //        if (text.Result == true) { sname = text.Message; }
                //    }
                //});
                LODOP.ADD_PRINT_TEXT(122 + i * 40, 42, 90, 25, griddata[i].SName);
                //LODOP.ADD_PRINT_TEXT(122 + i * 40, 42, 90, 25, sname);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                LODOP.SET_PRINT_STYLEA(0, "Alignment", 1);
                LODOP.ADD_PRINT_SHAPE(2, 111 + i * 40 - i, 121, 90, 40, 0, 1, "#000000");
                LODOP.ADD_PRINT_TEXT(122 + i * 40, 121, 100, 25, getNowFormatDate(griddata[i].HappenDate));
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                LODOP.ADD_PRINT_SHAPE(2, 111 + i * 40 - i, 210, 262, 40, 0, 1, "#000000");
                LODOP.ADD_PRINT_TEXT(118 + i * 40, 212, 273, 25, griddata[i].Memo);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                LODOP.ADD_PRINT_SHAPE(2, 111 + i * 40 - i, 471, 167, 40, 0, 1, "#000000");
                LODOP.ADD_PRINT_TEXT(122 + i * 40, 471, 167, 25, griddata[i].Summary);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                LODOP.ADD_PRINT_SHAPE(2, 111 + i * 40 - i, 637, 100, 40, 0, 1, "#000000");
                LODOP.ADD_PRINT_TEXT(122 + i * 40, 637, 100, 25, griddata[i].DetailCharge);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                js += Number(griddata[i].DetailCharge);
                hg = 111 + (i + 1) * 40 - i - 1;
                if (i == griddata.length - 1) {
                    LODOP.ADD_PRINT_SHAPE(2, hg, 8, 630, 30, 0, 1, "#000000");
                    LODOP.ADD_PRINT_SHAPE(2, hg, 637, 100, 30, 0, 1, "#000000");
                    LODOP.ADD_PRINT_TEXT(hg + 10, 232, 100, 25, "合计：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(hg + 10, 639, 100, 25, Number(js).toFixed(2));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                    LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);

                    LODOP.ADD_PRINT_SHAPE(2, hg + 29, 8, 729, 30, 0, 1, "#000000");
                    LODOP.ADD_PRINT_TEXT(hg + 35, 77, 100, 25, "金额大写：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                    LODOP.ADD_PRINT_TEXT(hg + 35, 168, 600, 25, atoc(js));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                    LODOP.ADD_PRINT_SHAPE(2, hg + 58, 8, 60, 40, 0, 1, "#000000");
                    LODOP.ADD_PRINT_TEXT(hg + 68, 12, 70, 25, "领导审批");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                    LODOP.ADD_PRINT_SHAPE(2, hg + 58, 67, 120, 40, 0, 1, "#000000");
                    LODOP.ADD_PRINT_SHAPE(2, hg + 58, 186, 40, 40, 0, 1, "#000000");
                    LODOP.ADD_PRINT_TEXT(hg + 68, 190, 40, 25, "备注");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
                    LODOP.ADD_PRINT_SHAPE(2, hg + 58, 225, 512, 40, 0, 1, "#000000");
                    LODOP.ADD_PRINT_TEXT(hg + 65, 230, 515, 40, $('#AReason').val());
                    wh = hg + 110;
                }
            }
            if (wh == 0) {
                wh = 120;
            }
            LODOP.ADD_PRINT_TEXT(wh, 8, 90, 25, "复核：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(wh, 177, 54, 25, "会计：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(wh, 316, 54, 25, "出纳：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(wh, 452, 70, 25, "操作员：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(wh, 523, 90, 25, $('#OperaName').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            LODOP.ADD_PRINT_TEXT(wh, 596, 70, 25, "领款人：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#000000");
            //LODOP.PRINT_DESIGN();
        }
    </script>

</asp:Content>
