<%@ Page Title="客户管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dealerClientManager.aspx.cs" Inherits="Cargo.Client.dealerClientManager" %>

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
                idField: 'ClientID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ClientID', checkbox: true, width: '30px' },
                    { title: '客户编码', field: 'ClientNum', width: '70px' },
                    {
                        title: '公司名称', field: 'ClientName', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '公司简称', field: 'ClientShortName', width: '100px' },
                    {
                        title: '客户类型', field: 'ClientType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "个人客户"; }
                            else if (val == "1") { return "月结客户"; }
                            else if (val == "2") { return "VIP客户"; }
                            else if (val == "3") { return "逾期客户"; }
                            else { return "个人客户"; }
                        }
                    },

                    { title: '公司负责人', field: 'Boss', width: '80px' },
                    { title: '手机号码', field: 'Cellphone', width: '100px' },
                    { title: '公司电话', field: 'Telephone', width: '100px' },
                    {
                        title: '公司地址', field: 'Address', width: '200px', formatter: function (val, row, index) {
                            return "<span title='" + row.Province + " " + row.City + " " + row.Country + " " + row.Address + "'>" + row.Province + " " + row.City + " " + row.Country + " " + row.Address + "</span>";
                        }
                    },
                    { title: '透支额度', field: 'LimitMoney', width: '70px', align: 'right' },
                    { title: '目标销量', field: 'TargetNum', width: '70px', align: 'right' },
                    { title: '业务员', field: 'UserName', width: '60px' },
                    { title: '所属仓库', field: 'HouseName', width: '80px' },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                rowStyler: function (index, row) {
                    if (row.ClientType == "3") { return "color:#FF0000"; };
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItem(); }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Client/clientApi.aspx?method=QueryCargoClient';
            $('#dg').datagrid('load', {
                ClientNum: $('#QClientNum').val(),
                ClientName: $("#QClientName").val(),
                ClientShortName: $("#QClientShortName").val(),
                Boss: $("#QBoss").val(),
                AcceptPeople: $("#QAcceptPeople").val(),
                Cellphone: $("#QCellphone").val(),
                HID: "<%= UserInfor.HouseID%>"
            });
            //adjustment();
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
                <td style="text-align: right;">负责人:
                </td>
                <td>
                    <input id="QBoss" class="easyui-textbox" data-options="prompt:'请输入公司负责人'" style="width: 120px">
                </td>
                <td style="text-align: right;">公司全称:
                </td>
                <td>
                    <input id="QClientName" class="easyui-textbox" data-options="prompt:'请输入客户全称'" style="width: 120px">
                </td>
                <td style="text-align: right;">公司简称:
                </td>
                <td>
                    <input id="QClientShortName" class="easyui-textbox" data-options="prompt:'请输入客户简称'" style="width: 120px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="QAcceptPeople" class="easyui-textbox" data-options="prompt:'请输入收货人'" style="width: 120px">
                </td>
                <td style="text-align: right;">客户编码:
                </td>
                <td>
                    <input id="QClientNum" class="easyui-numberbox" data-options="prompt:'请输入客户编码'" style="width: 120px">
                </td>
                <td style="text-align: right;">手机号码:
                </td>
                <td>
                    <input id="QCellphone" class="easyui-numberbox" data-options="prompt:'请输入手机号码'" style="width: 120px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()" id="btnUpdate">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 250px; padding: 0px 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="ClientID" />
                <table>
                    <tr>
                        <td style="text-align: right;">客户编号:
                        </td>
                        <td>
                            <input name="ClientNum" id="ClientNum" class="easyui-textbox" data-options="required:true" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">客户全称:
                        </td>
                        <td>
                            <input name="ClientName" id="ClientName" class="easyui-textbox" data-options="required:true" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">目标条数:
                        </td>
                        <td>
                            <input name="TargetNum" id="TargetNum" class="easyui-numberbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">透支额度:
                        </td>
                        <td>
                            <input type="hidden" id="MaxLimitMoney" />
                            <input name="LimitMoney" id="LimitMoney" class="easyui-numberbox" data-options="min:0,precision:3 " style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" id="LabText"></td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <script type="text/javascript">
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $.ajax({
                    url: "clientApi.aspx?method=GetAvailableLimit&ClientNum=" + row.ClientNum,
                    cache: false,
                    async: true,
                    dataType: "json",
                    success: function (text) {
                        $.messager.progress("close");
                        debugger;
                        if (text <= 0) {
                            //$('#LimitMoney').numberbox('readonly', true);
                            $("#LabText").html("当前可调整数量为：0");
                            $('#LimitMoney').numberbox('textbox').css('background-color', '#e8e8e8');
                            $('#MaxLimitMoney').val(parseFloat(row.LimitMoney));
                        } else {
                            //$('#LimitMoney').numberbox('readonly', false);
                            $("#LabText").html("当前可调整数量为：" + text + "，总额不得超过：" + parseFloat(parseFloat(row.LimitMoney) + parseFloat(text)));
                            $('#LimitMoney').numberbox('textbox').css('background-color', 'white');
                            $('#MaxLimitMoney').val(parseFloat(row.LimitMoney) + parseFloat(text));
                        }
                    }
                });
                $('#fm').form('clear');
                $('#dlg').dialog('open').dialog('setTitle', '修改客户信息');
                $('#fm').form('load', row);
                $('#ClientNum').combobox('readonly', true);
                $('#ClientName').combobox('readonly', true);
                $('#ClientNum').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ClientName').combobox('textbox').css('background-color', '#e8e8e8');
                $.messager.progress({ msg: '请稍后,正在查询可调整数量...' });
            }
        }
        //保存客户信息
        function saveItem() {
            if (parseFloat($('#LimitMoney').numberbox('getValue')) > parseFloat($('#MaxLimitMoney').val())) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '透支额度调整过高', 'warning');
                return;
            }
            $('#fm').form('submit', {
                url: 'clientApi.aspx?method=UpdateClentMoney',
                onSubmit: function (param) {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#dg').datagrid('reload');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>
</asp:Content>
