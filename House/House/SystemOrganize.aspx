<%@ Page Title="组织架构管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemOrganize.aspx.cs" Inherits="House.SystemOrganize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tg').treegrid({
                url: 'sysService.aspx?method=SystemOrganizeQuery',
                idField: 'ID',
                treeField: 'Name',
                iconCls: 'icon-ok',
                rownumbers: true,
                animate: true,
                collapsible: true,
                fitColumns: true,
                method: 'post',
                //onContextMenu: onContextMenu,
                toolbar: '#toolbar',
                columns: [[
                    { title: '架构名称', field: 'Name', width: 180 },
                    { title: '架构代码', field: 'Code', width: 60, },
                    { title: '负责人', field: 'Boss', width: 80 },
                { title: '备注', field: 'Remark', width: 150 }
                    //{ title: '排序', field: 'Sort', width: 80 },
                    //{ title: '创建时间', field: 'OP_DATE', width: 80 }
                ]]
            });
        });
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
                        <li class="active">
                            <a href="SystemOrganize.aspx">
                                <i class="menu-icon fa fa-caret-right"></i>
                                组织架构
                            </a>

                            <b class="arrow"></b>
                        </li>
                        <%--<li class="">
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
                    <li class="active">组织架构</li>
                </ul>
            </div>

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">
                <table id="tg" class="easyui-treegrid" style="width: 100%; height: 600px">
                </table>
                <div id="toolbar">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="collapse()">&nbsp;折&nbsp;叠&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="expand()">&nbsp;展&nbsp;开&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
                </div>
                <div id="dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 10px 10px"
                    closed="true" buttons="#dlg-buttons">
                    <form id="fm" class="easyui-form" method="post">
                        <input type="hidden" name="ID" />
                        <input type="hidden" name="PID" id="PID" />
                        <table>
                            <tr>
                                <td style="text-align: right;">组织架构名称:
                                </td>
                                <td>
                                    <input name="Name" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">组织架构代码:
                                </td>
                                <td>
                                    <input name="Code" id="Code" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">负责人:
                                </td>
                                <td>
                                    <input name="Boss" class="easyui-textbox" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">备注:
                                </td>
                                <td>
                                    <textarea id="Remark" rows="3" name="Remark" style="width: 250px;"></textarea>
                                </td>
                            </tr>

                        </table>
                    </form>
                </div>
                <div id="dlg-buttons">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">保存</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
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
        //新增组织架构
        function addItem() {
            var row = $('#tg').treegrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请先选择组织架构！', 'warning');
                return;
            }
            $('#dlg').dialog('open').dialog('setTitle', '新增组织架构');
            $('#fm').form('clear');
            $('#PID').val(row.ID);
            $('#Code').textbox('readonly', false);
            $('#Code').textbox('setValue', row.Code);
        }
        //修改组织架构
        function editItem() {
            $('#fm').form('clear');
            var row = $('#tg').treegrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请先选择要修改的组织架构！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改组织架构：' + row.Name);
                $('#fm').form('load', row);
                $('#PID').val(row.ParentID);
                $('#Code').textbox('readonly', true);
            }
        }
        //删除组织架构
        function DelItem() {
            var rows = $('#tg').treegrid('getSelected');
            if (rows == null || rows == "") {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= House.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify([rows])
                    $.ajax({
                        url: 'sysService.aspx?method=SystemOrganizeDel',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $('#tg').treegrid('reload');
                            }
                            else {
                                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //保存导航链接
        function saveItem() {
            $('#fm').form('submit', {
                url: 'sysService.aspx?method=SystemOrganizeSave',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#tg').treegrid('reload');
                    } else {
                        $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>
</asp:Content>
