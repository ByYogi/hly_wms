<%@ Page Title="供应商管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaserManager.aspx.cs" Inherits="Cargo.Client.PurchaserManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //页面加载时执行
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
        //页面加载显示遮罩层防止用户看见未加载CSS的页面
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //加载完成后关闭遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }

        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    {
                        title: '', field: 'ID', checkbox: true, width: '30px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '供应商名称', field: 'PurchaserName', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '供应商简称', field: 'PurchaserShortName', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '供应商类型', field: 'PurchaserType', width: '10%', formatter: function (val, row, index) {
                            if (val == "0") { return "普通供应商"; }
                            else if (val == "1") { return "月结供应商"; }
                        }
                    },
                    {
                        title: '负责人', field: 'Boss', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '手机号码', field: 'Cellphone', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '电话', field: 'Telephone', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '所在省份', field: 'Province', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '所在城市', field: 'City', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '地址', field: 'Address', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '所属仓库', field: 'HouseName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '状态标识', field: 'DelFlag', width: '10%', formatter: function (val, row, index) {
                            if (val == "0") { return "启用"; }
                            else if (val == "1") { return "停用"; }
                        }
                    },
                    { title: '操作时间', field: 'OP_DATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { edit(index); }
            });

            //省市区三级联动
            $('#AProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData&PID=0',
                valueField: 'ID', textField: 'City',
                onSelect: function (rec) {
                    $('#ACity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#ACity').combobox('reload', url);
                    //一级仓库
                    $('#ACity').combobox({
                        onSelect: function (fai) {
                            $('#ACountry').combobox('clear');
                            var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + fai.ID;
                            $('#ACountry').combobox('reload', url);
                        }
                    });
                }
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });

            //列表回车响应查询
            $("#saPanelTab").keydown(function (e) { if (e.keyCode == 13) { dosearch(); } });


            $('#dgDeliveryAddress').datagrid({
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
                idField: 'DAID',
                url: null,
                toolbar: '#DeliveryAddressbar',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    { title: '提货单位', field: 'DeliveryName', width: '80px' },
                    { title: '提货人', field: 'DeliveryBoss', width: '80px' },
                    { title: '手机号码', field: 'DeliveryCellphone', width: '80px' },
                    { title: '电话', field: 'DeliveryTelephone', width: '80px' },
                    { title: '收货地址', field: 'DeliveryAddress', width: '200px' },
                    { title: '所在省', field: 'DeliveryProvince', width: '60px' },
                    { title: '所在市', field: 'DeliveryCity', width: '60px' },
                    { title: '所在区', field: 'DeliveryCountry', width: '60px' },
                    { title: '操作时间', field: 'OP_DATE', width: '120px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dgDeliveryAddress').datagrid('clearSelections');
                    $('#dgDeliveryAddress').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                    editAcceptAddressByID(index);
                }
            })
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Client/clientApi.aspx?method=QueryCargoPurchaser';
            $('#dg').datagrid('load', {
                PurchaserName: $('#PurchaserName').val(),
                ShortName: $('#ShortName').val(),
                Address: $("#Address").val(),
                Boss: $("#Boss").val(),
                Cellphone: $("#Cellphone").val(),
                DelFlag: $("#DelFlag").combobox('getValue')
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%" />
    <table id="saPanelTab">
        <tr>
            <td style="text-align: right;">名称:
            </td>
            <td>
                <input id="PurchaserName" class="easyui-textbox" data-options="prompt:'请输入名称'" style="width: 100px" />
            </td>
            <td style="text-align: right;">简称:
            </td>
            <td>
                <input id="ShortName" class="easyui-textbox" data-options="prompt:'请输入简称'" style="width: 100px" />
            </td>
            <td style="text-align: right;">地址:
            </td>
            <td>
                <input id="Address" class="easyui-textbox" data-options="prompt:'请输入地址'" style="width: 100px" />
            </td>
            <td style="text-align: right;">负责人:
            </td>
            <td>
                <input id="Boss" class="easyui-textbox" data-options="prompt:'请输入负责人'" style="width: 100px" />
            </td>
            <td style="text-align: right;">手机号码:
            </td>
            <td>
                <input id="Cellphone" class="easyui-textbox" data-options="prompt:'请输入手机号码'" style="width: 100px" />
            </td>
            <td style="text-align: right;">状态:
            </td>
            <td>
                <select class="easyui-combobox" id="DelFlag" style="width: 80px;" panelheight="auto" editable="false">
                    <option value="0">启用</option>
                    <option value="1">停用</option>
                    <option value="-1">全部</option>
                </select>
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
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="add()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="edit()" id="btnUpdate">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="Del()" id="btnDel">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-tag_blue_edit" plain="false" onclick="btnAddress()" id="btnAddress">&nbsp;提货地址&nbsp;</a>&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="Export()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnExport" runat="server" Style="display: none;" Text="导出" OnClick="btnExport_Click" />
        </form>
    </div>
    <%--此div用于显示按钮操作--%>
    <!--新增-->
    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 450px; padding: 0px 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="PurchaserID" />
                <table>
                    <tr>
                        <td style="text-align: right;">供应商类型:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="APurchaserType" name="PurchaserType" data-options="editable:false" style="width: 250px;" panelheight="auto" required="true">
                                <option value="0">普通供应商</option>
                                <option value="1">月结供应商</option>
                            </select>
                        </td>
                        <td style="text-align: right;">状态标识:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="ADelFlag" name="DelFlag" style="width: 250px;"
                                panelheight="auto" required="true">
                                <option value="0">启用</option>
                                <option value="1">停用</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">供应商名称:
                        </td>
                        <td>
                            <input name="PurchaserName" id="APurchaserName" class="easyui-textbox" data-options="required:true" style="width: 250px;" />
                        </td>
                        <td style="text-align: right;">供应商简称:
                        </td>
                        <td>
                            <input name="PurchaserShortName" id="APurchaserShortName" class="easyui-textbox" data-options="required:true" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="AHouseID" name="HouseID" class="easyui-combobox" style="width: 250px;" data-options="required:true" panelheight="auto" />
                        </td>
                        <td style="text-align: right;">联系人:
                        </td>
                        <td>
                            <input name="Boss" id="ABoss" class="easyui-textbox" data-options="required:true" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">联系电话:
                        </td>
                        <td>
                            <%--90px--%>
                            <input name="Cellphone" id="ACellphone" class="easyui-numberbox" style="width: 250px;" />
                        </td>
                        <td style="text-align: right;">公司电话:
                        </td>
                        <td>
                            <input name="Telephone" id="ATelephone" class="easyui-textbox" style="width: 250px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">营业执照:
                        </td>
                        <td>
                            <input name="SocialCreditCode" id="SocialCreditCode" class="easyui-textbox" style="width: 250px;" />
                        </td>
                        <td style="text-align: right;">法人:
                        </td>
                        <td>
                            <input name="LegalPerson" id="LegalPerson" class="easyui-textbox" style="width: 250px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">公司地址:
                        </td>
                        <td colspan="3">
                            <input id="AProvince" name="Province" class="easyui-combobox" style="width: 70px;" />&nbsp;省
                            <input id="ACity" name="City" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;市
                            <input id="ACountry" name="Country" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;区&nbsp;
                            <input name="Address" id="AAddress" class="easyui-textbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">客户备注:
                        </td>
                        <td colspan="3">
                            <textarea id="Remark" rows="6" name="Remark" style="width: 500px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">供应商评价:
                        </td>
                        <td colspan="3">
                            <textarea id="SupplierEvaluation" rows="4" name="SupplierEvaluation" style="width: 500px;"></textarea>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="save()">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>

    <!--新增-->

    <!--客户收货地址-->
    <div id="dlgDeliveryAddress" class="easyui-dialog" style="width: 1000px; height: 550px; padding: 0px 0px" closed="true" buttons="#dlgDeliveryAddress-buttons">
        <div id="saPanel">
            <input type="hidden" id="APurchaserID" name="PurchaserID" />
            <table>
                <tr>
                    <td style="text-align: right;">提货单位:
                    </td>
                    <td>
                        <input id="ADeliveryName" class="easyui-textbox" data-options="prompt:'请输入公司名称'" style="width: 150px" />
                    </td>
                    <td style="text-align: right;">负责人:
                    </td>
                    <td>
                        <input id="ADeliveryBoss" class="easyui-textbox" data-options="prompt:'请输入客户姓名'" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">手机号码:
                    </td>
                    <td>
                        <input id="ADeliveryCellphone" class="easyui-textbox" data-options="prompt:'请输入手机号码'" style="width: 90px" />
                    </td>
                </tr>
            </table>
        </div>
        <table id="dgDeliveryAddress" class="easyui-datagrid">
        </table>
        <div id="DeliveryAddressbar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryDeliveryAddress()">&nbsp;查询&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addDeliveryAddress()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editDeliveryAddressByID()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelDeliveryAddress()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;

        </div>
    </div>
    <div id="dlgDeliveryAddress-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgDeliveryAddress').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--客户收货地址-->
    <!--新增客户收货地址-->
    <div id="dlgAddDeliveryAddress" class="easyui-dialog" style="width: 800px; height: 300px; padding: 0px 0px" closed="true" buttons="#dlgAddDeliveryAddress-buttons">
        <div id="saPanel">
            <form id="fmAddDeliveryAddress" class="easyui-form" method="post">
                <input type="hidden" name="DAID" />
                <input type="hidden" name="PurchaserID" id="UPurchaserID" />
                <table>
                    <tr>
                        <td style="text-align: right;">提货单位:
                        </td>
                        <td>
                            <input name="DeliveryName" id="DeliveryName" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                        </td>
                        <td style="text-align: right;">提货人:
                        </td>
                        <td>
                            <input name="DeliveryBoss" id="DeliveryBoss" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">提货号码:
                        </td>
                        <td>
                            <input name="DeliveryCellphone" id="DeliveryCellphone" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                        </td>
                        <td style="text-align: right;">提货电话:
                        </td>
                        <td>
                            <input name="DeliveryTelephone" id="DeliveryTelephone" class="easyui-textbox" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">提货地址:
                        </td>
                        <td colspan="3">
                            <input id="DeliveryProvince" name="DeliveryProvince" class="easyui-combobox" style="width: 70px;" />&nbsp;省
                            <input id="DeliveryCity" name="DeliveryCity" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;市
                            <input id="DeliveryCountry" name="DeliveryCountry" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;区&nbsp;
                            <input name="DeliveryAddress" id="DeliveryAddress" class="easyui-textbox" style="width: 400px;" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlgAddDeliveryAddress-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveDeliveryAddress()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAddDeliveryAddress').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--新增客户收货地址-->
    <script type="text/javascript">

        //查询客户收货地址数据
        function QueryDeliveryAddress() {
            var mOpts = $('#dgDeliveryAddress').datagrid('options');
            mOpts.url = 'clientApi.aspx?method=QueryDeliveryAddress';
            $('#dgDeliveryAddress').datagrid('load', {
                PurchaserID: $('#APurchaserID').val(),
                DeliveryName: $('#ADeliveryName').val(),
                DeliveryBoss: $('#ADeliveryBoss').val(),
                DeliveryCellphone: $('#ADeliveryCellphone').val()
            });
        }
        //新增收货地址
        function addDeliveryAddress() {
            $('#dlgAddDeliveryAddress').dialog('open').dialog('setTitle', '新增客户收货地址信息');
            $('#fmAddDeliveryAddress').form('clear');
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#UPurchaserID').val($('#APurchaserID').val());
            }
        }
        //提货地址管理
        function btnAddress() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#APurchaserID').val(row.PurchaserID);
                QueryDeliveryAddress();

                $('#dgDeliveryAddress').datagrid('loadData', { total: 0, rows: [] });
                $('#dlgDeliveryAddress').dialog('open').dialog('setTitle', '客户收货地址管理');

                //省市区三级联动
                $('#DeliveryProvince').combobox({
                    url: '../House/houseApi.aspx?method=QueryCityData&PID=0',
                    valueField: 'ID', textField: 'City',
                    onSelect: function (rec) {
                        $('#DeliveryCity').combobox('clear');
                        var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                        $('#DeliveryCity').combobox('reload', url);
                        //一级仓库
                        $('#DeliveryCity').combobox({
                            onSelect: function (fai) {
                                $('#DeliveryCountry').combobox('clear');
                                var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + fai.ID;
                                $('#DeliveryCountry').combobox('reload', url);
                            }
                        });
                    }
                });
            }
        }
        //修改客户信息
        function editDeliveryAddressByID() {
            var rows = $('#dgDeliveryAddress').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#fmAddDeliveryAddress').form('clear');
                $('#dlgAddDeliveryAddress').dialog('open').dialog('setTitle', '修改客户收货地址信息');
                $('#fmAddDeliveryAddress').form('load', rows[0]);

                $('#AClientNum').combobox('readonly', true);
            }
        }
        //删除客户收货地址
        function DelDeliveryAddress() {
            var rows = $('#dgDeliveryAddress').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'clientApi.aspx?method=DelDeliveryAddress',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dgDeliveryAddress').datagrid('reload');
                                $('#dgDeliveryAddress').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存客户收货地址
        function saveDeliveryAddress() {
            debugger;
            $('#fmAddDeliveryAddress').form('submit', {
                url: 'clientApi.aspx?method=SaveDeliveryAddress',
                onSubmit: function (param) {
                    param.AProvince = $('#DeliveryProvince').combobox('getText');
                    param.ACity = $('#DeliveryCity').combobox('getText');
                    param.ACountry = $('#DeliveryCountry').combobox('getText');
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgAddDeliveryAddress').dialog('close'); 	// close the dialog
                        QueryDeliveryAddress();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //新增客户信息
        function add() {
            $('#dlg').dialog('open').dialog('setTitle', '新增供应商信息');
            $('#fm').form('clear');
            $('#ADelFlag').combobox('select', '0');
            $('#APurchaserType').combobox('select', '0');
            $('#AHouseID').combobox('setValue', "<%= UserInfor.HouseID%>");
        }
        //修改客户信息
        function edit() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#fm').form('clear');
                $('#dlg').dialog('open').dialog('setTitle', '修改供应商信息');
                $('#fm').form('load', row);
            }
        }
        //保存客户信息
        function save() {
            $('#fm').form('submit', {
                url: 'clientApi.aspx?method=SaveCargoPurchaser',
                onSubmit: function (param) {
                    param.AProvince = $('#AProvince').combobox('getText');
                    param.ACity = $('#ACity').combobox('getText');
                    param.ACountry = $('#ACountry').combobox('getText');
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        $('#dg').datagrid('reload');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //删除客户信息
        function Del() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'clientApi.aspx?method=DelCargoPurchaser',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dg').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        function Export() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var obj = document.getElementById("<%=btnExport.ClientID %>"); obj.click();
        }
        function Export() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#PurchaserName').val();
            key[1] = $("#Address").val();
            key[2] = $("#Boss").val();
            key[3] = $("#Cellphone").val();
            key[4] = $('#DelFlag').datebox('getValue');

            $.ajax({
                url: "clientApi.aspx?method=QueryCargoPurchaserForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnExport.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
    </script>
</asp:Content>
