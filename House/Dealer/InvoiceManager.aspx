<%@ Page Title="开票管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvoiceManager.aspx.cs" Inherits="Dealer.InvoiceManager" %>

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
            //$('#btnAdd').hide();
            //$('#btnUpdate').hide();
            //$('#btnDel').hide();
            //$('#Save').hide();
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            //var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            //$('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                height: '300px',
                title: '发票抬头管理', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '发票抬头', field: 'HeaderInfo', width: '150px' },
                    {
                        title: '抬头类型', field: 'HeaderType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "企业"; } else if (val == "1") { return "个人"; } else { return ""; }
                        }
                    },
                    {
                        title: '发票类型', field: 'InvoiceType', width: '100px', formatter: function (val, row, index) {
                            if (val == "0") { return "增值税普通发票"; } else if (val == "1") { return "增值税专用发票"; } else { return ""; }
                        }
                    },
                    { title: '统一社会信用代码', field: 'SocialCode', width: '150px' },
                    { title: '开户银行名称', field: 'BankName', width: '120px' },
                    { title: '开户基本账号', field: 'BankNumber', width: '120px' },
                    { title: '注册公司地址', field: 'RegisAddress', width: '200px' },
                    { title: '注册电话', field: 'RegisTelephone', width: '100px' },
                    { title: '操作时间', field: 'OP_DATE', width: '120px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                rowStyler: function (index, row) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItem(index); }
            });
            $('#dgPostAddress').datagrid({
                width: '100%',
                height: '300px',
                title: '邮寄地址管理', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbarPostAddress',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '收件人', field: 'AcceptPeople', width: '80px' },
                    { title: '收件电话', field: 'AcceptCellphone', width: '100px' },
                    {
                        title: '收件地址', field: 'AcceptAddress', width: '300px', formatter: function (val, row, index) {
                            return row.AcceptProvince + " " + row.AcceptCity + " " + row.AcceptCountry + " " + row.AcceptAddress;
                        }
                    },
                    { title: '邮政编码', field: 'ZipCode', width: '80px' },
                    { title: '操作时间', field: 'OP_DATE', width: '120px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                rowStyler: function (index, row) { },
                onClickRow: function (index, row) {
                    $('#dgPostAddress').datagrid('clearSelections');
                    $('#dgPostAddress').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editPostAddress(index); }
            });
            $("input[name=InvoiceType]").click(function () { showCont(); });
            //省市区三级联动
            $('#AcceptProvince').combobox({
                url: '../FormService.aspx?method=QueryCityData&PID=0',valueField: 'ID', textField: 'City',
                onSelect: function (rec) {
                    $('#AcceptCity').combobox('clear');
                    var url = '../FormService.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#AcceptCity').combobox('reload', url);
                    $('#AcceptCity').combobox({
                        onSelect: function (fai) {
                            $('#AcceptCountry').combobox('clear');
                            var url = '../FormService.aspx?method=QueryCityData&PID=' + fai.ID;
                            $('#AcceptCountry').combobox('reload', url);
                        }
                    });
                }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryClientInvoiceHeader';
            $('#dg').datagrid('load', {
                ClientNum: "<%=UserInfor.LoginName%>"
            });
        }
        //查询邮寄地址数据
        function queryPostAddress() {
            $('#dgPostAddress').datagrid('clearSelections');
            var gridOpts = $('#dgPostAddress').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryClientPostAddress';
            $('#dgPostAddress').datagrid('load', {
                ClientNum: "<%=UserInfor.LoginName%>"
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
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()" id="btnAdd">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()" id="btnUpdate">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()" id="btnDel">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;
    </div>
    <!--新增客户收货地址-->
    <div id="dlgAddAcceptAddress" class="easyui-dialog" style="width: 600px; height: 300px; padding: 0px 0px"
        closed="true" buttons="#dlgAddAcceptAddress-buttons">
        <div id="saPanel">
            <form id="fmAddAcceptAddress" class="easyui-form" method="post">
                <input type="hidden" name="ID" />
                <table>
                    <tr>
                        <td style="text-align: right;">抬头类型:
                        </td>
                        <td>
                            <input name="HeaderType" id="HeaderType0" type="radio" value="0" /><label for="HeaderType0" style="font-size: 15px;">企业</label>
                            <%--<input name="CardType" id="CardType1" type="radio" value="1" /><label for="CardType1" style="font-size: 15px;">个人</label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">发票抬头:
                        </td>
                        <td>
                            <input name="HeaderInfo" id="HeaderInfo" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">发票类型:
                        </td>
                        <td>
                            <input name="InvoiceType" id="InvoiceType0" type="radio" value="0" /><label for="InvoiceType0" style="font-size: 15px;">增值税普通发票</label>
                            <input name="InvoiceType" id="InvoiceType1" type="radio" value="1" /><label for="InvoiceType1" style="font-size: 15px;">增值税专用发票</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">统一社会信用代码:
                        </td>
                        <td>
                            <input name="SocialCode" id="SocialCode" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">开户银行名称:
                        </td>
                        <td>
                            <input name="BankName" id="BankName" class="easyui-textbox" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">基本开户账号:
                        </td>
                        <td>
                            <input name="BankNumber" id="BankNumber" class="easyui-textbox" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">注册场所地址:
                        </td>
                        <td>
                            <input name="RegisAddress" id="RegisAddress" class="easyui-textbox" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">注册固定电话:
                        </td>
                        <td>
                            <input name="RegisTelephone" id="RegisTelephone" class="easyui-textbox" style="width: 250px;">
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlgAddAcceptAddress-buttons">
        <a href="#" id="Save" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveAcceptAddress()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAddAcceptAddress').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--新增客户收货地址-->
    <table id="dgPostAddress" class="easyui-datagrid">
    </table>
    <div id="toolbarPostAddress">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="queryPostAddress()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addPostAddress()" id="btnPostAdd">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editPostAddress()" id="btnPostUpdate">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelPostAddress()" id="btnPostDel">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;
    </div>
    <!--新增客户收货地址-->
    <div id="dlgAddPostAddress" class="easyui-dialog" style="width: 600px; height: 300px; padding: 0px 0px"
        closed="true" buttons="#dlgAddPostAddress-buttons">
        <div id="saPanel">
            <form id="fmAddPostAddress" class="easyui-form" method="post">
                <input type="hidden" name="ID" />
                <table>

                    <tr>
                        <td style="text-align: right;">收件人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AcceptPeople" class="easyui-textbox" data-options="required:true"
                                style="width: 400px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">联系电话:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AcceptCellphone" class="easyui-textbox" data-options="required:true" style="width: 400px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">邮政编码:
                        </td>
                        <td>
                            <input name="ZipCode" id="ZipCode" class="easyui-textbox" style="width: 400px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">省市区:
                        </td>
                        <td colspan="3">
                            <input id="AcceptProvince" name="AcceptProvince" class="easyui-combobox" style="width: 80px;" />&nbsp;省
                            <input id="AcceptCity" name="AcceptCity" class="easyui-combobox" style="width: 80px;" data-options="valueField:'ID',textField:'City'" />&nbsp;市
                            <input id="AcceptCountry" name="AcceptCountry" class="easyui-combobox" style="width: 80px;" data-options="valueField:'ID',textField:'City'" />&nbsp;区&nbsp;
                          
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">收件地址:
                        </td>
                        <td>
                            <input name="AcceptAddress" id="AcceptAddress" class="easyui-textbox" data-options="required:true" style="width: 400px;">
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
    <div id="dlgAddPostAddress-buttons">
        <a href="#" id="SavePostAddress" class="easyui-linkbutton" iconcls="icon-ok" onclick="savePostAddress()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAddPostAddress').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--新增客户收货地址-->
    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        //删除邮寄地址
        function DelPostAddress() {
            var rows = $('#dgPostAddress').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Dealer.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../FormService.aspx?method=DelClientPostAddress',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dgPostAddress').datagrid('reload');
                                $('#dgPostAddress').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //新增邮寄地址
        function addPostAddress() {
            $('#dlgAddPostAddress').dialog('open').dialog('setTitle', '新增发票邮寄地址');
            $('#dlgAddPostAddress').form('clear');
        }
        function savePostAddress() {
            $('#fmAddPostAddress').form('submit', {
                url: '../FormService.aspx?method=SaveClientPostAddress',
                onSubmit: function (param) {
                    param.AProvince = $('#AcceptProvince').combobox('getText');
                    param.ACity = $('#AcceptCity').combobox('getText');
                    param.ACountry = $('#AcceptCountry').combobox('getText');
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgAddPostAddress').dialog('close'); 	// close the dialog
                        queryPostAddress();
                    } else {
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        function editPostAddress() {
            var rows = $('#dgPostAddress').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#fmAddPostAddress').form('clear');
                $('#dlgAddPostAddress').dialog('open').dialog('setTitle', '修改发票邮寄地址信息');
                $('#fmAddPostAddress').form('load', rows[0]);
            }
        }
        function showCont() {
            switch ($("input[name=InvoiceType]:checked").attr("id")) {
                case "InvoiceType1":
                    $("#BankName").textbox('textbox').validatebox({ required: true });
                    $("#BankNumber").textbox('textbox').validatebox({ required: true });
                    $("#RegisAddress").textbox('textbox').validatebox({ required: true });
                    $("#RegisTelephone").textbox('textbox').validatebox({ required: true });
                    break;
                case "InvoiceType0":
                    $("#BankName").textbox('textbox').validatebox({ required: false });
                    $("#BankNumber").textbox('textbox').validatebox({ required: false });
                    $("#RegisAddress").textbox('textbox').validatebox({ required: false });
                    $("#RegisTelephone").textbox('textbox').validatebox({ required: false });
                    break;
                default:
                    break;
            }
        }
        //保存发票抬头信息
        function saveAcceptAddress() {
            $('#fmAddAcceptAddress').form('submit', {
                url: '../FormService.aspx?method=SaveClientInvoiceHeader',
                onSubmit: function (param) {
                    //param.AProvince = $('#AcceptProvince').combobox('getText');
                    //param.ACity = $('#AcceptCity').combobox('getText');
                    //param.ACountry = $('#AcceptCountry').combobox('getText');
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgAddAcceptAddress').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //新增发票抬头信息
        function addItem() {
            $('#dlgAddAcceptAddress').dialog('open').dialog('setTitle', '新增发票抬头信息');
            $('#fmAddAcceptAddress').form('clear');
            $("input[name=HeaderType]").get(0).checked = true;
        }
        //修改发票抬头信息
        function editItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#fmAddAcceptAddress').form('clear');
                $('#dlgAddAcceptAddress').dialog('open').dialog('setTitle', '修改发票抬头信息');
                $('#fmAddAcceptAddress').form('load', rows[0]);
            }
        }
        //删除发票抬头信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Dealer.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../FormService.aspx?method=DelClientInvoiceHeader',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dg').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
    </script>

</asp:Content>
