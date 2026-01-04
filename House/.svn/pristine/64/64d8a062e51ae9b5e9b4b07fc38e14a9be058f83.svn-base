<%@ Page Title="天猫订单同步" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CreateTMallOrder.aspx.cs" Inherits="Cargo.Order.CreateTMallOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .combo-panel.panel-body.panel-body-noheader {
            max-height: 250px;
        }
    </style>
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
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2 + 50);
            $('#dg').datagrid({ height: height });
            $('#outDg').datagrid({ height: (Number($(window).height()) - 50) - height });
            //$('#outDg3').datagrid({ height: (Number($(window).height()) - 50) - height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: 'id', checkbox: true });
            columns.push({ title: '业务单号', field: 'originBillNo', width: '140px' });
            columns.push({ title: '用户ID', field: 'userId', width: '100px' });
            //columns.push({ title: '收件人城市ID', field: 'consigneeCityId', width: '100px' });
            columns.push({
                title: '发货类型', field: 'sendType', width: '80px',
                formatter: function (val, row, index) {
                    if (val == "1") { return "到店"; }
                    else if (val == "2") { return "到家"; }
                    else if (val == "3") { return "无需配送"; }
                    else { return "未知类型"; }
                }
            });
            columns.push({ title: '收件人手机号', field: 'consigneePhone', width: '120px' });
            //columns.push({ title: '原始单据一级渠道', field: 'originBillFirstChannel', width: '110px' });
            //columns.push({ title: '原始单据二级渠道', field: 'originBillSecondChannel', width: '110px' });
            //columns.push({ title: '原始单据三级渠道', field: 'originBillThirdChannel', width: '110px' });
            columns.push({ title: '同步时间', field: 'OP_DATE', width: '160px', formatter: DateTimeFormatter });
            columns.push({ title: '客户联系人', field: 'customerContact', width: '80px' });
            columns.push({ title: '出库通知单号', field: 'outboundNoticeNo', width: '140px' });
            columns.push({
                title: '是否生成', field: 'InCreateStatus', width: '140px',
                formatter: function (val, row, index) {
                    if (val == "1") { return "已生成"; }
                    else { return ""; }
                } });
            columns.push({ title: '系统订单号', field: 'CargoOrderNo', width: '150px' });
            columns.push({ title: '创建人', field: 'CreateAwb', width: '150px' });
            columns.push({ title: '创建时间', field: 'CreateDate', width: '150px', formatter: DateTimeFormatter });
            columns.push({
                title: '是否推送', field: 'IsDeliveryPush', width: '140px',
                formatter: function (val, row, index) {
                    if (val == 1) { return "已推送"; }
                    else { return ""; }
                } });
            columns.push({ title: '推送时间', field: 'DeliveryPushTime', width: '150px', formatter: DateTimeFormatter });
            columns.push({ title: '客户名称', field: 'customerName', width: '150px' });
            columns.push({ title: '收件人', field: 'consigneeContacts', width: '100px' });
            columns.push({ title: '收件人详细地址', field: 'consigneeDetail', width: '200px' });
            columns.push({ title: '收件人省份名称', field: 'consigneeProvinceName', width: '100px' });
            columns.push({ title: '收件人城市名称', field: 'consigneeCityName', width: '100px' });
            columns.push({ title: '收件人区县名称', field: 'consigneeCountyName', width: '100px' });
            columns.push({ title: '要求送达时间', field: 'requireArriveTime_Actual', width: '160px', formatter: DateTimeFormatter });
            columns.push({
                title: '业务类型', field: 'originBillType', width: '90px',
                formatter: function (val, row, index) {
                    if (val == "1") { return "销售单"; }
                    else if (val == "4") { return "退供单"; }
                    else if (val == "5") { return "调拨单"; }
                    else if (val == "6") { return "报废单"; }
                    else if (val == "7") { return "补发单"; }
                    else { return "未知类型"; }
                }
            });
            columns.push({
                title: '紧急级别', field: 'urgentLevel', width: '80px',
                formatter: function (val, row, index) {
                    if (val == "1") { return "一般"; }
                    else if (val == "2") { return "紧急"; }
                    else { return "未知级别"; }
                }
            });
            columns.push({ title: '额外信息', field: 'extend', width: '120px' });
            columns.push({ title: '天猫仓库编码', field: 'warehouseCode', width: '100px' });
            columns.push({ title: '销售类型', field: 'saleType', width: '80px' });
            columns.push({ title: '仓库名称', field: 'warehouseName', width: '120px' });
            columns.push({ title: '快递面单是否加密', field: 'isExpressSheetEncrypted', width: '110px' });
            //columns.push({ title: '系统仓库编码', field: 'thirdWarehouseName', width: '120px' });
            columns.push({ title: '买家备注', field: 'buyerRemark', width: '120px' });
            columns.push({ title: '卖家备注', field: 'sellerRemark', width: '120px' });
            columns.push({ title: '配送备注', field: 'deliverRemark', width: '150px' });
            columns.push({ title: '原始单据创建时间', field: 'originBillTime_Actual', width: '160px', formatter: DateTimeFormatter });
            columns.push({ title: '要求出库时间', field: 'requireOutWarehouseTime_Actual', width: '160px', formatter: DateTimeFormatter });
            columns.push({
                title: '是否允许缺货发货', field: 'isAllowLack', width: '100px',
                formatter: function (val, row, index) {
                    if (val == "1") { return "是"; }
                    else if (val == "0") { return "否"; }
                    else { return "未知类型"; }
                }
            });
            columns.push({ title: '支付时间', field: 'payTime_Actual', width: '160px', formatter: DateTimeFormatter });

            $('#dg').datagrid({
                width: '100%',
                //height: '50%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                rownumbers: true, //行号
                pageSize: 50, //每页多少条
                pageList: [50,100,200],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'orderId',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#outDg').datagrid('clearSelections');

                    var gridOpts = $('#outDg').datagrid('options');
                    console.log(row)
                    gridOpts.url = 'orderApi.aspx?method=QueryTMallOrderGoods';
                    $('#outDg').datagrid('load', { id: row.id });



                    //var gridOpts3 = $('#outDg3').datagrid('options');
                    //gridOpts3.url = 'orderApi.aspx?method=QueryCassMallGiftItems';
                    //$('#outDg3').datagrid('load', { OrderID: row.OrderId });

                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.InCreateStatus == "0") { return "background-color:#FCF3CF"; }
                },
                onDblClickRow: function (index, row) { }
            });

            columns = [];
            //columns.push({ title: '', field: 'OrderId', checkbox: true });
            columns.push({ title: '', field: 'id', checkbox: true });
            columns.push({ title: 'SKU名称', field: 'skuName', width: '350px' });
            columns.push({ title: '需出库数量', field: 'needOutboundNum', width: '80px' });
            columns.push({ title: 'SKU订单编码', field: 'skuOrderCode', width: '140px' });
            columns.push({
                title: '商品品质等级', field: 'goodsQuality', width: '100px',
                formatter: function (val, row, index) {
                    if (val == "1") { return "好件"; }
                    else if (val == "2") { return "残次品"; }
                    else if (val == "3") { return "坏件"; }
                    else { return "未知等级"; }
                }
});
            columns.push({
                title: '保质期类型', field: 'guaranteePeriod', width: '120px',
                formatter: function (val, row, index) {
                    if (val == "1") { return "正常"; }
                    else if (val == "2") { return "临期"; }
                    else if (val == "4") { return "年份"; }
                    else { return "未知等级"; }
                } });
            columns.push({ title: 'SKU唯一标识ID', field: 'skuId', width: '120px' });
            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '订单明细内容', //标题内容
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
                idField: 'orderId',
                showFooter: true,
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { }
            });

        //所在线路
   <%--         $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name', onSelect: function (rec) {
                    //$('#AFirstAreaID').combobox('clear');
                    //var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    //$('#AFirstAreaID').combobox('reload', url);
                }
            });
                //$('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });--%>

            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#AreaID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#AreaID').combobox('reload', url);
                    $('#AreaID').combobox('textbox').bind('focus', function () { $('#AreaID').combobox('showPanel'); });
                }
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            $('#AorderType').combobox('textbox').bind('focus', function () { $('#AorderType').combobox('showPanel'); });
            $('#AorderLabel').combobox('textbox').bind('focus', function () { $('#AorderLabel').combobox('showPanel'); });
            $('#AInCreateStatus').combobox('textbox').bind('focus', function () { $('#AInCreateStatus').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryTMallOrder';
            $('#dg').datagrid('load', {
                //HouseID: $("#AHouseID").combobox('getValue'),//线路ID
                //AreaID: $("#AFirstAreaID").combobox('getValue'),//线路ID
                orderCode: $('#AorderCode').val(),
                customerName: $('#AcustomerName').val(),
                consigneeName: $('#AconsigneeName').val(),
                //orderType: $("#AorderType").combobox('getValue'),
                //orderLabel: $("#AorderLabel").combobox('getValue'),
                InCreateStatus: $("#AInCreateStatus").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading'
        style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1>
                <font size="9">页面加载中……</font>
            </h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'"
        style="width: 100%">
        <table>
            <tr>
                <%--                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>--%>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AorderCode" class="easyui-textbox" data-options="prompt:'请输入订单号/通知单号'"
                        style="width: 100px" />
                </td>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AcustomerName" class="easyui-textbox" data-options="prompt:'请输入客户名称'"
                        style="width: 100px" />
                </td>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="AconsigneeName" class="easyui-textbox" data-options="prompt:'请输入收货人'"
                        style="width: 100px" />
                </td>
<%--                <td style="text-align: right;">订单渠道:
                </td>
                <td>
                    <select class="easyui-combobox" id="AorderLabel" style="width: 120px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="PLATMALL">商城订单</option>
                        <option value="GROUP_BUY">团购订单</option>
                        <option value="PRE_SALE">预售订单</option>
                        <option value="DIRECTIONAL">定向采购单</option>
                        <option value="AFTER_SALE_LABEL">售后标签</option>
                        <option value="DISTRIBUTION_INQUIRY">备货询价订单</option>
                        <option value="CUSTOMIZE_INQUIRY">事故车询价订单</option>
                        <option value="COMMON_INQUIRY">常规询价订单</option>
                        <option value="PREPARE_GOODS">备货协议</option>
                        <option value="GARAGE_RIGHTS">权益订单</option>
                        <option value="DEMAND_QUOTATION">F2B需求单交易</option>
                        <option value="PRODUCT_CATALOG">集采目录</option>
                        <option value="OPTIONAL_GOODS">自选商品</option>
                        <option value="DEMAND_QUOTE_AGENT">F2B需求单代采</option>
                        <option value="PROCUREMENT">采购清单</option>
                    </select>
                </td>--%>
                <td style="text-align: right;">是否生成:
                </td>
                <td>
                    <select class="easyui-combobox" id="AInCreateStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未生成</option>
                        <option value="1">已生成</option>
                    </select>
                </td>
                <td style="text-align: right;">同步时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
 <%--               <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>--%>
            </tr>

        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 100%; height: 100%; margin: 0px; padding: 0px;">
                <table id="outDg" class="easyui-datagrid">
                </table>
            </td>
        </tr>
    </table>


    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false"
            onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#"
            class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;生成仓库订单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
                        plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
                        <form runat="server" id="fm1">
                            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />

                        </form>
        <%--        <a href="#" class="easyui-linkbutton"
                iconcls="icon-lock_add" onclick="SaveContiOrderOK()">&nbsp;审核订单&nbsp;</a>&nbsp;&nbsp
        ;<a href="#"
                    class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
                &nbsp;生成仓库订单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false"
                    onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>--%>
    </div>

    <div id="dlg" class="easyui-dialog" data-options="modal:true" style="width: 400px; height: 400px; padding: 0px" closed="true"
        buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="SaleManName" id="SaleManName" />
                <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
                <table>
                    <tr>
                        <td style="text-align: right;">客户名称:
                        </td>
                        <td>
                            <input id="ClientNum" name="ClientNum" class="easyui-combobox" disabled readonly style="width: 250px;"
                                data-options="required:true" />

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input id="AcceptPeople" name="AcceptPeople" class="easyui-textbox"
                                data-options="prompt:'请输入收货人'" style="width: 250px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">收货地址:
                        </td>
                        <td>
                            <input id="AcceptAddress" name="AcceptAddress" class="easyui-textbox"
                                data-options="prompt:'请输入收货地址',required:true" style="width: 250px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 250px;"
                                panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">出库仓库:
                        </td>
                        <td>
                            <input id="AreaID" name="AreaID" class="easyui-combobox" style="width: 250px;"
                                data-options="valueField:'AreaID',textField:'Name',required:true"
                                panelheight="auto" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">到达城市:
                        </td>
                        <td>
                            <input name="Dest" id="ADest" class="easyui-combobox" style="width: 250px;"
                                data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:true,editable:true " />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">业务员:
                        </td>
                        <td>
                            <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 250px;"
                                data-options="url: 'orderApi.aspx?method=QueryUserByDepCodes',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged" />
                        </td>
                    </tr>
         <%--           <tr>
                        <td style="text-align: right;">批次:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="ABatch" style="width: 250px;" panelheight="auto">
                                <option value="0">全年</option>
                                <option value="1">上半年</option>
                                <option value="2">下半年</option>
                            </select>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right;">备注:
                        </td>
                        <td>
                            <textarea name="Remark" id="Remark" placeholder="请输入备注"
                                style="width: 250px; height: 50px; resize: none"></textarea>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok"
            onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <script type="text/javascript">

        //导出数据
        function AwbExport() {
            var obj = document.getElementById("<%=btnDerived.ClientID %>");
            obj.click();

            var json = {
                originBillNo: $('#AorderCode').val(),
                customerName: $('#AcustomerName').val(),
                consigneeName: $('#AconsigneeName').val(),
                InCreateStatus: $("#AInCreateStatus").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
            }
            $.ajax({
                url: "orderApi.aspx?method=QueryTMallOrderExport&key=" + JSON.stringify(json),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }

        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleManID').val(item.LoginName);
                $('#SaleManName').val(item.UserName);
                $('#SaleCellPhone').val(item.CellPhone);
                console.log('onSaleManIDChanged', item)
            }
        }

        //$('#AHouseID').combobox({
        //    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
        //    valueField: 'HouseID', textField: 'Name', onSelect: function (rec) { }
        //});

        //审核订单
        function SaveContiOrderOK() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=SaveContiOrderOK',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核成功!', 'info');
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
        var isLoad = true;
        function onAcceptAddressChanged(val) {
            $('#AcceptPeople').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + $('#ClientNum').combobox('getValue'),
            }).combobox('reload');
        }
        function onAcceptAddressChanged2(val) {
            console.log(val)
            $('#AcceptAddress').textbox('setValue', val.AcceptAddress);
        }
        //生成仓库订单
        function addItem() {
            isLoad = true;
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要生成订单的数据！', 'warning');
                return;
            }
            $('#dlg').dialog('open').dialog('setTitle', '生成仓库订单');
            $('#fm').form('clear');
            $('#ClientNum').combobox({
                valueField: 'ClientNum', textField: 'Boss',
                url: '../Client/clientApi.aspx?method=GetCompleteClient',
                onSelect: onAcceptAddressChanged,
            });
            $('#ClientNum').combobox('setValue', 863602);


            $('#AcceptPeople').combobox({
                valueField: 'AcceptPeople', textField: 'AcceptPeople',
                url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + $('#ClientNum').combobox('getValue'),
                onSelect: onAcceptAddressChanged2,
                required: true,
                editable: true
            });
            $('#ClientNum').combobox('textbox').bind('focus', function () { $('#ClientNum').combobox('showPanel'); });
            //if (row[0].HouseID) {
            //    $('#HouseID').combobox('setValue', row[0].HouseID);
            //    if (row[0].AreaID) {
            //        $('#AreaID').combobox('clear');
            //        var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + row[0].HouseID;
            //        $('#AreaID').combobox('reload', url);
            //        $('#AreaID').combobox('setValue', row[0].AreaID);
            //    }
            //}
            $('#AcceptPeople').combobox('setValue', row[0].consigneeContacts);
            var address = row[0].consigneeProvinceName + row[0].consigneeCityName + row[0].consigneeCountyName + row[0].consigneeDetail
            $('#AcceptAddress').textbox('setValue', address);
            if (row[0].consigneeCityName) {
                row[0].consigneeCityName = row[0].consigneeCityName.replace("市", "");
            }
            $('#ADest').combobox('setValue', row[0].consigneeCityName);
            //南宁云仓 固定叶树仁
            $('#SaleManID').combobox('setValue', 3357);
            $('#SaleManName').val('陈鑫宇');
            $('#SaleCellPhone').val('13873312771');


        }
        function containsString(sourceStr) {
            if (!sourceStr || sourceStr.trim() === "") {
                return 0;
            }

            if (sourceStr.includes("上半年")) {
                return 1;
            }

            if (sourceStr.includes("下半年")) {
                return 2;
            }

            return 0;
        }

        //删除信息
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
                        url: 'orderApi.aspx?method=DelContiOrder',
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

        //保存生成仓库订单
        function saveItem() {
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要生成订单的数据！', 'warning');
                return;
            }
            var goods = $('#outDg').datagrid('getRows');
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定生成仓库订单？', function (r) {
                if (r) {
                    var json = JSON.stringify(row)
                    var sku = JSON.stringify(goods)
                    $.ajax({
                        url: 'orderApi.aspx?method=SaveTMallOrder&ClientNum=' + escape($('#ClientNum').combobox('getValue')) + '&HouseID=' + $('#HouseID').combobox('getValue') + '&AreaID=' + $('#AreaID').combobox('getValue') + '&Dest=' + $('#ADest').combobox('getValue') + '&SaleManID=' + $('#SaleManID').combobox('getValue') + '&SaleManName=' + $('#SaleManName').val() + '&SaleCellPhone=' + $('#SaleCellPhone').val() + '&AcceptPeople=' + $('#AcceptPeople').combobox('getValue') + '&AcceptAddress=' + $('#AcceptAddress').textbox('getValue') + '&Remark=' + escape($('#Remark').val()),
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '生成成功!', 'info');
                                $('#dlg').dialog('close'); 	// close the dialog
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
    </script>
</asp:Content>
