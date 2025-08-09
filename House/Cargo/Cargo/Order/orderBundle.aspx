<%@ Page Title="捆包管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="orderBundle.aspx.cs" Inherits="Cargo.Order.orderBundle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var pc;
        var LogiName =<%=UserInfor.LoginName%>;
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
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height });
            $('#outDg').datagrid({ height: (Number($(window).height()) - 90) - height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: 'BunID', checkbox: true });
            columns.push({ title: '区域大仓', field: 'HouseName', width: '100px' });
            columns.push({ title: '捆包单号', field: 'BundleNo', width: '100px' });
            //columns.push({ title: '承运物流商', field: 'LogisticName', width: '100px' });
            columns.push({ title: '捆包数量', field: 'TotalNum', width: '80px', align: 'right' });
            columns.push({
                title: '捆包状态', field: 'BundleStatus', width: '80px', formatter: function (value) {
                    if (value == "0") { return "未开始"; }
                    else if (value == "1") { return "捆包中"; }
                    else if (value == "2") { return "已结束"; }
                }
            });
            columns.push({ title: '创建时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter });

            $('#dg').datagrid({
                width: '100%',
                //height: '50%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                rownumbers: true, //行号
                pageSize: Math.floor((Number($(window).height()) / 2 - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) / 2 - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) / 2 - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#outDg').datagrid('clearSelections');
                    var gridOpts = $('#outDg').datagrid('options');
                    gridOpts.url = 'orderApi.aspx?method=QueryBundleGoods';
                    $('#outDg').datagrid('load', { BundleNo: row.BundleNo });
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { }
            });

            columns = [];
            columns.push({ title: '', field: 'BunGoodsID', checkbox: true });
            columns.push({ title: '零件号码', field: 'GoodsCode', width: '80px' });
            columns.push({ title: '零件名称', field: 'ProductName', width: '150px' });
            columns.push({ title: '零件数量', field: 'Piece', width: '80px', align: 'right' });
            columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });



            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '捆包单明细', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true, //行号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#unbindBar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { }
            });
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name', onSelect: function (rec) { }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
        });


        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryBundle';
            $('#dg').datagrid('load', {
                HouseID: $("#AHouseID").combobox('getValue'),
                StartDate: $('#AStartDate').datebox('getValue'),
                EndDate: $('#AEndDate').datebox('getValue'),
                BundleNo: $("#ABundleNo").textbox('getValue'),
                BundleStatus: $("#ABundleStatus").combobox('getValue')
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
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">捆绑包号:
                </td>
                <td>
                    <input id="ABundleNo" class="easyui-textbox" data-options="prompt:'请输入捆包单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="ABundleStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未开始</option>
                        <option value="1">捆包中</option>
                        <option value="2">已结束</option>
                    </select>
                </td>
                <td style="text-align: right;">查询时间:
                </td>
                <td>
                    <input id="AStartDate" class="easyui-datebox" style="width: 100px" />~
                        <input id="AEndDate" class="easyui-datebox" style="width: 100px" />
                </td>

            </tr>

        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="outDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
    </div>

    <div id="unbindBar">
        &nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 400px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="BunGoodsID" />
                <table>
                    <tr>
                        <td style="text-align: right;">捆包单号:
                        </td>
                        <td>
                            <input name="BundleNo" class="easyui-textbox"
                                style="width: 250px;" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">零件号码:
                        </td>
                        <td>
                            <input name="GoodsCode" class="easyui-textbox" data-options="prompt:'请输入零件号码'"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">零件名称:
                        </td>
                        <td>
                            <input name="ProductName" class="easyui-textbox" data-options="prompt:'请输入零件名称'"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">扫描数量:
                        </td>
                        <td>
                            <input name="Piece" class="easyui-textbox" data-options="prompt:'请输入扫描数量'"
                                style="width: 250px;" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <script type="text/javascript">
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            $('#fm').form('clear');
        }

        function saveItem() {
            $('#fm').form('submit', {
                url: 'orderApi.aspx?method=SaveBundleGoods',
                onSubmit: function (param) {
                    var trd = $(this).form('enableValidation').form('validate');
                    return trd;
                    //return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        function editItem() {
            var row = $('#outDg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改信息');
                $('#fm').form('load', row);
                console.log(row)
            }
        }

        function DelItem() {
            var rows = $('#outDg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    console.log(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=DelBundleGoods',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
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
    </script>
</asp:Content>
