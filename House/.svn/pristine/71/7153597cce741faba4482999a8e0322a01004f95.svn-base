<%@ Page Title="日志管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemLog.aspx.cs" Inherits="House.SystemLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                height: '400px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'BID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '用户名', field: 'UserID', width: '60px' },
                  { title: '登陆IP', field: 'IPAddress', width: '60px' },
                   {
                       title: '操作类型', field: 'Operate', width: '60px',
                       formatter: function (val, row, index) {
                           if (val == "Q") { return "查询"; }
                           else if (val == "A") { return "新增"; }
                           else if (val == "D") { return "删除"; }
                           else if (val == "U") { return "修改"; }
                           else if (val == "L") { return "登陆"; }
                           else { return "查询"; }
                       }
                   },
                  { title: '模块名', field: 'Moudle', width: '100px' },
                  { title: '界面名', field: 'NvgPage', width: '100px' },
                  { title: '日志内容', field: 'Memo', width: '600px' },
                  {
                      title: '日志状态', field: 'Status', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "成功"; }
                          else if (val == "1") { return "失败"; }
                          else { return "成功"; }
                      }
                  },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=LogQuery';
            $('#dg').datagrid('load', {
                UserID: $('#UserID').val(),
                Moudle: $('#Moudle').val(),
                MainKey: $("#MainKey").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                OperType: $("#OperType").combobox('getValue'),
                status: $("#status").combobox('getValue')
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
                        <li class="active">
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
                    <li class="active">日志管理</li>
                </ul>
            </div>

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">
                <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
                    <table>
                        <tr>
                            <td style="text-align: right;">用户名:
                            </td>
                            <td>
                                <input id="UserID" class="easyui-textbox" data-options="prompt:'请输入用户名'" style="width: 100px">
                            </td>
                               <td style="text-align: right;">模块:
                            </td>
                            <td>
                                <input id="Moudle" class="easyui-textbox" data-options="prompt:'请输入模块名'" style="width: 100px">
                            </td>
                            <td style="text-align: right;">操作类型:
                            </td>
                            <td>
                                <select class="easyui-combobox" id="OperType" style="width: 60px;" panelheight="auto">
                                    <option value="-1">全部</option>
                                    <option value="Q">查询</option>
                                    <option value="A">新增</option>
                                    <option value="D">删除</option>
                                    <option value="U">修改</option>
                                    <option value="L">登陆</option>
                                </select>
                            </td>
                            <td style="text-align: right;">状态:
                            </td>
                            <td>
                                <select class="easyui-combobox" id="status" style="width: 60px;" panelheight="auto">
                                    <option value="-1">全部</option>
                                    <option value="0">成功</option>
                                    <option value="1">失败</option>
                                </select>
                            </td>
                            <td style="text-align: right;">时间范围:
                            </td>
                            <td>
                                <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px">
                            </td>
                            <td style="text-align: right;">关键字:
                            </td>
                            <td>
                                <input id="MainKey" class="easyui-textbox" data-options="prompt:'请输入关键字'" style="width: 150px">
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="dg" class="easyui-datagrid">
                </table>
                <div id="toolbar">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                </div>
                <div id="dlg" class="easyui-dialog" style="width: 550px; height: 420px; padding: 10px 10px"
                    closed="true" buttons="#dlg-buttons">
                    <form id="fm" class="easyui-form" method="post">
                        <input type="hidden" name="Batch_ID" />
                        <table>
                            <tr>
                                <td style="text-align: right;">用户名:
                                </td>
                                <td>
                                    <input name="UserID" class="easyui-textbox">
                                </td>
                                <td style="text-align: right;">IP地址:
                                </td>
                                <td>
                                    <input name="IPAddress" class="easyui-textbox">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">操作时间:
                                </td>
                                <td>
                                    <input name="OP_DATE" class="easyui-datetimebox" data-options="formatter:AllDateTime">
                                </td>
                                <td style="text-align: right;">操作类型:
                                </td>
                                <td>
                                    <select class="easyui-combobox" id="Operate" name="Operate" style="width: 70px;"
                                        panelheight="auto">
                                        <option value="Q">查询</option>
                                        <option value="A">新增</option>
                                        <option value="D">删除</option>
                                        <option value="U">修改</option>
                                        <option value="L">登陆</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">模块名:
                                </td>
                                <td>
                                    <input name="Moudle" class="easyui-textbox">
                                </td>
                                <td style="text-align: right;">界面名:
                                </td>
                                <td>
                                    <input name="NvgPage" class="easyui-textbox">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">日志内容:
                                </td>
                                <td colspan="3">
                                    <textarea rows="10" name="Memo" style="width: 400px;"></textarea>
                                </td>
                            </tr>
                        </table>
                    </form>
                </div>
                <div id="dlg-buttons">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">关闭</a>
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
        //查看日志
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '查看系统日志');
                row.OP_DATE = AllDateTime(row.OP_DATE);
                $('#fm').form('load', row);
            }
        }
    </script>

</asp:Content>
