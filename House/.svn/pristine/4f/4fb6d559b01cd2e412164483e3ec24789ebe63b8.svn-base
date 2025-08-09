<%@ Page Title="渠道订单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChannelOrders.aspx.cs" Inherits="Supplier.Order.ChannelOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <script src="../JS/easy/js/datagrid-cellediting.js"></script>
    <style type="text/css">
        .commTblStyle_8 th {
            border: 1px solid rgb(205, 205, 205);
            text-align: center;
            color: rgb(255, 255, 255);
            line-height: 28px;
            background-color: rgb(15, 114, 171);
        }

        .commTblStyle_8 tr.BlankRow td {
            line-height: 10px;
        }

        .commTblStyle_8 tr td {
            border: 1px solid rgb(205, 205, 205);
            text-align: center;
            line-height: 20px;
        }

            .commTblStyle_8 tr td.left {
                text-align: right;
                padding-right: 10px;
                font-weight: bold;
                white-space: nowrap;
                background-color: rgb(239, 239, 239);
            }

            .commTblStyle_8 tr td.right {
                text-align: left;
                padding-left: 10px;
            }

        .commTblStyle_8 .whiteback {
            background-color: rgb(255, 255, 255);
        }
    </style>
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
            $('#up').hide();
            $('#save').hide();
            document.body.style.overflow = 'auto';
        }
        $(window).resize(function () {
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
        });
      
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({
                title: '件数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '总价', field: 'TransportFee', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '客户名称', field: 'AcceptUnit', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '收货人', field: 'AcceptPeople', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '联系电话', field: 'AcceptCellphone', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '收货地址', field: 'AcceptAddress', width: '20%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '发货方式', field: 'DeliveryType', width: '110px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='配送'>配送</span>"; }
                    else if (val == "1") { return "<span title='自提'>自提</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '所属仓库', field: 'OutHouseName', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单状态', field: 'AwbStatus', width: '80px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                    else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                    else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                    else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                    else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                    else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                    else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                    else if (val == "7") { return "<span title='正在配送'>正在配送</span>"; }
                    else { return ""; }
                }
            });

            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50, 200],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#dgtoolbar',
                frozenColumns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                    {
                        title: '渠道单号', field: 'OrderNo', width: '90px', formatter: function (value) {
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
                    if (row.TrafficType == "2") { return "background-color:#b3ce7e"; };
                    if (row.ThrowGood == "17") { return "background-color:#f38f8f"; };
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '10%' });
            columns.push({
                title: '产品编码', field: 'ProductCode', width: '180px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品品牌', field: 'TypeName', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '180px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '周期年', field: 'BatchYear', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '库存数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'SalePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            $('#dgAdd').datagrid({
                width: '100%',
                height: '240px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbarAdd',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { plOutCargo(); },
                rowStyler: function (index, row) {
                }
            });
            columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '10%' });
            columns.push({
                title: '产品编码', field: 'ProductCode', width: '180px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品品牌', field: 'TypeName', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '180px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '周期年', field: 'BatchYear', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'ActSalePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //出库列表
            $('#outDgAdd').datagrid({
                width: '100%',
                height: '240px',
                title: '订单产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); },
                rowStyler: function (index, row) {
                }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            var topMenuNew = [];
            var SettleHouseID = '<%= UserInfor.SettleHouseID%>'.split(',');
            var SettleHouseName = '<%= UserInfor.SettleHouseName%>'.split(',');
            for (var i = 0; i < SettleHouseID.length; i++) {
                topMenuNew.push({ "text": SettleHouseName[i], "id": SettleHouseID[i] });
            }
            $("#AHouseID").combobox("loadData", topMenuNew);
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            topMenuNew = [];
            var ClientTypeID = '<%= UserInfor.ClientTypeID%>'.split(',');
            var ClientTypeName = '<%= UserInfor.ClientTypeName%>'.split(',');
            for (var i = 0; i < ClientTypeID.length; i++) {
                topMenuNew.push({ "text": ClientTypeName[i], "id": ClientTypeID[i] });
            }
            $("#ASID").combobox("loadData", topMenuNew);
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            $("#ESID").combobox("loadData", topMenuNew);
            $('#ESID').combobox('textbox').bind('focus', function () { $('#ESID').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                OrderModel: "0",
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
    <%--查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px" />
                </td>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AAwbStatus" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">已下单</option>
                        <option value="1">出库中</option>
                        <option value="2">已出库</option>
                        <option value="5">已签收</option>
                    </select>
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
    <%--查询条件结束--%>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="Add()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" id="delete" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnExportOrder" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BatchExportOrderInfo()">&nbsp;导出订单列表&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />
            <asp:Button ID="btnOrderGoods" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderGoods_Click" />
        </form>
    </div>
    <%--新增渠道订单--%>
    <div id="dlgAdd" class="easyui-dialog" style="width: 75%; height: 650px;" closed="true" closable="false" buttons="#dlgAddOrder-buttons">
        <form id="fmAddDep" method="post">
            <div id="saPanel">
                <input type="hidden" name="AcceptTelephone" id="AAcceptTelephone" />
                <input type="hidden" name="AcceptCity" id="AAcceptCity" />
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right;">区域仓库：</td>
                        <td>
                            <input name="HouseID" id="AHouseID" style="width: 80px;" class="easyui-combobox" data-options="valueField:'id',textField:'text',required:'true'" />
                        </td>
                        <td style="text-align: right;">送货方式:</td>
                        <td>
                            <select class="easyui-combobox" id="ADeliveryType" name="DeliveryType" style="width: 80px;" panelheight="auto" editable="false">
                                <option value="0">配送</option>
                                <option value="1">自提</option>
                            </select>
                        </td>
                        <td style="text-align: right;">客户:</td>
                        <td>
                            <input id="AAcceptUnit" name="AcceptUnit" style="width: 150px;" data-options="required:false" class="easyui-combobox" />
                        </td>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AAcceptPeople" data-options="disabled:true" class="easyui-textbox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">手机号码:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" data-options="disabled:true" class="easyui-textbox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">收货地址:
                        </td>
                        <td>
                            <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:true" style="width: 350px;" class="easyui-textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总数量:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true,disabled:true" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">销售费:
                        </td>
                        <td>
                            <input name="TransportFee" id="ATransportFee" data-options="min:0,precision:2,disabled:true" class="easyui-numberbox" style="width: 80px;" />
                            <input type="hidden" id="hiddenATransportFee" />
                        </td>
                        <td style="text-align: right;">服务费:
                        </td>
                        <td>
                            <input name="TransitFee" id="ATransitFee" data-options="min:0,precision:2,disabled:true" class="easyui-numberbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">费用合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="ATotalCharge" data-options="min:0,precision:2,disabled:true" class="easyui-numberbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">备注:
                        </td>
                        <td colspan="3">
                            <input name="Remark" id="ARemark" data-options="disabled:false,prompt:'请输入订单备注'" style="width: 95%;" class="easyui-textbox" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table id="dgAdd" class="easyui-datagrid">
        </table>
        <table id="outDgAdd" class="easyui-datagrid">
        </table>
    </div>
    <div id="toolbarAdd">
        <table>
            <tr>
                <td style="text-align: right;">品牌:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'id',textField:'text'" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>
                <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px" />
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="selectProduct()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;&nbsp;双击产品信息进行下单
                </td>
            </tr>
        </table>
    </div>
    <div id="dlgAddOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="btnSave" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAdd').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <%--新增渠道订单结束--%>

    <!--Begin 出库操作-->
    <div id="dlgOutCargo" class="easyui-dialog" style="width: 350px; height: 350px; padding: 5px 5px" closed="true" buttons="#dlgOutCargo-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <input type="hidden" id="index" />
            <input type="hidden" id="DisplayNum" />
            <input type="hidden" id="DisplayPiece" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">销售价:
                    </td>
                    <td>
                        <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 200px;" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outAddOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作结束-->
    <%--编辑订单--%>
    <div id="dlgOrder" class="easyui-dialog" style="width: 85%; height: 600px;" closed="true" closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <input type="hidden" name="OrderID" id="EOrderID" />
            <input type="hidden" name="OrderNo" id="EOrderNo" />
            <input type="hidden" name="ClientNum" id="EClientNum" />
            <input type="hidden" name="AcceptCity" id="EAcceptCity" />
            <input type="hidden" name="HouseID" id="EHouseID" />
            <div id="saPanel">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">收货单位:
                        </td>
                        <td>
                            <input name="AcceptUnit" id="EAcceptUnit" data-options="disabled:false" class="easyui-textbox" style="width: 125px;" />
                        </td>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="EAcceptPeople" data-options="disabled:true" class="easyui-textbox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">手机号码:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="EAcceptCellphone" data-options="disabled:false" class="easyui-textbox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">收货地址:
                        </td>
                        <td colspan="3">
                            <input name="AcceptAddress" id="EAcceptAddress" data-options="disabled:false" style="width: 250px;" class="easyui-textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总件数:</td>
                        <td>
                            <input name="Piece" id="EPiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 70px;" readonly="true" />
                        </td>
                        <td style="text-align: right;">销售费:
                        </td>
                        <td>
                            <input name="TransportFee" id="ETransportFee" data-options="min:0,precision:2,disabled:true" class="easyui-numberbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">服务费:
                        </td>
                        <td>
                            <input name="TransitFee" id="ETransitFee" data-options="min:0,precision:2,disabled:true" class="easyui-numberbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">费用合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="ETotalCharge" data-options="min:0,precision:2,disabled:true" class="easyui-numberbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">送货方式:</td>
                        <td>
                            <select class="easyui-combobox" id="EDeliveryType" name="DeliveryType" style="width: 80px;" panelheight="auto" editable="false">
                                <option value="0">配送</option>
                                <option value="1">自提</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="Remark" id="ERemark" rows="3" style="width: 95%; resize: none"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table>
            <tr>
                <td>
                    <input type="hidden" id="dgSaveAwbStatus" />
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
                <td class="outDg" id="outTd">
                    <table id="outDg" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
        <div id="toolbar">
            <table>
                <tr>
                    <td style="text-align: right;">品牌:
                    </td>
                    <td>
                        <input id="ESID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'id',textField:'text'" />
                    </td>
                    <td style="text-align: right;">规格:</td>
                    <td>
                        <input id="ESpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 80px">
                    </td>
                    <td style="text-align: right;">花纹:</td>
                    <td>
                        <input id="EFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px" />
                    </td>
                    <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="queryInCargoProduct()">查询</a></td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" id="btnExportOrderGoods" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportOrderGoods()">&nbsp;导出订单明细&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <%--编辑订单出库--%>
    <div id="dlgEOutCargo" class="easyui-dialog" style="width: 350px; height: 350px; padding: 5px 5px" closed="true" buttons="#dlgEOutCargo-buttons">
        <form id="Efm" class="easyui-form" method="post">
            <input type="hidden" id="EInPiece" />
            <input type="hidden" id="EInIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="ENumbers" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">销售价:
                    </td>
                    <td>
                        <input name="ActSalePrice" id="EActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 200px;" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgEOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgEOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--编辑订单出库结束--%>
    <%--编辑订单结束--%>

    <script type="text/javascript">
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 500;
            var x = 2;  //只接受整数
            $.messager.alert(title, msg, icon, function () { });
            interval = setInterval(fun, time);
            function fun() {
                --x;
                if (x == 0) {
                    clearInterval(interval);
                    $(".messager-body").window('close');
                }
            };
        }
        //批量导出订单信息
        function BatchExportOrderInfo() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "orderApi.aspx?method=QueryOrderInfoExport&OrderNo=" + $('#AOrderNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&AcceptUnit=" + $('#AcceptUnit').val(),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnOrderInfo.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //导出订单明细信息
        function ExportOrderGoods() {
            var row = $("#dgSave").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var obj = document.getElementById("<%=btnOrderGoods.ClientID %>"); obj.click();
        }
        function Add() {
            $('#dlgAdd').dialog('open').dialog('setTitle', '新增渠道订单');
            //收货人
            $('#AAcceptUnit').combobox({
                valueField: 'ADID',
                textField: 'AcceptCompany',
                delay: '10',
                url: 'orderApi.aspx?method=AutoCompleteClientAcceptPeople',
                onSelect: function (item) {
                    $('#AAcceptUnit').textbox('setValue', item.AcceptCompany);
                    $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
                    $('#AAcceptTelephone').val(item.AcceptTelephone);
                    $('#AAcceptCity').val(item.AcceptCity);
                    $('#AAcceptCellphone').textbox('setValue', item.AcceptCellphone);
                    $('#AAcceptPeople').textbox('setValue', item.AcceptPeople);

                },
                required: true,
                editable: false
            });
        }
        //查询
        function selectProduct() {
            $('#dgAdd').datagrid('clearSelections');
            var gridOpts = $('#dgAdd').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryALLHouseData';
            $('#dgAdd').datagrid('load', {
                HouseID: $("#AHouseID").combobox('getValue'),
                SID: $("#ASID").combobox('getValue'),
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                GoodsCode: $('#AGoodsCode').val()
            });
        }
        ///出库
        function plOutCargo() {
            $('#index').val("");
            var row = $('#dgAdd').datagrid('getSelected');
            $('#index').val($('#dgAdd').datagrid('getRowIndex', row.ID));
            if (row == null || row == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '库存不足', 'warning');
                return;
            }
            if (row) {
                $('#dlgOutCargo').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#dgAdd').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#ActSalePrice').numberbox('setValue', row.SalePrice);

                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();
            }
        }
        //新增出库数据
        function outAddOK() {
            $("#ActSalePrice").numberbox('enable');
            var SalePrice = $('#ActSalePrice').numberbox('getValue');
            $('#dgAdd').datagrid('selectRow', $('#index').val());
            var row = $('#dgAdd').datagrid('getSelected');
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请输入拉上订单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '拉上订单数量必须大于0！', 'warning');
                return;
            }
            var Total = Number(row.Piece);
            var tCharge = $('#ATransportFee').numberbox('getValue') == null || $('#ATransportFee').numberbox('getValue') == "" ? 0 : Number($('#ATransportFee').numberbox('getValue'));
            var hiddenTransportFeeVal = $('#hiddenATransportFee').val() == null || $('#hiddenATransportFee').val() == "" ? 0 : Number($('#hiddenATransportFee').val());
            var Aindex = $('#InIndex').val();
            var index = $('#outDgAdd').datagrid('getRowIndex', row.ID);
            if (index < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                row.ActSalePrice = SalePrice;
                $('#outDgAdd').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                var fullDiscountCut = 0;
                if (getCookie("FullDiscountFull") > 0) {
                    var ptNum = (parseInt(Number($('#Numbers').numberbox('getValue'))) + parseInt(getCookie("FullDiscountSum"))) / getCookie("FullDiscountFull");
                    var ptList = (ptNum + "").split(".");
                    if (ptList.length > 1) {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    } else {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    }
                }
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#APiece').numberbox('setValue', Number(pt));
                $('#ATransitFee').numberbox('setValue', Number(pt) * 10);
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $("#hiddenATransportFee").val(hiddenTransportFeeVal + NC);
                $('#ATransportFee').numberbox('setValue', tCharge + NC);
                $('#ATotalCharge').numberbox('setValue', tCharge + NC + Number(pt) * 10);

                var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
                $('#outDgAdd').datagrid("getPanel").panel("setTitle", title);
                closedgShowData();

                if (Total > Number($('#Numbers').numberbox('getValue')) + fullDiscountCut) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#dgAdd').datagrid('updateRow', { index: Aindex, row: row });
            } else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        function getCookie(cname) {
            var name = cname + "=";
            var cookie = document.cookie.split(';');
            for (var i = 0; i < cookie.length; i++) {
                var c = cookie[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        }
        //关闭
        function closedgShowData() {
            $('#dlgOutCargo').dialog('close');
        }
        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDgAdd").datagrid('getData').rows[Did];
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - (Number(row.InPiece) - Number(row.Piece));
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#APiece').numberbox('setValue', Number(pt));

            var title = "订单物料     已拉上：" + n + "个物料号，总数量：" + pt + " 条";
            $('#outDgAdd').datagrid("getPanel").panel("setTitle", title);
            var index = $('#dgAdd').datagrid('getRowIndex', row.ID);
            if (index >= 0) {
                var Trow = $("#dgAdd").datagrid('getData').rows[index];
                Trow.Piece = Trow.InPiece;
                $('#dgAdd').datagrid('updateRow', { index: index, row: Trow });
            }
            var index = $('#outDgAdd').datagrid('getRowIndex', row);
            $('#outDgAdd').datagrid('deleteRow', index);
        }
        //保存订单
        function saveOutCargo() {
            //取消业务员禁用方便后台取值
            $('#APiece').combobox('enable');
            $('#TotalCharge').combobox('enable');
            $('#AAcceptAddress').combobox('enable');
            $('#AAcceptPeople').combobox('enable');
            $('#AAcceptCellphone').combobox('enable');
            $('#ATransportFee').combobox('enable');
            var rows = $('#outDgAdd').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '出库列表为空！', 'warning'); return; }

            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#btnSave').linkbutton('disable');
            var json = JSON.stringify(rows);

            $('#fmAddDep').form('submit', {
                url: 'orderApi.aspx?method=saveOrderData',
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    param.submitData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#btnSave').linkbutton('disable'); } else {
                        $.messager.progress("close");
                        $('#btnSave').linkbutton('enable');
                    }
                    return trd;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    $('#btnSave').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        var dd = result.Message.split('/');
                        dosearch();
                        location.reload();
                    } else {
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })

        }
        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].AwbStatus != 0) {
                    $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已出库无法删除！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=DelOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //双击显示订单详细界面 
        function editItemByID(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#outDg').datagrid('loadData', { total: 0, rows: [] });
            IsModifyOrder = false;
            editIndex = undefined;
            var row = $("#dg").datagrid('getData').rows[Did];
            CheckStatus = row.CheckStatus;
            rowHouseID = row.HouseID;
            //$('#postpone').show();
            if (row) {
                $('#dgSaveAwbStatus').val(row.AwbStatus);
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo);
                $('#fmDep').form('clear');
                var topMenuNew = [];
                var ClientTypeID = '<%= UserInfor.ClientTypeID%>'.split(',');
                var ClientTypeName = '<%= UserInfor.ClientTypeName%>'.split(',');
                for (var i = 0; i < ClientTypeID.length; i++) {
                    topMenuNew.push({ "text": ClientTypeName[i], "id": ClientTypeID[i] });
                }
                $("#ESID").combobox("loadData", topMenuNew);
                $('#ESID').combobox('textbox').bind('focus', function () { $('#ESID').combobox('showPanel'); });
                //收货人
                $('#EAcceptUnit').combobox({
                    valueField: 'ADID',
                    textField: 'AcceptCompany',
                    delay: '10',
                    url: 'orderApi.aspx?method=AutoCompleteClientAcceptPeople',
                    onSelect: function (item) {
                        $('#EAcceptUnit').textbox('setValue', item.AcceptCompany);
                        $('#EAcceptAddress').textbox('setValue', item.AcceptAddress);
                        $('#EAcceptTelephone').val(item.AcceptTelephone);
                        $('#EAcceptCity').val(item.AcceptCity);
                        $('#EAcceptCellphone').textbox('setValue', item.AcceptCellphone);
                        $('#EAcceptPeople').textbox('setValue', item.AcceptPeople);

                    },
                    required: true,
                    editable: false
                });
                row.CreateDate = AllDateTime(row.CreateDate);
                $('#fmDep').form('load', row);
                $('#TransportFee').numberbox('setValue', row.TotalCharge);
                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });

                $('#EAcceptCity').val(row.Dest);

                TrafficType = row.TrafficType;

                if (row.CheckStatus == "1") {
                    columns.push({ title: '数量', field: 'Piece', width: '50px' });
                    columns.push({ title: '销售价', field: 'ActSalePrice', width: '60px' });
                } else {
                    columns.push({ title: '数量', field: 'Piece', width: '50px', editor: { type: 'numberbox' } });
                    columns.push({ title: '销售价', field: 'ActSalePrice', width: '60px', editor: { type: 'numberbox', options: { precision: 2 } } });
                }
                columns.push({ title: '产品编码', field: 'ProductCode', width: '100px' });
                columns.push({ title: '品牌', field: 'TypeName', width: '80px' });
                columns.push({ title: '货品代码', field: 'GoodsCode', width: '160px' });
                columns.push({ title: '规格', field: 'Specs', width: '90px' });
                columns.push({ title: '花纹', field: 'Figure', width: '90px' });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                        return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                    }
                });
                columns.push({ title: '批次', field: 'BatchYear', width: '50px' });

                showGrid(columns, row.HouseID);

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;

                $(function () {
                    $('#dgSave').datagrid('enableCellEditing').datagrid('gotoCell', {
                        index: 0,
                        field: 'ID'
                    });
                });
            }
        }

        //关闭弹出框
        function closeDlg() {
            $('#dlgOutCargo').dialog('close');
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
        //显示列表
        function showGrid(dgSaveCol, houseID) {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({
                title: '在库件数', field: 'Piece', width: '70px', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '销售价', field: 'SalePrice', width: '60px' });
            columns.push({ title: '产品编码', field: 'ProductCode', width: '100px' });
            columns.push({ title: '品牌', field: 'TypeName', width: '80px' });
            columns.push({ title: '货品代码', field: 'GoodsCode', width: '150px' });
            columns.push({ title: '规格', field: 'Specs', width: '90px' });
            columns.push({ title: '花纹', field: 'Figure', width: '90px' });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '70px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({ title: '批次', field: 'BatchYear', width: '60px' });
            $('#outDg').datagrid({
                width: Number($("#dlgOrder").width()) * 0.5 - 5 - 50,
                height: 390,
                title: '在库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onDblClickRow: function (index, row) { plup(index); },
                rowStyler: function (index, row) {
                }
            });
            var dgSavewidth = Number($("#dlgOrder").width()) * 0.5 - 5 + 50;
            //dgSavewidth = Number($("#dlgOrder").width()) - 10;

            $('#dgSave').datagrid({
                width: dgSavewidth,
                height: 390,
                title: '订单明细', //标题内容
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
                columns: [dgSaveCol],
                onClickCell: onClickCell,
                rowStyler: function (index, row) {
                    if (row.RuleTitle != null && row.RuleTitle != "") { return "background-color:#D4EFDF"; };
                }
            });
        }
        function onAcceptAddressChanged(item) {
            $('#EAcceptUnit').textbox('setValue', item.AcceptCompany);
            $('#EAcceptAddress').textbox('setValue', item.AcceptAddress);
            $('#EAcceptTelephone').textbox('setValue', item.AcceptTelephone);
            $('#EAcceptCellphone').textbox('setValue', item.AcceptCellphone);
            $('#EAcceptPeople').textbox('setValue', item.AcceptPeople);
            $('#HiddenAAcceptPeople').val(item.AcceptPeople);
            $('#EAcceptCity').val(item.AcceptCity);
        }
        //查询在库产品
        function queryInCargoProduct() {
            $('#outDg').datagrid('clearSelections');
            var gridOpts = $('#outDg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryALLHouseData';
            $('#outDg').datagrid('load', {
                HouseID: $("#EHouseID").val(),
                SID: $("#ASID").combobox('getValue'),
                Specs: $('#ESpecs').val(),
                Figure: $('#EFigure').val()
            });
        }

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
        var IsModifyOrder = false;
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgSave').datagrid('validateRow', editIndex)) {
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) { return false; }
                var sum = 0;
                if (cg.field == "Piece") {
                    //修改件数
                    var oldPiece = Number(rows.Piece);
                    var salePrice = Number(rows.ActSalePrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        if (i == editIndex) {
                            count = Number(count) + newPiece;
                        } else {
                            count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                        }
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', editIndex);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#EOrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            $.messager.progress("close");
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    ModifyPrice = ModifyPiece * salePrice;
                                    var TPiece = Number($('#EPiece').numberbox('getValue'));
                                    $('#EPiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    $('#ETransitFee').numberbox('setValue', Number(TPiece + ModifyPiece) * 10);
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee + ModifyPrice + Number(TPiece + ModifyPiece) * 10).toFixed(2));
                                } else {
                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#EPiece').numberbox('getValue'));
                                    $('#EPiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    $('#ETransitFee').numberbox('setValue', Number(TPiece - ModifyPiece) * 10);
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee - ModifyPrice + Number(TPiece - ModifyPiece) * 10).toFixed(2));
                                }
                                alert_autoClose('<%= Supplier.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                if (cg.field == "ActSalePrice") {
                    var oldPrice = Number(rows.ActSalePrice);//旧价格
                    var piece = Number(rows.Piece);//新件数
                    var newPrice = Number(cg.target.val());//新价格
                    if (oldPrice == newPrice) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', editIndex);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }
                    var salePrice = Number(rows.SalePrice);
                    rows.SalePrice = oldPrice;
                    rows.ActSalePrice = cg.target.val();
                    rows.OrderNo = $('#EOrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderSalePrice',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加价格
                                if (newPrice > oldPrice) {
                                    ModifyPrice = newPrice - oldPrice;
                                    ModifyPiece = ModifyPrice * piece;
                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee + ModifyPiece).toFixed(2));
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee + ModifyPrice + Number(piece) * 10).toFixed(2));
                                } else {
                                    ModifyPrice = oldPrice - newPrice;
                                    ModifyPiece = ModifyPrice * piece;

                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee - ModifyPiece).toFixed(2));
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee - ModifyPrice + Number(piece) * 10).toFixed(2));
                                }
                                alert_autoClose('<%= Supplier.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
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
            var rows = $("#dgSave").datagrid('getData').rows[index];
            if (TrafficType == "2") { return; }
            if ($('#dgSaveAwbStatus').val() * 1 < 1 && field == "Piece" || field == "ActSalePrice") {
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
                    var salePrice = Number(rows.ActSalePrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#EOrderNo').val();

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', index);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    ModifyPrice = ModifyPiece * salePrice;
                                    var TPiece = Number($('#EPiece').numberbox('getValue'));
                                    $('#EPiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    $('#ETransitFee').numberbox('setValue', Number(TPiece + ModifyPiece) * 10);
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee + ModifyPrice + Number(TPiece + ModifyPiece) * 10).toFixed(2));
                                } else {

                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#EPiece').numberbox('getValue'));
                                    $('#EPiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    $('#ETransitFee').numberbox('setValue', Number(TPiece - ModifyPiece) * 10);
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee - ModifyPrice + Number(TPiece - ModifyPiece) * 10).toFixed(2));
                                }
                                alert_autoClose('<%= Supplier.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                if (cg.field == "ActSalePrice") {
                    //修改销售价
                    var oldPrice = Number(rows.ActSalePrice);//旧价格
                    var piece = Number(rows.Piece);//新件数
                    var newPrice = Number(cg.target.val());//新价格
                    if (oldPrice == newPrice) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }

                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', index);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    var salePrice = Number(rows.SalePrice);
                    rows.SalePrice = oldPrice;
                    rows.ActSalePrice = cg.target.val();
                    rows.OrderNo = $('#EOrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderSalePrice',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加价格
                                if (newPrice > oldPrice) {
                                    ModifyPrice = newPrice - oldPrice;
                                    ModifyPiece = ModifyPrice * piece;
                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee + ModifyPiece).toFixed(2));
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee + ModifyPrice + Number(piece) * 10).toFixed(2));
                                } else {
                                    ModifyPrice = oldPrice - newPrice;
                                    ModifyPiece = ModifyPrice * piece;

                                    var TFee = Number($('#ETransportFee').numberbox('getValue'));
                                    $('#ETransportFee').numberbox('setValue', Number(TFee - ModifyPiece).toFixed(2));
                                    $('#ETotalCharge').numberbox('setValue', Number(TFee - ModifyPrice + Number(piece) * 10).toFixed(2));
                                }
                                alert_autoClose('<%= Supplier.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
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


        ///拉上订单
        function plup() {
            var row = $('#outDg').datagrid('getSelected');
            if (CheckStatus != "0") { return; }
            if (row == null || row == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (row) {
                var clientNum = $('#ClientNum').val();
                $('#dlgEOutCargo').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
                $('#EInPiece').val(row.Piece);
                $('#EInIndex').val($('#outDg').datagrid('getRowIndex', row));
                $('#ENumbers').numberbox('setValue', '');
                $('#EActSalePrice').numberbox('setValue', row.SalePrice);

                $('#ENumbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#ENumbers').numberbox().next('span').find('input').focus();

            }
        }
        //新增出库数据
        function outOK() {
            var row = $('#outDg').datagrid('getSelected');
            var SalePrice = $('#EActSalePrice').numberbox('getValue');
            var Total = Number(row.Piece);
            var OutCargoID;
            var dgS = $('#dgSave').datagrid('getRows');
            for (var i = 0; i < dgS.length; i++) {
                OutCargoID = dgS[i].OutCargoID;
                if (dgS[i].Specs == row.Specs && dgS[i].Figure == row.Figure && dgS[i].GoodsCode == row.GoodsCode && dgS[i].LoadIndex == row.LoadIndex && dgS[i].SpeedLevel == row.SpeedLevel && dgS[i].BatchYear == row.BatchYear) {
                    $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '该产品已在订单上，请直接修改数量操作！', 'warning');
                    return;
                }
            }
            var tCharge = $('#ETransportFee').numberbox('getValue') == null || $('#ETransportFee').numberbox('getValue') == "" ? 0 : Number($('#ETransportFee').numberbox('getValue'));
            var Aindex = $('#EInIndex').val();
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (indexD < 0) {
                debugger;
                row.Piece = Number($('#ENumbers').numberbox('getValue'));
                row.ActSalePrice = SalePrice;
                row.OutCargoID = OutCargoID;
                row.OrderNo = $('#EOrderNo').val();
                var json = JSON.stringify([row])
                //$('#dgSave').datagrid('appendRow', row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p + Number($('#ENumbers').numberbox('getValue'));
                $('#EPiece').numberbox('setValue', Number(pt));
                if (Total != Number($('#ENumbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#ENumbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#outDg').datagrid('updateRow', { index: Aindex, row: row });


                $.messager.progress({ msg: '请稍后,拉上订单中...' });
                $.ajax({
                    url: 'orderApi.aspx?method=DrawUpOrder',
                    type: 'post', dataType: 'json', data: { data: json },
                    success: function (text) {
                        $.messager.progress("close");
                        if (text.Result == true) {
                            $('#ETransportFee').numberbox('setValue', Number(tCharge + Number($('#ENumbers').numberbox('getValue')) * SalePrice).toFixed(2));
                            $('#ETransitFee').numberbox('setValue', Number($('#ETransitFee').numberbox('getValue')) + Number($('#ENumbers').numberbox('getValue')) * 10);
                            $('#ETotalCharge').numberbox('setValue', Number($('#ETransportFee').numberbox('getValue')) + Number($('#ETransitFee').numberbox('getValue')).toFixed(2));
                            //刷新列表
                            $('#dgSave').datagrid('clearSelections');
                            var gridOpts = $('#dgSave').datagrid('options');
                            gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
                            $('#dgSave').datagrid('load', {
                                OrderNo: $('#OrderNo').val()
                            });
                            $('#dlgEOutCargo').dialog('close');
                        }
                        else {
                            $('#EPiece').numberbox('setValue', Number(p));
                            $('#ETransportFee').numberbox('setValue', tCharge);
                            $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                        }
                    }
                });
            } else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
    </script>
</asp:Content>
