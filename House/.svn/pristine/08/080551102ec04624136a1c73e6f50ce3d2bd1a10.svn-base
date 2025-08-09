<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dealerOrderManger.aspx.cs" Inherits="Cargo.Order.dealerOrderManger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
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

        /*流程图样式*/
        .processBar {
            float: left;
            width: 100px;
            margin-top: 15px;
        }

            .processBar .bar {
                background: rgb(230, 224, 236);
                height: 3px;
                position: relative;
                width: 100px;
                margin-left: 10px;
            }

            .processBar .b-select {
                background: rgb(96, 72, 124);
            }

            .processBar .bar .c-step {
                position: absolute;
                width: 8px;
                height: 8px;
                border-radius: 50%;
                background: rgb(230, 224, 236);
                left: -12px;
                top: 50%;
                margin-top: -4px;
            }

            .processBar .bar .c-select {
                width: 10px;
                height: 10px;
                margin: -5px -1px;
                background: rgb(96, 72, 124);
            }

        .main-hide {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }

        .poetry {
            color: rgb(41, 41, 41);
            font-family: KaiTi_GB2312, KaiTi, STKaiti;
            font-size: 16px;
            background-color: transparent;
            font-weight: bold;
            font-style: normal;
            text-decoration: none;
        }

        button {
            width: 80px;
            line-height: 30px;
            font-size: 11px;
            color: rgb(116, 42, 149);
            text-align: center;
            border-radius: 6px;
            border: 1px solid #e2e2e2;
            cursor: pointer;
            background-color: #fff;
            outline: none;
        }

            button:hover {
                border: 1px solid rgb(179, 161, 200);
            }
    </style>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
    <script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"></script>
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
            //$('#btnApprove').hide();
            $('#btnTag').hide();
            RoleCName = "<%=UserInfor.RoleCName%>";
            var HID = '<%=UserInfor.HouseID%>';
            $('#HiddenHouseID').val(HID);
            var columns = [];
            $("td.ThrowGood").hide();
            $("td.AcceptPeople").hide();
            $("td.AcceptUnit").hide();
            columns.push({
                title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '费用', field: 'TransportFee', width: '80px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            columns.push({
                title: '客户名称', field: 'PayClientName', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '收货人', field: 'AcceptPeople', width: '55px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID != 10) {
                columns.push({
                    title: '业务员', field: 'SaleManName', width: '55px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '订单状态', field: 'AwbStatus', width: '60px',
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
            columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter });
            if (HID == 10) {
                columns.push({
                    title: '订单类型', field: 'ThrowGood', width: '60px',
                    formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='普送单'>普送单</span>"; }
                        else if (val == "17") { return "<span title='急送单'>急送单</span>"; }
                        else { return ""; }
                    }
                });
            }
            if (HID != 10) {
                columns.push({
                    title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='未结算'>未结算</span>"; }
                        else if (val == "1") { return "<span title='已结算'>已结算</span>"; }
                        else if (val == "2") { return "<span title='未结清'>未结清</span>"; }
                        else { return ""; }
                    }
                });
            }

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
                        title: '出库仓库', field: 'OutHouseName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
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
                    if (row.AwbStatus == 0 || HID == "82") {
                        $('#btnTag').hide();
                    } else {
                        $('#btnTag').show();
                    }

                },
                rowStyler: function (index, row) {
                    if (row.AwbStatus == "5") { return "color:#2a83de"; };
                    if (row.TrafficType == "2") { return "background-color:#b3ce7e"; };
                    if (row.ThrowGood == "17") { return "background-color:#f38f8f"; };
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });
            var datenow = new Date();
            //$('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            //$('#Dep').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            //$('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            //$('#AOrderType').combobox('textbox').bind('focus', function () { $('#AOrderType').combobox('showPanel'); });
            //$('#CheckOutType').combobox('textbox').bind('focus', function () { $('#CheckOutType').combobox('showPanel'); });

            //$('#ASaleManID').combobox('textbox').bind('focus', function () { $('#ASaleManID').combobox('showPanel'); });
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });

            //所在仓库
            $('#AHouseID').combobox({ url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name', });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

            if (HID == "82") {
                $('#ASaleManID').combobox('clear');
                var salemancombo = [{ 'UserName': '柯观文', 'LoginName': '2521' }, { 'UserName': '丁志强', 'LoginName': '2522' }, { 'UserName': '戴继贤', 'LoginName': '2902' }, { 'UserName': '何佳文', 'LoginName': '2986' }, { 'UserName': '胡高飞', 'LoginName': '2760' }, { 'UserName': '卓孝钱', 'LoginName': '2825' }, { 'UserName': '黄勇', 'LoginName': '2961' }, { 'UserName': '方良柏', 'LoginName': '2826' }, { 'UserName': '陈坚文', 'LoginName': '2534' }, { 'UserName': '黄智芬', 'LoginName': '2542' }];
                $("#ASaleManID").combobox("loadData", salemancombo);
                $('#ASaleManID').combobox('setValue', <%=UserInfor.LoginName%>);
                if (<%=UserInfor.IsAdmin%>!= 1) {
                    $("#ASaleManID").combobox("readonly", true);
                    $('#ASaleManID').combobox('textbox').unbind('focus');
                    $('#ASaleManID').combobox('textbox').css('background-color', '#e8e8e8');
                }
                $('#ThrowGood').textbox('setValue', '21');
            }
            else if (HID == "10" && RoleCName.indexOf("安泰路斯") >= 0) {
                $('#ASaleManID').combobox('clear');
                var salemancombo = [{ 'UserName': '安鹏', 'LoginName': '2368' }];
                $("#ASaleManID").combobox("loadData", salemancombo);
                $('#ASaleManID').combobox('setValue', '2368');
                $("td.ASaleManID").hide();

            }
            else {
                $('#ASaleManID').combobox('clear');
                var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=<%=UserInfor.HouseID%>';
                $('#ASaleManID').combobox('reload', url);
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
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                FinanceSecondCheck: '-1',
                CheckOutType: '',
                ThrowGood: $('#ThrowGood').val(),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                SaleManID: $('#ASaleManID').combobox('getValue'),
                //CreateAwb: $('#ACreateAwb').textbox('getValue'),
                //FirstID: $("#PID").combobox('getValue'),//父ID
                AcceptUnit: $('#AcceptUnit').val(),
                OrderModel: "0",//订单类型
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
                <td class="ThrowGood">
                    <input class="easyui-textbox" id="ThrowGood"></td>
                <td style="text-align: right;">订单号:
                </td>
                <td>

                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px">
                </td>
                <td class="ASaleManID" style="text-align: right;">业务员:
                </td>
                <td class="ASaleManID">
                    <input id="ASaleManID" class="easyui-combobox" style="width: 100px;" data-options="valueField: 'LoginName',textField: 'UserName'" />
                </td>
                <td class="AcceptUnit" style="text-align: right;">客户名称:
                </td>
                <td class="AcceptUnit">
                    <input id="AcceptUnit" class="easyui-textbox" data-options="prompt:'请输入客户名称'" style="width: 100px">
                </td>
                <td class="AcceptPeople" style="text-align: right;">收货人:
                </td>
                <td class="AcceptPeople">
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
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
                        <%-- <option value="4">已到达</option>--%>
                        <option value="5">已签收</option>
                        <option value="7">异常</option>
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
    <input type="hidden" id="HiddenHouseID" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnOrderStatus" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="updateOrderStatus()">&nbsp;订单跟踪&nbsp;</a>&nbsp;&nbsp;<a href="#" id="btnExportOrder" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BatchExportOrderInfo()">&nbsp;导出订单列表&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnTag" class="easyui-linkbutton" iconcls="icon-tag_blue" plain="false" onclick="queryTag()">&nbsp;出库标签&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />
        </form>
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 90%; height: 555px;" closed="true"
        closable="false" buttons="#dlgOrder-buttons">
        <table id="dgSave" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlgOrder-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>


    <!--订单状态跟踪-->
    <div id="dlgStatus" class="easyui-dialog" style="width: 900px; height: 650px; padding: 1px 1px"
        closed="true" closable="false" buttons="#dlgStatus-buttons">
        <form id="fmStatus" class="easyui-form" method="post">
            <input type="hidden" name="OrderID" id="SOrderID" />
            <input type="hidden" name="OrderNo" id="SOrderNo" />
            <input type="hidden" name="WXOrderNo" id="SWXOrderNo" />
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
                        <td class="OrderStatusTd" align="left">&nbsp;&nbsp;状态：
                        </td>
                        <td align="left" class="OrderStatusTd">
                            <input name="OrderStatus" id="AOrderStatus5" type="radio" value="6"><label for="AOrderStatus5" style="font-size: 14px;">已拣货</label>
                            <input name="OrderStatus" id="AOrderStatus1" type="radio" value="2"><label for="AOrderStatus1" style="font-size: 14px;" id="AOrderStatus1Show">已出库</label>
                            <input name="OrderStatus" id="AOrderStatus2" type="radio" value="3"><label for="AOrderStatus2" style="font-size: 14px;">已装车</label>
                            <%-- <input name="OrderStatus" id="AOrderStatus3" type="radio" value="4"><label for="AOrderStatus3" style="font-size: 14px;">已到达</label>--%>
                            <input name="OrderStatus" id="AOrderStatus4" type="radio" value="5"><label for="AOrderStatus4" style="font-size: 14px;" id="AOrderStatus4Show">已签收</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" class="OrderStatusTd">
                            <textarea name="DetailInfo" id="DetailInfo" cols="60" style="height: 20px; width: 95%; resize: none" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true" style="height: 220px; width: 860px">
                                <div title="订单跟踪" id="lblTrack" data-options="iconCls:'icon-page_add'"></div>
                                <div title="好来运跟踪" id="lblTrack2" data-options="iconCls:'icon-page_add'"></div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <div id="map" <%-- style="width: 100%; height: 63%"--%>>
        </div>
    </div>
    <div id="dlgStatus-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--订单状态跟踪-->
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 600px; overflow: hidden; display: flex; justify-content: center; align-items: center;">
        <img id="simg" style="max-width: 100%; max-height: 170%;" />
    </div>
    <!--Begin 查询产品标签列表-->
    <div id="productTag" class="easyui-dialog" style="width: 1000px; height: 520px; padding: 2px 2px"
        closed="true" buttons="#productTag-buttons">
        <table id="dgTag" class="easyui-datagrid">
        </table>
        <div id="productTag-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#productTag').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>

    <script type="text/javascript">
        //复选框只能选中一个
        $(function () {
            $('#OrderModel').find('input[type=checkbox]').bind('click', function () {
                var id = $(this).attr("id");
                if (this.checked) {
                    $("#OrderModel").find('input[type=checkbox]').not(this).attr("checked", false);
                }
            });
        })
        //查询产品标签数据列表 
        function queryTag() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查询标签的订单！', 'warning');
                return;
            }
            if (row) {
                $('#productTag').dialog('open').dialog('setTitle', '查询订单：' + row.OrderNo + '出库标签列表');
                $('#HOrderID').val(row.OrderID);
                showTagGrid();
                $('#dgTag').datagrid('clearSelections');
                var gridOpts = $('#dgTag').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryTagByOrderNo&OrderNo=' + row.OrderNo;
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
                    { title: '订单号', field: 'OrderNo', width: '80px' },
                    { title: '轮胎码', field: 'TyreCode', width: '80px' },
                    { title: '标签编码', field: 'TagCode', width: '80px' },
                    { title: '入库时间', field: 'InCargoTime', width: '120px', formatter: DateTimeFormatter },
                    { title: '出库时间', field: 'OutCargoTime', width: '120px', formatter: DateTimeFormatter },
                    { title: '出库人', field: 'OutCargoOperID', width: '60px' },
                    { title: '产品ID', field: 'ProductID', width: '50px' },
                    { title: '规格', field: 'Specs', width: '70px' },
                    { title: '花纹', field: 'Figure', width: '80px' },
                    { title: '批次', field: 'Batch', width: '50px' },
                    { title: '型号', field: 'Model', width: '60px' },
                    { title: '货品代码', field: 'GoodsCode', width: '80px' },
                    { title: '载重指数', field: 'LoadIndex', width: '60px' },
                    { title: '速度级别', field: 'SpeedLevel', width: '60px' },
                    { title: '货位代码', field: 'ContainerCode', width: '80px' },
                    { title: '一级区域', field: 'ParentAreaName', width: '60px' },
                    { title: '二级区域', field: 'AreaName', width: '60px' }
                ]],
                onClickRow: function (index, row) {
                    $('#dgTag').datagrid('clearSelections');
                    $('#dgTag').datagrid('selectRow', index);
                }
            });
        }
        //批量导出订单信息
        function BatchExportOrderInfo() {
            $.ajax({
                url: "orderApi.aspx?method=QueryOrderInfoExport&OrderNo=" + $('#AOrderNo').val() + "&LogisAwbNo=&AcceptPeople=&Piece=&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&FinanceSecondCheck=-1&CheckOutType=''&ThrowGood=" + $('#ThrowGood').val() + "&OrderType=&AwbStatus=" + $("#AAwbStatus").combobox('getValue') + "&Dep=&Dest=&HouseID=" + $("#AHouseID").combobox('getValue') + "&SaleManID=" + $('#ASaleManID').combobox('getValue') + "&CreateAwb=&OutHouseName=&AcceptUnit=&OrderModel=0",
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnOrderInfo.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }

        //修改订单跟踪状态
        function updateOrderStatus() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查看跟踪状态的数据！', 'info');
                return;
            }
           <%-- if (row.OrderStatus == "5") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单：' + row.OrderNo + '已签收，不能修改', 'info');
                return;
            }--%>
            $(".OrderStatusTd").hide();

            $('#saveStatus').hide();

            if (row) {
                $('#dlgStatus').dialog('open').dialog('setTitle', '查看订单：' + row.OrderNo + ' 跟踪状态');
                $('#fmStatus').form('clear');
                $('#SOrderID').val(row.OrderID);
                $('#SOrderNo').val(row.OrderNo);
                $('#SWXOrderNo').val(row.WXOrderNo);
                $('#lblTrack').empty();
                $('#lblTrack2').empty();
                $.ajax({
                    async: false,
                    url: "orderApi.aspx?method=QueryOrderStatus&OrderNo=" + row.OrderNo + "&HouseID=" + row.HouseID + "&LogisAwbNo=" + row.LogisAwbNo + "&ThrowGood=" + row.ThrowGood + "&BelongHouse=" + row.BelongHouse + "&TrafficType=" + row.TrafficType + "&LogisID=" + row.LogisID,
                    cache: false,
                    dataType: "json",
                    success: function (text) {
                        var ldl = document.getElementById("lblTrack");
                        ldl.innerHTML = text.HtmlStr;
                        var ldl2 = document.getElementById("lblTrack2");
                        ldl2.innerHTML = text.HtmlStr2;
                        if (text.StartLatitude != "" && text.StartLatitude != null && text.StartLongitude != "" && text.StartLongitude != null && text.StartTime != "" && text.StartTime != null && text.EndLatitude != "" && text.EndLatitude != null && text.EndLongitude != "" && text.EndLongitude != null && text.EndTime != "" && text.EndTime != null) {
                            openMap(text.StartLatitude, text.StartLongitude, text.StartTime, text.EndLatitude, text.EndLongitude, text.EndTime, text.Status, text.Distance);
                        } else if (text.StartLatitude != "" && text.StartLatitude != null && text.StartLongitude != "" && text.StartLongitude != null && text.StartTime != "" && text.StartTime != null && text.EndLatitude != "" && text.EndLatitude != null && text.EndLongitude != "" && text.EndLongitude != null) {
                            openMap(text.StartLatitude, text.StartLongitude, text.StartTime, text.EndLatitude, text.EndLongitude, "", text.Status, text.Distance);
                        }
                    }
                });
            }
        }
        function restore() {
            var bounds = new TMap.LatLngBounds();

            //判断标注点是否在范围内
            markerArr.forEach(function (item) {
                //若坐标点不在范围内，扩大bounds范围
                if (bounds.isEmpty() || !bounds.contains(item.position)) {
                    bounds.extend(item.position);
                }
            })
            //设置地图可视范围
            map.fitBounds(bounds, {
                padding: 70 // 自适应边距
            });
        }
        function openMap(StartLat, StartLng, StartTime, EndLat, EndLng, EndTime, Status, Distance) {
            $("#map").css("width", "100%");
            $("#map").css("height", $("#dlgStatus").height() - $("#fmStatus").height());
            $('#map').append('<input type="button" id="restore" onclick="restore()" value="复位" style="right: 31px;height: 33px;width: 39px;top: 178px;z-index: 10000;background-color: #ffffff;border: none;border-radius: 3px;outline: none;position:absolute" />');
            var center = new TMap.LatLng(StartLat, StartLng);
            start = new TMap.LatLng(StartLat, StartLng);
            end = new TMap.LatLng(EndLat, EndLng);
            startTime = StartTime.substring(5, 16);
            endTime = EndTime == "" ? "" : EndTime.substring(5, 16);
            status = Status;
            distance = Distance;
            speed = parseInt(Distance / 3)
            var Datetime1 = new Date();
            var NewTime = Datetime1.getTime();
            var Datetime2 = new Date(StartTime);
            var OldTime = Datetime2.getTime();
            var Time = NewTime - OldTime;
            hour = parseInt(Time / 1000 / 60);//分钟
            //初始化地图
            map = new TMap.Map('map', {
                center: center,
                zoom: 17.5	//缩放级别
            });

            //WebServiceAPI请求URL（驾车路线规划默认会参考实时路况进行计算）
            var url = "https://apis.map.qq.com/ws/direction/v1/driving/"; //请求路径
            url += "?from=" + StartLat + "," + StartLng; //起点坐标
            url += "&to=" + EndLat + "," + EndLng;  //终点坐标
            url += "&output=jsonp&callback=cb";	//指定JSONP回调函数名，本例为cb
            url += "&key=CMZBZ-64RLS-7FROW-6SNUF-C3P5Z-DGB2J"; //开发key，可在控制台自助创建


            //发起JSONP请求，获取路线规划结果
            jsonp_request(url);
        }
        //浏览器调用WebServiceAPI需要通过Jsonp的方式，此处定义了发送JOSNP请求的函数
        function jsonp_request(url) {
            var script = document.createElement('script');
            script.src = url;
            document.body.appendChild(script);
        }

        //定义请求回调函数，在此拿到计算得到的路线，并进行绘制
        function cb(ret) {
            var coords = ret.result.routes[0].polyline, pl = [], path = [];
            //坐标解压（返回的点串坐标，通过前向差分进行压缩）
            var kr = 1000000;
            for (var i = 2; i < coords.length; i++) {
                coords[i] = Number(coords[i - 2]) + Number(coords[i]) / kr;
            }
            var position = hour < 2160 ? parseInt(coords.length / 2160 * hour) : parseInt(coords.length * 0.7) + hour;
            //将解压后的坐标放入点串数组pl中
            for (var i = 0; i < coords.length; i += 2) {
                if (status == 1) {

                }
                else if (status == 2) {
                    if (endTime == '' && i < position) {
                        path.push(new TMap.LatLng(coords[i], coords[i + 1]));
                    }
                }
                else if (status == 5) {
                    path.push(new TMap.LatLng(coords[i], coords[i + 1]));
                }
                pl.push(new TMap.LatLng(coords[i], coords[i + 1]));
            }
            markerArr = [{
                "id": 'start',
                "styleId": 'start',
                "position": start
            }];
            if (status == 1) {
            }
            else if (status == 2) {
                markerArr.push({
                    "id": 'end',
                    "styleId": 'end2',
                    "position": end
                }, {
                    id: 'car',
                    styleId: 'car-down',
                    position: start,
                });
            }
            else if (status == 5) {
                markerArr.push({
                    "id": 'end',
                    "styleId": 'end',
                    "position": end
                }, {
                    id: 'car',
                    styleId: 'car-down',
                    position: start,
                });
            }

            if (status > 1) {
                display_polyline(pl)//显示路线
            }

            //标记起终点marker
            var marker = new TMap.MultiMarker({
                id: 'marker-layer',
                map: map,
                styles: {
                    'car-down': new TMap.MarkerStyle({
                        'width': 40,
                        'height': 40,
                        'anchor': {
                            x: 20,
                            y: 20,
                        },
                        'faceTo': 'map',
                        'rotate': 180,
                        'src': 'https://mapapi.qq.com/web/lbs/javascriptGL/demo/img/car.png',
                    }),
                    "start": new TMap.MarkerStyle({
                        "width": 30,
                        "height": 45,
                        "anchor": { x: 16, y: 32 },
                        "src": 'https://i.loli.net/2021/03/22/1xa7ErotXd2li8I.png'
                    }),
                    "end": new TMap.MarkerStyle({
                        "width": 30,
                        "height": 45,
                        "anchor": { x: 16, y: 32 },
                        "src": 'https://i.loli.net/2021/03/22/HkaCIm4zFZvLYR5.png'
                    }),
                    "end2": new TMap.MarkerStyle({
                        "width": 30,
                        "height": 45,
                        "anchor": { x: 16, y: 32 },
                        "src": 'https://i.loli.net/2021/03/25/kYFrUsIAKfN9SWn.png'
                    })
                },
                geometries: markerArr
            });
            marker.moveAlong({ 'car': { path, speed: speed } }, { autoRotation: true });

            if (status > 1) {
                //初始化
                var bounds = new TMap.LatLngBounds();

                //判断标注点是否在范围内
                markerArr.forEach(function (item) {
                    //若坐标点不在范围内，扩大bounds范围
                    if (bounds.isEmpty() || !bounds.contains(item.position)) {
                        bounds.extend(item.position);
                    }
                })
                //设置地图可视范围
                map.fitBounds(bounds, {
                    padding: 70 // 自适应边距
                });
            }
            var geometries = [{
                'id': 'label1', //点图形数据的标志信息
                'styleId': 'style', //样式id
                'position': start, //标注点位置
                'content': startTime, //标注文本
                'properties': { //标注点的属性数据
                    'title': 'label'
                }
            }]
            if (status == 5) {
                geometries.push({
                    'id': 'label', //点图形数据的标志信息
                    'styleId': 'style', //样式id
                    'position': end, //标注点位置
                    'content': endTime, //标注文本
                    'properties': { //标注点的属性数据
                        'title': 'label'
                    }
                })
            }

            //初始化label
            var label = new TMap.MultiLabel({
                id: 'label-layer',
                map: map,
                styles: {
                    'style': new TMap.LabelStyle({
                        'color': '#d5371a', //颜色属性
                        'size': 18, //文字大小属性
                        'offset': { x: 0, y: 10 }, //文字偏移属性单位为像素
                        'angle': 0, //文字旋转属性
                        'alignment': 'center', //文字水平对齐属性
                        'verticalAlignment': 'middle' //文字垂直对齐属性
                    })
                },
                geometries: geometries
            });
        }

        function display_polyline(pl) {
            //创建 MultiPolyline显示折线
            var polylineLayer = new TMap.MultiPolyline({
                id: 'polyline-layer', //图层唯一标识
                map: map,//绘制到目标地图
                //折线样式定义
                styles: {
                    'style_blue': new TMap.PolylineStyle({
                        'color': '#3777FF', //线填充色
                        'width': 4, //折线宽度
                        'borderWidth': 5, //边线宽度
                        'borderColor': '#FFF', //边线颜色
                        'lineCap': 'round', //线端头方式
                    })
                },
                //折线数据定义
                geometries: [
                    {
                        'id': 'pl_1',//折线唯一标识，删除时使用
                        'styleId': 'style_blue',//绑定样式名
                        'paths': pl
                    }
                ]
            });
        }
        function destroy() {
            map.destroy();
        }
        function download(img) {
            var simg = img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);

        }
        //关闭订单状态跟踪弹出框
        function closeDlgStatus() {
            $('#dlgStatus').dialog('close');
            $('#dg').datagrid('reload');
            map.destroy();
        }
        //双击显示订单详细界面 
        function editItemByID(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            var row = $("#dg").datagrid('getData').rows[Did];
            rowHouseID = row.HouseID;
            //$('#postpone').show();
            if (row) {
                $('#dgSaveAwbStatus').val(row.AwbStatus);
                $('#dlgOrder').dialog('open').dialog('setTitle', '查看订单：' + row.OrderNo + " 出库明细");

                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
                columns.push({ title: '出库数量', field: 'Piece' });
                columns.push({ title: '销售价格', field: 'ActSalePrice' });
                columns.push({ title: '品牌', field: 'TypeName' });
                columns.push({ title: '货品代码', field: 'GoodsCode' });
                columns.push({ title: '规格', field: 'Specs' });
                columns.push({ title: '花纹', field: 'Figure' });
                columns.push({ title: '批次', field: 'Batch' });
                columns.push({
                    title: '载速', field: 'LoadIndex', formatter: function (value, row) {
                        return value + row.SpeedLevel;
                    }
                });
                //columns.push({ title: '速度级别', field: 'SpeedLevel', width: '50px' });
                //columns.push({ title: '载重指数', field: 'LoadIndex', width: '50px' });
                columns.push({ title: '型号', field: 'Model' });


                columns.push({ title: '货位代码', field: 'ContainerCode' });
                columns.push({ title: '所在区域', field: 'AreaName' });
                columns.push({ title: '所在仓库', field: 'FirstAreaName' });
                columns.push({ title: '产品ID', field: 'ProductID' });
                $('#dgSave').datagrid({
                    width: '99%',
                    height: 480,
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
                    columns: [columns]
                });
                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;

            }
        }

        //关闭弹出框
        function closeDlg() {
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
    </script>

</asp:Content>
