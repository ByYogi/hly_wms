<%@ Page Title="企业号基础配置" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="qyConfig.aspx.cs" Inherits="Cargo.QY.qyConfig" %>

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
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当标签点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当标签点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ID', checkbox: true, width: '30px' },
                  {
                      title: '企业微信', field: 'QYKind', width: '80px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "迪乐泰"; }
                          if (val == "1") { return "好来运"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '消息类型', field: 'SendType', width: '80px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "消息推送"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '业务分类', field: 'WorkClass', width: '100px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "下单推送"; }
                          else if (val == "1") { return "价格变动"; }
                          else if (val == "2") { return "改价申请"; }
                          else if (val == "3") { return "我的客户订单"; }
                          else { return ""; }
                      }
                  },
                  { title: '应用ID', field: 'AgentID', width: '80px' },
                  { title: '应用Secret', field: 'AppSecret', width: '300px' }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'wxQYServices.aspx?method=QueryQyConfig';
            $('#dg').datagrid('load', {
                WorkClass: $('#WorkClass').textbox('getValue')
            });
            adjustment();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table>
            <tr>
                <td style="text-align: right;">业务分类:
                </td>
                <td>
                    <input class="easyui-combobox" id="WorkClass" data-options="url:'../Data/TagType.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                        style="width: 250px;">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                        iconcls="icon-arrow_refresh" plain="false" onclick="SyncTag()">&nbsp;同&nbsp;步&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 5px 5px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="ID" />
            <table>
                <td style="text-align: right;">企业微信:
                </td>
                <td>
                    <select class="easyui-combobox" name="QYKind" id="QYKind" style="width: 250px;" panelheight="auto">
                        <option value="0">迪乐泰</option>
                        <option value="1">好来运</option>
                    </select>
                </td>
                <tr>
                    <td style="text-align: right;">消息类型:
                    </td>
                    <td>
                        <input class="easyui-combobox" name="SendType" data-options="url:'../Data/SendType.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">业务分类:
                    </td>
                    <td>
                        <input class="easyui-combobox" name="WorkClass" data-options="url:'../Data/TagType.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">推送应用ID:
                    </td>
                    <td>
                        <input name="AgentID" class="easyui-textbox" data-options="prompt:'请输入推送应用ID',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">推送应用Secret:
                    </td>
                    <td>
                        <input name="AppSecret" class="easyui-textbox" data-options="prompt:'请输入推送应用Secret',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>

    <script type="text/javascript">

        //新增信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增微信配置');
            $('#fm').form('clear');

        }
        //修改微信企业标签信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改微信配置');
                $('#fm').form('load', row);
            }
        }
        //修改标签信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改微信配置');
                $('#fm').form('load', row);
            }
        }
        //删除标签信息
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
                        url: 'wxQYServices.aspx?method=DelQYTag',
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

        //保存信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'wxQYServices.aspx?method=SaveQYConfig',
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
