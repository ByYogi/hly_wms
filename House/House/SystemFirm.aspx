<%@ Page Title="公司管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemFirm.aspx.cs" Inherits="House.SystemFirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'UnitID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'UnitID', checkbox: true, width: '30px' },
                  { title: '单位名称', field: 'CName', width: '120px' },
                  { title: '单位地址', field: 'Address', width: '200px' },
                  { title: '联系人', field: 'Boss', width: '80px' },
                  { title: '联系人手机', field: 'CellPhone', width: '100px' },
                  { title: '单位电话', field: 'phone', width: '100px' },
                  { title: '单位传真', field: 'Fax', width: '100px' },
                  { title: '备注', field: 'Remark', width: '200px' },
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
            gridOpts.url = '../systempage/sysService.aspx?method=QueryUnit';
            $('#dg').datagrid('load', {
                UnitName: $('#UnitName').textbox('getValue'),
                dFlag: $("#dFlag").combobox('getValue')
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-container" id="main-container">
        <!-- #section:basics/sidebar -->
        <div id="sidebar" class="sidebar                  responsive">
            <ul class="nav nav-list">
                <li class="active">
                    <a href="#" class="dropdown-toggle">
                        <i class="menu-icon fa fa-desktop"></i>
                        <span class="menu-text">系统管理 </span>

                        <b class="arrow fa fa-angle-down"></b>
                    </a>

                    <b class="arrow"></b>

                    <ul class="submenu">
                        <li class="">
                            <a href="index.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                我的工作台
                            </a>

                            <b class="arrow"></b>
                        </li>
                        <li class="">
                            <a href="SystemSet.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                系统管理
                            </a>

                            <b class="arrow"></b>
                        </li>
                        <li class="active">
                            <a href="SystemFirm.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                公司管理
                            </a>

                            <b class="arrow"></b>
                        </li>
                        <li class="">
                            <a href="SystemUnit.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                单位管理
                            </a>

                            <b class="arrow"></b>
                        </li>

                        <li class="">
                            <a href="SystemDepart.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                部门管理 
                            </a>

                            <b class="arrow"></b>
                        </li>

                        <li class="">
                            <a href="SystemRole.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                角色管理
                            </a>

                            <b class="arrow"></b>
                        </li>

                        <li class="">
                            <a href="SystemUser.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                用户管理 
                            </a>

                            <b class="arrow"></b>
                        </li>

                        <li class="">
                            <a href="SystemItem.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                导航管理
                            </a>

                            <b class="arrow"></b>
                        </li>


                    </ul>
                </li>

            </ul>
            <!-- /.nav-list -->

            <!-- #section:basics/sidebar.layout.minimize -->
            <div class="sidebar-toggle sidebar-collapse" id="sidebar-collapse">
                <i class="ace-icon fa fa-angle-double-left" data-icon1="ace-icon fa fa-angle-double-left" data-icon2="ace-icon fa fa-angle-double-right"></i>
            </div>

            <!-- /section:basics/sidebar.layout.minimize -->
            <script type="text/javascript">
                try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }
            </script>
        </div>

        <!-- /section:basics/sidebar -->
        <div class="main-content">
            <!-- #section:basics/content.breadcrumbs -->
            <div class="breadcrumbs" id="breadcrumbs">
                <script type="text/javascript">
                    try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                </script>

                <ul class="breadcrumb">
                    <li>
                        <i class="ace-icon fa fa-home home-icon"></i>
                        <a href="../index.aspx">我的工作台</a>
                    </li>
                    <li class="active">公司管理</li>
                </ul>
            </div>

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">
                <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
                    <table>
                        <tr>
                            <td style="text-align: right; width: 60px;">单位名称:
                            </td>
                            <td>
                                <input id="UnitName" class="easyui-textbox" data-options="prompt:'请输入单位名称'" style="width: 150px">
                            </td>
                            <td style="text-align: right; width: 60px;">状态:
                            </td>
                            <td>
                                <select class="easyui-combobox" id="dFlag" style="width: 60px;" panelheight="auto">
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
                </div>
                <div id="dlg" class="easyui-dialog" style="width: 500px; height: 420px; padding: 10px 10px"
                    closed="true" buttons="#dlg-buttons">
                    <form id="fm" class="easyui-form" method="post">
                        <input type="hidden" name="UnitID" />
                        <table>
                            <tr>
                                <td style="text-align: right;">单位名称:
                                </td>
                                <td>
                                    <input name="CName" class="easyui-validatebox" data-options="prompt:'请输入单位名称',required:true"
                                        style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">单位地址:
                                </td>
                                <td>
                                    <input name="Address" class="easyui-textbox" data-options="prompt:'请输入单位地址'" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">单位联系人:
                                </td>
                                <td>
                                    <input name="Boss" class="easyui-combobox" style="width: 250px;" data-options="url: '../systempage/sysService.aspx?method=QueryALLUser',valueField: 'UserName',textField: 'UserName',onSelect:onBossChanged,editable:false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">联系人手机:
                                </td>
                                <td>
                                    <input name="CellPhone" id="CellPhone" class="easyui-textbox" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">单位电话:
                                </td>
                                <td>
                                    <input name="phone" class="easyui-textbox" data-options="prompt:'请输入单位电话'" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">单位传真:
                                </td>
                                <td>
                                    <input name="Fax" class="easyui-textbox" data-options="prompt:'请输入单位传真'" style="width: 250px;">
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: right;">备注:
                                </td>
                                <td>
                                    <textarea id="Remark" rows="3" name="Remark" style="width: 250px;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">状态标识:
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
                </div>

                <script type="text/javascript">
                    //联系人选择后赋值
                    function onBossChanged(item) { if (item) { $('#CellPhone').textbox('setValue', item.CellPhone); } }
                    //新增单位信息
                    function addItem() {
                        $('#dlg').dialog('open').dialog('setTitle', '新增单位信息');
                        $('#fm').form('clear');
                        $('#DelFlag').combobox('select', '0');
                    }
                    //修改单位信息
                    function editItem() {
                        var row = $('#dg').datagrid('getSelected');
                        if (row == null || row == "") {
                            $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                            return;
                        }
                        if (row) {
                            $('#dlg').dialog('open').dialog('setTitle', '修改单位信息');
                            $('#fm').form('load', row);
                        }
                    }
                    //修改单位信息
                    function editItemByID(Did) {
                        var row = $("#dg").datagrid('getData').rows[Did];
                        if (row) {
                            $('#dlg').dialog('open').dialog('setTitle', '修改单位信息');
                            $('#fm').form('load', row);
                        }
                    }
                    //删除单位信息
                    function delItemByID(Did) {
                        var rows = $("#dg").datagrid('getData').rows[Did];
                        if (rows) {
                            $.messager.confirm('<%= Perfor.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: '../systempage/sysService.aspx?method=DelUnit',
                            type: 'post',
                            dataType: 'json',
                            data: { data: json },
                            success: function (text) {
                                //var res = eval('(' + text + ')');
                                if (text.Result == true) {
                                    $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                    $('#dg').datagrid('reload');
                                } else {
                                    $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                }
                            }
                        });
                    }
                });
            }
            else { $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning'); }
        }

        //删除单位信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Perfor.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../systempage/sysService.aspx?method=DelUnit',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //保存单位信息
        function saveItem() {
            $('#fm').form('submit', {
                url: '../systempage/sysService.aspx?method=SaveUnit',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Perfor.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
                </script>

            </div>
            <!-- /.page-content -->
        </div>
        <!-- /.main-content -->


        <div class="footer">
            <div class="footer-inner">
                <!-- #section:basics/footer -->
                <div class="footer-content">
                    <span class="bigger-120">广州好来运速递有限公司 &copy; 2017-2020
                    </span>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
