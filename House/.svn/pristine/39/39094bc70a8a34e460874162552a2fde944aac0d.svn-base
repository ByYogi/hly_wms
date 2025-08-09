<%@ Page Title="仓库到货订单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HouseArrivalOrderManager.aspx.cs" Inherits="Cargo.Order.HouseArrivalOrderManager" %>

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
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({
                title: '业务员', field: 'SaleManName', width: '75px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '开单员ID', field: 'CreateAwbID', width: '60px', hidden: 'true', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单状态', field: 'AwbStatus', width: '80px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                    else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                    else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                    else if (val == "3") { return "<span title='运输中'>运输中</span>"; }
                    else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                    else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                    else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                    else if (val == "7") { return "<span title='配送中'>配送中</span>"; }
                    else if (val == "8") { return "<span title='已到货'>已到货</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '开单员', field: 'CreateAwb', width: '75px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单备注', field: 'Remark', width: '300px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter });
            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#dgtoolbar',
                frozenColumns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    {
                        title: '出库仓库', field: 'OutHouseName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '订单号', field: 'OrderNo', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '到达站', field: 'Dest', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '件数', field: 'Piece', width: '55px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '合计费用', field: 'TotalCharge', width: '100px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户名称', field: 'PayClientName', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                columns: [columns],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.AwbStatus != "4") { return "background-color:#FCF3CF"; };
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryHouseArrivalOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                LogisAwbNo: $('#LogisAwbNo').val(),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue')
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" />
    <table>
        <tr>
            <td style="text-align: right;">订单号:
            </td>
            <td>
                <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px" />
            </td>
            <td style="text-align: right;">物流单号:
            </td>
            <td>
                <input id="LogisAwbNo" class="easyui-textbox" data-options="prompt:'请输入物流单号'" style="width: 100px" />
            </td>
            <td style="text-align: right;">订单状态:
            </td>
            <td>
                <select class="easyui-combobox" id="AAwbStatus" style="width: 100px;" panelheight="auto" editable="false">
                <option value="-1">全部</option>
                <option value="0">已下单</option>
                <option value="6">已拣货</option>
                <option value="1">出库中</option>
                <option value="2">已出库</option>
                <option value="3">已装车</option>
                <option value="5">已签收</option>
                <option value="7">配送中</option>
                <option value="8">已到货</option>
                </select>
            </td>
            <td style="text-align: right;">开单时间:
            </td>
            <td colspan="3">
                <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
            </td>
        </tr>
    </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="postpone" class="easyui-linkbutton" iconcls="icon-basket_put" plain="false" onclick="ConfirmReceipt()">&nbsp;确认收货&nbsp;</a>&nbsp;&nbsp;
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 90%; height: 555px;" closed="true"
        closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <div id="saPanel">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">客户名称:</td>
                        <td>
                            <input name="PayClientNum" id="APayClientNum" style="width: 85px;" class="easyui-textbox" /></td>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AAcceptPeople" style="width: 85px;" class="easyui-textbox" />
                        </td>
                        <td style="text-align: right;">公司名称:
                        </td>
                        <td>
                            <input name="AcceptUnit" id="AAcceptUnit" data-options="disabled:false" class="easyui-textbox" style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">收货地址:
                        </td>
                        <td colspan="3">
                            <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:false" style="width: 80%;" class="easyui-textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">销售费用:
                        </td>
                        <td>
                            <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" readonly="true" class="easyui-numberbox" style="width: 85px;" />
                        </td>
                        <td style="text-align: right; display: none">其它费用:
                        </td>
                        <td style="display: none">
                            <input name="OtherFee" id="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 85px;" />
                        </td>
                        <td style="text-align: right;">物流费用:
                        </td>
                        <td>
                            <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 85px;" />
                        </td>
                        <td style="text-align: right;">费用合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2" readonly="true" style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">电话号码:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AAcceptTelephone" data-options="disabled:false" class="easyui-textbox" style="width: 85px;" />
                        </td>
                        <td style="text-align: right;">手机号码:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" data-options="disabled:false" class="easyui-textbox" style="width: 85px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 85px;" readonly="true" />
                        </td>
                        <td style="text-align: right;">开单员:
                        </td>
                        <td>
                            <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="true" style="width: 85px;" />
                        </td>
                        <td style="text-align: right;">开单时间:
                        </td>
                        <td colspan="1">
                            <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime" readonly="true" style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">物流单号:
                        </td>
                        <td>
                            <input name="LogisAwbNo" id="ALogisAwbNo" class="easyui-textbox" style="width: 85px;" />
                        </td>
                        <td style="text-align: right;">到货仓库:</td>
                        <td>
                            <input id="ThrowID" name="TranHouse" class="easyui-textbox" style="width: 85px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="9" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 95%; resize: none"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table>
            <tr>
                <td>
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
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
        function ConfirmReceipt() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要确认的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].AwbStatus == 8) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已到货无法再次到货！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定订单已到货？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在操作中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=ConfirmReceipt',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '到货成功!', 'info');
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
        //双击显示订单详细界面 
        function editItemByID(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            var row = $("#dg").datagrid('getData').rows[Did];
            rowHouseID = row.HouseID;
            if (row) {
                $('#dlgOrder').dialog('open').dialog('setTitle', '订单明细：' + row.OrderNo);
                $('#fmDep').form('clear');
                $('#fmDep').form('load', row);
                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
                columns.push({ title: '到货件数', field: 'Piece', width: '60px' });
                columns.push({ title: '到货价格', field: 'ActSalePrice', width: '60px' });
                columns.push({ title: '型号', field: 'Model', width: '90px' });
                columns.push({ title: '规格', field: 'Specs', width: '90px' });
                columns.push({ title: '花纹', field: 'Figure', width: '100px' });
                columns.push({ title: '批次', field: 'Batch', width: '50px' });
                columns.push({ title: '载重', field: 'LoadIndex', width: '50px' });
                columns.push({ title: '速度', field: 'SpeedLevel', width: '50px' });
                columns.push({ title: '品牌', field: 'TypeName', width: '80px' });
                columns.push({ title: '货品代码', field: 'GoodsCode', width: '90px' });
                columns.push({
                    title: '归属部门', field: 'BelongDepart', width: '100px', formatter: function (value) {
                        return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                    }
                });
                columns.push({
                    title: '规格类型', field: 'SpecsType', width: '90px', formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='分配规格'>分配规格</span>"; }
                        else if (val == "1") { return "<span title='普通规格'>普通规格</span>"; }
                        else if (val == "2") { return "<span title='特价分配规格'>特价分配规格</span>"; }
                        else if (val == "3") { return "<span title='手工单'>手工单</span>"; }
                        else { return ""; }
                    }
                });
                $("#APayClientNum").textbox("setValue", row.PayClientName);
                showGrid(columns);
                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
            }
        }
        //显示列表
        function showGrid(dgSaveCol) {
            var dgSavewidth = Number($("#dlgOrder").width()) - 10;
            $('#dgSave').datagrid({
                width: dgSavewidth,
                height: 310,
                title: '到货产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                columns: [dgSaveCol],
                toolbar: '',
            });
        }
        //关闭弹出框
        function closeDlg() {
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
    </script>
</asp:Content>
