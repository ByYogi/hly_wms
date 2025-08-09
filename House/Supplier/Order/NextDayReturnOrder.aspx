<%@ Page Title="次日达退货单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NextDayReturnOrder.aspx.cs" Inherits="Supplier.Order.NextDayReturnOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            //$.ajaxSetup({ async: true });
            document.body.style.overflow = 'hidden';
            adjustment();
            $('#up').hide();
            $('#save').hide();
            document.body.style.overflow = 'auto';
        }
        $(window).resize(function () {
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
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
                title: '客户单位', field: 'AcceptUnit', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '客户名称', field: 'AcceptPeople', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '联系电话', field: 'AcceptCellphone', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '退货地址', field: 'AcceptAddress', width: '20%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '退货金额', field: 'TransportFee', width: '20%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单状态', field: 'AwbStatus', width: '80px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                    else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                    else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                    else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                    else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                    else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                    else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                    else if (val == "7") { return "<span title='正在配送'>正在配送</span>"; }
                    else { return ""; }
                }
            });

            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50, 200],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#dgtoolbar',
                frozenColumns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    { title: '退货时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                    {
                        title: '退货单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                columns: [columns],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.TrafficType == "2") { return "background-color:#b3ce7e"; };
                    if (row.ThrowGood == "17") { return "background-color:#f38f8f"; };
                },
                onDblClickRow: function (index, row) { editItem(index); }
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
            gridOpts.url = 'orderApi.aspx?method=QueryReturnOrdersInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                ThrowGood:'23'
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
                <td style="text-align: right;">销售客户:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'请输入销售客户'" style="width: 100px;" />
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
        <a href="#" id="btnExportOrder" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportOrderInfo()">&nbsp;导出订单列表&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />
        </form>
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 75%; height: 600px;" closed="true" closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <div id="saPanel">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">退货单位:
                        </td>
                        <td>
                            <input name="AcceptUnit" id="AAcceptUnit" data-options="disabled:false" class="easyui-textbox" style="width: 125px;" />
                        </td>
                        <td style="text-align: right;">联系人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AAcceptPeople" style="width: 150px;" class="easyui-textbox" />
                        </td>
                        <td style="text-align: right;">手机号码:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" data-options="disabled:false" class="easyui-textbox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">电话号码:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AAcceptTelephone" data-options="disabled:false" class="easyui-textbox" style="width: 100px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 70px;" readonly="true" />
                        </td>
                        <td style="text-align: right;">退货金额:
                        </td>
                        <td>
                            <input name="TotalCharge" id="ATotalCharge" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 70px;" readonly="true" />
                        </td>
                        <td style="text-align: right;">收货地址:
                        </td>
                        <td colspan="3">
                            <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:false" style="width: 250px;" class="easyui-textbox" />
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
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOrder').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        //双击显示订单详细界面 
        function editItem(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo);
                $('#fmDep').form('clear');
                $('#fmDep').form('load', row);
                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
                columns.push({ title: '产品编码', field: 'GoodsCode', width: '180px' });
                columns.push({ title: '品牌', field: 'TypeName', width: '100px' });
                columns.push({ title: '规格', field: 'Specs', width: '110px' });
                columns.push({ title: '花纹', field: 'Figure', width: '100px' });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                        return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                    }
                });
                columns.push({ title: '批次', field: 'Batch', width: '60px' });
                columns.push({ title: '数量', field: 'Piece', width: '50px' });
                columns.push({ title: '单价', field: 'ActSalePrice', width: '50px' });

                showGrid(columns);

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryReturnOrderDetails&OrderNo=' + row.OrderNo;
            }
        }
        //显示列表
        function showGrid(dgSaveCol) {
            $('#dgSave').datagrid({
                width: Number($("#dlgOrder").width())-50,
                height: 390,
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
                columns: [dgSaveCol]
            });
        }
        function ExportOrderInfo() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "orderApi.aspx?method=QueryReturnOrdersInfo&OrderNo=" + $("#AOrderNo").val() + "&AcceptPeople=" + $("#AcceptPeople").val() +
                    "&ThrowGood=23&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue'),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnOrderInfo.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
            $.messager.progress("close");
        }
    </script>
</asp:Content>
