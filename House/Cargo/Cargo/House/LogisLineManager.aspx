<%@ Page Title="线路管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogisLineManager.aspx.cs" Inherits="Cargo.House.LogisLineManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        //页面加载显示遮罩层
        var pc;
        var LogiName =<%=UserInfor.LoginName%>;
        var IsQueryLockStock = null;

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
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height });
            $('#outDg').datagrid({ height: (Number($(window).height()) - 90) - height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: 'LineID', checkbox: true });
            columns.push({ title: '区域大仓', field: 'HouseName', width: '100px' });
            //columns.push({ title: '所属仓库', field: 'AreaName', width: '100px' });
            columns.push({ title: '线路名称', field: 'LineName', width: '100px' });
            columns.push({ title: '承运物流商', field: 'LogisticName', width: '100px' });
            columns.push({ title: '线路备注', field: 'Memo', width: '100px' });
            columns.push({
                title: '启用状态', field: 'DelFlag', width: '70px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "启用"; }
                    else if (val == "1") { return "停用"; }
                    else { return "启用"; }
                }
            });

            columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });

            $('#dg').datagrid({
                width: '100%',
                //height: '50%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                rownumbers: true, //行号
                pageSize: Math.floor((Number($(window).height()) / 2 - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) / 2 - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) / 2 - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#outDg').datagrid('clearSelections');
                    var gridOpts = $('#outDg').datagrid('options');
                    gridOpts.url = '../House/houseApi.aspx?method=QueryLogisLineClientListData';
                    $('#outDg').datagrid('load', { LineID: row.LineID });
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { }
            });

            columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true });
            columns.push({ title: '店代码', field: 'ShopCode', width: '80px' });
            columns.push({ title: '公司名称', field: 'ClientName', width: '120px' });
            columns.push({ title: '客户简称', field: 'ClientShortName', width: '120px' });
            columns.push({ title: '联系人', field: 'Boss', width: '100px' });
            columns.push({ title: '手机号码', field: 'Cellphone', width: '100px' });
            columns.push({ title: '公司电话', field: 'Telephone', width: '100px' });
            columns.push({ title: '省', field: 'Province', width: '70px' });
            columns.push({ title: '市', field: 'City', width: '70px' });
            columns.push({ title: '地址', field: 'Address', width: '200px' });


            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '绑定客户列表', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true, //行号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#unbindBar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { }
            });

            //所在线路
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name', onSelect: function (rec) {
                    //$('#AFirstAreaID').combobox('clear');
                    //var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    //$('#AFirstAreaID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#ADelFlag').combobox('textbox').bind('focus', function () { $('#ADelFlag').combobox('showPanel'); });
            $('#ALogisID').combobox('textbox').bind('focus', function () { $('#ALogisID').combobox('showPanel'); });

            $('#HouseID').combobox({
                url: 'houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                //onSelect: function (rec) {
                //    $('#AreaID').combobox('clear');
                //    var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                //    $('#AreaID').combobox('reload', url);
                //    $('#AreaID').combobox('textbox').bind('focus', function () { $('#AreaID').combobox('showPanel'); });
                //}
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#VHID').combobox({ url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name' });
            $('#VHID').combobox('textbox').bind('focus', function () { $('#VHID').combobox('showPanel'); });

            //省市区三级联动
            $('#VProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData',
                valueField: 'City', textField: 'City',
                onSelect: function (rec) {
                    $('#VCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#VCity').combobox('reload', url);
                }
            });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryCargoLogisLine';
            $('#dg').datagrid('load', {
                HouseID: $("#AHouseID").combobox('getValue'),//线路ID
                //AreaID: $("#AFirstAreaID").combobox('getValue'),//线路ID
                LineName: $('#ALineName').val(),
                DelFlag: $("#ADelFlag").combobox('getValue'),
                LogisID: $("#ALogisID").combobox('getValue')
            });
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
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
               <%-- <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AFirstAreaID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'" />
                </td>--%>
                <td style="text-align: right;">线路名称:
                </td>
                <td>
                    <input id="ALineName" class="easyui-textbox" data-options="prompt:'请输入线路名称'" style="width: 100px" />
                </td>
                <td style="text-align: right;">状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="ADelFlag" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">启用</option>
                        <option value="1">停用</option>
                    </select>
                </td>
                <td style="text-align: right;">承运商:
                </td>
                <td>
                    <input id="ALogisID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'" />
                </td>

            </tr>

        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="outDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-link" plain="false" onclick="bindClient()">&nbsp;绑定线路客户&nbsp;</a>
    </div>

    <div id="unbindBar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-link_break" plain="false" onclick="UnbindClient()">&nbsp;解绑线路客户&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 400px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="LineID" />
                <table>
                    <tr>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 250px;" data-options="required:true"
                                panelheight="auto" />
                        </td>
                    </tr>
                  <%--  <tr>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="AreaID" name="AreaID" class="easyui-combobox" style="width: 250px;" data-options="valueField:'AreaID',textField:'Name',required:true"
                                panelheight="auto" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right;">线路名称:
                        </td>
                        <td>
                            <input name="LineName" class="easyui-textbox" data-options="prompt:'请输入线路名称',required:true"
                                style="width: 250px;">
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">备注:
                        </td>
                        <td>
                            <textarea id="Memo" rows="4" name="Memo" style="width: 250px;"></textarea>
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
                        <td style="text-align: right;">承运商:
                        </td>
                        <td>
                            <input id="LogisID" name="LogisID" class="easyui-combobox" style="width: 250px;" data-options="valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <%-- 绑定线路客户 --%>
    <div id="dlgViolate" class="easyui-dialog" style="width: 1000px; height: 530px; padding: 0px" closed="true" buttons="#dlgViolate-buttons">
        <input type="hidden" id="VID" />
        <div id="saPanel" class="easyui-panel" title="">
            <table>
                <tr>
                    <td style="text-align: right;">区域大仓:
                    </td>
                    <td>
                        <input id="VHID" class="easyui-combobox" style="width: 90px;" />
                    </td>
                    <td style="text-align: right;">公司简称:
                    </td>
                    <td>
                        <input id="VName" class="easyui-textbox" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">店代码:
                    </td>
                    <td>
                        <input id="VShopCode" class="easyui-textbox" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">省:</td>
                    <td>
                        <input id="VProvince" class="easyui-combobox" style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">市:</td>
                    <td>
                        <input id="VCity" class="easyui-combobox" style="width: 80px;" data-options="valueField:'City',textField:'City'" /></td>
                    <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryViolate()">&nbsp;查询客户数据&nbsp;</a></td>
                </tr>
            </table>
        </div>
        <table id="dgViolate" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlgViolate-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-link" plain="false" onclick="saveBind()">&nbsp;绑&nbsp;定&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgViolate').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <%-- 绑定线路客户 --%>

    <script type="text/javascript">
        //解绑客户
        function UnbindClient() {
            var row = $('#outDg').datagrid('getSelections');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要绑定的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定解绑？', function (r) {
                if (r) {
                    var json = JSON.stringify(row)
                    $.ajax({
                        url: 'houseApi.aspx?method=DelCargoLogisLineClient',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '绑定成功!', 'info');
                                $('#outDg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        ///绑定客户数据
        function saveBind() {
            var row = $('#dgViolate').datagrid('getSelections');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要绑定的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定绑定？', function (r) {
                if (r) {
                    var json = JSON.stringify(row)
                    $.ajax({
                        url: 'houseApi.aspx?method=bindLogisLineClient&LineID=' + escape($('#VID').val()),
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '绑定成功!', 'info');
                                $('#dlgViolate').dialog('close');
                                $('#outDg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //查询客户数据
        function QueryViolate() {
            $('#dgViolate').datagrid('clearSelections');
            var gridOpts = $('#dgViolate').datagrid('options');
            gridOpts.url = '../Client/clientApi.aspx?method=QueryCargoClient';
            $('#dgViolate').datagrid('load', {
                HID: $("#VHID").combobox('getValue'),
                Province: $("#VProvince").combobox('getValue'),
                City: $("#VCity").combobox('getValue'),
                ShopCode: $("#VShopCode").val(),
                ClientShortName: $("#VName").val(),
                DelFlag: '0'
            });
        }
        //打开绑定店代码客户信息
        function bindClient() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要操作的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlgViolate').dialog('open').dialog('setTitle', '绑定线路：' + row.LineName + " 的客户");
                $('#dgViolate').datagrid('loadData', { total: 0, rows: [] });
                $('#VHID').combobox('setValue', row.HouseID);
                $('#VID').val(row.LineID);

                showdgViolate();
            }
        }

        //显示客户数据列表
        function showdgViolate() {
            $('#dgViolate').datagrid({
                width: '100%',
                height: '410px',
                title: '',
                //iconCls: 'icon-view',//标题图标
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: true, //行高是否自动
                //striped: true, //奇偶行使用不同背景色
                collapsible: false, //是否可折叠
                pagination: true, //分页是否显示
                rownumbers: true, //行号
                pageSize: 12, //每页多少条
                pageList: [12, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                //ctrlSelect:true,
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ClientID',
                url: null,
                columns: [[
                    { title: '', field: 'ClientID', checkbox: true, width: '30px' },
                    { title: '区域大仓', field: 'HouseName', width: '80px' },
                    //{ title: '所属仓库', field: 'AreaName', width: '80px' },
                    { title: '所属公司', field: 'UpClientShortName', width: '80px' },
                    { title: '店代码', field: 'ShopCode', width: '70px' },
                    { title: '公司名称', field: 'ClientName', width: '80px' },
                    { title: '公司负责人', field: 'Boss', width: '80px' },
                    { title: '手机号码', field: 'Cellphone', width: '80px' },
                    //{ title: '公司电话', field: 'Telephone', width: '100px' },
                    { title: '省', field: 'Province', width: '70px' },
                    { title: '市', field: 'City', width: '70px' },
                    //{ title: '公司地址', field: 'Address', width: '100px' },

                    {
                        title: '绑定线路', field: 'LineName', width: '60px', formatter: function (val, row, index) {
                            if (val == "") { return "未绑定"; }
                            else { return val; }
                        }
                    }
                ]],
                onClickRow: function (index, row) {
                    $('#dgViolate').datagrid('clearSelections');
                    $('#dgViolate').datagrid('selectRow', index);
                },
            });
        }
        //新增线路信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增线路信息');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
        }
        //修改线路信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改线路信息');
                $('#fm').form('load', row);
            }
        }
        //修改线路信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改线路信息');
                $('#fm').form('load', row);
            }
        }
        //删除线路信息
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
                        url: 'houseApi.aspx?method=DelCargoLogisLine',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
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

        //保存线路信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'houseApi.aspx?method=SaveCargoLogisLine',
                onSubmit: function (param) {
                    param.LogisticName = $('#LogisID').combobox('getText');
                    var trd = $(this).form('enableValidation').form('validate');
                    return trd;
                    //return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
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
