<%@ Page Title="角色管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemRole.aspx.cs" Inherits="House.SystemRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var hList;
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
                idField: 'RoleID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'RoleID', checkbox: true, width: '30px' },
                  { title: '角色名称', field: 'CName', width: '120px' },
                  { title: '角色备注', field: 'Remark', width: '150px' },
                  {
                      title: '是否管理员', field: 'IsAdmin', width: '80px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "否"; }
                          else if (val == "1") { return "是"; }
                          else { return "否"; }
                      }
                  },
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
                      field: 'opt', title: '操作', width: '100px', align: 'left',
                      formatter: function (value, rec, index) {
                          var btn = '<a class="setcls" onclick="setPermison(\'' + index + '\')" href="javascript:void(0)">设置权限</a>';
                          return btn;
                      }
                  }
                ]],
                onLoadSuccess: function (data) {
                    $('.setcls').linkbutton({ text: '设置权限', plain: false, iconCls: 'icon-bullet_wrench' });
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            $('#dFlag').combobox('textbox').bind('focus', function () { $('#dFlag').combobox('showPanel'); });
            queryAllSet();
        });
        function queryAllSet() {
            $.ajax({
                url: 'sysService.aspx?method=QueryALLHouse',
                cache: false, dataType: 'text', success: function (data) {
                    hList = data;
                }
            });
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=QueryRoles';
            $('#dg').datagrid('load', {
                RoleName: $('#RoleName').val(),
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

                        <li class="active">
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
                    <li class="active">角色管理</li>
                </ul>
            </div>

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">

                <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
                    <table>
                        <tr>
                            <td style="text-align: right;">角色名称:
                            </td>
                            <td>
                                <input id="RoleName" class="easyui-textbox" data-options="prompt:'请输入角色名称'">
                            </td>
                            <td style="text-align: right;">状态:
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add"
                        plain="false" onclick="addItem()"> &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#"
                            class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a
                                href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
                </div>
                <div id="dlg" class="easyui-dialog" style="width: 500px; height: 350px; padding: 10px 10px"
                    closed="true" buttons="#dlg-buttons">
                    <form id="fm" class="easyui-form" method="post">
                        <input type="hidden" name="RoleID" id="RoleID" />
                        <table>
                            <tr>
                                <td style="text-align: right;">角色名称:
                                </td>
                                <td>
                                    <input name="CName" class="easyui-validatebox" data-options="prompt:'请输入角色名称',required:true"
                                        style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">是否管理员:
                                </td>
                                <td>
                                    <input name="IsAdmin" type="radio" id="dlt1" value="0" /><label for="dlt1" style="font-size: 15px;">否</label>
                                    <input name="IsAdmin" type="radio" id="dlt2" value="1" /><label for="dlt2" style="font-size: 15px;">管理员</label>
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
                                    <select class="easyui-combobox" id="DelFlag" name="DelFlag" style="width: 250px;"
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

                <div id="plg" class="easyui-dialog" style="width: 880px; height: 540px; padding: 3px 3px"
                    closed="true" buttons="#plg-buttons">
                    <div id="tabs1" class="easyui-tabs" data-options="fit:true">
                    </div>
                </div>


                <div id="plg-buttons">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="savePermission()">保存</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#plg').dialog('close')">取消</a>
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
        //保存角色权限
        function savePermission() {
            var roleID = $('#RoleID').attr('value');
            $.ajax({
                url: "sysService.aspx?method=SaveRoleItem&RID=" + roleID + "&pnodes=" + GetNode("fnode") + "&cnodes=" + GetNode("child"),
                cache: false, type: "post",
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#plg').dialog('close'); 	// close the dialog
                    } else {
                        $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            });
        }
        function GetNode(type) {
            var cnodes = '';
            var pnodes = '';
            var prevNode = ''; //保存上一步所选父节点

            var res = eval('(' + hList + ')');
            for (var j = 0; j < res.length; j++) {
                var node = $('#houseTree' + res[j].HouseID).tree('getChecked');
                for (var i = 0; i < node.length; i++) {
                    if ($('#houseTree' + res[j].HouseID).tree('isLeaf', node[i].target)) {
                        cnodes += node[i].id + ',';
                        var pnode = $('#houseTree' + res[j].HouseID).tree('getParent', node[i].target); //获取当前节点的父节点
                        if (prevNode != pnode.id) //保证当前父节点与上一次父节点不同
                        {
                            pnodes += pnode.id + ',';
                            prevNode = pnode.id; //保存当前节点
                        }
                    }
                }
            }

            cnodes = cnodes.substring(0, cnodes.length - 1);
            pnodes = pnodes.substring(0, pnodes.length - 1);
            if (type == 'child') { return cnodes; }
            else { return pnodes };
        };
        //设置权限
        function setPermison(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#plg').dialog('open').dialog('setTitle', '设置权限信息');
                $("#RoleID").val(row.RoleID);
                var items;
                $.ajax({
                    url: 'sysService.aspx?method=GetItemByRoleID&id=' + row.RoleID,
                    cache: false, dataType: 'text', async: false, success: function (data) {
                        items = data;
                    }
                })

                var res = eval('(' + hList + ')');
                for (var i = 0; i < res.length; i++) {
                    var hid = res[i].HouseID;
                    if (!$('#tabs1').tabs('exists', res[i].HouseName)) {
                        $('#tabs1').tabs('add', {
                            title: res[i].HouseName,
                            iconCls: 'icon-monitor',
                            content: '<ul id=houseTree' + hid + ' class="easyui-tree"></ul>'
                        });
                    }
                }
                $('#tabs1').tabs('select', 0);

                for (var h = 0; h < res.length; h++) {
                    (function (h) {
                        $('#houseTree' + res[h].HouseID).tree({
                            checkbox: true, animate: true, cascadeCheck: true, async: false,
                            url: 'sysService.aspx?method=QueryItemFormat&hid=' + res[h].HouseID,
                            onLoadSuccess: function () {
                                //绑定权限
                                var array = items.split(',');
                                for (var j = 0; j < array.length; j++) {
                                    var node = $('#houseTree' + res[h].HouseID).tree('find', array[j]);
                                    if (node != null) {
                                        $('#houseTree' + res[h].HouseID).tree('check', node.target);
                                    }
                                }
                            }
                        })
                    })(h)
                }
            }
        }
        //新增角色信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增角色信息');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
            $('#DelFlag').combobox('textbox').bind('focus', function () { $('#DelFlag').combobox('showPanel'); });
            $('input:radio[name="IsAdmin"][value="0"]').prop('checked', true); //是否管理员赋值
        }
        //修改角色信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改角色信息');
                $('#fm').form('load', row);
                $('#DelFlag').combobox('textbox').bind('focus', function () { $('#DelFlag').combobox('showPanel'); });
            }
        }
        //修改角色信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改角色信息');
                $('#fm').form('load', row);
                $('#DelFlag').combobox('textbox').bind('focus', function () { $('#DelFlag').combobox('showPanel'); });
            }
        }

        //删除角色
        function delItemByID(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                $.messager.confirm('<%= House.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: 'sysService.aspx?method=DelRole',
                            type: 'post',
                            dataType: 'json',
                            data: { data: json },
                            success: function (text) {
                                //var res = eval('(' + text + ')');
                                if (text.Result == true) {
                                    $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                    $('#dg').datagrid('reload');
                                } else {
                                    $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                }
                            }
                        });
                    }
                });
            }
            else { $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning'); }
        }
        //删除角色
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
                        url: 'sysService.aspx?method=DelRole',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
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

        //保存角色
        function saveItem() {
            $('#fm').form('submit', {
                url: 'sysService.aspx?method=SaveRoles',
                onSubmit: function () {
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

        //按键权限
        function onButtonPower(val, row, index) {
            var bp = val.split(',');
            var res = "";
            for (var i = 0, l = bp.length; i < l; i++) {
                switch (bp[i]) {
                    case "Q1": res += "查询订单/"; break;
                    case "Q2": res += "查询运单/"; break;
                    case "Q3": res += "查询车辆合同/"; break;
                    case "M1": res += "管理订单/"; break;
                    case "M2": res += "管理运单/"; break;
                    case "M3": res += "管理车辆合同/"; break;
                    default: break;
                }
            }
            return res;
        }
    </script>
</asp:Content>
