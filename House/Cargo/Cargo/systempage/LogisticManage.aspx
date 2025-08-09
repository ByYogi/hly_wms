<%@ Page Title="物流公司管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogisticManage.aspx.cs" Inherits="Cargo.systempage.LogisticManage" %>

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
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '物流公司', field: 'LogisticName', width: '160px' },
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

            //物流订阅数据
            $('#dgAcceptAddress').datagrid({
                width: '100%',
                height: '440px',
                title: '', //标题内容
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#AcceptAddressbar',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    { title: '所属仓库', field: 'HouseName', width: '80px' },
                    { title: '订阅物流', field: 'LogisticName', width: '100px' },
                    { title: '物流代码', field: 'ComCode', width: '100px' },
                    {
                        title: '启用状态', field: 'DelFlag', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "启用"; } else if (val == "1") { return "停用"; } else { return ""; }
                        }
                    },
                    { title: '操作时间', field: 'OP_DATE', width: '120px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dgAcceptAddress').datagrid('clearSelections');
                    $('#dgAcceptAddress').datagrid('selectRow', index);
                }
            })
        })
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../systempage/sysService.aspx?method=QueryLogistic';
            $('#dg').datagrid('load', {
                LogisticName: $('#LogisticName').val(),
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right; width: 60px;">物流公司:
                </td>
                <td>
                    <input id="LogisticName" class="easyui-textbox" data-options="prompt:'请输入物流公司'" style="width: 100px">
                </td>
                <%--<td style="text-align: right; width: 60px;">
                    城市代码:
                </td>
                <td>
                    <input id="CityCode" class="easyui-textbox" data-options="prompt:'请输入城市代码'" style="width: 100px">
                </td>--%>
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
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add"
            plain="false" onclick="addItem()"> &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#"
                class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a
                    href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;<a
                        href="#" class="easyui-linkbutton" iconcls="icon-feed" plain="false" onclick="SetFeed()">&nbsp;抓取订阅&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 10px 10px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="OrderID" />
            <table>
                <tr>
                    <td style="text-align: right;">物流公司:
                    </td>
                    <td>
                        <input name="LogisticName" class="easyui-validatebox" data-options="required:true">
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


    <!--物流状态订阅-->
    <div id="dlgAcceptAddress" class="easyui-dialog" style="width: 1000px; height: 550px; padding: 0px 0px"
        closed="true" buttons="#dlgAcceptAddress-buttons">
        <div id="saPanel">
            <table>
                <tr>
                    <td style="text-align: right;">订阅仓库:
                    </td>
                    <td>
                        <input id="AHID" class="easyui-combobox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">物流名称:
                    </td>
                    <td>
                        <input id="ALogisticName" class="easyui-textbox" data-options="prompt:'请输入物流公司名称'" style="width: 150px" />
                    </td>

                </tr>
            </table>
        </div>
        <table id="dgAcceptAddress" class="easyui-datagrid">
        </table>
        <div id="AcceptAddressbar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryAcceptAddress()">&nbsp;查询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addAcceptAddress()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editAcceptAddressByID()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelAcceptAddress()">&nbsp;删&nbsp;除&nbsp;</a>

        </div>
    </div>
    <div id="dlgAcceptAddress-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAcceptAddress').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--物流状态订阅-->
    <!--新增订阅物流-->
    <div id="dlgAddAcceptAddress" class="easyui-dialog" style="width: 500px; height: 300px; padding: 0px 0px"
        closed="true" buttons="#dlgAddAcceptAddress-buttons">
        <div id="saPanel">
            <form id="fmAddAcceptAddress" class="easyui-form" method="post">
                <input type="hidden" name="ID" />
                <table>
                    <tr>

                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="AHouseID" name="HouseID" class="easyui-combobox" style="width: 150px;" data-options="required:true" />
                        </td>

                    </tr>
                    <tr>

                        <td style="text-align: right;">订阅物流:
                        </td>
                        <td>
                            <input name="LogisID" id="LogisID" class="easyui-combobox" data-options="panelHeight:'250px',valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic',required:true"
                                panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">物流代码:
                        </td>
                        <td>
                            <input name="ComCode" id="ComCode" class="easyui-textbox" data-options="required:true" style="width: 150px;">
                        </td>

                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlgAddAcceptAddress-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveAcceptAddress()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAddAcceptAddress').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--新增订阅物流-->

    <script type="text/javascript">

        //删除订阅物流
        function DelAcceptAddress() {
            var rows = $('#dgAcceptAddress').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'sysService.aspx?method=DelHouseLogisPoll',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dgAcceptAddress').datagrid('reload');
                                $('#dgAcceptAddress').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存订阅物流
        function saveAcceptAddress() {
            $('#fmAddAcceptAddress').form('submit', {
                url: 'sysService.aspx?method=SaveHouseLogisPoll',
                onSubmit: function (param) {
                    param.LogisticName = $('#LogisID').combobox('getText');
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgAddAcceptAddress').dialog('close'); 	// close the dialog
                        QueryAcceptAddress();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //新增订阅物流
        function addAcceptAddress() {
            $('#dlgAddAcceptAddress').dialog('open').dialog('setTitle', '新增订阅');
            $('#fmAddAcceptAddress').form('clear');
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('setValue', "<%= UserInfor.HouseID%>");
        }
        //查询订阅物流
        function QueryAcceptAddress() {
            var mOpts = $('#dgAcceptAddress').datagrid('options');
            mOpts.url = 'sysService.aspx?method=QueryHouseLogisPoll';
            $('#dgAcceptAddress').datagrid('load', {
                LogisticName: $('#ALogisticName').val(),
                HID: $("#AHID").combobox('getValue')
            });
        }
        //订阅物流
        function SetFeed() {
            $('#dgAcceptAddress').datagrid('loadData', { total: 0, rows: [] });
            $('#dlgAcceptAddress').dialog('open').dialog('setTitle', '订阅管理');
            $('#AHID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHID').combobox('setValue', "<%= UserInfor.HouseID%>");
            $('#AHID').combobox('textbox').bind('focus', function () { $('#AHID').combobox('showPanel'); });
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                QueryAcceptAddress();
            }
        }

        //新增物流公司
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增物流公司');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
            //$("#City").attr('readonly', false);
            //$("#Code").attr('readonly', false);
        }
        //修改物流公司
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改物流公司');
                $('#fm').form('load', row);
            }
        }
        //修改物流公司
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改物流公司');
                $('#fm').form('load', row);
            }
        }
        //删除物流公司
        function delItemByID(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: '../systempage/sysService.aspx?method=DelLogistic',
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

        //删除物流公司
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
                        url: '../systempage/sysService.aspx?method=DelLogistic',
                        type: 'post', dataType: 'json', data: { data: json },
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

        //保存物流公司
        function saveItem() {
            $('#fm').form('submit', {
                url: '../systempage/sysService.aspx?method=SaveLogistic',
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
