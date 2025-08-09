<%@ Page Title="导航管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemItem.aspx.cs" Inherits="House.SystemItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                //iconCls: 'icon-view',//标题图标
                loadMsg: '数据加载中请稍候...',
                //width: function () { return document.body.clientWidth * 0.9 },
                //nowrap: true, //是否禁止文字自动换行设置为 true，则把数据显示在一行里。设置为 true 可提高加载性能。
                autoRowHeight: false, //行高是否自动
                //striped: true, //奇偶行使用不同背景色
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                //rownumbers: true, //行号
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                //ctrlSelect:true,
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ItemID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ItemID', checkbox: true, width: '30px' },
                  { title: '导航名称', field: 'CName', width: '100px' },
                  { title: '上级名称', field: 'ParentCName', width: '100px' },
                  { title: '链接地址', field: 'ItemSrc', width: '250px' },
                  {
                      title: '状态', field: 'DelFlag', width: '40px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "启用"; }
                          else if (val == "1") { return "停用"; }
                          else { return "启用"; }
                      }
                  },
                  { title: '所属系统', field: 'HouseName', width: '130px' },
                  { title: '图标', field: 'ItemIcon', width: '120px' },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });
            $('#ParentID').combobox('textbox').bind('focus', function () { $('#ParentID').combobox('showPanel'); });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#dFlag').combobox('textbox').bind('focus', function () { $('#dFlag').combobox('showPanel'); });
            $('#DelFlag').combobox('textbox').bind('focus', function () { $('#DelFlag').combobox('showPanel'); });
            $('#AParentID').combobox('textbox').bind('focus', function () { $('#AParentID').combobox('showPanel'); });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });

            //所属单位
            $('#HouseID').combobox({
                url: 'sysService.aspx?method=QueryALLHouse',
                valueField: 'HouseID', textField: 'HouseName',
                onSelect: function (rec) {
                    $('#ParentID').combobox('clear');
                    var url = 'sysService.aspx?method=ParentItemQueryByHouseID&id=' + rec.HouseID;
                    $('#ParentID').combobox('reload', url);
                }
            });
        });

        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=SystemItemQuery';
            $('#dg').datagrid('load', {
                CName: $('#CName').val(),
                ParentID: $("#ParentID").combobox('getValue'),
                HouseID: $("#HouseID").combobox('getValue'),
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
                        <%--<li class="">
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

                        <li class="active">
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
                    <li class="active">导航管理</li>
                </ul>
            </div>

            <!-- /section:basics/content.breadcrumbs -->
            <div class="page-content">

                <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
                    <table>
                        <tr>
                            <td style="text-align: right; width: 60px;">导航名称:
                            </td>
                            <td>
                                <input id="CName" class="easyui-textbox" data-options="prompt:'请输入导航名称'" style="width: 100px">
                            </td>
                            <td style="text-align: right; width: 60px;">所属系统:
                            </td>
                            <td>
                                <input id="HouseID" class="easyui-combobox" style="width: 150px;"
                                    panelheight="auto" />
                            </td>
                            <td style="text-align: right; width: 60px;">父级名称:
                            </td>
                            <td>
                                <input id="ParentID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'ItemID',textField:'CName'"
                                    panelheight="auto" />
                            </td>

                            <td style="text-align: right; width: 60px;">状态:
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
                </div>
                <div id="dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 10px 10px"
                    closed="true" buttons="#dlg-buttons">
                    <form id="fm" class="easyui-form" method="post">
                        <input type="hidden" name="ItemID" />
                        <input type="hidden" id="AHouseName" name="AHouseName" />
                        <input type="hidden" id="AHouseCode" name="AHouseCode" />
                        <table>
                            <tr>
                                <td style="text-align: right;">导航中文名称:
                                </td>
                                <td>
                                    <input name="CName" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">导航英文名称:
                                </td>
                                <td>
                                    <input name="EName" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">导航链接路径:
                                </td>
                                <td>
                                    <input name="ItemSrc" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">所属系统:
                                </td>
                                <td>
                                    <input name="HouseID" id="AHouseID" class="easyui-combobox" data-options="valueField:'HouseID',textField:'HouseName',url:'sysService.aspx?method=QueryALLHouse',onSelect:houseChange"
                                        panelheight="auto" style="width: 250px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">对应父级:
                                </td>
                                <td>
                                    <input name="ParentID" id="AParentID" class="easyui-combobox" data-options="valueField:'ItemID',textField:'CName'"
                                        panelheight="auto" style="width: 250px;" />
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
                            <tr>
                                <td style="text-align: right;">选择图标:
                                </td>
                                <td>
                                    <input name="ItemIcon" class="easyui-textbox" data-options="required:true" style="width: 250px;">
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

        //系统选择方法
        function houseChange(item) {
            if (item) {
                $('#AHouseCode').val(item.HouseCode);
                $('#AHouseName').val(item.HouseName);
                $('#AParentID').combobox('clear');
                var url = 'sysService.aspx?method=ParentItemQueryByHouseID&id=' + item.HouseID;
                $('#AParentID').combobox('reload', url);
                //$('#AHouseCode').textbox('setValue', item.HouseCode);
                //$('#AHouseName').textbox('setValue', item.HouseName);
            }
        }
        //新增导航链接
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增导航链接');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
        }
        //修改导航链接
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改导航链接');
                $('#AParentID').combobox('clear');
                var url = 'sysService.aspx?method=ParentItemQueryByHouseID&id=' + row.HouseID;
                $('#AParentID').combobox('reload', url);
                $('#fm').form('load', row);

                $('#AHouseName').val(row.HouseName)
                $('#AHouseCode').val(row.HouseCode)
            }
        }
        //修改导航链接
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改导航链接');
                $('#AParentID').combobox('clear');
                var url = 'sysService.aspx?method=ParentItemQueryByHouseID&id=' + row.HouseID;
                $('#AParentID').combobox('reload', url);
                $('#fm').form('load', row);
                $('#AHouseName').val(row.HouseName)
                $('#AHouseCode').val(row.HouseCode)
            }
        }
        //删除导航链接
        function delItemByID(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                $.messager.confirm('<%= House.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: 'sysService.aspx?method=SystemItemDel',
                            type: 'post', dataType: 'json', data: { data: json },
                            success: function (text) {
                                //var res = eval('(' + text + ')');
                                if (text.Result == true) {
                                    $('#dg').datagrid('reload');
                                } else {
                                    $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                }
                            }
                        });
                    }
                });
            }
            else {
                $.messager.alert('<%= House.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
            }
        }

        //删除导航链接
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
                        url: 'sysService.aspx?method=SystemItemDel',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
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

        //保存导航链接
        function saveItem() {
            $('#fm').form('submit', {
                url: 'sysService.aspx?method=SystemItemSave',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
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
