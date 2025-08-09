<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="clientManager.aspx.cs" Inherits="Dealer.clientManager" %>

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
            $('#btnAdd').hide();
            $('#btnUpdate').hide();
            $('#btnDel').hide();
            $('#Save').hide();
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
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ADID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ADID', checkbox: true, width: '30px' },
                    { title: '收货单位', field: 'AcceptCompany', width: '80px' },
                    { title: '收货人', field: 'AcceptPeople', width: '80px' },
                    { title: '手机号码', field: 'AcceptCellphone', width: '80px' },
                    { title: '电话', field: 'AcceptTelephone', width: '80px' },
                    { title: '所在省', field: 'AcceptProvince', width: '60px' },
                    { title: '所在市', field: 'AcceptCity', width: '60px' },
                    { title: '收货地址', field: 'AcceptAddress', width: '300px' },
                    { title: '操作时间', field: 'OP_DATE', width: '120px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                rowStyler: function (index, row) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItem(index); },
                onLoadError: function (data) { }
            });
            //省市区三级联动
            $('#AcceptProvince').combobox({
                url: '../FormService.aspx?method=QueryCityData&PID=0',
                valueField: 'ID', textField: 'City',
                onSelect: function (rec) {
                    $('#AcceptCity').combobox('clear');
                    var url = '../FormService.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#AcceptCity').combobox('reload', url);
                }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryAcceptAddress';
            $('#dg').datagrid('load', {
                AcceptCompany: $('#AAcceptCompany').val(),
                AcceptPeople: $('#AAcceptPeople').val(),
                AcceptCellphone: $('#AAcceptCellphone').val()
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
                <td style="text-align: right;">收货单位:
                </td>
                <td>
                    <input id="AAcceptCompany" class="easyui-textbox" data-options="prompt:'请输入收货单位'" style="width: 150px" />
                </td>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="AAcceptPeople" class="easyui-textbox" data-options="prompt:'请输入收货人'" style="width: 100px" />
                </td>
                <td style="text-align: right;">手机号码:
                </td>
                <td>
                    <input id="AAcceptCellphone" class="easyui-textbox" data-options="prompt:'请输入手机号码'" style="width: 90px" />
                </td>
            </tr>
        </table>
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
    <div id="dlgAddAcceptAddress" class="easyui-dialog" style="width: 800px; height: 300px; padding: 0px 0px"
        closed="true" buttons="#dlgAddAcceptAddress-buttons">
        <div id="saPanel">
            <form id="fmAddAcceptAddress" class="easyui-form" method="post">
                <input type="hidden" name="ADID" />
                <input type="hidden" name="ClientID" id="ADClientID" />
                <table>
                    <tr>
                        <td style="text-align: right;">收货单位:
                        </td>
                        <td>
                            <input name="AcceptCompany" id="AcceptCompany" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AcceptPeople" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">手机号码:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AcceptCellphone" class="easyui-numberbox" data-options="required:true" style="width: 250px;">
                        </td>
                        <td style="text-align: right;">公司电话:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AcceptTelephone" class="easyui-textbox" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">收货地址:
                        </td>
                        <td colspan="3">
                            <input id="AcceptProvince" name="AcceptProvince" class="easyui-combobox" style="width: 70px;" data-options="required:true" />&nbsp;省
                            <input id="AcceptCity" name="AcceptCity" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City',required:true" />&nbsp;市
                            <%--<input id="AcceptCountry" name="AcceptCountry" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;区&nbsp;--%>
                            <input name="AcceptAddress" id="AcceptAddress" class="easyui-textbox" style="width: 400px;">
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
    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">

        //保存客户收货地址
        function saveAcceptAddress() {
            $('#fmAddAcceptAddress').form('submit', {
                url: '../FormService.aspx?method=SaveAcceptAddress',
                onSubmit: function (param) {
                    param.AProvince = $('#AcceptProvince').combobox('getText');
                    param.ACity = $('#AcceptCity').combobox('getText');
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
        //新增收货地址
        function addItem() {
            $('#dlgAddAcceptAddress').dialog('open').dialog('setTitle', '新增收货地址信息');
            $('#fmAddAcceptAddress').form('clear');
        }
        //修改客户信息
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
                $('#dlgAddAcceptAddress').dialog('open').dialog('setTitle', '收货地址信息');
                $('#fmAddAcceptAddress').form('load', rows[0]);

                $('#AClientNum').combobox('readonly', true);
            }
        }
        //删除客户收货地址
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
                        url: '../FormService.aspx?method=DelAcceptAddress',
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
