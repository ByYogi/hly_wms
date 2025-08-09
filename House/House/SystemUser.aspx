<%@ Page Title="用户管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemUser.aspx.cs" Inherits="House.SystemUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'UserID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'UserID', checkbox: true, width: '30px' },
                  { title: '系统登陆名', field: 'LoginName', width: '80px' },
                  { title: '员工姓名', field: 'UserName', width: '60px' },
                  { title: '性别', field: 'Sex', width: '30px', formatter: function (val, row, index) { if (val == "0") { return "男"; } else { return "女"; } } },
                  { title: '年龄', field: 'Age', width: '30px' },
                  //{ title: '身份证号', field: 'UserIDNum', width: '150px' },
                  { title: '所属角色', field: 'RoleCName', width: '120px' },
                  { title: '所属部门', field: 'DepCName', width: '80px' },
                  { title: '手机号码', field: 'CellPhone', width: '100px' },
                  { title: 'Email', field: 'Email', width: '100px' },
                  { title: '住址电话', field: 'AddressPhone', width: '100px' },
                  { title: '所在岗位', field: 'UserJob', width: '80px' },
                  { title: '所属仓库', field: 'HouseName', width: '80px' },
                  { title: '仓库权限', field: 'CargoPermisName', width: '300px' },
                  { title: '状态', field: 'DelFlag', width: '40px', formatter: function (val, row, index) { if (val == "0") { return "启用"; } else if (val == "1") { return "停用"; } else { return "启用"; } } },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            $('#dFlag').combobox('textbox').bind('focus', function () { $('#dFlag').combobox('showPanel'); });
            $('#RoleID').combobox('textbox').bind('focus', function () { $('#RoleID').combobox('showPanel'); });
            $('#Sex').combobox('textbox').bind('focus', function () { $('#Sex').combobox('showPanel'); });
            $('#ARoleID').combobox('textbox').bind('focus', function () { $('#ARoleID').combobox('showPanel'); });
            $('#DelFlag').combobox('textbox').bind('focus', function () { $('#DelFlag').combobox('showPanel'); });

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=QueryUsers';
            $('#dg').datagrid('load', {
                LoginName: $('#LoginName').val(),
                UserName: $('#UserName').val(),
                DepID: $("#DepID").combobox('getValue'),
                RoleID: $("#RoleID").combobox('getValue'),
                CellPhone: $('#CellPhone').val(),
                dFlag: $("#dFlag").combobox('getValue')
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-container" id="main-container">
        <script type="text/javascript">
            try { ace.settings.check('main-container', 'fixed') } catch (e) { }
        </script>
        <!-- #section:basics/sidebar -->
        <div id="sidebar" class="sidebar                  responsive">
            <script type="text/javascript">
                try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
            </script>
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
                        <li class="">
                            <a href="SystemOrganize.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                组织架构
                            </a>

                            <b class="arrow"></b>
                        </li>
                        <%-- <li class="">
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
                        </li>--%>

                        <li class="">
                            <a href="SystemRole.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                角色管理
                            </a>

                            <b class="arrow"></b>
                        </li>

                        <li class="active">
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
                          <li class="">
                            <a href="SystemLog.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                日志管理
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
                    <li class="active">用户管理</li>
                </ul>
            </div>

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">

                <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
                    <table>
                        <tr>
                            <td style="text-align: right; width: 60px;">登陆名:
                            </td>
                            <td>
                                <input id="LoginName" class="easyui-textbox" data-options="prompt:'请输入用户登陆名'" style="width: 120px">
                            </td>
                            <td style="text-align: right; width: 60px;">姓名:
                            </td>
                            <td>
                                <input id="UserName" class="easyui-textbox" data-options="prompt:'请输入用户姓名'" style="width: 120px">
                            </td>
                            <td style="text-align: right; width: 60px;">手机号码:
                            </td>
                            <td>
                                <input id="CellPhone" class="easyui-textbox" data-options="prompt:'请输入手机号码'" style="width: 120px">
                            </td>
                            <td style="text-align: right; width: 60px;">所在部门:
                            </td>
                            <td>
                                <input id="DepID" class="easyui-combotree" data-options="url:'sysService.aspx?method=QueryAllOrganize'" style="width: 200px;">
                            </td>
                            <td style="text-align: right; width: 60px;">所属角色:
                            </td>
                            <td>
                                <input id="RoleID" class="easyui-combobox" style="width: 100px;" panelheight="auto"
                                    data-options="url:'sysService.aspx?method=QueryALLRole',textField:'CName',valueField:'RoleID'" />
                            </td>

                            <td style="text-align: right; width: 60px;">状态:
                            </td>
                            <td>
                                <select class="easyui-combobox" id="dFlag" style="width: 100px;" panelheight="auto">
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add"
                        plain="false" onclick="addItem()"> &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#"
                            class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a
                                href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
                </div>
                <div id="dlg" class="easyui-dialog" style="width: 600px; height: 450px; padding: 10px 10px"
                    closed="true" buttons="#dlg-buttons">
                    <form id="fm" class="easyui-form" method="post">
                        <input type="hidden" name="UserID" />
                        <input type="hidden" name="BelongSystemName" id="BelongSystemName" />
                        <table>
                            <tr>
                                <td style="text-align: right;">系统登陆名:
                                </td>
                                <td>
                                    <input name="LoginName" id="ALoginName" class="easyui-textbox" data-options="prompt:'请输入系统登陆名',required:true"
                                        style="width: 200px;">
                                </td>
                                <td style="text-align: right;">真实姓名:
                                </td>
                                <td>
                                    <input name="UserName" class="easyui-textbox" data-options="prompt:'请输入真实姓名',required:true"
                                        style="width: 200px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">身份证号码:
                                </td>
                                <td>
                                    <input name="UserIDNum" class="easyui-textbox"
                                        style="width: 200px;">
                                </td>
                                <td style="text-align: right;">登陆密码:
                                </td>
                                <td>
                                    <input name="LoginPwd" id="LoginPwd" type="password" class="easyui-textbox" data-options="prompt:'请输入系统登陆密码'"
                                        style="width: 200px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">性别:
                                </td>
                                <td>
                                    <select class="easyui-combobox" id="Sex" name="Sex" style="width: 200px;" panelheight="auto"
                                        required="true">
                                        <option value="0">男</option>
                                        <option value="1">女</option>
                                    </select>
                                </td>
                                <td style="text-align: right;">年龄:
                                </td>
                                <td>
                                    <input id="Age" name="Age" class="easyui-numberspinner" data-options="min:18,max:100,required:true"
                                        style="width: 200px;" />
                                </td>
                            </tr>
                            <tr>

                                <td style="text-align: right;">所属部门:
                                </td>
                                <td>
                                    <input id="Did" name="DepID" class="easyui-combotree" data-options="url:'sysService.aspx?method=QueryAllOrganize'" style="width: 200px;">
                                </td>
                                <td style="text-align: right;">所属角色:
                                </td>
                                <td>
                                    <input name="RoleID" id="ARoleID" class="easyui-combobox" style="width: 200px;" panelheight="auto"
                                        data-options="url:'/sysService.aspx?method=QueryALLRole',textField:'CName',valueField:'RoleID'" />
                                </td>
                            </tr>
                            <tr>

                                <td style="text-align: right;">所在岗位:
                                </td>
                                <td>
                                    <input name="UserJob" class="easyui-textbox" style="width: 200px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">公司电话:
                                </td>
                                <td>
                                    <input name="CompanyPhone" class="easyui-textbox" style="width: 200px;">
                                </td>
                                <td style="text-align: right;">住址电话:
                                </td>
                                <td>
                                    <input name="AddressPhone" class="easyui-textbox" style="width: 200px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">手机号码:
                                </td>
                                <td>
                                    <input name="CellPhone" id="CellPhone" class="easyui-validatebox" data-options="validType:'mobile'"
                                        style="width: 200px;">
                                </td>
                                <td style="text-align: right;">EMAIL:
                                </td>
                                <td>
                                    <input name="Email" id="Email" class="easyui-textbox"
                                        style="width: 200px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">状态标识:
                                </td>
                                <td>
                                    <select class="easyui-combobox" id="DelFlag" name="DelFlag" style="width: 200px;"
                                        panelheight="auto" required="true">
                                        <option value="0">启用</option>
                                        <option value="1">停用</option>
                                    </select>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align: right;">所属仓库:
                                </td>
                                <td colspan="3">
                                    <input class="easyui-combobox" name="HouseID" id="AHouseID" data-options="url:'../Data/CargoPer.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                                        style="width: 200px;"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">仓库权限:
                                </td>
                                <td colspan="3">
                                    <input class="easyui-combobox" name="CargoPermisID" id="ACargoPermisID" data-options="url:'../Data/CargoPer.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto',multiple:true"
                                        style="width: 450px;"></td>
                            </tr>
                        </table>
                    </form>
                </div>
                <div id="dlg-buttons">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
                </div>

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
    <script type="text/javascript">
        function cmbChange(item) {
            if (item) {
                $('#BelongSystemName').val(item.SystemName);
            }
        }
        //新增用户
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增用户信息');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
            $('#LoginPwd').textbox('enable');
            $('#ALoginName').textbox('readonly', false);
        }
        //修改用户
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            $('#ALoginName').textbox('readonly', true);
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改用户信息：' + row.UserName);
                $('#LoginPwd').textbox('disable');
                $('#fm').form('load', row);
            }
        }
        //修改用户
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#ALoginName').textbox('readonly', true);
                $('#dlg').dialog('open').dialog('setTitle', '修改用户信息：' + row.UserName);
                $('#LoginPwd').textbox('disable');
                $('#fm').form('load', row);
            }
        }
        //删除用户
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= House.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'sysService.aspx?method=DelUsers',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存用户
        function saveItem() {
            $('#fm').form('submit', {
                url: 'sysService.aspx?method=SaveUsers',
                onSubmit: function (param) {
                    param.CargoPermisName = $('#ACargoPermisID').combobox('getText');
                    param.HouseName = $('#AHouseID').combobox('getText');
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>
</asp:Content>
