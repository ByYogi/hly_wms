<%@ Page Title="工厂进货单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FactoryPurchaseOrderManager.aspx.cs" Inherits="Cargo.Order.FactoryPurchaseOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .commTblStyle_8 th {
            border: 1px solid rgb(205, 205, 205);
            text-align: center;
            color: rgb(255, 255, 255);
            line-height: 28px;
            background-color: rgb(15, 114, 171);
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
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({
                title: '件数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单费用', field: 'TotalCharge', width: '75px', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单备注', field: 'Remark', width: '200px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '申请状态', field: 'CheckStatus', width: '80px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未审批'>未审批</span>"; }
                    else if (val == "1") { return "<span title='已审批'>已审批</span>"; }
                    else if (val == "2") { return "<span title='审批中'>审批中</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '审批状态', field: 'ApplyStatus', width: '80px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='提交申请'>提交申请</span>"; }
                    else if (val == "1") { return "<span title='审批通过'>审批通过</span>"; }
                    else if (val == "2") { return "<span title='审批拒绝'>审批拒绝</span>"; }
                    else if (val == "3") { return "<span title='审批结束'>审批结束</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '审批意见', field: 'CheckResult', width: '200px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '下一审批人', field: 'NextCheckName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });

            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#dgtoolbar',
                frozenColumns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '35px' },
                    {
                        title: '订单号', field: 'OrderNo', width: '115px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                columns: [columns],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                    if (row.CheckStatus != "0") {
                        $('#btnApprove').show();
                    } else {
                        $('#btnApprove').hide();
                    }
                    if (row.ApplyStatus == "3") {
                        $('#update').show();
                    } else {
                        $('#update').hide();
                    }
                    if (row.CheckStatus == "0" || row.CheckStatus == "1" && row.ApplyStatus == "2") {
                        $('#delete').show();
                    } else {
                        $('#delete').hide();
                    }
                },
                rowStyler: function (index, row) {
                    if (row.ApplyStatus == "2") { return "background-color:#F5B7B1"; };
                    if (row.CheckStatus == "0") { return "background-color:#FCF3CF"; };
                    if (row.CheckStatus == "1" && row.ApplyStatus == "3") { return "background-color:#A9DFBF"; };
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryFactoryPurchaseOrderListInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                CheckStatus: $("#ACheckStatus").combobox('getValue'),
                EndDate: $('#EndDate').datebox('getValue')
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px" />
                </td>
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AAwbStatus" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">已下单</option>
                        <option value="1">出库中</option>
                        <option value="2">已出库</option>
                        <option value="5">已签收</option>
                    </select>
                </td>
                <td style="text-align: right;">结算状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="ACheckStatus" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结算</option>
                        <option value="2">未结清</option>
                    </select>
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" id="delete" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" id="update" plain="false" onclick="UpdateFacOrderNo()">&nbsp;更新订货单号&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnApprove" class="easyui-linkbutton" iconcls="icon-sitemap_color" plain="false" onclick="QuerydlgApproval()">&nbsp;审批流程图&nbsp;</a>&nbsp;&nbsp;
        <%--<a href="#" id="btnExportOrder" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BatchExportOrderInfo()">&nbsp;导出订单列表&nbsp;</a>&nbsp;&nbsp;--%>
        <form runat="server" id="fm1">
            <%--            <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />--%>
            <asp:Button ID="btnOrderGoods" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderGoods_Click" />
        </form>
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 75%; height: 600px;" closed="true" closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <div id="saPanel">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">总件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 70px;" readonly="true" />
                        </td>
                        <td class="TotalCharge" style="text-align: right;">费用合计:
                        </td>
                        <td class="TotalCharge">
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2" readonly="true" style="width: 80px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 95%; resize: none"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table>
            <tr>
                <td>
                    <input type="hidden" id="dgSaveAwbStatus" />
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
                <td class="outDg" id="outTd">
                    <table id="outDg" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
        <div id="toolbar">
            <table>
                <tr>
                    <td style="text-align: right;">品牌:</td>
                    <td>
                        <input id="ASID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName',required:true" />
                    </td>
                    <td class="AASpecs" id="AASpecs" style="text-align: right;">规格:</td>
                    <td class="AASpecs">
                        <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 80px">
                    </td>
                    <td class="AFigure" style="text-align: right;">花纹:</td>
                    <td class="AFigure">
                        <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px" />
                    </td>
                    <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="queryInCargoProduct()">查询</a></td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveOrderUpdate()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" id="btnExportOrderGoods" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportOrderGoods()">&nbsp;导出订单明细&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" id="SubmitReview" class="easyui-linkbutton" iconcls="icon-table_sort" plain="false" onclick="SubmitReview()">&nbsp;提交审核&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>

    <!--Begin 出库操作-->

    <div id="dlgOutCargo" class="easyui-dialog" style="width: 350px; height: 350px; padding: 5px 5px" closed="true" buttons="#dlgOutCargo-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">工厂进货价：</td>
                    <td>
                        <input name="UnitPrice" id="UnitPrice" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">是否含税：</td>
                    <td>
                        <select class="easyui-combobox" id="WhetherTax" name="WhetherTax" style="width: 200px;" panelheight="auto" editable="false">
                            <option value="1">含税</option>
                            <option value="0">不含税</option>
                        </select>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->
    
    <div id="dlgUpdateFacOrderNo" class="easyui-dialog" style="width: 300px; height: 150px;" closed="true" closable="false" buttons="#dlgUpdateFacOrderNo-buttons">
        <form id="fmFacOrderNo" method="post">
            <div id="saPanel">
                <input type="hidden" id="faOrderID" name="faOrderID" />
                <input type="hidden" id="faOrderNo" name="faOrderNo" />
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">订货单号:
                        </td>
                        <td>
                        <input id="FacOrderNo" name="FacOrderNo" class="easyui-textbox" data-options="prompt:'请输入订货单号'" style="width: 180px"/>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlgUpdateFacOrderNo-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveFacOrderNo()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <!--订单审批流程图-->
    <div id="dlgApproval" class="easyui-dialog" style="width: 800px; height: 470px; padding: 1px 1px"
        closed="true" closable="false" buttons="#dlgApproval-buttons">
        <div id="lblApproval">
        </div>
    </div>
    <div id="dlgApproval-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="javascript:$('#dlgApproval').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--订单审批流程图-->
    <script type="text/javascript">
        //查看审批流程
        function QuerydlgApproval() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查看的数据！', 'info');
                return;
            }
            if (row) {
                $('#dlgApproval').dialog('open').dialog('setTitle', '查看订单：' + row.OrderNo + ' 的审核流程图');
                $.ajax({
                    url: "orderApi.aspx?method=QueryFactoryPurchaseExpenseRout&OrderID=" + row.OrderID + "&ApplyID=" + row.ApplyID + "&ApplyName=" + row.ApplyName + "&HouseID=" + row.HouseID,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblApproval");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        function UpdateFacOrderNo() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0].CheckStatus == "1" && rows[0].ApplyStatus == "3") {
                $("#faOrderID").val(rows[0].OrderID);
                $("#faOrderNo").val(rows[0].OrderNo);
                $('#FacOrderNo').textbox('setValue', '');
                $('#dlgUpdateFacOrderNo').dialog('open').dialog('setTitle', '修改订单：' + rows[0].OrderNo);
            } else {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审批未完成无法操作！', 'warning'); return;
            }
        }
        function SaveFacOrderNo() { 
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $('#fmFacOrderNo').form('submit', {
                        url: 'orderApi.aspx?method=UpdateFactoryPurchaseFacOrderNo',
                        success: function (msgg) {
                            $.messager.progress("close");
                            var result = eval('(' + msgg + ')');
                            if (result.Result) {
                                closeDlg();
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        },
                        error: function (res) {
                            debugger;
                        }
                    })
                }
            });
        }
        //提交审批
        function SubmitReview() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要提交的数据', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定提交审批？', function (r) {
                if (r) {
                    var json = JSON.stringify(row);
                    $.messager.progress({ msg: '请稍后,正在提交中...' });

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=SubmitFactoryPurchaseOrderReview',
                        onSubmit: function (param) {
                            param.submitData = json;
                        },
                        success: function (msgg) {
                            $.messager.progress("close");
                            var result = eval('(' + msgg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '提交成功!', 'info');
                                closeDlg()
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '提交失败：' + result.Message, 'warning');
                            }
                        },
                        error: function (res) {
                            debugger;
                        }
                    })
                }
            });
        }
        //批量导出订单信息
        function BatchExportOrderInfo() {
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "orderApi.aspx?method=QueryFactoryPurchaseOrderListInfoExport&OrderNo=" + $('#AOrderNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&AcceptUnit=" + $('#AcceptUnit').val(),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { <%--var obj = document.getElementById("<%=btnOrderInfo.ClientID %>"); obj.click()--%> }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //导出订单明细信息
        function ExportOrderGoods() {
            var row = $("#dgSave").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var obj = document.getElementById("<%=btnOrderGoods.ClientID %>"); obj.click();
        }
        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=DelFactoryPurchaseOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存订单
        function SaveOrderUpdate() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var json = JSON.stringify(row);
                    $.messager.progress({ msg: '请稍后,正在保存中...' });

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=UpdateFactoryPurchaseOrderInfo',
                        onSubmit: function (param) {
                            param.submitData = json;
                        },
                        success: function (msgg) {
                            $.messager.progress("close");
                            var result = eval('(' + msgg + ')');
                            if (result.Result) {
                                closeDlg();
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        },
                        error: function (res) {
                            debugger;
                        }
                    })
                }
            });
        }
        //新增出库数据
        function outOK() {
            var row = $('#outDg').datagrid('getSelected');
            var Total = Number(row.Piece);
            var UnitPrice = $('#UnitPrice').numberbox('getValue');
            var dgS = $('#dgSave').datagrid('getRows');
            for (var i = 0; i < dgS.length; i++) {
                if (dgS[i].Specs == row.Specs && dgS[i].Figure == row.Figure && dgS[i].GoodsCode == row.GoodsCode && dgS[i].LoadIndex == row.LoadIndex && dgS[i].SpeedLevel == row.SpeedLevel) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该产品已在订单上，请直接修改数量操作！', 'warning');
                    return;
                }
            }
            var tCharge = Number($('#TotalCharge').numberbox('getValue'));
            var Aindex = $('#InIndex').val();
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (indexD < 0) {
                row.Piece = Number($('#Numbers').numberbox('getValue'));
                row.UnitPrice = UnitPrice;
                row.OrderNo = $('#OrderNo').val();
                row.WhetherTax = $('#WhetherTax').numberbox('getValue');
                var json = JSON.stringify([row])
                //$('#dgSave').datagrid('appendRow', row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = UnitPrice * Number($('#Numbers').numberbox('getValue'));
                $('#TotalCharge').numberbox('setValue', tCharge + NC);
                if (Total != Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#outDg').datagrid('updateRow', { index: Aindex, row: row });


                $.messager.progress({ msg: '请稍后,拉上订单中...' });
                $.ajax({
                    url: 'orderApi.aspx?method=AddFactoryPurchaseOrderGoods',
                    type: 'post', dataType: 'json', data: { data: json },
                    success: function (text) {
                        $.messager.progress("close");
                        if (text.Result == true) {
                            //刷新列表
                            $('#dgSave').datagrid('clearSelections');
                            var gridOpts = $('#dgSave').datagrid('options');
                            gridOpts.url = 'orderApi.aspx?method=QueryFactoryPurchaseOrderGoodsInfo&OrderID=' + $('#OrderID').val();
                            $('#dgSave').datagrid('load', {
                                OrderNo: $('#OrderNo').val()
                            });
                            $('#dlgOutCargo').dialog('close');
                        }
                        else {
                            $('#APiece').numberbox('setValue', Number(p));
                            $('#TotalCharge').numberbox('setValue', tCharge);
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                        }
                    }
                });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        ///拉上订单
        function plup() {
            var row = $('#outDg').datagrid('getSelected');
            if (CheckStatus != "0" && ApplyStatus!="2") { return; }
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (Number(row.TypeParentID) == 1 && Number(row.SalePrice) <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先录入销售价格', 'warning');
                return;
            }
            if (row) {
                $('#dlgOutCargo').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#outDg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#UnitPrice').numberbox('setValue', row.UnitPrice);

                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();
            }
        }
        //查询在库产品
        function queryInCargoProduct() {
            if ($("#ASID").combobox('getValue') == undefined || $("#ASID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择品牌！', 'warning');
                return;
            }
            $('#outDg').datagrid('clearSelections');
            var gridOpts = $('#outDg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryALLProductData';
            $('#outDg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                SID: $("#ASID").combobox('getValue')
            });
        }
        //双击显示订单详细界面 
        function editItemByID(Did) {
            //一级产品
            $('#ASID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#outDg').datagrid('loadData', { total: 0, rows: [] });
            editIndex = undefined;
            var row = $("#dg").datagrid('getData').rows[Did];
            CheckStatus = row.CheckStatus;
            ApplyStatus = row.ApplyStatus;
            rowHouseID = row.HouseID;
            if (row) {
                $('#dgSaveAwbStatus').val(row.AwbStatus);
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo);
                $('#fmDep').form('clear');
                bindMethod();
                row.CreateDate = AllDateTime(row.CreateDate);
                $('#fmDep').form('load', row);
                $('#TotalCharge').numberbox('setValue', row.TotalCharge);
                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
                TrafficType = row.TrafficType;
                if (row.CheckStatus == "0" || ApplyStatus=="2") {
                    columns.push({ title: '数量', field: 'Piece', width: '50px', editor: { type: 'numberbox' } });
                } else {
                    columns.push({ title: '数量', field: 'Piece', width: '50px' });
                }
                columns.push({
                    title: '含税', field: 'WhetherTax', width: '40px', formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='否'>否</span>"; } else if (val == "1") { return "<span title='是'>是</span>"; } else { return ""; }
                    }
                });
                columns.push({ title: '进货价', field: 'UnitPrice', width: '55px' });
                columns.push({ title: '规格', field: 'Specs', width: '100px' });
                columns.push({ title: '花纹', field: 'Figure', width: '100px' });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                        return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                    }
                });
                columns.push({ title: '品牌', field: 'TypeName', width: '95px' });
                columns.push({ title: '货品代码', field: 'GoodsCode', width: '160px' });
                showGrid(columns, row.HouseID);
                if (row.CheckStatus == "2") {
                    $('#SubmitReview').hide();
                    $('#save').hide();
                } else {
                    if (row.ApplyStatus != "" && row.ApplyStatus != "2") {
                        $('#SubmitReview').hide();
                        $('#save').hide();
                    } else {
                        $('#SubmitReview').show();
                        $('#save').show();
                    }
                }

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryFactoryPurchaseOrderGoodsInfo&OrderID=' + row.OrderID;
            }
        }
        //显示列表
        function showGrid(dgSaveCol, houseID) {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({ title: '货品代码', field: 'GoodsCode', width: '150px' });
            columns.push({ title: '规格', field: 'Specs', width: '90px' });
            columns.push({ title: '花纹', field: 'Figure', width: '90px' });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '70px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({ title: '品牌', field: 'TypeName', width: '100px' });
            $('#outDg').datagrid({
                width: Number($("#dlgOrder").width()) * 0.5 - 5 - 50,
                height: 425,
                title: '在库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onDblClickRow: function (index, row) { plup(index); },
                rowStyler: function (index, row) {
                }
            });
            var dgSavewidth = Number($("#dlgOrder").width()) * 0.5 - 5 + 50;
            //dgSavewidth = Number($("#dlgOrder").width()) - 10;

            $('#dgSave').datagrid({
                width: dgSavewidth,
                height: 425,
                title: '订单明细', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [dgSaveCol],
                onClickCell: onClickCell,
                rowStyler: function (index, row) {
                    if (row.RuleTitle != null && row.RuleTitle != "") { return "background-color:#D4EFDF"; };
                }
            });
        }

        $.extend($.fn.datagrid.methods, {
            editCell: function (jq, param) {
                return jq.each(function () {
                    var fields = $(this).datagrid('getColumnFields');
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor1 = col.editor;
                        if (fields[i] != param.field) {
                            col.editor = null;
                        }
                    }
                    $(this).datagrid('beginEdit', param.index);
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor = col.editor1;
                    }
                });
            }
        });
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgSave').datagrid('validateRow', editIndex)) {
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) { return false; }
                var sum = 0;
                if (cg.field == "Piece") {
                    //修改件数
                    var oldPiece = Number(rows.Piece);
                    var salePrice = Number(rows.UnitPrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        if (i == editIndex) {
                            count = Number(count) + newPiece;
                        } else {
                            count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                        }
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', editIndex);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateFactoryPurchaseOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            $.messager.progress("close");
                            if (text.Result == true) {
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    ModifyPrice = ModifyPiece * salePrice;
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TotalCharge').numberbox('getValue'));
                                    $('#TotalCharge').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    qh();
                                } else {
                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TotalCharge').numberbox('getValue'));
                                    $('#TotalCharge').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        var OldIndex = -1;
        function onClickCell(index, field) {
            if (OldIndex == -1) {
                OldIndex = index;
            } else {
                index = OldIndex;
            }
            var rows = $("#dgSave").datagrid('getData').rows[index];
            if (TrafficType == "2") { OldIndex = -1; return; }
            if ($('#dgSaveAwbStatus').val() * 1 < 1 && field == "Piece") {
                if (endEditing()) {
                    $('#dgSave').datagrid('selectRow', index)
                        .datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            } else {
                if (editIndex == undefined) { OldIndex = -1; return true }
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) {
                    OldIndex = -1;
                    return true;
                }
                var sum = 0;
                if (cg.field == "Piece") {
                    var oldPiece = Number(rows.Piece);
                    var salePrice = Number(rows.UnitPrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('cancelEdit', editIndex);
                        editIndex = undefined;
                        OldIndex = -1;
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', index);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        OldIndex = -1;
                        return;
                    }
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateFactoryPurchaseOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            debugger;
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    ModifyPrice = ModifyPiece * salePrice;
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TotalCharge').numberbox('getValue'));
                                    $('#TotalCharge').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    qh();
                                } else {

                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TotalCharge').numberbox('getValue'));
                                    $('#TotalCharge').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }

                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                OldIndex = -1;
            }
        }
        //绑定费用框
        function bindMethod() {
            $("#TotalCharge").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t = Number($('#TotalCharge').numberbox('getValue'));
            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
        }
        //关闭弹出框
        function closeDlg() {
            $('#dlgOutCargo').dialog('close');
            $('#dlgOrder').dialog('close');
            $('#dlgUpdateFacOrderNo').dialog('close');
            $('#dg').datagrid('reload');
        }
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 500;
            var x = 2;  //只接受整数
            $.messager.alert(title, msg, icon, function () { });
            interval = setInterval(fun, time);
            function fun() {
                --x;
                if (x == 0) {
                    clearInterval(interval);
                    $(".messager-body").window('close');
                }
            };
        }
    </script>
</asp:Content>
