<%@ Page Title="微信商城数据统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportWeixinMall.aspx.cs" Inherits="Cargo.Report.reportWeixinMall" %>


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
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            setDatagrid();
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

        });
        //查询
        function dosearch() {
            if ($('#StartDate').datebox('getValue') == undefined || $('#StartDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择查询时间', 'warning');
                return;
            }
            if ($('#EndDate').datebox('getValue') == undefined || $('#EndDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择查询时间', 'warning');
                return;
            }
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryWeixinMalReportData';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue')
            });
            adjustment();
        }
        function setDatagrid(mulCol) {
            $('#dg').datagrid({
                width: '100%',
                //height: '600px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ReportDate',
                rownumbers: true,
                toolbar: '#toolbar',
                showFooter: true,
                columns: [[
                  { title: '日期', field: 'ReportDate', width: '70px', rowspan: 2 },
                  { title: '湖南仓库', colspan: 4, width: '80px' },
                  { title: '湖北仓库', colspan: 4, width: '80px' },
                  { title: '揭阳仓库', colspan: 4, width: '80px' },
                  { title: '广州仓库', colspan: 4, width: '80px' },
                  { title: '海南仓库', colspan: 4, width: '80px' },
                  { title: '梅州仓库', colspan: 4, width: '80px' },
                  { title: '西北仓库', colspan: 4, width: '80px' },
                  { title: '汇总', colspan: 4, width: '80px' }
                ],
                [
                  { title: "客户数", field: 'HNOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'HNOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'HNSaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'HNSaleTotalCharge', rowspan: 1 },
                  { title: "客户数", field: 'HBOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'HBOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'HBSaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'HBSaleTotalCharge', rowspan: 1 },
                  { title: "客户数", field: 'JYOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'JYOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'JYSaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'JYSaleTotalCharge', rowspan: 1 },
                  { title: "客户数", field: 'GZOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'GZOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'GZSaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'GZSaleTotalCharge', rowspan: 1 },
                  { title: "客户数", field: 'HAOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'HAOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'HASaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'HASaleTotalCharge', rowspan: 1 },
                  { title: "客户数", field: 'MZOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'MZOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'MZSaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'MZSaleTotalCharge', rowspan: 1 },
                  { title: "客户数", field: 'XBOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'XBOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'XBSaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'XBSaleTotalCharge', rowspan: 1 },
                  { title: "客户数", field: 'DayOrderClientNum', rowspan: 1 },
                  { title: "订单数", field: 'DayOrderNum', rowspan: 1 },
                  { title: "轮胎数", field: 'DaySaleTyreNum', rowspan: 1 },
                  { title: "总金额", field: 'DaySaleTotalCharge', rowspan: 1 }
                ]
                ]
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table>
            <tr>
                <td style="text-align: right;">查询时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a><%--&nbsp;&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
            plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" /><asp:HiddenField ID="hiddenID" runat="server" Value="0" />
        </form>--%>
    </div>
    <script type="text/javascript">
        //导出数据
        <%-- function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#StartDate').datebox('getValue');
            key[1] = $('#EndDate').datebox('getValue');
            key[2] = $('#ASpecs').val();
            key[3] = $('#AFigure').val();
            key[4] = $("#APID").combobox('getValue');
            key[5] = $("#ASID").combobox('getValue');
            key[6] = $("#AHouseID").combobox('getValue');//仓库ID
            $.ajax({
                url: "reportApi.aspx?method=QuerySaleStockDataForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }--%>


    </script>
</asp:Content>
