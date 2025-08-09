<%@ Page Title="进仓单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderManager.aspx.cs" Inherits="Cargo.Order.PurchaseOrderManager" %>

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
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {

            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height });
            $('#orderGoodsDg').datagrid({ height: (Number($(window).height()) - 90) - height });
        }
        $(document).ready(function () {
            var columns = [];
           
            columns.push({ title: '', field: '', checkbox: true, width: '30px' });
            columns.push({
                title: '进货单号', field: 'FacOrderNo', width: '150px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '总数量', field: 'Piece', width: '80px' });
            columns.push({ title: '合计总费用', field: 'TotalCharge', width: '100px' });
            columns.push({ title: '客户编码', field: 'ClientNum', width: '90px' });
            columns.push({ title: '客户名称', field: 'AcceptUnit', width: '150px' });
            columns.push({ title: '客户联系人', field: 'AcceptPeople', width: '90px' });
            columns.push({ title: '客户联系电话', field: 'AcceptTelephone', width: '120px' });
            columns.push({ title: '客户手机', field: 'AcceptCellphone', width: '120px' });
            columns.push({ title: '所在仓库', field: 'HouseName', width: '80px' });
            columns.push({
                title: '收货状态', field: 'ReceivingStatus', width: '90px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未收货'>未收货</span>"; }
                    else if (val == "1") { return "<span title='已收货'>已收货</span>"; }
                    else if (val == "2") { return "<span title='部分收货'>部分收货</span>"; }
                    else { return ""; }
                }
            });
            columns.push({ title: '备注', field: 'Remark', width: '260px' });
            columns.push({ title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter });

            //进仓单主表
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
                idField: 'FacOrderNo',
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
                    $('#AcceptUnit').val(row.AcceptUnit);
                    $('#FacOrderNo').val(row.FacOrderNo);
                    $('#ReceivingStatus').val(row.ReceivingStatus);

                    $('#dg').datagrid('clearSelections');
                    $('#orderGoodsDg').datagrid('clearSelections');
                    var gridOpts = $('#orderGoodsDg').datagrid('options');
                    gridOpts.url = 'orderApi.aspx?method=QueryAllPurcgaseOrderGoods';
                    $('#orderGoodsDg').datagrid('load', { OrderID: row.OrderID });
                    $('#dg').datagrid('selectRow', index);
                },
            });
            //进仓订单与产品关联表列表
            $('#orderGoodsDg').datagrid({
                width: '100%',
                title: '进仓产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    {
                        title: '产品编码', field: 'ProductCode', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '品牌', field: 'TypeName', width: '100px' },
                    { title: '来货条数', field: 'Piece', width: '80px', },
                    { title: '收货条数', field: 'ReceivePiece', width: '80px', editor: { type: 'numberbox' } },//可编辑单元格
                    { title: '退货条数', field: 'ReturnPiece', width: '80px', editor: { type: 'numberbox' } },//可编辑单元格
                    {
                        title: '规格', field: 'Specs', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    //{
                    //    title: '货品代码', field: 'GoodsCode', width: '150px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                    {
                        title: '载重', field: 'LoadIndex', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '速度', field: 'SpeedLevel', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产地', field: 'Born', width: '80px', formatter: function (value) {
                            if (value == "0") { return "<span title='国产'>国产</span>"; }
                            else if (value == "1") { return "<span title='进口'>进口</span>"; } else { return ""; }
                        }
                    },
                    { title: '周期批次', field: 'Batch', width: '90px' },
                    { title: '周期年', field: 'BatchYear', width: '90px' },
                    {
                        title: '销售价', field: 'SalePrice', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '操作时间', field: 'OP_DATE', width: '150px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },//加载完事件
                onClickCell: onClickCell//单击单元格事件
            });

            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                }
            });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
            $('#HID').combobox('setValue', '<%=UserInfor.HouseID%>');

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

        })
        $.extend($.fn.datagrid.methods, {
            editCell: function (jq, param) {
                return jq.each(function () {
                    var fields = $(this).datagrid('getColumnFields');
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor1 = col.editor;
                        if (fields[i] != param.field) {
                            col.editor = null;
                        }
                    }
                    $(this).datagrid('beginEdit', param.index);
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor = col.editor1;
                    }
                });
            }
        });

        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#orderGoodsDg').datagrid('validateRow', editIndex)) {
                var rows = $("#orderGoodsDg").datagrid('getData').rows[editIndex];
                var ed = $('#orderGoodsDg').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg) {
                    if (cg.field == "ReceivePiece") {
                        //收货件数
                        //rows.ReceivePiece = cg.target.val();
                    }
                    else if (cg.field == "ReturnPiece") {
                        //退货件数
                    }
                    $('#orderGoodsDg').datagrid('endEdit', editIndex);
                    editIndex = undefined;
                    return true;
                }
            } else {
                return false;
            }
        }
        //点击进仓订单  单击单元格事件 判断是否有要修改价格单元格
        function onClickCell(index, field) {
            var status = $("#ReceivingStatus").val();
            if (field == "ReceivePiece" && status == 0) {
                if (endEditing()) {
                    $('#orderGoodsDg').datagrid('selectRow', index).datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            }
            else if (field == "ReturnPiece" && status == 0) {
                if (endEditing()) {
                    $('#orderGoodsDg').datagrid('selectRow', index).datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            }
            else {
                if (editIndex == undefined) { return true }
                var rows = $("#orderGoodsDg").datagrid('getData').rows[editIndex];
                var ed = $('#orderGoodsDg').datagrid('getEditors', editIndex);
                var cg = ed[0];
                var sum = 0;
                //if (cg.field == "ReceivePiece") { }
                //if (cg.field == "ReturnPiece") { }
                $('#orderGoodsDg').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }

        //查询进仓订单
        function dosearch() {
            console.log('testSearch')
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryAllPagePurcgaseOrders';
            $('#dg').datagrid('load', {
                FacOrderNo: $('#AFacOrderNo').textbox('getValue'),//进货单号
                ClientNum: $('#AClientNum').val(),//客户编码
                AcceptUnit: $('#AAcceptUnit').val(),//客户名称
                ReceivingStatus: $('#AReceivingStatus').combobox('getValue'),//收货状态
                HID: $("#HID").combobox('getValue'),//区域大仓
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
                <td style="text-align: right;">进货单号:
                </td>
                <td>
                    <input id="AFacOrderNo" class="easyui-textbox" data-options="prompt:'请输入进货单号',required:false" style="width: 120px" />
                </td>
                <td style="text-align: right;">客户编码:
                </td>
                <td>
                    <input id="AClientNum" class="easyui-textbox" data-options="prompt:'请输入客户编码'" style="width: 120px" />
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
                    <input id="HID" class="easyui-combobox" style="width: 100px;" />
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
    </div>

    <input type="hidden" name="HouseID" id="HouseID" />
    <input type="hidden" name="FacOrderNo" id="FacOrderNo" />
    <input type="hidden" name="ClientNum" id="ClientNum" />
    <input type="hidden" name="AcceptUnit" id="AcceptUnit" />
    <input type="hidden" name="ReceivingStatus" id="ReceivingStatus" />
    <div id="saPanel">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right;">收货备注:
                </td>
                <td>
                    <textarea name="returnRemark" id="returnRemark" placeholder="请输入收货备注" style="width: 70%; resize: none"></textarea>
                </td>
                <td style="text-align: center;">
                    <a href="#" class="easyui-linkbutton" id="saveBtn" iconcls="icon-ok" onclick="savePiece()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        //保存件数
        function savePiece() {
            debugger;
            if (editIndex != undefined) {
                if ($('#orderGoodsDg').datagrid('validateRow', editIndex)) {
                    var editors = $('#orderGoodsDg').datagrid('getEditors', editIndex);
                    if (editors.length > 0) {
                        if (editors[0].field == "ReceivePiece") {
                            $('#orderGoodsDg').datagrid('getRows')[editIndex]['ReceivePiece'] = $(editors[0].target).val();
                        } else if (editors[0].field == "ReturnPiece") {
                            $('#orderGoodsDg').datagrid('getRows')[editIndex]['ReturnPiece'] = $(editors[0].target).val();
                        }
                    }
                }
                $('#orderGoodsDg').datagrid('endEdit', editIndex);
            }
            var rows = $('#orderGoodsDg').datagrid('getRows');
            var orderRows = $('#dg').datagrid('getSelected');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows);
                    var arrays = JSON.parse(json);
                    var tempData = {
                        List: arrays,
                        AcceptUnit: $('#AcceptUnit').val(),
                        ClientNum: $('#ClientNum').val(),
                        FacOrderNo: $('#FacOrderNo').val(),
                        HouseID: $('#HouseID').val(),
                        PurchaseOrder: orderRows,
                        returnRemark: $('#returnRemark').val(),
                    }
                    var orderData = JSON.stringify(tempData);
                    $.ajax({
                        url: "orderApi.aspx?method=UpdatePurchaseOrderGoodsPiece",
                        type: 'post',
                        dataType: 'json',
                        data: { orderData },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#orderGoodsDg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //重置全部table
        function reset() {
            $('#dg').datagrid('loadData', { total: 0, rows: [] });
            $('#orderGoodsDg').datagrid('loadData', { total: 0, rows: [] });
        }

    </script>
</asp:Content>
