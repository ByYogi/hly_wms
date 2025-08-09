<%@ Page Title="城市管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CityManage.aspx.cs" Inherits="Cargo.systempage.CityManage" %>

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
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'CID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'CID', checkbox: true, width: '30px' },
                  { title: '城市名称', field: 'CityName', width: '60px' },
                  //{ title: '城市代码', field: 'CityCode', width: '60px' },
                  {
                      title: '状态', field: 'DelFlag', width: '40px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "启用"; }
                          else if (val == "1") { return "停用"; }
                          else { return "启用"; }
                      }
                  },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter },
                  {
                      field: 'opt', title: '操作', width: 120, align: 'left',
                      formatter: function (value, rec, index) {
                          var btn = '<a class="editcls" onclick="editItemByID(\'' + index + '\')" href="javascript:void(0)">编辑</a><a class="delcls" onclick="delItemByID(\'' + index + '\')" href="javascript:void(0)">删除</a>';
                          return btn;
                      }
                  }
                ]],
                onLoadSuccess: function (data) {
                    $('.editcls').linkbutton({ text: '编辑', plain: true, iconCls: 'icon-edit' });
                    $('.delcls').linkbutton({ text: '删除', plain: true, iconCls: 'icon-cut' });
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../systempage/sysService.aspx?method=QueryCity';
            $('#dg').datagrid('load', {
                CityName: $('#CityName').val(),
                CityCode: $("#CityCode").val(),
                dFlag: $("#dFlag").combobox('getValue')
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
                <td style="text-align: right; width: 60px;">
                    城市名称:
                </td>
                <td>
                    <input id="CityName" class="easyui-textbox" data-options="prompt:'请输入城市名称'" style="width: 100px">
                </td>
                <%--<td style="text-align: right; width: 60px;">
                    城市代码:
                </td>
                <td>
                    <input id="CityCode" class="easyui-textbox" data-options="prompt:'请输入城市代码'" style="width: 100px">
                </td>--%>
                <td style="text-align: right; width: 60px;">
                    状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="dFlag" style="width: 70px;" panelheight="auto">
                        <option value="0">启用</option>
                        <option value="1">停用</option>
                        <option value="-1">全部</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">
            &nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add"
                plain="false" onclick="addItem()"> &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#"
                    class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a
                        href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 10px 10px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
        <input type="hidden" name="CID" />
        <table>
            <tr>
                <td style="text-align: right;">
                    城市名称:
                </td>
                <td>
                    <input name="CityName" id="City" class="easyui-validatebox" data-options="required:true">
                </td>
            </tr>
           <%-- <tr>
                <td style="text-align: right;">
                    城市代码:
                </td>
                <td>
                    <input name="CityCode" id="Code" class="easyui-validatebox" data-options="required:true,validType:['maxLength[2]','ENG']">
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: right;">
                    状态标识:
                </td>
                <td>
                    <select class="easyui-combobox" id="DelFlag" name="DelFlag" style="width: 70px;"
                        panelheight="auto" required="true">
                        <option value="0">启用</option>
                        <option value="1">停用</option>
                    </select>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">
            取消</a>
    </div>

    <script type="text/javascript">
        //新增城市名称
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增城市名称');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
            $("#City").attr('readonly', false);
            //$("#Code").attr('readonly', false);
        }
        //修改城市名称
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改城市名称');
                $('#fm').form('load', row);
                if (row["DelFlag"] == "1") {
                    $("#City").attr('readonly', true);
                    //$("#Code").attr('readonly', true);
                }
                else {
                    $("#City").attr('readonly', false);
                    //$("#Code").attr('readonly', false);
                }
            }
        }
        //修改城市名称
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改城市名称');
                $('#fm').form('load', row);
                if (row["DelFlag"] == "1") {
                    $("#City").attr('readonly', true);
                    //$("#Code").attr('readonly', true);
                }
                else {
                    $("#City").attr('readonly', false);
                    //$("#Code").attr('readonly', false);
                }
            }
        }
        //删除城市名称
        function delItemByID(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: '../systempage/sysService.aspx?method=DelCity',
                            type: 'post',
                            dataType: 'json',
                            data: { data: json },
                            success: function (text) {
                                //var res = eval('(' + text + ')');
                                if (text.Result == true) {
                                    $('#dg').datagrid('reload');
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                }
                            }
                        });
                    }
                });
            }
            else {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
            }
        }

        //删除城市名称
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
                        url: '../systempage/sysService.aspx?method=DelCity',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
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

        //保存城市名称
        function saveItem() {
            $('#fm').form('submit', {
                url: '../systempage/sysService.aspx?method=SaveCity',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
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
