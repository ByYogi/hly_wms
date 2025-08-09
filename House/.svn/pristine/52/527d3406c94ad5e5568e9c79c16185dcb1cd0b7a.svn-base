<%@ Page Title="销售客户管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SaleClientManager.aspx.cs" Inherits="Cargo.Client.SaleClientManager" %>

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
        var LoginName;
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                //pageSize: 20, //每页多少条
                //pageList: [20, 50],
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ClientID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ClientID', checkbox: true, width: '30px' },
                    { title: '客户编码', field: 'ClientNum', width: '70px' },
                    {
                        title: '公司名称', field: 'ClientName', width: '120px'
                    },
                    { title: '公司简称', field: 'ClientShortName', width: '120px' },
                    {
                        title: '客户类型', field: 'ClientType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "个人客户"; }
                            else if (val == "1") { return "月结客户"; }
                            else if (val == "2") { return "VIP客户"; }
                            else if (val == "3") { return "逾期客户"; }
                            else { return "个人客户"; }
                        }
                    },

                    { title: '公司负责人', field: 'Boss', width: '80px' },
                    { title: '手机号码', field: 'Cellphone', width: '100px' },
                    { title: '公司电话', field: 'Telephone', width: '100px' },
                    {
                        title: '公司地址', field: 'Address', width: '300px', formatter: function (val, row, index) {
                            return "<span title='" + row.Province + " " + row.City + " " + row.Country + " " + row.Address + "'>" + row.Province + " " + row.City + " " + row.Country + " " + row.Address + "</span>";
                        }
                    },
                    //{ title: '预收款余额', field: 'PreReceiveMoney', width: '70px', align: 'right' },
                    //{ title: '返利款余额', field: 'RebateMoney', width: '70px', align: 'right' },
                    //{ title: '透支额度', field: 'LimitMoney', width: '70px', align: 'right' },
                    //{ title: '目标销量', field: 'TargetNum', width: '70px', align: 'right' },
                    //{ title: '业务员', field: 'UserName', width: '60px' },
                    //{ title: '所属仓库', field: 'HouseName', width: '80px' },
                    //{ title: '店代码', field: 'ShopCode', width: '60px' },
                    {
                        title: '启用状态', field: 'DelFlag', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "启用"; }
                            else if (val == "1") { return "停用"; }
                            else { return "启用"; }
                        }
                    },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                rowStyler: function (index, row) {
                    if (row.ClientType == "3") { return "color:#FF0000"; };
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            $('#QClientType').combobox('textbox').bind('focus', function () { $('#QClientType').combobox('showPanel'); });
            $('#QDelFlag').combobox('textbox').bind('focus', function () { $('#QDelFlag').combobox('showPanel'); });
            $('#ClientType').combobox('textbox').bind('focus', function () { $('#ClientType').combobox('showPanel'); });
            //省市区三级联动
            $('#Province').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData&PID=0',
                valueField: 'ID', textField: 'City',
                onSelect: function (rec) {
                    $('#City').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#City').combobox('reload', url);
                    //一级仓库
                    $('#City').combobox({
                        onSelect: function (fai) {
                            $('#Country').combobox('clear');
                            var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + fai.ID;
                            $('#Country').combobox('reload', url);
                        }
                    });
                }
            });
            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HID').combobox('setValue', "<%= UserInfor.HouseID%>");
            var houid = "<%= UserInfor.HouseID%>";
            if (houid == "10") {
                LoginName = "2368";
            }
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
            });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
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
                idField: 'ADID',
                url: null,
                toolbar: '#AcceptAddressbar',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    { title: '客户编码', field: 'ClientNum', width: '60px' },
                    { title: '收货人', field: 'AcceptPeople', width: '80px' },
                    { title: '手机号码', field: 'AcceptCellphone', width: '80px' },
                    { title: '电话', field: 'AcceptTelephone', width: '80px' },
                    { title: '收货地址', field: 'AcceptAddress', width: '200px' },
                    { title: '所在省', field: 'AcceptProvince', width: '60px' },
                    { title: '所在市', field: 'AcceptCity', width: '60px' },
                    { title: '所在区', field: 'AcceptCountry', width: '60px' },
                    //{ title: '目标销量', field: 'TargetNum', width: '60px', align: 'right' },
                    //{ title: '所属仓库', field: 'HouseName', width: '80px' },
                    { title: '操作时间', field: 'OP_DATE', width: '120px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dgAcceptAddress').datagrid('clearSelections');
                    $('#dgAcceptAddress').datagrid('selectRow', index);
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
            gridOpts.url = '../Client/clientApi.aspx?method=QueryCargoClient';
            $('#dg').datagrid('load', {
                ClientNum: $('#QClientNum').val(),
                ClientName: $("#QClientName").val(),
                ClientShortName: $("#QClientShortName").val(),
                Boss: $("#QBoss").val(),
                AcceptPeople: $("#QAcceptPeople").val(),
                Cellphone: $("#QCellphone").val(),
                DelFlag: $("#QDelFlag").combobox('getValue'),
                ClientType: $("#QClientType").combobox('getValue'),
                HID: $("#HID").combobox('getValue'),
                UserID: LoginName
            });
            //adjustment();
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
                <td style="text-align: right;">负责人:
                </td>
                <td>
                    <input id="QBoss" class="easyui-textbox" data-options="prompt:'请输入公司负责人'" style="width: 120px">
                </td>
                <td style="text-align: right;">公司全称:
                </td>
                <td>
                    <input id="QClientName" class="easyui-textbox" data-options="prompt:'请输入客户全称'" style="width: 120px">
                </td>
                <td style="text-align: right;">公司简称:
                </td>
                <td>
                    <input id="QClientShortName" class="easyui-textbox" data-options="prompt:'请输入客户简称'" style="width: 120px">
                </td>
                <td style="text-align: right;">客户类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="QClientType" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">个人客户</option>
                        <option value="1">月结客户</option>
                        <option value="2">VIP客户</option>
                        <option value="3">逾期客户</option>
                    </select>
                </td>
                <td style="text-align: right;">状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="QDelFlag" style="width: 80px;" panelheight="auto" editable="false">
                        <option value="0">启用</option>
                        <option value="1">停用</option>
                        <option value="-1">全部</option>
                    </select>
                </td>

            </tr>
            <tr>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="QAcceptPeople" class="easyui-textbox" data-options="prompt:'请输入收货人'" style="width: 120px">
                </td>
                <td style="text-align: right;">客户编码:
                </td>
                <td>
                    <input id="QClientNum" class="easyui-numberbox" data-options="prompt:'请输入客户编码'" style="width: 120px">
                </td>
                <%--                <td style="text-align: right;">公司电话:
                </td>
                <td>
                    <input id="QTelephone" class="easyui-textbox" data-options="prompt:'请输入公司电话'" style="width: 120px">
                </td>--%>
                <td style="text-align: right;">手机号码:
                </td>
                <td>
                    <input id="QCellphone" class="easyui-numberbox" data-options="prompt:'请输入手机号码'" style="width: 120px">
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;"></td>
                <td></td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()" id="btnAdd">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()" id="btnUpdate">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()" id="btnDel">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
     <%--   <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()" id="btnExport">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;--%>
        <a href="#" class="easyui-linkbutton" iconcls="icon-tag_blue_edit" plain="false" onclick="btnAddress()" id="btnAddress">&nbsp;收货地址&nbsp;</a>
        <%-- <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>--%>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 450px; padding: 0px 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="ClientID" />
                <input type="hidden" name="UserID" id="UserID" />
                <input type="hidden" name="UserName" id="UserName" />

                <table>
                    <tr>
                        <td style="text-align: right;">客户类型:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="ClientType" name="ClientType" data-options="onSelect:ctypeChange" style="width: 250px;"
                                panelheight="auto" required="true">
                                <option value="1">月结客户</option>
                                <option value="0">个人客户</option>
                                <option value="2">VIP客户</option>
                                <option value="3">逾期客户</option>
                            </select>
                        </td>
                        <td style="text-align: right;">客户编号:
                        </td>
                        <td>
                            <select name="ClientNum" id="ClientNum" class="easyui-combogrid" style="width: 250px" data-options="panelWidth: 200,idField: 'ClientNum',textField: 'ClientNum',url: 'clientApi.aspx?method=QuerySpecialCodeList',method: 'get',columns: [[{field:'ClientNum',title:'客户编码',width:100},{field:'ClientShortName',title:'客户名称',width:100}]],fitColumns: true">
                            </select>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">客户全称:
                        </td>
                        <td>
                            <input name="ClientName" id="ClientName" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>
                        <td style="text-align: right;">客户简称:
                        </td>
                        <td>
                            <input name="ClientShortName" id="ClientShortName" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">公司地址:
                        </td>
                        <td colspan="3">
                            <input id="Province" name="Province" class="easyui-combobox" style="width: 70px;" data-options="required:true" />&nbsp;省
                            <input id="City" name="City" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City',required:true" />&nbsp;市
                            <input id="Country" name="Country" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;区&nbsp;
                            <input name="Address" id="Address" class="easyui-textbox" style="width: 250px;" data-options="required:true" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">负责人名:
                        </td>
                        <td>
                            <input name="Boss" id="Boss" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                        </td>
                        <td style="text-align: right;">手机号码:
                        </td>
                        <td>
                            <input name="Cellphone" id="Cellphone" class="easyui-numberbox" style="width: 90px;">
                            公司电话:
                            <input name="Telephone" id="Telephone" class="easyui-textbox" style="width: 90px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">公司邮件:
                        </td>
                        <td>
                            <input name="Email" id="Email" class="easyui-textbox" style="width: 250px;">
                        </td>
                        <td style="text-align: right;">公司邮编:
                        </td>
                        <td>
                            <input name="ZipCode" id="ZipCode" class="easyui-textbox" style="width: 90px;">
                            公司传真:
                      
                            <input name="Fax" id="Fax" class="easyui-textbox" style="width: 90px;">
                        </td>
                    </tr>
                    <tr>

                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 250px;" data-options="required:true"
                                panelheight="auto" />
                        </td>
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
                        <td style="text-align: right;">客户备注:
                        </td>
                        <td colspan="3">
                            <textarea id="Remark" rows="6" name="Remark" style="width: 500px;"></textarea>
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <!--客户收货地址-->
    <div id="dlgAcceptAddress" class="easyui-dialog" style="width: 1000px; height: 550px; padding: 0px 0px"
        closed="true" buttons="#dlgAcceptAddress-buttons">
        <div id="saPanel">
            <table>
                <tr>
                    <td style="text-align: right;">客户编码:
                    </td>
                    <td>
                        <input id="AdClientNum" class="easyui-textbox" data-options="prompt:'请输入客户编码',readonly:true" style="width: 80px" />
                    </td>
                    <td style="text-align: right;">公司名称:
                    </td>
                    <td>
                        <input id="AAcceptCompany" class="easyui-textbox" data-options="prompt:'请输入公司名称'" style="width: 150px" />
                    </td>
                    <td style="text-align: right;">客户姓名:
                    </td>
                    <td>
                        <input id="AAcceptPeople" class="easyui-textbox" data-options="prompt:'请输入客户姓名'" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">手机号码:
                    </td>
                    <td>
                        <input id="AAcceptCellphone" class="easyui-textbox" data-options="prompt:'请输入手机号码'" style="width: 90px" />
                    </td>

                    <td style="text-align: right;">所属仓库:
                    </td>
                    <td>
                        <input id="AHID" class="easyui-combobox" style="width: 90px;" />
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
    <!--客户收货地址-->
    <!--新增客户收货地址-->
    <div id="dlgAddAcceptAddress" class="easyui-dialog" style="width: 800px; height: 300px; padding: 0px 0px"
        closed="true" buttons="#dlgAddAcceptAddress-buttons">
        <div id="saPanel">
            <form id="fmAddAcceptAddress" class="easyui-form" method="post">
                <input type="hidden" name="ADID" />
                <input type="hidden" name="ClientID" id="ADClientID" />
                <table>
                    <tr>
                        <td style="text-align: right;">客户编码:
                        </td>
                        <td>
                            <input name="ClientNum" id="AClientNum" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="AHouseID" name="HouseID" class="easyui-combobox" style="width: 250px;" data-options="required:true"
                                panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">收货单位:
                        </td>
                        <td>
                            <input name="AcceptCompany" id="AcceptCompany" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AcceptPeople" class="easyui-textbox" data-options="required:true"
                                style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">手机号码:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AcceptCellphone" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                        </td>
                        <td style="text-align: right;">公司电话:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AcceptTelephone" class="easyui-textbox" style="width: 250px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">公司地址:
                        </td>
                        <td colspan="3">
                            <input id="AcceptProvince" name="AcceptProvince" class="easyui-combobox" style="width: 70px;" data-options="required:true" />&nbsp;省
                            <input id="AcceptCity" name="AcceptCity" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City',required:true" />&nbsp;市
                            <input id="AcceptCountry" name="AcceptCountry" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;区&nbsp;
                            <input name="AcceptAddress" id="AcceptAddress" class="easyui-textbox" style="width: 400px;" data-options="required:true" />
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
    <!--新增客户收货地址-->

    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        //保存客户收货地址
        function saveAcceptAddress() {
            var clientnum = $('#AClientNum').textbox('getValue');
            if (clientnum == undefined || clientnum == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '客户编码不能为空!', 'warning');
                return;
            }
            $('#fmAddAcceptAddress').form('submit', {
                url: 'clientApi.aspx?method=SaveAcceptAddress',
                onSubmit: function (param) {
                    param.AProvince = $('#AcceptProvince').combobox('getText');
                    param.ACity = $('#AcceptCity').combobox('getText');
                    param.ACountry = $('#AcceptCountry').combobox('getText');
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
        //新增收货地址
        function addAcceptAddress() {
            $('#dlgAddAcceptAddress').dialog('open').dialog('setTitle', '新增客户收货地址信息');
            $('#fmAddAcceptAddress').form('clear');
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#AClientNum').textbox('setValue', row.ClientNum);
                $('#ADClientID').val(row.ClientID);
            }
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('setValue', "<%= UserInfor.HouseID%>");
        }
        //修改收货地址
        function editAcceptAddress() {

        }
        //修改客户信息
        function editAcceptAddressByID() {
            var rows = $('#dgAcceptAddress').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#fmAddAcceptAddress').form('clear');
                $('#dlgAddAcceptAddress').dialog('open').dialog('setTitle', '修改客户收货地址信息');
                $('#fmAddAcceptAddress').form('load', rows[0]);

                $('#AClientNum').combobox('readonly', true);
            }
        }
        //导出数据
        function AwbExport() {
           <%-- var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#QClientNum').val();
            key[1] = $("#QClientName").val();
            key[2] = $("#QClientShortName").val();
            key[3] = $("#QBoss").val();
            key[4] = '';
            key[5] = $("#QCellphone").val();
            key[6] = $("#QDelFlag").combobox('getValue');
            key[7] = $("#QClientType").combobox('getValue');
            key[8] = $("#HID").combobox('getValue');
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "clientApi.aspx?method=QueryCargoClientForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") {
                        $.messager.progress("close");
                        var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click();
                    }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });--%>
        }
        //客户类型选择方法
        function ctypeChange(item) {
            if (item.value == "2") {
                $('#ClientNum').combogrid('readonly', false);
            } else {
                $('#ClientNum').combogrid('readonly', true);
            }
        }
        //查询客户收货地址数据
        function QueryAcceptAddress() {
            var mOpts = $('#dgAcceptAddress').datagrid('options');
            mOpts.url = 'clientApi.aspx?method=QueryAcceptAddress';
            $('#dgAcceptAddress').datagrid('load', {
                ClientNum: $('#AdClientNum').val(),
                AcceptCompany: $('#AAcceptCompany').val(),
                AcceptPeople: $('#AAcceptPeople').val(),
                AcceptCellphone: $('#AAcceptCellphone').val(),
                HID: $("#AHID").combobox('getValue')
            });
        }
        //新增客户信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增客户信息');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
            $('#HouseID').combobox('setValue', "<%= UserInfor.HouseID%>");
            $('#ClientType').combobox('setValue', "1");
            var houid = "<%= UserInfor.HouseID%>";
            if (houid == "10") {
                $('#UserID').val("2368");
                $('#UserName').val("安鹏");
            }
        }
        //修改客户信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#fm').form('clear');
                $('#dlg').dialog('open').dialog('setTitle', '修改客户信息');
                $('#fm').form('load', row);
                $('#ClientNum').combobox('readonly', true);
                var houid = "<%= UserInfor.HouseID%>";
                if (houid == "10") {
                    $('#UserID').val("2368");
                    $('#UserName').val("安鹏");
                }
            }
        }
        //修改客户信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#fm').form('clear');
                $('#dlg').dialog('open').dialog('setTitle', '修改客户信息');

                $('#fm').form('load', row);

                $('#ClientNum').combobox('readonly', true);
                var houid = "<%= UserInfor.HouseID%>";
                if (houid == "10") {
                    $('#UserID').val("2368");
                    $('#UserName').val("安鹏");
                }
            }
        }

        //删除客户收货地址
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
                        url: 'clientApi.aspx?method=DelAcceptAddress',
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
        //收货地址管理
        function btnAddress() {
            $('#dgAcceptAddress').datagrid('loadData', { total: 0, rows: [] });
            $('#dlgAcceptAddress').dialog('open').dialog('setTitle', '客户收货地址管理');
            $('#AHID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });

            //省市区三级联动
            $('#AcceptProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData&PID=0',
                valueField: 'ID', textField: 'City',
                onSelect: function (rec) {
                    $('#AcceptCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#AcceptCity').combobox('reload', url);
                    //一级仓库
                    $('#AcceptCity').combobox({
                        onSelect: function (fai) {
                            $('#AcceptCountry').combobox('clear');
                            var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + fai.ID;
                            $('#AcceptCountry').combobox('reload', url);
                        }
                    });
                }
            });
            $('#AHID').combobox('setValue', "<%= UserInfor.HouseID%>");
            $('#AHID').combobox('textbox').bind('focus', function () { $('#AHID').combobox('showPanel'); });
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#AdClientNum').textbox('setValue', row.ClientNum);
                //$('#AAcceptCompany').textbox('setValue', row.ClientShortName);
                //$('#AAcceptPeople').textbox('setValue', row.Boss);
                QueryAcceptAddress();
            }
        }
        //删除客户信息
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
                        url: 'clientApi.aspx?method=DelCargoClient',
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

        //保存客户信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'clientApi.aspx?method=SaveCargoClient',
                onSubmit: function (param) {
                    param.AProvince = $('#Province').combobox('getText');
                    param.ACity = $('#City').combobox('getText');
                    param.ACountry = $('#Country').combobox('getText');
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
    </script>

</asp:Content>
