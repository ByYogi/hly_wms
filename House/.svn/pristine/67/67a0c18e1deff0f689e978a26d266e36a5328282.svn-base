<%@ Page Title="退货管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="orderReturnManager.aspx.cs" Inherits="Cargo.Order.orderReturnManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
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
                toolbar: '',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    {
                        title: '退货单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '退货仓库', field: 'OutHouseName', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '退货件数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#f1866b;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '退货金额', field: 'TransportFee', width: '60px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '合计', field: 'TotalCharge', width: '80px', align: 'right', formatter: function (value) {
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
                    {
                        title: '业务员', field: 'SaleManName', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                    {
                        title: '审核状态', field: 'FinanceSecondCheck', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未审核'>未审核</span>"; }
                            else if (val == "1") { return "<span title='已审核'>已审核</span>"; }
                            else { return "未审核"; }
                        }
                    },
                    {
                        title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未结算'>未结算</span>"; }
                            else if (val == "1") { return "<span title='已结算'>已结算</span>"; }
                            else if (val == "2") { return "<span title='未结清'>未结清</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '订单状态', field: 'AwbStatus', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                            else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                            else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                            else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                            else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                            else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '下单方式', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='电脑下单'>电脑下单</span>"; }
                            else if (val == "1") { return "<span title='企业号下单'>企业号下单</span>"; }
                            else if (val == "2") { return "<span title='公众号下单'>公众号下单</span>"; }
                            else if (val == "4") { return "<span title='小程序下单'>小程序下单</span>"; }
                            else { return "<span title='电脑下单'>电脑下单</span>"; }
                        }
                    },
                    //{
                    //    title: '物流公司名称', field: 'LogisticName', width: '90px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                    {
                        title: '物流公司单号', field: 'LogisAwbNo', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '物流配送费用', field: 'TransitFee', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
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
            $('#eHouseID').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            $('#HAID').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            $('#AOrderType').combobox('textbox').bind('focus', function () { $('#AOrderType').combobox('showPanel'); });
            //$('#CheckOutType').combobox('textbox').bind('focus', function () { $('#CheckOutType').combobox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#ASaleManID').combobox('clear');
                    var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=' + rec.HouseID;
                    $('#ASaleManID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#ASaleManID').combobox('textbox').bind('focus', function () { $('#ASaleManID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#ASaleManID').combobox('clear');
            var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=<%=UserInfor.HouseID%>';
            $('#ASaleManID').combobox('reload', url);
            $('#eHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#HAID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#HAID').combobox('reload', url);
                }
            });
            RoleCName = "<%=UserInfor.RoleCName%>";
            if (RoleCName.indexOf("安泰路斯") >= 0) {
                $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ASaleManID').combobox('setValue', 2370);
                $('#ASaleManID').combobox('setText', '赵武凯');
                $("#ASaleManID").combobox("readonly", true);
                $('#ASaleManID').combobox('textbox').unbind('focus');
                $('#ASaleManID').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AOrderType").combobox("readonly", true);
                $('#AOrderType').combobox('textbox').unbind('focus');
                $('#AOrderType').combobox('textbox').css('background-color', '#e8e8e8');
            }
            else if (RoleCName.indexOf("汕头科矿") >= 0) {
                $('#eHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $("#eHouseID").combobox("readonly", true);
                $('#eHouseID').combobox('textbox').unbind('focus');
                $('#eHouseID').combobox('textbox').css('background-color', '#e8e8e8');
                $('#HAID').combobox('clear');
                var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
                $('#HAID').combobox('reload', url);
                $('#HAID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                $('#HAID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                $("#HAID").combobox("readonly", true);
                $('#HAID').combobox('textbox').css('background-color', '#e8e8e8');

            }

        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                Piece: $("#Piece").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                CheckOutType: '',
                OrderType: $("#AOrderType").combobox('getValue'),
                Dep: $("#eHouseID").combobox('getValue'),
                Dest: $("#HAID").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                SaleManID: $('#ASaleManID').combobox('getValue'),
                ThrowGood: "5",
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
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入退货单号'" style="width: 100px">
                </td>

                <td style="text-align: right;">退货客户:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">退货件数:
                </td>
                <td>
                    <input id="Piece" class="easyui-textbox" data-options="prompt:'请输入退货件数'" style="width: 60px">
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="eHouseID" name="eHouseID" class="easyui-combobox" style="width: 95px;" data-options="required:false" panelheight="auto" />
                </td>
                <td style="text-align: right;">退货仓库:
                </td>
                <td>
                    <input id="HAID" name="HAID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'AreaID',textField:'Name',required:false" panelheight="auto" />
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td style="text-align: right;">下单方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="AOrderType" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">电脑下单</option>
                        <option value="1">企业号下单</option>
                        <option value="2">公众号下单</option>
                        <option value="4">小程序下单</option>
                    </select>
                </td>


                <%--                <td style="text-align: right;">付款方式:
                </td>
                <td>
                    <input class="easyui-combobox" id="CheckOutType" data-options="url:'../Data/check.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                        style="width: 70px;">
                </td>--%>
            </tr>
            <tr>
                <td colspan="10">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-lorry_add" plain="false" onclick="addLogisNo()">&nbsp;输入物流单号&nbsp;</a>
                    <%--<a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="updateOrderStatus()">&nbsp;修改退货单状态&nbsp;</a>--%>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
                    <a href="#" id="btnExportOrder" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExportOrderInfo()">&nbsp;导出订单列表&nbsp;</a>&nbsp;&nbsp;
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnOrder" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                        <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />
                    </form>
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" ssss />
                </td>
                <td style="text-align: right;">业务员:
                </td>
                <td>
                    <input id="ASaleManID" class="easyui-combobox" style="width: 100px;"
                        data-options="valueField: 'LoginName',textField: 'UserName'" />
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
        <%--<table id="outDg" class="easyui-datagrid">
        </table>--%>
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
                        <td style="text-align: right;">退货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AAcceptPeople" style="width: 80px;" class="easyui-combobox" />
                        </td>
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
                            <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox"
                                style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">手机:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:true" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">业务员:
                        </td>
                        <td>
                            <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 100px;"
                                data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged," />
                        </td>
                        <td class="LogisID" style="text-align: right;">物流名称:
</td>
<td class="LogisID">
    <input name="LogisID" id="LogisID" class="easyui-combobox" style="width: 85px;" data-options="panelHeight:'200px',valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'"
        panelheight="auto" />
</td>

                    </tr>

                    <tr>
                        <td style="text-align: right;">退货件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true"
                                style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">退货费用:
                        </td>
                        <td>
                            <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">开单员:
                        </td>
                        <td>
                            <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="true" style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">开单时间:
                        </td>
                        <td colspan="3">
                            <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime"
                                readonly="true" style="width: 150px;" />
                        </td>
                    </tr>


                    <tr>
                        <%--<td style="text-align: right;" rowspan="2">副单号:
                            </td>
                            <td colspan="4" rowspan="2">
                                <textarea name="HAwbNo" id="AHAwbNo" rows="3" style="width: 300px;"></textarea>
                            </td>--%>
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
        <%--  <a href="#" class="easyui-linkbutton"
            iconcls="icon-basket_put" plain="false" onclick="pldown()" id="down">&nbsp;拉下退货单&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-ok"
                    onclick="SaveOrderUpdate()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;--%>
        <a href="#" class="easyui-linkbutton" id="payprint" iconcls="icon-printer" onclick="prePrint()">&nbsp;打印退货单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton"
            iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>

    <!--Begin 出库操作-->

    <div id="dlgOutCargo" class="easyui-dialog" style="width: 350px; height: 200px; padding: 5px 5px"
        closed="true" buttons="#dlgOutCargo-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0"
                            style="width: 200px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">业务员价格：</td>
                    <td>
                        <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:0"
                            style="width: 200px;">
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
    <!--Begin 输入物流单号操作-->
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 10px"
        closed="true" buttons="#dlg-buttons">
        <form id="fmLogic" class="easyui-form" method="post">
            <input type="hidden" name="OrderID" id="BOrderID" />
            <input type="hidden" name="OrderNo" id="BOrderNo" />
            <table>
                <tr>
                    <td style="text-align: right;">物流公司名称:
                    </td>
                    <td>
                        <input name="LogisID" id="ALogisID" class="easyui-combobox" data-options="panelHeight:'200px',valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'"
                            panelheight="auto" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">物流快递单号:
                    </td>
                    <td>
                        <input name="LogisAwbNo" id="BLogisAwbNo" class="easyui-textbox" data-options="required:true">
                    </td>
                </tr>
                <tr>

                    <td style="text-align: right;">物流送货费用:
                    </td>
                    <td>
                        <input name="BTransitFee" id="BTransitFee" data-options="min:0,precision:2" class="easyui-numberbox" />
                    </td>
                </tr>

            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>
    <!--End 输入物流单号操作-->

    <!--订单状态跟踪-->
    <div id="dlgStatus" class="easyui-dialog" style="width: 450px; height: 350px; padding: 3px 3px"
        closed="true" closable="false" buttons="#dlgStatus-buttons">
        <form id="fmStatus" class="easyui-form" method="post">
            <input type="hidden" name="OrderID" id="SOrderID" />
            <input type="hidden" name="OrderNo" id="SOrderNo" />
            <div id="saPanel">
                <table class="mini-toolbar" style="width: 100%;">
                    <tr>
                        <td rowspan="3" style="border-right: 1px solid #909aa6;">订<br />
                            单<br />
                            在<br />
                            途<br />
                            跟<br />
                            踪
                        </td>
                        <td align="left">&nbsp;&nbsp;状态：
                        </td>
                        <td align="left">
                            <input name="OrderStatus" id="AOrderStatus1" type="radio" value="2"><label for="AOrderStatus1" style="font-size: 14px;">已出库</label>
                            <input name="OrderStatus" id="AOrderStatus2" type="radio" value="3"><label for="AOrderStatus2" style="font-size: 14px;">运输在途</label>
                            <input name="OrderStatus" id="AOrderStatus3" type="radio" value="4"><label for="AOrderStatus3" style="font-size: 14px;">已到达</label>
                            <input name="OrderStatus" id="AOrderStatus4" type="radio" value="5"><label for="AOrderStatus4" style="font-size: 14px;">已签收</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <textarea name="DetailInfo" id="DetailInfo" cols="60" style="height: 55px;" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="lblTrack">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlgStatus-buttons">
        <a href="#" class="easyui-linkbutton" id="saveStatus" iconcls="icon-ok" onclick="saveStatus()">&nbsp;保&nbsp;存&nbsp;</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="closeDlgStatus()">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--订单状态跟踪-->
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
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

        //批量导出订单信息
        function AwbExport() {
            $.ajax({
                url: "orderApi.aspx?method=QueryOrderInfoExport&OrderNo=" + $('#AOrderNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&Piece=" + $("#Piece").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&CheckOutType=''&ThrowGood=5&OrderType=" + $("#AOrderType").combobox('getValue') + "&Dep=" + $("#eHouseID").combobox('getText') + "&Dest=" + $("#HAID").combobox('getText') + "&HouseID=" + $("#AHouseID").combobox('getValue') + "&SaleManID=" + $('#ASaleManID').combobox('getValue') + "&OrderModel=1",
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnOrder.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //批量导出订单信息
        function AwbExportOrderInfo() {
            $.ajax({
                url: "orderApi.aspx?method=QuerReturnOrderInfo&OrderNo=" + $('#AOrderNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&Piece=" + $("#Piece").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&CheckOutType=''&ThrowGood=5&OrderType=" + $("#AOrderType").combobox('getValue') + "&Dep=" + $("#eHouseID").combobox('getText') + "&Dest=" + $("#HAID").combobox('getText') + "&HouseID=" + $("#AHouseID").combobox('getValue') + "&SaleManID=" + $('#ASaleManID').combobox('getValue') + "&OrderModel=1",
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnOrderInfo.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //输入物流单号
        function addLogisNo() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要输入物流单号的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', row.OrderNo + ' 输入物流快递单号');
                $('#fmLogic').form('clear');
                $('#BOrderID').val(row.OrderID);
                $('#BOrderNo').val(row.OrderNo);
                $('#BLogisAwbNo').textbox('setValue', $.trim(row.LogisAwbNo));

                $('#BTransitFee').numberbox('setValue', row.TransitFee);

            }
        }
        //保存物流公司单号
        function saveItem() {
            $('#fmLogic').form('submit', {
                url: 'orderApi.aspx?method=UpdateReturnLogisAwbNo',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $('#dlg').dialog('close'); 	// close the dialog
                        //dosearch();
                        $('#dg').datagrid('reload');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //保存订单跟踪状态
        function saveStatus() {
            $('#fmStatus').form('submit', {
                url: 'orderApi.aspx?method=SaveOrderStatus',
                onSubmit: function () {
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#saveStatus').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#saveStatus').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '更新成功!', 'info');
                        $.ajax({
                            async: false,
                            url: "orderApi.aspx?method=QueryOrderStatus&OrderNo=" + escape($('#SOrderNo').val()),
                            cache: false,
                            success: function (text) {
                                var ldl = document.getElementById("lblTrack");
                                ldl.innerHTML = text;
                            }
                        });
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //修改订单跟踪状态
        function updateOrderStatus() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改跟踪状态的数据！', 'warning');
                return;
            }
            if (row.OrderStatus == "5") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单：' + row.OrderNo + '已签收，不能修改', 'warning');
                return;
            }
            if (row) {
                $('#dlgStatus').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo + ' 跟踪状态');
                $('#fmStatus').form('clear');
                $('#SOrderID').val(row.OrderID);
                $('#SOrderNo').val(row.OrderNo);
                $.ajax({
                    async: false,
                    url: "orderApi.aspx?method=QueryOrderStatus&OrderNo=" + row.OrderNo,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblTrack");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        //关闭订单状态跟踪弹出框
        function closeDlgStatus() {
            $('#dlgStatus').dialog('close');
            $('#dg').datagrid('reload');
        }
        //删除订单信息
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
                        url: 'orderApi.aspx?method=DelReturnOrder',
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
        //保存订单
        function SaveOrderUpdate() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var json = JSON.stringify(row);

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=updateOrderData',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                var dd = result.Message.split('/');
                                $('#ONum').val(dd[0]);
                                $('#OutNum').val(dd[1]);
                                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功，是否打印发货单？', function (r) {
                                    if (r) { prePrint(); }//打印
                                });
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning'); }
                        }
                    })
                }
            });
        }
        //拉下订单
        function pldown() {
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉下订单的数据！', 'warning');
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
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgSave').datagrid('getRowIndex', copyRows[i]);
                $('#dgSave').datagrid('deleteRow', index);
            }
        }
        //关闭
        function closedgShowData() {
            $('#dlgOutCargo').dialog('close');
        }
        //双击显示订单详细界面
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                if (RoleCName.indexOf("安泰路斯") >= 0) {
                    $("#SaleManID").combobox("readonly", true);
                    $('#SaleManID').combobox('textbox').unbind('focus');

                }
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改退货单：' + row.OrderNo);
                $('#fmDep').form('clear');
                //客户姓名
                $('#AAcceptPeople').combobox({
                    valueField: 'Boss',
                    textField: 'Boss',
                    delay: '10',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                    onSelect: onAcceptAddressChanged,
                    required: true
                });
                bindMethod();
                $('#fmDep').form('load', row);
                showGrid();

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
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
                    { title: '件数', field: 'Piece', width: '40px', editor: { type: 'numberbox' } },
                    { title: '关联订单', field: 'RelateOrderNo', width: '100px' },
                    { title: '业务员价', field: 'ActSalePrice', width: '55px', editor: { type: 'numberbox', options: { precision: 2 } } },
                    { title: '单价', field: 'UnitPrice', width: '55px' },
                    { title: '规格', field: 'Specs', width: '80px' },
                    { title: '花纹', field: 'Figure', width: '100px' },
                    { title: '型号', field: 'Model', width: '80px' },
                    { title: '载重', field: 'LoadIndex', width: '40px' },
                    { title: '速度', field: 'SpeedLevel', width: '40px' },
                    //{ title: '批次', field: 'Batch', width: '50px' },
                    { title: '批次', field: 'BatchYear', width: '50px', formatter: function (val, row, index) { return val + "年" } },
                    { title: '货品代码', field: 'GoodsCode', width: '70px' },
                    { title: '产品类型', field: 'TypeName', width: '60px' },
                    {
                        title: '归属部门', field: 'BelongDepart', width: '100px', formatter: function (value) {
                            return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                        }
                    },
                    { title: '产品名称', field: 'ProductName', width: '100px' },
                    { title: '货位代码', field: 'ContainerCode', width: '80px' },
                    { title: '所在区域', field: 'AreaName', width: '60px' },
                    { title: '所在仓库', field: 'FirstAreaName', width: '80px' },
                    //{ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter },
                    { title: '销售价', field: 'SalePrice', hidden: true },
                    //{ title: '胎面宽度', field: 'TreadWidth', width: '60px' },
                    //{ title: '扁平比', field: 'FlatRatio', width: '60px' },
                    //{ title: '子午线', field: 'Meridian', width: '60px' },
                    //{ title: '轮毂直径', field: 'HubDiameter', width: '60px' },
                    { title: '最高速度', field: 'SpeedMax', width: '60px' },
                    { title: '尺寸', field: 'Size', width: '80px' },
                    { title: '产品来源', field: 'SourceName', width: '80px' },
                    //{ title: '数量', field: 'Numbers', width: '60px' },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onClickCell: onClickCell,
                groupField: 'TypeParentName',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value;
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

        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgSave').datagrid('validateRow', editIndex)) {
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                var sum = 0;
                if (cg.field == "Piece") {
                    //修改件数
                }
                if (cg.field == "ActSalePrice") {
                    //修改销售价
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            //件数和销售价允许修改
            if (field == "Piece" || field == "ActSalePrice") {
                if (endEditing()) {
                    $('#dgSave').datagrid('selectRow', index)
                        .datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            }
            else {
                if (editIndex == undefined) { return true }
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
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
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
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
                    var ModifyPiece = 0, ModifyPrice = 0;
                    //说明是增加价格
                    if (newPrice > oldPrice) {
                        ModifyPrice = newPrice - oldPrice;
                        ModifyPiece = ModifyPrice * piece;
                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                        $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPiece).toFixed(2));
                        qh();
                    } else {
                        ModifyPrice = oldPrice - newPrice;
                        ModifyPiece = ModifyPrice * piece;

                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                        $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPiece).toFixed(2));
                        qh();
                    }
                    rows.SalePrice = oldPrice;
                    rows.ActSalePrice = cg.target.val();
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderSalePrice',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }

                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }
        //绑定费用框
        function bindMethod() {
            $("#TransportFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t = Number($('#TransportFee').numberbox('getValue'));
            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
        }
        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleManName').val(item.UserName);
                $('#SaleCellPhone').val(item.CellPhone);
            }
        }
        //收货人自动选择方法
        function onAcceptAddressChanged(item) {
            if (item) {
                $('#AAcceptUnit').textbox('setValue', item.ClientName);
                $('#AAcceptAddress').textbox('setValue', item.Address);
                $('#AAcceptTelephone').textbox('setValue', item.Telephone);
                $('#AAcceptCellphone').textbox('setValue', item.Cellphone);
            }
        }
        //重置
        function reset() {
            // prePrint();
            $('#fmDep').form('clear');
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });

            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            var title = "";
            $('#dgSave').datagrid("getPanel").panel("setTitle", title);
        }
        //关闭弹出框
        function closeDlg() {
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
        var LODOP;
        //订单打印
        function prePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var griddata = $('#dgSave').datagrid('getRows');
            var js = 0, Alltotal = 0, AllPiece = 0; p = 1; pie = 0; total = 0;
            for (var k = 0; k < griddata.length; k++) {
                pie = Number(griddata[k].Piece);
                total = Number(pie) * Number(griddata[k].ActSalePrice);
                Alltotal += total;
                AllPiece += Number(pie);
            }
            for (var i = 0; i < griddata.length; i++) {
                if (i == (p - 1) * 10) {
                    if (p > 1) {
                        LODOP.NewPage();
                    }
                    p++;
                    LODOP.SET_PRINT_PAGESIZE(3, 2100, 30, "");
                    LODOP.ADD_PRINT_RECT(-2, 2, 788, 522, 0, 1);

                    LODOP.ADD_PRINT_LINE(31, 3, 32, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(58, 3, 57, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(86, 82, 31, 83, 0, 1);
                    LODOP.ADD_PRINT_LINE(133, 615, 31, 616, 0, 1);
                    LODOP.ADD_PRINT_LINE(87, 3, 86, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(110, 3, 109, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(133, 121, 86, 122, 0, 1);
                    LODOP.ADD_PRINT_LINE(134, 3, 133, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(134, 247, 57, 248, 0, 1);
                    LODOP.ADD_PRINT_LINE(134, 397, 57, 398, 0, 1);
                    LODOP.ADD_PRINT_LINE(133, 523, 31, 524, 0, 1);

                    LODOP.ADD_PRINT_LINE(396, 3, 395, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(479, 58, 395, 59, 0, 1);
                    LODOP.ADD_PRINT_LINE(453, 285, 434, 286, 0, 1);
                    LODOP.ADD_PRINT_LINE(415, 3, 414, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(479, 553, 395, 554, 0, 1);
                    LODOP.ADD_PRINT_LINE(453, 648, 395, 649, 0, 1);
                    LODOP.ADD_PRINT_LINE(435, 3, 434, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(455, 3, 454, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(480, 3, 479, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(454, 384, 435, 385, 0, 1);

                    var hous = '<%= HouseName%>';
                    var com = '湖南省迪乐泰贸易有限公司退货清单';
                    if (hous.indexOf('湖北') != -1) {
                        com = '湖北省迪乐泰贸易有限公司退货清单';
                    }
                    if (hous.indexOf('西安') != -1) {
                        com = '陕西省西安新陆程物流有限公司退货清单';
                    }
                    if (hous.indexOf('梅州') != -1) {
                        com = '梅州市新陆程供应链管理有限公司退货清单';
                    }
                    if (hous.indexOf('广东') != -1) {
                        com = '广州迪乐泰有限公司退货清单';
                    }
                    if (hous.indexOf('广州') != -1) {
                        com = '广州迪乐泰有限公司退货清单';
                    }
                    if (hous.indexOf('海南') != -1) {
                        com = '海南迪乐泰有限公司退货清单';
                    }
                    if (hous.indexOf('揭阳') != -1) {
                        com = '揭阳迪乐泰有限公司退货清单';
                    }
                    if (hous.indexOf('四川') != -1) {
                        com = '四川迪乐泰有限公司退货清单';
                    }
                    if (hous.indexOf('广通慧采') != -1) {
                        com = '广通轮胎销售退货清单';
                    }
                    if (hous.indexOf('沈阳') != -1) {
                        com = '广州迪乐泰有限公司退货清单';
                    }
                    //var sendTitle = '<%=SendTitle%>'
                    LODOP.ADD_PRINT_TEXT(1, 206, 541, 33, com);
                    LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_IMAGE(-1, 7, 198, 32, "");
                    LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                    LODOP.ADD_PRINT_TEXT(37, 5, 87, 26, "退货地址：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(37, 91, 440, 27, $('#AAcceptAddress').textbox('getValue'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(37, 533, 85, 26, "退货日期：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(37, 633, 125, 27, getNowFormatDate($('#CreateDate').datetimebox('getValue')));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 5, 87, 26, "物流公司：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 91, 136, 27, $('#ALogisID').combobox('getText'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 278, 100, 26, "物流费用：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 411, 96, 27, "");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_TEXT(64, 533, 85, 26, "退货单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 633, 104, 27, $('#OrderNo').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 15, 79, 25, "发货单位");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 143, 56, 25, "发货人");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 279, 75, 25, "联系电话");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 422, 74, 25, "收货单位");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 545, 58, 25, "收货人");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 651, 80, 25, "联系电话");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 10, 100, 25, $('#AAcceptUnit').combobox('getText'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 127, 100, 25, $('#AAcceptPeople').textbox('getValue'));//收货人
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var sphone = $('#AAcceptCellphone').textbox('getValue');
                    if (sphone == "") {
                        sphone = $('#AAcceptTelephone').textbox('getValue');
                    }
                    LODOP.ADD_PRINT_TEXT(112, 263, 130, 25, sphone);//填收货人联系电话
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var sendUnit = '湖南迪乐泰';
                    var sendPhone = '0731-86889941';
                    if (hous.indexOf('湖北') != -1) {
                        sendUnit = '湖北迪乐泰';
                        sendPhone = '17343331012';
                    }
                    if (hous.indexOf('西安') != -1) {
                        sendUnit = '西安新陆程';
                        sendPhone = '029-84524648';
                    }
                    if (hous.indexOf('梅州') != -1) {
                        sendUnit = '梅州新陆程';
                        sendPhone = '18088857730';
                    }
                    if (hous.indexOf('广州') != -1) {
                        sendUnit = '广州迪乐泰';
                        sendPhone = '13687469699';
                    }
                    if (hous.indexOf('海南') != -1) {
                        sendUnit = '海南迪乐泰';
                        sendPhone = '15120882670';
                    }
                    if (hous.indexOf('揭阳') != -1) {
                        sendUnit = '揭阳迪乐泰';
                        sendPhone = '13377790810';
                    }
                    if (hous.indexOf('广东') != -1) {
                        sendUnit = '广州迪乐泰';
                        sendPhone = '13687469699';
                    }
                    if (hous.indexOf('四川') != -1) {
                        sendUnit = '四川迪乐泰';
                        sendPhone = '18122771967';
                    }
                    if (hous.indexOf('广通慧采') != -1) {
                        sendUnit = '广通慧采';
                        sendPhone = '18122106189';
                    }
                    if (hous.indexOf('沈阳') != -1) {
                        sendUnit = '广州迪乐泰';
                        sendPhone = '13687469699';
                    }
                    LODOP.ADD_PRINT_TEXT(112, 419, 94, 25, sendUnit);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 532, 100, 25, $('#SaleManName').val());//填业务员
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 626, 127, 25, sendPhone);//业务员的联系电话$('#SaleCellPhone').val()
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


                    LODOP.ADD_PRINT_TEXT(135, 10, 47, 24, "编号");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 85, 51, 24, "代码");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 176, 63, 24, "规格");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 302, 45, 24, "花纹");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 401, 80, 24, "速度级别");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 494, 58, 24, "单价");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 577, 63, 24, "数量");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 677, 53, 24, "总价");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_LINE(153, 3, 152, 791, 0, 1);


                    LODOP.ADD_PRINT_TEXT(397, 16, 54, 25, "总计");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(397, 565, 69, 19, AllPiece);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(397, 667, 105, 19, Alltotal);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(416, 13, 61, 25, "总 计");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(416, 241, 350, 25, atoc(Alltotal));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(436, 16, 66, 22, "制表：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 84, 100, 19, $('#CreateAwb').textbox('getValue'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 313, 61, 22, "仓库：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 421, 100, 19, "");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 570, 67, 22, "司机：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 660, 100, 22, "");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(460, 11, 58, 28, "备 注");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    LODOP.ADD_PRINT_TEXT(456, 72, 357, 29, $('#ARemark').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var daili = '';
                    if (hous.indexOf('湖北') != -1) {
                        daili = '迪乐泰湖北轮胎总代理';
                        LODOP.ADD_PRINT_TEXT(436, 662, 100, 19, "17771448223");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    }
                    if (hous.indexOf('西安') != -1) {
                        daili = '优科豪马轮胎西安新陆程总代理';
                    }
                    if (hous.indexOf('梅州') != -1) {
                        daili = '马牌轮胎新陆程总代理';
                    }
                    if (hous.indexOf('广东') != -1) {
                        daili = '广州迪乐泰';
                    }
                    if (hous.indexOf('广州') != -1) {
                        daili = '广州迪乐泰轮胎总代理';
                    }
                    if (hous.indexOf('海南') != -1) {
                        daili = '海南迪乐泰轮胎总代理';
                    }
                    if (hous.indexOf('四川') != -1) {
                        daili = '四川迪乐泰轮胎总代理';
                    }
                    if (hous.indexOf('广通慧采') != -1) {
                        daili = '广通慧采';
                    }
                    LODOP.ADD_PRINT_TEXT(460, 566, 189, 30, daili);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    var beizu = '注：第一联结算联（白）、第二联收货人留存（红）、第三联对账联（黄）';
                    if (hous.indexOf('湖北') != -1) {
                        beizu = '湖北优科豪马轮胎代理迪乐泰公司感谢于您的合作，谢谢！';
                    }
                    LODOP.ADD_PRINT_TEXT(484, 11, 645, 26, beizu);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                }

                LODOP.ADD_PRINT_LINE(176 + (i - (p - 2) * 10) * 23, 3, 175 + (i - (p - 2) * 10) * 23, 791, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 58, 134 + (i - (p - 2) * 10) * 23, 59, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 148, 134 + (i - (p - 2) * 10) * 23, 149, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 269, 134 + (i - (p - 2) * 10) * 23, 270, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 390, 134 + (i - (p - 2) * 10) * 23, 391, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 479, 134 + (i - (p - 2) * 10) * 23, 480, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 562, 134 + (i - (p - 2) * 10) * 23, 563, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 654, 134 + (i - (p - 2) * 10) * 23, 655, 0, 1);

                js++;
                pie = Number(griddata[i].Piece);
                total = Number(pie) * Number(griddata[i].ActSalePrice);

                LODOP.ADD_PRINT_TEXT(156 + (i - (p - 2) * 10) * 23, 21, 36, 23, js);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 70, 90, 23, griddata[i].Model);//型号
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 159, 100, 23, griddata[i].Specs);//规格 
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 276, 130, 23, griddata[i].Figure);//花纹
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 403, 90, 23, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 490, 85, 23, griddata[i].ActSalePrice);//单价销售价
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 573, 95, 23, pie);//数量出库件数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 671, 105, 23, total);//总价
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            }
            LODOP.PREVIEW();
        }


        //退货单打印
        function pickUpOrder() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();
            LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "A4");
            var hous = '<%= HouseName%>';
            var com = '湖南省迪乐泰贸易有限公司退货单';
            if (hous.indexOf('湖北') != -1) {
                com = '湖北省迪乐泰贸易有限公司退货单';
            }
            if (hous.indexOf('西安') != -1) {
                com = '陕西省西安新陆程物流有限公司退货单';
            }
            if (hous.indexOf('四川') != -1) {
                com = '四川迪乐泰贸易有限公司退货单';
            }
            LODOP.ADD_PRINT_TEXT(4, 253, 485, 33, com);
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "<img src=\"../CSS/image/dlqf.jpg\"/>");
            LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);

            LODOP.ADD_PRINT_TEXT(41, 120, 70, 20, "订单号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 180, 110, 20, $('#OrderNo').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_TEXT(41, 450, 75, 20, "收货人：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 510, 105, 20, $('#AAcceptPeople').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 565, 90, 20, "联系电话：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            var tell = $('#AAcceptCellphone').textbox('getValue');
            if (tell == '' || tell == null) { tell = $('#AAcceptTelephone').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(41, 638, 117, 20, tell);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            //LODOP.ADD_PRINT_RECT(66, 3, 100, 25, 0, 1);
            //LODOP.ADD_PRINT_RECT(66, 102, 80, 25, 0, 1);
            //LODOP.ADD_PRINT_RECT(66, 181, 100, 25, 0, 1);
            //LODOP.ADD_PRINT_RECT(66, 280, 100, 25, 0, 1);

            //LODOP.ADD_PRINT_RECT(66, 379, 65, 25, 0, 1);//速度级别
            //LODOP.ADD_PRINT_RECT(66, 443, 60, 25, 0, 1);//出库件数
            //LODOP.ADD_PRINT_RECT(66, 512, 85, 25, 0, 1);//货位代码
            //LODOP.ADD_PRINT_RECT(66, 656, 132, 25, 0, 1);//所在区域

            //LODOP.ADD_PRINT_TEXT(70, 21, 78, 25, "产品名称");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 118, 52, 25, "型号");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 202, 65, 25, "规格");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 292, 53, 25, "花纹");

            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 386, 64, 25, "出库件数");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 477, 78, 25, "货位代码");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 580, 72, 25, "所在区域");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 672, 78, 25, "所在仓库");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);


            LODOP.ADD_PRINT_RECT(66, 3, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 102, 79, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 181, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 280, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 379, 64, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 443, 69, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 512, 74, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 586, 96, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 682, 106, 25, 0, 1);

            LODOP.ADD_PRINT_TEXT(70, 21, 78, 25, "产品名称");
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
            //LODOP.ADD_PRINT_TEXT(70, 520, 78, 25, "货位代码");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 604, 72, 25, "所在区域");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 690, 78, 25, "所在仓库");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

            LODOP.ADD_PRINT_TEXT(70, 522, 60, 25, "批次");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 590, 88, 25, "货位代码");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 684, 72, 25, "所在区域");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

            var griddata = $('#dgSave').datagrid('getRows');
            LODOP.ADD_PRINT_TEXT(41, 290, 90, 20, "所在仓库：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 370, 80, 20, griddata[0].HouseName);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            var js = 0, Alltotal = 0, AllPiece = 0;
            for (var i = 0; i < griddata.length; i++) {
                js++;
                var p = Number(griddata[i].Piece);

                LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 102, 79, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 181, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 280, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 379, 64, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 443, 69, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 512, 74, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 586, 96, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 682, 106, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 111, 23, griddata[i].ProductName);//产品名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 110, 67, 20, griddata[i].Model);//型号
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 185, 94, 20, griddata[i].Specs);//规格 
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 286, 82, 20, griddata[i].Figure);//花纹
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 386, 82, 20, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 450, 51, 20, p);//数量出库件数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                //LODOP.ADD_PRINT_TEXT(95 + i * 25, 520, 106, 20, griddata[i].ContainerCode);//货位代码
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                //LODOP.ADD_PRINT_TEXT(95 + i * 25, 602, 69, 20, griddata[i].AreaName);//所在区域
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                //LODOP.ADD_PRINT_TEXT(95 + i * 25, 688, 121, 20, griddata[i].HouseName);//所在仓库
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                LODOP.ADD_PRINT_TEXT(95 + i * 25, 520, 100, 20, griddata[i].Batch);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 590, 106, 20, griddata[i].ContainerCode);//货位代码
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 684, 69, 20, griddata[i].AreaName);//所在区域
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            }
            LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


            LODOP.PREVIEW();
        }
    </script>

</asp:Content>
