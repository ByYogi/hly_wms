<%@ Page Title="销售利润统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportSaleProfit.aspx.cs" Inherits="Cargo.Report.reportSaleProfit" %>
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
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))));
            $('#dg').datagrid({ height: height-15 });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                //height: '300px',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                rownumbers: true,
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    {
                        title: '项目', field: 'TypeName', width: '220px', align: 'Center', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '数量', field: 'Piece', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '金额', field: 'ActSalePrice', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                groupField: 'OrderType',
                view: groupview,
                groupFormatter: function (value, rows) {
                    var CountPiece = 0;
                    var CountPrice = 0;
                    for (var i = 0; i < rows.length; i++) {
                        CountPiece += rows[i].Piece;
                        CountPrice += rows[i].ActSalePrice;
                    }
                    if (value == "销售收入") {
                        str += "销售收入 ";
                        CountIncome = CountPrice;
                        return value + ' - 总件数：' + CountPiece + " - 总销售收入：" + CountPrice.toFixed(2) + ' 元';
                    } else if (value == "销售成本") {
                        str += " - 销售成本";
                        CountCost = CountPrice;
                        return value + ' - 总件数：' + CountPiece + " - 总销售成本：" + CountPrice.toFixed(2) + ' 元';
                    } else if (value == "管理费用") {
                        str += " - 管理费用";
                        CountManage = CountPrice;
                        return value + ' - 总次数：' + CountPiece + " - 总管理费用：" + CountPrice.toFixed(2) + ' 元';
                    } else if (value == "销售费用") {
                        str += " - 销售费用";
                        CountSale = CountPrice;
                        return value + ' - 总次数：' + CountPiece + " - 总销售费用：" + CountPrice.toFixed(2) + ' 元';
                    } else if (value == "其它收入") {
                        str += " + 其它收入";
                        OtherIncome = CountPrice;
                        return value + ' - 总次数：' + CountPiece + " - 总其它收入：" + CountPrice.toFixed(2) + ' 元';
                    } else if (value == "其它成本") {
                        str += " - 其它成本";
                        OtherCost = CountPrice;
                        return value + ' - 总次数：' + CountPiece + " - 总其它成本：" + CountPrice.toFixed(2) + ' 元';
                    } else if (value == "合计利润") {
                        return value + " ：" + str + " = " + (CountIncome - CountCost - CountManage - CountSale - OtherCost + OtherIncome).toFixed(2) + ' 元';
                    }
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { },
                onLoadSuccess: function (data) {
                    $("#dg").datagrid("mergeCells", { index: data.total-1, colspan: 3, field: "TypeName" });
                }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            CountIncome = 0;
            CountCost = 0;
            CountManage = 0;
            CountSale = 0;
            OtherIncome = 0;
            OtherCost = 0;
            str = "";
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QuerySaleProfitData';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue')
            });
        }
        //导出
        function Export() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var obj = document.getElementById("<%=btn.ClientID %>"); obj.click();
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
                <td style="text-align: right;">开单时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px"/>~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px"/>
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" panelheight="auto" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
        <a href="#" id="btnExport" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="Export()">&nbsp;导出&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btn" runat="server" Style="display: none;" Text="导出" OnClick="btn_Click" />
        </form>
    </div>
</asp:Content>
