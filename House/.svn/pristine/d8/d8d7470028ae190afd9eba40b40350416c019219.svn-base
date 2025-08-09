<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderManager.aspx.cs" Inherits="Dealer.OrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
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
            $('#up').hide();
            //$('#save').hide();
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
                title: '订单费用', field: 'Total', width: '75px', align: 'right', formatter: function (value, row) {
                    return "<span title='" + (Number(row.TotalCharge) + Number(row.InsuranceFee)).toFixed(2) + "'>" + (Number(row.TotalCharge) + Number(row.InsuranceFee)).toFixed(2) + "</span>";
                }
            });
            columns.push({
                title: '返利', field: 'InsuranceFee', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '合计', field: 'TotalCharge', width: '60px', align: 'right', formatter: function (value) {
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
                title: '订单备注', field: 'Remark', width: '200px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //    title: '订单状态', field: 'AwbStatus', width: '80px',
            //    formatter: function (val, row, index) {
            //        if (val == "0") { return "<span title='已下单'>已下单</span>"; }
            //        else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
            //        else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
            //        else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
            //        else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
            //        else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
            //        else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
            //        else if (val == "7") { return "<span title='正在配送'>正在配送</span>"; }
            //        else { return ""; }
            //    }
            //});
            columns.push({
                title: '结算状态', field: 'CheckStatus', width: '80px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未结算'>未结算</span>"; }
                    else if (val == "1") { return "<span title='已结算'>已结算</span>"; }
                    else if (val == "2") { return "<span title='未结清'>未结清</span>"; }
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
                    if (row.CheckStatus == "0") { return "background-color:#FCF3CF"; };
                    //if (row.PostponeShip == "1") { return "background-color:#FFCC66"; };
                    if (new Date().getTime() > new Date(new Date(row.CreateDate).setHours(new Date(row.CreateDate).getHours() + row.AgingDelayTime)).getTime()) {
                        return "background-color:#e68585;color:#2a83de";
                    };
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
            gridOpts.url = '../FormService.aspx?method=QueryOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                CheckStatus: $("#ACheckStatus").combobox('getValue'),
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
                <td style="text-align: right;">结算状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="ACheckStatus" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结算</option>
                        <option value="2">未结清</option>
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
                        <td class="TotalCharge" style="text-align: right;">费用合计:
                        </td>
                        <td class="TotalCharge">
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2" readonly="true" style="width: 80px;" />
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
                <tr>
                    <td style="text-align: right;">系统销售价：</td>
                    <td>
                        <input type="hidden" id="RuleType" />
                        <input type="hidden" id="RuleID" />
                        <input type="hidden" id="SuitClientNum" />
                        <input type="hidden" id="Regular" />
                        <input type="hidden" id="RuleTitle" />
                        <input type="hidden" id="SystemSalePrice" />
                        <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 200px;" readonly="readonly"/>
                    </td>
                </tr>
            </table>
            <div id="lblRule">
            </div>
        </form>
    </div>
    <div id="dlgOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->

    <script type="text/javascript">
        //批量导出订单信息
        function BatchExportOrderInfo() {
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "../FormService.aspx?method=QueryOrderInfoExport&OrderNo=" + $('#AOrderNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&AcceptUnit=" + $('#AcceptUnit').val(),
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
                        url: '../FormService.aspx?method=DelOrder',
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
                        url: '../FormService.aspx?method=updateOrderData',
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
                var SalePrice = Number(row.ActSalePrice);//销售价
                var NC = SalePrice * Number(row.Piece);
                $('#TransportFee').numberbox('setValue', tCharge - NC);

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
            var SalePrice = $('#ActSalePrice').numberbox('getValue');// Number(row.SalePrice);//销售价
            var OutCargoID;
            var RuleType = $('#RuleType').val();
            var dgS = $('#dgSave').datagrid('getRows');
            for (var i = 0; i < dgS.length; i++) {
                OutCargoID = dgS[i].OutCargoID;
                if (dgS[i].Specs == row.Specs && dgS[i].Figure == row.Figure && dgS[i].GoodsCode == row.GoodsCode && dgS[i].LoadIndex == row.LoadIndex && dgS[i].SpeedLevel == row.SpeedLevel && dgS[i].TradePrice == row.TradePrice) {
                    $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '该产品已在订单上，请直接修改数量操作！', 'warning');
                    return;
                }
            }
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var Aindex = $('#InIndex').val();
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (indexD < 0) {
                if (RuleType != "" && RuleType != null) {
                    row.IsRuleBank = 1;
                    row.RuleType = RuleType;
                    row.RuleID = $('#RuleID').val();
                    row.RuleTitle = $('#RuleTitle').val();
                    row.SuitClientNum = $('#SuitClientNum').val();

                    if (RuleType.indexOf('1') > -1) {

                    }
                } else {
                    row.IsRuleBank = "";
                    row.RuleType = "";
                    row.RuleID = "";
                    row.RuleTitle = "";
                    row.SuitClientNum = "";
                }
                row.Piece = Number($('#Numbers').numberbox('getValue'));
                row.ActSalePrice = SalePrice;
                row.OutCargoID = OutCargoID;
                row.OrderNo = $('#OrderNo').val();
                var json = JSON.stringify([row])
                //$('#dgSave').datagrid('appendRow', row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $('#TransportFee').numberbox('setValue', tCharge + NC);
                if (Total != Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#outDg').datagrid('updateRow', { index: Aindex, row: row });


                $.messager.progress({ msg: '请稍后,拉上订单中...' });
                $.ajax({
                    url: '../FormService.aspx?method=DrawUpOrder',
                    type: 'post', dataType: 'json', data: { data: json },
                    success: function (text) {
                        $.messager.progress("close");
                        if (text.Result == true) {
                            //刷新列表
                            $('#dgSave').datagrid('clearSelections');
                            var gridOpts = $('#dgSave').datagrid('options');
                            gridOpts.url = '../FormService.aspx?method=QueryOrderByOrderNo';
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
        $("body").on('click', '.RuleBankCheck', function () {
            var id = $(this).attr("id");
            var name = $(this).attr("name");
            var title = $(this).attr("value");
            if (this.checked) {
                $("#rule").find('input[type=checkbox]').not(this).attr("checked", false);
                UselRuleBank(id, name, title);
            } else {
                CancelRuleBank(id, name, title);
            }
        });

        function UselRuleBank(id, name, title) {
            var RuleType = $('#RuleType').val();
            var SystemSalePrice = $('#SystemSalePrice').val();

            var ThisRuleType = $('#RuleType' + id).val();
            if (ThisRuleType == RuleType) {
                RuleType = null;
                $('#RuleType').val("");
                $('#RuleID').val("");
                $('#RuleTitle').val("");
                $('#SuitClientNum').val("");
            }
            switch (parseInt(ThisRuleType)) {
                case 0:
                    break;
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 6:
                    var ThisRuleContent = $('#CutEntry' + id).val();
                    if (RuleType == null || RuleType == "") {
                        $('#RuleType').val(6);
                        $('#RuleID').val(id);
                        $('#RuleTitle').val(title);
                        $('#SuitClientNum').val(name);
                        var price = ($('#ActSalePrice').val() * 1 - ThisRuleContent);
                    } else {
                        if (RuleType.indexOf(',6') < 0) {
                            $('#RuleType').val(RuleType + ',' + 6);
                            $('#RuleID').val($('#RuleID').val() + ',' + id);
                            $('#RuleTitle').val($('#RuleTitle').val() + ',' + title);
                            $('#SuitClientNum').val($('#SuitClientNum').val() + ',' + name);
                        } else {
                            $('#RuleID').val($('#HiddenLimitID').val() + ',' + id);
                            $('#RuleTitle').val($('#HiddenLimitTitle').val() + ',' + title);
                            $('#SuitClientNum').val($('#SuitClientNum').val() + ',' + name);
                        }
                        var price = (SystemSalePrice * 1 - ThisRuleContent);
                    }
                    $('#ActSalePrice').numberbox('setValue', price);
                    break;
                default:
                    break;
            }
        }
        function CancelRuleBank(id, name, title) {
            var RuleType = $('#RuleType').val();

            var ThisRuleType = $('#RuleType' + id).val();
            switch (parseInt(ThisRuleType)) {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 6:
                    var ThisRuleContent = $('#CutEntry' + id).val();
                    if (RuleType.indexOf(",") < 0) {
                        $('#RuleType').val("");
                        $('#RuleID').val("");
                        $('#RuleTitle').val("");
                        $('#SuitClientNum').val("");
                        var price = ($('#ActSalePrice').val() * 1 + ThisRuleContent * 1);
                    } else {
                        $('#RuleType').val($('#RuleType').val().replace(',6', ''));
                        $('#RuleID').val($('#RuleID').val().replace(',' + id, ''));
                        $('#RuleTitle').val($('#RuleTitle').val().replace(',' + title, ''));
                        $('#SuitClientNum').val($('#SuitClientNum').val().replace(',' + name, ''));
                        var price = ($('#ActSalePrice').val() * 1 + ThisRuleContent * 1);
                    }
                    $('#ActSalePrice').numberbox('setValue', price);
                    //$("#ActSalePrice").numberbox('enable');
                    break;
                default:
                    break;
            }
        }
        ///拉上订单
        function plup() {
            var row = $('#outDg').datagrid('getSelected');
            debugger;
            if (CheckStatus != "0") { return; }
            if (row == null || row == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (Number(row.TypeParentID) == 1 && Number(row.SalePrice) <= 0) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请先录入销售价格', 'warning');
                return;
            }
            $('#RuleType').val("");
            $('#RuleID').val("");
            $('#SuitClientNum').val("");
            $('#Regular').val("");
            $('#RuleTitle').val("");
            $('#SystemSalePrice').val("");
            if (row) {
                $.ajax({
                    url: "../FormService.aspx?method=QueryPriceRuleBankInfo&HouseID=" + row.HouseID + "&TypeID=" + row.TypeID + "&Specs=" + encodeURIComponent(row.Specs) + "&Figure=" + encodeURIComponent(row.Figure) + "&LoadIndex=" + encodeURIComponent(row.LoadIndex) + "&SpeedLevel=" + encodeURIComponent(row.SpeedLevel) + "&Batch=" + row.Batch + "&ClientNum=" + <%=UserInfor.LoginName%>,
                    cache: false,
                    async: true,
                    dataType: "json",
                    success: function (text) {
                        $("#lblRule").html(text.Html);
                        $.parser.parse($("#lblRule"));
                        if (text.QuotaNum != -1) {
                            var RuleID = $('#LimitID').val();
                            var RuleTitle = $('#LimitTitle').val();
                            $('#RuleType').val(4);
                            $('#RuleID').val(RuleID);
                            $('#RuleTitle').val(RuleTitle);
                            $('#HiddenLimitID').val(RuleID);
                            $('#HiddenLimitTitle').val(RuleTitle);
                            var outDgrows = $("#outDg").datagrid('getData').rows;
                            var num = 0;
                            if (outDgrows.length > 0) {
                                for (var i = 0; i < outDgrows.length; i++) {
                                    var batch = outDgrows[i].Batch;
                                    if (batch.length == 4) {
                                        var batch1 = batch.substring(0, 2);
                                        var batch2 = batch.substring(2);
                                        batch = batch2 + batch1;
                                    }

                                    var ifstr = 1 == 1;
                                    if (text.QuotaSpecs != "") {
                                        ifstr = ifstr && text.QuotaSpecs == outDgrows[i].Specs;
                                    }
                                    if (text.QuotaFigure != "") {
                                        ifstr = ifstr && text.QuotaFigure == outDgrows[i].Figure;
                                    }
                                    if (text.QuotaStartBatch != -1) {
                                        ifstr = ifstr && text.QuotaStartBatch <= batch;
                                    }
                                    if (text.QuotaEndBatch != -1) {
                                        ifstr = ifstr && text.QuotaEndBatch >= batch;
                                    }
                                    if (ifstr) {
                                        var piece = parseInt(outDgrows[i].InPiece) - parseInt(outDgrows[i].Piece);
                                        num = parseInt(num) + parseInt(piece);
                                    }
                                }
                            } else {

                            }
                            if (parseInt(num) >= parseInt(text.QuotaNum)) {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '此产品达到限购数量', 'warning');
                                $('#dlg').dialog('close');
                                return;
                            } else {
                                var quotaNmu = parseInt(text.QuotaNum) - num;
                                if (row.Piece < quotaNmu) {
                                    quotaNmu = row.Piece;
                                }
                                $('#Numbers').numberbox({
                                    min: 0,
                                    max: parseInt(quotaNmu),
                                    precision: 0
                                });
                                $('#dlg').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
                            }
                        }
                    }
                });

                var clientNum = $('#ClientNum').val();
                $('#dlgOutCargo').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure );
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#outDg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                if (row.SalePriceClient != undefined && row.SalePriceClient != "") {
                    $('#ActSalePrice').numberbox('setValue', row.SalePriceClient);
                } else {
                    $('#ActSalePrice').numberbox('setValue', row.TradePrice);
                }
                $('#SystemSalePrice').val(row.SalePrice);

                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();
                $("#ActSalePrice").numberbox('disable');
            }
        }
        //查询在库产品
        function queryInCargoProduct() {
            $('#outDg').datagrid('clearSelections');
            var gridOpts = $('#outDg').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryALLHouseData';
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
                    url: '../FormService.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + row.ClientNum,
                    onSelect: onAcceptAddressChanged,
                    required: true,
                    editable: false
                });
                $('#AAcceptPeople').combobox('textbox').validatebox('options').required = true;

                bindMethod();
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
                columns.push({ title: '销售价', field: 'ActSalePrice', width: '60px' });
                //columns.push({ title: '型号', field: 'Model', width: '60px' });
                columns.push({ title: '规格', field: 'Specs', width: '110px' });
                columns.push({ title: '花纹', field: 'Figure', width: '100px' });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                        return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                    }
                });
                columns.push({
                    title: '批次', field: 'BatchYear', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({ title: '产品品牌', field: 'TypeName', width: '100px' });
                columns.push({ title: '货品代码', field: 'GoodsCode', width: '130px' });

                showGrid(columns, row.HouseID);

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = '../FormService.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
            }
        }
        //显示列表
        function showGrid(dgSaveCol, houseID) {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({ title: '货品代码', field: 'GoodsCode', width: '110px' });
            columns.push({ title: '规格', field: 'Specs', width: '90px' });
            columns.push({ title: '花纹', field: 'Figure', width: '90px' });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '70px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'BatchYear', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '在库件数', field: 'Piece', width: '70px', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    if (value < 9) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    } else if (value > 8 && value < 21) {
                        return "<span title='紧张'>紧张</span>";
                    } else {
                        return "<span title='充足'>充足</span>";
                    }
                }
});
            columns.push({ title: '销售价', field: 'SalePrice', width: '70px' });
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
                    if (row.RuleType == "1") { return "background-color:#f5a99a"; };
                    if (row.RuleType == "2") { return "background-color:#e1bd7f"; };
                    if (row.RuleType == "3") { return "background-color:#D4EFDF"; };
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
                        url: '../FormService.aspx?method=UpdateOrderPiece',
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
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    qh();
                                } else {
                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    qh();
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
                    var salePrice = Number(rows.ActSalePrice);
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
                        url: '../FormService.aspx?method=UpdateOrderPiece',
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
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    qh();
                                } else {

                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    qh();
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
        //绑定费用框
        function bindMethod() {
            $("#TransportFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t =  Number($('#TransportFee').numberbox('getValue')) ;
            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
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
<%--            if (IsModifyOrder) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请先保存订单再关闭！', 'warning');
                return;
            }--%>
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
