<%@ Page Title="修改用户密码" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="House.UpdatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var IsAdmin = "<%= IsAdmin%>";
            if (IsAdmin == "False") {
                $('#LoginName').textbox('readonly');
            }
            else {
                $('#LoginName').textbox('readonly', false);
            }
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

                <div style="height: 100%">
                    <div id="saPanel" class="easyui-panel" title="修改密码" data-options="iconCls:'icon-eye',collapsible:true,fit:true"
                        style="height: 100%;">
                        <form id="fm" class="easyui-form" method="post">
                            <table>
                                <tr>
                                    <td style="text-align: right;">用户登陆名:
                                    </td>
                                    <td>
                                        <input id="LoginName" name="LoginName" class="easyui-textbox" data-options="prompt:'请输入用户登陆名',required:true,iconCls:'icon-man'"
                                            value="<%=UserInfor.LoginName %>" style="width: 200px; height: 35px; padding: 12px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">新密码:
                                    </td>
                                    <td>
                                        <input id="Password" name="Password" class="easyui-textbox" type="password" style="width: 200px; height: 35px; padding: 12px"
                                            data-options="iconCls:'icon-lock',required:true">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">确认新密码:
                                    </td>
                                    <td>
                                        <input id="NewPwd" name="NewPwd" class="easyui-textbox" type="password" style="width: 200px; height: 35px; padding: 12px"
                                            data-options="iconCls:'icon-lock',required:true">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="false" onclick="saveItem()">&nbsp;确&nbsp;定&nbsp;</a>
                                    </td>
                                </tr>
                            </table>
                        </form>
                    </div>
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
        //修改密码
        function saveItem() {
            $('#fm').form('submit', {
                url: 'sysService.aspx?method=UpdateUserPwd',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result == true) { $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '修改密码成功，请妥善保存密码!', 'info'); } else {
                        $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '修改密码失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>

</asp:Content>
