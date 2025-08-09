<%@ Page Title="企业号部门管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="qyDepart.aspx.cs" Inherits="Cargo.QY.qyDepart" %>

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
            $('#tg').datagrid({ height: height });
        }
        $(document).ready(function () {
            //$('#dg').datagrid({
            //    width: '100%',
            //    title: '', //标题内容
            //    loadMsg: '数据加载中请稍候...',
            //    autoRowHeight: false, //行高是否自动
            //    collapsible: true, //是否可折叠
            //    pagination: true, //分页是否显示
            //    pageSize: 12, //每页多少条
            //    pageList: [12, 20],
            //    fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
            //    singleSelect: false, //设置为 true，则只允许选中一行。
            //    checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
            //    idField: 'Id',
            //    url: null,
            //    toolbar: '#toolbar',
            //    columns: [[
            //      { title: '', field: 'Id', checkbox: true, width: '30px' },
            //      { title: '部门名称', field: 'Name', width: '150px' },
            //      { title: '父ID', field: 'Parentid', width: '80px' }
            //    ]],
            //    onLoadSuccess: function (data) { },
            //    onDblClickRow: function (index, row) { editItemByID(index); }
            //});
            $('#tg').treegrid({
                url: 'wxQYServices.aspx?method=queryDepart',
                idField: 'ID', treeField: 'Name', iconCls: 'icon-ok', rownumbers: true, animate: true, collapsible: true, fitColumns: true,
                method: 'post',
                //onContextMenu: onContextMenu,
                toolbar: '#toolbar',
                columns: [[
                    { title: '部门名称', field: 'Name', width: 180 },
                    { title: '部门负责人', field: 'Boss', width: 80 },
                    { title: '分管领导', field: 'Leader', width: 80 }
                ]],
                onLoadSuccess: function (data) {
                    $('#tg').treegrid('expandAll');
                },
                onDblClickRow: function (row) {
                    editItemByID(row);
                }
            });
        });
        //查询
        function dosearch() {
            var gridOpts = $('#tg').treegrid('options');
            gridOpts.url = 'wxQYServices.aspx?method=queryDepart';
            $('#tg').treegrid('load');
            adjustment();
            //$('#dg').datagrid('clearSelections');
            //var gridOpts = $('#dg').datagrid('options');
            //gridOpts.url = 'wxQYServices.aspx?method=queryDepart';
            //$('#dg').datagrid('load', {
            //    Name: $('#Name').textbox('getValue')
            //});
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
   <%-- <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">部门名称:
                </td>
                <td>
                    <input id="Name" class="easyui-textbox" data-options="prompt:'请输入仓库名称'" style="width: 150px">
                </td>
            </tr>
        </table>
    </div>--%>
    <%--<table id="dg" class="easyui-datagrid">
    </table>--%>
    <table id="tg" class="easyui-treegrid" style="width: 100%; height: 600px">
    </table>
    <div id="toolbar" name="SelectDiv1">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="collapse()">&nbsp;折&nbsp;叠&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="expand()">&nbsp;展&nbsp;开&nbsp;</a>&nbsp;&nbsp;
        <%--<a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>--%>
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_refresh" plain="false" onclick="SyncDepart()">&nbsp;同&nbsp;步&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 350px; padding: 5px 5px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="ID" id="Departid" />
            <input type="hidden" name="PId" id="PId" />
            <table>
                <tr>
                    <td style="text-align: right;">部门名称:
                    </td>
                    <td>
                        <input name="Name" class="easyui-textbox" data-options="prompt:'请输入部门名称',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr style="display:none">
                    <td style="text-align: right;">负责人:
                    </td>
                    <td>
                        <input type="hidden" id="hidBossID" />
                        <input id="BossID" name="BossID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'UserID',textField:'WxName',onSelect: onBossIDChanged,editable:true,required:false" editable="false" />
                    </td>
                </tr>
                <tr style="display:none">
                    <td style="text-align: right;">分管领导:
                    </td>
                    <td>
                        <input type="hidden" id="hidLeaderID" />
                        <input id="LeaderID" name="LeaderID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'UserID',textField:'WxName',onSelect: onLeaderIDChanged,editable:true,required:false" editable="false" />
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

        function collapse() {
            var node = $('#tg').treegrid('getSelected');
            if (node) {
                $('#tg').treegrid('collapse', node.ID);
            }
        }
        function expand() {
            var node = $('#tg').treegrid('getSelected');
            if (node) {
                $('#tg').treegrid('expand', node.ID);
            }
        }

        //选择负责人
        function onBossIDChanged(item) {
            if (item) {
                $('#hidBossID').val(item.UserID);
            }
        }
        //选择分管领导
        function onLeaderIDChanged(item) {
            if (item) {
                $('#hidLeaderID').val(item.UserID);
            }
        }
        //同步通讯录
        function SyncDepart() {
            $.ajax({
                url: 'wxQYServices.aspx?method=SyncDepart',
                type: 'post', dataType: 'json', data: {},
                success: function (text) {
                    //var result = eval('(' + msg + ')');
                    if (text.Result == true) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '同步成功!', 'info');
                        dosearch();
                    }
                    else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                    }
                }
            });
        }
        //新增部门信息
        function addItem() {
            var row = $('#tg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要一个部门！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '新增部门');
                $('#fm').form('clear');
                $('#PId').val(row.ID);
            }
        }
        //修改部门信息
        function editItem() {
            $('#fm').form('clear');
            var row = $('#tg').treegrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先选择要修改的组织架构！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改部门');
                $('#fm').form('load', row);
                $('#PId').val(row.Parentid);
                $('#Departid').val(row.ID);
                var url = 'wxQYServices.aspx?method=QueryDepartList&Department=' + row.ID;
                $('#BossID').combobox('reload', url);
                $('#LeaderID').combobox('reload', url);
            }
        }
        //修改部门信息
        function editItemByID(row) {
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改部门');
                $('#fm').form('load', row);
                $('#PId').val(row.Parentid);
            }
        }
        //删除部门信息
        function DelItem() {
            var rows = $('#tg').treegrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'wxQYServices.aspx?method=DelQYDepart',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                dosearch();
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
                url: 'wxQYServices.aspx?method=SaveQYDepart',
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
