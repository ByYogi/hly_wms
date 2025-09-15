<%@ Page Title="补货单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoveOrderManager.aspx.cs" Inherits="Cargo.Order.MoveOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script src="../JS/easy/js/datagrid-cellediting.js" type="text/javascript"></script> --%>
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
            $.ajaxSetup({ async: true });
            $.getJSON("../Client/clientApi.aspx?method=QueryAllUpClientDep", function (data) {
                UpClientDep = data;
            });
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid("resize", { height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中, 请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 28, //每页多少条
                pageList: [28, 50, 500, 1000, 10000, 100000],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'RplID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'RplID', checkbox: true, width: '30px' },
                    {
                        title: '补货单号', field: 'RplNo', width: '130px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '来源仓库', field: 'FromHouseName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '目标仓库', field: 'HouseName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '补货数量', field: 'Piece', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '开单人', field: 'ReqByName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '补货单状态', field: 'Status', width: '100px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='待处理'>待处理</span>"; }
                            else if (val == "1") { return "<span title='补货中'>补货中</span>"; }
                            else if (val == "2") { return "<span title='已完成'>已完成</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '补货品牌', field: 'TypeNames', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '补货原因', field: 'Reason', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '备注', field: 'Remark', width: '200px'},
                    { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                    { title: '完成时间', field: 'CompletedDate', width: '130px', formatter: DateTimeFormatter },
                    { title: '花费天数', field: 'SpendDays', width: '130px'}
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.MoveStatus == "2") { return "color:#2a83de"; };
                    if (row.MoveStatus == "1" || row.MoveStatus == "3") { return "background-color:#f7e4e0"; };
                    if (row.MoveStatus == "1" || row.MoveStatus == "3" || row.MoveStatus == "4") { return "background-color:#FCF3CF"; };

                },
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            //一级产品
            $('#TypeCate').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#TypeID').combobox('clear');
                    $('#TypeID').combobox({
                        url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                        },
                    });
                }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            <%-- $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); }); --%>

            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
            });
            $('#FromHouse').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#FromHouse').combobox('textbox').bind('focus', function () { $('#FromHouse').combobox('showPanel'); });
            $('#HouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryRplOrder';
            $('#dg').datagrid('load', {
                RplNo: $('#RplNo').val(),
                HouseID: $("#HouseID").combobox('getValue'),
                FromHouse: $("#FromHouse").combobox('getValue'),
                Status: $("#Status").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                TypeCate: $('#TypeCate').datebox('getValue'),
                TypeID: $('#TypeID').datebox('getValue') 
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
                <td style="text-align: right;">补货单号:
                </td>
                <td>
                    <input id="RplNo" class="easyui-textbox" data-options="prompt:'请输入移库单号'" style="width: 120px">
                </td>
                <%-- <td style="text-align: right;">规格:
                </td> --%>
                <%-- <td>
                    <input id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 120px">
                </td> --%>
                <td style="text-align: right;">目标仓库:
                </td>
                <td>
                    <input id="HouseID" class="easyui-combobox" style="width: 120px;" panelheight="auto" />
                </td>
                <td style="text-align: right;">来源仓库:
                </td>
                <td>
                    <input id="FromHouse" class="easyui-combobox" style="width: 120px;" panelheight="auto" />
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 120px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 120px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">移库状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="Status" style="width: 120px;" panelheight="auto">
                        <option value="">全部</option>
                        <option value="0">待处理</option>
                        <option value="1">处理中</option>
                        <option value="2">已完成</option>
                    </select>
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="TypeCate" class="easyui-combobox" style="width: 120px;" panelheight="auto" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="TypeID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid"></table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" id="btnDel" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;
   
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
            <asp:Button ID="btnMoveCode" runat="server" Style="display: none;" Text="导出" OnClick="btnMoveCode_Click" />
        </form>
    </div>


    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 450px; padding: 5px 5px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="HouseID" />
            <table>
                <tr>
                    <td style="text-align: right;">仓库名称:
                    </td>
                    <td>
                        <input name="Name" class="easyui-textbox" data-options="prompt:'请输入仓库名称',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">仓库代码:
                    </td>
                    <td>
                        <input name="HouseCode" class="easyui-textbox" data-options="prompt:'请输入仓库代码',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">仓库负责人:
                    </td>
                    <td>
                        <input name="Person" class="easyui-textbox" data-options="prompt:'请输入仓库负责人',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">仓库类型:
                    </td>
                    <td>
                        <select class="easyui-combobox" name="BelongHouse" id="eBelongHouse" editable="false" style="width: 250px;"
                            panelheight="auto" required="true">
                            <option value="0">迪乐泰</option>
                            <option value="1">好来运</option>
                            <option value="2">富添盛</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">仓库部门:
                    </td>
                    <td>
                        <input name="CargoDepart" id="eCargoDepart" class="easyui-textbox" data-options="prompt:'请输入仓库部门',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">负责人手机:
                    </td>
                    <td>
                        <input name="Cellphone" class="easyui-textbox" style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">发货单表头:
                    </td>
                    <td>
                        <input name="SendTitle" class="easyui-textbox" style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">拣货单表头:
                    </td>
                    <td>
                        <input name="PickTitle" class="easyui-textbox" style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">配送区域:
                    </td>
                    <td>
                        <input name="DeliveryArea" class="easyui-combobox" style="width: 250px;"
                            data-options="url:'../House/houseApi.aspx?method=QueryCityData',valueField:'City',textField:'City',multiple:true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">备注:
                    </td>
                    <td>
                        <textarea id="Remark" rows="4" name="Remark" style="width: 250px;"></textarea>
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
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>


    <div id="dlgOrder" class="easyui-dialog" style="width: 1080px; height: 540px;" closed="true"
        closable="false" buttons="#dlgOrder-buttons">
        <table>
            <tr>
                <td>
                    <input type="hidden" id="HiddenMoveStatus" />
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>

    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOrder').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>

    <!--Begin 查询产品标签列表-->
    <div id="productTag" class="easyui-dialog" style="width: 1100px; height: 520px; padding: 1px"
        closed="true" buttons="#productTag-buttons">
        <table id="dgTag" class="easyui-datagrid">
        </table>
        <div id="productTag-buttons">

            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#productTag').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <!--End 查询产品标签列表-->

    <script type="text/javascript">

        //一键入库
        function LotinCargo() {
            var NewHouseID; var NewAreaID;
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要操作的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                NewHouseID = rows[i].NewHouseID;
                NewAreaID = rows[i].NewAreaID;
                if (rows[i].NewHouseID != 86 && rows[i].NewHouseID != 87 && rows[i].NewHouseID != 90 && rows[i].NewHouseID != 64 && rows[i].NewHouseID != 82) {
                    if (rows[i].NewAreaID != 3822 && rows[i].NewAreaID != 3823 && rows[i].NewAreaID != 3828 && rows[i].NewAreaID != 3833 && rows[i].NewAreaID != 3836 && rows[i].NewAreaID != 3977) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '不允许手工入库！', 'warning'); return;
                    }
                    //$.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '不允许手工入库！', 'warning'); return;
                }

                if (rows[i].MoveStatus == 2) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].MoveNo + '已完成！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定一键入库？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });

                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=MoveOrderHandInCargo',
                        type: 'post', dataType: 'json', data: { data: json, NewHouseID: NewHouseID, NewAreaID: NewAreaID },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '一键入库完成!', 'info');
                                dosearch();
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //查询产品标签数据列表 
        function queryTag() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查询标签的数据！', 'warning');
                return;
            }
            if (row) {
                $('#productTag').dialog('open').dialog('setTitle', '查询移库单：' + row.MoveNo + '标签列表');
                showTagGrid();
                $('#dgTag').datagrid('clearSelections');
                var gridOpts = $('#dgTag').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryMoveScanTagByMoveNo&MoveNo=' + row.MoveNo;
            }
        }

        //标签数据列表
        function showTagGrid() {
            $('#dgTag').datagrid({
                width: '100%',
                height: '440px',
                title: '', //标题内容
                rownumbers: true,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TagCode',
                url: null,
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    { title: '移库单号', field: 'MoveOrderNo', width: '80px' },
                    { title: '产品ID', field: 'ProductID', width: '70px' },
                    { title: '标签编码', field: 'TagCode', width: '100px' },
                    { title: '轮胎码', field: 'TyreCode', width: '80px' },
                    //{ title: '出库时间', field: 'OutCargoTime', width: '120px', formatter: DateTimeFormatter },
                    //{ title: '出库人', field: 'OutCargoOperID', width: '60px' },
                    //{ title: '产品ID', field: 'ProductID', width: '50px' },
                    //{ title: '产品名称', field: 'ProductName', width: '100px' },
                    { title: '规格', field: 'Specs', width: '80px' },
                    { title: '花纹', field: 'Figure', width: '100px' },
                    { title: '批次', field: 'Batch', width: '50px' },
                    { title: '型号', field: 'Model', width: '60px' },
                    { title: '货品代码', field: 'GoodsCode', width: '80px' },
                    { title: '载重指数', field: 'LoadIndex', width: '60px' },
                    { title: '速度级别', field: 'SpeedLevel', width: '60px' },
                    { title: '入库时间', field: 'InCargoTime', width: '120px', formatter: DateTimeFormatter },
                    //{ title: '货位代码', field: 'ContainerCode', width: '80px' }
                    //{ title: '一级区域', field: 'ParentAreaName', width: '60px' },
                    //{ title: '二级区域', field: 'AreaName', width: '60px' }
                ]],
                onClickRow: function (index, row) {
                    $('#dgTag').datagrid('clearSelections');
                    $('#dgTag').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.MoveStatus == "1") { return "background-color: #24d3fb82; "; }
                }
            });
        }
        //根据公司部门ID获取部门名称
        function GetUpClientDepName(value) {
            var name = '';
            if (value != "") {
                for (var i = 0; i < UpClientDep.length; i++) {
                    if (UpClientDep[i].ID == value) {
                        name = UpClientDep[i].DepName;
                        break;
                    }
                }
            }
            return name;
        };
        //导出数据
        function AwbExport() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要导出的数据！', 'warning');
                return;
            }
            var MoveNoStr = "";
            for (var i = 0; i < rows.length; i++) {
                MoveNoStr += rows[i].MoveNo + ",";
            }
            var json = JSON.stringify(MoveNoStr)
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: 'orderApi.aspx?method=QueryMoveOrderForExport',
                data: { data: MoveNoStr },
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
            <%--$.ajax({
                url: "orderApi.aspx?method=QueryMoveOrderForExport&MoveNo=" + rows[0].MoveNo,
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });--%>
        }

        //打印拣货单
        function pickUpOrder() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要打印的数据！', 'warning');
                return;
            }
            if (row) {
                $.messager.progress({ msg: '请稍后,正在查询中...' });
                $.ajax({
                    url: 'orderApi.aspx?method=QueryMoveOrderGoodsList',
                    type: 'post', dataType: 'json', data: { MoveNo: row.MoveNo },
                    success: function (data) {
                        $.messager.progress("close");
                        if (data == null || data == undefined || data.length < 1) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有移库明细数据！', 'warning');
                            return;
                        }
                        massPrint(data, row);
                    }
                });
            }
        }

        function massPrint(griddata, row) {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();
            LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "A4");
            var com = '<%=PickTitle%>'
            var hous = '<%= HouseName%>';
            if (hous.indexOf('广东') != -1) {
                com = griddata[0].FirstAreaName + "拣货单";
            }

            LODOP.ADD_PRINT_TEXT(4, 253, 485, 33, com);
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
            LODOP.ADD_PRINT_TEXT(41, 12, 90, 40, "移库单号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 100, 110, 20, row.MoveNo);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 461, 97, 20, "移库数量：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 542, 56, 20, row.MoveNum);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 605, 90, 20, "联系电话：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            //var tell = $('#AAcceptCellphone').textbox('getValue');
            //if (tell == '' || tell == null) { tell = $('#AAcceptTelephone').textbox('getValue'); }
            //LODOP.ADD_PRINT_TEXT(41, 668, 117, 20, tell);
            LODOP.ADD_PRINT_TEXT(41, 668, 117, 20, "");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


            LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "<img src=\"../CSS/image/dlqf.jpg\"/>");
            //if (hous.indexOf('武汉') != -1) {
            //    com = '湖北省迪乐泰仓库拣货单';
            //} else if (hous.indexOf('广州') != -1) {
            //    com = '好来运广州仓库拣货单';
            //}
            LODOP.ADD_PRINT_RECT(66, 3, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 102, 79, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 181, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 280, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 379, 64, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 443, 69, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 512, 74, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 586, 96, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 682, 106, 25, 0, 1);

            LODOP.ADD_PRINT_TEXT(70, 21, 78, 25, "品牌");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 118, 52, 25, "型号");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 202, 65, 25, "规格");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 292, 53, 25, "花纹");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 383, 61, 25, "速度级别");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 448, 64, 25, "出库件数");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

            LODOP.ADD_PRINT_TEXT(70, 522, 60, 25, "批次");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 590, 88, 25, "货位代码");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 684, 72, 25, "所在区域");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

            LODOP.ADD_PRINT_TEXT(41, 275, 90, 20, "目标仓库：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            //LODOP.ADD_PRINT_TEXT(41, 330, 200, 20, $('#ADest').combobox('getText') + " 物流：" + $('#ALogisID').combobox('getText'));
            LODOP.ADD_PRINT_TEXT(41, 362, 75, 20, row.NewHouseName);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            var js = 0, Alltotal = 0, AllPiece = 0;
            for (var i = 0; i < griddata.length; i++) {
                js++;
                LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 102, 79, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 181, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 280, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 379, 64, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 443, 69, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 512, 74, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 586, 96, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 682, 106, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 111, 23, griddata[i].TypeName);//产品名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 105, 80, 20, griddata[i].Model);//型号
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 185, 94, 20, griddata[i].Specs);//规格 
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 286, 110, 20, griddata[i].Figure);//花纹
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 386, 82, 20, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 450, 51, 20, griddata[i].Piece);//数量出库件数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 520, 100, 20, griddata[i].Batch);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 590, 106, 20, griddata[i].ContainerCode);//货位代码
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 684, 100, 20, griddata[i].FirstAreaName);//所在区域
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 50, 102, 23, "备注：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 150, 400, 23, row.Memo);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            }
            //LODOP.PRINT_DESIGN();
            LODOP.PREVIEW();
        }
        //修改仓库信息
        function editItem() {
           <%-- var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改仓库信息');
                $('#fm').form('load', row);
            }--%>
        }

        //双击显示订单详细界面 
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            $("#HiddenMoveStatus").val(row.MoveStatus);
            if (row) {
                $('#dlgOrder').dialog('open').dialog('setTitle', row.MoveNo + '移库明细');
                var columns = [];

                columns.push({
                    title: '产品代码', field: 'ProductCode', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品名称', field: 'GoodsName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '数量', field: 'Piece', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '单位', field: 'UnitName', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
<%-- public int ID { get; set; }                  // 主键
public int RplID { get; set; }               // 补货单ID
public long ProductID { get; set; }          // 产品ID
public string ProductName { get; set; }      // 产品名称
public string ProductCode { get; set; }      // 产品代码
public string GoodsCode { get; set; }        // 货品代码
public int TypeCate { get; set; } // 品类ID
public int TypeID { get; set; }              // 品牌ID
public string TypeName { get; set; }         // 品牌名称
public int Piece { get; set; }               // 补货数量
public int SysPiece { get; set; }            // 建议补货数
public int? MinStock { get; set; }           // 最小库存数
public int? MaxStock { get; set; }           // 最大库存数
public int? SrcPiece { get; set; }           // 缺货仓在库数
public int? PenddimgQty { get; set; }        // 待出库数量
public int? InTransitQty { get; set; }       // 在途库存
public int? AvgSalSUM { get; set; }          // 月均销量
public DateTime CreateDate { get; set; }     // 创建时间
public DateTime? UpdateDate { get; set; }    // 更新时间 --%>
                );
                columns.push({
                    title: '移库数量', field: 'Piece', width: '60px', align: 'right', editor: { type: 'numberbox', options: { precision: 0 } }, styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                //columns.push({
                //    title: '扫描数量', field: 'ScanNum', width: '60px', formatter: function (value) {
                //        return "<span title='" + value + "'>" + value + "</span>";
                //    }
                //});
                columns.push({
                    title: '已入数量', field: 'NewPiece', width: '60px', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '品牌', field: 'TypeName', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '货品代码', field: 'GoodsCode', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                        return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                    }
                });
                //columns.push({
                //    title: '速度级别', field: 'SpeedLevel', width: '60px', formatter: function (value) {
                //        return "<span title='" + value + "'>" + value + "</span>";
                //    }
                //});
                columns.push({
                    title: '所在区域', field: 'FirstAreaName', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '所在仓库', field: 'HouseName', width: '80px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '销售价', field: 'SalePrice', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                //columns.push({
                //    title: '产品名称', field: 'ProductName', width: '100px', formatter: function (value) {
                //        return "<span title='" + value + "'>" + value + "</span>";
                //    }
                //});
                //columns.push({
                //    title: '归属部门', field: 'BelongDepart', width: '100px', formatter: function (value) {
                //        return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                //    }
                //});
                columns.push({
                    title: '单价', field: 'UnitPrice', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品来源', field: 'SourceName', width: '100px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                showGrid(columns);

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryRplOrderGoods&RplID=' + row.RplID;
            }
        }
        //显示列表
        function showGrid(dgSaveCol) {
            $('#dgSave').datagrid({
                width: '1050px',
                height: '450px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                onClickCell: onClickCell,
                columns: [dgSaveCol],
                rowStyler: function (index, row) {
                    if (row.Piece != row.NewPiece) { return "background-color:#f7d9d9"; };
                }
            });
        }
        //删除仓库信息
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
                        url: 'orderApi.aspx?method=DelMoveOrder',
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

        //保存仓库信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'houseApi.aspx?method=SaveCargoHouse',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
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
        var IsModifyOrder = false;
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgSave').datagrid('validateRow', editIndex)) {
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) { return false; }
                if (cg.field == "Piece") {
                    //修改件数
                    var oldPiece = Number(rows.Piece);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateMoveOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            $.messager.progress("close");
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            if ($("#HiddenMoveStatus").val() == "1" || $("#HiddenMoveStatus").val() == "2" || $("#HiddenMoveStatus").val() == "4") { return; }
            var rows = $("#dgSave").datagrid('getData').rows[index];
            if (field == "Piece") {
                if (endEditing()) {
                    $('#dgSave').datagrid('selectRow', index)
                        .datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            } else {
                if (editIndex == undefined) { return true }
                rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) {
                    return true;
                }
                var sum = 0;
                if (cg.field == "Piece") {
                    var oldPiece = Number(rows.Piece);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;

                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateMoveOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }


        //批量导出移库订单信息
        function MoveExportOrderInfo() {
            $.messager.progress({ msg: '请稍后,正在导出...' });
            if ($("#OldHouseID").combobox('getValue') === undefined) {
                $('#OldHouseID').combobox('setValue', '');
            }
            $.ajax({
                url: 'orderApi.aspx?method=QueryMoveOrderDataExport&MoveNo=' + $('#MoveNo').val() + "&Specs=" + $('#Specs').val() + "&OldHouseID=" + $("#OldHouseID").combobox('getValue') + "&NewHouseID=" + $('#NewHouseID').combobox('getValue') + "&MoveStatus=" + $('#MoveStatus').combobox('getValue') + "&AUserName=" + $('#AUserName').val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&APID=" + $('#APID').combobox('getValue') + "&ASID=" + $('#ASID').combobox('getValue'),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnMoveCode.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }

    </script>

</asp:Content>
