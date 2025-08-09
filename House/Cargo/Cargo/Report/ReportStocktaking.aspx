<%@ Page Title="仓库盘点统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportStocktaking.aspx.cs" Inherits="Cargo.Report.ReportStocktaking" %>
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
        $(document).ready(function () {
            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
        });
        //查询
        function dosearch() {
            $('#dg').treegrid('loadData', { total: 0, rows: [], footer: [] });
            $('#dg').treegrid({
                width: '100%',
                height: '450px',
                title: '按仓库区域进行盘点', //标题内容
                loadMsg: '数据加载中请稍候...',
                url: null,
                method: 'post',
                rownumbers: true,
                showFooter: true,
                idField: 'id',
                treeField: 'region',
                columns: [[
                  { title: '区域', field: 'region', width: '150px' },
                  { title: '轮胎(条)', field: 'f1', width: '80px' }
                ]]
            });
            var gridOpts = $('#dg').treegrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryHouseStocktaking';
            $('#dg').treegrid('load', {
                HID: $("#HID").combobox('getValue')
            });
        }
        ///按产品查询统计
        function productQuery() {
            $('#dg').treegrid('loadData', { total: 0, rows: [], footer: [] });
            $('#dg').treegrid({
                width: '100%',
                height: '450px',
                title: '按产品类型进行盘点', //标题内容
                loadMsg: '数据加载中请稍候...',
                url: null,
                method: 'post',
                rownumbers: true,
                showFooter: true,
                idField: 'id',
                treeField: 'TypeName',
                columns: [[
                  { title: '产品分类', field: 'TypeName', width: '150px' },
                  { title: '在库数量', field: 'InPiece', width: '80px' }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { }
            });
            var gridOpts = $('#dg').treegrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryHouseStockByType';
            $('#dg').treegrid('load', {
                HID: $("#HID").combobox('getValue')
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
    <div name="SelectDiv2" style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" style="color: Red;" iconcls="icon-chart_bar" plain="false" href="../Report/ReportStocktaking.aspx"
                        target="_self">&nbsp;仓库盘点&nbsp;</a>&nbsp;&nbsp;<a id="saleManReport" class="easyui-linkbutton" iconcls="icon-chart_bar"
                            plain="false" href="../Report/reportDayStockStatis.aspx" target="_self">
                            &nbsp;每日库存统计&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">仓库名称:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 150px;"
                        panelheight="auto" />
                </td>
                <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;按区域查询&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="productQuery()">&nbsp;按产品查询&nbsp;</a></td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-treegrid">
    </table>
    <script type="text/javascript">
    </script>

</asp:Content>
