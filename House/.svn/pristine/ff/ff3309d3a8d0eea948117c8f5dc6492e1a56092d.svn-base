<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderManager.aspx.cs" Inherits="Dealer.Antyres.OrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
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
            adjustment();
            $('#up').hide();
            $('#save').hide();
            //$('#btnExportOrder').hide();
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
                title: '件数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
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
            columns.push({
                title: '订单类型', field: 'ThrowGood', width: '60px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='普送单'>普送单</span>"; }
                    else if (val == "17") { return "<span title='急送单'>急送单</span>"; }
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
                        title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
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
            gridOpts.url = 'FormService.aspx?method=QueryOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                ThrowGood: $("#AThrowGood").combobox('getValue'),
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px" />
                </td>
                <td class="AcceptPeople" style="text-align: right;">收货人:
                </td>
                <td class="AcceptPeople">
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">订单类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="AThrowGood" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">普送单</option>
                        <option value="17">急送单</option>
                    </select>
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
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="updateOrderStatus()">&nbsp;订单跟踪&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" id="delete" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnExportOrder" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BatchExportOrderInfo()">&nbsp;导出订单列表&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />
            <asp:Button ID="btnOrderGoods" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderGoods_Click" />
        </form>
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 75%; height: 600px;" closed="true" closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <input type="hidden" name="ClientNum" id="ClientNum" />
            <input type="hidden" name="AcceptCity" id="AcceptCity" />
            <div id="saPanel">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AAcceptPeople" style="width: 150px;" class="easyui-combobox" />
                        </td>
                        <td class="AAcceptUnit" style="text-align: right;">收货单位:
                        </td>
                        <td class="AAcceptUnit">
                            <input name="AcceptUnit" id="AAcceptUnit" data-options="disabled:false" class="easyui-textbox" style="width: 125px;" />
                        </td>
                        <td class="AAcceptCellphone" style="text-align: right;">手机号码:
                        </td>
                        <td class="AAcceptCellphone">
                            <input name="AcceptCellphone" id="AAcceptCellphone" data-options="disabled:false" class="easyui-textbox" style="width: 100px;" />
                        </td>
                        <td class="AAcceptTelephone" style="text-align: right;">电话号码:
                        </td>
                        <td class="AAcceptTelephone">
                            <input name="AcceptTelephone" id="AAcceptTelephone" data-options="disabled:false" class="easyui-textbox" style="width: 100px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 70px;" readonly="true" />
                        </td>
                        <td hidden="hidden">
                            <input id="TransportFee" data-options="min:0,precision:2" readonly="true" class="easyui-numberbox" style="width: 70px;" />
                        </td>
                        <td class="AAcceptAddress" style="text-align: right;">收货地址:
                        </td>
                        <td class="AAcceptAddress" colspan="3">
                            <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:false" style="width: 250px;" class="easyui-textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 95%; resize: none"></textarea>
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
                    <td class="AASpecs" id="AASpecs" style="text-align: right;">规格:</td>
                    <td class="AASpecs">
                        <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 80px">
                    </td>
                    <td class="AFigure" style="text-align: right;">花纹:</td>
                    <td class="AFigure">
                        <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px" />
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-basket_put" plain="false" onclick="plup()" id="up">&nbsp;拉上订单&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveOrderUpdate()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" id="btnExportOrderGoods" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportOrderGoods()">&nbsp;导出订单明细&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>

    <!--Begin 出库操作-->

    <div id="dlgOutCargo" class="easyui-dialog" style="width: 350px; height: 350px; padding: 5px 5px" closed="true" buttons="#dlgOutCargo-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 200px;"/>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->
    <!--订单状态跟踪-->
    <div id="dlgStatus" class="easyui-dialog" style="width: 950px; height: 360px; padding: 0px" closed="true" closable="false" buttons="#dlgStatus-buttons">
        <form id="fmStatus" class="easyui-form" method="post">
            <div id="saPanel">
                <table class="mini-toolbar" style="width: 98%;">
                    <tr>
                        <td rowspan="3" style="border-right: 1px solid #909aa6;">订<br />
                            单<br />
                            在<br />
                            途<br />
                            跟<br />
                            踪
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true" style="height: 250px; width: 850px">
                                <div title="订单跟踪" id="lblTrack" data-options="iconCls:'icon-page_add'"></div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlgStatus-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgStatus').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--订单状态跟踪-->
    
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 600px; overflow: hidden; display: flex; justify-content: center; align-items: center;">
        <img id="simg" style="max-width: 100%; max-height: 170%;" />
    </div>
    <script type="text/javascript">
        //修改订单跟踪状态
        function updateOrderStatus() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要查看跟踪状态的数据！', 'info');
                return;
            }
            $('#saveStatus').show();
            var HID = "<%=UserInfor.HouseID%>";
            if (HID == "10" || HID == "13" || HID == "14" || HID == "15" || HID == "23" || HID == "24" || HID == "25" || HID == "27" || HID == "29" || HID == "30" || HID == "32" || HID == "33" || HID == "43" || HID == "58") {
                $('#saveStatus').hide();
            }
            if (row) {
                $('#dlgStatus').dialog('open').dialog('setTitle', '查看订单：' + row.OrderNo + ' 物流跟踪状态');
                $('#fmStatus').form('clear');
                $('#lblTrack').empty();
                $.ajax({
                    async: false,
                    url: "FormService.aspx?method=QueryOrderStatus&OrderNo=" + row.OrderNo + "&HouseID=" + row.HouseID + "&LogisAwbNo=" + row.LogisAwbNo,
                    cache: false,
                    dataType: "json",
                    success: function (text) {
                        var ldl = document.getElementById("lblTrack");
                        ldl.innerHTML = text.HtmlStr;
                    }
                });
            }
        }
        function download(img) {
            var simg = img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);

        }
        //批量导出订单信息
        function BatchExportOrderInfo() {
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "FormService.aspx?method=QueryOrderInfoExport&OrderNo=" + $('#AOrderNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&AcceptUnit=" + $('#AcceptUnit').val(),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnOrderInfo.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //导出订单明细信息
        function ExportOrderGoods() {
            var row = $("#dgSave").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var obj = document.getElementById("<%=btnOrderGoods.ClientID %>"); obj.click();
        }
        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].AwbStatus != 0) {
                    if (rows[i].Piece != 0) {
                        if ("<%=UserInfor.LoginName%>" != "1000" && "<%=UserInfor.LoginName%>" != "2076") {
                            $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已出库无法删除！', 'warning'); return;
                        }
                    }
                }
            }
            $.messager.confirm('<%= Dealer.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'FormService.aspx?method=DelOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
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
        //保存订单
        function SaveOrderUpdate() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            $.messager.confirm('<%= Dealer.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var json = JSON.stringify(row);
                    $.messager.progress({ msg: '请稍后,正在保存中...' });

                    $('#fmDep').form('submit', {
                        url: 'FormService.aspx?method=updateOrderData',
                        onSubmit: function (param) {
                            param.submitData = json;
                        },
                        success: function (msgg) {
                            IsModifyOrder = false;
                            $.messager.progress("close");
                            var result = eval('(' + msgg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            } else {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        },
                        error: function (res) {
                            debugger;
                        }
                    })
                }
            });
        }
        //拉下订单
        function pldown() {
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要拉下订单的数据！', 'warning');
                return;
            }
            var copyRows = [];
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p - Number(row.Piece);
                $('#APiece').numberbox('setValue', Number(pt));

                var index = $('#outDg').datagrid('getRowIndex', copyRows[i].ID);
                if (index >= 0) {

                    var Trow = $("#outDg").datagrid('getData').rows[index];
                    Trow.Piece = Trow.InPiece;
                    $('#outDg').datagrid('updateRow', { index: index, row: Trow });
                } else {
                    //$('#outDg').datagrid('updateRow', { index: 1, row: row });
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgSave').datagrid('getRowIndex', copyRows[i]);
                $('#dgSave').datagrid('deleteRow', index);
            }
        }
        //新增出库数据
        function outOK() {
            var row = $('#outDg').datagrid('getSelected');
            var Total = Number(row.Piece);
            var OutCargoID;
            var RuleType = $('#RuleType').val();
            var dgS = $('#dgSave').datagrid('getRows');
            for (var i = 0; i < dgS.length; i++) {
                OutCargoID = dgS[i].OutCargoID;
                if (dgS[i].Specs == row.Specs && dgS[i].Figure == row.Figure && dgS[i].GoodsCode == row.GoodsCode && dgS[i].LoadIndex == row.LoadIndex && dgS[i].SpeedLevel == row.SpeedLevel && dgS[i].BatchYear == row.BatchYear) {
                    $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '该产品已在订单上，请直接修改数量操作！', 'warning');
                    return;
                }
            }
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var Aindex = $('#InIndex').val();
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (indexD < 0) {
                row.Piece = Number($('#Numbers').numberbox('getValue'));
                row.OutCargoID = OutCargoID;
                row.OrderNo = $('#OrderNo').val();
                var json = JSON.stringify([row])
                //$('#dgSave').datagrid('appendRow', row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#APiece').numberbox('setValue', Number(pt));
                if (Total != Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#outDg').datagrid('updateRow', { index: Aindex, row: row });


                $.messager.progress({ msg: '请稍后,拉上订单中...' });
                $.ajax({
                    url: 'FormService.aspx?method=DrawUpOrder',
                    type: 'post', dataType: 'json', data: { data: json },
                    success: function (text) {
                        $.messager.progress("close");
                        if (text.Result == true) {
                            //刷新列表
                            $('#dgSave').datagrid('clearSelections');
                            var gridOpts = $('#dgSave').datagrid('options');
                            gridOpts.url = 'FormService.aspx?method=QueryOrderByOrderNo';
                            $('#dgSave').datagrid('load', {
                                OrderNo: $('#OrderNo').val()
                            });
                            $('#dlgOutCargo').dialog('close');
                        }
                        else {
                            $('#APiece').numberbox('setValue', Number(p));
                            $('#TransportFee').numberbox('setValue', tCharge);
                            $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                        }
                    }
                });
            } else { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        ///拉上订单
        function plup() {
            var row = $('#outDg').datagrid('getSelected');
            if (CheckStatus != "0") { return; }
            if (row == null || row == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (row) {
                var clientNum = $('#ClientNum').val();
                $('#dlgOutCargo').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure );
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#outDg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');

                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();
            }
        }
        //查询在库产品
        function queryInCargoProduct() {
            $('#outDg').datagrid('clearSelections');
            var gridOpts = $('#outDg').datagrid('options');
            gridOpts.url = 'FormService.aspx?method=QueryALLHouseData';
            $('#outDg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
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
                //收货人
                $('#AAcceptPeople').combobox({
                    valueField: 'ADID',
                    textField: 'AcceptPeople',
                    delay: '10',
                    url: 'FormService.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + row.ClientNum,
                    onSelect: onAcceptAddressChanged,
                    required: true,
                    editable: false
                });
                $('#AAcceptPeople').combobox('textbox').validatebox('options').required = true;

                row.CreateDate = AllDateTime(row.CreateDate);
                $('#fmDep').form('load', row);
                $('#TransportFee').numberbox('setValue', row.TotalCharge);
                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });

                $('#AcceptCity').val(row.Dest);

                TrafficType = row.TrafficType;

                if (row.CheckStatus == "1") {
                    columns.push({ title: '数量', field: 'Piece', width: '50px' });
                } else {
                    columns.push({ title: '数量', field: 'Piece', width: '50px', editor: { type: 'numberbox' } });
                }
                //columns.push({ title: '型号', field: 'Model', width: '60px' });
                columns.push({ title: '规格', field: 'Specs', width: '110px' });
                columns.push({ title: '花纹', field: 'Figure', width: '100px' });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                        return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                    }
                });
                columns.push({ title: '批次', field: 'BatchYear', width: '60px' });
                columns.push({ title: '产品品牌', field: 'TypeName', width: '100px' });
                columns.push({ title: '货品代码', field: 'GoodsCode', width: '180px' });

                showGrid(columns, row.HouseID);

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'FormService.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
            }
        }
        //显示列表
        function showGrid(dgSaveCol, houseID) {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({ title: '货品代码', field: 'GoodsCode', width: '150px' });
            columns.push({ title: '规格', field: 'Specs', width: '90px' });
            columns.push({ title: '花纹', field: 'Figure', width: '90px' });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '70px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({ title: '批次', field: 'BatchYear', width: '60px' });
            columns.push({
                title: '在库件数', field: 'Piece', width: '70px', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            $('#outDg').datagrid({
                width: Number($("#dlgOrder").width()) * 0.5 - 5-50,
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
            var dgSavewidth = Number($("#dlgOrder").width()) * 0.5 - 5+50;
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
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'FormService.aspx?method=UpdateOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            $.messager.progress("close");
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                } else {
                                    ModifyPiece = oldPiece - newPiece;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                }
                                alert_autoClose('<%= Dealer.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
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
        var OldIndex = -1;
        function onClickCell(index, field) {
            if (OldIndex == -1) {
                OldIndex = index;
            } else {
                index = OldIndex;
            }
            var rows = $("#dgSave").datagrid('getData').rows[index];
            if (TrafficType == "2") { OldIndex = -1; return; }
            if ($('#dgSaveAwbStatus').val() * 1 < 1 && field == "Piece") {
                if (endEditing()) {
                    $('#dgSave').datagrid('selectRow', index)
                        .datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            } else {
                if (editIndex == undefined) { OldIndex = -1; return true }
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) {
                    OldIndex = -1;
                    return true;
                }
                var sum = 0;
                if (cg.field == "Piece") {
                    var oldPiece = Number(rows.Piece);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('cancelEdit', editIndex);
                        editIndex = undefined;
                        OldIndex = -1;
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();

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
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        OldIndex = -1;
                        return;
                    }
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'FormService.aspx?method=UpdateOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                } else {

                                    ModifyPiece = oldPiece - newPiece;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                }
                                alert_autoClose('<%= Dealer.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }

                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                OldIndex = -1;
            }
        }
        function onAcceptAddressChanged(item) {
            $('#AAcceptUnit').textbox('setValue', item.AcceptCompany);
            $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
            $('#AAcceptTelephone').textbox('setValue', item.AcceptTelephone);
            $('#AAcceptCellphone').textbox('setValue', item.AcceptCellphone);
            $('#AAcceptPeople').textbox('setValue', item.AcceptPeople);
            $('#HiddenAAcceptPeople').val(item.AcceptPeople);
            $('#AcceptCity').val(item.AcceptCity);
        }

        //关闭弹出框
        function closeDlg() {
            $('#dlgOutCargo').dialog('close');
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
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
    </script>

</asp:Content>
