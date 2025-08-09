<%@ Page Title="应收应付统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseSalesStatistics.aspx.cs" Inherits="Cargo.Finance.PurchaseSalesStatistics" %>
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
                idField: 'ClientID',
                url: null,
                toolbar: '#dgtoolbar',
                showFooter: true,
                columns: [[
                    { title: '', field: 'ClientID', checkbox: true, width: '30px' },
                    {
                        title: '客户名称', field: 'ClientName', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系人', field: 'Boss', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '电话', field: 'Cellphone', width: '120px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '业务员', field: 'UserName', width: '60px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售合计', field: 'SelFee', width: '80px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '已收款', field: 'SelAffectTotal', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '采购合计', field: 'PurFee', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '已付款', field: 'PurAffectTotal', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '抵扣剩余', field: 'Weight', width: '80px', formatter: function (value, row) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '所属仓库', field: 'HouseID', hidden: true }
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
                valueField: 'HouseID', textField: 'Name',
                required: true
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#AcceptUnit').combobox({
                valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName', delay: '10',
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
            if ($("#AHouseID").combobox('getValue') == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓!', 'info');
                return;
            }
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Finance/financeApi.aspx?method=QueryPurSelFeeData';
            $('#dg').datagrid('load', {
                //AcceptPeople: $("#AcceptPeople").val(),
                ClientNum: $('#AcceptUnit').datebox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue')
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
                gridOpts.url = '../Finance/financeApi.aspx?method=QueryPurSelFeeOrderData&ClientNum=' + row.ClientNum + '&StartDate=' + $('#StartDate').datebox('getValue') + '&EndDate=' + $('#EndDate').datebox('getValue') + '&HouseID=' + row.HouseID;
            }

            //显示列表
            function showGrid() {
                var columns = [];
                //columns.push({ title: '', field: '', checkbox: true, width: '30px' });
                columns.push({ title: '订单类型', field: 'OrderType', width: '60px' });
                columns.push({ title: '订单号', field: 'OrderNo', width: '90px', align: 'right' });
                columns.push({ title: '数量', field: 'Piece', width: '60px' });
                columns.push({ title: '进/销费', field: 'TransportFee', width: '80px' });
                columns.push({ title: '配送费', field: 'TransitFee', width: '80px' });
                columns.push({ title: '装卸费', field: 'HandFee', width: '80px' });
                columns.push({ title: '合计', field: 'TotalCharge', width: '80px' });
                columns.push({ title: '订单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter });
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
