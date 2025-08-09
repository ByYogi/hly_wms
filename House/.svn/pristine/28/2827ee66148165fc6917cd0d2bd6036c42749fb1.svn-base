<%@ Page Title="会计科目管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinanceSubject.aspx.cs" Inherits="Cargo.systempage.FinanceSubject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
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
            $("#eZID").combobox({
                //相当于html >> select >> onChange事件  
                onSelect: function () {
                        var Name = $('#eZID').combobox('getText');
                        $('#ZName').val(Name);
                        if ($('#editZID').val() == "") {
                        $('#eFID').combobox('clear');
                        $('#eSID').combobox('clear');
                        var url = 'sysService.aspx?method=QueryAllFirstSubject&ZID=' + $('#eZID').combobox('getValue');
                        $('#eFID').combobox('reload', url);
                    }
                }
            });
            $("#eFID").combobox({
                //相当于html >> select >> onChange事件  
                onSelect: function () {
                        var Name = $('#eFID').combobox('getText');
                        $('#FName').val(Name);
                        if ($('#editZID').val() == "") {
                        $('#eSID').combobox('clear');
                        url = 'sysService.aspx?method=QueryAllSecondSubject&FID=' + $('#eFID').combobox('getValue');
                        $('#eSID').combobox('reload', url);
                    }
                },
                onChange: function () {
                    var Name = $('#eFID').combobox('getText');
                    $('#FName').val(Name);
                }
            });
            $("#eSID").combobox({
                //相当于html >> select >> onChange事件  
                onSelect: function () {
                    var Name = $('#eSID').combobox('getText');
                    $('#SName').val(Name);
                },
                onChange: function () {
                    var Name = $('#eSID').combobox('getText');
                    $('#SName').val(Name);
                }
            });
            $("#ZID").combobox({
                onSelect: function () {
                    $('#FID').combobox('clear');
                    url = 'sysService.aspx?method=QueryAllFirstSubject&ZID=' + $('#ZID').combobox('getValue');
                    $('#FID').combobox('reload', url);
                }
            });
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
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'RowNumber',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'RowNumber', checkbox: true, width: '30px' },
                  { title: '一级科目名称', field: 'ZName', width: '120px' },
                  { title: '二级科目名称', field: 'FName', width: '120px' },
                  { title: '三级科目名称', field: 'SName', width: '125px' },
                  { title: '使用次数', field: 'UseCount', width: '30px', hidden: true }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItem(); }
            });

            var url = 'sysService.aspx?method=QueryAllZeroSubject';
            $('#ZID').combobox('reload', url);
            var url = 'sysService.aspx?method=QueryAllFirstSubject&ZID=-1';
            $('#FID').combobox('reload', url);
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=QueryFinance';
            $('#dg').datagrid('load', {
                ZID: $("#ZID").combobox('getValue'),
                FID: $("#FID").combobox('getValue'),
                Name: $("#Name").val()
            });
            adjustment();
        }
        //新增
        function addItem() {
            $('#dlgedit').form('clear');
            $('#dlgedit').dialog('open').dialog('setTitle', '新增会计科目');
            var url = 'sysService.aspx?method=QueryAllZeroSubject';
            $('#eZID').combobox('reload', url);
        }
        //修改
        function editItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#editZID').val(rows[0].ZID);
                $('#dlgedit').dialog('open').dialog('setTitle', '修改会计科目');
                var url = 'sysService.aspx?method=QueryAllZeroSubject';
                $('#eZID').combobox('reload', url);
                url = 'sysService.aspx?method=QueryAllFirstSubject&ZID=' + rows[0].ZID;
                $('#eFID').combobox('reload', url);
                url = 'sysService.aspx?method=QueryAllSecondSubject&FID=' + rows[0].FID;
                $('#eSID').combobox('reload', url);
                $('#fm').form('load', rows[0]);
                if (rows[0].ZID == 0) {
                    $('#OldZID').val(-1);
                    $('#eZID').combobox('clear');
                }else{
                    $('#OldZID').val(rows[0].ZID);
                }
                if (rows[0].FID == 0) {
                    $('#OldFID').val(-1);
                    $('#eFID').combobox('clear');
                } else {
                    $('#OldFID').val(rows[0].FID);
                }
                if (rows[0].SID == 0) {
                    $('#OldSID').val(-1);
                    $('#eSID').combobox('clear');
                } else {
                    $('#OldSID').val(rows[0].SID);
                }
                $('#OldFName').val(rows[0].FName);
                $('#OldSName').val(rows[0].SName);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">一级科目:
                </td>
                <td>
                    <select class="easyui-combobox" id="ZID" style="width: 100px;" panelheight="auto" data-options="valueField:'ZID',textField:'ZName',editable:false">
                    </select>
                </td>
                <td style="text-align: right;">二级科目:
                </td>
                <td>
                    <select class="easyui-combobox" id="FID" style="width: 100px;" panelheight="auto" data-options="valueField:'FID',textField:'FName',editable:false">
                    </select>
                </td>
                <td style="text-align: right; width: 60px;">科目名称:
                </td>
                <td>
                    <input id="Name" class="easyui-textbox" data-options="prompt:'请输入科目名称'" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <%--此div用于显示查询条件--%>
    <table id="dg" class="easyui-datagrid">
    </table>
    <%--此div用于显示按钮操作--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
    </div>
    <%--此div用于显示按钮操作--%>
    <%--此div用于新增/编辑数据--%>
    <div id="dlgedit" class="easyui-dialog" style="width: 300px; height: 200px; padding: 5px 5px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="editZID" name="editZID" />
            <table id="editTab">
                <tr>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td style="text-align: right;">一级科目:
                                </td>
                                <td>
                                    <input type="hidden" name="OldZID" id="OldZID" />
                                    <input type="hidden" name="ZName" id="ZName" />
                                    <input id="eZID" name="ZID" class="easyui-combobox" style="width: 200px;" onSelect="test()" data-options="valueField:'ZID',textField:'ZName',editable:false,required:true" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">二级科目:
                                </td>
                                <td>
                                    <input type="hidden" name="OldFID" id="OldFID" />
                                    <input type="hidden" name="OldFName" id="OldFName" />
                                    <input type="hidden" name="FName" id="FName" />
                                    <input id="eFID" name="FID" class="easyui-combobox" style="width: 200px;" data-options="valueField:'FID',textField:'FName',editable:true,required:true" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">三级科目:
                                </td>
                                <td>
                                    <input type="hidden" name="OldSID" id="OldSID" />
                                    <input type="hidden" name="OldSName" id="OldSName" />
                                    <input type="hidden" name="SName" id="SName" />
                                    <input id="eSID" name="SID" class="easyui-combobox" style="width: 200px;" data-options="valueField:'SID',textField:'SName',editable:true,required:false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgedit').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--此div用于新增/编辑数据--%>
    <script type="text/javascript">
        function test() {
            var Name = $('#eZID').combobox('getText');
            //alert(Name);
        }
        //保存数据
        function saveItem() {
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#fm').form('submit', {
                url: 'sysService.aspx?method=SaveFinanceData',
                onSubmit: function () {
                    var check = $(this).form('enableValidation').form('validate');
                    if (!check) { $.messager.progress("close"); }
                    return check;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgedit').dialog('close');
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }

        //删除数据
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            if (rows) {
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i].SID == 0 && rows[i].FID == 0) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选数据中包含一级科目！</br>只可删除二三级科目', 'warning'); return;
                    }
                    if (rows[i].UseCount > 0) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].SName+'已有报销流程，无法删除！', 'warning'); return;
                    }
                }
                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                    if (r) {
                        var json = JSON.stringify(rows)
                        $.ajax({
                            url: 'sysService.aspx?method=DeleteFinanceData',
                            type: 'post', dataType: 'json', data: { data: json },
                            success: function (text) {
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
        }
    </script>
</asp:Content>
