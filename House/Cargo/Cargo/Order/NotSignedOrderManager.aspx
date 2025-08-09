<%@ Page Title="未签收管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotSignedOrderManager.aspx.cs" Inherits="Cargo.Order.NotSignedOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //页面加载时执行
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
        //页面加载显示遮罩层防止用户看见未加载CSS的页面
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //加载完成后关闭遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }

        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    {
                        title: '订单号', field: 'OrderNo', width: '95px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '物流单号', field: 'LogisAwbNo', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '物流公司', field: 'LogisticName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
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
                            else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                            else if (val == "7") { return "<span title='正在配送'>正在配送</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '出发站', field: 'Dep', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '件数', field: 'Piece', width: '35px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '140px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收货人', field: 'AcceptPeople', width: '95px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系手机', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收货地址', field: 'AcceptAddress', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter },
                    { title: '出库时间', field: 'OutCargoTime', width: '125px', formatter: DateTimeFormatter },
                    {
                        title: '备注', field: 'Remark', width: '250px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '出库仓库', field: 'OutHouseName', width: '75px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                onLoadSuccess: function (data) {
                    $('#dg').datagrid('reloadFooter', [{ ActSalePrice: '合计', Piece: Piece, TransportFee: TransportFee, OrderType: '合计' }]);

                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
            });

            //列表回车响应查询
            $("#saPanelTab").keydown(function (e) { if (e.keyCode == 13) { dosearch(); } });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));


            //所在仓库
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#PID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                }
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#HouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
        });

        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryNotSignedOrderData';
            $('#dg').datagrid('load', {
                OrderNo: $('#OrderNo').val(),
                LogisAwbNo: $("#LogisAwbNo").val(),
                AcceptUnit: $("#AcceptUnit").val(),
                AcceptPeople: $("#AcceptPeople").val(),
                HouseID: $("#HouseID").combobox('getValues').toString(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $("#EndDate").datebox('getValue')
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
    <%--此div用于在界面未完全加载样式前显示内容--%>
        <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%"/>
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="OrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px"/>
                </td>
                <td style="text-align: right;">物流单号:
                </td>
                <td>
                    <input id="LogisAwbNo" class="easyui-textbox" data-options="prompt:'请输入物流单号'" style="width: 100px"/>
                </td>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AcceptUnit" class="easyui-textbox" data-options="prompt:'请输入客户名称'" style="width: 100px"/>
                </td>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'请输入收货人名称'" style="width: 100px"/>
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HouseID" class="easyui-combobox" style="width: 100px;" panelheight="auto" data-options="multiple:true" />
                </td>
                <td style="text-align: right;">下单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px"/>~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px"/>
                </td>
            </tr>
        </table>
    </div>
    <%--此div用于显示查询条件--%>
    <table id="dg" class="easyui-datagrid">
    </table>
    <%--此div用于显示按钮操作--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="Export()">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnNotSignedOrder" runat="server" Style="display: none;" Text="导出" OnClick="btnNotSignedOrder_Click" />
        </form>
    </div>
    <%--此div用于显示按钮操作--%>    
    <script type="text/javascript">
        function Export() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出！', 'warning'); return; }
            var obj = document.getElementById("<%=btnNotSignedOrder.ClientID %>"); obj.click();
        }
    </script>
</asp:Content>
