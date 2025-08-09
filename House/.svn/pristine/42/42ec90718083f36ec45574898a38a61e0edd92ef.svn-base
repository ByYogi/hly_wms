<%@ Page Title="产品价格管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="priceProductManagerFTS.aspx.cs" Inherits="Cargo.Price.priceProductManagerFTS" %>

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
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ID', checkbox: true },
                    {
                        title: '产品编码', field: 'ProductCode'
                    },
                    {
                        title: '产品名称', field: 'ProductName'
                    },
                    {
                        title: '价格类型', field: 'PriceType',
                        formatter: function (value) {
                            if (value == "5") { return "<span title='长和'>长和</span>"; }
                            else if (value == "6") { return "<span title='商贸'>商贸</span>"; }
                            else if (value == "7") { return "<span title='其它'>其它</span>"; }
                            else if (value == "10") { return "<span title='祺航'>祺航</span>"; }
                        }
                    },
                    { title: '入手价', field: 'SalePriceClient' },
                    { title: '对店价', field: 'CostPriceStore' },
                    { title: '操作时间', field: 'OP_DATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'priceApi.aspx?method=QueryCargoProductPriceFTS';
            $('#dg').datagrid('load', {
                ProductCode: $('#ProductCode').val(),
                ProductName: $("#ProductName").val()
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
                <td style="text-align: right;">产品编码:
                </td>
                <td>
                    <input id="ProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码'" style="width: 150px" />
                </td>
                <td style="text-align: right;">产品名称:
                </td>
                <td>
                    <input id="ProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 150px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="Import()">&nbsp;导&nbsp;入&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 600px; height: 300px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="ID" />
                <table>
                    <tr>
                        <td style="text-align: right;">产品编码:
                        </td>
                        <td>
                            <input name="ProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码',required:true"
                                style="width: 150px;">
                        </td>
                        <td style="text-align: right;">产品名称:
                        </td>
                        <td>
                            <input name="ProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称',required:true"
                                style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">商贸入手价:
                        </td>
                        <td>
                            <input name="SMClientPrice" id="SMClientPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">商贸对店价:
                        </td>
                        <td>
                            <input name="SMStorePrice" id="SMStorePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">长和入手价:
                        </td>
                        <td>
                            <input name="CHClientPrice" id="CHClientPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">长和对店价:
                        </td>
                        <td>
                            <input name="CHStorePrice" id="CHStorePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">祺航入手价:
                        </td>
                        <td>
                            <input name="QHClientPrice" id="QHClientPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">祺航对店价:
                        </td>
                        <td>
                            <input name="QHStorePrice" id="QHStorePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">其它入手价:
                        </td>
                        <td>
                            <input name="DefaultClientPrice" id="DefaultClientPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">其它对店价:
                        </td>
                        <td>
                            <input name="DefaultStorePrice" id="DefaultStorePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">出库包装:
                        </td>
                        <td>
                            <input name="OutPackType" id="CardType0" type="radio" value="0" /><label for="CardType0" style="font-size: 15px;">无</label>
                            <input name="OutPackType" id="CardType1" type="radio" value="1" /><label for="CardType1" style="font-size: 15px;">二次包装</label>
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
    <%--此div用于导入数据--%>
    <div id="dlgImport" class="easyui-dialog" style="width: 1000px; height: 530px; padding: 2px 2px" closed="true" buttons="#dlgImport-buttons">
        <table id="dgImport" class="easyui-datagrid">
        </table>
        <div id="dginporttoolbar">
            <input type="file" id="fileT" name="file" accept=".xls,.xlsx" onchange="saveFile()" style="width: 250px;" />
            <input type="hidden" id="ExistCount" />
            <a href="#" id="btnload" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="saveFile()">&nbsp;重新上传&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="btnSaveData()">&nbsp;保存数据&nbsp;</a>&nbsp;&nbsp;
            <a href="../upload/sFile/基础产品价格导入模板.xls" id="dowload" class="easyui-linkbutton" iconcls="icon-application_put" plain="false">&nbsp;下载模板&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <%--此div用于导入数据--%>

    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        //导入
        function Import() {
            $('#dlgImport').dialog('open').dialog('setTitle', '导入产品价格数据');
            showData();
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
        }

        //显示DataGrid数据列表
        function showData() {
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
                columns: [[
                    { title: '产品编码', field: 'ProductCode', width: '13%' },
                    { title: '产品名称', field: 'ProductName', width: '22%' },
                    { title: '商贸入手价', field: 'SMClientPrice', width: '8%' },
                    { title: '商贸对店价', field: 'SMStorePrice', width: '8%' },
                    { title: '长河入手价', field: 'CHClientPrice', width: '8%' },
                    { title: '长河对店价', field: 'CHStorePrice', width: '8%' },
                    { title: '祺航入手价', field: 'QHClientPrice', width: '8%' },
                    { title: '祺航对店价', field: 'QHStorePrice', width: '8%' },
                    { title: '其它入手价', field: 'DefaultClientPrice', width: '8%' },
                    { title: '其它对店价', field: 'DefaultStorePrice', width: '8%' }
                ]]
            });
        }
        //保存上传的文件
        function saveFile() {
            var file = $("#fileT").val();
            if (file == null || file == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择导入文件!', 'info');
                return;
            }
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            ajaxFileUpload();
        }

        //文件上传
        function ajaxFileUpload() {
            $('#ExistCount').val('');
            $.ajaxFileUpload({
                url: 'priceApi.aspx?method=saveProductPriceFile',
                secureuri: false, fileElementId: 'fileT', dataType: 'json',
                success: function (data) {
                    var val = JSON.parse(data.responseText);
                    var result = val.Result;
                    if (result == true) {
                        var type = val.Type;
                        if (type == 1) {

                            var reg = new RegExp("\r\n", "g");
                            var message = val.Message.replace(reg, "<br>");
                            $.messager.alert('以下Excel数据有误已跳过导入', message, 'warning');
                        }
                        var value = eval(val.Data);
                        $('#dgImport').datagrid('loadData', value);
                        if (val.ExistCount > 0) {
                            $('#ExistCount').val(val.ExistCount);
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
                        var type = val.Type;
                        if (type == 1) {

                            var reg = new RegExp("\r\n", "g");
                            var message = val.Message.replace(reg, "<br>");
                            $.messager.alert('以下Excel数据有误已跳过导入', message, 'warning');
                        }
                        var value = eval(val.Data);
                        $('#dgImport').datagrid('loadData', value);
                        if (val.ExistCount > 0) {
                            $('#ExistCount').val(val.ExistCount);
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
            var msg = "确定保存？";

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'priceApi.aspx?method=SaveProductPriceData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '导入成功!', 'info');
                                $('#dlgImport').dialog('close');
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

        //关闭数据导入弹出框
        function closeDlgStatus() {
            $('#dlgImport').dialog('close');
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            $('#fileT').val("");
        }

        //新增仓库信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增信息');
            $('#fm').form('clear');
            $('#SMClientPrice').numberbox('enable');
            $('#SMStorePrice').numberbox('enable');
            $('#CHClientPrice').numberbox('enable');
            $('#CHStorePrice').numberbox('enable');
            $('#QHClientPrice').numberbox('enable');
            $('#QHStorePrice').numberbox('enable');
            $('#DefaultClientPrice').numberbox('enable');
            $('#DefaultStorePrice').numberbox('enable');
        }
        //修改仓库信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改产品价格信息');
                $('#fm').form('load', row);
                $('#SMClientPrice').numberbox('disable');
                $('#SMStorePrice').numberbox('disable');
                $('#CHClientPrice').numberbox('disable');
                $('#CHStorePrice').numberbox('disable');
                $('#QHClientPrice').numberbox('disable');
                $('#QHStorePrice').numberbox('disable');
                $('#DefaultClientPrice').numberbox('disable');
                $('#DefaultStorePrice').numberbox('disable');
                if (row.PriceType == "5") {
                    //商贸
                    $('#SMClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#SMStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#SMClientPrice').numberbox('enable');
                    $('#SMStorePrice').numberbox('enable');
                } else if (row.PriceType == "6") {
                    //长和
                    $('#CHClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#CHStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#CHClientPrice').numberbox('enable');
                    $('#CHStorePrice').numberbox('enable');
                } else if (row.PriceType == "7") {
                    //其他
                    $('#DefaultClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#DefaultStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#DefaultClientPrice').numberbox('enable');
                    $('#DefaultStorePrice').numberbox('enable');
                } else if (row.PriceType == "10") {
                    //祺航
                    $('#QHClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#QHStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#QHClientPrice').numberbox('enable');
                    $('#QHStorePrice').numberbox('enable');
                }
            }
        }
        //修改仓库信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改产品价格信息');
                $('#fm').form('load', row);
                $('#SMClientPrice').numberbox('disable');
                $('#SMStorePrice').numberbox('disable');
                $('#CHClientPrice').numberbox('disable');
                $('#CHStorePrice').numberbox('disable');
                $('#QHClientPrice').numberbox('disable');
                $('#QHStorePrice').numberbox('disable');
                $('#DefaultClientPrice').numberbox('disable');
                $('#DefaultStorePrice').numberbox('disable');
                if (row.PriceType == "5") {
                    //商贸
                    $('#CHClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#CHStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#CHClientPrice').numberbox('enable');
                    $('#CHStorePrice').numberbox('enable');
                } else if (row.PriceType == "6") {
                    //长和
                    $('#SMClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#SMStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#SMClientPrice').numberbox('enable');
                    $('#SMStorePrice').numberbox('enable');
                } else if (row.PriceType == "7") {
                    //其他
                    $('#DefaultClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#DefaultStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#DefaultClientPrice').numberbox('enable');
                    $('#DefaultStorePrice').numberbox('enable');
                } else if (row.PriceType == "10") {
                    //祺航
                    $('#QHClientPrice').numberbox('setValue', row.SalePriceClient);
                    $('#QHStorePrice').numberbox('setValue', row.CostPriceStore);
                    $('#QHClientPrice').numberbox('enable');
                    $('#QHStorePrice').numberbox('enable');
                }
            }
        }
        //删除仓库信息
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
                        url: 'priceApi.aspx?method=DelProductPriceFTS',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('clearSelections');
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

        //保存仓库信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'priceApi.aspx?method=SaveProductPriceFTS',
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
