<%@ Page Title="客户欠款统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientArrearsStatistics.aspx.cs" Inherits="Cargo.Report.ClientArrearsStatistics" %>

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
            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ClientNum',
                url: null,
                toolbar: '#dgtoolbar',
                showFooter: true,
                columns: [[
                    { title: '', field: 'ClientNum', checkbox: true, width: '30px' },
                    {
                        title: '仓库', field: 'HouseName', width: '80px'
                    },
                    {
                        title: '订单总数', field: 'OrderCount', width: '60px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '欠款合计', field: 'TotalCharge', width: '80px', align: 'right', styler: function () { return "color:#C98B6F;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '业务员', field: 'SaleManName', width: '60px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '250px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系人', field: 'AcceptPeople', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '电话', field: 'AcceptCellphone', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '地址', field: 'AcceptAddress', width: '350px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '最早下单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { OpenOrderDataByClientNum(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#AcceptUnit').combobox({
                valueField: 'ClientNum', textField: 'ClientName', AddField: 'PinyinName', delay: '10',
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
                required: false
            });
            $('#AcceptUnit').combobox('textbox').bind('focus', function () { $('#AcceptUnit').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryClientArrearsStatistics';
            $('#dg').datagrid('load', {
                //AcceptPeople: $("#AcceptPeople").val(),
                AcceptUnit: $('#AcceptUnit').datebox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),
                SaleManID: $("#SaleManID").combobox('getValue')
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
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <%--<input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'请输入客户联系人'" style="width: 100px" />--%>
                    <input id="AcceptUnit" style="width: 120px;" class="easyui-combobox AcceptPeople" data-options="valueField:'Boss',textField:'Boss',editable:true,required:false" />
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">业务员:
                </td>
                <td>
                    <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 100px;"  data-options="url: 'reportApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName',"/>
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
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="Export()">&nbsp;导出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnExport" runat="server" Style="display: none;" Text="导出" OnClick="btnExport_Click" />
        </form>
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 700px; height: 540px;" closed="true" closable="false" buttons="#dlgOrder-buttons">
        <table id="infoDg" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlgOrder-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <script type="text/javascript">
        //双击显示订单详细界面 
        function OpenOrderDataByClientNum(Did) {
            $('#infoDg').datagrid('loadData', { total: 0, rows: [] });
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgOrder').dialog('open').dialog('setTitle', '查看详细信息');
                showGrid();
                var gridOpts = $('#infoDg').datagrid('options');
                gridOpts.url = 'reportApi.aspx?method=QueryClientArrearsDetailed&ClientNum=' + row.ClientNum + '&StartDate=' + $('#StartDate').datebox('getValue') + '&EndDate=' + $('#EndDate').datebox('getValue') + '&HouseID=' + $("#AHouseID").combobox('getValue');
            }

            //显示列表
            function showGrid() {
                var columns = [];
                //columns.push({ title: '', field: '', checkbox: true, width: '30px' });
                columns.push({
                    title: '订单类型', field: 'OrderModel', width: '70px',
                    formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='客户单'>客户单</span>"; }
                        else if (val == "1") { return "<span title='退货单'>退货单</span>"; }
                        else if (val == "2") { return "<span title='采购单'>采购单</span>"; }
                        else { return ""; }
                    }
                });
                columns.push({ title: '订单号', field: 'OrderNo', width: '90px', align: 'right' });
                columns.push({ title: '总件数', field: 'Piece', width: '60px' });
                columns.push({ title: '销售费用', field: 'TransportFee', width: '80px' });
                columns.push({ title: '物流费用', field: 'DeliveryFee', width: '80px' });
                columns.push({ title: '合计', field: 'TotalCharge', width: '80px', styler: function () { return "color:#C98B6F;font-weight:bold;" } });
                columns.push({ title: '订单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter });
                columns.push({
                    title: '结算状态', field: 'CheckStatus', width: '70px',
                    formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='未结算'>未结算</span>"; }
                        else if (val == "1") { return "<span title='已结清'>已结清</span>"; }
                        else if (val == "2") { return "<span title='未结清'>未结清</span>"; }
                        else { return ""; }
                    }
                });
                $('#infoDg').datagrid({
                    width: '100%',
                    height: '100%',
                    title: '', //标题内容
                    rownumbers: true,
                    loadMsg: '数据加载中请稍候...',
                    autoRowHeight: false, //行高是否自动
                    collapsible: false, //是否可折叠
                    pagination: true, //分页是否显示
                    pageSize: 15, //每页多少条
                    pageList: [15, 30],
                    fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                    singleSelect: true, //设置为 true，则只允许选中一行。
                    checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                    idField: 'ProductID',
                    url: null,
                    toolbar: '#toolbar',
                    columns: [columns],
                    rowStyler: function (index, row) {
                        if (row.OrderType == "销售单") { return "color:#77B563"; };
                        if (row.OrderType == "进货单") { return "color:#B57863"; };
                    }
                });
            }
        }
        //关闭弹出框
        function closeDlg() {
            $('#dlgOrder').dialog('close');
        }
        function Export() {
            var obj = document.getElementById("<%=btnExport.ClientID %>");
            obj.click();
        }
    </script>
</asp:Content>
