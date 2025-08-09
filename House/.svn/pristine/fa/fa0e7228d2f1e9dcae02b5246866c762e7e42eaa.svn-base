<%@ Page Title="品番资料" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productGtmcUnit.aspx.cs" Inherits="Cargo.Product.productGtmcUnit" %>

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
        //页面加载时执行
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
                idField: 'PID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'PID', checkbox: true, width: '5%' },
                    {
                        title: '供应商名称', field: 'SupplierName', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '供应商品番', field: 'SupplierPinfan', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '广丰品番', field: 'GtmcPinfan', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '广丰背番', field: 'GtmcBeifan', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    { title: '操作时间', field: 'OPDATE', width: '15%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) {  }
                //onDblClickRow: function (index, row) { editItemByID(index); }
            });
            //所在品番
            $('#SupplierCode').combobox({
                url: 'productApi.aspx?method=QueryGtmcSource',
                valueField: 'SourceCode', textField: 'SourceName',
                onSelect: function (rec) {
                    $('#SupplierName').val(rec.SourceName);
                }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'productApi.aspx?method=QueryGtmcProductBasic';
            $('#dg').datagrid('load', {
                SupplierCode: $('#ASupplierCode').val(),
                SupplierPinfan: $("#ASupplierPinfan").val(),
                SupplierName: $("#ASupplierName").val(),
                GtmcPinfan: $("#AGtmcPinfan").val(),
                GtmcBeifan: $("#AGtmcBeifan").val()
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
                <td style="text-align: right;">供应商代码:
                </td>
                <td style="width: 10%">
                    <input id="ASupplierCode" class="easyui-textbox" data-options="prompt:'请输入供应商代码'" style="width: 100%">
                </td>
                <td style="text-align: right;">供应商品番:
                </td>
                <td style="width: 10%">
                    <input id="ASupplierPinfan" class="easyui-textbox" data-options="prompt:'请输入供应商品番'" style="width: 100%">
                </td>

            </tr>
            <tr>
                <td style="text-align: right;">供应商名称:
                </td>
                <td style="width: 10%">
                    <input id="ASupplierName" class="easyui-textbox" data-options="prompt:'请输入供应商名称'" style="width: 100%">
                </td>
                <td style="text-align: right;">广丰品番:
                </td>
                <td style="width: 10%">
                    <input id="AGtmcPinfan" class="easyui-textbox" data-options="prompt:'请输入广丰品番'" style="width: 100%">
                </td>
                <td style="text-align: right;">广丰背番:
                </td>
                <td style="width: 10%">
                    <input id="AGtmcBeifan" class="easyui-textbox" data-options="prompt:'请输入广丰背番'" style="width: 100%">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<%--<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;--%><a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 250px; padding: 1px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="PID" />
                <input type="hidden" name="SupplierName" id="SupplierName" />

                <table>
                    <tr>
                        <td style="text-align: right;">供应商名称:
                        </td>
                        <td>
                            <input name="SupplierCode" id="SupplierCode" class="easyui-combobox" data-options="prompt:'请输入品番名称',required:true"
                                style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">供应商品番:
                        </td>
                        <td>
                            <input name="SupplierPinfan" class="easyui-textbox" data-options="prompt:'请输入供应商品番',required:true"
                                style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">广丰品番:
                        </td>
                        <td>
                            <input name="GtmcPinfan" class="easyui-textbox" data-options="prompt:'请输入广丰品番',required:true"
                                style="width: 250px;">
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">广丰背番:
                        </td>
                        <td>
                            <input name="GtmcBeifan" class="easyui-textbox" data-options="prompt:'请输入广丰背番'"
                                style="width: 250px;">
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <script type="text/javascript">
        //新增品番信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增品番信息');
            $('#fm').form('clear');
            $('#SupplierCode').combobox('textbox').bind('focus', function () { $('#SupplierCode').combobox('showPanel'); });
        }
        //修改品番信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改品番信息');
                $('#fm').form('load', row);
                $('#SupplierCode').combobox('textbox').bind('focus', function () { $('#SupplierCode').combobox('showPanel'); });
            }
        }
        //修改品番信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改品番信息');
                $('#fm').form('load', row);
                $('#SupplierCode').combobox('textbox').bind('focus', function () { $('#SupplierCode').combobox('showPanel'); });
            }
        }
        //删除品番信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'productApi.aspx?method=DelGtmcProductBasic',
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

        //保存品番信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'productApi.aspx?method=SaveGtmcProductBasic',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
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
    </script>

</asp:Content>
