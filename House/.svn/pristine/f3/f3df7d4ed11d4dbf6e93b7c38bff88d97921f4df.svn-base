<%@ Page Title="订单导入开单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GtmcImportOrder.aspx.cs" Inherits="Cargo.Order.GtmcImportOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        //页面加载时执行
        window.onload = function () {
            $.ajaxSetup({ async: true });
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        //页面加载显示遮罩层防止用户看见未加载CSS的页面
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //加载完成后关闭遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }

        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: 'TID', checkbox: true, width: '30px' });
            //columns.push({
            //    title: '所属仓库', field: 'HouseName', width: '70px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '订单类型', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='正常订单'>正常订单</span>"; }
                    else if (val == "1") { return "<span title='紧急订单'>紧急订单</span>"; }
                    else { return "<span title=''></span>" }
                }
            });
            columns.push({ title: '路线名', field: 'Route', width: '50px' });
            columns.push({ title: '便次', field: 'FrequentNo', width: '40px' });
            columns.push({
                title: '供应商代码', field: 'SourceCode', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '供应商名称', field: 'SourceName', width: '150px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '发车时间', field: 'DepartTime', width: '130px', formatter: DateTimeFormatter });
            columns.push({ title: '广丰单号', field: 'GtmcNo', width: '80px' });
            columns.push({ title: '纳入番号', field: 'TakeNo', width: '100px' });
            //columns.push({ title: '纳入时间', field: 'TakeTime', width: '130px', formatter: DateTimeFormatter });
            columns.push({
                title: '订单箱数', field: 'Piece', width: '70px', align: 'right'
            });
            columns.push({
                title: '订单状态', field: 'OrderStatus', width: '70px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未生成'>未生成</span>"; }
                    else if (val == "1") { return "<span title='已生成'>已生成</span>"; }
                    else if (val == "2") { return "<span title='缺货订单'>缺货订单</span>" }
                    else { return "<span title=''></span>" }
                }
            });
            columns.push({
                title: '出库单号', field: 'OrderNo', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '出库状态', field: 'AwbStatus', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未出库'>未出库</span>"; }
                    else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                    else if (val == "2") { return "<span title='已出库'>已出库</span>" }
                    else { return "<span title=''></span>" }
                }
            });
            columns.push({ title: '开单时间', field: 'CreateDate', width: '130px' });
            columns.push({
                title: '操作人员', field: 'OPName', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '导单时间', field: 'OPDATE', width: '130px', formatter: DateTimeFormatter });
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.OrderStatus == "1") { return "background-color:#FCF3CF"; };
                    if (row.OrderStatus == "2") { return "background-color:#fbebec;color:#2a83de"; };
                    if (row.OrderType == "1") { return "background-color:#f38f8f"; };
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryGtmcImportData';
            $('#dg').datagrid('load', {
                TakeNo: $('#TakeNo').val(),
                GtmcNo: $("#GtmcNo").val(),
                SourceCode: $("#SourceCode").val(),
                SourceName: $("#SourceName").val(),
                Route: $("#Route").val(),
                FrequentNo: $("#FrequentNo").val(),
                OrderNo: $("#OrderNo").val(),
                AwbStatus: $('#AwbStatus').combobox('getValue'),
                OrderStatus: $('#OrderStatus').combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $("#EndDate").datebox('getValue')
            });

        }
        //导入
        function Import() {
            $('#dlg').dialog('open').dialog('setTitle', '导入GTMC订单数据');
            showData();
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
        }
        //批量导入
        function PlImport() {
            $('#dlgPL').dialog('open').dialog('setTitle', '批量导入GTMC订单数据');
            showPLData();
            $('#dgPLImport').datagrid('loadData', { total: 0, rows: [] });
        }
        //双击显示订单详细界面
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgedit').dialog('open').dialog('setTitle', '查看广丰订单：' + row.GtmcNo);
                $('#fmDep').form('clear');
                $('#fmDep').form('load', row);
                showGrid();
                $("#btnSave").hide();

                if (row.OrderStatus == "0") {
                    $("#btnSave").show();
                }
                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryGTMCOrderByID&TID=' + row.TID;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">路线名:
                </td>
                <td>
                    <input id="Route" class="easyui-textbox" data-options="prompt:'请输入路线名'" style="width: 80px">
                </td>
                <td style="text-align: right;" id="beifan">纳入番号:
                </td>
                <td>
                    <input id="TakeNo" class="easyui-textbox" data-options="prompt:'请输入纳入番号'" style="width: 100px">
                </td>

                <td style="text-align: right;" id="shry">供应商代码:
                </td>
                <td>
                    <input id="SourceCode" class="easyui-textbox" data-options="prompt:'请输入供应商代码'" style="width: 90px">
                </td>
                <td style="text-align: right;">出库单号:
                </td>
                <td>
                    <input id="OrderNo" class="easyui-textbox" data-options="prompt:'请输入出库单号'" style="width: 80px" />
                </td>
                <td style="text-align: right;">订单类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="OrderType" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">正常</option>
                        <option value="1">紧急</option>
                    </select>
                </td>
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="OrderStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未生成</option>
                        <option value="1">已生成</option>
                        <option value="2">缺货订单</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">便次:
                </td>
                <td>
                    <input id="FrequentNo" class="easyui-textbox" data-options="prompt:'请输入便次'" style="width: 80px">
                </td>
                <td style="text-align: right;" id="huawen">广丰单号:
                </td>
                <td>
                    <input id="GtmcNo" class="easyui-textbox" data-options="prompt:'请输入广丰订单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">供应商名称:
                </td>
                <td>
                    <input id="SourceName" class="easyui-textbox" data-options="prompt:'请输入供应商名称'" style="width: 90px">
                </td>
                <td style="text-align: right;">出库状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AwbStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未出库</option>
                        <option value="1">出库中</option>
                        <option value="2">已出库</option>
                    </select>
                </td>
                <td style="text-align: right;">发车时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <%--此div用于显示查询条件--%>
    <table id="dg" class="easyui-datagrid">
    </table>
    <%--此div用于显示按钮操作--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="PlImport()">&nbsp;批&nbsp;量&nbsp;导&nbsp;入&nbsp;</a>&nbsp;&nbsp;
       <%-- <a href="#" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="Import()">&nbsp;导&nbsp;入&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()" id="btnUpdate">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;--%>
    </div>
    <%--此div用于显示按钮操作--%>
    <%--此div用于导入数据--%>
    <div id="dlgPL" class="easyui-dialog" style="width: 1000px; height: 550px; padding: 2px 2px" closed="true" buttons="#dlgPL-buttons">
        <table id="dgPLImport" class="easyui-datagrid">
        </table>
        <%-- <input type="hidden" id="GTMCOrder" />
        <input type="hidden" id="IsExistOrder" />--%>

        <div id="dgPLInporttoolbar">
            <input type="file" id="fileTPL" name="file" accept=".xls,.xlsx,.pdf" multiple="multiple" onchange="savePLFile()" style="width: 250px;" />
            &nbsp;&nbsp;
           <%-- <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="btnPlSaveData()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;--%>
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closePlDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <%--此div用于导入数据--%>


    <%--此div用于导入数据--%>
    <div id="dlg" class="easyui-dialog" style="width: 1000px; height: 530px; padding: 2px 2px" closed="true" buttons="#dlg-buttons">
        <table id="dgImport" class="easyui-datagrid">
        </table>
        <input type="hidden" id="GTMCOrder" />
        <input type="hidden" id="IsExistOrder" />

        <div id="dginporttoolbar">
            <input type="file" id="fileT" name="file" accept=".xls,.xlsx,.pdf" multiple="multiple" onchange="saveFile()" style="width: 250px;" />
            &nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="btnSaveData()">&nbsp;保存开单&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <%--此div用于导入数据--%>
    <%--此div用于查看/新增/编辑数据--%>
    <div id="dlgedit" class="easyui-dialog" style="width: 900px; height: 500px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <form id="fmDep" class="easyui-form" method="post">
            <input type="hidden" name="TID" />
            <table id="saPanel">
                <tr>
                    <td style="text-align: right;">纳入番号:
                    </td>
                    <td>
                        <input name="TakeNo" class="easyui-textbox" style="width: 100px;">
                    </td>
                    <td style="text-align: right;">广丰单号:
                    </td>
                    <td>
                        <input name="GtmcNo" class="easyui-textbox" style="width: 90px;">
                    </td>
                    <td style="text-align: right;">订单箱数:
                    </td>
                    <td>
                        <input name="Piece" class="easyui-textbox" style="width: 70px;">
                    </td>
                    <td style="text-align: right;">供应商代码:
                    </td>
                    <td>
                        <input name="SourceCode" class="easyui-textbox" style="width: 70px;">
                    </td>
                    <td style="text-align: right;">供应商名称:
                    </td>
                    <td>
                        <input name="SourceName" class="easyui-textbox" style="width: 160px;">
                    </td>
                </tr>
            </table>
        </form>
        <table id="dgSave" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="saveItem()">&nbsp;保存开单&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgedit').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <%--此div用于新增/编辑数据--%>

    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">

        function showGrid() {
            var columns = [];
            columns.push({ title: '广丰订单号', field: 'GtmcNo', width: '100px' });
            columns.push({ title: '供应商代码', field: 'SourceCode', width: '100px' });
            columns.push({ title: '品番号', field: 'GoodsCode', width: '100px' });
            columns.push({ title: '背番号', field: 'Specs', width: '70px' });
            columns.push({ title: '箱种', field: 'Cage', width: '50px' });
            //columns.push({ title: '收容', field: 'ReplyNumber', width: '55px' });
            columns.push({ title: '看板张数', field: 'Piece', width: '60px', align: 'right' });
            columns.push({ title: 'A仓库存数', field: 'Stock', width: '70px', align: 'right' });
            columns.push({ title: '其他库存数', field: 'NoStock', width: '70px', align: 'right' });
            columns.push({
                title: '状态', field: 'status', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='正常'>正常</span>"; }
                    else if (val == "1") { return "<span title='缺货'>缺货</span>"; }
                    else { return "<span title=''></span>" }
                }
            });
            //columns.push({ title: '库存情况', field: 'StockInfo', width: '150px' });

            $('#dgSave').datagrid({
                width: '100%',
                height: '380px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,//显示序号
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                url: null,
                columns: [columns]
            });
        }
        //显示DataGrid数据列表
        function showData() {
            var columns = [];
            columns.push({ title: '广丰订单号', field: 'GtmcNo', width: '100px' });
            columns.push({ title: '供应商代码', field: 'SourceCode', width: '100px' });
            columns.push({ title: '品番号', field: 'GoodsCode', width: '100px' });
            columns.push({ title: '背番号', field: 'Specs', width: '70px' });
            columns.push({ title: '箱种', field: 'Cage', width: '50px' });
            //columns.push({ title: '收容', field: 'ReplyNumber', width: '55px' });
            columns.push({ title: '看板张数', field: 'Piece', width: '60px', align: 'right' });
            columns.push({ title: 'A仓库存数', field: 'Stock', width: '70px', align: 'right' });
            columns.push({ title: '其他库存数', field: 'NoStock', width: '70px', align: 'right' });
            columns.push({ title: '状态', field: 'status', width: '55px', hidden: true });
            //columns.push({ title: '库存情况', field: 'StockInfo', width: '150px' });

            $('#dgImport').datagrid({
                width: '100%',
                height: '450px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,//显示序号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                url: null,
                toolbar: '#dginporttoolbar',
                columns: [columns]
            });
        }
        //批量DataGrid数据
        function showPLData() {
            var columns = [];
            columns.push({ title: '', field: 'TID', checkbox: true, width: '30px' });
            columns.push({
                title: '订单类型', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='正常订单'>正常订单</span>"; }
                    else if (val == "1") { return "<span title='紧急订单'>紧急订单</span>"; }
                    else { return "<span title=''></span>" }
                }
            });
            columns.push({ title: '路线名', field: 'Route', width: '50px' });
            columns.push({ title: '便次', field: 'FrequentNo', width: '50px' });
            columns.push({
                title: '供应商代码', field: 'SourceCode', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '供应商名称', field: 'SourceName', width: '150px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '发车时间', field: 'DepartTime', width: '130px', formatter: DateTimeFormatter });
            columns.push({ title: '广丰单号', field: 'GtmcNo', width: '80px' });
            columns.push({ title: '纳入番号', field: 'TakeNo', width: '100px' });
            //columns.push({ title: '纳入时间', field: 'TakeTime', width: '130px', formatter: DateTimeFormatter });
            columns.push({ title: '订单箱数', field: 'Piece', width: '60px', align: 'right' });
            //columns.push({
            //    title: '订单状态', field: 'OrderStatus', width: '70px', formatter: function (val, row, index) {
            //        if (val == "0") { return "<span title='未生成'>未生成</span>"; }
            //        else if (val == "1") { return "<span title='已生成'>已生成</span>"; }
            //        else if (val == "2") { return "<span title='缺货订单'>缺货订单</span>" }
            //        else { return "<span title=''></span>" }
            //    }
            //});
            columns.push({ title: '导入信息', field: 'Info', width: '80px' });
            columns.push({ title: '文件名称', field: 'OrderFileName', width: '250px' });
            //columns.push({title: '系统订单号', field: 'OrderNo', width: '100px'});
            //columns.push({
            //    title: '操作人员', field: 'OPName', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            //columns.push({ title: '操作时间', field: 'OPDATE', width: '130px', formatter: DateTimeFormatter });

            $('#dgPLImport').datagrid({
                width: '100%',
                height: '500px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,//显示序号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                url: null,
                toolbar: '#dgPLInporttoolbar',
                columns: [columns],
                rowStyler: function (index, row) {
                    if (row.OrderType == "1") { return "background-color:#f38f8f"; };
                }
            });
        }
        //批量保存上传的文件
        function savePLFile() {
            var file = $("#fileTPL").val();
            if (file == null || file == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择导入文件!', 'info');
                return;
            }
            $('#dgPLImport').datagrid('loadData', { total: 0, rows: [] });
            $.messager.progress({ msg: '请稍后,正在导入中...' });
            ajaxFilePLUpload();
            $.messager.progress("close");
        }
        //批量文件上传
        function ajaxFilePLUpload() {
            $.ajaxFileUpload({
                url: 'orderApi.aspx?method=savePLPDFFile',
                secureuri: false, dataType: 'json', fileElementId: 'fileTPL',
                success: function (data) {
                    var val = JSON.parse(data.responseText);
                    var result = val.Result;
                    if (result == true) {
                        var value = eval(val.Data);
                        $('#dgPLImport').datagrid('loadData', value);
                        //$('#GTMCOrder').val(val.TempData);
                        if (val.Type == 1) {
                            $('#IsExistOrder').val(1);
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '已存在该广丰订单号数据，请核对!', 'info');
                        }
                    }
                    else {
                        var message = val.Message;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }
                },
                error: function (data) {
                    var val = JSON.parse(data.responseText);
                    var result = val.Result;
                    if (result == true) {
                        var value = eval(val.Data);
                        $('#dgPLImport').datagrid('loadData', value);
                        // $('#GTMCOrder').val(val.TempData);
                        if (val.Type == 1) {
                            $('#IsExistOrder').val(1);
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '已存在该广丰订单号数据，请核对!', 'info');
                        }
                    }
                    else {
                        var message = val.Message;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }

                }
            })
            return false;
        }
        //保存上传的文件
        function saveFile() {
            var file = $("#fileT").val();
            if (file == null || file == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择导入文件!', 'info');
                return;
            }
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            $.messager.progress({ msg: '请稍后,正在导入中...' });
            ajaxFileUpload();
            $.messager.progress("close");
        }

        //文件上传
        function ajaxFileUpload() {
            $.ajaxFileUpload({
                url: 'orderApi.aspx?method=savePDFFile',
                secureuri: false, fileElementId: 'fileT', dataType: 'json',
                success: function (data) {
                    var val = JSON.parse(data.responseText);
                    var result = val.Result;
                    if (result == true) {
                        var value = eval(val.Data);
                        $('#dgImport').datagrid('loadData', value);
                        $('#GTMCOrder').val(val.TempData);
                        if (val.Type == 1) {
                            $('#IsExistOrder').val(1);
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '已存在该广丰订单号数据，请核对!', 'info');
                        }
                    }
                    else {
                        var message = val.Message;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }
                },
                error: function (data) {
                    var val = JSON.parse(data.responseText);
                    var result = val.Result;
                    if (result == true) {
                        var value = eval(val.Data);
                        $('#dgImport').datagrid('loadData', value);
                        $('#GTMCOrder').val(val.TempData);
                        if (val.Type == 1) {
                            $('#IsExistOrder').val(1);
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '已存在该广丰订单号数据，请核对!', 'info');
                        }
                    }
                    else {
                        var message = val.Message;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }

                }
            })
            return false;
        }
        //保存数据
        function btnSaveData() {
            var rows = $("#dgImport").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先导入需要保存的数据！', 'warning');
                return;
            }
            var IsExistOrder = $('#IsExistOrder').val();
            if (IsExistOrder == 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '已存在该广丰订单号数据，请核对后保存!！', 'warning');
                return;
            }
            var msg = "确定保存？";
            var GTMCOrder = $('#GTMCOrder').val();
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=SaveGTMCImportData',
                        type: 'post', dataType: 'json', data: { data: json, GTMCOrder: GTMCOrder },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '开单成功!', 'info');

                                $('#dlg').dialog('close');
                                dosearch();
                                $("#fileT").val("");
                                $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存仓库信息
        function saveItem() {
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#fmDep').form('submit', {
                url: 'orderApi.aspx?method=ReSaveGTMCImportData',
                onSubmit: function () {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    $.messager.progress("close");
                    var text = eval('(' + msg + ')');
                    if (text.Result == true) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '开单成功!', 'info');
                        $('#dlgedit').dialog('close');
                        dosearch();
                    }
                    else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                    }
                }
            })
        }
        //关闭数据导入弹出框
        function closeDlgStatus() {
            $('#dlg').dialog('close');
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            $('#fileT').val("");
        }
        //关闭数据导入弹出框
        function closePlDlgStatus() {
            $('#dlgPL').dialog('close');
            $('#dgPLImport').datagrid('loadData', { total: 0, rows: [] });
            $('#fileTPL').val("");
        }

    </script>
</asp:Content>
