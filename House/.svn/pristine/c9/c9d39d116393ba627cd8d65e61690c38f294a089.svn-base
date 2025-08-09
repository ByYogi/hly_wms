<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="orderReturnManager.aspx.cs" Inherits="Dealer.Antyres.orderReturnManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
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
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    {
                        title: '退货单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '退货数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#f1866b;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '退货原因', field: 'Remark', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系人', field: 'AcceptPeople', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系手机', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                    {
                        title: '审核状态', field: 'FinanceSecondCheck', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未审'>未审</span>"; }
                            else if (val == "1") { return "<span title='已审'>已审</span>"; }
                            else { return ""; }
                        }
                    }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'FormService.aspx?method=QueryOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                Piece: $("#Piece").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                CheckOutType: '',
                OrderModel: "1"//订单类型
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
                <td style="text-align: right;">退货单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入退货单号'" style="width: 100px"/>
                </td>
                <td style="text-align: right;">退货数量:
                </td>
                <td>
                    <input id="Piece" class="easyui-textbox" data-options="prompt:'请输入退货件数'" style="width: 100px"/>
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px"/>~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px"/>
                </td>
            </tr>
            <tr>
                <td colspan="10">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <div id="dlgOrder" class="easyui-dialog" style="width: 1020px; height: 500px;" closed="true"
        closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" class="easyui-form" method="post">
            <input type="hidden" name="SaleManName" id="SaleManName" />
            <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
            <input type="hidden" name="HouseCode" id="HouseCode" />
            <input type="hidden" name="HouseID" id="HouseID" />
            <input type="hidden" name="ONum" id="ONum" />
            <input type="hidden" name="OutNum" id="OutNum" />
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <div id="saPanel">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right;">客户名称:
                        </td>
                        <td>
                            <input name="AcceptUnit" id="AAcceptUnit" class="easyui-textbox" style="width: 80px;" />
                        </td>

                        <td style="text-align: right;">退货地址:
                        </td>
                        <td>
                            <input name="AcceptAddress" id="AAcceptAddress" style="width: 100px;" class="easyui-textbox" />
                        </td>
                        <td style="text-align: right;">电话:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">手机:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:true" style="width: 100px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">退货数量:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">退货费用:
                        </td>
                        <td>
                            <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">开单时间:
                        </td>
                        <td colspan="3">
                            <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime" readonly="true" style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 400px;"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </form>

        <table id="dgSave" class="easyui-datagrid">
        </table>

    </div>
    <div id="dlgOrder-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <script type="text/javascript">
        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].FinanceSecondCheck != 0) {
                    $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已审核无法删除！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Dealer.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'FormService.aspx?method=DelReturnOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //关闭
        function closedgShowData() {
            $('#dlgOutDealer').dialog('close');
        }
        //双击显示订单详细界面
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgOrder').dialog('open').dialog('setTitle', '查看退货单：' + row.OrderNo);
                $('#fmDep').form('clear');
                $('#fmDep').form('load', row);
                showGrid();
                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'FormService.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
            }
        }
        //显示列表
        function showGrid() {
            $('#dgSave').datagrid({
                width: '100%',
                height: '310px',
                title: '退货产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '退货数量', field: 'Piece', width: '40px', editor: { type: 'numberbox' } },
                    { title: '关联订单', field: 'RelateOrderNo', width: '100px' },
                    { title: '产品品牌', field: 'TypeName', width: '60px' },
                    { title: '货品代码', field: 'GoodsCode', width: '70px' },
                    { title: '规格', field: 'Specs', width: '80px' },
                    { title: '花纹', field: 'Figure', width: '100px' },
                    //{ title: '型号', field: 'Model', width: '80px' },
                    { title: '载重', field: 'LoadIndex', width: '40px' },
                    { title: '速度', field: 'SpeedLevel', width: '40px' },
                    { title: '周期年', field: 'BatchYear', width: '50px', formatter: function (val, row, index) { return val } },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                groupField: 'TypeParentName',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value;
                }
            });
        }
        //关闭弹出框
        function closeDlg() {
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
    </script>
</asp:Content>
