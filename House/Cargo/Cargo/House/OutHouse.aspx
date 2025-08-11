<%@ Page Title="出库管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OutHouse.aspx.cs"
    Inherits="Cargo.House.OutHouse" %>

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
                    idField: 'ID',
                    url: null,
                    toolbar: '#toolbar',
                    columns: [[
                        {
                            title: '', field: 'ID', checkbox: true, width: '30px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '出库单号', field: 'OutCargoID', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '出库件数', field: 'Piece', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '打印出库单', field: 'IsPrintOutCargo', width: '100px',
                            formatter: function (val, row, index) {
                                if (val == false) { return "未打印"; }
                                else if (val == true) { return "已打印"; }
                                else { return "未打印"; }
                            }
                        },
                        {
                            title: '产品名称', field: 'ProductName', width: '100px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '产品类型', field: 'TypeName', width: '60px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '货位代码', field: 'ContainerCode', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '所在区域', field: 'AreaName', width: '60px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '所在仓库', field: 'HouseName', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        { title: '出库时间', field: 'OutHouseTime', width: '130px', formatter: DateTimeFormatter }
                    ]],
                    onLoadSuccess: function (data) { },
                    onDblClickRow: function (index, row) { editItemByID(index); }
                });
                $('#AIsPrintOutCargo').combobox('textbox').bind('focus', function () { $('#AIsPrintOutCargo').combobox('showPanel'); });
            });
            //查询
            function dosearch() {
                $('#dg').datagrid('clearSelections');
                var gridOpts = $('#dg').datagrid('options');
                gridOpts.url = 'houseApi.aspx?method=QueryOutHouseData';
                $('#dg').datagrid('load', {
                    OutCargoID: $('#AOutCargoID').val(),
                    Piece: $("#APiece").val(),
                    IsPrintOutCargo: $("#AIsPrintOutCargo").combobox('getValue')
                });
            }
        </script>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id='Loading'
            style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
            <div style="display: table-cell; vertical-align: middle">
                <h1>
                    <font size="9">页面加载中……</font>
                </h1>
            </div>
        </div>
        <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
            <table>
                <tr>
                    <td style="text-align: right;">出库单号:
                    </td>
                    <td>
                        <input id="AOutCargoID" class="easyui-textbox" data-options="prompt:'请输入出库单号'"
                            style="width: 150px">
                    </td>
                    <td style="text-align: right;">出库件数:
                    </td>
                    <td>
                        <input id="APiece" class="easyui-textbox" data-options="prompt:'请输入出库件数'" style="width: 150px">
                    </td>

                    <td style="text-align: right;">是否打印出库单:
                    </td>
                    <td>
                        <select class="easyui-combobox" id="AIsPrintOutCargo" style="width: 100px;" panelheight="auto">
                            <option value="0">未打印</option>
                            <option value="1">已打印</option>
                            <option value="-1">全部</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <table id="dg" class="easyui-datagrid">
        </table>
        <div id="toolbar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false"
                onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-print" plain="false" onclick="printOutCargoAwb()">&nbsp;打印出库单&nbsp;</a>
        </div>
        <!--Begin 打印入库单-->
        <div id="printInCargoAwb" class="easyui-dialog" style="width: 800px; height: 430px; padding: 2px 2px"
            closed="true" buttons="#printInCargoAwb-buttons">
            <table id="dgPrintInCargo" class="easyui-datagrid">
            </table>
        </div>
        <div id="printInCargoAwb-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-print"
                onclick="btnprintCargoAwb()">&nbsp;打&nbsp;印&nbsp;</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#printInCargoAwb').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
        </div>
        <!--End 打印入库单-->

        <script type="text/javascript">
            //打印入库单
            function printOutCargoAwb() {
                var rows = $('#dg').datagrid('getSelections');
                if (rows == null || rows == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要打印的数据！', 'warning');
                    return;
                }
                $('#printInCargoAwb').dialog('open').dialog('setTitle', '打印出库单数据');
                showPrintGrid();
                var gridOpts = $('#dgPrintInCargo').datagrid('options');
                gridOpts.data = rows;
            }
            function showPrintGrid() {
                $('#dgPrintInCargo').datagrid({
                    width: '100%',
                    height: '100%',
                    title: '需要打印出库单的数据', //标题内容
                    rownumbers: true,
                    loadMsg: '数据加载中请稍候...',
                    autoRowHeight: false, //行高是否自动
                    collapsible: false, //是否可折叠
                    pagination: false, //分页是否显示
                    fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                    singleSelect: false, //设置为 true，则只允许选中一行。
                    checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                    idField: 'ID',
                    url: null,
                    columns: [[
                        { title: '出库单号', field: 'OutCargoID', width: '80px' },
                        { title: '产品名称', field: 'ProductName', width: '100px' },
                        { title: '分类名称', field: 'TypeName', width: '80px' },
                        { title: '型号', field: 'Model', width: '60px' },
                        { title: '货品代码', field: 'GoodsCode', width: '100px' },
                        { title: '规格', field: 'Specs', width: '80px' },
                        { title: '出库件数', field: 'Piece', width: '60px' },
                        { title: '货位代码', field: 'ContainerCode', width: '80px' },
                        {
                            title: '货位类型', field: 'ContainerType', width: '60px',
                            formatter: function (val, row, index) { if (val == "RCK") { return "货架"; } else if (val == "ULD") { return "板"; } else if (val == "CAR") { return "拖车"; } else if (val == "SHL") { return "空地"; } else { return ""; } }
                        },
                        { title: '所在区域', field: 'AreaName', width: '60px' },
                        { title: '一级区域', field: 'FirstAreaName', width: '60px' },
                        { title: '所在仓库', field: 'HouseName', width: '80px' },
                        { title: '出库时间', field: 'OutHouseTime', width: '120px', formatter: DateTimeFormatter }
                    ]]
                });
            }
        </script>

    </asp:Content>