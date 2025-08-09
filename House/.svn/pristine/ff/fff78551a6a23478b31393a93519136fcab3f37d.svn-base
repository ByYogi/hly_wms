<%@ Page Title="仓库退货单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HouseReturnOrder.aspx.cs" Inherits="Supplier.House.HouseReturnOrder" %>

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
            //$.ajaxSetup({ async: true });
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
        }
        $(window).resize(function () {
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
        });
        function adjustment() {
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height - 20 });
            $('#orderGoodsDg').datagrid({ height: (Number($(window).height()) - 10) - height  });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: '', checkbox: true, width: '30px' });
            columns.push({
                title: '退货单号', field: 'FacOrderNo', width: '150px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '总件数', field: 'Piece', width: '80px' });
            columns.push({ title: '合计总费用', field: 'TotalCharge', width: '100px' });
            columns.push({ title: '退货员', field: 'CreateAwb', width: '150px' });
            columns.push({ title: '所在仓库', field: 'HouseName', width: '80px' });
            columns.push({
                title: '收货状态', field: 'ReceivingStatus', width: '90px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未收货'>未收货</span>"; }
                    else if (val == "1") { return "<span title='已收货'>已收货</span>"; }
                    else if (val == "2") { return "<span title='部分收货'>部分收货</span>"; }
                    else { return ""; }
                }
            });
            columns.push({ title: '退货备注', field: 'Remark', width: '260px' });
            columns.push({ title: '退货时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter });

            //退仓单主表
            $('#dg').datagrid({
                width: 'auto',
                height: '350px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                rowStyler: function (index, row) {
                    if (row.IsModifyPrice == "1") { return "background-color:#D1EEEE"; }
                },
                onClickRow: function (index, row) {//当用户点击一行时触发
                    //初始赋值
                    $('#HouseID').val(row.HouseID);
                    $('#ClientNum').val(row.ClientNum);
                    $('#FacOrderNo').val(row.FacOrderNo);
                    $('#ReceivingStatus').val(row.ReceivingStatus);
                    $('#OrderID').val(row.OrderID);

                    $('#dg').datagrid('clearSelections');
                    $('#orderGoodsDg').datagrid('clearSelections');
                    var gridOpts = $('#orderGoodsDg').datagrid('options');
                    gridOpts.url = 'houseApi.aspx?method=QueryAllHouseReturnOrderGoods';
                    $('#orderGoodsDg').datagrid('load', { OrderID: row.OrderID });
                    $('#dg').datagrid('selectRow', index);
                },
            });
            //退仓订单与产品关联表列表
            $('#orderGoodsDg').datagrid({
                width: '100%',
                title: '退仓产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#toolbarGoods',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    {
                        title: '产品编码', field: 'ProductCode', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '品牌', field: 'TypeName', width: '120px' },
                    { title: '退货件数', field: 'ReturnPiece', width: '100px', editor: { type: 'numberbox' } },//可编辑单元格

                    {
                        title: '规格', field: 'Specs', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载速', field: 'LoadSpeed', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产地', field: 'Born', width: '80px', formatter: function (value) {
                            if (value == "0") { return "<span title='国产'>国产</span>"; }
                            else if (value == "1") { return "<span title='进口'>进口</span>"; } else { return ""; }
                        }
                    }, {
                        title: '批次', field: 'Batch', width: '90px'
                    },
                    {
                        title: '销售价', field: 'SalePrice', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                onLoadSuccess: function (data) { },//加载完事件
            });

            //仓库
            var topMenuNew = [];
            var SettleHouseID = '<%= UserInfor.SettleHouseID%>'.split(',');
            var SettleHouseName = '<%= UserInfor.SettleHouseName%>'.split(',');
            for (var i = 0; i < SettleHouseID.length; i++) {
                topMenuNew.push({ "text": SettleHouseName[i], "id": SettleHouseID[i] });
            }
            $("#AHouseID").combobox("loadData", topMenuNew);
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', "<%= UserInfor.SettleHouseID%>");

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

        })     

        //查询退仓订单
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'houseApi.aspx?method=QueryAllPageHouseReturnOrders';
            $('#dg').datagrid('load', {
                FacOrderNo: $('#AFacOrderNo').textbox('getValue'),//退货单号
                AcceptUnit: $('#AAcceptUnit').val(),//客户名称
                ReceivingStatus: $('#AReceivingStatus').combobox('getValue'),//收货状态
                HID: $("#AHouseID").combobox('getValue'),//区域大仓
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">退货单号:
                </td>
                <td>
                    <input id="AFacOrderNo" class="easyui-textbox" data-options="prompt:'请输入退货单号',required:false" style="width: 120px" />
                </td>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input class="easyui-textbox" id="AAcceptUnit" style="width: 100px;" />
                </td>
                <td style="text-align: right;">收货状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AReceivingStatus" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">未收货</option>
                        <option value="1">已收货</option>
                        <option value="2">部分收货</option>
                    </select>
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'id',textField:'text',editable:false,required:true" />
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
      <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="orderGoodsDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
          <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="exportData()">&nbsp;导出&nbsp;</a>&nbsp;&nbsp;
          <a href="#" class="easyui-linkbutton" iconcls="icon-table_edit" plain="false" onclick="deliveryBtn()">&nbsp;确认收货&nbsp;</a>&nbsp;&nbsp;
          <form runat="server" id="fm1">
              <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
              <asp:Button ID="btnDerived2" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click2" />
          </form>
    </div>
    <div id="toolbarGoods">
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="exportDataGoods()">&nbsp;导出&nbsp;</a>&nbsp;&nbsp;
    </div>
    <input type="hidden" name="OrderID" id="OrderID" />
    <script type="text/javascript">
        //导出退货仓主表
        function exportData() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "houseApi.aspx?method=ExportHouseReturnOrdersInfo&FacOrderNo=" + $("#AFacOrderNo").val() + "&AcceptUnit=" + $("#AAcceptUnit").val() + "&ReceivingStatus=" +
                    $("#AReceivingStatus").combobox('getValue') + "&HID=" + $('#AHouseID').combobox('getValue') + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue'),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
            $.messager.progress("close");
        }
        //导出退货仓产品明细表
        function exportDataGoods() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "houseApi.aspx?method=ExportHouseReturnOrderGoods&OrderID=" + $("#OrderID").val(),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived2.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
            $.messager.progress("close");
        }



        //批量保存 收货
        function deliveryBtn() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要收货的数据！', 'warning'); return; }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].ReceivingStatus != 0) {
                    $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', rows[i].FacOrderNo + '已收货无法再次收货！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', '确定收货？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows);
                    $.ajax({
                        url: "houseApi.aspx?method=UpdatePurchaseOrderStatus",
                        type: 'post',
                        dataType: 'json',
                        data: {
                            data: json
                        },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#orderGoodsDg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
    </script>
</asp:Content>
